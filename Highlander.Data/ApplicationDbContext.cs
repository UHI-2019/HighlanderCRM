using System;
using System.Collections.Generic;
using System.Text;
using Highlander.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Highlander.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, 
        IdentityUserClaim<int>, ApplicationUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Artefact> Artefacts { get; set; }
        public DbSet<BusinessSector> BusinessSectors { get; set; }
        public DbSet<CommercialContact> CommercialContacts { get; set; }
        public DbSet<Decoration> Decorations { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<DonorArtefact> DonorArtefacts { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }
        public DbSet<Expertise> Expertises { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberArchive> MemberArchives { get; set; }
        public DbSet<Regiment> Regiments { get; set; }
        public DbSet<Regimental> Regimentals { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<UserCommercialContact> UserCommercialContacts { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        
        
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne()
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne()
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne()
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<ApplicationRole>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });

        }
    }
}
