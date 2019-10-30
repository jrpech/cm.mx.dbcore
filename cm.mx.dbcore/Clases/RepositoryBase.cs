using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using cm.mx.dbCore.Interfaces;
using cm.mx.dbCore.Helper;
using System.Data;
using NHibernate;
using System.Linq.Expressions;

namespace cm.mx.dbCore.Clases
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class, new()
	{        
        public ISession _session;
    
		protected bool _exito;
		protected List<string> _errores;
		protected List<string> _mensajes;
		public bool Exito
		{
			get
			{
				return this._exito;
			}
		}
		public List<string> Errores
		{
			get
			{
				if (this._errores == null)
				{
					this._errores = new List<string>();
				}
				return this._errores;
			}
		}
		public List<string> Mensajes
		{
			get
			{
				if (this._mensajes == null)
				{
					this._mensajes = new List<string>();
				}
				return this._mensajes;
			}
			set
			{
				this._mensajes = value;
			}
		}

        public RepositoryBase()
		{
			try
			{				
				this.Errores.Clear();
				this.Mensajes.Clear();
                if (_session == null || !_session.IsOpen)
                    this._session = this.createContext(_session);
                this._exito = true;
				
			}
			catch (Exception innerException)
			{
				while (innerException.InnerException != null)
				{
					innerException = innerException.InnerException;
				}
				this.Errores.Add(innerException.Message);
				throw innerException;
			}
		}
        private ISession createContext(ISession Session)
		{
            string server = string.Empty;
            string dataBase = string.Empty;
            string usuario = string.Empty;
            string password = string.Empty;
            ISession oSession;

            server = ConfigurationManager.AppSettings["server"];
            dataBase = ConfigurationManager.AppSettings["dataBase"];
            usuario = ConfigurationManager.AppSettings["usuario"];
            password = ConfigurationManager.AppSettings["password"];

            if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(dataBase) || string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
			{
				throw new Exception("Un valor de la cadena de conexión no está correctamente configurado");
			}

            oSession = ConnectionHelper.GetConnection<T>(server, dataBase, usuario, password);

            return oSession;
		}
		public IEnumerable<T> GetAll()
		{
            IList<T> lsEntidad = new List<T>();

            lsEntidad = this._session.CreateCriteria<T>().List<T>();
            //Sthis._session.Close();

            return lsEntidad;
		}
		public void Add(T entidad)
		{
			this._session.Merge(entidad);
            //this._session.Close();
		}
		public void Delete(T entidad)
		{
            this._session.Delete(entidad);
            _session.Flush();
            //this._session.Close();
		}
		public IEnumerable<T> Query(Expression<Func<T,bool>> filter)
		{
            IList<T> lsEntidad = new List<T>();
            lsEntidad = this._session.QueryOver<T>().Where(filter).List();
            //this._session.Close();
            return lsEntidad;
		}
        public IEnumerable<T> Find(int Page, int Cant, Expression<Func<T, bool>> filter)
        {
            this._exito = false;
            IList<T> lsEntidad = new List<T>();

            try
            {
                lsEntidad = _session.QueryOver<T>().Where(filter).Skip(Page * Cant).Take(Cant).List();
            }
            catch (Exception innerException)
            {
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }
                this.Errores.Add(innerException.Message + ". Metodo: " + innerException.TargetSite.Name);
            }

            this._session.Close();

            return lsEntidad;
        }
		public bool Save(T entidad)
		{
			this._exito = false;
            try
            {
                this._session.SaveOrUpdate(entidad);

            }
            catch (Exception innerException)
            {                
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }
                this.Errores.Add(innerException.Message + ". Metodo: " + innerException.TargetSite.Name);
            }

            this._session.Close();

            return this._exito;
		}   
		public abstract bool Update(T modificado);
		public abstract T GetById(object id);
		public abstract T GetNewEntidad();
        
        public bool Save()
        {
            throw new NotImplementedException();
        }

        public abstract T GetById(int id);
    }
}
