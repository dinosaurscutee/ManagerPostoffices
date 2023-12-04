using ManagerPostoffices.Data;
using ManagerPostoffices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace ManagerPostoffices.Pages.Delivery
{
    public class DeliveryTimeModel : PageModel
    {
        private readonly ManagerPostofficesDbContext _context;

        public DeliveryTimeModel(ManagerPostofficesDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Package Package { get; set; }

        public async Task<IActionResult> OnGetAsync(int packageId)
        {
            Package = await _context.Packages
                .Include(p => p.Recipient)
                .Include(p => p.PackageDeliveryHistory)  // Include PackageDeliveryHistory
                    .ThenInclude(pd => pd.DeliveryStatus) // Include DeliveryStatus within PackageDeliveryHistory
                .FirstOrDefaultAsync(p => p.PackageId == packageId);

            if (Package == null)
            {
                return NotFound();
            }

            return Page();
        }


    }
}
