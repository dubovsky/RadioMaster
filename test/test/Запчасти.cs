namespace test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Запчасти
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Запчасти()
        {
            Заказы = new HashSet<Заказы>();
        }

        public double ЦенаЗапчасти { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ЗапчастьID { get; set; }

        public int НаличиеID { get; set; }

        public int ЗаводID { get; set; }

        [Required]
        [StringLength(50)]
        public string НазваниеЗапчасти { get; set; }

        public virtual ЗаводИзготовитель ЗаводИзготовитель { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Заказы> Заказы { get; set; }

        public virtual НаличиеЗапчастей НаличиеЗапчастей { get; set; }
    }
}
