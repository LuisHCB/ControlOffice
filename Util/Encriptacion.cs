using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Util
{
    public class Encriptacion
    {
        private string _LlavePrivada;

        public Encriptacion(string LlavePrivada)
        {
            this._LlavePrivada = LlavePrivada;
        }

        public Encriptacion()
        {
            this._LlavePrivada = "miLlave";
        }

        public string LlavePrivada
        {
            get { return _LlavePrivada; }
        }

        // byte[] keyArray;//aqui se guarda la llave


        /* public string Encriptar(string cadena)
         {
             byte[] cadAcifrar = UTF8Encoding.UTF8.GetBytes(cadena);//texto que se va a encriptar
             MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();//instancia
             keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(_LlavePrivada));
             //byte[] cifrado =  hashmd5.ComputeHash(cadAcifrar);
             hashmd5.Clear();
             char[] salida = new char [1024];
             int index =Convert.ToBase64CharArray(cifrado,0,cifrado.Length,salida,0);
             return new string(salida, 0, index);
         }

         public string Desencriptar(string cadena)
         {
             byte[] hash = Convert.FromBase64String(cadena);
             MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
             byte[] descifrado = hashmd5.ComputeHash(hash);
             hashmd5.Clear();
             return Encoding.UTF8.GetString(descifrado);

         }*/

        public string Encriptar(string texto)
        {
            try
            {

                byte[] keyArray;

                byte[] Arreglo_a_Cifrar =
                UTF8Encoding.UTF8.GetBytes(texto);


                MD5CryptoServiceProvider hashmd5 =
                new MD5CryptoServiceProvider();

                keyArray = hashmd5.ComputeHash(
                UTF8Encoding.UTF8.GetBytes(_LlavePrivada));

                hashmd5.Clear();

                //Algoritmo 3DAS
                TripleDESCryptoServiceProvider tdes =
                new TripleDESCryptoServiceProvider();

                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;


                ICryptoTransform cTransform =
                tdes.CreateEncryptor();


                byte[] ArrayResultado =
                cTransform.TransformFinalBlock(Arreglo_a_Cifrar,
                0, Arreglo_a_Cifrar.Length);

                tdes.Clear();


                return Convert.ToBase64String(ArrayResultado,
                0, ArrayResultado.Length);

            }
            catch
            {
                return null;
            }
        }

        public string Desencriptar(string textoEncriptado)
        {
            try
            {
                byte[] keyArray;
                byte[] Array_a_Descifrar =
                Convert.FromBase64String(textoEncriptado);

                MD5CryptoServiceProvider hashmd5 =
                new MD5CryptoServiceProvider();

                keyArray = hashmd5.ComputeHash(
                UTF8Encoding.UTF8.GetBytes(_LlavePrivada));

                hashmd5.Clear();

                TripleDESCryptoServiceProvider tdes =
                new TripleDESCryptoServiceProvider();

                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform =
                tdes.CreateDecryptor();

                byte[] resultArray =
                cTransform.TransformFinalBlock(Array_a_Descifrar,
                0, Array_a_Descifrar.Length);

                tdes.Clear();

                return UTF8Encoding.UTF8.GetString(resultArray);

            }
            catch
            {
                return null;
            }
        }
    }
}
