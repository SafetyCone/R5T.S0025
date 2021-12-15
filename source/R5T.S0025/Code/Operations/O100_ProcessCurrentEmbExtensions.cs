using System;
using System.Threading.Tasks;


namespace R5T.S0025
{
    /// <summary>
    /// This is the main operation, performing all steps in the process for creating a tab-autocompletion capablity listing of extension method base extension functionality, including:
    /// * Survey the local file system for extension method base extensions.
    /// * Update the extension method base extensions repository, including prompts for human action.
    /// * Output extension method base extension functionality names.
    /// </summary>
    public class O100_ProcessCurrentEmbExtensions : T0020.IOperation
    {
        private O001a_AnalyzeAllCurrentEmbExtensionsCore O001A_AnalyzeAllCurrentEmbExtensionsCore { get; }
        private O001b_SummarizeChanges O001B_SummarizeChanges { get; }
        private O002_BackupFileBasedRepositoryFiles O002_BackupFileBasedRepositoryFiles { get; }
        private O003a_PerformRequiredHumanActions O003A_PerformRequiredHumanActions { get; }
        private O004a_UpdateEmbExtensionsRepository O004A_UpdateEmbExtensionsRepository { get; }
        private O005a_OutputEmbFunctionalityNames O005A_OutputEmbFunctionalityNames { get; }


        public O100_ProcessCurrentEmbExtensions(
            O001a_AnalyzeAllCurrentEmbExtensionsCore o001A_AnalyzeAllCurrentEmbExtensionsCore,
            O001b_SummarizeChanges o001B_SummarizeChanges,
            O002_BackupFileBasedRepositoryFiles o002_BackupFileBasedRepositoryFiles,
            O003a_PerformRequiredHumanActions o003A_PerformRequiredHumanActions,
            O004a_UpdateEmbExtensionsRepository o004A_UpdateEmbExtensionsRepository,
            O005a_OutputEmbFunctionalityNames o005A_OutputEmbFunctionalityNames)
        {
            this.O001A_AnalyzeAllCurrentEmbExtensionsCore = o001A_AnalyzeAllCurrentEmbExtensionsCore;
            this.O001B_SummarizeChanges = o001B_SummarizeChanges;
            this.O002_BackupFileBasedRepositoryFiles = o002_BackupFileBasedRepositoryFiles;
            this.O003A_PerformRequiredHumanActions = o003A_PerformRequiredHumanActions;
            this.O004A_UpdateEmbExtensionsRepository = o004A_UpdateEmbExtensionsRepository;
            this.O005A_OutputEmbFunctionalityNames = o005A_OutputEmbFunctionalityNames;
        }

        public async Task Run()
        {
            var (analysisOutputData, analysisInputData) = await this.O001A_AnalyzeAllCurrentEmbExtensionsCore.Run();
            await this.O001B_SummarizeChanges.Run(analysisInputData, analysisOutputData);
            await this.O002_BackupFileBasedRepositoryFiles.Run();
            await this.O003A_PerformRequiredHumanActions.Run(analysisOutputData);
            await this.O004A_UpdateEmbExtensionsRepository.Run(analysisOutputData);
            await this.O005A_OutputEmbFunctionalityNames.Run();
        }
    }
}
