using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ScriptureJournal.Data;
using ScriptureJournal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ScriptureJournal.Pages.Scriptures
{
    public class IndexModel : PageModel
    {
        private readonly ScriptureJournal.Data.ScriptureJournalContext _context;
        private List<Scripture> Book;

        public IndexModel(ScriptureJournal.Data.ScriptureJournalContext context)
        {
            _context = context;
        }

        public IList<Scripture> Scripture { get;set; }
        [BindProperty(SupportsGet = true)]

        public string SearchString { get; set; }
        public SelectList Canon { get; set; }
        [BindProperty(SupportsGet = true)]

        public string SearchNotes { get; set; }
        public SelectList Notes{ get; set; }
        [BindProperty(SupportsGet = true)]

        public string ScriptureCanon { get; set; }

        public string CanonSort { get; set; }
        public string DateSort { get; set; }


        public async Task OnGetAsync(string sortOrder)
        {
            //IQueryable<string> genreQuery = from s in _context.Scripture
            //                                orderby s.Canon
            //                                select s.Canon;

            //var Scriptures = from s in _context.Scripture
            //                 select s;

            CanonSort = String.IsNullOrEmpty(sortOrder) ? "canon" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            IQueryable<Scripture> scriptures = from s in _context.Scripture
                                              select s;

            if (!string.IsNullOrEmpty(SearchString))
            {
                scriptures = scriptures.Where(s => s.Canon.Contains(SearchString));
            }

            if(!string.IsNullOrEmpty(SearchNotes))
            {
                scriptures = scriptures.Where(s => s.Notes.Contains(SearchNotes));
            }

            //Canon = new SelectList(await genreQuery.Distinct().ToListAsync());
            //Scripture = await Scriptures.ToListAsync();


            switch (sortOrder)
            {
                case "canon":
                    scriptures = scriptures.OrderByDescending(s => s.Canon);
                    break;
                case "Date":
                    scriptures = scriptures.OrderBy(s => s.CreateDate);
                    break;
                case "date_desc":
                    scriptures = scriptures.OrderByDescending(s => s.CreateDate);
                    break;
                default:
                    scriptures = scriptures.OrderBy(s => s.Canon);
                    break;
            }

            Scripture = await scriptures.AsNoTracking().ToListAsync();

        }
    }
}
