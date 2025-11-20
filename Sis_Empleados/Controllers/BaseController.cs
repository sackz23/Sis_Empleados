using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Sis_Empleados.Models;

public class BaseController : Controller
{
    protected readonly ApplicationDbContext _context;

    public BaseController(ApplicationDbContext context)
    {
        _context = context;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

        if (usuarioId != null)
        {
            var usuario = _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefault(u => u.Id_Usuario == usuarioId);

            if (usuario != null)
            {
                ViewBag.Usuario = usuario.Nombre_Usuario;
                ViewBag.Rol = usuario.Rol.Nombre_Rol;
            }
        }

        base.OnActionExecuting(context);
    }
}
