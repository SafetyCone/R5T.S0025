using System;
using System.Threading.Tasks;

using R5T.D0078;
using R5T.D0079;
using R5T.D0084.D002;
using R5T.T0020;

using R5T.S0025.Library;


namespace R5T.S0025
{
    /// <summary>
    /// Note: all extension method base extensions should be used (as opposed to just "selected" extension method base extensions) since the <see cref="O007_UpdateRepositoryWithAllEmbExtensions"/> has removed the distinction.
    /// </summary>
    [OperationMarker]
    public class O006_UpdateEmbFunctionalityIntellisense : IActionOperation
    {
        private IExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPathProvider ExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPathProvider { get; }
        private IRepositoriesDirectoryPathProvider RepositoriesDirectoryPathProvider { get; }
        private IRepository Repository { get; }
        private IVisualStudioProjectFileOperator VisualStudioProjectFileOperator { get; }
        private IVisualStudioSolutionFileOperator VisualStudioSolutionFileOperator { get; }


        public O006_UpdateEmbFunctionalityIntellisense(
            IExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPathProvider extensionMethodBaseFunctionalityExtensionMethodBaseProjectPathProvider,
            IRepositoriesDirectoryPathProvider repositoriesDirectoryPathProvider,
            IRepository repository,
            IVisualStudioProjectFileOperator visualStudioProjectFileOperator,
            IVisualStudioSolutionFileOperator visualStudioSolutionFileOperator)
        {
            this.ExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPathProvider = extensionMethodBaseFunctionalityExtensionMethodBaseProjectPathProvider;
            this.RepositoriesDirectoryPathProvider = repositoriesDirectoryPathProvider;
            this.Repository = repository;
            this.VisualStudioProjectFileOperator = visualStudioProjectFileOperator;
            this.VisualStudioSolutionFileOperator = visualStudioSolutionFileOperator;
        }

        public async Task Run()
        {
            // Inputs.
            var localDataLibraryName = Instances.LibraryName.LocalData();
            var localDataLibraryDescription = Instances.LibraryDescription.LocalData();

            // Get extension method base functionality names (with paired EMB extension identity).
            var (
                projects,
                extensionMethodBases,
                extensionMethodBaseExtensions,
                toProjectMappings,
                toExtensionMethodBaseMappings
                )
                = await this.Repository.GetExtensionMethodBaseFunctionalityData();

            var tuples = Instances.Operation.GetExtensionMethoBaseExtensionTuples(
                projects,
                extensionMethodBases,
                extensionMethodBaseExtensions,
                toProjectMappings,
                toExtensionMethodBaseMappings);

            var embExtensionFunctionalityNames = Instances.Operation.GetEmbExtensionFunctionalityNames(tuples);

            // Now write out the extension method base functionality names to a project in a solution in a local data repository.
            // Repository.
            var repositoryName = Instances.LibraryNameOperator.GetRepositoryName(localDataLibraryName);

            var respositoriesDirectoryPath = await this.RepositoriesDirectoryPathProvider.GetRepositoriesDirectoryPath();
            var extensionMethodBaseFunctionalityExtensionMethodBaseProjectPath = await this.ExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPathProvider.GetExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPath();

            await Instances.RepositoryGenerator.CreateLocalRepositoryDirectoryOkIfExists(
                repositoryName,
                respositoriesDirectoryPath,
                async localRepositoryContext =>
                {
                    // Solution.
                    var solutionName = Instances.LibraryNameOperator.GetSolutionName(localDataLibraryName);

                    var solutionDirectoryPath = Instances.SolutionPathsOperator.GetSourceSolutionDirectoryPath(localRepositoryContext.DirectoryPath);

                    await Instances.SolutionGenerator.CreateSolutionOnlyIfNotExistsButAlwaysModify(
                        solutionDirectoryPath,
                        solutionName,
                        this.VisualStudioSolutionFileOperator,
                        async solutionFileContext =>
                        {
                            // Project.
                            var projectName = Instances.LibraryNameOperator.GetProjectName(localDataLibraryName);
                            var projectDescription = Instances.ProjectDescriptionGenerator.GetProjectDescription(localDataLibraryDescription);

                            var projectSpecification = Instances.ProjectOperator.GetProjectFileSpecification(
                                projectName,
                                projectDescription,
                                solutionFileContext.DirectoryPath,
                                dependencyProjectReferenceFilePaths: ArrayHelper.From(extensionMethodBaseFunctionalityExtensionMethodBaseProjectPath));

                            await Instances.ProjectGenerator.CreateLibraryProjectOnlyIfNotExistsButAlwaysModify(
                                solutionFileContext,
                                projectSpecification,
                                this.VisualStudioProjectFileOperator,
                                this.VisualStudioSolutionFileOperator,
                                async projectFileContext =>
                                {
                                    // Create /Code/Bases/Extensions/IProjectPathExtensions.cs.
                                    // Code file.
                                    await projectFileContext.InProjectSubDirectoryPathContext(
                                        Instances.ProjectPathsOperator.GetBasesExtensionsDirectoryRelativePath(),
                                        async basesExtensionsDirectoryPathContext =>
                                        {
                                            await basesExtensionsDirectoryPathContext.InProjectSubFilePathContext(
                                                Instances.CodeFileName.IExtensionMethodBaseFunctionalityExtensions(),
                                                async filePathContext =>
                                                {
                                                    // Code file generator, compilation unit generator, class generator, and method generator.
                                                    await Instances.CodeFileGenerator.CreateIExtensionMethodBaseFunctionalityExtensions(
                                                        embExtensionFunctionalityNames,
                                                        projectSpecification.DefaultNamespaceName,
                                                        filePathContext.FilePath);
                                                });
                                        });
                                });
                        });
                });
        }
    }
}
