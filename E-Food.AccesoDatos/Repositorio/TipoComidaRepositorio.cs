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
    public class TipoComidaRepositorio : Repositorio<TipoComida>, ITipoComidaRepositorio
    {

        private readonly ApplicationDbContext _db;

        public TipoComidaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;    
        }

        public void Actualizar(TipoComida tipoComida)
        {
            var tipoComidaBD = _db.TiposComidas.FirstOrDefault(b => b.Id == tipoComida.Id);
            if(tipoComidaBD !=null) 
            {
                tipoComidaBD.Nombre = tipoComida.Nombre;
                _db.SaveChanges();
            }
        }
    }
}
