namespace test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ЗаводИзготовитель
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ЗаводИзготовитель()
        {
            ЗаказДеталей = new HashSet<ЗаказДеталей>();
            Запчасти = new HashSet<Запчасти>();
        }

        [Required]
        [StringLength(20)]
        public string НазваниеЗавода { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ЗаводID { get; set; }

        [Required]
        [StringLength(11)]
        public string Телефон { get; set; }

        [Required]
        [StringLength(40)]
        public string Адрес { get; set; }

        [StringLength(11)]
        public string Факс { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ЗаказДеталей> ЗаказДеталей { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Запчасти> Запчасти { get; set; }
    }
}
