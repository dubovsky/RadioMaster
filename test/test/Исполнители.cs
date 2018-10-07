namespace test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Исполнители
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Исполнители()
        {
            Заказы = new HashSet<Заказы>();
        }

        public int Стаж { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ИсполнительID { get; set; }

        [Required]
        [StringLength(11)]
        public string Телефон { get; set; }

        [Required]
        [StringLength(30)]
        public string ФИО { get; set; }

        public int КвалификацияID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Заказы> Заказы { get; set; }

        public virtual КвалификацияМастера КвалификацияМастера { get; set; }
    }
}
