using System;
namespace Bot.Builder.Community.Adapters.Alexa.Directives.Dialogs
{
    /// <summary>
    /// Sends Alexa a command to ask the user for the value of a specific slot.
    /// </summary>
    public class DialogElicitSlotDirective : DialogDirective
    {
        public DialogElicitSlotDirective(string slotToElicit)
        {
            SlotToElicit = slotToElicit;
        }

        public DialogElicitSlotDirective(string slotToElicit, AlexaIntent intent) : base(intent)
        {
            SlotToElicit = slotToElicit;
        }

        public DialogElicitSlotDirective(string slotToElicit, string intent)
            : base(intent) {

            SlotToElicit = slotToElicit;
        }

        public string Type => "Dialog.ElicitSlot";

        public string SlotToElicit { get; set; }
    }
}
