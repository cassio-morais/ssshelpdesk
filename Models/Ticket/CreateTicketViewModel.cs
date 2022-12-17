namespace SuperSuperSimpleHelpDesk.Models.Ticket
{
    public class CreateTicketViewModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Owner { get; set; }
        public string Place { get; set; }
        public string AssociatedPhone { get; set; }

    }
}
