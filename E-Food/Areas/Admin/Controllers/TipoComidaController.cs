using E_Food.AccesoDatos.Repositorio.IRepositorio;
using E_Food.Modelos;
using E_Food.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace E_Food.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TipoComidaController : Controller
    {

        private readonly IUnidadTrabajo _unidadTrabajo;

        public TipoComidaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            TipoComida tipoComida = new TipoComida();

            if (id == null)
            {
                // Crear un nuevo tipo de comida
                return View(tipoComida);
            }
            // Actualizamos tipo de comida
            tipoComida = await _unidadTrabajo.TipoComida.Obtener(id.GetValueOrDefault());
            if (tipoComida == null)
            { 
                return NotFound();
            }
            return View(tipoComida);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TipoComida tipoComida)
        {
            if (ModelState.IsValid)
            {
                if (tipoComida.Id == 0) 
                {
                    await _unidadTrabajo.TipoComida.Agregar(tipoComida);
                    TempData[DS.Exitosa] = "Línea de Comida creada Exitosamente";
                }
                else 
                {
                    _unidadTrabajo.TipoComida.Actualizar(tipoComida);
                    TempData[DS.Exitosa] = "Línea de Comida actualizada Exitosamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al grabar la Linea de Comida";
            return View(tipoComida);
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.TipoComida.ObtenerTodos();
            return Json(new { data = todos });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var tipoComidaDb = await _unidadTrabajo.TipoComida.Obtener(id);
            if (tipoComidaDb == null)
            {
                return Json(new { success = false, message = "Error al borrar la linea de Comida" });
            }
            _unidadTrabajo.TipoComida.Remover(tipoComidaDb);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Linea de Comida borrada exitosamente" });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.TipoComida.ObtenerTodos();
            if (id == 0)
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else 
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim() && b.Id !=id);
            }
            if (valor)
            {
                return Json(new { data = true });
            }
            return Json(new { data = false });
        }

        #endregion

    }
}
