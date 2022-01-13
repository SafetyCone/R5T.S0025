using System;
using System.Threading.Tasks;


namespace R5T.S0025
{
    public interface IFileBasedExtensionMethodBaseExtensionDiscoveryRepositoryFilePathsProvider
    {
        Task<string> GetIgnoredExtensionMethodBaseIdentitiesFilePath();
        Task<string> GetIgnoredProjectIdentitiesFilePath();
    }
}
