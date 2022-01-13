using System;

using R5T.T0034;
using R5T.T0035;
using R5T.T0036;
using R5T.T0039;
using R5T.T0040;
using R5T.T0041;
using R5T.T0045;


namespace R5T.S0025.Library
{
    public static class Instances
    {
        public static IClassGenerator ClassGenerator { get; } = T0045.ClassGenerator.Instance;
        public static ICodeDirectoryOperator CodeDirectoryOperator { get; } = T0045.CodeDirectoryOperator.Instance;
        public static ICompilationUnitGenerator CompilationUnitGenerator { get; } = T0045.CompilationUnitGenerator.Instance;
        public static ICompilationUnitOperator CompilationUnitOperator { get; } = T0045.CompilationUnitOperator.Instance;
        public static IExtensionMethodBaseOperator ExtensionMethodBaseOperator { get; } = T0039.ExtensionMethodBaseOperator.Instance;
        public static IIndentation Indentation { get; } = T0036.Indentation.Instance;
        public static IMethodGenerator MethodGenerator { get; } = T0045.MethodGenerator.Instance;
        public static IMethodNameOperator MethodNameOperator { get; } = T0036.MethodNameOperator.Instance;
        public static INamespacedTypeName NamespacedTypeName { get; } = T0034.NamespacedTypeName.Instance;
        public static INamespaceName NamespaceName { get; } = T0035.NamespaceName.Instance;
        public static IPathOperator PathOperator { get; } = T0041.PathOperator.Instance;
        public static IProjectPathsOperator ProjectPathsOperator { get; } = T0040.ProjectPathsOperator.Instance;
        public static ITypeName TypeName { get; } = T0034.TypeName.Instance;
    }
}
