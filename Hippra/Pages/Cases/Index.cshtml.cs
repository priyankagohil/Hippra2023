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
    public class IndexModel : PageModel
    {
        private readonly Hippra.Data.ApplicationDbContext _context;

        public IndexModel(Hippra.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Case> Case { get;set; }

        public async Task OnGetAsync()
        {
            Case = await _context.Cases.ToListAsync();
        }
    }
}
