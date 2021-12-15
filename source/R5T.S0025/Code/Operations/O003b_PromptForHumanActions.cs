using System;
using System.Threading.Tasks;

using R5T.D0105;
using R5T.D0110;

using R5T.S0025.Library;


namespace R5T.S0025
{
    public class O003b_PromptForHumanActions
    {
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private ISummaryFilePathProvider SummaryFilePathProvider { get; }


        public O003b_PromptForHumanActions(
            INotepadPlusPlusOperator notepadPlusPlusOperator,
            ISummaryFilePathProvider summaryFilePathProvider)
        {
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
            this.SummaryFilePathProvider = summaryFilePathProvider;
        }

        public async Task Run(HumanActionsRequired humanActionsRequired)
        {
            var summaryFilePath = await this.SummaryFilePathProvider.GetSummaryFilePath();

            await this.NotepadPlusPlusOperator.OpenFilePath(summaryFilePath);

            Console.WriteLine($"Review the summary file (which should be open in Notepad++):\n{summaryFilePath}\n");

            // * New extension method base extensions.
            if (humanActionsRequired.ReviewNewEmbExtensions)
            {
                Console.WriteLine("=> Review the list of new extension method base extensions.\n");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No new extension method base extensions to review. (ok)");
            }
            Console.WriteLine();

            // * Departed extension method base extensions.
            if (humanActionsRequired.ReviewDepartedEmbExtensions)
            {
                Console.WriteLine("=> Review the list of departed extension method base extensions.\n\nNote: departed extension method base extension names will be removed from the lists of ignored and selected extension method base extension names.\n");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No departed extension method base extenions to review. (ok)");
            }
            Console.WriteLine();

            if (humanActionsRequired.ReviewEmbExtensionsUnmappableToEmbs)
            {
                Console.WriteLine("=> Review the list of extension method base extensions unmappable to extension method bases.\n");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No extension method base extensions were unmappable to extension method bases. (good)");
            }
            Console.WriteLine();

            if (humanActionsRequired.ReviewEmbExtensionsUnmappedToEmb)
            {
                Console.WriteLine("=> Review the list of extension method base extensions missing to-extension method base mappings.\n");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No extension method base extensions are missing to-extension method base mappings. (good)");
            }
            Console.WriteLine();

            if (humanActionsRequired.ReviewInvalidToEmbMappings)
            {
                Console.WriteLine("=> Review the list of invalid extension method base extensions-to-extension method base mappings.\n");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No extension method base extension-to-project mappings are invalid. (good)");
            }
            Console.WriteLine();

            if(humanActionsRequired.ReviewNewToEmbMappings)
            {
                Console.WriteLine("=> Review the list of new extension method base extensions-to-extension method base mappings.\n");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No new extension method base extension-to-project mappings. (ok)");
            }
            Console.WriteLine();

            if (humanActionsRequired.ReviewDepartedToEmbMappings)
            {
                Console.WriteLine("=> Review the list of departed extension method base extensions-to-extension method base mappings.\n");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No departed extension method base extension-to-project mappings. (ok)");
            }
            Console.WriteLine();

            if (humanActionsRequired.ReviewEmbExtensionsUnmappedToProject)
            {
                Console.WriteLine("=> Review the list of extension method base extensions missing to-project mappings.\n");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No extension method base extensions are missing to-project mappings. (good)");
            }
            Console.WriteLine();

            if (humanActionsRequired.ReviewInvalidToProjectMappings)
            {
                Console.WriteLine("=> Review the list of invalid extension method base extension-to-project mappings.\n");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No extension method base extension-to-project mappings are invalid. (good)");
            }
            Console.WriteLine();

            if (humanActionsRequired.ReviewNewToProjectMappings)
            {
                Console.WriteLine("=> Review the list of new extension method base extension-to-project mappings.\n");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No new extension method base extension-to-project mappings. (ok)");
            }
            Console.WriteLine();

            if (humanActionsRequired.ReviewDepartedToProjectMappings)
            {
                Console.WriteLine("=> Review the list of departed extension method base extension-to-project mappings.\n");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No departed extension method base extension-to-project mappings. (ok)");
            }
            Console.WriteLine();
        }
    }
}
