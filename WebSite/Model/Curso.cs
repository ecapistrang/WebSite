namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Curso")]
    public partial class Curso
    {
        public Curso()
        {
            Alumno = new HashSet<Alumno>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Column("Curso")]
        [Required]
        [StringLength(100)]
        public string Curso1 { get; set; }

        public virtual ICollection<Alumno> Alumno { get; set; }
    }
}
