using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SurveyApi.Models;

namespace SurveyApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<SurveyApi.Models.Category> Category { get; set; } = default!;

        public DbSet<SurveyApi.Models.Photo> Photo { get; set; }

        public DbSet<SurveyApi.Models.Question> Question { get; set; }

        public DbSet<SurveyApi.Models.QuestionOption> QuestionOption { get; set; }

        public DbSet<SurveyApi.Models.Role> Role { get; set; }

        public DbSet<SurveyApi.Models.Survey> Survey { get; set; }

        public DbSet<SurveyApi.Models.User> User { get; set; }

        public DbSet<SurveyApi.Models.UserAnswer> UserAnswer { get; set; }
    }
}
