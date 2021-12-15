using System;


namespace R5T.S0025.Library
{
    public static class HumanActionsRequiredExtensions
    {
        public static bool Any(this HumanActionsRequired humanActionsRequired)
        {
            var output = false
                || humanActionsRequired.ReviewDepartedEmbExtensions
                || humanActionsRequired.ReviewDepartedToEmbMappings
                || humanActionsRequired.ReviewDepartedToProjectMappings
                || humanActionsRequired.ReviewEmbExtensionsUnmappableToEmbs
                || humanActionsRequired.ReviewEmbExtensionsUnmappedToEmb
                || humanActionsRequired.ReviewEmbExtensionsUnmappedToProject
                || humanActionsRequired.ReviewInvalidToEmbMappings
                || humanActionsRequired.ReviewInvalidToProjectMappings
                || humanActionsRequired.ReviewNewEmbExtensions
                || humanActionsRequired.ReviewNewToEmbMappings
                || humanActionsRequired.ReviewNewToProjectMappings
                ;

            return output;
        }

        public static bool AnyMandatory(this HumanActionsRequired humanActionsRequired)
        {
            var output = false
                || humanActionsRequired.ReviewEmbExtensionsUnmappableToEmbs
                || humanActionsRequired.ReviewEmbExtensionsUnmappedToEmb
                || humanActionsRequired.ReviewEmbExtensionsUnmappedToProject
                ;

            return output;
        }

        public static void UnsetNonMandatory(this HumanActionsRequired humanActionsRequired)
        {
            humanActionsRequired.ReviewDepartedEmbExtensions = false;
            humanActionsRequired.ReviewDepartedToEmbMappings = false;
            humanActionsRequired.ReviewDepartedToProjectMappings = false;
            humanActionsRequired.ReviewInvalidToEmbMappings = false;
            humanActionsRequired.ReviewInvalidToProjectMappings = false;
            humanActionsRequired.ReviewNewEmbExtensions = false;
            humanActionsRequired.ReviewNewToEmbMappings = false;
            humanActionsRequired.ReviewNewToProjectMappings = false;
        }
    }
}
