using E_Food.AccesoDatos.Data;
using E_Food.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Food.AccesoDatos.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly ApplicationDbContext _db;
        public ITipoComidaRepositorio TipoComida { get; private set; }
        public ITipoPrecioRepositorio TipoPrecio { get; private set; }

        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db = db;
            TipoComida = new TipoComidaRepositorio(_db);
            TipoPrecio = new TipoPrecioRepositorio(_db);
        }


        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }
    }
}
