using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace cm.mx.dbCore.Interfaces
{
    internal interface IRepository<T> where T : class
    {
        bool Exito
        {
            get;
        }
        List<string> Errores
        {
            get;
        }
        List<string> Mensajes
        {
            get;
        }
        IEnumerable<T> GetAll();
        void Add(T entidad);
        bool Update(T modificado);
        void Delete(T entidad);
        bool Save(T entidad);
        bool Save();
        IEnumerable<T> Query(Expression<Func<T, bool>> filter);
        IEnumerable<T> Find(int Page, int Cant, Expression<Func<T, bool>> filter);
        T GetById(int id);
        T GetById(object id);
        T GetNewEntidad();
    }
}
