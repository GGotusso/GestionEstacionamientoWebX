using Services.DAL.Contracts;
using Services.DAL.Implementations.SqlServer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DAL.FactoryServices
{
    internal class FactoryDAL
    {
        private static IUsuarioDAL _UsuarioRepository;
        private static ILogDAL _LogRepository;
        private static readonly int backendType;
        private static readonly object _lock = new object();
        /// <summary>
        /// Constructor estático que inicializa el tipo de backend desde la configuración de la aplicación.
        /// </summary>
        static FactoryDAL()
        {
            var configValue = ConfigurationManager.AppSettings["BackendType"];
            if (!int.TryParse(configValue, out backendType))
            {
                backendType = (int)BackendType.SqlServer;
            }
        }
        /// <summary>
        /// Obtiene una instancia del repositorio de usuario según el backend configurado.
        /// Implementa un patrón singleton y garantiza que solo se cree una instancia.
        /// </summary>
        public static IUsuarioDAL UsuarioRepository
        {
            get
            {
                if (_UsuarioRepository == null)
                {
                    lock (_lock)
                    {
                        if (_UsuarioRepository == null)
                        {
                            switch ((BackendType)backendType)
                            {
                                case BackendType.SqlServer:
                                    _UsuarioRepository = new UsuarioRepository();
                                    break;
                                default:
                                    throw new NotSupportedException("Backend no soportado.");
                            }
                        }
                    }
                }
                return _UsuarioRepository;
            }
        }

        public static ILogDAL LogRepository
        {
            get
            {
                if (_LogRepository == null)
                {
                    lock (_lock)
                    {
                        if (_LogRepository == null)
                        {
                            switch ((BackendType)backendType)
                            {
                                case BackendType.SqlServer:
                                    _LogRepository = new LogRepository();
                                    break;
                                default:
                                    throw new NotSupportedException("Backend no soportado.");
                            }
                        }
                    }
                }
                return _LogRepository;
            }
        }
    }
    /// <summary>
    /// Enumeración que define los tipos de backend compatibles para la fábrica.
    /// </summary>
    internal enum BackendType
    {
        /// <summary>
        /// Backend de base de datos SQL Server.
        /// </summary>
        SqlServer = 1

    }
}
