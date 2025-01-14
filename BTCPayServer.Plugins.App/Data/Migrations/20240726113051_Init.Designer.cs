﻿// <auto-generated />
using System;
using System.Collections.Generic;
using BTCPayServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BTCPayServer.Plugins.App.Data.Migrations
{
    [DbContext(typeof(AppPluginDbContext))]
    [Migration("20240726113051_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("BTCPayServer.Plugins.App")
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BTCPayServer.Plugins.App.Data.Models.AppStorageItemData", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("text");

                    b.Property<long>("Version")
                        .HasColumnType("bigint");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<byte[]>("Value")
                        .HasColumnType("bytea");

                    b.HasKey("Key", "Version", "UserId");

                    b.HasIndex("UserId");

                    b.HasIndex("Key", "UserId")
                        .IsUnique();

                    b.ToTable("AppStorageItems", "BTCPayServer.Plugins.App", t =>
                        {
                            t.HasTrigger("LC_TRIGGER_BEFORE_INSERT_APPSTORAGEITEMDATA");
                        });

                    b.HasAnnotation("LC_TRIGGER_BEFORE_INSERT_APPSTORAGEITEMDATA", "CREATE FUNCTION \"LC_TRIGGER_BEFORE_INSERT_APPSTORAGEITEMDATA\"() RETURNS trigger as $LC_TRIGGER_BEFORE_INSERT_APPSTORAGEITEMDATA$\r\nBEGIN\r\n  DELETE FROM \"AppStorageItems\"\r\n  WHERE NEW.\"UserId\" = \"AppStorageItems\".\"UserId\" AND NEW.\"Key\" = \"AppStorageItems\".\"Key\" AND NEW.\"Version\" > \"AppStorageItems\".\"Version\";\r\nRETURN NEW;\r\nEND;\r\n$LC_TRIGGER_BEFORE_INSERT_APPSTORAGEITEMDATA$ LANGUAGE plpgsql;\r\nCREATE TRIGGER LC_TRIGGER_BEFORE_INSERT_APPSTORAGEITEMDATA BEFORE INSERT\r\nON \"AppStorageItems\"\r\nFOR EACH ROW EXECUTE PROCEDURE \"LC_TRIGGER_BEFORE_INSERT_APPSTORAGEITEMDATA\"();");
                });
        }
    }
}
