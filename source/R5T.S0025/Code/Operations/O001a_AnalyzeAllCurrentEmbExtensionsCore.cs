using System;
using System.Threading.Tasks;

using R5T.D0084.D001;
using R5T.D0101;
using R5T.D0108;
using R5T.D0109;

using R5T.S0025.Library;


namespace R5T.S0025
{
    public class O001a_AnalyzeAllCurrentEmbExtensionsCore
    {
        private IAllProjectDirectoryPathsProvider AllProjectDirectoryPathsProvider { get; }
        private IExtensionMethodBaseExtensionRepository ExtensionMethodBaseExtensionRepository { get; }
        private IExtensionMethodBaseRepository ExtensionMethodBaseRepository { get; }
        private IProjectRepository ProjectRepository { get; }


        public O001a_AnalyzeAllCurrentEmbExtensionsCore(
            IAllProjectDirectoryPathsProvider allProjectDirectoryPathsProvider,
            IExtensionMethodBaseExtensionRepository extensionMethodBaseExtensionRepository,
            IExtensionMethodBaseRepository extensionMethodBaseRepository,
            IProjectRepository projectRepository)
        {
            this.AllProjectDirectoryPathsProvider = allProjectDirectoryPathsProvider;
            this.ExtensionMethodBaseExtensionRepository = extensionMethodBaseExtensionRepository;
            this.ExtensionMethodBaseRepository = extensionMethodBaseRepository;
            this.ProjectRepository = projectRepository;
        }

        public async Task<(AnalysisOutputData AnalysisOutputData, AnalysisInputData AnalysisInputData)> Run()
        {
            var inputData = await Instances.Operation.GetAnalysisInputData(
                this.AllProjectDirectoryPathsProvider,
                this.ExtensionMethodBaseExtensionRepository,
                this.ExtensionMethodBaseRepository,
                this.ProjectRepository);

            var analysisData = Instances.Operation.PerformAnalysis(inputData);

            return (analysisData, inputData);
        }
    }
}
