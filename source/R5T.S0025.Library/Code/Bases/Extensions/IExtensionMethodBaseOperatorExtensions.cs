using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.T0039;
using R5T.T0102;

using ExtensionMethodBase = R5T.T0100.ExtensionMethodBase;


// Use specific namespace name.
namespace R5T.S0025.Library
{
    public record ExtensionMethodBaseExtensionTuple(ExtensionMethodBaseExtension ExtensionMethodBaseExtension, ExtensionMethodBase ExtensionMethodBase);


    public static class IExtensionMethodBaseOperatorExtensions
    {
        public static async Task<ExtensionMethodBaseExtensionTuple[]> GetExtensionMethodBaseExtensionTuples(this IExtensionMethodBaseOperator _,
            string projectDirectoryPath,
            IDictionary<string, ExtensionMethodBase> extensionMethodBasesByNamespacedTypeName)
        {
            var output = new List<ExtensionMethodBaseExtensionTuple>();

            var extensionMethodBasesExtensionsCodeFilePaths = Instances.CodeDirectoryOperator.GetBasesExtensionsDirectoryFilePaths(projectDirectoryPath);
            foreach (var filePath in extensionMethodBasesExtensionsCodeFilePaths)
            {
                var compilationUnit = await Instances.CompilationUnitOperator.Load(filePath);

                var extensionMethodBaseExtensionNameTuples = Instances.CompilationUnitOperator.GetExtensionMethodBaseExtensionNameTuples(
                    compilationUnit,
                    extensionMethodBasesByNamespacedTypeName);

                var extensionMethodBaseExtensionTuples = extensionMethodBaseExtensionNameTuples
                    .ExceptWhere(xTuple => xTuple.extensionMethodBase == default) // Filter out those not found.
                    .Select(xTuple =>
                    {
                        var extensionMethodBaseExtension = new ExtensionMethodBaseExtension
                        {
                            //Identity, do not set an identity.
                            NamespacedTypedParameterizedMethodName = xTuple.namespacedTypedParameterizedMethodName,
                            CodeFilePath = filePath,
                        };

                        return new ExtensionMethodBaseExtensionTuple(extensionMethodBaseExtension, xTuple.extensionMethodBase);
                    });

                output.AddRange(extensionMethodBaseExtensionTuples);
            }

            return output.ToArray();
        }
    }
}
