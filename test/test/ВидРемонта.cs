namespace test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ВидРемонта
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ВидРемонта()
        {
            Заказы = new HashSet<Заказы>();
        }

        [Required]
        [StringLength(50)]
        public string ТипРемонта { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ВидРемонтаID { get; set; }

        public int СрокРемонта { get; set; }

        public double ЦенаРемонта { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Заказы> Заказы { get; set; }
    }
}
