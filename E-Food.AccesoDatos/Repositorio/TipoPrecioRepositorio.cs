using E_Food.AccesoDatos.Data;
using E_Food.AccesoDatos.Repositorio.IRepositorio;
using E_Food.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Food.AccesoDatos.Repositorio
{
    public class TipoPrecioRepositorio : Repositorio<TipoPrecio>, ITipoPrecioRepositorio
    {

        private readonly ApplicationDbContext _db;

        public TipoPrecioRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;    
        }

        public void Actualizar(TipoPrecio tipoPrecio)
        {
            var tipoPrecioBD = _db.TiposPrecios.FirstOrDefault(b => b.Id == tipoPrecio.Id);
            if(tipoPrecioBD !=null) 
            {
                tipoPrecioBD.Nombre = tipoPrecio.Nombre;
                _db.SaveChanges();
            }
        }
    }
}
