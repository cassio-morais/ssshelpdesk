using SuperSuperSimpleHelpDesk.Entities;

namespace SuperSuperSimpleHelpDesk.Data
{
    public static class TempSeed
    {
        public static List<Ticket> CreateTickets()
        {
            var tickets = new List<Ticket>()
            {
                new Ticket {
                    Title = "Problema na impressa",
                    Message = "Essa caralha é uma caralha",
                    Owner = "Fulano de tal",
                    AssociatedPhone = "(31) 99442-9999",
                    CreatedAt = DateTime.Now,
                    Place = "Agência do seu Zé",
                    Status = TicketStatus.Open,
                },
                new Ticket {
                    Title = "Teclado afundando",
                    Message = "Israel isso aqui tá estranho... me liga.",
                    Owner = "Fernando Sossego",
                    AssociatedPhone = "(31) 99442-9999",
                    CreatedAt = DateTime.Now.AddDays(-1),
                    Place = "Meios e Metas - Pampulha",
                    Status = TicketStatus.InAttendance,
                    UpdatedAt = DateTime.Now.AddHours(-1),
                },
            };

            return tickets;
        }
    }
}
