namespace test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Заказы
    {
        [Column(TypeName = "date")]
        public DateTime ДатаЗаказа { get; set; }

        [Key]
        public int ЗаказID { get; set; }

        [Required]
        [StringLength(50)]
        public string Поломка { get; set; }

        public int ИсполнительID { get; set; }

        public int СостояниеID { get; set; }

        public int ГарантияID { get; set; }

        public int КлиентID { get; set; }

        public int ВидРемонтаID { get; set; }

        public int? ЗапчастьID { get; set; }

        public virtual ВидРемонта ВидРемонта { get; set; }

        public virtual Гарантии Гарантии { get; set; }

        public virtual Запчасти Запчасти { get; set; }

        public virtual Исполнители Исполнители { get; set; }

        public virtual Клиенты Клиенты { get; set; }

        public virtual СостояниеРемонта СостояниеРемонта { get; set; }
    }
}
