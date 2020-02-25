using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hippra.Data;
using Hippra.Models.SQL;

namespace Hippra.Pages.Comments
{
    public class DeleteModel : PageModel
    {
        private readonly Hippra.Data.ApplicationDbContext _context;

        public DeleteModel(Hippra.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CaseComment CaseComment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CaseComment = await _context.CaseComments
                .Include(c => c.Case).FirstOrDefaultAsync(m => m.ID == id);

            if (CaseComment == null)
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

            //CaseComment = await _context.CaseComments.FindAsync(id);

            //if (CaseComment != null)
            //{
            //    _context.CaseComments.Remove(CaseComment);
            //    await _context.SaveChangesAsync();
            //}

            return RedirectToPage("./Index");
        }
    }
}
