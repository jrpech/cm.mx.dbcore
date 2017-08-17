using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cm.mx.dbCore.Tools
{
    public class Paginacion
    {
        /// <summary>
        /// Número de página
        /// </summary>
        public int Pagina { get; set; }
        /// <summary>
        /// Cantidad de registros por página
        /// </summary>
        public int Cantidad { get; set; }
        /// <summary>
        /// Valida si el método realizará la paginación del resultado
        /// </summary>
        public bool Paginar { get; set; }
        /// <summary>
        /// Retorna el total de registros encontrados.
        /// </summary>
        public int TotalRegistros { get; set; }
    }
}
