using System;
using System.Threading.Tasks;


namespace R5T.S0025
{
    public class HardCodedFileBasedExtensionMethodBaseExtensionDiscoveryRepositoryFilePathsProvider : IFileBasedExtensionMethodBaseExtensionDiscoveryRepositoryFilePathsProvider
    {
        public Task<string> GetIgnoredExtensionMethodBaseIdentitiesFilePath()
        {
            var output = @"C:\Temp\Ignored Extension Method Base Identities-For EMB Extension Discovery.txt";

            return Task.FromResult(output);
        }

        public Task<string> GetIgnoredProjectIdentitiesFilePath()
        {
            var output = @"C:\Temp\Ignored Project Identities-For EMB Extension Discovery.txt";

            return Task.FromResult(output);
        }
    }
}
