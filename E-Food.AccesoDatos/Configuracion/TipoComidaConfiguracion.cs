using E_Food.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Food.AccesoDatos.Configuracion
{
    public class TipoComidaConfiguracion : IEntityTypeConfiguration<TipoComida>
    {
        public void Configure(EntityTypeBuilder<TipoComida> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Nombre).IsRequired().HasMaxLength(60);
        }
    }
}
