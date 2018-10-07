namespace test
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AdministerContext : DbContext
    {
        public AdministerContext()
            : base("name=AdministerContext")
        {
        }

        public virtual DbSet<ВидРемонта> ВидРемонта { get; set; }
        public virtual DbSet<Гарантии> Гарантии { get; set; }
        public virtual DbSet<ЗаводИзготовитель> ЗаводИзготовитель { get; set; }
        public virtual DbSet<ЗаказДеталей> ЗаказДеталей { get; set; }
        public virtual DbSet<Заказы> Заказы { get; set; }
        public virtual DbSet<Запчасти> Запчасти { get; set; }
        public virtual DbSet<Исполнители> Исполнители { get; set; }
        public virtual DbSet<Клиенты> Клиенты { get; set; }
        public virtual DbSet<НаличиеЗапчастей> НаличиеЗапчастей { get; set; }
        public virtual DbSet<СостояниеРемонта> СостояниеРемонта { get; set; }
        public virtual DbSet<КвалификацияМастера> КвалификацияМастера { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ВидРемонта>()
                 .Property(e => e.ТипРемонта)
                 .IsUnicode(false);

            modelBuilder.Entity<ВидРемонта>()
                .HasMany(e => e.Заказы)
                .WithRequired(e => e.ВидРемонта)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Гарантии>()
                .Property(e => e.Гарантия)
                .IsUnicode(false);

            modelBuilder.Entity<Гарантии>()
                .HasMany(e => e.Заказы)
                .WithRequired(e => e.Гарантии)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ЗаводИзготовитель>()
                .Property(e => e.НазваниеЗавода)
                .IsUnicode(false);

            modelBuilder.Entity<ЗаводИзготовитель>()
                .Property(e => e.Телефон)
                .IsUnicode(false);

            modelBuilder.Entity<ЗаводИзготовитель>()
                .Property(e => e.Адрес)
                .IsUnicode(false);

            modelBuilder.Entity<ЗаводИзготовитель>()
                .Property(e => e.Факс)
                .IsUnicode(false);

            modelBuilder.Entity<ЗаводИзготовитель>()
                .HasMany(e => e.ЗаказДеталей)
                .WithRequired(e => e.ЗаводИзготовитель)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ЗаводИзготовитель>()
                .HasMany(e => e.Запчасти)
                .WithRequired(e => e.ЗаводИзготовитель)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ЗаказДеталей>()
                .Property(e => e.НазваниеДетали)
                .IsUnicode(false);

            modelBuilder.Entity<Заказы>()
                .Property(e => e.Поломка)
                .IsUnicode(false);

            modelBuilder.Entity<Запчасти>()
                .Property(e => e.НазваниеЗапчасти)
                .IsUnicode(false);

            modelBuilder.Entity<Исполнители>()
                .Property(e => e.Телефон)
                .IsUnicode(false);

            modelBuilder.Entity<Исполнители>()
                .Property(e => e.ФИО)
                .IsUnicode(false);

            modelBuilder.Entity<Исполнители>()
                .HasMany(e => e.Заказы)
                .WithRequired(e => e.Исполнители)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<КвалификацияМастера>()
                .Property(e => e.Квалификация)
                .IsUnicode(false);

            modelBuilder.Entity<КвалификацияМастера>()
                .HasMany(e => e.Исполнители)
                .WithRequired(e => e.КвалификацияМастера)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Клиенты>()
                .Property(e => e.Телефон)
                .IsUnicode(false);

            modelBuilder.Entity<Клиенты>()
                .Property(e => e.Адрес)
                .IsUnicode(false);

            modelBuilder.Entity<Клиенты>()
                .Property(e => e.ФИО)
                .IsUnicode(false);

            modelBuilder.Entity<Клиенты>()
                .HasMany(e => e.Заказы)
                .WithRequired(e => e.Клиенты)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<НаличиеЗапчастей>()
                .Property(e => e.НаличиеЗапчасти)
                .IsUnicode(false);

            modelBuilder.Entity<НаличиеЗапчастей>()
                .HasMany(e => e.Запчасти)
                .WithRequired(e => e.НаличиеЗапчастей)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<СостояниеРемонта>()
                .Property(e => e.Состояние)
                .IsUnicode(false);

            modelBuilder.Entity<СостояниеРемонта>()
                .HasMany(e => e.Заказы)
                .WithRequired(e => e.СостояниеРемонта)
                .WillCascadeOnDelete(false);
        }
    }
}
