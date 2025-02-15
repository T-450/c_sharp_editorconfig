﻿namespace Catalog.API.Infrastructure.Migrations
{
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Metadata;

    [DbContext(typeof(CatalogContext))]
    internal class CatalogContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("Relational:Sequence:.catalog_brand_hilo", "'catalog_brand_hilo', '', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("Relational:Sequence:.catalog_hilo", "'catalog_hilo', '', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("Relational:Sequence:.catalog_type_hilo", "'catalog_type_hilo', '', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.eShopOnContainers.Services.Catalog.API.Model.CatalogBrand", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:HiLoSequenceName", "catalog_brand_hilo")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                b.Property<string>("Brand")
                    .IsRequired()
                    .HasMaxLength(100);

                b.HasKey("Id");

                b.ToTable("CatalogBrand");
            });

            modelBuilder.Entity("Microsoft.eShopOnContainers.Services.Catalog.API.Model.CatalogItem", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:HiLoSequenceName", "catalog_hilo")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                b.Property<int>("AvailableStock");

                b.Property<int>("CatalogBrandId");

                b.Property<int>("CatalogTypeId");

                b.Property<string>("Description");

                b.Property<int>("MaxStockThreshold");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(50);

                b.Property<bool>("OnReorder");

                b.Property<string>("PictureFileName");

                b.Property<decimal>("Price");

                b.Property<int>("RestockThreshold");

                b.HasKey("Id");

                b.HasIndex("CatalogBrandId");

                b.HasIndex("CatalogTypeId");

                b.ToTable("Catalog");
            });

            modelBuilder.Entity("Microsoft.eShopOnContainers.Services.Catalog.API.Model.CatalogType", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:HiLoSequenceName", "catalog_type_hilo")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                b.Property<string>("Type")
                    .IsRequired()
                    .HasMaxLength(100);

                b.HasKey("Id");

                b.ToTable("CatalogType");
            });

            modelBuilder.Entity("Microsoft.eShopOnContainers.Services.Catalog.API.Model.CatalogItem", b =>
            {
                b.HasOne("Microsoft.eShopOnContainers.Services.Catalog.API.Model.CatalogBrand", "CatalogBrand")
                    .WithMany()
                    .HasForeignKey("CatalogBrandId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("Microsoft.eShopOnContainers.Services.Catalog.API.Model.CatalogType", "CatalogType")
                    .WithMany()
                    .HasForeignKey("CatalogTypeId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
