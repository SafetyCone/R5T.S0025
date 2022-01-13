using System;
using System.Linq;


namespace R5T.S0025.Library
{
    public static class AnalysisDataExtensions
    {
        public static HumanActionsRequired01 DetermineRequredHumanActions(this AnalysisData analysisData)
        {
            var output = new HumanActionsRequired01
            {
                ReviewNewExtensionMethodBaseExtensions = analysisData.NewExtensionMethodBaseExtensions.Any(),
                ReviewDepartedExtensionMethodBaseExtensions = analysisData.DepartedExtensionMethodBaseExtensions.Any(),
                ReviewNewToProjectMappings = analysisData.NewToProjectMappings.Any(),
                ReviewDepartedToProjectMappings = analysisData.DepartedToProjectMappings.Any(),
                ReviewNewToExtensionMethodBaseMappings = analysisData.NewToExtensionMethodBaseMappings.Any(),
                ReviewDepartedToExtensionMethodBaseMappings = analysisData.DepartedToExtensionMethodBaseMappings.Any()
            };

            return output;
        }
    }
}
