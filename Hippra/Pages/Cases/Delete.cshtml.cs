using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hippra.Data;
using Hippra.Models.SQL;

namespace Hippra.Pages.Cases
{
    public class DeleteModel : PageModel
    {
        private readonly Hippra.Data.ApplicationDbContext _context;

        public DeleteModel(Hippra.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Case Case { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Case = await _context.Cases.FirstOrDefaultAsync(m => m.ID == id);

            if (Case == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //Case = await _context.Cases.FindAsync(id);

            //if (Case != null)
            //{
            //    _context.Cases.Remove(Case);
            //    await _context.SaveChangesAsync();
            //}

            return RedirectToPage("./Index");
        }
    }
}
