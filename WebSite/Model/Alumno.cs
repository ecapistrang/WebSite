namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.SqlClient;
    using System.Linq;

    [Table("Alumno")]
    public partial class Alumno
    {
        public Alumno()
        {
            Cursos = new List<Curso>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }

        public ICollection<Curso> Cursos { get; set; }

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
                
                throw new Exception(e.Message);
            }

            return alumnos;
        }

        public Alumno Obtener(int id)
        {
            var alumno = new Alumno();
            try
            {
                using (var context = new TextContext())
                {
                    alumno = context.Alumno
                                        .Include("Cursos")
                                        .Where(i => i.id == id)
                                        .Single(); // Retorna una sola fila
                                        //.First() retorna el primer elemento encontrado
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

            return alumno;
        }


        public void Guardar()
        {            
            try
            {
                using (var context = new TextContext())
                {
                    if (this.id == 0)
                    {
                        context.Entry(this).State = EntityState.Added;
                    }else
                    {
                        context.Database.ExecuteSqlCommand(
                                            "DELETE FROM AlumnoCurso WHERE Alumno_id = @id",
                                            new SqlParameter("id", this.id)
                                        );

                        var cursoBK = this.Cursos;

                        this.Cursos = null;
                        context.Entry(this).State = EntityState.Modified;
                        this.Cursos = cursoBK;
                    }

                    foreach (var c in this.Cursos)
                        context.Entry(c).State = EntityState.Unchanged;

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }       
        }
    }
}
