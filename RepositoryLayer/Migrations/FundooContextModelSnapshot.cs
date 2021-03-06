// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RepositoryLayer.Context;

namespace RepositoryLayer.Migrations
{
    [DbContext(typeof(FundooContext))]
    partial class FundooContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CommonLayer.CollaboratorModel", b =>
                {
                    b.Property<int>("CollaborationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("NoteId")
                        .HasColumnType("int");

                    b.Property<string>("SharedEmail")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("CollaborationId");

                    b.HasIndex("NoteId");

                    b.ToTable("Collaborators");
                });

            modelBuilder.Entity("CommonLayer.LabelNameModel", b =>
                {
                    b.Property<int>("LabelNameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("LabelName")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LabelNameId");

                    b.HasIndex("UserId");

                    b.ToTable("LabelNames");
                });

            modelBuilder.Entity("CommonLayer.LabelNoteModel", b =>
                {
                    b.Property<int>("LabelNoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("LabelNameId")
                        .HasColumnType("int");

                    b.Property<int>("NoteId")
                        .HasColumnType("int");

                    b.HasKey("LabelNoteId");

                    b.HasIndex("LabelNameId");

                    b.HasIndex("NoteId");

                    b.ToTable("LabelNotes");
                });

            modelBuilder.Entity("CommonLayer.NotesModel", b =>
                {
                    b.Property<int>("NoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AddedImage")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("Archive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Body")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Color")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("Pin")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Reminder")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Title")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("Trash")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("NoteId");

                    b.HasIndex("UserId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("CommonLayer.RegisterModel", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CommonLayer.CollaboratorModel", b =>
                {
                    b.HasOne("CommonLayer.NotesModel", "Notes")
                        .WithMany()
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CommonLayer.LabelNameModel", b =>
                {
                    b.HasOne("CommonLayer.RegisterModel", "Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CommonLayer.LabelNoteModel", b =>
                {
                    b.HasOne("CommonLayer.LabelNameModel", "LabelNames")
                        .WithMany()
                        .HasForeignKey("LabelNameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CommonLayer.NotesModel", "Notes")
                        .WithMany()
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CommonLayer.NotesModel", b =>
                {
                    b.HasOne("CommonLayer.RegisterModel", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
