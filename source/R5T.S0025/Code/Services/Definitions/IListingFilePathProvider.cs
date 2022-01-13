using System;
using System.Threading.Tasks;

using R5T.T0064;


namespace R5T.S0025
{
    [ServiceDefinitionMarker]
    public interface IListingFilePathProvider : IServiceDefinition
    {
        Task<string> GetListingFilePath();
    }
}
