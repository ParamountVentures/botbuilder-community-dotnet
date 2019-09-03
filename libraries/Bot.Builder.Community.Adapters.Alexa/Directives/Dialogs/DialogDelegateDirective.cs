using System;
namespace Bot.Builder.Community.Adapters.Alexa.Directives.Dialogs
{
    /// <summary>
    /// Sends Alexa a command to handle the next turn in the dialog with the user.
    /// More information here https://developer.amazon.com/docs/custom-skills/dialog-interface-reference.html#delegate
    /// </summary>
    public class DialogDelegateDirective : DialogDirective
    {
        /// <summary>
        /// In cases where we do not need to change the intent, slot values, or confirmation statuses, we can use this constructor.
        /// </summary>
        public DialogDelegateDirective() { }

        /// <summary>
        /// We can pass in a full intent with new values for the intent, status or slots.
        /// </summary>
        /// <param name="intent"></param>
        public DialogDelegateDirective(AlexaIntent intent) : base(intent) { }

        public DialogDelegateDirective(string intent, AlexaConfirmationState confirmationStatus)
            : base(intent, confirmationStatus) { }

        public string Type => "Dialog.Delegate";
    }
}
