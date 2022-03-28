using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace bliss.recruitment.api.Data
{
    public class QuestionsContext : DbContext
    {
        public QuestionsContext(DbContextOptions<QuestionsContext> options) : base(options)
        {
        }

        public DbSet<Model.Data.Question> Questions { get; set; }
        public DbSet<Model.Data.Choice> Choices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model.Data.Question>().ToTable("T_Questions");
            modelBuilder.Entity<Model.Data.Choice>().ToTable("T_Choices");
        }
    }

    public static class DbInitializer
    {
        public static void Initialize(QuestionsContext context)
        {
            context.Database.EnsureCreated();

            // Look for any Questions.
            if (context.Questions.Any())
            {
                return;   // DB has been seeded
            }

            var questions = new Model.Data.Question[]
            {
                new Model.Data.Question{Name = "Favourite programming language?", Image_Url = "https://dummyimage.com/600x400/000/fff.png&text=question+1+image+(600x400)", Thumb_Url = "https://dummyimage.com/120x120/000/fff.png&text=question+1+image+(120x120)", Published_At = DateTime.Parse("2015-08-05 09:40:51.620")},
                new Model.Data.Question{Name = "Favourite programming language?", Image_Url = "https://dummyimage.com/600x400/000/fff.png&text=question+1+image+(600x400)", Thumb_Url = "https://dummyimage.com/120x120/000/fff.png&text=question+1+image+(120x120)", Published_At = DateTime.Parse("2015-08-05 09:40:51.620")},
                new Model.Data.Question{Name = "Favourite programming language?", Image_Url = "https://dummyimage.com/600x400/000/fff.png&text=question+1+image+(600x400)", Thumb_Url = "https://dummyimage.com/120x120/000/fff.png&text=question+1+image+(120x120)", Published_At = DateTime.Parse("2015-08-05 09:40:51.620")},
                new Model.Data.Question{Name = "Favourite programming language?", Image_Url = "https://dummyimage.com/600x400/000/fff.png&text=question+1+image+(600x400)", Thumb_Url = "https://dummyimage.com/120x120/000/fff.png&text=question+1+image+(120x120)", Published_At = DateTime.Parse("2015-08-05 09:40:51.620")},
                new Model.Data.Question{Name = "Favourite programming language?", Image_Url = "https://dummyimage.com/600x400/000/fff.png&text=question+1+image+(600x400)", Thumb_Url = "https://dummyimage.com/120x120/000/fff.png&text=question+1+image+(120x120)", Published_At = DateTime.Parse("2015-08-05 09:40:51.620")},
                new Model.Data.Question{Name = "Favourite programming language?", Image_Url = "https://dummyimage.com/600x400/000/fff.png&text=question+1+image+(600x400)", Thumb_Url = "https://dummyimage.com/120x120/000/fff.png&text=question+1+image+(120x120)", Published_At = DateTime.Parse("2015-08-05 09:40:51.620")},
                new Model.Data.Question{Name = "Favourite programming language?", Image_Url = "https://dummyimage.com/600x400/000/fff.png&text=question+1+image+(600x400)", Thumb_Url = "https://dummyimage.com/120x120/000/fff.png&text=question+1+image+(120x120)", Published_At = DateTime.Parse("2015-08-05 09:40:51.620")},
                new Model.Data.Question{Name = "Favourite programming language?", Image_Url = "https://dummyimage.com/600x400/000/fff.png&text=question+1+image+(600x400)", Thumb_Url = "https://dummyimage.com/120x120/000/fff.png&text=question+1+image+(120x120)", Published_At = DateTime.Parse("2015-08-05 09:40:51.620")},
                new Model.Data.Question{Name = "Favourite programming language?", Image_Url = "https://dummyimage.com/600x400/000/fff.png&text=question+1+image+(600x400)", Thumb_Url = "https://dummyimage.com/120x120/000/fff.png&text=question+1+image+(120x120)", Published_At = DateTime.Parse("2015-08-05 09:40:51.620")},
                new Model.Data.Question{Name = "Favourite programming language?", Image_Url = "https://dummyimage.com/600x400/000/fff.png&text=question+1+image+(600x400)", Thumb_Url = "https://dummyimage.com/120x120/000/fff.png&text=question+1+image+(120x120)", Published_At = DateTime.Parse("2015-08-05 09:40:51.620")},
            };

            foreach (var q in questions)
            {
                context.Questions.Add(q);
            }

            context.SaveChanges();

            for (int i = 1; i <= 10; i++)
            {
                var choices = new Model.Data.Choice[]
                {
                    new Model.Data.Choice{Name = "Swift", Votes = 2048, QuestionId = i},
                    new Model.Data.Choice{Name = "Python", Votes = 1024, QuestionId = i},
                    new Model.Data.Choice{Name = "Objective-C", Votes = 512, QuestionId = i},
                    new Model.Data.Choice{Name = "Ruby", Votes = 256, QuestionId = i},
                };

                foreach (var c in choices)
                {
                    context.Choices.Add(c);
                }

                context.SaveChanges();
            }
        }
    }
}
