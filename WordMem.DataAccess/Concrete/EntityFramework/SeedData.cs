
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordMem.Entity;

namespace WordMem.DataAccess.Concrete.EntityFramework
{
    public static class SeedData
    {
        

        public static void EnsurePopulated(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetRequiredService<DBContext>();

            context.Database.Migrate();

            if (!context.Words.Any())
            {

                var categories = new[]
                {
                    new Category(){ CategoryName="Electronics"},
                    new Category(){ CategoryName="Accessories"},
                    new Category(){ CategoryName="Furniture"}
                };

                context.Categories.AddRange(categories);


                var words = new[]
{
                    new Word(){ OwnLang="Bilgisayar",ForeignLang="Computer", SampleSentence="This is a computer", Image="computer.jpg"},
                    new Word(){ OwnLang="Telefon",ForeignLang="Phone", SampleSentence="This is a phone", Image="computer.jpg"},
                    new Word(){ OwnLang="Ekran",ForeignLang="Screen", SampleSentence="This is a screen", Image="computer.jpg"},
                    new Word(){ OwnLang="Klavye",ForeignLang="Keyboard", SampleSentence="This is a keyboard", Image="computer.jpg"}
                };

                context.Words.AddRange(words);

                var wordCategories = new[]
{
                    new WordCategory(){Category= categories[0], Word=words[0]},
                    new WordCategory(){Category= categories[0], Word=words[1]},
                    new WordCategory(){Category= categories[1], Word=words[2]},
                    new WordCategory(){Category= categories[2], Word=words[3]}
                };
                context.AddRange(wordCategories);

                context.Add(new WordCategory() { Category=categories[0],Word=words[2]});

                context.SaveChanges();
            }
        }
    }
}
