using ASP.Net_MVC5Membresias.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace ASP.Net_MVC5Membresias.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles ="ApruebaPrestamos")]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    var idUsuarioActual = User.Identity.GetUserId();

                    var userManager = new UserManager<ApplicationUser>
                        (new UserStore<ApplicationUser>(db));

                    var usuario = userManager.FindById(idUsuarioActual);
                    var lugarDeNacimiento = usuario.LugarDeNacimiento;

                    #region Roles
                    //var roleManager = new RoleManager<IdentityRole>
                    //    (new RoleStore<IdentityRole>(db));

                    //// Crear rol
                    //var resultado = roleManager.Create(new IdentityRole("ApruebaPrestamos"));

                    //var userManager = new UserManager<ApplicationUser>
                    //    (new UserStore<ApplicationUser>(db));

                    //// Agregar usuario a rol
                    //resultado = userManager.AddToRole(idUsuarioActual, "ApruebaPrestamos");

                    //// Usuario está en rol?
                    //var usuarioEstaEnRol = userManager.IsInRole(idUsuarioActual, "ApruebaPrestamos");
                    //var usuarioEstaEnRol2 = userManager.IsInRole(idUsuarioActual, "TDC.Reportes.Distribuciones");

                    //// roles del usuario
                    //var roles = userManager.GetRoles(idUsuarioActual);

                    //// Remover a usuario de Rol
                    //resultado = userManager.RemoveFromRole(idUsuarioActual, "ApruebPrestamos");

                    //// Borrar rol
                    //var rolVendedor = roleManager.FindByName("ApruebPrestamos");
                    //roleManager.Delete(rolVendedor);
                    #endregion

                }
            }
            return View();
        }

        public ActionResult About()
        {
            var secureAppSettings = ConfigurationManager.GetSection("secureAppSettings") as NameValueCollection;
            var dato1 = secureAppSettings["dato1"];
            var dato2 = secureAppSettings["dato2"];
            var sinSal1 = System.Web.Helpers.Crypto.Hash("hola");
            var sinSal2 = System.Web.Helpers.Crypto.Hash("hola");
            var iguales = sinSal1 == sinSal2; // True

            var conSal1 = System.Web.Helpers.Crypto.HashPassword("hola");
            var conSal2 = System.Web.Helpers.Crypto.HashPassword("hola");
            var iguales2 = conSal1 == conSal2;

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public static string Hash(string textoPlano)
        {
            byte[] salt;
            byte[] buffer;
            if(textoPlano == null)
            {
                throw new ArgumentNullException(nameof(textoPlano));
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(textoPlano, 16, 1000))
            {
                salt = bytes.Salt;
                buffer = bytes.GetBytes(32);
            }
            byte[] dst = new byte[49];
            Buffer.BlockCopy(salt, 0, dst, 1, 16);
            Buffer.BlockCopy(buffer, 0, dst, 17, 32);
            return Convert.ToBase64String(dst);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if(password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if((src.Length != 49) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[16];
            Buffer.BlockCopy(src, 1, dst, 0, 16);
            byte[] buffer3 = new byte[32];
            Buffer.BlockCopy(src, 17, buffer3, 0, 32);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 1000))
            {
                buffer4 = bytes.GetBytes(32);
            }

            return buffer3.SequenceEqual(buffer4);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}