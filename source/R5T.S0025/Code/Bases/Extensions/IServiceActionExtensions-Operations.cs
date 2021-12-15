using System;

using R5T.D0084.D001;
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
        /// Adds the <see cref="O100_ProcessCurrentEmbExtensions"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O100_ProcessCurrentEmbExtensions> AddO100_ProcessCurrentEmbExtensionsAction(this IServiceAction _,
            IServiceAction<O001a_AnalyzeAllCurrentEmbExtensionsCore> o001a_AnalyzeAllCurrentEmbExtensionsCoreAction,
            IServiceAction<O001b_SummarizeChanges> o001b_SummarizeChangesAction,
            IServiceAction<O002_BackupFileBasedRepositoryFiles> o002_BackupFileBasedRepositoryFilesAction,
            IServiceAction<O003a_PerformRequiredHumanActions> o003a_PerformRequiredHumanActionsAction,
            IServiceAction<O004a_UpdateEmbExtensionsRepository> o004a_UpdateEmbExtensionsRepositoryAction,
            IServiceAction<O005a_OutputEmbFunctionalityNames> o005a_OutputEmbFunctionalityNamesAction)
        {
            var serviceAction = _.New<O100_ProcessCurrentEmbExtensions>(services => services.AddO100_ProcessCurrentEmbExtensions(
                o001a_AnalyzeAllCurrentEmbExtensionsCoreAction,
                o001b_SummarizeChangesAction,
                o002_BackupFileBasedRepositoryFilesAction,
                o003a_PerformRequiredHumanActionsAction,
                o004a_UpdateEmbExtensionsRepositoryAction,
                o005a_OutputEmbFunctionalityNamesAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O005a_OutputEmbFunctionalityNames"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O005a_OutputEmbFunctionalityNames> AddO005a_OutputEmbFunctionalityNamesAction(this IServiceAction _,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IRepository> repositoryAction)
        {
            var serviceAction = _.New<O005a_OutputEmbFunctionalityNames>(services => services.AddO005a_OutputEmbFunctionalityNames(
                notepadPlusPlusOperatorAction,
                repositoryAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O004a_UpdateEmbExtensionsRepository"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O004a_UpdateEmbExtensionsRepository> AddO004a_UpdateEmbExtensionsRepositoryAction(this IServiceAction _,
            IServiceAction<IExtensionMethodBaseExtensionRepository> extensionMethodBaseExtensionRepositoryAction)
        {
            var serviceAction = _.New<O004a_UpdateEmbExtensionsRepository>(services => services.AddO004a_UpdateEmbExtensionsRepository(
                extensionMethodBaseExtensionRepositoryAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O003b_PromptForHumanActions"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
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
        /// Adds the <see cref="O003a_PerformRequiredHumanActions"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O003a_PerformRequiredHumanActions> AddO003a_PerformRequiredHumanActionsAction(this IServiceAction _,
            IServiceAction<O003b_PromptForHumanActions> o003b_PromptForHumanActionsAction)
        {
            var serviceAction = _.New<O003a_PerformRequiredHumanActions>(services => services.AddO003a_PerformRequiredHumanActions(
                o003b_PromptForHumanActionsAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O002_BackupFileBasedRepositoryFiles"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
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
        /// Adds the <see cref="O001_AnalyzeAllCurrentEmbExtensions"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
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
        /// Adds the <see cref="O001b_SummarizeChanges"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
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
        /// Adds the <see cref="O001a_AnalyzeAllCurrentEmbExtensionsCore"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
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
        /// Adds the <see cref="O900_OpenAllEmbExtensionRepositoryFiles"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
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
