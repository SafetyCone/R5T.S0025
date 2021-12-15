using System;
using System.Threading.Tasks;

using R5T.S0025.Library;


namespace R5T.S0025
{
    public class O003a_PerformRequiredHumanActions
    {
        private O003b_PromptForHumanActions O003B_PromptForHumanActions { get; }


        public O003a_PerformRequiredHumanActions(
            O003b_PromptForHumanActions o003B_PromptForHumanActions)
        {
            this.O003B_PromptForHumanActions = o003B_PromptForHumanActions;
        }

        public async Task Run(AnalysisOutputData analysisData)
        {
            var humanActionsRequired = new HumanActionsRequired();

            Instances.Operation.SetRequiredHumanActions(
                analysisData,
                humanActionsRequired);

            var anyHumanActionsRequired = humanActionsRequired.Any();
            if (anyHumanActionsRequired)
            {
                Console.WriteLine("Human actions are required before updating the extension method base extensions repository.");

                // Prompt for human actions.
                await this.O003B_PromptForHumanActions.Run(humanActionsRequired);

                while (true)
                {
                    // Reload input data.

                    // Recalculate analysis data.

                    Instances.Operation.SetRequiredHumanActions(
                        analysisData,
                        humanActionsRequired);

                    // Only mandatory human actions prevent progress.
                    var anyMandatoryHumanActionsRequired = humanActionsRequired.AnyMandatory();
                    if (!anyMandatoryHumanActionsRequired)
                    {
                        break;
                    }

                    Console.WriteLine("MANDATORY human actions are required before updating the extension method base extensions repository.\n");

                    // Prompt for mandatory human actions only.
                    humanActionsRequired.UnsetNonMandatory();

                    await this.O003B_PromptForHumanActions.Run(humanActionsRequired);
                }
            }
            else
            {
                Console.WriteLine("No human actions are required before updating the extension method base repository.\n");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
        }
    }
}
