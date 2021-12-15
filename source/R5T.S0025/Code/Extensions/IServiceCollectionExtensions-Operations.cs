using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.D0084.D001;
using R5T.D0096;
using R5T.D0101;
using R5T.D0105;
using R5T.D0108;    
using R5T.D0109;
using R5T.D0109.I001;
using R5T.D0110;
using R5T.T0063;


namespace R5T.S0025
{
    public static partial class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="O100_ProcessCurrentEmbExtensions"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO100_ProcessCurrentEmbExtensions(this IServiceCollection services,
            IServiceAction<O001a_AnalyzeAllCurrentEmbExtensionsCore> o001a_AnalyzeAllCurrentEmbExtensionsCoreAction,
            IServiceAction<O001b_SummarizeChanges> o001b_SummarizeChangesAction,
            IServiceAction<O002_BackupFileBasedRepositoryFiles> o002_BackupFileBasedRepositoryFilesAction,
            IServiceAction<O003a_PerformRequiredHumanActions> o003a_PerformRequiredHumanActionsAction,
            IServiceAction<O004a_UpdateEmbExtensionsRepository> o004a_UpdateEmbExtensionsRepositoryAction,
            IServiceAction<O005a_OutputEmbFunctionalityNames> o005a_OutputEmbFunctionalityNamesAction)
        {
            services
                .Run(o001a_AnalyzeAllCurrentEmbExtensionsCoreAction)
                .Run(o001b_SummarizeChangesAction)
                .Run(o002_BackupFileBasedRepositoryFilesAction)
                .Run(o003a_PerformRequiredHumanActionsAction)
                .Run(o004a_UpdateEmbExtensionsRepositoryAction)
                .Run(o005a_OutputEmbFunctionalityNamesAction)
                .AddSingleton<O100_ProcessCurrentEmbExtensions>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O005a_OutputEmbFunctionalityNames"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO005a_OutputEmbFunctionalityNames(this IServiceCollection services,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IRepository> repositoryAction)
        {
            services
                .Run(notepadPlusPlusOperatorAction)
                .Run(repositoryAction)
                .AddSingleton<O005a_OutputEmbFunctionalityNames>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O004a_UpdateEmbExtensionsRepository"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO004a_UpdateEmbExtensionsRepository(this IServiceCollection services,
            IServiceAction<IExtensionMethodBaseExtensionRepository> extensionMethodBaseExtensionRepositoryAction)
        {
            services
                .Run(extensionMethodBaseExtensionRepositoryAction)
                .AddSingleton<O004a_UpdateEmbExtensionsRepository>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O003b_PromptForHumanActions"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO003b_PromptForHumanActions(this IServiceCollection services,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<ISummaryFilePathProvider> summaryFilePathProviderAction)
        {
            services
                .Run(notepadPlusPlusOperatorAction)
                .Run(summaryFilePathProviderAction)
                .AddSingleton<O003b_PromptForHumanActions>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O003a_PerformRequiredHumanActions"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO003a_PerformRequiredHumanActions(this IServiceCollection services,
            IServiceAction<O003b_PromptForHumanActions> o003b_PromptForHumanActionsAction)
        {
            services
                .Run(o003b_PromptForHumanActionsAction)
                .AddSingleton<O003a_PerformRequiredHumanActions>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O002_BackupFileBasedRepositoryFiles"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO002_BackupFileBasedRepositoryFiles(this IServiceCollection services,
            IServiceAction<IBackupExtensionMethodBaseExtensionRepositoryFilePathsProvider> backupExtensionMethodBaseExtensionRepositoryFilePathsProviderAction,
            IServiceAction<IHumanOutput> humanOutputAction,
            IServiceAction<IExtensionMethodBaseExtensionRepositoryFilePathsProvider> extensionMethodBaseExtensionRepositoryFilePathsProviderAction)
        {
            services
                .Run(backupExtensionMethodBaseExtensionRepositoryFilePathsProviderAction)
                .Run(humanOutputAction)
                .Run(extensionMethodBaseExtensionRepositoryFilePathsProviderAction)
                .AddSingleton<O002_BackupFileBasedRepositoryFiles>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O001_AnalyzeAllCurrentEmbExtensions"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO001_AnalyzeAllCurrentEmbExtensions(this IServiceCollection services,
            IServiceAction<O001a_AnalyzeAllCurrentEmbExtensionsCore> o001a_AnalyzeAllCurrentEmbExtensionsCoreAction,
            IServiceAction<O001b_SummarizeChanges> o001b_SummarizeChangesAction)
        {
            services
                .Run(o001a_AnalyzeAllCurrentEmbExtensionsCoreAction)
                .Run(o001b_SummarizeChangesAction)
                .AddSingleton<O001_AnalyzeAllCurrentEmbExtensions>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O001b_SummarizeChanges"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO001b_SummarizeChanges(this IServiceCollection services,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<ISummaryFilePathProvider> summaryFilePathProviderAction)
        {
            services
                .Run(notepadPlusPlusOperatorAction)
                .Run(summaryFilePathProviderAction)
                .AddSingleton<O001b_SummarizeChanges>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O001a_AnalyzeAllCurrentEmbExtensionsCore"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO001a_AnalyzeAllCurrentEmbExtensionsCore(this IServiceCollection services,
            IServiceAction<IAllProjectDirectoryPathsProvider> allProjectDirectoryPathsProviderAction,
            IServiceAction<IExtensionMethodBaseExtensionRepository> extensionMethodBaseExtensionRepositoryAction,
            IServiceAction<IExtensionMethodBaseRepository> extensionMethodBaseRepositoryAction,
            IServiceAction<IProjectRepository> projectRepositoryAction)
        {
            services
                .Run(allProjectDirectoryPathsProviderAction)
                .Run(extensionMethodBaseExtensionRepositoryAction)
                .Run(extensionMethodBaseRepositoryAction)
                .Run(projectRepositoryAction)
                .AddSingleton<O001a_AnalyzeAllCurrentEmbExtensionsCore>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O900_OpenAllEmbExtensionRepositoryFiles"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO900_OpenAllEmbExtensionRepositoryFiles(this IServiceCollection services,
            IServiceAction<IExtensionMethodBaseExtensionRepositoryFilePathsProvider> extensionMethodBaseExtensionRepositoryFilePathsProviderAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction)
        {
            services
                .Run(extensionMethodBaseExtensionRepositoryFilePathsProviderAction)
                .Run(notepadPlusPlusOperatorAction)
                .AddSingleton<O900_OpenAllEmbExtensionRepositoryFiles>();

            return services;
        }
    }
}
