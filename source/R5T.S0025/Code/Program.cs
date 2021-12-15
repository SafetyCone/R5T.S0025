using System;
using System.Threading;
using System.Threading.Tasks;

using R5T.D0088;
using R5T.D0090;
using R5T.D0103.I001;


namespace R5T.S0025
{
    public class Program : ProgramAsAServiceBase
    {
        #region Static

        static async Task Main()
        {
            //OverridableProcessStartTimeProvider.Override("20211214-163052");
            OverridableProcessStartTimeProvider.DoNotOverride();

            await Instances.Host.NewBuilder()
                .UseProgramAsAService<Program, T0075.IHostBuilder>()
                .UseHostStartup<HostStartup, T0075.IHostBuilder>(Instances.ServiceAction.AddStartupAction())
                .Build()
                .SerializeConfigurationAudit()
                .SerializeServiceCollectionAudit()
                .RunAsync();
        }

        #endregion


        public Program(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        protected override Task ServiceMain(CancellationToken stoppingToken)
        {
            return this.RunOperation();
        }

        private async Task RunOperation()
        {
            //await this.ServiceProvider.Run<O900_OpenAllEmbExtensionRepositoryFiles>();

            await this.ServiceProvider.Run<O100_ProcessCurrentEmbExtensions>();

            //await this.ServiceProvider.Run<O001_AnalyzeAllCurrentEmbExtensions>();
        }
    }
}
