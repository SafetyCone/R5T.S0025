using System;
using System.Threading.Tasks;


namespace R5T.S0025
{
    public class O001_AnalyzeAllCurrentEmbExtensions : T0020.IActionOperation
    {
        private O001a_AnalyzeAllCurrentEmbExtensionsCore O001A_AnalyzeAllCurrentEmbExtensionsCore { get; }
        private O001b_SummarizeChanges O001B_SummarizeChanges { get; }


        public O001_AnalyzeAllCurrentEmbExtensions(
            O001a_AnalyzeAllCurrentEmbExtensionsCore o001A_AnalyzeAllCurrentEmbExtensionsCore,
            O001b_SummarizeChanges o001B_SummarizeChanges)
        {
            this.O001A_AnalyzeAllCurrentEmbExtensionsCore = o001A_AnalyzeAllCurrentEmbExtensionsCore;
            this.O001B_SummarizeChanges = o001B_SummarizeChanges;
        }

        public async Task Run()
        {
            var (analysisOutputData, analysisInputData) = await this.O001A_AnalyzeAllCurrentEmbExtensionsCore.Run();

            await this.O001B_SummarizeChanges.Run(analysisInputData, analysisOutputData);
        }
    }
}
