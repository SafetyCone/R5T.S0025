using System;
using System.Threading.Tasks;

using R5T.T0020;


namespace R5T.S0025
{
    [OperationMarker]
    public class O000_Main : IActionOperation
    {
        private O101_ProcessEmbExtensions O101_ProcessEmbExtensions { get; }


        public O000_Main(
            O101_ProcessEmbExtensions o101_ProcessEmbExtensions)
        {
            this.O101_ProcessEmbExtensions = o101_ProcessEmbExtensions;
        }

        public async Task Run()
        {
            await this.O101_ProcessEmbExtensions.Run();
        }
    }
}
