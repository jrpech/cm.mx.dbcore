using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cm.mx.dbCore.Tools
{
    [Serializable]
    public class FiltrosPrincipal
    {
        public string Buscar { get; set; }
        public string Campos { get; set; }
        public string Movimientos { get; set; }
        public string Estatus { get; set; }
        public string Situacion { get; set; }
        public string Fecha { get; set; }
        public string Usuario { get; set; }
        public string Sucursal { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }

    }
}
