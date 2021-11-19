using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApplication.Models.AppDBContext
{
    public class AppDBContext : IdentityDbContext
    {
        private readonly DbContextOptions _options;
        public AppDBContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }
        public AppDBContext(string deger) 
        {
           
        }
        public virtual DbSet<Survey> Survey { get; set; }
        public virtual DbSet<Questions> Question { get; set; }
        public virtual DbSet<Options> Option { get; set; }
        public virtual DbSet<Results> Result { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
