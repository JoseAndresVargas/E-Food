using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Food.AccesoDatos.Repositorio.IRepositorio
{
    public interface IUnidadTrabajo : IDisposable 
    {

        ITipoComidaRepositorio TipoComida { get; }

        Task Guardar();
    }
}
