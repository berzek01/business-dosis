// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.UserControls.Encryption
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

#nullable disable
namespace SIIV.WebApp.UserControls
{
  public class Encryption
  {
    private static readonly string EncryptCode = ConfigurationManager.AppSettings[nameof (EncryptCode)];
    private static readonly string PrivateKey = ConfigurationManager.AppSettings[nameof (PrivateKey)];

    public string Encrypt(string cadena, string seguridad)
    {
      byte[] bytes1 = Encoding.UTF8.GetBytes(cadena);
      byte[] bytes2 = new Rfc2898DeriveBytes(seguridad, Encoding.ASCII.GetBytes(Encryption.EncryptCode)).GetBytes(32);
      RijndaelManaged rijndaelManaged = new RijndaelManaged();
      rijndaelManaged.Mode = CipherMode.CBC;
      rijndaelManaged.Padding = PaddingMode.PKCS7;
      ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor(bytes2, Encoding.ASCII.GetBytes(Encryption.PrivateKey));
      byte[] array;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, encryptor, CryptoStreamMode.Write))
        {
          cryptoStream.Write(bytes1, 0, bytes1.Length);
          cryptoStream.FlushFinalBlock();
          array = memoryStream.ToArray();
          cryptoStream.Close();
        }
        memoryStream.Close();
      }
      return Convert.ToBase64String(array);
    }

    public string Decrypt(string cadenaTexto, string seguridad)
    {
      byte[] buffer = Convert.FromBase64String(cadenaTexto);
      byte[] bytes = new Rfc2898DeriveBytes(seguridad, Encoding.ASCII.GetBytes(Encryption.EncryptCode)).GetBytes(32);
      RijndaelManaged rijndaelManaged = new RijndaelManaged();
      rijndaelManaged.Mode = CipherMode.CBC;
      rijndaelManaged.Padding = PaddingMode.PKCS7;
      ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor(bytes, Encoding.ASCII.GetBytes(Encryption.PrivateKey));
      MemoryStream memoryStream = new MemoryStream(buffer);
      CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, decryptor, CryptoStreamMode.Read);
      byte[] numArray = new byte[buffer.Length];
      int count = cryptoStream.Read(numArray, 0, numArray.Length);
      memoryStream.Close();
      cryptoStream.Close();
      return Encoding.UTF8.GetString(numArray, 0, count).TrimEnd("\0".ToCharArray());
    }
  }
}
