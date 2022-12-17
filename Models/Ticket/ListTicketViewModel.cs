namespace SuperSuperSimpleHelpDesk.Models.Ticket
{
    public class ListTicketViewModel
    {
        public int Id { get; set; }
        public string CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string Owner { get; set; }
        public string Place { get; set; }
        public string AssociatedPhone { get; set; }
    }
}
