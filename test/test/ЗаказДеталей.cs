namespace test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ЗаказДеталей
    {
        [Required]
        [StringLength(50)]
        public string НазваниеДетали { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int НоваяДетальID { get; set; }

        [Column(TypeName = "date")]
        public DateTime Дата { get; set; }

        public int Количество { get; set; }

        public int ЗаводID { get; set; }

        public virtual ЗаводИзготовитель ЗаводИзготовитель { get; set; }
    }
}
