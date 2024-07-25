﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BulkyWeb.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) 
        {
            
        }
    }
}
