using E_Food.AccesoDatos.Repositorio.IRepositorio;
using E_Food.Modelos;
using E_Food.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace E_Food.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TipoPrecioController : Controller
    {

        private readonly IUnidadTrabajo _unidadTrabajo;

        public TipoPrecioController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            TipoPrecio tipoPrecio = new TipoPrecio();

            if (id == null)
            {
                // Crear un nuevo tipo de comida
                return View(tipoPrecio);
            }
            // Actualizamos tipo de comida
            tipoPrecio = await _unidadTrabajo.TipoPrecio.Obtener(id.GetValueOrDefault());
            if (tipoPrecio == null)
            { 
                return NotFound();
            }
            return View(tipoPrecio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TipoPrecio tipoPrecio)
        {
            if (ModelState.IsValid)
            {
                if (tipoPrecio.Id == 0) 
                {
                    await _unidadTrabajo.TipoPrecio.Agregar(tipoPrecio);
                    TempData[DS.Exitosa] = "Tipo de Precio creado Exitosamente";
                }
                else 
                {
                    _unidadTrabajo.TipoPrecio.Actualizar(tipoPrecio);
                    TempData[DS.Exitosa] = "Tipo de Precio actualizado Exitosamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al grabar el Tipo de Precio";
            return View(tipoPrecio);
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.TipoPrecio.ObtenerTodos();
            return Json(new { data = todos });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var tipoPrecioDb = await _unidadTrabajo.TipoPrecio.Obtener(id);
            if (tipoPrecioDb == null)
            {
                return Json(new { success = false, message = "Error al borrar el Tipo de Precio" });
            }
            _unidadTrabajo.TipoPrecio.Remover(tipoPrecioDb);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Tipo de Precio borrada exitosamente" });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.TipoPrecio.ObtenerTodos();
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
