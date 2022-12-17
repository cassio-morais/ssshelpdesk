using System.ComponentModel.DataAnnotations;

namespace SuperSuperSimpleHelpDesk.Models.Ticket
{
    public enum TicketStatusViewModel
    {
        [Display(Name = "Aberto")]
        Open,
        [Display(Name = "Em Atendimento")]
        InAttendance,
        [Display(Name = "Fechado")]
        Closed
    }
}
