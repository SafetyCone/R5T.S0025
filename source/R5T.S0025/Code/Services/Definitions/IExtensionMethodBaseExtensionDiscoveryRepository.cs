using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using R5T.Magyar;


namespace R5T.S0025
{
    /// <summary>
    /// Repository for values related to the extension method base extension discovery process.
    /// </summary>
    public interface IExtensionMethodBaseExtensionDiscoveryRepository
    {
        #region Ignored Projects

        Task<Dictionary<Guid, bool>> AddIgnoredProjectIdentities(IEnumerable<Guid> projectIdenties);

        Task<Guid[]> GetIgnoredProjectIdentities();

        Task<Dictionary<Guid, bool>> HasIgnoredProjectIdentities(IEnumerable<Guid> projectIdentities);

        Task<Dictionary<Guid, bool>> DeleteIgnoredProjectIdentities(IEnumerable<Guid> projectIdentities);

        Task ClearIgnoredProjectIdentities();

        #endregion

        #region Ignored Extension Method Bases

        Task<Dictionary<Guid, bool>> AddIgnoredExtensionMethodBaseIdentities(IEnumerable<Guid> extensionMethodBaseIdentities);

        Task<Guid[]> GetIgnoredExtensionMethodBaseIdentities();

        Task<Dictionary<Guid, bool>> HasIgnoredExtensionMethodBasedentities(IEnumerable<Guid> extensionMethodBaseIdentities);

        Task<Dictionary<Guid, bool>> DeleteIgnoredExtensionMethodBaseIdentities(IEnumerable<Guid> extensionMethodBaseIdentities);

        Task ClearIgnoredExtensionMethodBaseIdentities();

        #endregion
    }
}
