using System;
using System.Collections.Generic;

using R5T.T0097;
using R5T.T0100;
using R5T.T0102;


namespace R5T.S0025.Library
{
    public class AnalysisData
    {
        public IDictionary<Guid, Project> ProjectsByIdentity { get; set; }
        public IDictionary<Guid, ExtensionMethodBase> ExtensionMethodBasesByIdentity { get; set; }
        public IDictionary<Guid, ExtensionMethodBaseExtension> CurrentExtensionMethodBaseExtensionsByIdentity { get; set; }
        public IDictionary<Guid, ExtensionMethodBaseExtension> RepositoryExtensionMethodBaseExtensionsByIdentity { get; set; }
        public IList<ExtensionMethodBaseExtension> NewExtensionMethodBaseExtensions { get; set; }
        public IList<ExtensionMethodBaseExtension> DepartedExtensionMethodBaseExtensions { get; set; }
        public IList<ExtensionMethodBaseExtensionToProjectMapping> NewToProjectMappings { get; set; }
        public IList<ExtensionMethodBaseExtensionToProjectMapping> DepartedToProjectMappings { get; set; }
        public IList<ExtensionMethodBaseExtensionToExtensionMethodBaseMapping> NewToExtensionMethodBaseMappings { get; set; }
        public IList<ExtensionMethodBaseExtensionToExtensionMethodBaseMapping> DepartedToExtensionMethodBaseMappings { get; set; }
    }
}
