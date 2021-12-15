using System;

using R5T.D0048;
using R5T.D0101;
using R5T.D0108;
using R5T.D0109;
using R5T.D0109.I001;
using R5T.T0062;
using R5T.T0063;


namespace R5T.S0025
{
    public static partial class IServiceActionExtensions
    {
        /// <summary>
        /// Adds the <see cref="Repository"/> implementation of <see cref="IRepository"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IRepository> AddRepositoryAction(this IServiceAction _,
            IServiceAction<IExtensionMethodBaseExtensionRepository> extensionMethodBaseExtensionRepositoryAction,
            IServiceAction<IExtensionMethodBaseRepository> extensionMethodBaseRepositoryAction,
            IServiceAction<IProjectRepository> projectRepositoryAction)
        {
            var serviceAction = _.New<IRepository>(services => services.AddRepository(
                extensionMethodBaseExtensionRepositoryAction,
                extensionMethodBaseRepositoryAction,
                projectRepositoryAction));
            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="BackupExtensionMethodBaseExtensionRepositoryFilePathsProvider"/> implementation of <see cref="IBackupExtensionMethodBaseExtensionRepositoryFilePathsProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IBackupExtensionMethodBaseExtensionRepositoryFilePathsProvider> AddBackupExtensionMethodBaseExtensionRepositoryFilePathsProviderAction(this IServiceAction _,
            IServiceAction<IOutputFilePathProvider> outputFilePathProviderAction)
        {
            var serviceAction = _.New<IBackupExtensionMethodBaseExtensionRepositoryFilePathsProvider>(services => services.AddBackupExtensionMethodBaseExtensionRepositoryFilePathsProvider(
                outputFilePathProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="HardCodedExtensionMethodBaseExtensionRepositoryFilePathsProvider"/> implementation of <see cref="IExtensionMethodBaseExtensionRepositoryFilePathsProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IExtensionMethodBaseExtensionRepositoryFilePathsProvider> AddHardCodedExtensionMethodBaseExtensionRepositoryFilePathsProviderAction(this IServiceAction _)
        {
            var serviceAction = _.New<IExtensionMethodBaseExtensionRepositoryFilePathsProvider>(services => services.AddHardCodedExtensionMethodBaseExtensionRepositoryFilePathsProvider());
            return serviceAction;
        }

        public static IServiceAction<HostStartup> AddStartupAction(this IServiceAction _)
        {
            var output = _.New<HostStartup>(services => services.AddHostStartup());

            return output;
        }
    }
}
