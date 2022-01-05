using System;
using System.Threading.Tasks;

using R5T.Magyar.IO;

using R5T.D0105;
using R5T.D0110;
using R5T.T0020;

using R5T.S0025.Library;


namespace R5T.S0025
{
    [OperationMarker]
    public class O001b_SummarizeChanges : IOperation
    {
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private ISummaryFilePathProvider SummaryFilePathProvider { get; }


        public O001b_SummarizeChanges(
            INotepadPlusPlusOperator notepadPlusPlusOperator,
            ISummaryFilePathProvider summaryFilePathProvider)
        {
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
            this.SummaryFilePathProvider = summaryFilePathProvider;
        }

        public async Task Run(AnalysisInputData analysisInputData, AnalysisOutputData analysisOutputData)
        {
            // Summarize changes.
            var summaryFilePath = await this.SummaryFilePathProvider.GetSummaryFilePath();

            using var summaryFile = FileHelper.WriteTextFile(summaryFilePath);

            Instances.Operation.WriteSummary(
                summaryFile,
                analysisInputData,
                analysisOutputData);

            // Flush now.
            await summaryFile.FlushAsync();

            // Show the summary in Notepad++ to be immediately helpful.
            await this.NotepadPlusPlusOperator.OpenFilePath(summaryFilePath);
        }
    }
}
