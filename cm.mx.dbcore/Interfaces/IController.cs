using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cm.mx.dbCore.Interfaces
{
    public interface IController
    {
        bool Exito
        {
            get;
            set;
        }
        string Mensaje
        {
            get;
            set;
        }
        List<string> Mensajes
        {
            get;
        }
        List<string> Errores
        {
            get;
        }
                
        //bool Guardar(T entidad);
        //T Recuperar(object Id);
        //IEnumerable<T> Listar();
        //void Eliminar(object Id);
        //T Actualizar(T entidad);
    }
}
