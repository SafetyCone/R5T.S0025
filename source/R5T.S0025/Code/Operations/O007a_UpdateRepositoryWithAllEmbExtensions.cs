using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

using R5T.D0101;
using R5T.D0105;
using R5T.D0108;
using R5T.D0109;
using R5T.D0110;
using R5T.T0020;
using R5T.T0102;

using R5T.S0025.Library;


namespace R5T.S0025
{
    /// <summary>
    /// Updates the list of extension method base extensions, selected those that are not in ignored projects, or on ignored extension method bases.
    /// Skips the "selection" process for extension method base extensions.
    /// </summary>
    [OperationMarker]
    public class O007a_UpdateRepositoryWithAllEmbExtensions : IActionOperation
    {
        private IExtensionMethodBaseExtensionDiscoveryRepository ExtensionMethodBaseExtensionDiscoveryRepository { get; }
        private IExtensionMethodBaseExtensionRepository ExtensionMethodBaseExtensionRepository { get; }
        private IExtensionMethodBaseRepository ExtensionMethodBaseRepository { get; }
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private IProjectRepository ProjectRepository { get; }
        private ISummaryFilePathProvider SummaryFilePathProvider { get; }


        public O007a_UpdateRepositoryWithAllEmbExtensions(
            IExtensionMethodBaseExtensionDiscoveryRepository extensionMethodBaseExtensionDiscoveryRepository,
            IExtensionMethodBaseExtensionRepository extensionMethodBaseExtensionRepository,
            IExtensionMethodBaseRepository extensionMethodBaseRepository,
            INotepadPlusPlusOperator notepadPlusPlusOperator,
            IProjectRepository projectRepository,
            ISummaryFilePathProvider summaryFilePathProvider)
        {
            this.ExtensionMethodBaseExtensionDiscoveryRepository = extensionMethodBaseExtensionDiscoveryRepository;
            this.ExtensionMethodBaseExtensionRepository = extensionMethodBaseExtensionRepository;
            this.ExtensionMethodBaseRepository = extensionMethodBaseRepository;
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
            this.ProjectRepository = projectRepository;
            this.SummaryFilePathProvider = summaryFilePathProvider;
        }

        public async Task Run()
        {
            // Backup should be run in calling operation.

            // Load data.
            var projects = await this.ProjectRepository.GetAllProjects();
            var extensionMethodBases = await this.ExtensionMethodBaseRepository.GetAllExtensionMethodBases();
            var ignoredProjectIdentities = await this.ExtensionMethodBaseExtensionDiscoveryRepository.GetIgnoredProjectIdentities();
            var ignoredExtensionMethodBaseIdentities = await this.ExtensionMethodBaseExtensionDiscoveryRepository.GetIgnoredExtensionMethodBaseIdentities();

            var repositoryExtensionMethodBaseExtensions = await this.ExtensionMethodBaseExtensionRepository.GetAllExtensionMethodBaseExtensions();
            var repositoryToProjectMappings = await this.ExtensionMethodBaseExtensionRepository.GetAllToProjectMappings();
            var repositoryToExtensionMethodBaseMappings = await this.ExtensionMethodBaseExtensionRepository.GetAllToExtensionMethodBaseMappings();

            // Find all extensions for extension method bases in projects.
            var extensionMethodBaseExtensionTuples = await Instances.Operation.GetExtensionMethodBaseExtensionTuples(
                projects,
                extensionMethodBases,
                ignoredProjectIdentities,
                ignoredExtensionMethodBaseIdentities);

            var currentExtensionMethodBaseExtensions = extensionMethodBaseExtensionTuples.Select(x => x.ExtensionMethodBaseExtension).Now();

            // Get current to-X mappings.
            // First, fill in identities for all current extension method base extensions, either with existing identities or new identities.

            currentExtensionMethodBaseExtensions.FillIdentitiesFromSourceOrSetNew(repositoryExtensionMethodBaseExtensions);

            var currentToProjectMappings = extensionMethodBaseExtensionTuples
                .Select(x =>
                {
                    var output = new ExtensionMethodBaseExtensionToProjectMapping
                    {
                        ExtensionMethodBaseExtensionIdentity = x.ExtensionMethodBaseExtension.Identity,
                        ProjectIdentity = x.Project.Identity,
                    };

                    return output;
                })
                .Now();
            var currentToExtensionMethodBaseMappings = extensionMethodBaseExtensionTuples
                .Select(x =>
                {
                    var output = new ExtensionMethodBaseExtensionToExtensionMethodBaseMapping
                    {
                        ExtensionMethodBaseExtensionIdentity = x.ExtensionMethodBaseExtension.Identity,
                        ExtensionMethodBaseIdentity = x.ExtensionMethodBase.Identity,
                    };

                    return output;
                })
                .Now();

            // Determine new/departed.
            var (newExtensionMethodBaseExtensions, departedExtensionMethodBaseExtensions) = Instances.Operation.GetNewAndDepartedByNameAndFilePath(
                repositoryExtensionMethodBaseExtensions,
                currentExtensionMethodBaseExtensions);

            var (newToProjectMappings, departedToProjectMappings) = Instances.Operation.GetNewAndDepartedIdentityMappings(repositoryToProjectMappings, currentToProjectMappings);
            var (newToExtensionMethodBaseMappings, departedToExtensionMethodBaseMappings) = Instances.Operation.GetNewAndDepartedIdentityMappings(repositoryToExtensionMethodBaseMappings, currentToExtensionMethodBaseMappings);

            var analysisData = new AnalysisData
            {
                ProjectsByIdentity = projects.ToDictionaryByIdentity(),
                ExtensionMethodBasesByIdentity = extensionMethodBases.ToDictionaryByIdentity(),
                CurrentExtensionMethodBaseExtensionsByIdentity = currentExtensionMethodBaseExtensions.ToDictionaryByIdentity(),
                RepositoryExtensionMethodBaseExtensionsByIdentity = repositoryExtensionMethodBaseExtensions.ToDictionaryByIdentity(),
                NewExtensionMethodBaseExtensions = newExtensionMethodBaseExtensions,
                DepartedExtensionMethodBaseExtensions = departedExtensionMethodBaseExtensions,
                NewToProjectMappings = newToProjectMappings,
                DepartedToProjectMappings = departedToProjectMappings,
                NewToExtensionMethodBaseMappings = newToExtensionMethodBaseMappings,
                DepartedToExtensionMethodBaseMappings = departedToExtensionMethodBaseMappings
            };

            // Write summary.
            var summaryFilePath = await this.SummaryFilePathProvider.GetSummaryFilePath();

            using(var writer = TextWriterHelper.New(summaryFilePath))
            {
                Instances.Operation.WriteSummary(
                    writer,
                    analysisData);
            }

            // Now prompt for human actions.

            var humanActionsRequired = analysisData.DetermineRequredHumanActions();

            await this.NotepadPlusPlusOperator.OpenFilePath(summaryFilePath);

            Console.WriteLine($"Review the summary file (which should be open in Notepad++):\n{summaryFilePath}\n");

            // * New extension method base extensions.
            if (humanActionsRequired.ReviewNewExtensionMethodBaseExtensions)
            {
                Console.WriteLine("=> Review the list of new extension method base extensions.\n");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No new extension method base extensions to review. (ok)");
            }
            Console.WriteLine();

            // * Departed extension method base extensions.
            if (humanActionsRequired.ReviewDepartedExtensionMethodBaseExtensions)
            {
                Console.WriteLine("=> Review the list of departed extension method base extensions.\n\nNote: departed extension method base extension names will be removed from the lists of ignored and selected extension method base extension names.\n");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No departed extension method base extenions to review. (ok)");
            }
            Console.WriteLine();

            if (humanActionsRequired.ReviewNewToExtensionMethodBaseMappings)
            {
                Console.WriteLine("=> Review the list of new extension method base extensions-to-extension method base mappings.\n");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No new extension method base extension-to-project mappings. (ok)");
            }
            Console.WriteLine();

            if (humanActionsRequired.ReviewDepartedToExtensionMethodBaseMappings)
            {
                Console.WriteLine("=> Review the list of departed extension method base extensions-to-extension method base mappings.\n");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No departed extension method base extension-to-project mappings. (ok)");
            }
            Console.WriteLine();

            if (humanActionsRequired.ReviewNewToProjectMappings)
            {
                Console.WriteLine("=> Review the list of new extension method base extension-to-project mappings.\n");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No new extension method base extension-to-project mappings. (ok)");
            }
            Console.WriteLine();

            if (humanActionsRequired.ReviewDepartedToProjectMappings)
            {
                Console.WriteLine("=> Review the list of departed extension method base extension-to-project mappings.\n");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No departed extension method base extension-to-project mappings. (ok)");
            }
            Console.WriteLine();

            // Any mandatory?
            // None are mandatory.

            Console.WriteLine("The extension method base extensions repository will now be updated.");
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();

            // Now update the repository.
            await this.ExtensionMethodBaseExtensionRepository.DeleteExtensionMethodBaseExtensions(analysisData.DepartedExtensionMethodBaseExtensions);
            await this.ExtensionMethodBaseExtensionRepository.AddExtensionMethodBaseExtensions(analysisData.NewExtensionMethodBaseExtensions);

            await this.ExtensionMethodBaseExtensionRepository.DeleteToProjectMappings(analysisData.DepartedToProjectMappings);
            await this.ExtensionMethodBaseExtensionRepository.AddToProjectMappings(analysisData.NewToProjectMappings);

            await this.ExtensionMethodBaseExtensionRepository.DeleteToExtensionMethodBaseMappings(analysisData.DepartedToExtensionMethodBaseMappings);
            await this.ExtensionMethodBaseExtensionRepository.AddToExtensionMethodBaseMappings(analysisData.NewToExtensionMethodBaseMappings);
        }
    }
}
