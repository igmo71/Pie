﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pie.Data;
using Pie.Data.Models;

namespace Pie.Areas.Config.Pages.ChangeReasonsIn
{
    public class DetailsModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public DetailsModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public ChangeReasonIn ChangeReasonIn { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var changereasonin = await _context.ChangeReasonsIn.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == id);
            if (changereasonin == null)
            {
                return NotFound();
            }
            else
            {
                ChangeReasonIn = changereasonin;
            }
            return Page();
        }
    }
}
