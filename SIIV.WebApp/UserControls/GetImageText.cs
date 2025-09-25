// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.UserControls.GetImageText
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.Warehouse.BL;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Web;

#nullable disable
namespace SIIV.WebApp.UserControls
{
  public class GetImageText : IHttpHandler
  {
    private int _imagenAncho = 50;
    private int _imagenAlto = 50;
    private string _texto = "Empty";
    private string _tipo = "";
    private string _pkImagen = "";
    private string _fuenteFamily = "Verdana";
    private float _fuenteSize = 2f;
    private string _fuenteColor = "Black";
    private int _fuentePosX = 0;
    private int _fuentePosY = 0;

    public void ProcessRequest(HttpContext context)
    {
      this.getParameters();
      this.GetImage();
    }

    public bool ThumbnailCallback() => false;

    private void GetImage()
    {
      byte[] numArray = (byte[]) null;
      if (this._tipo == "imgDeliverPC")
        numArray = new PlateDeliverQueriesBL().GetProductImage(this._pkImagen);
      if (this._tipo == "imgDeliverValid")
        numArray = (byte[]) new PlateDeliverQueriesBL().GetPlateDeliverSearchPlateOldAsPlateNewBy(this._pkImagen.Substring(0, 10).Trim(), Convert.ToInt32(this._pkImagen.Substring(10), (IFormatProvider) CultureInfo.CurrentCulture)).Rows[0]["g_Image"];
      else if (this._tipo == "imgPlate")
        numArray = this.ImageToBytes(Image.FromFile(this._pkImagen));
      Bitmap bitmap = (Bitmap) TypeDescriptor.GetConverter(typeof (Bitmap)).ConvertFrom((object) numArray);
      bitmap.SetResolution(75f, 75f);
      if (this._imagenAncho < 0)
        this._imagenAncho = bitmap.Width;
      this._imagenAlto = bitmap.Height * this._imagenAncho / bitmap.Width;
      Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(this.ThumbnailCallback);
      Image thumbnailImage = bitmap.GetThumbnailImage(this._imagenAncho, this._imagenAlto, callback, IntPtr.Zero);
      if (this._texto.Trim().Length > 0)
      {
        Font font = this.GetFont();
        Graphics graphics = Graphics.FromImage(thumbnailImage);
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.DrawString(this._texto, font, this.GetColor(this._fuenteColor), (float) this._fuentePosX, (float) this._fuentePosY);
      }
      HttpContext.Current.Response.ContentType = "image/Jpeg";
      thumbnailImage.Save(HttpContext.Current.Response.OutputStream, ImageFormat.Jpeg);
    }

    private Font GetFont() => new Font(this._fuenteFamily, this._fuenteSize, FontStyle.Regular);

    private Brush GetColor(string ColorName) => (Brush) new SolidBrush(Color.FromName(ColorName));

    public bool IsReusable => false;

    private void getParameters()
    {
      if (HttpContext.Current.Request.QueryString["type"] != "")
        this._tipo = HttpUtility.UrlDecode(HttpContext.Current.Request.QueryString["type"]);
      if (HttpContext.Current.Request.QueryString["ancho"] != "")
        this._imagenAncho = Convert.ToInt32(HttpContext.Current.Request.QueryString["ancho"], (IFormatProvider) CultureInfo.CurrentCulture);
      if (HttpContext.Current.Request.QueryString["pkImagen"] != "")
        this._pkImagen = HttpUtility.UrlDecode(HttpContext.Current.Request.QueryString["pkImagen"]);
      if (HttpContext.Current.Request.QueryString["t"] != "")
        this._texto = HttpUtility.UrlDecode(HttpContext.Current.Request.QueryString["t"]);
      if (HttpContext.Current.Request.QueryString["ff"] != "")
        this._fuenteFamily = HttpUtility.UrlDecode(HttpContext.Current.Request.QueryString["ff"]);
      if (HttpContext.Current.Request.QueryString["fs"] != "")
        this._fuenteSize = Convert.ToSingle(HttpUtility.UrlDecode(HttpContext.Current.Request.QueryString["fs"]), (IFormatProvider) CultureInfo.CurrentCulture);
      if (HttpContext.Current.Request.QueryString["fc"] != "")
        this._fuenteColor = HttpUtility.UrlDecode(HttpContext.Current.Request.QueryString["fc"]);
      if (HttpContext.Current.Request.QueryString["fx"] != "")
        this._fuentePosX = Convert.ToInt32(HttpUtility.UrlDecode(HttpContext.Current.Request.QueryString["fx"]), (IFormatProvider) CultureInfo.CurrentCulture);
      if (!(HttpContext.Current.Request.QueryString["fy"] != ""))
        return;
      this._fuentePosY = Convert.ToInt32(HttpUtility.UrlDecode(HttpContext.Current.Request.QueryString["fy"]), (IFormatProvider) CultureInfo.CurrentCulture);
    }

    public byte[] ImageToBytes(Image img)
    {
      return (byte[]) new ImageConverter().ConvertTo((object) img, typeof (byte[]));
    }
  }
}
