using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreWebApi.Models;

public partial class DeviceManagementContext : DbContext
{
    public DeviceManagementContext()
    {
    }

    public DeviceManagementContext(DbContextOptions<DeviceManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Device> Devices { get; set; }

    public virtual DbSet<WeatherForecast> WeatherForecasts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=PSL-5CD15210NT;Database=DeviceManagement;TrustServerCertificate=True;User Id=sa;Password=Persistent@123#456");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.DeviceId).HasName("PK__Device__49E123117CAB95E0");

            entity.ToTable("Device");

            entity.Property(e => e.DeviceId).ValueGeneratedNever();
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Stage)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<WeatherForecast>(entity =>
        {
            entity.HasKey(e => e.TemperatureC).HasName("PK__WeatherF__27F2D9A818D9C140");

            entity.ToTable("WeatherForecast");

            entity.Property(e => e.TemperatureC).ValueGeneratedNever();
            entity.Property(e => e.Summary)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
