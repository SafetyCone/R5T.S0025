using System;

using R5T.T0036;
using R5T.T0097;
using R5T.T0100;
using R5T.T0102;

using Instances = R5T.S0025.Library.Instances;

namespace System
{
    public static class IMethodNameOperatorExtensions
    {
        public static string GetTypeBasedProjectedMethodName(this IMethodNameOperator _,
            string fullMethodName,
            string extensionMethodBaseNamespacedTypeName,
            string projectName)
        {
            var typeName = Instances.NamespacedTypeName.GetTypeName(extensionMethodBaseNamespacedTypeName);

            var methodName = _.GetMethodNameFromFullMethodNameWithoutParentheses(fullMethodName);

            var modifiedTypeName = typeName.Replace(Characters.Period, Characters.Underscore);
            var modifiedProjectName = projectName.Replace(Characters.Period, Characters.Underscore);

            var usefulProjectedMethodName = $"{modifiedTypeName}_{methodName}_{modifiedProjectName}";
            return usefulProjectedMethodName;
        }

        public static string GetTypeBasedProjectedMethodName(this IMethodNameOperator _,
            (ExtensionMethodBaseExtension ExtensionMethodBaseExtension, ExtensionMethodBase ExtensionMethodBase, Project Project) tuple)
        {
            var output = _.GetTypeBasedProjectedMethodName(
                tuple.ExtensionMethodBaseExtension.NamespacedTypedParameterizedMethodName,
                tuple.ExtensionMethodBase.NamespacedTypeName,
                tuple.Project.Name);

            return output;
        }
    }
}
