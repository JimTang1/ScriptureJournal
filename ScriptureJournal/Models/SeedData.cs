using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ScriptureJournal.Data;
using System;
using System.Linq;

namespace ScriptureJournal.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ScriptureJournalContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ScriptureJournalContext>>()))
            {
                // Look for any movies.
                if (context.Scripture.Any())
                {
                    return;   // DB has been seeded
                }



                context.Scripture.AddRange(
                    new Scripture
                    {
                        Canon = "When Harry Met Sally",
                        Chapter = 11,
                        Verses = "Romantic Comedy",
                        Notes = "111",
                        CreateDate = DateTime.Parse("1989-2-12"),

                    },
                    new Scripture
                    {
                        Canon = "When",
                        Chapter = 22,
                        Verses = "Romantic Comedy",
                        Notes = "222",
                        CreateDate = DateTime.Parse("1989-2-12"),

                    }
                );
                context.SaveChanges();
            }
        }
    }
}
