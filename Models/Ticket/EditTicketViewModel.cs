namespace SuperSuperSimpleHelpDesk.Models.Ticket
{
    public class EditTicketViewModel
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public TicketStatusViewModel Status { get; set; }
        public string Owner { get; set; }
        public string Place { get; set; }
        public string AssociatedPhone { get; set; }
    }
}
