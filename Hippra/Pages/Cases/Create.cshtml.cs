using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hippra.Data;
using Hippra.Models.SQL;

namespace Hippra.Pages.Cases
{
    public class CreateModel : PageModel
    {
        private readonly Hippra.Data.ApplicationDbContext _context;

        public CreateModel(Hippra.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Case Case { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            //_context.Cases.Add(Case);
            //await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
