using System;
namespace Bot.Builder.Community.Adapters.Alexa.Directives.Dialogs
{
    /// <summary>
    /// Sends Alexa a command to confirm the all the information the user has provided for the intent before the skill takes action.
    /// </summary>
    public class DialogConfirmIntentDirective : DialogDirective
    {
        /// <summary>
        /// In cases where we do not need to change the intent, slot values, or confirmation statuses, we can use this constructor.
        /// </summary>
        public DialogConfirmIntentDirective() { }

        /// <summary>
        /// We can pass in a full intent with new values for the intent, status or slots.
        /// </summary>
        /// <param name="intent"></param>
        public DialogConfirmIntentDirective(AlexaIntent intent) : base(intent) { }

        public DialogConfirmIntentDirective(string intent, AlexaConfirmationState confirmationStatus)
            : base(intent, confirmationStatus) { }

        public string Type => "Dialog.ConfirmIntent";
    }
}
