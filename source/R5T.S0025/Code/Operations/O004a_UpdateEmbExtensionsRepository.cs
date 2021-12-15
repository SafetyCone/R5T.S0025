using System;
using System.Threading.Tasks;

using R5T.D0109;

using R5T.S0025.Library;


namespace R5T.S0025
{
    public class O004a_UpdateEmbExtensionsRepository
    {
        private IExtensionMethodBaseExtensionRepository ExtensionMethodBaseExtensionRepository { get; }


        public O004a_UpdateEmbExtensionsRepository(
            IExtensionMethodBaseExtensionRepository extensionMethodBaseExtensionRepository)
        {
            this.ExtensionMethodBaseExtensionRepository = extensionMethodBaseExtensionRepository;
        }

        public async Task Run(
            AnalysisOutputData analysisOutputData)
        {
            await Instances.Operation.UpdateEmbExtensionsRepository(
                analysisOutputData,
                this.ExtensionMethodBaseExtensionRepository);
        }
    }
}
