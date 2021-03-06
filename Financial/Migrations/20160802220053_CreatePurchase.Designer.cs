using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Financial.Models.Context;

namespace Financial.Migrations
{
    [DbContext(typeof(FinancialContext))]
    [Migration("20160802220053_CreatePurchase")]
    partial class CreatePurchase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Financial.Models.Entities.CreditCard", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("International");

                    b.Property<Guid>("NetworkId");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 40);

                    b.Property<int>("OwnerId");

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "CreditCards");
                });

            modelBuilder.Entity("Financial.Models.Entities.CreditCardNetwork", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DisplayName")
                        .IsRequired();

                    b.Property<string>("ImageName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "CreditCardNetworks");
                });

            modelBuilder.Entity("Financial.Models.Entities.Purchase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CreditCardId");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.Property<int>("InstalmentSplit");

                    b.Property<int>("OwnerId");

                    b.Property<DateTime>("PurchasedOn");

                    b.Property<decimal>("TotalAmount");

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "Purchases");
                });

            modelBuilder.Entity("Financial.Models.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "Users");
                });

            modelBuilder.Entity("Financial.Models.Entities.CreditCard", b =>
                {
                    b.HasOne("Financial.Models.Entities.CreditCardNetwork")
                        .WithMany()
                        .HasForeignKey("NetworkId");

                    b.HasOne("Financial.Models.Entities.User")
                        .WithMany()
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("Financial.Models.Entities.Purchase", b =>
                {
                    b.HasOne("Financial.Models.Entities.CreditCard")
                        .WithMany()
                        .HasForeignKey("CreditCardId");

                    b.HasOne("Financial.Models.Entities.User")
                        .WithMany()
                        .HasForeignKey("OwnerId");
                });
        }
    }
}
