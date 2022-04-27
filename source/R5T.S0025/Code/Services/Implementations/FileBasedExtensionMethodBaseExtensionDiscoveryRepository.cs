using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;using R5T.T0064;



namespace R5T.S0025
{[ServiceImplementationMarker]
    /// <summary>
    /// <inheritdoc cref="IExtensionMethodBaseExtensionDiscoveryRepository"/>
    /// <para>File-based implementation.</para>
    /// </summary>
    public class FileBasedExtensionMethodBaseExtensionDiscoveryRepository : IExtensionMethodBaseExtensionDiscoveryRepository,IServiceImplementation
    {
        private IFileBasedExtensionMethodBaseExtensionDiscoveryRepositoryFilePathsProvider FileBasedExtensionMethodBaseExtensionDiscoveryRepositoryFilePathsProvider { get; }


        public FileBasedExtensionMethodBaseExtensionDiscoveryRepository(
            IFileBasedExtensionMethodBaseExtensionDiscoveryRepositoryFilePathsProvider fileBasedExtensionMethodBaseExtensionDiscoveryRepositoryFilePathsProvider)
        {
            this.FileBasedExtensionMethodBaseExtensionDiscoveryRepositoryFilePathsProvider = fileBasedExtensionMethodBaseExtensionDiscoveryRepositoryFilePathsProvider;
        }

        #region Load/Save

        private async Task<Guid[]> LoadIgnoredExtensionMethodBaseIdentities()
        {
            var ignoredExtensionMethodBaseIdentitiesFilePath = await this.FileBasedExtensionMethodBaseExtensionDiscoveryRepositoryFilePathsProvider
                .GetIgnoredExtensionMethodBaseIdentitiesFilePath();

            var output = Instances.FileSystemOperator.FileExists(ignoredExtensionMethodBaseIdentitiesFilePath)
                ? await Instances.GuidOperator.ReadGuidsFromTextFile(ignoredExtensionMethodBaseIdentitiesFilePath)
                : ArrayHelper.Empty<Guid>()
                ;

            return output;
        }

        private async Task SaveIgnoredExtensionMethodBaseIdentities(IEnumerable<Guid> ignoredExtensionMethodBaseIdentities)
        {
            var ignoredExtensionMethodBaseIdentitiesFilePath = await this.FileBasedExtensionMethodBaseExtensionDiscoveryRepositoryFilePathsProvider
                .GetIgnoredExtensionMethodBaseIdentitiesFilePath();

            await Instances.GuidOperator.WriteGuidsToTextFile(ignoredExtensionMethodBaseIdentitiesFilePath, ignoredExtensionMethodBaseIdentities);
        }

        private async Task<Guid[]> LoadIgnoredProjectIdentities()
        {
            var ignoredProjectIdentitiesFilePath = await this.FileBasedExtensionMethodBaseExtensionDiscoveryRepositoryFilePathsProvider
                .GetIgnoredProjectIdentitiesFilePath();

            var output = Instances.FileSystemOperator.FileExists(ignoredProjectIdentitiesFilePath)
                ? await Instances.GuidOperator.ReadGuidsFromTextFile(ignoredProjectIdentitiesFilePath)
                : ArrayHelper.Empty<Guid>()
                ;

            return output;
        }

        private async Task SaveIgnoredProjectIdentities(IEnumerable<Guid> ignoredProjectIdentities)
        {
            var ignoredProjectIdentitiesFilePath = await this.FileBasedExtensionMethodBaseExtensionDiscoveryRepositoryFilePathsProvider
                .GetIgnoredProjectIdentitiesFilePath();

            await Instances.GuidOperator.WriteGuidsToTextFile(ignoredProjectIdentitiesFilePath, ignoredProjectIdentities);
        }

        #endregion

        public async Task<Dictionary<Guid, bool>> AddIgnoredExtensionMethodBaseIdentities(IEnumerable<Guid> extensionMethodBaseIdentities)
        {
            var ignoredExtensionMethodBaseIdentities = await this.LoadIgnoredExtensionMethodBaseIdentities();

            var output = ignoredExtensionMethodBaseIdentities
                .SetContains(extensionMethodBaseIdentities)
                .Invert();

            var additionalExtensionMethodBaseIdentities = extensionMethodBaseIdentities
                .Where(x => output.ContainsKey(x));

            var modifiedIgnoredExtensionMethodBaseIdentities = ignoredExtensionMethodBaseIdentities
                .AppendRange(additionalExtensionMethodBaseIdentities);

            await this.SaveIgnoredExtensionMethodBaseIdentities(modifiedIgnoredExtensionMethodBaseIdentities);

            return output;
        }

        public async Task<Dictionary<Guid, bool>> AddIgnoredProjectIdentities(IEnumerable<Guid> projectIdenties)
        {
            var ignoredProjectIdentities = await this.LoadIgnoredProjectIdentities();

            var output = ignoredProjectIdentities
                .SetContains(projectIdenties)
                .Invert();

            var additionalProjectIdentities = projectIdenties
                .Where(x => output.ContainsKey(x));

            var modifiedIgnoredProjectIdentities = ignoredProjectIdentities
                .AppendRange(additionalProjectIdentities);

            await this.SaveIgnoredProjectIdentities(modifiedIgnoredProjectIdentities);

            return output;
        }

        public async Task ClearIgnoredExtensionMethodBaseIdentities()
        {
            await this.SaveIgnoredExtensionMethodBaseIdentities(EnumerableHelper.Empty<Guid>());
        }

        public async Task ClearIgnoredProjectIdentities()
        {
            await this.SaveIgnoredProjectIdentities(EnumerableHelper.Empty<Guid>());
        }

        public async Task<Dictionary<Guid, bool>> DeleteIgnoredExtensionMethodBaseIdentities(IEnumerable<Guid> extensionMethodBaseIdentities)
        {
            var ignoredExtensionMethodBaseIdentities = await this.LoadIgnoredExtensionMethodBaseIdentities();

            var output = ignoredExtensionMethodBaseIdentities.SetContains(extensionMethodBaseIdentities);

            var modifiedIgnoredExtensionMethodBaseIdentities = ignoredExtensionMethodBaseIdentities
                .ExceptWhere(x => output.ContainsKey(x));

            await this.SaveIgnoredExtensionMethodBaseIdentities(modifiedIgnoredExtensionMethodBaseIdentities);

            return output;
        }

        public async Task<Dictionary<Guid, bool>> DeleteIgnoredProjectIdentities(IEnumerable<Guid> projectIdentities)
        {
            var ignoredProjectIdentities = await this.LoadIgnoredProjectIdentities();

            var output = ignoredProjectIdentities.SetContains(projectIdentities);

            var modifiedIgnoredProjectIdentities = ignoredProjectIdentities
                .ExceptWhere(x => output.ContainsKey(x));

            await this.SaveIgnoredProjectIdentities(modifiedIgnoredProjectIdentities);

            return output;
        }

        public async Task<Guid[]> GetIgnoredExtensionMethodBaseIdentities()
        {
            var output = await this.LoadIgnoredExtensionMethodBaseIdentities();
            return output;
        }

        public async Task<Guid[]> GetIgnoredProjectIdentities()
        {
            var output = await this.LoadIgnoredProjectIdentities();
            return output;
        }

        public async Task<Dictionary<Guid, bool>> HasIgnoredExtensionMethodBasedentities(IEnumerable<Guid> extensionMethodBaseIdentities)
        {
            var ignoredExtensionMethodBaseIdentities = await this.LoadIgnoredExtensionMethodBaseIdentities();

            var output = ignoredExtensionMethodBaseIdentities.SetContains(extensionMethodBaseIdentities);
            return output;
        }

        public async Task<Dictionary<Guid, bool>> HasIgnoredProjectIdentities(IEnumerable<Guid> projectIdentities)
        {
            var ignoredProjectIdentities = await this.LoadIgnoredProjectIdentities();

            var output = ignoredProjectIdentities.SetContains(projectIdentities);
            return output;
        }
    }
}
