using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hippra.Data;
using Hippra.Models.SQL;

namespace Hippra.Pages.Comments
{
    public class EditModel : PageModel
    {
        private readonly Hippra.Data.ApplicationDbContext _context;

        public EditModel(Hippra.Data.ApplicationDbContext context)
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
           ViewData["CaseID"] = new SelectList(_context.Cases, "ID", "ID");
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            //_context.Attach(CaseComment).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!CaseCommentExists(CaseComment.ID))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return RedirectToPage("./Index");
        }

        private bool CaseCommentExists(int id)
        {
            return _context.CaseComments.Any(e => e.ID == id);
        }
    }
}
