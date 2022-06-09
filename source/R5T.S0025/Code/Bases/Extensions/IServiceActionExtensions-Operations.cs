using System;

using R5T.D0078;
using R5T.D0079;
using R5T.D0084.D001;
using R5T.D0084.D002;
using R5T.D0096;
using R5T.D0101;
using R5T.D0105;
using R5T.D0108;
using R5T.D0109;
using R5T.D0109.I001;
using R5T.D0110;
using R5T.T0062;
using R5T.T0063;


namespace R5T.S0025
{
    public static partial class IServiceActionExtensions
    {
        /// <summary>
        /// Adds the <see cref="O000_Main"/> operation as a <see cref="Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O000_Main> AddO000_MainAction(this IServiceAction _,
            IServiceAction<O101_ProcessEmbExtensions> o101_ProcessEmbExtensionsAction)
        {
            var serviceAction = _.New<O000_Main>(services => services.AddO000_Main(
                o101_ProcessEmbExtensionsAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O101_ProcessEmbExtensions"/> operation as a <see cref="Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O101_ProcessEmbExtensions> AddO101_ProcessEmbExtensionsAction(this IServiceAction _,
            IServiceAction<O005a_OutputEmbFunctionalityNames> o005a_OutputEmbFunctionalityNamesAction,
            IServiceAction<O006_UpdateEmbFunctionalityIntellisense> o006_UpdateEmbFunctionalityIntellisenseAction,
            IServiceAction<O007a_UpdateRepositoryWithAllEmbExtensions> o007a_UpdateRepositoryWithAllEmbExtensionsAction)
        {
            var serviceAction = _.New<O101_ProcessEmbExtensions>(services => services.AddO101_ProcessEmbExtensions(
                o005a_OutputEmbFunctionalityNamesAction,
                o006_UpdateEmbFunctionalityIntellisenseAction,
                o007a_UpdateRepositoryWithAllEmbExtensionsAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O100_ProcessCurrentEmbExtensions"/> operation as a <see cref="Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O100_ProcessCurrentEmbExtensions> AddO100_ProcessCurrentEmbExtensionsAction(this IServiceAction _,
            IServiceAction<O001a_AnalyzeAllCurrentEmbExtensionsCore> o001a_AnalyzeAllCurrentEmbExtensionsCoreAction,
            IServiceAction<O001b_SummarizeChanges> o001b_SummarizeChangesAction,
            IServiceAction<O002_BackupFileBasedRepositoryFiles> o002_BackupFileBasedRepositoryFilesAction,
            IServiceAction<O003a_PerformRequiredHumanActions> o003a_PerformRequiredHumanActionsAction,
            IServiceAction<O004a_UpdateEmbExtensionsRepository> o004a_UpdateEmbExtensionsRepositoryAction,
            IServiceAction<O005a_OutputEmbFunctionalityNames> o005a_OutputEmbFunctionalityNamesAction,
            IServiceAction<O006_UpdateEmbFunctionalityIntellisense> o006_UpdateEmbFunctionalityIntellisenseAction)
        {
            var serviceAction = _.New<O100_ProcessCurrentEmbExtensions>(services => services.AddO100_ProcessCurrentEmbExtensions(
                o001a_AnalyzeAllCurrentEmbExtensionsCoreAction,
                o001b_SummarizeChangesAction,
                o002_BackupFileBasedRepositoryFilesAction,
                o003a_PerformRequiredHumanActionsAction,
                o004a_UpdateEmbExtensionsRepositoryAction,
                o005a_OutputEmbFunctionalityNamesAction,
                o006_UpdateEmbFunctionalityIntellisenseAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O007a_UpdateRepositoryWithAllEmbExtensions"/> operation as a <see cref="Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O007a_UpdateRepositoryWithAllEmbExtensions> AddO007a_UpdateRepositoryWithAllEmbExtensionsAction(this IServiceAction _,
            IServiceAction<IExtensionMethodBaseExtensionDiscoveryRepository> extensionMethodBaseExtensionDiscoveryRepositoryAction,
            IServiceAction<IExtensionMethodBaseExtensionRepository> extensionMethodBaseExtensionRepositoryAction,
            IServiceAction<IExtensionMethodBaseRepository> extensionMethodBaseRepositoryAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IProjectRepository> projectRepositoryAction,
            IServiceAction<ISummaryFilePathProvider> summaryFilePathProviderAction)
        {
            var serviceAction = _.New<O007a_UpdateRepositoryWithAllEmbExtensions>(services => services.AddO007a_UpdateRepositoryWithAllEmbExtensions(
                extensionMethodBaseExtensionDiscoveryRepositoryAction,
                extensionMethodBaseExtensionRepositoryAction,
                extensionMethodBaseRepositoryAction,
                notepadPlusPlusOperatorAction,
                projectRepositoryAction,
                summaryFilePathProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O007_UpdateRepositoryWithAllEmbExtensions"/> operation as a <see cref="Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O007_UpdateRepositoryWithAllEmbExtensions> AddO007_UpdateRepositoryWithAllEmbExtensionsAction(this IServiceAction _,
            IServiceAction<O002_BackupFileBasedRepositoryFiles> o002_BackupFileBasedRepositoryFilesAction,
            IServiceAction<O007a_UpdateRepositoryWithAllEmbExtensions> o007a_UpdateRepositoryWithAllEmbExtensionsAction)
        {
            var serviceAction = _.New<O007_UpdateRepositoryWithAllEmbExtensions>(services => services.AddO007_UpdateRepositoryWithAllEmbExtensions(
                o002_BackupFileBasedRepositoryFilesAction,
                o007a_UpdateRepositoryWithAllEmbExtensionsAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O006_UpdateEmbFunctionalityIntellisense"/> operation as a <see cref="Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O006_UpdateEmbFunctionalityIntellisense> AddO006_UpdateEmbFunctionalityIntellisenseAction(this IServiceAction _,
            IServiceAction<IExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPathProvider> extensionMethodBaseFunctionalityExtensionMethodBaseProjectPathProviderAction,
            IServiceAction<IRepositoriesDirectoryPathProvider> repositoriesDirectoryPathProviderAction,
            IServiceAction<IRepository> repositoryAction,
            IServiceAction<IVisualStudioProjectFileOperator> visualStudioProjectFileOperatorAction,
            IServiceAction<IVisualStudioSolutionFileOperator> visualStudioSolutionFileOperatorAction)
        {
            var serviceAction = _.New<O006_UpdateEmbFunctionalityIntellisense>(services => services.AddO006_UpdateEmbFunctionalityIntellisense(
                extensionMethodBaseFunctionalityExtensionMethodBaseProjectPathProviderAction,
                repositoriesDirectoryPathProviderAction,
                repositoryAction,
                visualStudioProjectFileOperatorAction,
                visualStudioSolutionFileOperatorAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O005a_OutputEmbFunctionalityNames"/> operation as a <see cref="Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O005a_OutputEmbFunctionalityNames> AddO005a_OutputEmbFunctionalityNamesAction(this IServiceAction _,
            IServiceAction<IListingFilePathProvider> listingFilePathProviderAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IRepository> repositoryAction)
        {
            var serviceAction = _.New<O005a_OutputEmbFunctionalityNames>(services => services.AddO005a_OutputEmbFunctionalityNames(
                listingFilePathProviderAction,
                notepadPlusPlusOperatorAction,
                repositoryAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O004a_UpdateEmbExtensionsRepository"/> operation as a <see cref="Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O004a_UpdateEmbExtensionsRepository> AddO004a_UpdateEmbExtensionsRepositoryAction(this IServiceAction _,
            IServiceAction<IExtensionMethodBaseExtensionRepository> extensionMethodBaseExtensionRepositoryAction)
        {
            var serviceAction = _.New<O004a_UpdateEmbExtensionsRepository>(services => services.AddO004a_UpdateEmbExtensionsRepository(
                extensionMethodBaseExtensionRepositoryAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O003b_PromptForHumanActions"/> operation as a <see cref="Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O003b_PromptForHumanActions> AddO003b_PromptForHumanActionsAction(this IServiceAction _,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<ISummaryFilePathProvider> summaryFilePathProviderAction)
        {
            var serviceAction = _.New<O003b_PromptForHumanActions>(services => services.AddO003b_PromptForHumanActions(
                notepadPlusPlusOperatorAction,
                summaryFilePathProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O003a_PerformRequiredHumanActions"/> operation as a <see cref="Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O003a_PerformRequiredHumanActions> AddO003a_PerformRequiredHumanActionsAction(this IServiceAction _,
            IServiceAction<O003b_PromptForHumanActions> o003b_PromptForHumanActionsAction)
        {
            var serviceAction = _.New<O003a_PerformRequiredHumanActions>(services => services.AddO003a_PerformRequiredHumanActions(
                o003b_PromptForHumanActionsAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O002_BackupFileBasedRepositoryFiles"/> operation as a <see cref="Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O002_BackupFileBasedRepositoryFiles> AddO002_BackupFileBasedRepositoryFilesAction(this IServiceAction _,
            IServiceAction<IBackupExtensionMethodBaseExtensionRepositoryFilePathsProvider> backupExtensionMethodBaseExtensionRepositoryFilePathsProviderAction,
            IServiceAction<IHumanOutput> humanOutputAction,
            IServiceAction<IExtensionMethodBaseExtensionRepositoryFilePathsProvider> extensionMethodBaseExtensionRepositoryFilePathsProviderAction)
        {
            var serviceAction = _.New<O002_BackupFileBasedRepositoryFiles>(services => services.AddO002_BackupFileBasedRepositoryFiles(
                backupExtensionMethodBaseExtensionRepositoryFilePathsProviderAction,
                humanOutputAction,
                extensionMethodBaseExtensionRepositoryFilePathsProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O001_AnalyzeAllCurrentEmbExtensions"/> operation as a <see cref="Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O001_AnalyzeAllCurrentEmbExtensions> AddO001_AnalyzeAllCurrentEmbExtensionsAction(this IServiceAction _,
            IServiceAction<O001a_AnalyzeAllCurrentEmbExtensionsCore> o001a_AnalyzeAllCurrentEmbExtensionsCoreAction,
            IServiceAction<O001b_SummarizeChanges> o001b_SummarizeChangesAction)
        {
            var serviceAction = _.New<O001_AnalyzeAllCurrentEmbExtensions>(services => services.AddO001_AnalyzeAllCurrentEmbExtensions(
                o001a_AnalyzeAllCurrentEmbExtensionsCoreAction,
                o001b_SummarizeChangesAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O001b_SummarizeChanges"/> operation as a <see cref="Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O001b_SummarizeChanges> AddO001b_SummarizeChangesAction(this IServiceAction _,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<ISummaryFilePathProvider> summaryFilePathProviderAction)
        {
            var serviceAction = _.New<O001b_SummarizeChanges>(services => services.AddO001b_SummarizeChanges(
                notepadPlusPlusOperatorAction,
                summaryFilePathProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O001a_AnalyzeAllCurrentEmbExtensionsCore"/> operation as a <see cref="Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O001a_AnalyzeAllCurrentEmbExtensionsCore> AddO001a_AnalyzeAllCurrentEmbExtensionsCoreAction(this IServiceAction _,
            IServiceAction<IAllProjectDirectoryPathsProvider> allProjectDirectoryPathsProviderAction,
            IServiceAction<IExtensionMethodBaseExtensionRepository> extensionMethodBaseExtensionRepositoryAction,
            IServiceAction<IExtensionMethodBaseRepository> extensionMethodBaseRepositoryAction,
            IServiceAction<IProjectRepository> projectRepositoryAction)
        {
            var serviceAction = _.New<O001a_AnalyzeAllCurrentEmbExtensionsCore>(services => services.AddO001a_AnalyzeAllCurrentEmbExtensionsCore(
                allProjectDirectoryPathsProviderAction,
                extensionMethodBaseExtensionRepositoryAction,
                extensionMethodBaseRepositoryAction,
                projectRepositoryAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O900_OpenAllEmbExtensionRepositoryFiles"/> operation as a <see cref="Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O900_OpenAllEmbExtensionRepositoryFiles> AddO900_OpenAllEmbExtensionRepositoryFilesAction(this IServiceAction _,
            IServiceAction<IExtensionMethodBaseExtensionRepositoryFilePathsProvider> extensionMethodBaseExtensionRepositoryFilePathsProviderAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction)
        {
            var serviceAction = _.New<O900_OpenAllEmbExtensionRepositoryFiles>(services => services.AddO900_OpenAllEmbExtensionRepositoryFiles(
                extensionMethodBaseExtensionRepositoryFilePathsProviderAction,
                notepadPlusPlusOperatorAction));

            return serviceAction;
        }
    }
}
