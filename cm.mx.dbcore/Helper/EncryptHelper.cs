using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;

namespace cm.mx.dbCore.Helper
{
    public class EncryptHelper
    {
        protected static string clave = "#1F,)n<+o]a$I/IhbL4Hdp[{04^^9Mh~#i+5Ks]W/Kn QPgQ]|=HAV@v&$+? le+";
        public EncryptHelper() {
            string _clave = ConfigurationManager.AppSettings["encryptKey"];
            if (!string.IsNullOrEmpty(_clave))
                clave = _clave; 
        }
        /// <summary>
        /// Genera una clave apartir de la combinación del Nombre del Usuario y Contraseña , Tomando el AppSettings "encryptKey" en caso de no existir, toma un valor por defautl
        /// </summary>
        /// <param name="Usuario">Nombre de autentificacion</param>
        /// <param name="Password">Clave de la contraseña</param>
        /// <returns></returns>
        public static string EncriptarMD5(string Usuario, string Password)
        {
            string texto = Usuario.ToLowerInvariant() + "|" + Password;
            byte[] Results;
            UTF8Encoding UTF8 = new UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(clave));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToEncrypt = UTF8.GetBytes(texto);
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results);
        }
        /// <summary>
        /// Desencripta la clave de contraseña con una clave en especifica
        /// </summary>
        /// <param name="Password">Clave de la contraseña</param>
        /// <returns></returns>
        public static string DesencriptarMD5(string Password)
        {
            byte[] Results;
            UTF8Encoding UTF8 = new UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(clave));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToDecrypt = Convert.FromBase64String(Password);
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return UTF8.GetString(Results);
        }
    }
}
