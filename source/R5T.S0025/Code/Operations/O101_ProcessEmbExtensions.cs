using System;
using System.Threading.Tasks;

using R5T.T0020;


namespace R5T.S0025
{
    [OperationMarker]
    public class O101_ProcessEmbExtensions : IActionOperation
    {
        private O005a_OutputEmbFunctionalityNames O005A_OutputEmbFunctionalityNames { get; }
        private O006_UpdateEmbFunctionalityIntellisense O006_UpdateEmbFunctionalityIntellisense { get; }
        private O007a_UpdateRepositoryWithAllEmbExtensions O007A_UpdateRepositoryWithAllEmbExtensions { get; }


        public O101_ProcessEmbExtensions(
            O005a_OutputEmbFunctionalityNames o005A_OutputEmbFunctionalityNames,
            O006_UpdateEmbFunctionalityIntellisense o006_UpdateEmbFunctionalityIntellisense,
            O007a_UpdateRepositoryWithAllEmbExtensions o007A_UpdateRepositoryWithAllEmbExtensions)
        {
            this.O005A_OutputEmbFunctionalityNames = o005A_OutputEmbFunctionalityNames;
            this.O006_UpdateEmbFunctionalityIntellisense = o006_UpdateEmbFunctionalityIntellisense;
            this.O007A_UpdateRepositoryWithAllEmbExtensions = o007A_UpdateRepositoryWithAllEmbExtensions;
        }

        public async Task Run()
        {
            // Order operations matters (out-of-sequence ok).
            await this.O007A_UpdateRepositoryWithAllEmbExtensions.Run();
            await this.O006_UpdateEmbFunctionalityIntellisense.Run();
            await this.O005A_OutputEmbFunctionalityNames.Run();
        }
    }
}
