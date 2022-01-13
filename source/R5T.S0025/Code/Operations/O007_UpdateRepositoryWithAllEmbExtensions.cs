using System;
using System.Threading.Tasks;

using R5T.T0020;


namespace R5T.S0025
{
    /// <summary>
    /// Updates the list of extension method base extensions, selected those that are not in ignored projects, or on ignored extension method bases.
    /// Skips the "selection" process for extension method base extensions.
    /// </summary>
    [OperationMarker]
    public class O007_UpdateRepositoryWithAllEmbExtensions : IActionOperation
    {
        private O002_BackupFileBasedRepositoryFiles O002_BackupFileBasedRepositoryFiles { get; }
        private O007a_UpdateRepositoryWithAllEmbExtensions O007A_UpdateRepositoryWithAllEmbExtensions { get; }


        public O007_UpdateRepositoryWithAllEmbExtensions(
            O002_BackupFileBasedRepositoryFiles o002_BackupFileBasedRepositoryFiles,
            O007a_UpdateRepositoryWithAllEmbExtensions o007A_UpdateRepositoryWithAllEmbExtensions)
        {
            this.O002_BackupFileBasedRepositoryFiles = o002_BackupFileBasedRepositoryFiles;
            this.O007A_UpdateRepositoryWithAllEmbExtensions = o007A_UpdateRepositoryWithAllEmbExtensions;
        }

        public async Task Run()
        {
            // Backup.
            await this.O002_BackupFileBasedRepositoryFiles.Run();

            await this.O007A_UpdateRepositoryWithAllEmbExtensions.Run();
        }
    }
}
