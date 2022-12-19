using System.ComponentModel.DataAnnotations;

namespace SuperSuperSimpleHelpDesk.Entities
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public TicketStatus Status { get; set; }
        public string Owner { get; set; }
        public string Place { get; set; }
        public string AssociatedPhone { get; set; }
    }

    public enum TicketStatus
    {
        [Display(Name = "Aberto")]
        Open,
        [Display(Name = "Em Atendimento")]
        InAttendance,
        [Display(Name = "Fechado")]
        Closed
    }
}
