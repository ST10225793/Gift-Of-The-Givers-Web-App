using System.Threading.Tasks;
using Gift_Of_The_Givers_Web_App.Data;
using Gift_Of_The_Givers_Web_App.Models;

namespace Gift_Of_The_Givers_Web_App.Services

{
    public class IncidentReportService : IIncidentReportService
    {
        private readonly GiftOfTheGiversContext _context;

        public IncidentReportService(GiftOfTheGiversContext context)
        {
            _context = context;
        }

        public async Task AddIncidentReportAsync(IncidentReport incidentReport)
        {
            _context.IncidentReport.Add(incidentReport); 
            await _context.SaveChangesAsync();
        }
    }
}

