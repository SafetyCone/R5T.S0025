using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using R5T.Magyar;
using R5T.Ostrogothia.Rivet;

using R5T.A0003;
using R5T.D0048.Default;
using R5T.D0077.A002;
using R5T.D0078.A002;
using R5T.D0079.A002;
using R5T.D0081.I001;
using R5T.D0084.D002.I001;
using R5T.D0084.I0001; 
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
                servicesPlatform.BaseCommandLineOperatorAction,
                notepadPlusPlusExecutableFilePathProviderAction);

            // Visual Studio operators.
            var dotnetOperatorActions = Instances.ServiceAction.AddDotnetOperatorActions(
                servicesPlatform.CommandLineOperatorAction,
                servicesPlatform.SecretsDirectoryFilePathProviderAction);

            var visualStudioSolutionFileOperatorActions = Instances.ServiceAction.AddVisualStudioSolutionFileOperatorActions(
                dotnetOperatorActions.DotnetOperatorAction,
                servicesPlatform.FileNameOperatorAction,
                servicesPlatform.StringlyTypedPathOperatorAction);

            var visualStudioProjectFileOperatorActions = Instances.ServiceAction.AddVisualStudioProjectFileOperatorActions(
                dotnetOperatorActions.DotnetOperatorAction,
                servicesPlatform.FileNameOperatorAction,
                servicesPlatform.StringlyTypedPathOperatorAction);

            // Core competencies.
            var extensionMethodBaseFunctionalityExtensionMethodBaseProjectPathProviderAction = Instances.ServiceAction.AddConstructorBasedExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPathProviderAction(
                @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.T0039\source\R5T.T0039\R5T.T0039.csproj");
            var repositoriesDirectoryPathProviderAction = Instances.ServiceAction.AddConstructorBasedRepositoriesDirectoryPathProviderAction(
                @"C:\Code\DEV\Git\GitHub\SafetyCone");
            var summaryFileNameProviderAction = Instances.ServiceAction.AddConstructorBasedSummaryFileNameProviderAction("Summary-Current Extension Method Base Extensions Analysis.txt");
            var summaryFilePathProviderAction = Instances.ServiceAction.AddSummaryFilePathProviderAction(
                servicesPlatform.OutputFilePathProviderAction,
                summaryFileNameProviderAction);

            // Extension method base extensions discovery repository.
            var fileBasedExtensionMethodBaseExtensionDiscoveryRepositoryFilePathsProviderAction = Instances.ServiceAction.AddHardCodedFileBasedExtensionMethodBaseExtensionDiscoveryRepositoryFilePathsProviderAction();

            var extensionMethodBaseExtensionDiscoveryRepositoryAction = Instances.ServiceAction.AddFileBasedExtensionMethodBaseExtensionDiscoveryRepositoryAction(
                fileBasedExtensionMethodBaseExtensionDiscoveryRepositoryFilePathsProviderAction);

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

            // Project file paths.
            var allProjectDirectoryPathsProviderAction = Instances.ServiceAction.AddAllProjectDirectoryPathsProviderAction(
                projectRepositoryAction,
                servicesPlatform.StringlyTypedPathOperatorAction);

            // Listing file.
            var listingFilePathProviderAction = Instances.ServiceAction.AddListingFilePathProviderAction(
                servicesPlatform.OrganizationSharedDataDirectoryFilePathProviderAction);

            // Operations.
            // Level 01.
            var o001a_AnalyzeAllCurrentEmbExtensionsCoreAction = Instances.ServiceAction.AddO001a_AnalyzeAllCurrentEmbExtensionsCoreAction(
                allProjectDirectoryPathsProviderAction,
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
                listingFilePathProviderAction,
                notepadPlusPlusOperatorAction,
                repositoryAction);
            var o006_UpdateEmbFunctionalityIntellisenseAction = Instances.ServiceAction.AddO006_UpdateEmbFunctionalityIntellisenseAction(
                extensionMethodBaseFunctionalityExtensionMethodBaseProjectPathProviderAction,
                repositoriesDirectoryPathProviderAction,
                repositoryAction,
                visualStudioProjectFileOperatorActions.VisualStudioProjectFileOperatorAction,
                visualStudioSolutionFileOperatorActions.VisualStudioSolutionFileOperatorAction);
            var o007a_UpdateRepositoryWithAllEmbExtensionsAction = Instances.ServiceAction.AddO007a_UpdateRepositoryWithAllEmbExtensionsAction(
                extensionMethodBaseExtensionDiscoveryRepositoryAction,
                extensionMethodBaseExtensionRepositoryAction,
                extensionMethodBaseRepositoryAction,
                notepadPlusPlusOperatorAction,
                projectRepositoryAction,
                summaryFilePathProviderAction);

            // Level 02.
            var o007_UpdateRepositoryWithAllEmbExtensionsAction = Instances.ServiceAction.AddO007_UpdateRepositoryWithAllEmbExtensionsAction(
                o002_BackupFileBasedRepositoryFilesAction,
                o007a_UpdateRepositoryWithAllEmbExtensionsAction);

            var o100_ProcessCurrentEmbExtensionsAction = Instances.ServiceAction.AddO100_ProcessCurrentEmbExtensionsAction(
                o001a_AnalyzeAllCurrentEmbExtensionsCoreAction,
                o001b_SummarizeChangesAction,
                o002_BackupFileBasedRepositoryFilesAction,
                o003a_PerformRequiredHumanActionsAction,
                o004a_UpdateEmbExtensionsRepositoryAction,
                o005a_OutputEmbFunctionalityNamesAction,
                o006_UpdateEmbFunctionalityIntellisenseAction);
            var o101_ProcessEmbExtensionsAction = Instances.ServiceAction.AddO101_ProcessEmbExtensionsAction(
                o005a_OutputEmbFunctionalityNamesAction,
                o006_UpdateEmbFunctionalityIntellisenseAction,
                o007a_UpdateRepositoryWithAllEmbExtensionsAction);
            
            var o900_OpenAllEmbExtensionRepositoryFilesAction = Instances.ServiceAction.AddO900_OpenAllEmbExtensionRepositoryFilesAction(
                extensionMethodBaseExtensionRepositoryFilePathsProviderAction,
                notepadPlusPlusOperatorAction);

            // Level 03.
            var o000_MainAction = Instances.ServiceAction.AddO000_MainAction(
                o101_ProcessEmbExtensionsAction);

            // Run.
            services
                .Run(servicesPlatform.ConfigurationAuditSerializerAction)
                .Run(servicesPlatform.ServiceCollectionAuditSerializerAction)
                // Operations
                .Run(o001_AnalyzeAllCurrentEmbExtensionsAction)
                .Run(o006_UpdateEmbFunctionalityIntellisenseAction)
                .Run(o007_UpdateRepositoryWithAllEmbExtensionsAction)
                .Run(o100_ProcessCurrentEmbExtensionsAction)
                .Run(o101_ProcessEmbExtensionsAction)
                .Run(o900_OpenAllEmbExtensionRepositoryFilesAction)
                .Run(o000_MainAction);
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
