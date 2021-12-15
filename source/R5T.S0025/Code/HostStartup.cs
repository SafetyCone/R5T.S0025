using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using R5T.Magyar;
using R5T.Ostrogothia.Rivet;

using R5T.A0003;
using R5T.D0048.Default;
using R5T.D0081.I001;
using R5T.D0084.A001;
using R5T.D0084.D002.I001;
using R5T.D0088.I0002;
using R5T.D0101.I0001;
using R5T.D0101.I001;
using R5T.D0105.I001;
using R5T.D0108.I0001;
using R5T.D0108.I001;
using R5T.D0109.I001;
using R5T.D0110.I001;
using R5T.D0110.I002;
using R5T.T0063;

using IProvidedServiceActionAggregation = R5T.D0088.I0002.IProvidedServiceActionAggregation;
using IRequiredServiceActionAggregation = R5T.D0088.I0002.IRequiredServiceActionAggregation;
using ServicesPlatformRequiredServiceActionAggregation = R5T.A0003.RequiredServiceActionAggregation;


namespace R5T.S0025
{
    public class HostStartup : HostStartupBase
    {
        public override Task ConfigureConfiguration(IConfigurationBuilder configurationBuilder)
        {
            // Do nothing.

            return Task.CompletedTask;
        }

        protected override Task ConfigureServices(IServiceCollection services, IProvidedServiceActionAggregation providedServicesAggregation)
        {
            // Inputs.
            var executionSynchronicityProviderAction = Instances.ServiceAction.AddConstructorBasedExecutionSynchronicityProviderAction(Synchronicity.Synchronous);
            var organizationProviderAction = Instances.ServiceAction.AddOrganizationProviderAction(); // Rivet organization.
            var rootOutputDirectoryPathProviderAction = Instances.ServiceAction.AddConstructorBasedRootOutputDirectoryPathProviderAction(@"C:\Temp\Output");

            // Services platform.
            var servicesPlatformRequiredServiceActionAggregation = new ServicesPlatformRequiredServiceActionAggregation
            {
                ConfigurationAction = providedServicesAggregation.ConfigurationAction,
                ExecutionSynchronicityProviderAction = executionSynchronicityProviderAction,
                LoggerAction = providedServicesAggregation.LoggerAction,
                LoggerFactoryAction = providedServicesAggregation.LoggerFactoryAction,
                MachineMessageOutputSinkProviderActions = EnumerableHelper.Empty<IServiceAction<D0099.D002.IMachineMessageOutputSinkProvider>>(),
                MachineMessageTypeJsonSerializationHandlerActions = EnumerableHelper.Empty<IServiceAction<D0098.IMachineMessageTypeJsonSerializationHandler>>(),
                OrganizationProviderAction = organizationProviderAction,
                RootOutputDirectoryPathProviderAction = rootOutputDirectoryPathProviderAction,
            };

            var servicesPlatform = Instances.ServiceAction.AddProvidedServiceActionAggregation(
                servicesPlatformRequiredServiceActionAggregation);

            // Utility services.
            var notepadPlusPlusExecutableFilePathProviderAction = Instances.ServiceAction.AddHardCodedNotepadPlusPlusExecutableFilePathProviderAction();
            var notepadPlusPlusOperatorAction = Instances.ServiceAction.AddNotepadPlusPlusOperatorAction(
                servicesPlatform.CommandLineOperatorAction,
                notepadPlusPlusExecutableFilePathProviderAction);

            // Core competencies.
            var summaryFileNameProviderAction = Instances.ServiceAction.AddConstructorBasedSummaryFileNameProviderAction("Summary-Current Extension Method Base Extensions Analysis.txt");
            var summaryFilePathProviderAction = Instances.ServiceAction.AddSummaryFilePathProviderAction(
                servicesPlatform.OutputFilePathProviderAction,
                summaryFileNameProviderAction);

            // Project file paths.
            var repositoriesDirectoryPathProviderAction = Instances.ServiceAction.AddConstructorBasedRepositoriesDirectoryPathProviderAction(@"C:\Code\DEV\Git\GitHub\SafetyCone");

            var allProjectFilePathsProviderServiceActions = Instances.ServiceAction.AddAllProjectFilePathsProviderServiceActions(
                repositoriesDirectoryPathProviderAction);

            // Extension method base extensions repository.
            var extensionMethodBaseExtensionRepositoryFilePathsProviderAction = Instances.ServiceAction.AddHardCodedExtensionMethodBaseExtensionRepositoryFilePathsProviderAction();

            var fileBasedExtensionMethodBaseExtensionRepositoryAction = Instances.ServiceAction.AddFileBasedExtensionMethodBaseExtensionRepositoryAction(
                extensionMethodBaseExtensionRepositoryFilePathsProviderAction);
            var backupExtensionMethodBaseExtensionRepositoryFilePathsProviderAction = Instances.ServiceAction.AddBackupExtensionMethodBaseExtensionRepositoryFilePathsProviderAction(
                servicesPlatform.OutputFilePathProviderAction);

            var extensionMethodBaseExtensionRepositoryAction = Instances.ServiceAction.ForwardToIExtensionMethodBaseExtensionRepositoryAction(fileBasedExtensionMethodBaseExtensionRepositoryAction);

            // Project repository.
            var projectRepositoryFilePathsProviderAction = Instances.ServiceAction.AddHardCodedProjectRepositoryFilePathsProviderAction();

            var fileBasedProjectRepositoryAction = Instances.ServiceAction.AddFileBasedProjectRepositoryAction(
                projectRepositoryFilePathsProviderAction);

            var projectRepositoryAction = Instances.ServiceAction.ForwardFileBasedProjectRepositoryToProjectRepositoryAction(
                fileBasedProjectRepositoryAction);

            // Extension method base repository.
            var extensionMethodBaseRepositoryFilePathsProviderAction = Instances.ServiceAction.AddHardCodedExtensionMethodBaseRepositoryFilePathsProviderAction();

            var fileBasedExtensionMethodBaseRepositoryAction = Instances.ServiceAction.AddFileBasedExtensionMethodBaseRepositoryAction(
                extensionMethodBaseRepositoryFilePathsProviderAction);

            var extensionMethodBaseRepositoryAction = Instances.ServiceAction.ForwardToIExtensionMethodBaseRepositoryAction(
                fileBasedExtensionMethodBaseRepositoryAction);

            var repositoryAction = Instances.ServiceAction.AddRepositoryAction(
                extensionMethodBaseExtensionRepositoryAction,
                extensionMethodBaseRepositoryAction,
                projectRepositoryAction);

            // Operations.
            var o001a_AnalyzeAllCurrentEmbExtensionsCoreAction = Instances.ServiceAction.AddO001a_AnalyzeAllCurrentEmbExtensionsCoreAction(
                allProjectFilePathsProviderServiceActions.AllProjectDirectoryPathsProviderAction,
                extensionMethodBaseExtensionRepositoryAction,
                extensionMethodBaseRepositoryAction,
                projectRepositoryAction);
            var o001b_SummarizeChangesAction = Instances.ServiceAction.AddO001b_SummarizeChangesAction(
                notepadPlusPlusOperatorAction,
                summaryFilePathProviderAction);
            var o001_AnalyzeAllCurrentEmbExtensionsAction = Instances.ServiceAction.AddO001_AnalyzeAllCurrentEmbExtensionsAction(
                o001a_AnalyzeAllCurrentEmbExtensionsCoreAction,
                o001b_SummarizeChangesAction);
            var o002_BackupFileBasedRepositoryFilesAction = Instances.ServiceAction.AddO002_BackupFileBasedRepositoryFilesAction(
                backupExtensionMethodBaseExtensionRepositoryFilePathsProviderAction,
                servicesPlatform.HumanOutputAction,
                extensionMethodBaseExtensionRepositoryFilePathsProviderAction);
            var o003b_PromptForHumanActionsAction = Instances.ServiceAction.AddO003b_PromptForHumanActionsAction(
                notepadPlusPlusOperatorAction,
                summaryFilePathProviderAction);
            var o003a_PerformRequiredHumanActionsAction = Instances.ServiceAction.AddO003a_PerformRequiredHumanActionsAction(
                o003b_PromptForHumanActionsAction);
            var o004a_UpdateEmbExtensionsRepositoryAction = Instances.ServiceAction.AddO004a_UpdateEmbExtensionsRepositoryAction(
                extensionMethodBaseExtensionRepositoryAction);
            var o005a_OutputEmbFunctionalityNamesAction = Instances.ServiceAction.AddO005a_OutputEmbFunctionalityNamesAction(
                notepadPlusPlusOperatorAction,
                repositoryAction);

            var o100_ProcessCurrentEmbExtensionsAction = Instances.ServiceAction.AddO100_ProcessCurrentEmbExtensionsAction(
                o001a_AnalyzeAllCurrentEmbExtensionsCoreAction,
                o001b_SummarizeChangesAction,
                o002_BackupFileBasedRepositoryFilesAction,
                o003a_PerformRequiredHumanActionsAction,
                o004a_UpdateEmbExtensionsRepositoryAction,
                o005a_OutputEmbFunctionalityNamesAction);
            
            var o900_OpenAllEmbExtensionRepositoryFilesAction = Instances.ServiceAction.AddO900_OpenAllEmbExtensionRepositoryFilesAction(
                extensionMethodBaseExtensionRepositoryFilePathsProviderAction,
                notepadPlusPlusOperatorAction);

            // Run.
            services
                .Run(servicesPlatform.ConfigurationAuditSerializerAction)
                .Run(servicesPlatform.ServiceCollectionAuditSerializerAction)
                // Operations
                .Run(o001_AnalyzeAllCurrentEmbExtensionsAction)
                .Run(o100_ProcessCurrentEmbExtensionsAction)
                .Run(o900_OpenAllEmbExtensionRepositoryFilesAction)
                ;

            return Task.CompletedTask;
        }

        protected override Task FillRequiredServiceActions(IRequiredServiceActionAggregation requiredServiceActions)
        {
            // Do nothing since none are required.

            return Task.CompletedTask;
        }
    }
}
