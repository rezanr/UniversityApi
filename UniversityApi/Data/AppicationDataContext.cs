using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityApi.Models;

namespace UniversityApi.Data
{
    public class AppicationDataContext : DbContext
    {
       
        public AppicationDataContext(DbContextOptions<AppicationDataContext> options)
            : base(options)
        {

        }
        
        public DbSet<Unversity> unversities { get; set; }
    }
}
