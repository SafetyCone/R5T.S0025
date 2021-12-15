using System;
using System.Threading.Tasks;

using R5T.D0096;
using R5T.D0109.I001;


namespace R5T.S0025
{
    public class O002_BackupFileBasedRepositoryFiles : T0020.IOperation
    {
        private IBackupExtensionMethodBaseExtensionRepositoryFilePathsProvider BackupExtensionMethodBaseExtensionRepositoryFilePathsProvider { get; }
        private IHumanOutput HumanOutput { get; }
        private IExtensionMethodBaseExtensionRepositoryFilePathsProvider ExtensionMethodBaseExtensionRepositoryFilePathsProvider { get; }


        public O002_BackupFileBasedRepositoryFiles(
            IBackupExtensionMethodBaseExtensionRepositoryFilePathsProvider backupExtensionMethodBaseExtensionRepositoryFilePathsProvider,
            IHumanOutput humanOutput,
            IExtensionMethodBaseExtensionRepositoryFilePathsProvider extensionMethodBaseExtensionRepositoryFilePathsProvider)
        {
            this.BackupExtensionMethodBaseExtensionRepositoryFilePathsProvider = backupExtensionMethodBaseExtensionRepositoryFilePathsProvider;
            this.HumanOutput = humanOutput;
            this.ExtensionMethodBaseExtensionRepositoryFilePathsProvider = extensionMethodBaseExtensionRepositoryFilePathsProvider;
        }

        public async Task Run()
        {
            var (Task1Result, Task2Result, Task3Result, Task4Result, Task5Result, Task6Result) = await TaskHelper.WhenAll(
                TaskHelper.WhenAll(
                    this.ExtensionMethodBaseExtensionRepositoryFilePathsProvider.GetDuplicateExtensionMethodBaseExtensionNamesTextFilePath(),
                    this.BackupExtensionMethodBaseExtensionRepositoryFilePathsProvider.GetDuplicateExtensionMethodBaseExtensionNamesTextFilePath()),
                TaskHelper.WhenAll(
                    this.ExtensionMethodBaseExtensionRepositoryFilePathsProvider.GetExtensionMethodBaseExtensionSelectionsTextFilePath(),
                    this.BackupExtensionMethodBaseExtensionRepositoryFilePathsProvider.GetExtensionMethodBaseExtensionSelectionsTextFilePath()),
                TaskHelper.WhenAll(
                    this.ExtensionMethodBaseExtensionRepositoryFilePathsProvider.GetExtensionMethodBaseExtensionsListingJsonFilePath(),
                    this.BackupExtensionMethodBaseExtensionRepositoryFilePathsProvider.GetExtensionMethodBaseExtensionsListingJsonFilePath()),
                TaskHelper.WhenAll(
                    this.ExtensionMethodBaseExtensionRepositoryFilePathsProvider.GetIgnoredExtensionMethodBaseNamesTextFilePath(),
                    this.BackupExtensionMethodBaseExtensionRepositoryFilePathsProvider.GetIgnoredExtensionMethodBaseNamesTextFilePath()),
                TaskHelper.WhenAll(
                    this.ExtensionMethodBaseExtensionRepositoryFilePathsProvider.GetToExtensionMethodBaseMappingsJsonFilePath(),
                    this.BackupExtensionMethodBaseExtensionRepositoryFilePathsProvider.GetToExtensionMethodBaseMappingsJsonFilePath()),
                TaskHelper.WhenAll(
                    this.ExtensionMethodBaseExtensionRepositoryFilePathsProvider.GetToProjectMappingsJsonFilePath(),
                    this.BackupExtensionMethodBaseExtensionRepositoryFilePathsProvider.GetToProjectMappingsJsonFilePath()));

            foreach (var fileBackupSourceDestinationPair in new[]
            {
                Task1Result,
                Task2Result,
                Task3Result,
                Task4Result,
                Task5Result,
                Task6Result
            })
            {
                var sourceFilePath = fileBackupSourceDestinationPair.Task1Result;
                var destinationFilePath = fileBackupSourceDestinationPair.Task2Result;

                Instances.FileSystemOperator.CopyFile(sourceFilePath, destinationFilePath);

                this.HumanOutput.WriteLine($"File based project repository file back-up copy made:\nSource:\n{sourceFilePath}\nDestination:\n{destinationFilePath}\n");
            }
        }
    }
}
