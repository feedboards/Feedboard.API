using System;
using System.Collections.Generic;
using Feedboard.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Feedboard.DAL.Context;

public partial class FeedboardDbContext : DbContext
{
    public FeedboardDbContext(DbContextOptions<FeedboardDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AzureAccount> AzureAccounts { get; set; }

    public virtual DbSet<GitHubAccount> GitHubAccounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AzureAccount>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<GitHubAccount>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_GitHubAccounts_1");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
