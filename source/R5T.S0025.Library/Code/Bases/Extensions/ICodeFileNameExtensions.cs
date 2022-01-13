using System;

using R5T.T0037;

using Instances = R5T.S0025.Library.Instances;


namespace System
{
    public static class ICodeFileNameExtensions
    {
        public static string IExtensionMethodBaseFunctionalityExtensions(this ICodeFileName _)
        {
            var output = _.GetCSharpFileNameForTypeName(
                Instances.TypeName.IExtensionMethodBaseFunctionalityExtensions());

            return output;
        }
    }
}
