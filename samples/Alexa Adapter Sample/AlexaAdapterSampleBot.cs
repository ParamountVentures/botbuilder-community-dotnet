using System.Threading;
using System.Threading.Tasks;
using Bot.Builder.Community.Adapters.Alexa;
using Bot.Builder.Community.Adapters.Alexa.Directives.Dialogs;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;

namespace AlexaAdapter_Sample
{
    public class AlexaAdapterSampleBot : IBot
    {
        private readonly ILogger logger;

        public AlexaAdapterSampleBot(ILoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                throw new System.ArgumentNullException(nameof(loggerFactory));
            }

            logger = loggerFactory.CreateLogger<AlexaAdapterSampleBot>();
            logger.LogTrace("Turn start.");
        }

        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            switch (turnContext.Activity.Type)
            {
                case ActivityTypes.Message:
                    if (turnContext.Activity.Text == "AMAZON.CancelIntent")
                    {
                        await turnContext.SendActivityAsync("You asked to cancel!");
                    }
                    else if (turnContext.Activity.Text == "one")
                    {
                        RunDialogDelegateDirectiveSample(turnContext);
                    }
                    else if (turnContext.Activity.Text == "two")
                    {
                        await RunDialogElicitSlotDirectiveSample(turnContext);
                    }
                    else if (turnContext.Activity.Text == "three")
                    {
                        await RunDialogConfirmSlotDirectiveSample(turnContext);
                    }
                    else if (turnContext.Activity.Text == "four")
                    {
                        await RunDialogConfirmIntentDirectiveSample(turnContext);
                    }
                    else
                    {
                        await turnContext.SendActivityAsync($"You said '{turnContext.Activity.Text}'\n");

                        turnContext.AlexaSetCard(new AlexaCard()
                        {
                            Type = AlexaCardType.Simple,
                            Title = "Alexa Card Sample",
                            Content = $"You said '{turnContext.Activity.Text}'\n",
                        });
                    }

                    break;
                case AlexaRequestTypes.LaunchRequest:
                    var responseMessage = $"You launched the Alexa Bot Sample!";
                    await turnContext.SendActivityAsync(responseMessage);
                    break;
            }
        }

        /// <summary>
        /// A sample using the Delegate directive. Sends Alexa a command to handle the next turn in the dialog with the user.
        /// 
        /// </summary>
        /// <param name="turnContext"></param>
        /// <returns></returns>
        public void RunDialogDelegateDirectiveSample(ITurnContext turnContext)
        {
            //Sends Alexa a command to handle the next turn in the dialog with the user.
            DialogDelegateDirective directive = new DialogDelegateDirective("TEST_NextBinIntent", AlexaConfirmationState.NONE);

            // demonstrate elicit request that asks for a particular slot value
            //DialogElicitSlotDirective directive = new DialogElicitSlotDirective("GetUserIntent", "phrase");

            // add it to the response
            turnContext.AlexaResponseDirectives().Add(directive);

            // We don't need to provide a prompt. It will use the Dialog prompt and then the one beloe if it is sent.
            //await turnContext.SendActivityAsync($"Please say a value for postcode.");
        }

        /// <summary>
        /// Sends Alexa a command to ask the user for the value of a specific slot.
        /// Typically used when you have multiple slots in an intent and want to ask for a specific one
        /// to be filled.
        /// https://developer.amazon.com/docs/custom-skills/dialog-interface-reference.html#elicitslot
        /// </summary>
        /// <param name="turnContext"></param>
        /// <returns></returns>
        public async Task RunDialogElicitSlotDirectiveSample(ITurnContext turnContext)
        {
            // demonstrate elicit request that asks for a particular slot value
            DialogElicitSlotDirective directive = new DialogElicitSlotDirective("TEST_NextBinIntent", "test_postcode");

            // add it to the response
            turnContext.AlexaResponseDirectives().Add(directive);

            // we MUST provide a prompt in elicit responses - the prompt on the Amazon dialog is not used
            await turnContext.SendActivityAsync($"Please say a value for postcode.");
        }

        /// <summary>
        /// Demonstrates slot confirmation.
        /// https://developer.amazon.com/docs/custom-skills/dialog-interface-reference.html#confirmslot
        /// Note that after Alexa has asked for confirmation, you will get a response with the slot
        /// confirmation status set and NOT the built in AMAZON.YesIntent.
        /// </summary>
        /// <param name="turnContext"></param>
        /// <returns></returns>
        public async Task RunDialogConfirmSlotDirectiveSample(ITurnContext turnContext)
        {
            // demonstrate confirmation request that asks for confirmation of a particular slot value
            DialogConfirmSlotDirective directive = new DialogConfirmSlotDirective("TEST_NextBinIntent", "test_postcode");

            // add it to the response
            turnContext.AlexaResponseDirectives().Add(directive);

            // we MUST provide a prompt in confirm responses - the prompt on the Amazon dialog is not used
            await turnContext.SendActivityAsync($"Did you say {turnContext.Activity.Text} ?");
        }

        /// <summary>
        /// Demonstrates Intent confirmation. This confirms the entire intent - useful when you have
        /// multiple slots that have been filled and you want to confirm all of them.
        /// https://developer.amazon.com/docs/custom-skills/dialog-interface-reference.html#confirmintent
        /// Note that after Alexa has asked for confirmation, you will get a response with the intent
        /// confirmation status set and NOT the built in AMAZON.YesIntent.
        /// </summary>
        /// <param name="turnContext"></param>
        /// <returns></returns>
        public async Task RunDialogConfirmIntentDirectiveSample(ITurnContext turnContext)
        {
            // demonstrate confirmation request that asks for confirmation of a particular slot value
            DialogConfirmIntentDirective directive = new DialogConfirmIntentDirective("TEST_NextBinIntent", AlexaConfirmationState.NONE);

            // add it to the response
            turnContext.AlexaResponseDirectives().Add(directive);

            // we MUST provide a prompt in confirm responses - the prompt on the Amazon dialog is not used
            await turnContext.SendActivityAsync($"Finally, did you say {turnContext.Activity.Text} ?");
        }
    }
}
