namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Alumno")]
    public partial class Alumno
    {
        public Alumno()
        {
            Curso = new HashSet<Curso>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }

        public virtual ICollection<Curso> Curso { get; set; }

        public List<Alumno> Listar()
        {
            var alumnos = new List<Alumno>();
            try
            {                
                using (var context = new TextContext())
                {
                    alumnos = context.Alumno.ToList();
                }

            }
            catch (Exception e)
            {
                
                throw;
            }

            return alumnos;
        }
    }
}
