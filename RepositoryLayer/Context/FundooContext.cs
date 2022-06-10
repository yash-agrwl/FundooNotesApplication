﻿using CommonLayer;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Context
{
    public class FundooContext : DbContext
    {
        public FundooContext(DbContextOptions<FundooContext> options) : base(options)
        {

        }

        public DbSet<RegisterModel> Users { get; set; }
    }
}