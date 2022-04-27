using System;
using System.Threading.Tasks;

using R5T.D0048;using R5T.T0064;


namespace R5T.S0025
{[ServiceImplementationMarker]
    public class BackupExtensionMethodBaseExtensionRepositoryFilePathsProvider : IBackupExtensionMethodBaseExtensionRepositoryFilePathsProvider,IServiceImplementation
    {
        private IOutputFilePathProvider OutputFilePathProvider { get; }


        public BackupExtensionMethodBaseExtensionRepositoryFilePathsProvider(
            IOutputFilePathProvider outputFilePathProvider)
        {
            this.OutputFilePathProvider = outputFilePathProvider;
        }

        public async Task<string> GetDuplicateExtensionMethodBaseExtensionNamesTextFilePath()
        {
            var output = await this.OutputFilePathProvider.GetOutputFilePath("Extension Method Base Extensions-Duplicate Name Selections-Backup.txt");
            return output;
        }

        public async Task<string> GetExtensionMethodBaseExtensionSelectionsTextFilePath()
        {
            var output = await this.OutputFilePathProvider.GetOutputFilePath("Extension Method Base Extensions-Selected-Backup.txt");
            return output;
        }

        public async Task<string> GetExtensionMethodBaseExtensionsListingJsonFilePath()
        {
            var output = await this.OutputFilePathProvider.GetOutputFilePath("Extension Method Base Extensions-All-Backup.json");
            return output;
        }

        public async Task<string> GetIgnoredExtensionMethodBaseNamesTextFilePath()
        {
            var output = await this.OutputFilePathProvider.GetOutputFilePath("Extension Method Base Extensions-Ignored Names-Backup.txt");
            return output;
        }

        public async Task<string> GetToExtensionMethodBaseMappingsJsonFilePath()
        {
            var output = await this.OutputFilePathProvider.GetOutputFilePath("Extension Method Base Extensions-To Extension Method Base Mappings-Backup.json");
            return output;
        }

        public async Task<string> GetToProjectMappingsJsonFilePath()
        {
            var output = await this.OutputFilePathProvider.GetOutputFilePath("Extension Method Base Extensions-To Project Mappings-Backup.json");
            return output;
        }
    }
}
