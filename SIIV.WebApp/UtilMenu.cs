// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.UtilMenu
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp
{
  public static class UtilMenu
  {
    public static T Find<T>(ControlCollection Controls, string ID) where T : Control
    {
      T obj = default (T);
      if (Controls == null)
        return default (T);
      foreach (Control control in Controls)
      {
        if ((object) obj == null)
        {
          if (control is T)
          {
            if (string.IsNullOrEmpty(ID))
            {
              obj = control as T;
              break;
            }
            if (control.ID != null && control.ID.Equals(ID))
            {
              obj = control as T;
              break;
            }
            if (control.ID != null && control.UniqueID.Equals(ID))
            {
              obj = control as T;
              break;
            }
            if (control.ID != null && control.ClientID.Equals(ID))
            {
              obj = control as T;
              break;
            }
          }
          obj = UtilMenu.Find<T>(control.Controls, ID);
        }
        else
          break;
      }
      return obj;
    }

    public static string SerializePC(ParameterCollection pc)
    {
      if (pc == null)
        return (string) null;
      MemoryStream memoryStream = new MemoryStream();
      IEnumerable<Type> knownTypes = (IEnumerable<Type>) new Type[1]
      {
        typeof (Parameter)
      };
      new DataContractJsonSerializer(pc.GetType(), knownTypes).WriteObject((Stream) memoryStream, (object) pc);
      string str = Encoding.Default.GetString(memoryStream.ToArray());
      memoryStream.Close();
      return str;
    }

    public static ParameterCollection DeserializePC(string json)
    {
      if (string.IsNullOrEmpty(json))
        return (ParameterCollection) null;
      ParameterCollection parameterCollection1 = new ParameterCollection();
      MemoryStream memoryStream = new MemoryStream(Encoding.Default.GetBytes(json));
      IEnumerable<Type> knownTypes = (IEnumerable<Type>) new Type[1]
      {
        typeof (Parameter)
      };
      ParameterCollection parameterCollection2 = (ParameterCollection) new DataContractJsonSerializer(parameterCollection1.GetType(), knownTypes).ReadObject((Stream) memoryStream);
      memoryStream.Close();
      return parameterCollection2;
    }

    public static string normalize(object o)
    {
      return o == null || o == DBNull.Value ? string.Empty : o.ToString().Trim();
    }
  }
}
