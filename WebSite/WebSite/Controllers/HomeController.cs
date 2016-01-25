using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
namespace WebSite.Controllers
{
    public class HomeController : Controller
    {
        //generate an object alumno from Alumno Class
        private Alumno alumno = new Alumno();
        private Curso curso = new Curso();

        //
        // GET: /Home/

        public ActionResult Index()
        {            
            return View(alumno.Listar());
        }

        public ActionResult Ver(int id)
        {
            return View(alumno.Obtener(id));
        }

        //Este método encapsula la opción de editar y registrar en uno solo
        public ActionResult Crud(int id = 0)
        {            
            ViewBag.Cursos = curso.Todo();
            return View(
                id > 0 ? alumno.Obtener(id)
                        : this.alumno
            );
        }

        public ActionResult Guardar(Alumno model, int[] cursos = null)
        {
            if (cursos != null)
            {
                foreach (var item in cursos)
                {
                    model.Cursos.Add(new Curso { id = item });
                }
            }

            model.Guardar();
            return Redirect("~/home/crud/" + model.id);
        }
    }   
    
}

