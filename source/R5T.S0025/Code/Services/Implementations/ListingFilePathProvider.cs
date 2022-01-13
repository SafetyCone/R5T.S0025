using System;
using System.Threading.Tasks;

using R5T.Quadia.D002;

using R5T.T0064;


namespace R5T.S0025
{
    [ServiceImplementationMarker]
    public class ListingFilePathProvider : IListingFilePathProvider, IServiceImplementation
    {
        public const string FileName = "Extension Method Base Extensions-Functionality-All.txt";


        private IOrganizationSharedDataDirectoryFilePathProvider OrganizationSharedDataDirectoryFilePathProvider { get; }


        public ListingFilePathProvider(
            IOrganizationSharedDataDirectoryFilePathProvider organizationSharedDataDirectoryFilePathProvider)
        {
            this.OrganizationSharedDataDirectoryFilePathProvider = organizationSharedDataDirectoryFilePathProvider;
        }

        public async Task<string> GetListingFilePath()
        {
            var output = await this.OrganizationSharedDataDirectoryFilePathProvider.GetFilePath(ListingFilePathProvider.FileName);
            return output;
        }
    }
}
