using LogService.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogService.Core.Database
{
    public class IBaseDbContext : DbContext
    {
        public virtual DbSet<LogEntity> Log { get; set; }
    }
}
