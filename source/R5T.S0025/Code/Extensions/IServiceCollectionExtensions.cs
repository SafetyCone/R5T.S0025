using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.Quadia.D002;

using R5T.D0048;
using R5T.D0088.I0002;
using R5T.D0101;
using R5T.D0108;
using R5T.D0109;
using R5T.D0109.I001;
using R5T.T0063;


namespace R5T.S0025
{
    public static partial class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="ListingFilePathProvider"/> implementation of <see cref="IListingFilePathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddListingFilePathProvider(this IServiceCollection services,
            IServiceAction<IOrganizationSharedDataDirectoryFilePathProvider> organizationSharedDataDirectoryFilePathProviderAction)
        {
            services
                .Run(organizationSharedDataDirectoryFilePathProviderAction)
                .AddSingleton<IListingFilePathProvider, ListingFilePathProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="HardCodedFileBasedExtensionMethodBaseExtensionDiscoveryRepositoryFilePathsProvider"/> implementation of <see cref="IFileBasedExtensionMethodBaseExtensionDiscoveryRepositoryFilePathsProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddHardCodedFileBasedExtensionMethodBaseExtensionDiscoveryRepositoryFilePathsProvider(this IServiceCollection services)
        {
            services.AddSingleton<IFileBasedExtensionMethodBaseExtensionDiscoveryRepositoryFilePathsProvider, HardCodedFileBasedExtensionMethodBaseExtensionDiscoveryRepositoryFilePathsProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="FileBasedExtensionMethodBaseExtensionDiscoveryRepository"/> implementation of <see cref="IExtensionMethodBaseExtensionDiscoveryRepository"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddFileBasedExtensionMethodBaseExtensionDiscoveryRepository(this IServiceCollection services,
            IServiceAction<IFileBasedExtensionMethodBaseExtensionDiscoveryRepositoryFilePathsProvider> fileBasedExtensionMethodBaseExtensionDiscoveryRepositoryFilePathsProviderAction)
        {
            services
                .Run(fileBasedExtensionMethodBaseExtensionDiscoveryRepositoryFilePathsProviderAction)
                .AddSingleton<IExtensionMethodBaseExtensionDiscoveryRepository, FileBasedExtensionMethodBaseExtensionDiscoveryRepository>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="ConstructorBasedExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPathProvider"/> implementation of <see cref="IExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddConstructorBasedExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPathProvider(this IServiceCollection services,
            string extensionMethodBaseFunctionalityExtensionMethodBaseProjectPath)
        {
            services.AddSingleton<IExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPathProvider>(_ =>
                new ConstructorBasedExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPathProvider(
                    extensionMethodBaseFunctionalityExtensionMethodBaseProjectPath));

            return services;
        }

        /// <summary>
        /// Adds the <see cref="Repository"/> implementation of <see cref="IRepository"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddRepository(this IServiceCollection services,
            IServiceAction<IExtensionMethodBaseExtensionRepository> extensionMethodBaseExtensionRepositoryAction,
            IServiceAction<IExtensionMethodBaseRepository> extensionMethodBaseRepositoryAction,
            IServiceAction<IProjectRepository> projectRepositoryAction)
        {
            services
                .Run(extensionMethodBaseExtensionRepositoryAction)
                .Run(extensionMethodBaseRepositoryAction)
                .Run(projectRepositoryAction)
                .AddSingleton<IRepository, Repository>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="HardCodedExtensionMethodBaseExtensionRepositoryFilePathsProvider"/> implementation of <see cref="IExtensionMethodBaseExtensionRepositoryFilePathsProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddHardCodedExtensionMethodBaseExtensionRepositoryFilePathsProvider(this IServiceCollection services)
        {
            services.AddSingleton<IExtensionMethodBaseExtensionRepositoryFilePathsProvider, HardCodedExtensionMethodBaseExtensionRepositoryFilePathsProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="BackupExtensionMethodBaseExtensionRepositoryFilePathsProvider"/> implementation of <see cref="IBackupExtensionMethodBaseExtensionRepositoryFilePathsProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddBackupExtensionMethodBaseExtensionRepositoryFilePathsProvider(this IServiceCollection services,
            IServiceAction<IOutputFilePathProvider> outputFilePathProviderAction)
        {
            services
                .Run(outputFilePathProviderAction)
                .AddSingleton<IBackupExtensionMethodBaseExtensionRepositoryFilePathsProvider, BackupExtensionMethodBaseExtensionRepositoryFilePathsProvider>();

            return services;
        }

        public static IServiceCollection AddHostStartup(this IServiceCollection services)
        {
            var dependencyServiceActions = new DependencyServiceActionAggregation();

            services.AddHostStartup<HostStartup>(dependencyServiceActions)
                // Add services required by HostStartup, but not by HostStartupBase.
                ;

            return services;
        }
    }
}
