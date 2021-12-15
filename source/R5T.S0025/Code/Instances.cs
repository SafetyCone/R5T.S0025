using System;

using R5T.T0036;
using R5T.T0039;
using R5T.T0044;
using R5T.T0062;
using R5T.T0070;
using R5T.T0098;


namespace R5T.S0025
{
    public static class Instances
    {
        public static IExtensionMethodBaseOperator ExtensionMethodBaseOperator { get; } = T0039.ExtensionMethodBaseOperator.Instance;
        public static IFileSystemOperator FileSystemOperator { get; } = T0044.FileSystemOperator.Instance;
        public static IHost Host { get; } = T0070.Host.Instance;
        public static IMethodNameOperator MethodNameOperator { get; } = T0036.MethodNameOperator.Instance;
        public static IServiceAction ServiceAction { get; } = T0062.ServiceAction.Instance;
        public static IOperation Operation { get; } = T0098.Operation.Instance;
    }
}
