using System;
using System.Linq;
using System.Threading.Tasks;

using R5T.D0105;
using R5T.D0109.I001;
using R5T.T0020;


namespace R5T.S0025
{
    [OperationMarker]
    public class O900_OpenAllEmbExtensionRepositoryFiles : IActionOperation
    {
        private IExtensionMethodBaseExtensionRepositoryFilePathsProvider ExtensionMethodBaseExtensionRepositoryFilePathsProvider { get; }
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }


        public O900_OpenAllEmbExtensionRepositoryFiles(
            IExtensionMethodBaseExtensionRepositoryFilePathsProvider extensionMethodBaseExtensionRepositoryFilePathsProvider,
            INotepadPlusPlusOperator notepadPlusPlusOperator)
        {
            this.ExtensionMethodBaseExtensionRepositoryFilePathsProvider = extensionMethodBaseExtensionRepositoryFilePathsProvider;
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
        }

        public async Task Run()
        {
            var filePathsProvider = this.ExtensionMethodBaseExtensionRepositoryFilePathsProvider;

            var (Task1Result, Task2Result, Task3Result, Task4Result, Task5Result, Task6Result) = await TaskHelper.WhenAll(
                filePathsProvider.GetDuplicateExtensionMethodBaseExtensionNamesTextFilePath(),
                filePathsProvider.GetExtensionMethodBaseExtensionSelectionsTextFilePath(),
                filePathsProvider.GetExtensionMethodBaseExtensionsListingJsonFilePath(),
                filePathsProvider.GetIgnoredExtensionMethodBaseNamesTextFilePath(),
                filePathsProvider.GetToExtensionMethodBaseMappingsJsonFilePath(),
                filePathsProvider.GetToProjectMappingsJsonFilePath());

            var openingAllFilePaths = new[]
            {
                Task1Result,
                Task2Result,
                Task3Result,
                Task4Result,
                Task5Result,
                Task6Result,
            }
            .Select(filePath => this.NotepadPlusPlusOperator.OpenFilePath(filePath))
            ;

            await Task.WhenAll(openingAllFilePaths);
        }
    }
}
