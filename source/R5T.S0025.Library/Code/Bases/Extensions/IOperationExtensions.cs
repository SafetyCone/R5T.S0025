using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using R5T.Magyar;

using R5T.D0084.D001;
using R5T.D0101;
using R5T.D0108;
using R5T.D0109;
using R5T.T0097;
using R5T.T0098;
using R5T.T0092;
using R5T.T0100;
using R5T.T0102;

using R5T.S0025.Library;

using Instances = R5T.S0025.Library.Instances;

using R5T.D0037;


namespace System
{
    public record ExtensionMethodBaseExtensionProjectedTuple(ExtensionMethodBaseExtension ExtensionMethodBaseExtension, ExtensionMethodBase ExtensionMethodBase, Project Project);


    public static class IOperationExtensions
    {
        public static IEnumerable<NamedIdentified> GetEmbExtensionFunctionalityNames(this IOperation _,
            IEnumerable<(ExtensionMethodBaseExtension EmbExtension, ExtensionMethodBase Emb, Project Project)> tuples)
        {
            var firstEmbExtensionIdentiesByProjectedBasedMethodName = tuples
                .GroupBy(xTuple => Instances.MethodNameOperator.GetTypeBasedProjectedMethodName(xTuple))
                .Select(xGroup => new NamedIdentified()
                {
                    Name = xGroup.Key,
                    // Just use the identity of the first in a group of duplicates by type-based, projected, method name since that will be good enough to deliver the project identity for an extension method base extension identity.
                    Identity = xGroup.First().EmbExtension.Identity,
                })
                ;

            return firstEmbExtensionIdentiesByProjectedBasedMethodName;
        }

        public static async Task UpdateEmbExtensionsRepository(this IOperation _,
            AnalysisOutputData analysisOutputData,
            IExtensionMethodBaseExtensionRepository extensionMethodBaseExtensionRepository)
        {
            // Extension method bases.
            await extensionMethodBaseExtensionRepository.AddExtensionMethodBaseExtensions(analysisOutputData.NewEmbExtensions);
            await extensionMethodBaseExtensionRepository.DeleteExtensionMethodBaseExtensions(analysisOutputData.DepartedEmbExtensions);

            // To extension method base mappings.
            await extensionMethodBaseExtensionRepository.DeleteToExtensionMethodBaseMappings(analysisOutputData.InvalidToEmbMappings);
            await extensionMethodBaseExtensionRepository.DeleteToExtensionMethodBaseMappings(analysisOutputData.DepartedToEmbMappings);
            await extensionMethodBaseExtensionRepository.AddToExtensionMethodBaseMappings(analysisOutputData.NewToEmbMappings);

            // To project mappings.
            await extensionMethodBaseExtensionRepository.DeleteToProjectMappings(analysisOutputData.InvalidToProjectMappings);
            await extensionMethodBaseExtensionRepository.DeleteToProjectMappings(analysisOutputData.DepartedToProjectMappings);
            await extensionMethodBaseExtensionRepository.AddToProjectMappings(analysisOutputData.NewToProjectMappings);
        }

        public static void SetRequiredHumanActions(this IOperation _,
            AnalysisOutputData analysisData,
            HumanActionsRequired humanActionsRequired)
        {
            var anyNewEmbExtensions = analysisData.NewEmbExtensions.Any();

            humanActionsRequired.ReviewNewEmbExtensions = anyNewEmbExtensions;

            var anyNewToEmbMappings = analysisData.NewToEmbMappings.Any();

            humanActionsRequired.ReviewNewToEmbMappings = anyNewToEmbMappings;

            var anyNewToProjectMappings = analysisData.NewToProjectMappings.Any();

            humanActionsRequired.ReviewNewToProjectMappings = anyNewToProjectMappings;


            var anyDepartedEmbExtensions = analysisData.DepartedEmbExtensions.Any();

            humanActionsRequired.ReviewDepartedEmbExtensions = anyDepartedEmbExtensions;

            var anyDepartedToEmbMappings = analysisData.DepartedToEmbMappings.Any();

            humanActionsRequired.ReviewDepartedToEmbMappings = anyDepartedToEmbMappings;

            var anyDepartedToProjectMappings = analysisData.DepartedToProjectMappings.Any();

            humanActionsRequired.ReviewDepartedToProjectMappings = anyDepartedToProjectMappings;


            var anyUnmappableToEmbEmbExtensions = analysisData.EmbExtensionsUnmappableToEmbs.Any();

            humanActionsRequired.ReviewEmbExtensionsUnmappableToEmbs = anyUnmappableToEmbEmbExtensions;


            var anyUnmappedToEmbEmbExtensions = analysisData.EmbExtensionsUnmappedToEmb.Any();

            humanActionsRequired.ReviewEmbExtensionsUnmappedToEmb = anyUnmappedToEmbEmbExtensions;

            var anyInvalidToEmbMappings = analysisData.InvalidToEmbMappings.Any();

            humanActionsRequired.ReviewInvalidToEmbMappings = anyInvalidToEmbMappings;


            var anyUnmappedToProjectEmbExtensions = analysisData.EmbExtensionsUnmappedToProject.Any();

            humanActionsRequired.ReviewEmbExtensionsUnmappedToProject = anyUnmappedToProjectEmbExtensions;

            var anyInvalidToProjectMappings = analysisData.InvalidToProjectMappings.Any();

            humanActionsRequired.ReviewInvalidToProjectMappings = anyInvalidToProjectMappings;
        }

        public static void WriteSummary(this IOperation _,
            StreamWriter summaryFile,
            AnalysisInputData inputData,
            AnalysisOutputData analysisData)
        {
            // New and departed extension method bases.
            var newExtensionMethodBaseExtensions = analysisData.NewEmbExtensions;
            var departedExtensionMethodBaseExtensions = analysisData.DepartedEmbExtensions;

            _.WriteNewAndDepartedExtensionMethodBaseExtensions(
                summaryFile,
                newExtensionMethodBaseExtensions,
                departedExtensionMethodBaseExtensions);

            // Deal with any extenion method bases that cannot be mapped.
            // Unmappable EMB extensions.
            var unmappableEmbExtensions = inputData.EmbExtensionsUnmappableToEmbs;

            var unmappableEmbExtensionCount = unmappableEmbExtensions.Length;

            summaryFile.WriteLine($"Unmappable extension method bases");
            summaryFile.WriteLine($"{unmappableEmbExtensionCount}: count of extension method base extensions unmappable to an extension method base.");
            summaryFile.WriteLine();

            if (unmappableEmbExtensions.None())
            {
                summaryFile.WriteLine("<none> (good)");
            }
            else
            {
                foreach (var unmappedEmbExtension in unmappableEmbExtensions)
                {
                    summaryFile.WriteLine(unmappedEmbExtension.AsNamedFilePathedRepresentation());
                }
            }
            summaryFile.WriteLine();
            summaryFile.WriteLine("Unmappable extensions should be manually mapped.");
            summaryFile.WriteLine("***\n");

            // Unmapped-to-EMB EMB extensions.
            var embExtensionsUnmappedToEmb = analysisData.EmbExtensionsUnmappedToEmb;

            var unmappedEmbExtensionCount = embExtensionsUnmappedToEmb.Length;

            summaryFile.WriteLine($"Extensions unmapped to a base");
            summaryFile.WriteLine($"{unmappedEmbExtensionCount}: count of extension method base extensions unmapped to an extension method base (out of {inputData.CurrentEmbExtensions.Length} extension method base extensions).");
            summaryFile.WriteLine();

            if (embExtensionsUnmappedToEmb.None())
            {
                summaryFile.WriteLine("<none> (good)");
            }
            else
            {
                foreach (var embExtension in embExtensionsUnmappedToEmb)
                {
                    summaryFile.WriteLine(embExtension.AsNamedFilePathedRepresentation());
                }
            }
            summaryFile.WriteLine();
            summaryFile.WriteLine("Unmapped extensions should be manually mapped.");
            summaryFile.WriteLine("***\n");

            // Invalid to-EMB mappings.
            var invalidToEmbMappings = analysisData.InvalidToEmbMappings;

            var invalidToEmbMappingsCount = invalidToEmbMappings.Length;

            summaryFile.WriteLine($"Invalid to-extension method base mappings");
            summaryFile.WriteLine($"{invalidToEmbMappingsCount}: count of invalid to extension method base mappings.");
            summaryFile.WriteLine();

            var currentEmbExtensionsByIdentity = inputData.CurrentEmbExtensions.ToDictionaryByIdentity();
            var embsByIdentity = inputData.ExtensionMethodBases.ToDictionaryByIdentity();

            if (invalidToEmbMappings.None())
            {
                summaryFile.WriteLine("<none> (good)");
            }
            else
            {
                foreach (var mapping in invalidToEmbMappings)
                {
                    var hasEmbExtension = currentEmbExtensionsByIdentity.HasValue(mapping.ExtensionMethodBaseExtensionIdentity);
                    var hasEmb = embsByIdentity.HasValue(mapping.ExtensionMethodBaseIdentity);

                    summaryFile.WriteLine(mapping.ToStringRepresentation());
                    summaryFile.WriteLine($"\tMissing extension method base extension: {hasEmbExtension.Exists.Invert().YesOrNo()} ({mapping.ExtensionMethodBaseExtensionIdentity})");
                    summaryFile.WriteLine($"\tMissing extension method base: {hasEmb.Exists.Invert().YesOrNo()} ({mapping.ExtensionMethodBaseIdentity})");
                }
            }
            summaryFile.WriteLine();
            summaryFile.WriteLine("Invalid to-extension method base mappings will be removed.");
            summaryFile.WriteLine("***\n");

            var repositoryEmbExtensionsByIdentity = inputData.RepositoryEmbExtensions.ToDictionaryByIdentity();

            _.WriteNewAndDepartedToExtensionMethodBaseMappings(
                summaryFile,
                analysisData.NewToEmbMappings,
                analysisData.DepartedToEmbMappings,
                embsByIdentity,
                currentEmbExtensionsByIdentity,
                repositoryEmbExtensionsByIdentity);

            // Unmapped and invalid extension method to project mappings.
            var embExtensionsUnmappedToProject = analysisData.EmbExtensionsUnmappedToProject;

            var unmappedToProjectEmbExtensionMappingsCount = embExtensionsUnmappedToProject.Length;

            summaryFile.WriteLine("Extensions unmapped to a project");
            summaryFile.WriteLine($"{unmappedToProjectEmbExtensionMappingsCount}: count of extensions unmapped to a project:");
            summaryFile.WriteLine();

            if (embExtensionsUnmappedToProject.None())
            {
                summaryFile.WriteLine("<none> (good)");
            }
            else
            {
                foreach (var embExtension in embExtensionsUnmappedToProject)
                {
                    summaryFile.WriteLine(embExtension.AsNamedFilePathedRepresentation());
                }
            }
            summaryFile.WriteLine();
            summaryFile.WriteLine("Unmapped extensions should be manually mapped.");
            summaryFile.WriteLine("***\n");

            // Invalid to-project mappings.
            var invalidToProjectMappings = analysisData.InvalidToProjectMappings;

            var invalidToProjectEmbExtensionMappingsCount = invalidToProjectMappings.Length;

            summaryFile.WriteLine("Invalid to-project mappings");
            summaryFile.WriteLine($"({invalidToProjectEmbExtensionMappingsCount}): count of invalid to project mappings.");
            summaryFile.WriteLine();

            var projectsByIdentity = inputData.Projects.ToDictionaryByIdentity();

            if (invalidToProjectMappings.None())
            {
                summaryFile.WriteLine("<none> (good)");
            }
            else
            {
                foreach (var mapping in invalidToProjectMappings)
                {
                    var hasEmbExtension = currentEmbExtensionsByIdentity.HasValue(mapping.ExtensionMethodBaseExtensionIdentity);
                    var hasEmb = projectsByIdentity.HasValue(mapping.ProjectIdentity);

                    summaryFile.WriteLine(mapping.ToStringRepresentation());
                    summaryFile.WriteLine($"\tMissing extension method base extension: {hasEmbExtension.Exists.Invert().YesOrNo()} ({mapping.ExtensionMethodBaseExtensionIdentity})");
                    summaryFile.WriteLine($"\tMissing project: {hasEmb.Exists.Invert().YesOrNo()} ({mapping.ProjectIdentity})");
                }
            }
            summaryFile.WriteLine();
            summaryFile.WriteLine("Invalid to-project mappings will be removed.");
            summaryFile.WriteLine("***\n");

            _.WriteNewAndDepartedToProjectMappings(
                summaryFile,
                analysisData.NewToProjectMappings,
                analysisData.DepartedToProjectMappings,
                projectsByIdentity,
                currentEmbExtensionsByIdentity,
                repositoryEmbExtensionsByIdentity);
        }

        public static void WriteSummary(this IOperation _,
            TextWriter writer, 
            AnalysisData analysisData)
        {
            _.WriteNewAndDepartedExtensionMethodBaseExtensions(
                writer,
                analysisData.NewExtensionMethodBaseExtensions,
                analysisData.DepartedExtensionMethodBaseExtensions);

            _.WriteNewAndDepartedToProjectMappings(
                writer,
                analysisData.NewToProjectMappings,
                analysisData.DepartedToProjectMappings,
                analysisData.ProjectsByIdentity,
                analysisData.CurrentExtensionMethodBaseExtensionsByIdentity,
                analysisData.RepositoryExtensionMethodBaseExtensionsByIdentity);

            _.WriteNewAndDepartedToExtensionMethodBaseMappings(
                writer,
                analysisData.NewToExtensionMethodBaseMappings,
                analysisData.DepartedToExtensionMethodBaseMappings,
                analysisData.ExtensionMethodBasesByIdentity,
                analysisData.CurrentExtensionMethodBaseExtensionsByIdentity,
                analysisData.RepositoryExtensionMethodBaseExtensionsByIdentity);
        }

        public static void WriteNewAndDepartedToProjectMappings(this IOperation _,
            TextWriter writer,
            IList<ExtensionMethodBaseExtensionToProjectMapping> newToProjectMappings,
            IList<ExtensionMethodBaseExtensionToProjectMapping> departedToProjectMappings,
            IDictionary<Guid, Project> projectsByIdentity,
            IDictionary<Guid, ExtensionMethodBaseExtension> currentEmbExtensionsByIdentity,
            IDictionary<Guid, ExtensionMethodBaseExtension> repositoryEmbExtensionsByIdentity)
        {
            // New to-project mappings.
            var newToProjectMappingsCount = newToProjectMappings.Count;

            writer.WriteLine("New to-project mappings");
            writer.WriteLine($"({newToProjectMappingsCount}): count of new to-project mappings.");
            writer.WriteLine();

            if (newToProjectMappings.None())
            {
                writer.WriteLine("<none> (ok)");
            }
            else
            {
                foreach (var mapping in newToProjectMappings)
                {
                    var embExtension = currentEmbExtensionsByIdentity[mapping.ExtensionMethodBaseExtensionIdentity];
                    var project = projectsByIdentity[mapping.ProjectIdentity];

                    writer.WriteLine($"{embExtension.NamespacedTypedParameterizedMethodName}: {project.Name}, ({mapping.ToStringRepresentation()})");
                }
            }
            writer.WriteLine();
            writer.WriteLine("New to-project mappings will be added.");
            writer.WriteLine("***\n");

            // Departed to-project mappings.
            var departedToProjectMappingsCount = departedToProjectMappings.Count;

            writer.WriteLine("Departed to-project mappings");
            writer.WriteLine($"({departedToProjectMappingsCount}): count of departed to-project mappings.");
            writer.WriteLine();

            if (departedToProjectMappings.None())
            {
                writer.WriteLine("<none> (ok)");
            }
            else
            {
                foreach (var mapping in departedToProjectMappings)
                {
                    var embExtension = repositoryEmbExtensionsByIdentity[mapping.ExtensionMethodBaseExtensionIdentity]; // Use repsitory, not current.
                    var projectName = projectsByIdentity.ContainsKey(mapping.ProjectIdentity)
                        ? projectsByIdentity[mapping.ProjectIdentity].Name
                        : "<Project no longer exists>"
                        ;

                    writer.WriteLine($"{embExtension.NamespacedTypedParameterizedMethodName}: {projectName}, ({mapping.ToStringRepresentation()})");
                }
            }
            writer.WriteLine();
            writer.WriteLine("Departed to-project mappings will be added.");
            writer.WriteLine("***\n");
        }

        public static void WriteNewAndDepartedToExtensionMethodBaseMappings(this IOperation _,
            TextWriter writer,
            IList<ExtensionMethodBaseExtensionToExtensionMethodBaseMapping> newToEmbMappings,
            IList<ExtensionMethodBaseExtensionToExtensionMethodBaseMapping> departedToEmbMappings,
            IDictionary<Guid, ExtensionMethodBase> embsByIdentity,
            IDictionary<Guid, ExtensionMethodBaseExtension> currentEmbExtensionsByIdentity,
            IDictionary<Guid, ExtensionMethodBaseExtension> repositoryEmbExtensionsByIdentity)
        {
            // New to-EMB mappings.
            var newToEmbMappingsCount = newToEmbMappings.Count;

            writer.WriteLine($"New to-extension method base mappings");
            writer.WriteLine($"{newToEmbMappingsCount}: count of new to-extension method base mappings.");
            writer.WriteLine();

            if (newToEmbMappings.None())
            {
                writer.WriteLine("<none> (ok)");
            }
            else
            {
                foreach (var mapping in newToEmbMappings)
                {
                    var embExtension = currentEmbExtensionsByIdentity[mapping.ExtensionMethodBaseExtensionIdentity];
                    var emb = embsByIdentity[mapping.ExtensionMethodBaseIdentity];

                    writer.WriteLine($"{embExtension.NamespacedTypedParameterizedMethodName}:{emb.NamespacedTypeName}, ({mapping.ToStringRepresentation()})");
                }
            }
            writer.WriteLine();
            writer.WriteLine("New to-extension method base mappings will be added.");
            writer.WriteLine("***\n");

            // Departed to-EMB mappings.
            var departedToEmbMappingsCount = departedToEmbMappings.Count;

            writer.WriteLine($"Departed to-extension method base mappings");
            writer.WriteLine($"{departedToEmbMappingsCount}: count of departed to-extension method base mappings.");
            writer.WriteLine();

            if (departedToEmbMappings.None())
            {
                writer.WriteLine("<none> (ok)");
            }
            else
            {
                foreach (var mapping in departedToEmbMappings)
                {
                    var hasEmbExtension = repositoryEmbExtensionsByIdentity.TryGetValue(mapping.ExtensionMethodBaseExtensionIdentity, out var embExtension); // Repository, not current.
                    var hasEmb = embsByIdentity.TryGetValue(mapping.ExtensionMethodBaseIdentity, out var emb);

                    var embExtensionName = hasEmbExtension
                        ? embExtension.NamespacedTypedParameterizedMethodName
                        : "<No name, departed>"
                        ;

                    var embName = hasEmb
                        ? emb.NamespacedTypeName
                        : "<No name, departed>"
                        ;

                    writer.WriteLine($"{embExtensionName}:{embName}, ({mapping.ToStringRepresentation()})");
                }
            }
            writer.WriteLine();
            writer.WriteLine("Departed to-extension method base mappings will be removed.");
            writer.WriteLine("***\n");
        }

        public static void WriteNewAndDepartedExtensionMethodBaseExtensions(this IOperation _,
            TextWriter writer,
            IList<ExtensionMethodBaseExtension> newExtensionMethodBaseExtensions,
            IList<ExtensionMethodBaseExtension> departedExtensionMethodBaseExtensions)
        {
            var newEmbExtensionCount = newExtensionMethodBaseExtensions.Count;

            writer.WriteLine($"New extension method base extensions ({newEmbExtensionCount}):");
            writer.WriteLine();

            if (newExtensionMethodBaseExtensions.None())
            {
                writer.WriteLine("<none> (ok)");
            }
            else
            {
                var pairs = newExtensionMethodBaseExtensions
                    .Select(x => (extensionTypedMethodName: Instances.MethodNameOperator.GetExtensionTypedMethodName(x.NamespacedTypedParameterizedMethodName), extensionMethodBaseExtension: x))
                    .OrderAlphabetically(x => x.extensionTypedMethodName)
                    ;

                foreach (var (extensionTypedMethodName, extensionMethodBaseExtension) in pairs)
                {
                    writer.WriteLine($"{extensionTypedMethodName}: {extensionMethodBaseExtension.NamespacedTypedParameterizedMethodName}\n{Strings.DoubleSpaces}{extensionMethodBaseExtension.CodeFilePath}");
                }
            }
            writer.WriteLine("\n***\n");

            var departedExtensionMethodBasesCount = departedExtensionMethodBaseExtensions.Count;

            writer.WriteLine();
            writer.WriteLine($"Departed extension method bases ({departedExtensionMethodBasesCount}):");
            writer.WriteLine();

            if (departedExtensionMethodBaseExtensions.None())
            {
                writer.WriteLine("<none> (ok)");
            }
            else
            {
                var pairs = departedExtensionMethodBaseExtensions
                    .Select(x => (extensionTypedMethodName: Instances.MethodNameOperator.GetExtensionTypedMethodName(x.NamespacedTypedParameterizedMethodName), extensionMethodBaseExtension: x))
                    .OrderAlphabetically(x => x.extensionTypedMethodName)
                    ;

                foreach (var (extensionTypedMethodName, extensionMethodBaseExtension) in pairs)
                {
                    writer.WriteLine($"{extensionTypedMethodName}: {extensionMethodBaseExtension.NamespacedTypedParameterizedMethodName}\n{Strings.DoubleSpaces}{extensionMethodBaseExtension.CodeFilePath}");
                }
            }
            writer.WriteLine("\n***\n");
        }

        public static AnalysisOutputData PerformAnalysis(this IOperation _,
            AnalysisInputData inputData)
        {
            // Now determine new and old extension method base extensions.
            // This is done by namespaced, typed, parameterized method name (but not namespaced, typed, extension parameter parameterized, so there is a possible hole here if pathologically, there are two different namespaced, typed extension method bases with the same type name used in the same file (via type aliases or differential using statements within separate namespace scopes).

            // Extension method base extensions, new and departed.
            var (newExtensionMethodBaseExtensions, departedExtensionMethodBaseExtensions) = _.GetNewAndDepartedByNameAndFilePath(
                inputData.RepositoryEmbExtensions,
                inputData.CurrentEmbExtensions);

            // Determine new, old, and invalid to extension method base mappings.
            var embExtensionsUnmappedToEmb = _.GetUnmappedEmbExtensions(inputData.CurrentEmbExtensions, inputData.ExtensionMethodBases, inputData.CurrentToEmbMappings).Now();
            var invalidToEmbMappings = _.GetInvalidMappings(inputData.RepositoryToEmbMappings, inputData.CurrentEmbExtensions, inputData.ExtensionMethodBases).Now(); // Note, EMB extension identiies have been filled in.

            var embExtensionsUnmappedToProject = _.GetUnmappedEmbExtensions(inputData.CurrentEmbExtensions, inputData.Projects, inputData.CurrentToProjectMappings).Now();
            var invalidToProjectMappings = _.GetInvalidMappings(inputData.CurrentToProjectMappings, inputData.CurrentEmbExtensions, inputData.Projects).Now();

            // To extension method base mappings, new and departed.
            var (newToEmbMappings, departedToEmbMappings) = _.GetNewAndDepartedIdentityMappings(inputData.RepositoryToEmbMappings, inputData.CurrentToEmbMappings);
            var (newToProjectMappings, departedToProjectMappings) = _.GetNewAndDepartedIdentityMappings(inputData.RepositoryToProjectMappings, inputData.CurrentToProjectMappings);

            var output = new AnalysisOutputData()
            {
                DepartedEmbExtensions = departedExtensionMethodBaseExtensions,
                DepartedToEmbMappings = departedToEmbMappings,
                DepartedToProjectMappings = departedToProjectMappings,
                EmbExtensionsUnmappableToEmbs = inputData.EmbExtensionsUnmappableToEmbs, // Just copy over from input data.
                EmbExtensionsUnmappedToEmb = embExtensionsUnmappedToEmb,
                EmbExtensionsUnmappedToProject = embExtensionsUnmappedToProject,
                InvalidToEmbMappings = invalidToEmbMappings,
                InvalidToProjectMappings = invalidToProjectMappings,
                NewEmbExtensions = newExtensionMethodBaseExtensions,
                NewToEmbMappings = newToEmbMappings,
                NewToProjectMappings = newToProjectMappings,
            };

            return output;
        }

        public static async Task<AnalysisInputData> GetAnalysisInputData(this IOperation _,
            IAllProjectDirectoryPathsProvider allProjectDirectoryPathsProvider,
            IExtensionMethodBaseExtensionRepository extensionMethodBaseExtensionRepository,
            IExtensionMethodBaseRepository extensionMethodBaseRepository,
            IProjectRepository projectRepository)
        {
            var gettingExtensionMethodBases = extensionMethodBaseRepository.GetAllExtensionMethodBases();
            var gettingProjects = projectRepository.GetAllProjects();
            var gettingRepositoryAndCurrentExtensionMethodBaseExtensions = _.GetRepositoryAndCurrentExtensionMethodBaseExtensions(
                allProjectDirectoryPathsProvider,
                extensionMethodBaseExtensionRepository);
            var gettingRepositoryToExtensionMethodBaseMappings = extensionMethodBaseExtensionRepository.GetAllToExtensionMethodBaseMappings();
            var gettingRepositoryToProjectMappings = extensionMethodBaseExtensionRepository.GetAllToProjectMappings();

            // Step 1.
            await Task.WhenAll(
                gettingExtensionMethodBases,
                gettingProjects,
                gettingRepositoryAndCurrentExtensionMethodBaseExtensions,
                gettingRepositoryToExtensionMethodBaseMappings,
                gettingRepositoryToProjectMappings);

            var extensionMethodBases = await gettingExtensionMethodBases;
            var projects = await gettingProjects;
            var (repositoryExtensionMethodBaseExtensions, currentExtensionMethodBaseExtensions) = await gettingRepositoryAndCurrentExtensionMethodBaseExtensions;
            var repositoryToExtensionMethodBaseMappings = await gettingRepositoryToExtensionMethodBaseMappings;
            var repositoryToProjectMappings = await gettingRepositoryToProjectMappings;

            // Fill in identities for the current extension method base extensions, using identies from corresponding repository extension method base extensions if available, or generating new identies if not.
            // This is required for evaluating existing to-extension method base (and to-project) mappings below.
            currentExtensionMethodBaseExtensions.FillIdentitiesFromSourceOrSetNew(repositoryExtensionMethodBaseExtensions);

            // Map the current extension method base extensions to their extension method bases.
            var gettingEmbExtensionsToEmbMappings = _.MapEmbExtensionsToEmbs(
                currentExtensionMethodBaseExtensions,
                extensionMethodBases);

            // Step 2.
            var (currentToEmbMappings, unmappableEmbExtensions) = await gettingEmbExtensionsToEmbMappings;

            // Use existing project project mappings to increase speed (by just verifying that each existing project mapping is still valid instead of re-mapping).
            var (newToProjectMappings, oldToProjectMappings) = _.GetToProjectMappingChanges(
                currentExtensionMethodBaseExtensions,
                projects,
                repositoryToProjectMappings);

            var currentToProjectMappings = _.GetToProjectMappings(
                repositoryToProjectMappings,
                newToProjectMappings,
                oldToProjectMappings);

            var output = new AnalysisInputData()
            {
                CurrentToProjectMappings = currentToProjectMappings,
                CurrentEmbExtensions = currentExtensionMethodBaseExtensions,
                CurrentToEmbMappings = currentToEmbMappings,
                RepositoryEmbExtensions = repositoryExtensionMethodBaseExtensions,
                ExtensionMethodBases = extensionMethodBases,
                Projects = projects,
                RepositoryToEmbMappings = repositoryToExtensionMethodBaseMappings,
                RepositoryToProjectMappings = repositoryToProjectMappings,
                EmbExtensionsUnmappableToEmbs = unmappableEmbExtensions,
            };

            return output;
        }

        public static
            IEnumerable<(ExtensionMethodBaseExtension ExtensionMethodBaseExtension, ExtensionMethodBase ExtensionMethodBase, Project Project)>
        GetExtensionMethoBaseExtensionTuples(this IOperation _,
            IEnumerable<Project> projects,
            IEnumerable<ExtensionMethodBase> extensionMethodBases,
            IEnumerable<ExtensionMethodBaseExtension> extensionMethodBaseExtensions,
            IEnumerable<ExtensionMethodBaseExtensionToProjectMapping> toProjectMappings,
            IEnumerable<ExtensionMethodBaseExtensionToExtensionMethodBaseMapping> toEmbMappings)
        {
            var output = from embExtension in extensionMethodBaseExtensions
                         join toEmbMapping in toEmbMappings on embExtension.Identity equals toEmbMapping.ExtensionMethodBaseExtensionIdentity
                         join emb in extensionMethodBases on toEmbMapping.ExtensionMethodBaseIdentity equals emb.Identity
                         join toProjectMapping in toProjectMappings on embExtension.Identity equals toProjectMapping.ExtensionMethodBaseExtensionIdentity
                         join project in projects on toProjectMapping.ProjectIdentity equals project.Identity
                         select (embExtension, emb, project)
                    ;

            return output;
        }

        public static
            IEnumerable<(ExtensionMethodBaseExtension EmbExtension, ExtensionMethodBase Emb, Project Project)>
        GetEmbExtensionEmbAndProjectTuples(this IOperation _,
            IEnumerable<ExtensionMethodBaseExtension> extensionMethodBaseExtensions,
            IEnumerable<ExtensionMethodBase> extensionMethodBases,
            IEnumerable<Project> projects,
            IEnumerable<ExtensionMethodBaseExtensionToExtensionMethodBaseMapping> toEmbMappings,
            IEnumerable<ExtensionMethodBaseExtensionToProjectMapping> toProjectMappings)
        {
            var output = from embExtension in extensionMethodBaseExtensions
                    join toEmbMapping in toEmbMappings on embExtension.Identity equals toEmbMapping.ExtensionMethodBaseExtensionIdentity
                    join emb in extensionMethodBases on toEmbMapping.ExtensionMethodBaseIdentity equals emb.Identity
                    join toProjectMapping in toProjectMappings on embExtension.Identity equals toProjectMapping.ExtensionMethodBaseExtensionIdentity
                    join project in projects on toProjectMapping.ProjectIdentity equals project.Identity
                    select (embExtension, emb, project)
                    ;

            return output;
        }

        //public static
        //    IEnumerable<(ExtensionMethodBaseExtension ExtensionMethodBaseExtension, Project Project)>
        //GetEmbExtensionAndProjectTuples(this IOperation _,
        //    IEnumerable<ExtensionMethodBaseExtension> extensionMethodBaseExtensions,
        //    IEnumerable<Project> projects,
        //    IEnumerable<ExtensionMethodBaseExtensionToProjectMapping> toProjectMappings)
        //{

        //}

        public static
            IEnumerable<(ExtensionMethodBaseExtension ExtensionMethodBaseExtension, ExtensionMethodBase ExtensionMethodBase)>
        GetEmbExtensionAndEmbTuples(this IOperation _,
            IEnumerable<ExtensionMethodBaseExtension> embExtensions,
            IEnumerable<ExtensionMethodBase> embs,
            IEnumerable<ExtensionMethodBaseExtensionToExtensionMethodBaseMapping> toEmbMappings)
        {
            var embExtensionAndEmbTuples = _.GetEmbExtensionToEmbMappingTuples(
                embExtensions,
                toEmbMappings)
               .Join(embs,
                   xAggregate => xAggregate.ToEmbMapping.ExtensionMethodBaseIdentity,
                   xEmb => xEmb.Identity,
                   (xAggregate, xEmb) => (xAggregate.ExtensionMethodBaseExtension, xEmb))
               ;

            return embExtensionAndEmbTuples;
        }

        /// <summary>
        /// Get mappings referencing extension method base or extension method base extension identities not present in the respective set.
        /// </summary>
        public static
            IEnumerable<ExtensionMethodBaseExtensionToExtensionMethodBaseMapping>
        GetInvalidMappings(this IOperation _,
            IEnumerable<ExtensionMethodBaseExtensionToExtensionMethodBaseMapping> toEmbMappings,
            IEnumerable<ExtensionMethodBaseExtension> embExtensions,
            IEnumerable<ExtensionMethodBase> embs)
        {
            var invalidMappings = _.GetMappingsWithInvalidEmb(
                toEmbMappings,
                embs)
                .AppendRange(_.GetMappingsWithInvalidEmbExtension(
                    toEmbMappings,
                    embExtensions));

            return invalidMappings;
        }

        /// <summary>
        /// Get mappings referencing extension method base or extension method base extension identities not present in the respective set.
        /// </summary>
        public static
            IEnumerable<ExtensionMethodBaseExtensionToProjectMapping>
        GetInvalidMappings(this IOperation _,
            IEnumerable<ExtensionMethodBaseExtensionToProjectMapping> toProjectMappings,
            IEnumerable<ExtensionMethodBaseExtension> embExtensions,
            IEnumerable<Project> projects)
        {
            var invalidMappings = _.GetMappingsWithInvalidProject(
                toProjectMappings,
                projects)
                .AppendRange(_.GetMappingsWithInvalidEmbExtension(
                    toProjectMappings,
                    embExtensions));

            return invalidMappings;
        }

        /// <summary>
        /// Gets mappings with an extension method base extension identity not present in the set of extension method base extensions.
        /// </summary>
        public static
            IEnumerable<ExtensionMethodBaseExtensionToExtensionMethodBaseMapping>
        GetMappingsWithInvalidEmbExtension(this IOperation _,
            IEnumerable<ExtensionMethodBaseExtensionToExtensionMethodBaseMapping> embExtensionToEmbMappings,
            IEnumerable<ExtensionMethodBaseExtension> extensionMethodBaseExtensions)
        {
            var invalidMappings =
                from toEmbMapping in embExtensionToEmbMappings
                join embExtension in extensionMethodBaseExtensions on toEmbMapping.ExtensionMethodBaseExtensionIdentity equals embExtension.Identity into embExtensions
                from embExtension in embExtensions.DefaultIfEmpty()
                where embExtension == default
                select toEmbMapping;

            return invalidMappings;
        }

        /// <summary>
        /// Gets mappings with an extension method base extension identity not present in the set of extension method base extensions.
        /// </summary>
        public static
            IEnumerable<ExtensionMethodBaseExtensionToProjectMapping>
        GetMappingsWithInvalidEmbExtension(this IOperation _,
            IEnumerable<ExtensionMethodBaseExtensionToProjectMapping> embExtensionToProjectMappings,
            IEnumerable<ExtensionMethodBaseExtension> extensionMethodBaseExtensions)
        {
            var invalidMappings =
                from toProjectMapping in embExtensionToProjectMappings
                join embExtension in extensionMethodBaseExtensions on toProjectMapping.ExtensionMethodBaseExtensionIdentity equals embExtension.Identity into embExtensions
                from embExtension in embExtensions.DefaultIfEmpty()
                where embExtension == default
                select toProjectMapping;

            return invalidMappings;
        }

        /// <summary>
        /// Gets mappings with an extension method base identity not present in the set of extension method bases.
        /// </summary>
        public static
            IEnumerable<ExtensionMethodBaseExtensionToExtensionMethodBaseMapping>
        GetMappingsWithInvalidEmb(this IOperation _,
            IEnumerable<ExtensionMethodBaseExtensionToExtensionMethodBaseMapping> embExtensionToEmbMappings,
            IEnumerable<ExtensionMethodBase> extensionMethodBases)
        {
            var invalidMappings =
                from toEmbMapping in embExtensionToEmbMappings
                join emb in extensionMethodBases on toEmbMapping.ExtensionMethodBaseIdentity equals emb.Identity into embs
                from emb in embs.DefaultIfEmpty()
                where emb == default
                select toEmbMapping;

            return invalidMappings;
        }

        /// <summary>
        /// Gets mappings with an extension method base identity not present in the set of extension method bases.
        /// </summary>
        public static
            IEnumerable<ExtensionMethodBaseExtensionToProjectMapping>
        GetMappingsWithInvalidProject(this IOperation _,
            IEnumerable<ExtensionMethodBaseExtensionToProjectMapping> embExtensionToProjectMappings,
            IEnumerable<Project> projects)
        {
            var invalidMappings =
                from toProjectMapping in embExtensionToProjectMappings
                join project in projects on toProjectMapping.ProjectIdentity equals project.Identity into projs
                from project in projs.DefaultIfEmpty()
                where project == default
                select toProjectMapping;

            return invalidMappings;
        }

        public static IEnumerable<ExtensionMethodBaseExtension> GetUnmappedEmbExtensions(this IOperation _,
            IEnumerable<ExtensionMethodBaseExtension> extensionMethodBaseExtensions,
            IEnumerable<ExtensionMethodBaseExtensionToExtensionMethodBaseMapping> embExtensionToEmbMappings)
        {
            var unmappedEmbExtensions =
                from embExtension in extensionMethodBaseExtensions
                join toEmbMapping in embExtensionToEmbMappings on embExtension.Identity equals toEmbMapping.ExtensionMethodBaseExtensionIdentity into toEmbMappings
                from toEmbMapping in toEmbMappings.DefaultIfEmpty()
                where toEmbMapping == default
                select embExtension
                ;

            return unmappedEmbExtensions;
        }

        /// <summary>
        /// Gets <see cref="ExtensionMethodBaseExtension"/> that are either 1) unmapped to a to-extension method base mapping, or 2) mapped to an extension method base that does not exist.
        /// </summary>
        public static IEnumerable<ExtensionMethodBaseExtension> GetUnmappedEmbExtensions(this IOperation _,
            IEnumerable<ExtensionMethodBaseExtension> extensionMethodBaseExtensions,
            IEnumerable<ExtensionMethodBase> extensionMethodBases,
            IEnumerable<ExtensionMethodBaseExtensionToExtensionMethodBaseMapping> embExtensionToEmbMappings)
        {
            var unmappedEmbExtensions =
                from embExtension in extensionMethodBaseExtensions
                join toEmbMapping in embExtensionToEmbMappings on embExtension.Identity equals toEmbMapping.ExtensionMethodBaseExtensionIdentity into toEmbMappings
                from toEmbMapping in toEmbMappings.DefaultIfEmpty()
                join emb in extensionMethodBases on toEmbMapping?.ExtensionMethodBaseIdentity equals emb.Identity into embs
                from emb in embs.DefaultIfEmpty()
                where toEmbMapping == default || emb == default
                select embExtension
                ;

            return unmappedEmbExtensions;
        }

        public static IEnumerable<ExtensionMethodBaseExtension> GetUnmappedEmbExtensions(this IOperation _,
            IEnumerable<ExtensionMethodBaseExtension> extensionMethodBaseExtensions,
            IEnumerable<Project> projects,
            IEnumerable<ExtensionMethodBaseExtensionToProjectMapping> embExtensionToProjectMappings)
        {
            var unmappedEmbExtensions =
                from embExtension in extensionMethodBaseExtensions
                join toProjectMapping in embExtensionToProjectMappings on embExtension.Identity equals toProjectMapping.ExtensionMethodBaseExtensionIdentity into toProjectMappings
                from toProjectMapping in toProjectMappings.DefaultIfEmpty()
                join project in projects on toProjectMapping?.ProjectIdentity equals project.Identity into projs
                from project in projs.DefaultIfEmpty()
                where toProjectMapping == default || project == default
                select embExtension
                ;

            return unmappedEmbExtensions;
        }

        public static
            IEnumerable<(ExtensionMethodBaseExtension ExtensionMethodBaseExtension, ExtensionMethodBaseExtensionToExtensionMethodBaseMapping ToEmbMapping)>
        GetEmbExtensionToEmbMappingTuples(this IOperation _,
            IEnumerable<ExtensionMethodBaseExtension> embExtensions,
            IEnumerable<ExtensionMethodBaseExtensionToExtensionMethodBaseMapping> toEmbMappings)
        {
            var embExtensionToEmbMappingTuples = embExtensions
               .Join(toEmbMappings,
                   xEmbExtension => xEmbExtension.Identity,
                   xToEmbMapping => xToEmbMapping.ExtensionMethodBaseExtensionIdentity,
                   (xEmbExtension, xToEmbMapping) => (xEmbExtension, xToEmbMapping))
               ;

            return embExtensionToEmbMappingTuples;
        }

        public static async Task<(
            ExtensionMethodBaseExtensionToExtensionMethodBaseMapping[] newToEmbMappings,
            ExtensionMethodBaseExtensionToExtensionMethodBaseMapping[] oldToEmbMappings,
            ExtensionMethodBaseExtension[] unmappedEmbExtensions)>
        GetToExtensionMethodBaseMappingChanges(this IOperation _,
            ExtensionMethodBaseExtension[] embExtensions,
            ExtensionMethodBase[] embs,
            ExtensionMethodBaseExtensionToExtensionMethodBaseMapping[] existingMappings)
        {
            // Determine the current mappings.
            var (mappings, unmappedEmbExtensions) = await _.MapEmbExtensionsToEmbs(
                embExtensions,
                embs);

            // Now determine which mappings are new, and which are old.
            var (newMappings, departedMappings) = _.GetNewAndDepartedIdentityMappings(
                existingMappings,
                mappings);

            return (newMappings, departedMappings, unmappedEmbExtensions);
        }

        public static async Task<(
            ExtensionMethodBaseExtensionToExtensionMethodBaseMapping[] mappings,
            ExtensionMethodBaseExtension[] unmappableEmbExtensions)>
        MapEmbExtensionsToEmbs(this IOperation _,
            ExtensionMethodBaseExtension[] embExtensions,
            ExtensionMethodBase[] embs)
        {
            // Group extension method bases by type name, since we will just check whether the extension parameter's type name exists and is unique before guess-and-checking namespaced type names.
            var embsByTypeName = embs.ToDictionaryOfArrays(x => Instances.NamespacedTypeName.GetTypeName(x.NamespacedTypeName));

            // Group extension method bases by namespaced type name.
            var embsByNamespacedTypeName = embs.ToDictionaryOfArrays(x => x.NamespacedTypeName);

            // Group extension method base extensions by code file path (since we will analyze each compilation unit).
            var embExtensionsByCodeFilePathGroup = embExtensions.ToDictionaryOfArrays(x => x.CodeFilePath);

            var mappings = new List<ExtensionMethodBaseExtensionToExtensionMethodBaseMapping>();

            // Allow for extension methods that are unmappable via syntax-level analysis. (There is some hope by analyzing the containing project file path's references without doing a full semantic-level analysis, but that's just semantic-level analysis lite.)
            var unmappableEmbExtensions = new List<ExtensionMethodBaseExtension>();

            foreach (var pair in embExtensionsByCodeFilePathGroup)
            {
                var codeFilePath = pair.Key;
                var currentEmbExtensions = pair.Value;

                var embExtensionsForCodeFilePathByFullMethodName = currentEmbExtensions.ToDictionary(x => x.NamespacedTypedParameterizedMethodName);

                var extensionsCompilationUnit = await Instances.CompilationUnitOperator.Load(codeFilePath);

                var usingDirectivesSpecification = extensionsCompilationUnit.GetUsingDirectivesSpecification();

                var extensionMethodTuples = Instances.CompilationUnitOperator.GetExtensionMethodTuples(extensionsCompilationUnit);

                // For each extension method base extension namespaced, typed, parameterized, method name, locate that method declaration object in the code file's compilation unit.
                // Find the extension method base extension by going in the direction of method tuple -> full method name -> extension method base extension. It would be better to go in the other direction, starting with the extension method base extension, then parsing its full method name to choose the right tuple.
                // The benefit of going that direction would be not needing an up-to-date repository of extension method base extensions (up-to-date meaning "matching the state of the current code base").
                foreach (var extensionMethodTuple in extensionMethodTuples)
                {
                    var fullMethodName = Instances.MethodNameOperator.GetNamespacedTypedParameterizedMethodName(extensionMethodTuple);

                    // The weakness of going this direction is that the extension method base extension full method name from the code might not match the repository, if the repository is not up-to-date.
                    var embExtension = embExtensionsForCodeFilePathByFullMethodName[fullMethodName];

                    // From the using directives and using alias directives of the compilation unit, and from the type name of the extension parameter, guess and check to see if there is an extension method base with the right namespaced type name.
                    // Now analyze the extension parameter.
                    var extensionParameter = extensionMethodTuple.ExtensionMethod.GetExtensionParameter();

                    var extensionParameterTypeName = extensionParameter.GetTypeName();

                    // First check if the extension parameter type name is a direct using alias directive.
                    var hasAliasDirectiveForExtensionParameterTypeName = usingDirectivesSpecification.HasNameAliasFor(extensionParameterTypeName);
                    if (hasAliasDirectiveForExtensionParameterTypeName)
                    {
                        var aliasedNamespacedTypeName = hasAliasDirectiveForExtensionParameterTypeName.Result.SourceNameExpression;

                        var possibleEmbs = embsByNamespacedTypeName[aliasedNamespacedTypeName]; // TODO, check for existence of name.
                        if (possibleEmbs.Length > 1)
                        {
                            unmappableEmbExtensions.Add(embExtension);
                        }
                        else
                        {
                            var extensionMethodBase = possibleEmbs.Single(); // There is only one.

                            var mapping = new ExtensionMethodBaseExtensionToExtensionMethodBaseMapping
                            {
                                ExtensionMethodBaseExtensionIdentity = embExtension.Identity,
                                ExtensionMethodBaseIdentity = extensionMethodBase.Identity
                            };

                            mappings.Add(mapping);
                        }

                        continue; // Either way, if there is an explicity using alias directive, we are done.
                    }

                    // Then check if the extension parameter type name exists in the extension method bases by type name, and if it is unique.
                    var embWithTypeNameExists = embsByTypeName.ContainsKey(extensionParameterTypeName);
                    if (embWithTypeNameExists)
                    {
                        var possibleEmbEntries = embsByTypeName[extensionParameterTypeName]; // TODO, check for existence of name.
                        if (possibleEmbEntries.Length < 2)
                        {
                            var extensionMethodBase = possibleEmbEntries.Single(); // There is only one.

                            var mapping = new ExtensionMethodBaseExtensionToExtensionMethodBaseMapping
                            {
                                ExtensionMethodBaseExtensionIdentity = embExtension.Identity,
                                ExtensionMethodBaseIdentity = extensionMethodBase.Identity
                            };

                            mappings.Add(mapping);

                            continue; // Done.
                        }
                    }

                    // If the extension parameter type name does not exist or is not unique, guess and check each namespaced type name constructed from the extension parameter type name and each namespace in the file.
                    // (Including the namespace, and all sub-namspaces of the compilation unit itself.)
                    // If it exists as the namespaced type name of an extension method base, then note and reuse the success.
                    // If there is more than one EMB with the typename then see if only one EMB's namespace is present in the file:
                    //  * Guess-and-check each using namespace as a prefix to the extension method paramter type, to see if it exists in the EMB listing.
                    var namespaceWasGuessed = false;

                    var availableNamespaceNames = usingDirectivesSpecification.UsingNamespaceNames
                        .AppendRange(
                            Instances.NamespaceName.EnumerateNamespaceAndSubNamespaces(extensionMethodTuple.Namespace.Name.ToString()));

                    foreach (var namespaceName in availableNamespaceNames)
                    {
                        var possibleNamespacedTypeName = Instances.NamespacedTypeName.GetNamespacedName(
                            namespaceName,
                            extensionParameterTypeName);

                        var embWithNamespacedTypeNameExists = embsByNamespacedTypeName.ContainsKey(possibleNamespacedTypeName);
                        if (embWithNamespacedTypeNameExists)
                        {
                            var possibleEmbEntries = embsByNamespacedTypeName[possibleNamespacedTypeName]; // TODO, check for existence of name.
                            // If the extension parameter's namespaced type name is unique in the list of extension method bases, then use it.
                            if (possibleEmbEntries.Length < 2)
                            {
                                var extensionMethodBase = possibleEmbEntries.Single(); // There is only one.

                                var mapping = new ExtensionMethodBaseExtensionToExtensionMethodBaseMapping
                                {
                                    ExtensionMethodBaseExtensionIdentity = embExtension.Identity,
                                    ExtensionMethodBaseIdentity = extensionMethodBase.Identity
                                };

                                mappings.Add(mapping);

                                namespaceWasGuessed = true;

                                break; // Done guessing namespaces.
                            }
                        }
                    }

                    if (namespaceWasGuessed)
                    {
                        continue; // Done with this extension method tuple.
                    }

                    //  Finally, if nothing succeeds, then note the failure for later semantic analysis, or manual specification.
                    unmappableEmbExtensions.Add(embExtension);
                }
            }

            return (mappings.ToArray(), unmappableEmbExtensions.ToArray());
        }

        /// <summary>
        /// Maps all extension method bases to projects.
        /// This is slow since every extension method base extensions is tested against every project.
        /// </summary>
        public static
            ExtensionMethodBaseExtensionToProjectMapping[]
        MapEmbExtensionsToProjects_Slow(this IOperation _,
            ExtensionMethodBaseExtension[] extensionMethodBaseExtensions,
            Project[] projects)
        {
            var toProjectMappings = new List<ExtensionMethodBaseExtensionToProjectMapping>();

            var projectsByProjectDirectoryPath = projects.ToDictionaryByFilePathModified(
                Instances.ProjectPathsOperator.GetProjectDirectoryPath);

            foreach (var extensionMethodBase in extensionMethodBaseExtensions)
            {
                // If an extension method base does not have a mapping, determine it's mapping to a single project.
                _.AddMappingToMappings_Slow(
                    extensionMethodBase,
                    projectsByProjectDirectoryPath,
                    toProjectMappings);
            }

            return toProjectMappings.ToArray();
        }

        /// <summary>
        /// Maps all extension method base extensions to projects, using the existing to-project mappings as a starting point.
        /// This faster since only unmapped extension method base extensions are tested against every project; mapped extension method base extensions are merely verified.
        /// </summary>
        public static
            ExtensionMethodBaseExtensionToProjectMapping[]
        MapEmbExtensionsToProjects_Faster(this IOperation _,
            ExtensionMethodBaseExtension[] extensionMethodBaseExtensions,
            Project[] projects,
            ExtensionMethodBaseExtensionToProjectMapping[] existingToProjectMappings)
        {
            var (newToProjectMappings, oldToProjectMappings) = _.GetToProjectMappingChanges(
                extensionMethodBaseExtensions,
                projects,
                existingToProjectMappings);

            var output = _.GetToProjectMappings(
                existingToProjectMappings,
                newToProjectMappings,
                oldToProjectMappings);

            return output;
        }

        public static ExtensionMethodBaseExtensionToProjectMapping[] GetToProjectMappings(this IOperation _,
            ExtensionMethodBaseExtensionToProjectMapping[] existingToProjectMappings,
            ExtensionMethodBaseExtensionToProjectMapping[] newToProjectMappings,
            ExtensionMethodBaseExtensionToProjectMapping[] oldToProjectMappings)
        {
            var output = existingToProjectMappings
                .Except(oldToProjectMappings)
                .AppendRange(newToProjectMappings)
                .ToArray()
                ;

            return output;
        }

        public static (
            ExtensionMethodBaseExtensionToProjectMapping[] newToProjectMappings,
            ExtensionMethodBaseExtensionToProjectMapping[] oldToProjectMappings)
        GetToProjectMappingChanges(this IOperation _,
            ExtensionMethodBaseExtension[] extensionMethodBaseExtensions,
            Project[] projects,
            ExtensionMethodBaseExtensionToProjectMapping[] existingToProjectMappings)
        {
            extensionMethodBaseExtensions.VerifyAllIdentitiesAreSet();
            projects.VerifyAllIdentitiesAreSet();

            var existingToProjectMappingsByEmbIdentity = existingToProjectMappings
                .ToDictionary(
                    x => x.ExtensionMethodBaseExtensionIdentity);

            var projectsByProjectIdentity = projects
                .ToDictionary(
                    x => x.Identity);

            var projectsByProjectDirectoryPath = projects.ToDictionaryByFilePathModified(
                Instances.ProjectPathsOperator.GetProjectDirectoryPath);

            var newToProjectMappings = new List<ExtensionMethodBaseExtensionToProjectMapping>();
            var oldToProjectMappings = new List<ExtensionMethodBaseExtensionToProjectMapping>(); // For existing mappings that are no longer valid, and should be removed.

            foreach (var extensionMethodBase in extensionMethodBaseExtensions)
            {
                var hasMapping = existingToProjectMappingsByEmbIdentity.ContainsKey(extensionMethodBase.Identity);
                if (hasMapping)
                {
                    var existingMapping = existingToProjectMappingsByEmbIdentity[extensionMethodBase.Identity];

                    // If an extension method has a mapping, test if that mapping is still valid (this saves significant computation time).
                    var projectExists = projectsByProjectIdentity.ContainsKey(existingMapping.ProjectIdentity);
                    if (projectExists)
                    {
                        var project = projectsByProjectIdentity[existingMapping.ProjectIdentity];

                        // If the project exists, is the extension method base still in the project's directory?
                        var extensionMethodBaseCodeFileIsInProjectDirectory = Instances.PathOperator.IsFileInDirectoryOrSubDirectoriesOfFileDirectory(
                            extensionMethodBase.CodeFilePath,
                            project.FilePath);

                        if (!extensionMethodBaseCodeFileIsInProjectDirectory)
                        {
                            // Remove the mapping.
                            oldToProjectMappings.Add(existingMapping);

                            // The add new mapping for the extension method base.
                            _.AddMappingToMappings_Slow(
                                extensionMethodBase,
                                projectsByProjectDirectoryPath,
                                newToProjectMappings);
                        }
                        // Else, do nothing. Mapping is still valid.
                    }
                    else
                    {
                        // Project no longer exists, definitely remove the mapping.
                        oldToProjectMappings.Add(existingMapping);
                    }
                }
                else
                {
                    // If an extension method base does not have a mapping, determine it's mapping to a single project.
                    _.AddMappingToMappings_Slow(
                        extensionMethodBase,
                        projectsByProjectDirectoryPath,
                        newToProjectMappings);
                }
            }

            return (newToProjectMappings.ToArray(), oldToProjectMappings.ToArray());
        }

        /// <summary>
        /// Maps an extension method base to a single project.
        /// Performs an exhaustive search of all projects (which is slow) to ensure the extension method base only maps to a single project.
        /// </summary>
        public static void AddMappingToMappings_Slow(this IOperation _,
            ExtensionMethodBaseExtension extensionMethodBaseExtension,
            Dictionary<string, Project> projectsByProjectDirectoryPath,
            List<ExtensionMethodBaseExtensionToProjectMapping> mappings
            )
        {
            var projectAlreadyFound = false;

            foreach (var projectDirectoryPath in projectsByProjectDirectoryPath.Keys)
            {
                var extensionMethodBaseCodeFileIsInProjectDirectory = Instances.PathOperator.IsFileInDirectoryOrSubDirectories(
                    extensionMethodBaseExtension.CodeFilePath,
                    projectDirectoryPath);

                if (extensionMethodBaseCodeFileIsInProjectDirectory)
                {
                    if (projectAlreadyFound)
                    {
                        // If it does, that means there are multiple projects with the same project directory, or there is a project in Instances.PathOperator.IsPathSubPathOfParentPath() or GetProjectDirectoryPath() method.
                        throw new Exception("Should not happen.");
                    }

                    var project = projectsByProjectDirectoryPath[projectDirectoryPath];

                    var toProjectMapping = new ExtensionMethodBaseExtensionToProjectMapping
                    {
                        ExtensionMethodBaseExtensionIdentity = extensionMethodBaseExtension.Identity,
                        ProjectIdentity = project.Identity
                    };

                    mappings.Add(toProjectMapping);

                    projectAlreadyFound = true;
                }
            }

            if (!projectAlreadyFound)
            {
                // If it does, the project index needs to be updated by running the project update script (R5T.S0023) again to collect any new projects.
                throw new Exception("Should not happen.");
            }
        }

        public static async Task<(
            ExtensionMethodBaseExtension[] repositoryExtensionMethodBaseExtensions,
            ExtensionMethodBaseExtension[] currentExtensionMethodBaseExtensions)>
        GetRepositoryAndCurrentExtensionMethodBaseExtensions(this IOperation _,
            IAllProjectDirectoryPathsProvider allProjectDirectoryPathsProvider,
            IExtensionMethodBaseExtensionRepository extensionMethodBaseExtensionRepository)
        {
            // Get all repository extension method bases.
            var repositoryExtensionMethodBaseExtensions = await extensionMethodBaseExtensionRepository.GetAllExtensionMethodBaseExtensions();

            // Verify distinct by (namespaced type name, code file path) pair.
            repositoryExtensionMethodBaseExtensions.VerifyDistinctByNamedFilePathedData();

            // Get all current extension method bases from all current project directory paths.
            var currentExtensionMethodBaseExtensions = await _.GetCurrentExtensionMethodBaseExtensions(allProjectDirectoryPathsProvider);

            // Verify distinct by (namespaced type name, code file path) pair.
            currentExtensionMethodBaseExtensions.VerifyDistinctByNamedFilePathedData();

            return (repositoryExtensionMethodBaseExtensions, currentExtensionMethodBaseExtensions);
        }

        public static async Task<ExtensionMethodBaseExtension[]> GetCurrentExtensionMethodBaseExtensions(this IOperation _,
            IAllProjectDirectoryPathsProvider allProjectDirectoryPathsProvider)
        {
            var currentExtensionMethodBaseExtensions = new List<ExtensionMethodBaseExtension>();

            var projectDirectoryPaths = await allProjectDirectoryPathsProvider.GetAllProjectDirectoryPaths();
            foreach (var projectDirectoryPath in projectDirectoryPaths)
            {
                var currentExtensionMethods = await Instances.ExtensionMethodBaseOperator.GetExtensionMethodBaseExtensionMethods(projectDirectoryPath);
                foreach (var extensionMethod in currentExtensionMethods)
                {
                    var extensionMethodBaseExtension = new ExtensionMethodBaseExtension
                    {
                        // No identity, identity will be added (if needed) when added to the repository.
                        CodeFilePath = extensionMethod.CodeFilePath,
                        NamespacedTypedParameterizedMethodName = extensionMethod.NamespacedTypeName // Note, actually a namespaced, typed, parameterized method name, so ok.
                    };

                    currentExtensionMethodBaseExtensions.Add(extensionMethodBaseExtension);
                }
            }

            var output = currentExtensionMethodBaseExtensions.ToArray();
            return output;
        }

        public static Dictionary<string, Project> GetProjectsByDirectoryPath(this IOperation _,
            IEnumerable<Project> projects)
        {
            var output = projects.ToDictionaryByFilePathModified(
                xFilePath => Instances.PathOperator.GetDirectoryPathOfFilePath(xFilePath));

            return output;
        }

        public static (
            Project[] unignoredProjects,
            ExtensionMethodBase[] unignoredExtensionMethodBases)
        GetUnignoredProjectsAndExtensionMethodBases(this IOperation _,
            IEnumerable<Project> projects,
            IEnumerable<ExtensionMethodBase> extensionMethodBases,
            IEnumerable<Guid> ignoredProjectIdentities,
            IEnumerable<Guid> ignoredExtensionMethodBaseIdentities)
        {
            var ignoredProjectIdentitiesHash = new HashSet<Guid>(ignoredProjectIdentities);
            var ignoredExtensionMethodBaseIdentitiesHash = new HashSet<Guid>(ignoredExtensionMethodBaseIdentities);

            var unignoredProjects = projects
                .ExceptWhere(xProject => ignoredProjectIdentitiesHash.Contains(xProject.Identity))
                .Now();

            var unignoredExtensionMethodBases = extensionMethodBases
                .ExceptWhere(xExtensionMethodBase => ignoredExtensionMethodBaseIdentitiesHash.Contains(xExtensionMethodBase.Identity))
                .Now();

            return (unignoredProjects, unignoredExtensionMethodBases);
        }

        public static async
            Task<ExtensionMethodBaseExtensionProjectedTuple[]>
        GetExtensionMethodBaseExtensionTuples(this IOperation _,
            IEnumerable<Project> projects,
            IEnumerable<ExtensionMethodBase> extensionMethodBases,
            IEnumerable<Guid> ignoredProjectIdentities,
            IEnumerable<Guid> ignoredExtensionMethodBaseIdentities)
        {
            var (unignoredProjects, unignoredExtensionMethodBases) = _.GetUnignoredProjectsAndExtensionMethodBases(
                projects,
                extensionMethodBases,
                ignoredProjectIdentities,
                ignoredExtensionMethodBaseIdentities);

            var output = await _.GetExtensionMethodBaseExtensionTuples(
                unignoredProjects,
                unignoredExtensionMethodBases);

            return output;
        }

        public static async
            Task<ExtensionMethodBaseExtensionProjectedTuple[]>
        GetExtensionMethodBaseExtensionTuples(this IOperation _,
            IEnumerable<Project> unignoredProjects,
            IEnumerable<ExtensionMethodBase> unignoredExtensionMethodBases)
        {
            var unignoredProjectsByDirectoryPath = _.GetProjectsByDirectoryPath(unignoredProjects);
            var unignoredExtensionMethodBasesByNamespacedTypeName = unignoredExtensionMethodBases.ToDictionaryByName();

            var output = await _.GetExtensionMethodBaseExtensionTuples(
                unignoredProjectsByDirectoryPath,
                unignoredExtensionMethodBasesByNamespacedTypeName);

            return output;
        }

        public static async
            Task<ExtensionMethodBaseExtensionProjectedTuple[]>
        GetExtensionMethodBaseExtensionTuples(this IOperation _,
            IDictionary<string, Project> unignoredProjectsByDirectoryPath,
            IDictionary<string, ExtensionMethodBase> unignoredExtensionMethodBasesByNamespacedTypeName)
        {
            var output = new List<ExtensionMethodBaseExtensionProjectedTuple>();

            foreach (var pair in unignoredProjectsByDirectoryPath.OrderAlphabetically(xPair => xPair.Key))
            {
                var projectDirectoryPath = pair.Key;

                var extensionMethodBaseExtensionTuples = await Instances.ExtensionMethodBaseOperator.GetExtensionMethodBaseExtensionTuples(
                    projectDirectoryPath,
                    unignoredExtensionMethodBasesByNamespacedTypeName);

                var extensionMethodBaseExtensionProjectedTuples = extensionMethodBaseExtensionTuples
                    .Select(xTuple => new ExtensionMethodBaseExtensionProjectedTuple(
                        xTuple.ExtensionMethodBaseExtension,
                        xTuple.ExtensionMethodBase,
                        pair.Value));

                output.AddRange(extensionMethodBaseExtensionProjectedTuples);
            }

            return output.ToArray();
        }
    }
}
