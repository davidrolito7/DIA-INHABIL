using ApiDiaInhabil.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiDiaInhabil.Data;

public class ApiDiaInhabilContext(DbContextOptions<ApiDiaInhabilContext> options)
	: DbContext(options)
{
	public DbSet<Area> Areas => Set<Area>();
	public DbSet<DiaInhabil> DiasInhabiles => Set<DiaInhabil>();
	public DbSet<Sede> Sedes => Set<Sede>();
	public DbSet<DiaInhabilSede> DiasInhabilesSedes => Set<DiaInhabilSede>();
	public DbSet<DiaInhabilArea> DiasInhabilesAreas => Set<DiaInhabilArea>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Area>(entity =>
		{
			entity.ToTable("ADM_AREA", tableBuilder => tableBuilder.ExcludeFromMigrations());
			entity.HasOne(x => x.Sede)
				.WithMany(x => x.Areas)
				.HasForeignKey(x => x.IdSede)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.Restrict);
		});

		modelBuilder.Entity<DiaInhabil>(entity =>
		{
			entity.ToTable("DI_DiasInhabiles");
			entity.Property(x => x.Nombre).HasMaxLength(150).IsRequired();
			entity.Property(x => x.Descripcion).HasMaxLength(500);
		});

		modelBuilder.Entity<Sede>(entity =>
		{
			entity.ToTable("DI_Sedes");
			entity.Property(x => x.Nombre).HasMaxLength(150).IsRequired();
			entity.Property(x => x.Descripcion).HasMaxLength(500);
		});

		modelBuilder.Entity<DiaInhabilSede>(entity =>
		{
			entity.ToTable("DI_DiasInhabilesSedes");

			entity.HasOne(x => x.DiaInhabil)
				.WithMany(x => x.Sedes)
				.HasForeignKey(x => x.IdDiaInhabil)
				.OnDelete(DeleteBehavior.Restrict);

			entity.HasOne(x => x.Sede)
				.WithMany(x => x.DiasInhabiles)
				.HasForeignKey(x => x.IdSede)
				.OnDelete(DeleteBehavior.Restrict);
		});

		modelBuilder.Entity<DiaInhabilArea>(entity =>
		{
			entity.ToTable("DI_DiaInhabilArea");

			entity.HasOne(x => x.DiaInhabilSede)
				.WithMany(x => x.DiaInhabilAreas)
				.HasForeignKey(x => x.IdDiaInhabilSede)
				.OnDelete(DeleteBehavior.Restrict);

			entity.HasOne(x => x.Area)
				.WithMany()
				.HasForeignKey(x => x.IdArea)
				.OnDelete(DeleteBehavior.Restrict);
		});
	}
}
