// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Operations.PlateDeliveryItemOld
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Registration.BL;
using SIIV.SystemParameter.BL;
using SIIV.Warehouse.BL;
using SIIV.WebApp.Claim;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Operations
{
  public class PlateDeliveryItemOld : Page
  {
    private int i_ProductIdOld = 0;
    protected HiddenField hdiState;
    protected HiddenField hdiNumDoc;
    protected HiddenField hdiChek;
    protected HiddenField hdiFecIni;
    protected HiddenField hdiFenFin;
    protected HiddenField hdiPlateTypeId;
    protected TextBox txtRequirementId;
    protected TextBox txtContributor;
    protected TextBox txtTramite;
    protected TextBox txtPlateNew;
    protected TextBox txtTitle;
    protected TextBox txtRequirementType;
    protected TextBox txtPlateOld;
    protected TextBox txtBrand;
    protected TextBox txtModel;
    protected TextBox txtVehicleTypeUse;
    protected TextBox txtVehicleCategory;
    protected TextBox txtSerialNumber;
    protected TextBox txtOwnerCompleteName;
    protected TextBox txtOwnerDocumentType;
    protected TextBox txtOwnerDocumentNumber;
    protected TextBox txtCode;
    protected DataList dlProductComposition;
    protected CheckBox chkItems;
    protected UpdatePanel UpdatePanel1;
    protected System.Web.UI.WebControls.Image imgbtnPlateRecovery;
    protected RadioButtonList rblIsHavePlateOld;
    protected Fecha WebDatePicker1;
    protected HtmlGenericControl DivDatosIntercambio;
    protected Label lblTituloDatosDenuncia;
    protected CheckBoxList chklRequisiteExchangeMode;
    protected HtmlGenericControl divConsMTCtacha;
    protected Fecha wdpConsMTCtacha;
    protected TextBox txtReferMTCtacha;
    protected Label lblNroPlacaTachada;
    protected TextBox txtNroPlacaTachada;
    protected HtmlGenericControl divDatosDenuncia;
    protected Fecha wdpDateDenuncia;
    protected TextBox txtDependency;
    protected TextBox txtParte;
    protected HtmlTableRow trReferencesNumber;
    protected TextBox txtReferencesNumber;
    protected RadioButtonList rblPersonTypeOwner;
    protected HtmlGenericControl DivDatosRecabante;
    protected RadioButtonList rblTypeRecabante;
    protected RadioButtonList rblcurrentOption;
    protected RadioButtonList rblPersonTypeCollect;
    protected HtmlTableRow trwddOwner;
    protected DropDownList wddOwner;
    protected HtmlTableRow trtxtNombres;
    protected TextBox txtNombresRecabante;
    protected HtmlTableRow trwddDocument;
    protected DropDownList wddDocumentDescription;
    protected TextBox txtNumeroRecabante;
    protected HtmlTableRow trNotaria;
    protected TextBox txtNotariaRecabante;
    protected HtmlTableRow trPartida;
    protected TextBox txtPartidaRecabante;
    protected CheckBoxList chklRequisiteToDeliver;
    protected Label lblMessage;
    protected Button wibFinalize;
    protected Button wibCancel;
    protected Button wibClain;
    protected Button wibAceptar;
    protected HiddenField hddfVehicleClasification;
    protected HiddenField hddfProcessTypeId;
    protected HiddenField hddfUseCorrespondence;
    protected HiddenField hddfRequirementPlateId;
    protected HiddenField hddfPlateNew;
    protected HiddenField hddfPlateOld;
    protected HiddenField hddfUserId;
    protected HiddenField hddfProcessTypeSunarp;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.ShowPlateDeliverItem();
        this.trNotaria.Visible = false;
        this.trPartida.Visible = false;
        this.wdpDateDenuncia.Value = DateTime.Now;
        this.wdpConsMTCtacha.Value = DateTime.Now;
        if (this.Request.QueryString["v_NumDoc"] != null)
        {
          int result1 = 0;
          int result2 = 0;
          string empty1 = string.Empty;
          string empty2 = string.Empty;
          string empty3 = string.Empty;
          string str1 = this.Request.QueryString["v_NumDoc"];
          int.TryParse(this.Request.QueryString["i_status"], out result1);
          int.TryParse(this.Request.QueryString["i_chek"], out result2);
          string str2 = this.Request.QueryString["strFecIni"];
          string str3 = this.Request.QueryString["strFecFin"];
          int int32 = Convert.ToInt32(this.Request.QueryString["i_PlateTypeId"].ToString());
          this.hdiNumDoc.Value = str1;
          this.hdiState.Value = result1.ToString((IFormatProvider) CultureInfo.CurrentCulture);
          this.hdiChek.Value = result2.ToString((IFormatProvider) CultureInfo.CurrentCulture);
          this.hdiFecIni.Value = str2;
          this.hdiFenFin.Value = str3;
          this.hdiPlateTypeId.Value = int32.ToString();
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void rblIsHavePlateOld_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        enmHavePlateOld int32_1 = (enmHavePlateOld) Convert.ToInt32(this.rblIsHavePlateOld.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        enmVehicleClasification int32_2 = (enmVehicleClasification) Convert.ToInt32(this.hddfVehicleClasification.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        Convert.ToInt32(this.hddfProcessTypeId.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        this.hddfPlateNew.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        this.hddfPlateOld.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        switch (int32_1)
        {
          case enmHavePlateOld.Si:
            this.DivDatosIntercambio.Visible = true;
            this.lblTituloDatosDenuncia.Visible = false;
            this.divDatosDenuncia.Visible = false;
            this.txtReferencesNumber.Text = string.Empty;
            this.trReferencesNumber.Visible = false;
            this.divConsMTCtacha.Visible = false;
            switch (int32_2)
            {
              case enmVehicleClasification.Auto:
                IEnumerator enumerator1 = this.chklRequisiteExchangeMode.Items.GetEnumerator();
                try
                {
                  while (enumerator1.MoveNext())
                  {
                    ListItem current = (ListItem) enumerator1.Current;
                    if (current.Value != "5" && current.Value != "6")
                    {
                      current.Selected = false;
                      current.Enabled = true;
                    }
                    else
                    {
                      current.Selected = false;
                      current.Enabled = false;
                    }
                  }
                  break;
                }
                finally
                {
                  if (enumerator1 is IDisposable disposable)
                    disposable.Dispose();
                }
              case enmVehicleClasification.Moto:
                this.chklRequisiteExchangeMode.Items[0].Selected = false;
                this.chklRequisiteExchangeMode.Items[0].Enabled = false;
                this.chklRequisiteExchangeMode.Items[1].Selected = false;
                this.chklRequisiteExchangeMode.Items[1].Enabled = true;
                this.chklRequisiteExchangeMode.Items[2].Selected = false;
                this.chklRequisiteExchangeMode.Items[2].Enabled = true;
                this.chklRequisiteExchangeMode.Items[3].Selected = false;
                this.chklRequisiteExchangeMode.Items[3].Enabled = true;
                this.chklRequisiteExchangeMode.Items[4].Selected = false;
                this.chklRequisiteExchangeMode.Items[4].Enabled = false;
                this.chklRequisiteExchangeMode.Items[5].Selected = false;
                this.chklRequisiteExchangeMode.Items[5].Enabled = false;
                break;
            }
            break;
          case enmHavePlateOld.No:
            this.DivDatosIntercambio.Visible = true;
            this.lblTituloDatosDenuncia.Visible = false;
            this.divDatosDenuncia.Visible = false;
            this.trReferencesNumber.Visible = false;
            this.divConsMTCtacha.Visible = false;
            switch (int32_2)
            {
              case enmVehicleClasification.Auto:
                IEnumerator enumerator2 = this.chklRequisiteExchangeMode.Items.GetEnumerator();
                try
                {
                  while (enumerator2.MoveNext())
                  {
                    ListItem current = (ListItem) enumerator2.Current;
                    if (current.Value != "4" && current.Value != "3" && current.Value != "5" && current.Value != "6")
                    {
                      current.Selected = false;
                      current.Enabled = false;
                    }
                    else
                    {
                      current.Selected = false;
                      current.Enabled = true;
                    }
                  }
                  break;
                }
                finally
                {
                  if (enumerator2 is IDisposable disposable)
                    disposable.Dispose();
                }
              case enmVehicleClasification.Moto:
                IEnumerator enumerator3 = this.chklRequisiteExchangeMode.Items.GetEnumerator();
                try
                {
                  while (enumerator3.MoveNext())
                  {
                    ListItem current = (ListItem) enumerator3.Current;
                    if (current.Value != "1" && current.Value != "2")
                    {
                      current.Selected = false;
                      current.Enabled = true;
                    }
                    else
                    {
                      current.Selected = false;
                      current.Enabled = false;
                    }
                  }
                  break;
                }
                finally
                {
                  if (enumerator3 is IDisposable disposable)
                    disposable.Dispose();
                }
            }
            break;
        }
        this.VerificateLocation();
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void rblPersonTypeOwner_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        this.LoadInitial((enmOwnerPersonType) Convert.ToInt32(this.rblPersonTypeOwner.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
        this.DivDatosRecabante.Visible = true;
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void rblTypeRecabante_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        string pstrUseCorrespondence = this.hddfUseCorrespondence.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        int int32_1 = Convert.ToInt32(this.hddfProcessTypeId.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        enmOwnerPersonType int32_2 = (enmOwnerPersonType) Convert.ToInt32(this.rblPersonTypeOwner.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        enmTypeCollect int32_3 = (enmTypeCollect) Convert.ToInt32(this.rblTypeRecabante.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        switch (int32_2)
        {
          case enmOwnerPersonType.Natural:
            if (int32_3 == enmTypeCollect.Propietario)
            {
              this.rblcurrentOption.Visible = true;
              this.rblcurrentOption.Items[0].Selected = true;
              this.rblcurrentOption.Items[1].Selected = false;
              this.rblPersonTypeCollect.Visible = false;
              this.txtNotariaRecabante.Text = string.Empty;
              this.txtPartidaRecabante.Text = string.Empty;
              this.txtNombresRecabante.Text = string.Empty;
              this.trwddDocument.Visible = true;
              this.trwddOwner.Visible = true;
              this.trtxtNombres.Visible = false;
              this.trNotaria.Visible = false;
              this.trPartida.Visible = false;
              this.LoadOwners();
              this.LoadRequisiteToDeliver(int32_1, 1, 1, 1, pstrUseCorrespondence, 0);
              break;
            }
            if (int32_3 != enmTypeCollect.Apoderado)
              break;
            this.rblcurrentOption.Visible = false;
            this.rblPersonTypeCollect.Visible = true;
            this.rblPersonTypeCollect.Enabled = false;
            this.rblPersonTypeCollect.Items[0].Selected = true;
            this.rblPersonTypeCollect.Items[1].Selected = false;
            this.trwddDocument.Visible = false;
            this.trwddOwner.Visible = false;
            this.trtxtNombres.Visible = true;
            this.trNotaria.Visible = true;
            this.trPartida.Visible = false;
            this.wddDocumentDescription.Enabled = true;
            this.wddDocumentDescription.SelectedValue = "-1";
            this.txtNotariaRecabante.Text = string.Empty;
            this.txtPartidaRecabante.Text = string.Empty;
            this.txtNombresRecabante.Text = string.Empty;
            this.txtNombresRecabante.Focus();
            this.txtNumeroRecabante.Text = string.Empty;
            this.txtNumeroRecabante.ReadOnly = false;
            this.trwddDocument.Visible = true;
            this.LoadRequisiteToDeliver(int32_1, 1, 2, 1, pstrUseCorrespondence, 0);
            break;
          case enmOwnerPersonType.Juridica:
            switch ((enmTypeCollect) Convert.ToInt32(this.rblTypeRecabante.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
            {
              case enmTypeCollect.Propietario:
                this.rblcurrentOption.Visible = true;
                this.rblcurrentOption.Items[0].Selected = true;
                this.rblcurrentOption.Items[1].Selected = false;
                this.rblPersonTypeCollect.Visible = false;
                this.txtNotariaRecabante.Text = string.Empty;
                this.txtPartidaRecabante.Text = string.Empty;
                this.txtNombresRecabante.Text = string.Empty;
                this.trwddDocument.Visible = true;
                this.trwddOwner.Visible = true;
                this.trtxtNombres.Visible = false;
                this.trNotaria.Visible = false;
                this.trPartida.Visible = false;
                this.LoadOwners();
                this.LoadRequisiteToDeliver(int32_1, 2, 1, 1, pstrUseCorrespondence, 0);
                return;
              case enmTypeCollect.Apoderado:
                this.rblTypeRecabante.Items[1].Selected = true;
                this.rblcurrentOption.Visible = false;
                this.rblPersonTypeCollect.Visible = true;
                this.rblPersonTypeCollect.Enabled = true;
                this.rblPersonTypeCollect.Items[1].Selected = true;
                this.txtNotariaRecabante.Text = string.Empty;
                this.txtPartidaRecabante.Text = string.Empty;
                this.txtNombresRecabante.Text = string.Empty;
                this.txtNombresRecabante.Focus();
                this.txtNumeroRecabante.Text = string.Empty;
                this.txtNumeroRecabante.ReadOnly = false;
                this.trwddDocument.Visible = false;
                this.trwddOwner.Visible = false;
                this.trtxtNombres.Visible = true;
                this.trNotaria.Visible = true;
                this.trPartida.Visible = true;
                this.wddDocumentDescription.Enabled = true;
                this.wddDocumentDescription.SelectedValue = "-1";
                this.trwddDocument.Visible = true;
                this.LoadRequisiteToDeliver(int32_1, 2, 2, 1, "", 2);
                return;
              default:
                return;
            }
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void rblcurrentOption_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        int int32 = Convert.ToInt32(this.rblcurrentOption.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        switch (int32)
        {
          case 1:
            this.trwddDocument.Visible = true;
            this.trwddOwner.Visible = true;
            this.trtxtNombres.Visible = false;
            this.ChangeCollectType(int32);
            break;
          case 2:
            this.txtNombresRecabante.Focus();
            this.txtNumeroRecabante.ReadOnly = false;
            this.txtNumeroRecabante.Text = "";
            this.trwddOwner.Visible = false;
            this.trwddDocument.Visible = true;
            this.trtxtNombres.Visible = true;
            this.trNotaria.Visible = false;
            this.trPartida.Visible = false;
            this.wddDocumentDescription.Enabled = true;
            this.wddDocumentDescription.SelectedValue = "-1";
            break;
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void rblPersonTypeCollect_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        enmOwnerPersonType int32_1 = (enmOwnerPersonType) Convert.ToInt32(this.rblPersonTypeOwner.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        int int32_2 = Convert.ToInt32(this.hddfProcessTypeId.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        string pstrUseCorrespondence = this.hddfUseCorrespondence.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        enmPersonTypecollect int32_3 = (enmPersonTypecollect) Convert.ToInt32(this.rblPersonTypeCollect.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        switch (int32_1)
        {
          case enmOwnerPersonType.Juridica:
            if (Convert.ToInt32(this.rblTypeRecabante.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture) != 2)
              break;
            switch (int32_3)
            {
              case enmPersonTypecollect.Otros:
                this.trPartida.Visible = true;
                this.trNotaria.Visible = true;
                this.txtNotariaRecabante.Text = string.Empty;
                this.txtPartidaRecabante.Text = string.Empty;
                this.txtNombresRecabante.Text = string.Empty;
                this.txtNombresRecabante.Focus();
                this.txtNumeroRecabante.Text = string.Empty;
                this.txtParte.Text = string.Empty;
                this.LoadRequisiteToDeliver(int32_2, 2, 2, 1, pstrUseCorrespondence, 1);
                break;
              case enmPersonTypecollect.RepresentanteLegal:
                this.trPartida.Visible = true;
                this.trNotaria.Visible = false;
                this.txtNotariaRecabante.Text = string.Empty;
                this.txtPartidaRecabante.Text = string.Empty;
                this.txtNombresRecabante.Text = string.Empty;
                this.txtNombresRecabante.Focus();
                this.txtNumeroRecabante.Text = string.Empty;
                this.txtParte.Text = string.Empty;
                this.LoadRequisiteToDeliver(int32_2, 2, 2, 1, pstrUseCorrespondence, 2);
                break;
            }
            break;
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void chklRequisiteExchangeMode_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        enmVehicleClasification int32_1 = (enmVehicleClasification) Convert.ToInt32(this.hddfVehicleClasification.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        CheckBoxList checkBoxList = (CheckBoxList) sender;
        enmHavePlateOld int32_2 = (enmHavePlateOld) Convert.ToInt32(this.rblIsHavePlateOld.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        int num1 = (int) this.ViewState["vsCounterBlank"];
        int num2 = (int) this.ViewState["PlateOldIsNew"];
        int num3 = this.Session["SystemUser"] != null ? (this.Session["SystemUser"] as SystemUser).i_LocationId : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - PlateDeliveryItem.aspx");
        switch (int32_1)
        {
          case enmVehicleClasification.Auto:
            switch (int32_2)
            {
              case enmHavePlateOld.Si:
                switch (num1)
                {
                  case 1:
                    if (checkBoxList.Items[0].Selected)
                    {
                      checkBoxList.Items[1].Enabled = false;
                      checkBoxList.Items[2].Enabled = false;
                      checkBoxList.Items[3].Enabled = false;
                    }
                    else if (!checkBoxList.Items[1].Enabled && !checkBoxList.Items[2].Enabled && !checkBoxList.Items[3].Enabled)
                    {
                      checkBoxList.Items[1].Enabled = true;
                      checkBoxList.Items[2].Enabled = true;
                      checkBoxList.Items[3].Enabled = num3 == 14;
                    }
                    if (checkBoxList.Items[1].Selected)
                    {
                      if (checkBoxList.Items[0].Enabled && checkBoxList.Items[1].Enabled && checkBoxList.Items[2].Enabled && checkBoxList.Items[3].Enabled == (num3 == 14))
                      {
                        if (num2 == 1 || Convert.ToInt32(this.hddfVehicleClasification.Value, (IFormatProvider) CultureInfo.CurrentCulture) == 2)
                        {
                          checkBoxList.Items[0].Enabled = false;
                          checkBoxList.Items[1].Selected = true;
                          checkBoxList.Items[2].Enabled = false;
                          checkBoxList.Items[3].Enabled = false;
                        }
                        else
                        {
                          checkBoxList.Items[0].Enabled = false;
                          checkBoxList.Items[1].Selected = true;
                          checkBoxList.Items[2].Selected = true;
                          checkBoxList.Items[2].Enabled = false;
                          checkBoxList.Items[3].Enabled = false;
                          this.lblTituloDatosDenuncia.Visible = true;
                          this.divDatosDenuncia.Visible = true;
                          this.txtDependency.Text = string.Empty;
                          this.txtParte.Text = string.Empty;
                          this.txtDependency.Focus();
                        }
                      }
                    }
                    else if (!checkBoxList.Items[0].Enabled && !checkBoxList.Items[3].Enabled && !checkBoxList.Items[2].Enabled)
                    {
                      checkBoxList.Items[0].Enabled = true;
                      checkBoxList.Items[1].Selected = false;
                      checkBoxList.Items[2].Selected = false;
                      checkBoxList.Items[2].Enabled = true;
                      checkBoxList.Items[3].Enabled = num3 == 14;
                    }
                    if (checkBoxList.Items[2].Selected)
                    {
                      if (checkBoxList.Items[0].Enabled && checkBoxList.Items[1].Enabled && checkBoxList.Items[2].Enabled && checkBoxList.Items[3].Enabled == (num3 == 14))
                      {
                        checkBoxList.Items[0].Enabled = false;
                        checkBoxList.Items[1].Enabled = false;
                        this.lblTituloDatosDenuncia.Visible = true;
                        this.divDatosDenuncia.Visible = true;
                        this.txtDependency.Text = string.Empty;
                        this.txtParte.Text = string.Empty;
                        this.txtDependency.Focus();
                        checkBoxList.Items[3].Enabled = false;
                      }
                    }
                    else if (!checkBoxList.Items[0].Enabled && !checkBoxList.Items[1].Enabled && !checkBoxList.Items[3].Enabled)
                    {
                      checkBoxList.Items[0].Enabled = true;
                      checkBoxList.Items[1].Enabled = true;
                      checkBoxList.Items[3].Enabled = num3 == 14;
                      this.lblTituloDatosDenuncia.Visible = false;
                      this.divDatosDenuncia.Visible = false;
                      this.txtDependency.Text = string.Empty;
                      this.txtParte.Text = string.Empty;
                    }
                    if (checkBoxList.Items[3].Selected)
                    {
                      if (checkBoxList.Items[0].Enabled && checkBoxList.Items[1].Enabled && checkBoxList.Items[2].Enabled)
                      {
                        checkBoxList.Items[0].Enabled = false;
                        checkBoxList.Items[1].Enabled = false;
                        checkBoxList.Items[2].Enabled = false;
                        this.trReferencesNumber.Visible = true;
                        this.txtReferencesNumber.Text = string.Empty;
                        this.txtReferencesNumber.Focus();
                        break;
                      }
                      break;
                    }
                    if (!checkBoxList.Items[0].Enabled && !checkBoxList.Items[1].Enabled && !checkBoxList.Items[2].Enabled)
                    {
                      checkBoxList.Items[0].Enabled = true;
                      checkBoxList.Items[1].Enabled = true;
                      checkBoxList.Items[2].Enabled = true;
                      this.trReferencesNumber.Visible = false;
                      this.txtReferencesNumber.Text = string.Empty;
                    }
                    break;
                  case 2:
                    if (checkBoxList.Items[0].Selected)
                    {
                      if (checkBoxList.Items[1].Enabled && checkBoxList.Items[2].Enabled && checkBoxList.Items[3].Enabled == (num3 == 14))
                      {
                        checkBoxList.Items[1].Enabled = false;
                        checkBoxList.Items[2].Enabled = false;
                        checkBoxList.Items[3].Enabled = false;
                      }
                    }
                    else if (!checkBoxList.Items[1].Enabled && !checkBoxList.Items[2].Enabled && !checkBoxList.Items[3].Enabled)
                    {
                      checkBoxList.Items[1].Enabled = true;
                      checkBoxList.Items[2].Enabled = true;
                      checkBoxList.Items[3].Enabled = num3 == 14;
                    }
                    if (checkBoxList.Items[1].Selected)
                    {
                      if (checkBoxList.Items[0].Enabled && checkBoxList.Items[2].Enabled && checkBoxList.Items[3].Enabled == (num3 == 14))
                      {
                        checkBoxList.Items[0].Enabled = false;
                        checkBoxList.Items[1].Selected = true;
                        checkBoxList.Items[2].Selected = true;
                        checkBoxList.Items[2].Enabled = false;
                        this.lblTituloDatosDenuncia.Visible = true;
                        this.divDatosDenuncia.Visible = true;
                        this.txtDependency.Text = string.Empty;
                        this.txtParte.Text = string.Empty;
                        this.txtDependency.Focus();
                        checkBoxList.Items[3].Enabled = false;
                      }
                    }
                    else if (!checkBoxList.Items[0].Enabled && !checkBoxList.Items[2].Enabled && !checkBoxList.Items[3].Enabled)
                    {
                      checkBoxList.Items[0].Enabled = true;
                      checkBoxList.Items[1].Selected = false;
                      checkBoxList.Items[2].Selected = false;
                      checkBoxList.Items[2].Enabled = true;
                      checkBoxList.Items[3].Enabled = num3 == 14;
                      this.lblTituloDatosDenuncia.Visible = false;
                      this.divDatosDenuncia.Visible = false;
                      this.txtDependency.Text = string.Empty;
                      this.txtParte.Text = string.Empty;
                    }
                    if (checkBoxList.Items[2].Selected)
                    {
                      if (checkBoxList.Items[0].Enabled && checkBoxList.Items[1].Enabled && checkBoxList.Items[3].Enabled == (num3 == 14))
                      {
                        checkBoxList.Items[0].Enabled = false;
                        checkBoxList.Items[1].Enabled = false;
                        this.lblTituloDatosDenuncia.Visible = true;
                        this.divDatosDenuncia.Visible = true;
                        this.txtDependency.Text = string.Empty;
                        this.txtParte.Text = string.Empty;
                        this.txtDependency.Focus();
                        checkBoxList.Items[3].Enabled = false;
                      }
                    }
                    else if (!checkBoxList.Items[0].Enabled && !checkBoxList.Items[1].Enabled && !checkBoxList.Items[3].Enabled)
                    {
                      checkBoxList.Items[0].Enabled = true;
                      checkBoxList.Items[1].Enabled = true;
                      checkBoxList.Items[3].Enabled = num3 == 14;
                      this.lblTituloDatosDenuncia.Visible = false;
                      this.divDatosDenuncia.Visible = false;
                      this.txtDependency.Text = string.Empty;
                      this.txtParte.Text = string.Empty;
                    }
                    if (checkBoxList.Items[3].Selected)
                    {
                      if (checkBoxList.Items[0].Enabled && checkBoxList.Items[1].Enabled && checkBoxList.Items[2].Enabled)
                      {
                        checkBoxList.Items[0].Enabled = false;
                        checkBoxList.Items[1].Enabled = false;
                        checkBoxList.Items[2].Enabled = false;
                        this.trReferencesNumber.Visible = true;
                        this.txtReferencesNumber.Text = string.Empty;
                        this.txtReferencesNumber.Focus();
                      }
                    }
                    else if (!checkBoxList.Items[0].Enabled && !checkBoxList.Items[1].Enabled && !checkBoxList.Items[2].Enabled)
                    {
                      checkBoxList.Items[0].Enabled = true;
                      checkBoxList.Items[1].Enabled = true;
                      checkBoxList.Items[2].Enabled = true;
                      this.trReferencesNumber.Visible = false;
                      this.txtReferencesNumber.Text = string.Empty;
                    }
                    break;
                }
                break;
              case enmHavePlateOld.No:
                if (checkBoxList.Items[2].Selected)
                {
                  if (checkBoxList.Items[3].Enabled == (num3 == 14) && checkBoxList.Items[4].Enabled && checkBoxList.Items[5].Enabled)
                  {
                    checkBoxList.Items[3].Selected = false;
                    checkBoxList.Items[3].Enabled = false;
                    checkBoxList.Items[4].Selected = false;
                    checkBoxList.Items[4].Enabled = false;
                    checkBoxList.Items[5].Selected = false;
                    checkBoxList.Items[5].Enabled = false;
                    this.lblTituloDatosDenuncia.Visible = true;
                    this.divDatosDenuncia.Visible = true;
                    this.txtDependency.Text = string.Empty;
                    this.txtParte.Text = string.Empty;
                    this.txtDependency.Focus();
                  }
                }
                else if (!checkBoxList.Items[3].Enabled && !checkBoxList.Items[4].Enabled && !checkBoxList.Items[5].Enabled)
                {
                  checkBoxList.Items[3].Selected = false;
                  checkBoxList.Items[3].Enabled = num3 == 14;
                  checkBoxList.Items[4].Selected = false;
                  checkBoxList.Items[4].Enabled = true;
                  checkBoxList.Items[5].Selected = false;
                  checkBoxList.Items[5].Enabled = true;
                  this.lblTituloDatosDenuncia.Visible = false;
                  this.divDatosDenuncia.Visible = false;
                  this.txtDependency.Text = string.Empty;
                  this.txtParte.Text = string.Empty;
                }
                if (checkBoxList.Items[3].Selected)
                {
                  if (checkBoxList.Items[2].Enabled && checkBoxList.Items[4].Enabled && checkBoxList.Items[5].Enabled)
                  {
                    checkBoxList.Items[2].Selected = false;
                    checkBoxList.Items[2].Enabled = false;
                    checkBoxList.Items[4].Selected = false;
                    checkBoxList.Items[4].Enabled = false;
                    checkBoxList.Items[5].Selected = false;
                    checkBoxList.Items[5].Enabled = false;
                    this.trReferencesNumber.Visible = true;
                    this.txtReferencesNumber.Text = string.Empty;
                    this.txtReferencesNumber.Focus();
                  }
                }
                else if (!checkBoxList.Items[2].Enabled && !checkBoxList.Items[4].Enabled && !checkBoxList.Items[5].Enabled)
                {
                  checkBoxList.Items[2].Selected = false;
                  checkBoxList.Items[2].Enabled = true;
                  checkBoxList.Items[4].Selected = false;
                  checkBoxList.Items[4].Enabled = true;
                  checkBoxList.Items[5].Selected = false;
                  checkBoxList.Items[5].Enabled = true;
                  this.trReferencesNumber.Visible = false;
                  this.txtReferencesNumber.Text = string.Empty;
                }
                if (checkBoxList.Items[4].Selected)
                {
                  if (checkBoxList.Items[2].Enabled && checkBoxList.Items[3].Enabled == (num3 == 14) && checkBoxList.Items[5].Enabled)
                  {
                    checkBoxList.Items[2].Selected = false;
                    checkBoxList.Items[2].Enabled = false;
                    checkBoxList.Items[3].Selected = false;
                    checkBoxList.Items[3].Enabled = false;
                    checkBoxList.Items[5].Selected = false;
                    checkBoxList.Items[5].Enabled = false;
                    this.divConsMTCtacha.Visible = true;
                    this.lblNroPlacaTachada.Visible = false;
                    this.txtNroPlacaTachada.Visible = false;
                    this.txtReferMTCtacha.Focus();
                  }
                }
                else if (!checkBoxList.Items[2].Enabled && !checkBoxList.Items[3].Enabled && !checkBoxList.Items[5].Enabled)
                {
                  checkBoxList.Items[2].Selected = false;
                  checkBoxList.Items[2].Enabled = true;
                  checkBoxList.Items[3].Selected = false;
                  checkBoxList.Items[3].Enabled = num3 == 14;
                  checkBoxList.Items[5].Selected = false;
                  checkBoxList.Items[5].Enabled = true;
                  this.divConsMTCtacha.Visible = false;
                  this.lblNroPlacaTachada.Visible = false;
                  this.txtNroPlacaTachada.Visible = false;
                }
                if (checkBoxList.Items[5].Selected)
                {
                  if (checkBoxList.Items[2].Enabled && checkBoxList.Items[3].Enabled == (num3 == 14) && checkBoxList.Items[4].Enabled)
                  {
                    checkBoxList.Items[2].Selected = false;
                    checkBoxList.Items[2].Enabled = false;
                    checkBoxList.Items[3].Selected = false;
                    checkBoxList.Items[3].Enabled = false;
                    checkBoxList.Items[4].Selected = false;
                    checkBoxList.Items[4].Enabled = false;
                    this.divConsMTCtacha.Visible = true;
                    this.lblNroPlacaTachada.Visible = true;
                    this.txtNroPlacaTachada.Visible = true;
                    this.txtReferMTCtacha.Focus();
                    break;
                  }
                  break;
                }
                if (!checkBoxList.Items[2].Enabled && !checkBoxList.Items[3].Enabled && !checkBoxList.Items[4].Selected)
                {
                  checkBoxList.Items[2].Selected = false;
                  checkBoxList.Items[2].Enabled = true;
                  checkBoxList.Items[3].Selected = false;
                  checkBoxList.Items[3].Enabled = num3 == 14;
                  checkBoxList.Items[4].Selected = false;
                  checkBoxList.Items[4].Enabled = true;
                  this.divConsMTCtacha.Visible = false;
                  this.lblNroPlacaTachada.Visible = false;
                  this.txtNroPlacaTachada.Visible = false;
                }
                break;
            }
            break;
          case enmVehicleClasification.Moto:
            switch (int32_2)
            {
              case enmHavePlateOld.Si:
                if (checkBoxList.Items[1].Selected)
                {
                  checkBoxList.Items[0].Enabled = false;
                  checkBoxList.Items[2].Enabled = false;
                  checkBoxList.Items[3].Enabled = false;
                }
                else if (!checkBoxList.Items[0].Enabled && !checkBoxList.Items[2].Enabled && !checkBoxList.Items[3].Enabled)
                {
                  checkBoxList.Items[0].Enabled = false;
                  checkBoxList.Items[2].Enabled = true;
                  checkBoxList.Items[3].Enabled = num3 == 14;
                }
                if (checkBoxList.Items[2].Selected)
                {
                  if (!checkBoxList.Items[0].Enabled && checkBoxList.Items[1].Enabled && checkBoxList.Items[2].Enabled && checkBoxList.Items[3].Enabled == (num3 == 14))
                  {
                    checkBoxList.Items[0].Enabled = false;
                    checkBoxList.Items[1].Enabled = false;
                    checkBoxList.Items[3].Enabled = false;
                    this.lblTituloDatosDenuncia.Visible = true;
                    this.divDatosDenuncia.Visible = true;
                    this.txtDependency.Text = string.Empty;
                    this.txtParte.Text = string.Empty;
                    this.txtDependency.Focus();
                  }
                }
                else if (!checkBoxList.Items[0].Enabled && !checkBoxList.Items[1].Enabled && !checkBoxList.Items[3].Enabled)
                {
                  checkBoxList.Items[0].Enabled = false;
                  checkBoxList.Items[1].Enabled = true;
                  checkBoxList.Items[3].Enabled = num3 == 14;
                  this.lblTituloDatosDenuncia.Visible = false;
                  this.divDatosDenuncia.Visible = false;
                  this.txtDependency.Text = string.Empty;
                  this.txtParte.Text = string.Empty;
                }
                if (checkBoxList.Items[3].Selected)
                {
                  checkBoxList.Items[0].Enabled = false;
                  checkBoxList.Items[1].Enabled = false;
                  checkBoxList.Items[2].Enabled = false;
                  this.trReferencesNumber.Visible = true;
                  this.txtReferencesNumber.Text = string.Empty;
                  this.txtReferencesNumber.Focus();
                  break;
                }
                if (!checkBoxList.Items[0].Enabled && !checkBoxList.Items[1].Enabled && !checkBoxList.Items[2].Enabled)
                {
                  checkBoxList.Items[0].Enabled = false;
                  checkBoxList.Items[1].Enabled = true;
                  checkBoxList.Items[2].Enabled = true;
                  this.trReferencesNumber.Visible = false;
                  this.txtReferencesNumber.Text = string.Empty;
                }
                break;
              case enmHavePlateOld.No:
                if (checkBoxList.Items[2].Selected)
                {
                  if (!checkBoxList.Items[0].Enabled && !checkBoxList.Items[1].Enabled && checkBoxList.Items[2].Enabled && checkBoxList.Items[3].Enabled == (num3 == 14) && checkBoxList.Items[4].Enabled && checkBoxList.Items[5].Enabled)
                  {
                    checkBoxList.Items[0].Selected = false;
                    checkBoxList.Items[0].Enabled = false;
                    checkBoxList.Items[1].Selected = false;
                    checkBoxList.Items[1].Enabled = false;
                    checkBoxList.Items[3].Selected = false;
                    checkBoxList.Items[3].Enabled = false;
                    checkBoxList.Items[4].Selected = false;
                    checkBoxList.Items[4].Enabled = false;
                    checkBoxList.Items[5].Selected = false;
                    checkBoxList.Items[5].Enabled = false;
                    this.lblTituloDatosDenuncia.Visible = true;
                    this.divDatosDenuncia.Visible = true;
                    this.txtDependency.Text = string.Empty;
                    this.txtParte.Text = string.Empty;
                    this.txtDependency.Focus();
                  }
                }
                else if (!checkBoxList.Items[0].Enabled && !checkBoxList.Items[1].Enabled && !checkBoxList.Items[3].Enabled && !checkBoxList.Items[4].Enabled && !checkBoxList.Items[5].Enabled)
                {
                  checkBoxList.Items[0].Selected = false;
                  checkBoxList.Items[0].Enabled = false;
                  checkBoxList.Items[1].Selected = false;
                  checkBoxList.Items[1].Enabled = false;
                  checkBoxList.Items[3].Selected = false;
                  checkBoxList.Items[3].Enabled = num3 == 14;
                  checkBoxList.Items[4].Selected = false;
                  checkBoxList.Items[4].Enabled = true;
                  checkBoxList.Items[5].Selected = false;
                  checkBoxList.Items[5].Enabled = true;
                  this.lblTituloDatosDenuncia.Visible = false;
                  this.divDatosDenuncia.Visible = false;
                  this.txtDependency.Text = string.Empty;
                  this.txtParte.Text = string.Empty;
                }
                if (checkBoxList.Items[3].Selected)
                {
                  if (checkBoxList.Items[2].Enabled && checkBoxList.Items[4].Enabled && checkBoxList.Items[5].Enabled)
                  {
                    checkBoxList.Items[0].Selected = false;
                    checkBoxList.Items[0].Enabled = false;
                    checkBoxList.Items[1].Selected = false;
                    checkBoxList.Items[1].Enabled = false;
                    checkBoxList.Items[2].Selected = false;
                    checkBoxList.Items[2].Enabled = false;
                    checkBoxList.Items[4].Selected = false;
                    checkBoxList.Items[4].Enabled = false;
                    checkBoxList.Items[5].Selected = false;
                    checkBoxList.Items[5].Enabled = false;
                    this.trReferencesNumber.Visible = true;
                    this.txtReferencesNumber.Text = string.Empty;
                    this.txtReferencesNumber.Focus();
                  }
                }
                else if (!checkBoxList.Items[0].Enabled && !checkBoxList.Items[1].Enabled && !checkBoxList.Items[2].Enabled && !checkBoxList.Items[4].Enabled && !checkBoxList.Items[5].Enabled)
                {
                  checkBoxList.Items[0].Selected = false;
                  checkBoxList.Items[0].Enabled = false;
                  checkBoxList.Items[1].Selected = false;
                  checkBoxList.Items[1].Enabled = false;
                  checkBoxList.Items[2].Selected = false;
                  checkBoxList.Items[2].Enabled = true;
                  checkBoxList.Items[4].Selected = false;
                  checkBoxList.Items[4].Enabled = true;
                  checkBoxList.Items[5].Selected = false;
                  checkBoxList.Items[5].Enabled = true;
                  this.trReferencesNumber.Visible = false;
                  this.txtReferencesNumber.Text = string.Empty;
                }
                if (checkBoxList.Items[4].Selected)
                {
                  if (checkBoxList.Items[2].Enabled && checkBoxList.Items[3].Enabled == (num3 == 14) && checkBoxList.Items[5].Enabled)
                  {
                    checkBoxList.Items[2].Selected = false;
                    checkBoxList.Items[2].Enabled = false;
                    checkBoxList.Items[3].Selected = false;
                    checkBoxList.Items[3].Enabled = false;
                    checkBoxList.Items[5].Selected = false;
                    checkBoxList.Items[5].Enabled = false;
                  }
                  this.divConsMTCtacha.Visible = true;
                  this.lblNroPlacaTachada.Visible = false;
                  this.txtNroPlacaTachada.Visible = false;
                  this.txtReferMTCtacha.Focus();
                }
                else if (!checkBoxList.Items[2].Enabled && !checkBoxList.Items[3].Enabled && !checkBoxList.Items[5].Enabled)
                {
                  checkBoxList.Items[2].Selected = false;
                  checkBoxList.Items[2].Enabled = true;
                  checkBoxList.Items[3].Selected = false;
                  checkBoxList.Items[3].Enabled = num3 == 14;
                  checkBoxList.Items[5].Selected = false;
                  checkBoxList.Items[5].Enabled = true;
                  this.divConsMTCtacha.Visible = false;
                  this.lblNroPlacaTachada.Visible = false;
                  this.txtNroPlacaTachada.Visible = false;
                }
                if (checkBoxList.Items[5].Selected)
                {
                  if (checkBoxList.Items[2].Enabled && checkBoxList.Items[3].Enabled == (num3 == 14) && checkBoxList.Items[4].Enabled)
                  {
                    checkBoxList.Items[2].Selected = false;
                    checkBoxList.Items[2].Enabled = false;
                    checkBoxList.Items[3].Selected = false;
                    checkBoxList.Items[3].Enabled = false;
                    checkBoxList.Items[4].Selected = false;
                    checkBoxList.Items[4].Enabled = false;
                  }
                  this.divConsMTCtacha.Visible = true;
                  this.lblNroPlacaTachada.Visible = true;
                  this.txtNroPlacaTachada.Visible = true;
                  this.txtReferMTCtacha.Focus();
                  break;
                }
                if (!checkBoxList.Items[2].Enabled && !checkBoxList.Items[3].Enabled && !checkBoxList.Items[4].Selected)
                {
                  checkBoxList.Items[2].Selected = false;
                  checkBoxList.Items[2].Enabled = true;
                  checkBoxList.Items[3].Selected = false;
                  checkBoxList.Items[3].Enabled = num3 == 14;
                  checkBoxList.Items[4].Selected = false;
                  checkBoxList.Items[4].Enabled = true;
                  this.divConsMTCtacha.Visible = false;
                  this.lblNroPlacaTachada.Visible = false;
                  this.txtNroPlacaTachada.Visible = false;
                }
                break;
            }
            break;
        }
        this.VerificateLocation();
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void wddOwner_SelectionChanged(object sender, EventArgs e)
    {
      try
      {
        string[] strArray1 = this.ViewState["vsOwnerDocumentNumber"] as string[];
        string[] strArray2 = this.ViewState["vsOwnerDocumentType"] as string[];
        try
        {
          this.wddDocumentDescription.SelectedValue = this.wddOwner.SelectedIndex.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        }
        catch
        {
          this.wddDocumentDescription.SelectedIndex = -1;
        }
        this.txtNumeroRecabante.Text = strArray1[this.wddOwner.SelectedIndex].ToString((IFormatProvider) CultureInfo.CurrentCulture);
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void dlProductComposition_ItemCommand(object source, DataListCommandEventArgs e)
    {
      if (!(e.CommandName == "Select"))
        return;
      HtmlInputCheckBox control = (HtmlInputCheckBox) e.Item.FindControl("htmlchkItem");
      control.Checked = !control.Checked;
    }

    protected void wibFinalize_Click(object sender, EventArgs e)
    {
      try
      {
        this.hddfPlateOld.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        int int32_1 = Convert.ToInt32(this.hddfProcessTypeId.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        this.IsValidSelectProductComposition();
        if (this.rblIsHavePlateOld.Enabled && this.rblIsHavePlateOld.SelectedIndex == -1)
          throw new HandledException(1, "Datos de Intercambio : <br>__________________<br>Elija una opción si tiene placas antiguas.");
        this.IsValidInputDataExchange();
        if (this.rblPersonTypeOwner.SelectedIndex == -1)
          throw new HandledException(1, "Datos del Recabante : <br>__________________<br>Seleccione Tipo de Propietario [Natural / Jurídica]");
        int int32_2 = Convert.ToInt32(this.rblPersonTypeOwner.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        this.IsValidInputDataCollect();
        this.IsValidInputDataNewOwner();
        this.IsValidRequiredDocuments(int32_1, int32_2);
        if (this.ViewState["i_IsClaim"].ToString() == "0" && new PlateDeliverQueriesBL().VerifyStatusRequirementPlate(Convert.ToInt32(this.txtRequirementId.Text, (IFormatProvider) CultureInfo.CurrentCulture), 6) == 1)
          throw new HandledException(1, "El Proceso de Entrega Actual ya fué Grabado.");
        this.RegisterDeliveryProcess();
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void wibClain_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable dataTable = this.ViewState["vsdtPlateDeliverItem"] as DataTable;
        string empty = string.Empty;
        string[] strArray1 = dataTable.Rows[0]["v_OwnerCompleteName"].ToString().Split('/');
        string[] strArray2 = dataTable.Rows[0]["v_OwnerDocumentNumber"].ToString().Trim().Split('/');
        string[] strArray3 = dataTable.Rows[0]["v_OwnerDocumentType"].ToString().Trim().Split('/');
        this.CreatePopUp("Reclamo por producto", new ClaimGenerator().GenerateClaim_ProductNoAgree(dataTable.Rows[0]["i_RequirementPlateId"].ToString(), dataTable.Rows[0]["v_PlateNew"].ToString(), dataTable.Rows[0]["i_ProcessTypeId"].ToString(), dataTable.Rows[0]["i_ProductId"].ToString(), string.Empty, strArray1[strArray1.Length - 1], string.Empty, strArray3[strArray3.Length - 1], strArray2[strArray2.Length - 1], string.Empty, string.Empty, 0), "760px", "670px");
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void wibCancel_Click(object sender, EventArgs e)
    {
      try
      {
        this.Response.Redirect("PlateDeliveryManagement.aspx?v_NumDoc=" + this.hdiNumDoc.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&i_status=" + this.hdiState.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&i_chek=" + this.hdiChek.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&strFecIni=" + this.hdiFecIni.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&strFecFin=" + this.hdiFenFin.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&t=" + this.hdiPlateTypeId.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void wibAceptar_Click(object sender, EventArgs e)
    {
      try
      {
        this.Response.Redirect("PlateDeliveryManagement.aspx?t=" + this.hdiPlateTypeId.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void ShowPlateDeliverItem()
    {
      try
      {
        if (this.Request.QueryString["Ids"] != null)
        {
          DataTable plateDeliverItemBy = new PlateDeliverQueriesBL().GetPlateDeliverItemBy("", Convert.ToInt32(this.Request.QueryString["Ids"], (IFormatProvider) CultureInfo.CurrentCulture));
          if (plateDeliverItemBy == null || plateDeliverItemBy.Rows.Count == 0)
            throw new HandledException(1, "No se encontró información para realizar la entrega.");
          if (Convert.ToInt32(plateDeliverItemBy.Rows[0]["i_TrxId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture) == 0)
            Message.SetMessage(this.lblMessage, new HandledException(1, "Esta solicitud no cuenta con conciliación bancaria."));
          int int32 = Convert.ToInt32(plateDeliverItemBy.Rows[0]["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture);
          int pintRequirementPlateId = (int) plateDeliverItemBy.Rows[0]["i_RequirementPlateId"];
          int intProcessTypeId = (int) plateDeliverItemBy.Rows[0]["i_ProcessTypeId"];
          string str1 = plateDeliverItemBy.Rows[0]["v_PlateNew"].ToString();
          string pstrPlateOld = plateDeliverItemBy.Rows[0]["v_PlateOld"].ToString();
          plateDeliverItemBy.Rows[0]["v_PlateMotive"].ToString();
          plateDeliverItemBy.Rows[0]["v_UseType"].ToString();
          string str2 = plateDeliverItemBy.Rows[0]["v_ProcessTypeSunarp"].ToString();
          int num1 = (int) plateDeliverItemBy.Rows[0]["i_VehicleClasification"];
          string[] strArray1 = plateDeliverItemBy.Rows[0]["v_OwnerCompleteName"].ToString().Split('/');
          string[] strArray2 = plateDeliverItemBy.Rows[0]["v_OwnerDocumentDescription"].ToString().Trim().Split('/');
          string[] strArray3 = plateDeliverItemBy.Rows[0]["v_OwnerDocumentNumber"].ToString().Trim().Split('/');
          string[] strArray4;
          if (plateDeliverItemBy.Rows[0]["v_OwnerDocumentType"].ToString().Contains<char>('/'))
            strArray4 = plateDeliverItemBy.Rows[0]["v_OwnerDocumentType"].ToString().Trim().Split('/');
          else if (string.IsNullOrEmpty(plateDeliverItemBy.Rows[0]["v_OwnerDocumentType"].ToString()))
            strArray4 = new string[1]
            {
              plateDeliverItemBy.Rows[0]["v_OwnerDocumentType"].ToString().Trim()
            };
          else
            strArray4 = plateDeliverItemBy.Rows[0]["v_OwnerDocumentType"].ToString().Trim().Split('/');
          string empty = string.Empty;
          DataTable plateOldAsPlateNewBy1 = new PlateDeliverQueriesBL().GetPlateDeliverSearchPlateOldAsPlateNewBy(str1, pintRequirementPlateId);
          if (plateOldAsPlateNewBy1.Rows.Count > 0)
            empty = plateOldAsPlateNewBy1.Rows[0]["v_PlateOldVRD"].ToString();
          this.txtRequirementId.Text = pintRequirementPlateId.ToString((IFormatProvider) CultureInfo.CurrentCulture);
          this.txtContributor.Text = plateDeliverItemBy.Rows[0]["v_CompleteName"].ToString();
          this.txtTramite.Text = plateDeliverItemBy.Rows[0]["v_ProcessTypeSunarp"].ToString();
          this.txtPlateNew.Text = str1;
          this.txtTitle.Text = plateDeliverItemBy.Rows[0]["v_TitleNumber"].ToString();
          this.txtRequirementType.Text = plateDeliverItemBy.Rows[0]["v_RequirementType"].ToString();
          this.txtPlateOld.Text = empty.Equals(string.Empty) ? pstrPlateOld : empty;
          this.txtBrand.Text = plateDeliverItemBy.Rows[0]["v_Brand"].ToString();
          this.txtModel.Text = plateDeliverItemBy.Rows[0]["v_Model"].ToString();
          this.txtVehicleTypeUse.Text = plateDeliverItemBy.Rows[0]["v_VehicleTypeUse"].ToString();
          this.txtVehicleCategory.Text = plateDeliverItemBy.Rows[0]["v_VehicleCategory"].ToString();
          this.txtSerialNumber.Text = plateDeliverItemBy.Rows[0]["v_SerialNumber"].ToString();
          this.txtOwnerCompleteName.Text = strArray1[strArray1.Length - 1];
          this.txtOwnerDocumentType.Text = strArray2[strArray2.Length - 1];
          this.txtOwnerDocumentNumber.Text = strArray3[strArray3.Length - 1];
          this.txtCode.Text = plateDeliverItemBy.Rows[0]["v_Code"].ToString();
          this.ViewState["vsdtPlateDeliverItem"] = (object) plateDeliverItemBy;
          DataTable dataTable = new PlateDeliverQueriesBL().GetPlateDeliverProductConpositionBy(int32);
          if ((enmRequirementPlateType) plateDeliverItemBy.Rows[0]["i_RequirementPlateTypeId"] == enmRequirementPlateType.Premium)
            dataTable = new DataView(dataTable, "i_ComponentId <> 109", "", DataViewRowState.CurrentRows).ToTable();
          this.dlProductComposition.DataSource = (object) dataTable;
          this.dlProductComposition.DataBind();
          int num2 = 0;
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (Convert.ToInt32(row["i_CategoryId"], (IFormatProvider) CultureInfo.CurrentCulture) == 3)
              ++num2;
          }
          this.ViewState["vsCounterBlank"] = (object) num2;
          string str3 = plateDeliverItemBy.Rows[0]["v_UseCorrespondence"].ToString();
          if (0U > 0U)
          {
            int num3 = strArray4[strArray4.Length - 1].Trim() == "1" ? 1 : 2;
          }
          this.ViewState["vsOwners"] = (object) strArray1;
          this.ViewState["vsOwnerDocumentNumber"] = (object) strArray3;
          this.ViewState["vsOwnerDocumentType"] = (object) strArray4;
          this.hddfProcessTypeId.Value = intProcessTypeId.ToString((IFormatProvider) CultureInfo.CurrentCulture);
          this.hddfUseCorrespondence.Value = str3;
          this.hddfRequirementPlateId.Value = pintRequirementPlateId.ToString((IFormatProvider) CultureInfo.CurrentCulture);
          this.hddfPlateNew.Value = str1;
          this.hddfPlateOld.Value = pstrPlateOld;
          this.hddfUserId.Value = ((SystemUser) this.Session["SystemUser"]).i_SystemUserId.ToString((IFormatProvider) CultureInfo.CurrentCulture);
          this.hddfProcessTypeSunarp.Value = str2;
          this.hddfVehicleClasification.Value = num1.ToString((IFormatProvider) CultureInfo.CurrentCulture);
          if (!string.IsNullOrEmpty(pstrPlateOld) && (intProcessTypeId == 2 || intProcessTypeId == 3 || intProcessTypeId == 5))
          {
            if (str1.ToUpper(CultureInfo.CurrentCulture).Trim() != pstrPlateOld.ToUpper(CultureInfo.CurrentCulture).Trim())
            {
              this.SetPlateExchange(true, false, false, false, string.Empty, false);
            }
            else
            {
              DataTable plateOldAsPlateNewBy2 = new PlateDeliverQueriesBL().GetPlateDeliverSearchPlateOldAsPlateNewBy(str1, pintRequirementPlateId);
              if (plateOldAsPlateNewBy2.Rows.Count > 0)
              {
                this.i_ProductIdOld = Convert.ToInt32(plateOldAsPlateNewBy2.Rows[0]["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture);
                if (int32 != this.i_ProductIdOld)
                  this.SetPlateExchange(true, false, false, false, string.Empty, false);
                else
                  this.SetPlateExchange(false, false, false, false, string.Empty, false);
              }
              else
                this.SetPlateExchange(false, false, false, false, string.Empty, false);
            }
          }
          this.LoadPersonDocumentType();
          this.SetProductImagen(dataTable, str1, 0.0f, 0.0f);
          this.showPlateOld(intProcessTypeId, pintRequirementPlateId, str1, pstrPlateOld, empty, int32, this.i_ProductIdOld);
          this.LoadExchangeMode();
        }
        if (this.Request.QueryString["i_IsClaim"] != null)
          this.ViewState["i_IsClaim"] = (object) this.Request.QueryString["i_IsClaim"];
        if (this.Request.QueryString["d_DeliveryDate"] == null)
          return;
        this.ViewState["d_DeliveryDate"] = (object) this.Request.QueryString["d_DeliveryDate"];
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SetPlateExchange(
      bool IsHavePlateOld,
      bool DatosIntercambio,
      bool TituloDatosDenuncia,
      bool DatosDenuncia,
      string ReferencesNumber,
      bool tr_ReferencesNumber)
    {
      try
      {
        this.rblIsHavePlateOld.Enabled = IsHavePlateOld;
        this.DivDatosIntercambio.Visible = DatosIntercambio;
        this.lblTituloDatosDenuncia.Visible = TituloDatosDenuncia;
        this.divDatosDenuncia.Visible = DatosDenuncia;
        this.txtReferencesNumber.Text = ReferencesNumber;
        this.trReferencesNumber.Visible = tr_ReferencesNumber;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void LoadPersonDocumentType()
    {
      try
      {
        DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) ("" + SystemParameterGroups.PersonDocumentType.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
          (object) "1,4,3,2,32,19,33",
          (object) "",
          (object) ""
        });
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (row["i_GroupId"].ToString() == SystemParameterGroups.PersonDocumentType.ToString((IFormatProvider) CultureInfo.CurrentCulture))
              this.wddDocumentDescription.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
          }
        }
        this.wddDocumentDescription.Items.Insert(0, new ListItem("- Seleccione - ", "-1"));
        this.wddDocumentDescription.SelectedValue = "-1";
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void LoadExchangeMode()
    {
      try
      {
        DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) ("" + SystemParameterGroups.ExchangeMode.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
          (object) "",
          (object) "1",
          (object) "1"
        });
        if (dataTable == null)
          return;
        this.chklRequisiteExchangeMode.DataSource = (object) dataTable;
        this.chklRequisiteExchangeMode.DataTextField = "v_Description";
        this.chklRequisiteExchangeMode.DataValueField = "i_ParameterId";
        this.chklRequisiteExchangeMode.DataBind();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SetProductImagen(
      DataTable dt,
      string strTitle,
      float fltFuentePosX,
      float fltFuentePosY)
    {
      try
      {
        System.Web.UI.WebControls.Image image = new System.Web.UI.WebControls.Image();
        for (int index = 0; index < dt.Rows.Count; ++index)
        {
          string pstrPkImagen = dt.Rows[index]["i_ProductId"].ToString() + dt.Rows[index]["i_ComponentId"].ToString() + dt.Rows[index]["i_Index"].ToString();
          ((System.Web.UI.WebControls.Image) this.dlProductComposition.Items[index].FindControl("ImgProductComposition")).ImageUrl = "~\\UserControls\\GetImageText.ashx?" + PlateDeliveryItemOld.getParameterRequest("imgDeliverPC", "110", " ", "", "", "", "30", "30", pstrPkImagen);
          if (index == 0)
            this.imgbtnPlateRecovery.ImageUrl = "~\\UserControls\\GetImageText.ashx?" + PlateDeliveryItemOld.getParameterRequest("imgDeliverPC", "110", strTitle, "Arial Black", "Black", "9", "30", "20", pstrPkImagen);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void showPlateOld(
      int intProcessTypeId,
      int pintRequirementPlateId,
      string strPlateNew,
      string pstrPlateOld,
      string strPlateOldVRD,
      int pintProductId,
      int pintProductIdOld)
    {
      try
      {
        string empty = string.Empty;
        this.ViewState["PlateOldIsNew"] = (object) -1;
        if (this.txtTitle.Text.Contains("AAP"))
        {
          this.ViewState["PlateOldIsNew"] = (object) 1;
          switch (intProcessTypeId)
          {
            case 4:
              this.ViewState["PlateOldIsNew"] = (object) -1;
              this.imgbtnPlateRecovery.ImageUrl = "~/Images/Requirement/TerceraPlaca.gif";
              break;
            case 6:
              string pstrPkImagen = strPlateNew.PadLeft(10, ' ') + pintRequirementPlateId.ToString((IFormatProvider) CultureInfo.CurrentCulture);
              this.imgbtnPlateRecovery.ImageUrl = "~\\UserControls\\GetImageText.ashx?" + PlateDeliveryItemOld.getParameterRequest("imgDeliverValid", "110", strPlateNew, "Arial Black", "Black", "9", "30", "20", pstrPkImagen);
              break;
          }
        }
        else if (intProcessTypeId == 2 || intProcessTypeId == 3)
        {
          if (strPlateNew.Replace("-", "") != pstrPlateOld)
          {
            DataTable plateOldAsPlateNewBy = new PlateDeliverQueriesBL().GetPlateDeliverSearchPlateOldAsPlateNewBy(strPlateNew, pintRequirementPlateId);
            if (plateOldAsPlateNewBy.Rows.Count > 0)
            {
              this.ViewState["PlateOldIsNew"] = (object) 1;
              this.imgbtnPlateRecovery.Attributes["src"] = this.ShowBinaryImage((byte[]) plateOldAsPlateNewBy.Rows[0]["g_Image"], strPlateOldVRD, 120, 35f, 30f);
            }
            else
            {
              this.ViewState["PlateOldIsNew"] = (object) 0;
              this.imgbtnPlateRecovery.Attributes["src"] = this.ShowBinaryImage(this.ImageToBytes(System.Drawing.Image.FromFile(this.Server.MapPath("~/Images/Requirement/placas/Placa_Antigua.png"))), pstrPlateOld, 120, 35f, 30f);
            }
          }
          else if (pintProductId != pintProductIdOld && pintProductIdOld != 0)
          {
            DataTable plateOldAsPlateNewBy = new PlateDeliverQueriesBL().GetPlateDeliverSearchPlateOldAsPlateNewBy(strPlateNew, pintRequirementPlateId);
            if (plateOldAsPlateNewBy.Rows.Count > 0)
            {
              this.ViewState["PlateOldIsNew"] = (object) 1;
              this.imgbtnPlateRecovery.Attributes["src"] = this.ShowBinaryImage((byte[]) plateOldAsPlateNewBy.Rows[0]["g_Image"], strPlateOldVRD, 120, 35f, 30f);
            }
            else
            {
              this.ViewState["PlateOldIsNew"] = (object) 0;
              this.imgbtnPlateRecovery.Attributes["src"] = this.ShowBinaryImage(this.ImageToBytes(System.Drawing.Image.FromFile(this.Server.MapPath("~/Images/Requirement/placas/Placa_Antigua.png"))), pstrPlateOld, 120, 35f, 30f);
            }
          }
          else
          {
            this.Session["PlateOldIsNew"] = (object) -1;
            this.imgbtnPlateRecovery.ImageUrl = "~/Images/Requirement/placas/sinplaca.png";
          }
        }
        else if (intProcessTypeId != 2 && intProcessTypeId != 3 && intProcessTypeId != 5)
        {
          DataTable plateOldAsPlateNewBy = new PlateDeliverQueriesBL().GetPlateDeliverSearchPlateOldAsPlateNewBy(strPlateNew, pintRequirementPlateId);
          if (plateOldAsPlateNewBy.Rows.Count > 0)
          {
            this.ViewState["PlateOldIsNew"] = (object) 1;
            this.imgbtnPlateRecovery.Attributes["src"] = this.ShowBinaryImage((byte[]) plateOldAsPlateNewBy.Rows[0]["g_Image"], strPlateOldVRD, 120, 35f, 30f);
          }
          else
          {
            this.ViewState["PlateOldIsNew"] = (object) 0;
            this.imgbtnPlateRecovery.Attributes["src"] = this.ShowBinaryImage(this.ImageToBytes(System.Drawing.Image.FromFile(this.Server.MapPath("~/Images/Requirement/placas/Placa_Antigua.png"))), pstrPlateOld, 120, 35f, 30f);
          }
        }
        else
        {
          if (intProcessTypeId != 5)
            return;
          this.Session["PlateOldIsNew"] = (object) -1;
          this.imgbtnPlateRecovery.ImageUrl = "~/Images/Requirement/placas/sinplaca.png";
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private static string getParameterRequest(
      string pstrType,
      string pstrWidht,
      string pstrMessage,
      string pstrFontFamily,
      string pstrFontColor,
      string pstrFontSize,
      string pstrPosX,
      string pstrPosY,
      string pstrPkImagen)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (pstrType.Length > 0)
      {
        stringBuilder.Append("type=");
        stringBuilder.Append(HttpUtility.UrlEncode(pstrType));
        stringBuilder.Append("&");
      }
      if (pstrPkImagen.Length > 0)
      {
        stringBuilder.Append("pkImagen=");
        stringBuilder.Append(HttpUtility.UrlEncode(pstrPkImagen));
        stringBuilder.Append("&");
      }
      if (pstrWidht.Length > 0)
      {
        stringBuilder.Append("ancho=");
        stringBuilder.Append(pstrWidht);
        stringBuilder.Append("&");
      }
      if (pstrMessage.Length > 0)
      {
        stringBuilder.Append("t=");
        stringBuilder.Append(HttpUtility.UrlEncode(pstrMessage));
        stringBuilder.Append("&");
      }
      if (pstrFontFamily.Length > 0)
      {
        stringBuilder.Append("ff=");
        stringBuilder.Append(HttpUtility.UrlEncode(pstrFontFamily));
        stringBuilder.Append("&");
      }
      if (pstrFontColor.Length > 0)
      {
        stringBuilder.Append("fc=");
        stringBuilder.Append(HttpUtility.UrlEncode(pstrFontColor));
        stringBuilder.Append("&");
      }
      if (pstrFontSize.Length > 0)
      {
        stringBuilder.Append("fs=");
        stringBuilder.Append(HttpUtility.UrlEncode(pstrFontSize));
        stringBuilder.Append("&");
      }
      if (pstrPosX.Length > 0)
      {
        stringBuilder.Append("fx=");
        stringBuilder.Append(HttpUtility.UrlEncode(pstrPosX));
        stringBuilder.Append("&");
      }
      if (pstrPosY.Length > 0)
      {
        stringBuilder.Append("fy=");
        stringBuilder.Append(HttpUtility.UrlEncode(pstrPosY));
        stringBuilder.Append("&");
      }
      return stringBuilder.ToString();
    }

    public byte[] ImageToBytes(System.Drawing.Image img)
    {
      return (byte[]) new ImageConverter().ConvertTo((object) img, typeof (byte[]));
    }

    private string ShowBinaryImage(
      byte[] image,
      string strTitle,
      int pintimagenAncho,
      float fltFuentePosX,
      float fltFuentePosY)
    {
      try
      {
        Font font = new Font("Verdana", 8f, FontStyle.Bold);
        Brush black = Brushes.Black;
        ImageConverter imageConverter = new ImageConverter();
        using (MemoryStream memoryStream = new MemoryStream(image))
        {
          using (Bitmap bitmap = (Bitmap) System.Drawing.Image.FromStream((Stream) memoryStream))
          {
            int thumbHeight = bitmap.Height * pintimagenAncho / bitmap.Width;
            System.Drawing.Image thumbnailImage = bitmap.GetThumbnailImage(pintimagenAncho, thumbHeight, (System.Drawing.Image.GetThumbnailImageAbort) null, IntPtr.Zero);
            if (!string.IsNullOrEmpty(strTitle))
            {
              Graphics graphics = Graphics.FromImage(thumbnailImage);
              graphics.SmoothingMode = SmoothingMode.AntiAlias;
              graphics.DrawString(strTitle, font, black, fltFuentePosX, fltFuentePosY);
              graphics.Save();
            }
            return "data:image/jpg; base64," + Convert.ToBase64String((byte[]) imageConverter.ConvertTo((object) thumbnailImage, typeof (byte[])));
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void VerificateLocation()
    {
      try
      {
        if (this.Session["SystemUser"] == null)
          throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - PlateDeliveryItem.aspx");
        if ((this.Session["SystemUser"] as SystemUser).i_LocationId == 14)
          return;
        this.chklRequisiteExchangeMode.Items[3].Enabled = false;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void LoadInitial(enmOwnerPersonType pOwnerPersonType)
    {
      try
      {
        int int32 = Convert.ToInt32(this.hddfProcessTypeId.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        string pstrUseCorrespondence = this.hddfUseCorrespondence.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        switch (pOwnerPersonType)
        {
          case enmOwnerPersonType.Natural:
            this.rblTypeRecabante.Items[0].Enabled = true;
            this.rblTypeRecabante.Items[0].Selected = true;
            this.rblTypeRecabante.Items[1].Selected = false;
            switch ((enmTypeCollect) Convert.ToInt32(this.rblTypeRecabante.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
            {
              case enmTypeCollect.Propietario:
                this.rblcurrentOption.Visible = true;
                this.rblcurrentOption.Items[0].Selected = true;
                this.rblPersonTypeCollect.Visible = false;
                this.txtNotariaRecabante.Text = string.Empty;
                this.txtPartidaRecabante.Text = string.Empty;
                this.txtNombresRecabante.Text = string.Empty;
                this.trwddDocument.Visible = true;
                this.trwddOwner.Visible = true;
                this.trtxtNombres.Visible = false;
                this.trNotaria.Visible = false;
                this.trPartida.Visible = false;
                this.LoadOwners();
                this.LoadRequisiteToDeliver(int32, 1, 1, 1, pstrUseCorrespondence, 0);
                return;
              case enmTypeCollect.Apoderado:
                this.rblcurrentOption.Visible = false;
                this.rblPersonTypeCollect.Visible = true;
                this.rblPersonTypeCollect.Enabled = false;
                this.rblPersonTypeCollect.Items[0].Selected = true;
                this.txtNotariaRecabante.Text = string.Empty;
                this.txtPartidaRecabante.Text = string.Empty;
                this.txtNombresRecabante.Text = string.Empty;
                this.txtNombresRecabante.Focus();
                this.txtNumeroRecabante.Text = string.Empty;
                this.txtNumeroRecabante.ReadOnly = false;
                this.trwddDocument.Visible = false;
                this.trwddOwner.Visible = false;
                this.trtxtNombres.Visible = true;
                this.trNotaria.Visible = true;
                this.trPartida.Visible = false;
                this.wddDocumentDescription.Enabled = true;
                this.wddDocumentDescription.SelectedValue = "-1";
                this.trwddDocument.Visible = true;
                this.LoadRequisiteToDeliver(int32, 1, 2, 1, pstrUseCorrespondence, 0);
                return;
              default:
                return;
            }
          case enmOwnerPersonType.Juridica:
            this.rblTypeRecabante.Items[0].Selected = false;
            this.rblTypeRecabante.Items[0].Enabled = false;
            this.rblTypeRecabante.Items[1].Selected = true;
            switch ((enmTypeCollect) Convert.ToInt32(this.rblTypeRecabante.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
            {
              case enmTypeCollect.Propietario:
                this.rblPersonTypeCollect.Enabled = false;
                this.rblPersonTypeCollect.Items[0].Selected = true;
                this.rblPersonTypeCollect.Items[1].Selected = false;
                this.trPartida.Visible = false;
                this.LoadRequisiteToDeliver(int32, 2, 1, 1, pstrUseCorrespondence, 1);
                return;
              case enmTypeCollect.Apoderado:
                this.rblcurrentOption.Visible = false;
                this.rblPersonTypeCollect.Visible = true;
                this.rblPersonTypeCollect.Enabled = true;
                this.rblPersonTypeCollect.Items[0].Selected = false;
                this.rblPersonTypeCollect.Items[1].Selected = true;
                this.txtNotariaRecabante.Text = string.Empty;
                this.txtPartidaRecabante.Text = string.Empty;
                this.txtNombresRecabante.Text = string.Empty;
                this.txtNombresRecabante.Focus();
                this.txtNumeroRecabante.Text = string.Empty;
                this.txtNumeroRecabante.ReadOnly = false;
                this.trwddDocument.Visible = false;
                this.trwddOwner.Visible = false;
                this.trtxtNombres.Visible = true;
                this.trNotaria.Visible = false;
                this.trPartida.Visible = true;
                this.wddDocumentDescription.Enabled = true;
                this.wddDocumentDescription.SelectedValue = "-1";
                this.trwddDocument.Visible = true;
                this.LoadRequisiteToDeliver(int32, 2, 2, 1, pstrUseCorrespondence, 2);
                return;
              default:
                return;
            }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void LoadOwners()
    {
      try
      {
        string[] strArray1 = this.ViewState["vsOwnerDocumentNumber"] as string[];
        string[] strArray2 = this.ViewState["vsOwnerDocumentType"] as string[];
        string[] strArray3 = this.ViewState["vsOwners"] as string[];
        int int32 = Convert.ToInt32(this.hddfProcessTypeId.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        this.wddOwner.DataSource = (object) strArray3;
        this.wddOwner.DataBind();
        if (int32 != 2)
        {
          this.wddOwner.SelectedIndex = strArray3.Length - 1;
          try
          {
            this.wddDocumentDescription.SelectedValue = strArray2[strArray2.Length - 1];
          }
          catch (Exception ex)
          {
            this.wddDocumentDescription.SelectedIndex = -1;
          }
          this.txtNumeroRecabante.Text = strArray1[strArray1.Length - 1];
        }
        else
        {
          try
          {
            this.wddDocumentDescription.SelectedValue = strArray2[strArray2.Length - 1];
          }
          catch (Exception ex)
          {
            this.wddDocumentDescription.SelectedIndex = -1;
          }
          this.txtNumeroRecabante.Text = strArray1[0];
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void LoadRequisiteToDeliver(
      int pintProcessTypeId,
      int pintPersonTypeId,
      int pintCollectTypeId,
      int pintIsVisible,
      string pstrUseCorrespondence,
      int pintRepresentativeCollectTypeId)
    {
      try
      {
        DataTable forExchangeModeBy = new PlateDeliverQueriesBL().GetPlateDeliverRequisiteForExchangeModeBy(pintProcessTypeId, pintPersonTypeId, pintCollectTypeId, pintIsVisible, pstrUseCorrespondence, pintRepresentativeCollectTypeId);
        this.chklRequisiteToDeliver.DataSource = (object) forExchangeModeBy;
        this.chklRequisiteToDeliver.DataTextField = "v_RequisiteObjectName";
        this.chklRequisiteToDeliver.DataValueField = "i_ObjectId";
        this.chklRequisiteToDeliver.DataBind();
        this.ViewState["vsRequisiteForExchangeMode"] = (object) forExchangeModeBy;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ChangeCollectType(int intOption)
    {
      try
      {
        switch (intOption)
        {
          case 1:
            this.ClearControls(1);
            break;
          case 2:
            this.ClearControls(2);
            break;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ClearControls(int intOption)
    {
      try
      {
        if (intOption == 1)
        {
          this.rblTypeRecabante.Items[0].Selected = true;
          this.txtNotariaRecabante.Text = string.Empty;
          this.txtPartidaRecabante.Text = string.Empty;
          this.txtNombresRecabante.Text = string.Empty;
          this.LoadOwners();
        }
        else
        {
          if (intOption != 2)
            return;
          this.rblTypeRecabante.Items[1].Selected = true;
          this.txtNotariaRecabante.Text = string.Empty;
          this.txtPartidaRecabante.Text = string.Empty;
          this.txtNombresRecabante.Text = string.Empty;
          this.txtNumeroRecabante.Text = string.Empty;
          this.trwddDocument.Visible = true;
          this.wddDocumentDescription.Enabled = true;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void RegisterDeliveryProcess()
    {
      try
      {
        using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
        {
          Timeout = new TimeSpan(0, 0, 5, 1)
        }))
        {
          int int32_1 = Convert.ToInt32(this.hddfRequirementPlateId.Value, (IFormatProvider) CultureInfo.CurrentCulture);
          string str1 = string.Empty;
          string str2 = string.Empty;
          string str3 = string.Empty;
          for (int index = 0; index < this.dlProductComposition.Items.Count; ++index)
          {
            HtmlInputCheckBox control = (HtmlInputCheckBox) this.dlProductComposition.Items[index].FindControl("htmlchkItem");
            if (control.Checked)
              str2 = str2 + control.Value + " / ";
          }
          foreach (ListItem listItem in this.chklRequisiteExchangeMode.Items)
          {
            if (listItem.Selected)
              str1 = str1 + listItem.Value + " / ";
          }
          foreach (ListItem listItem in this.chklRequisiteToDeliver.Items)
          {
            if (listItem.Selected)
              str3 = str3 + listItem.Value + " / ";
          }
          if (this.chklRequisiteExchangeMode.Items[0].Selected)
            str3 += "14 / 15";
          else if (this.chklRequisiteExchangeMode.Items[1].Selected && this.chklRequisiteExchangeMode.Items[2].Selected)
            str3 += "14 / 13";
          string str4 = (string) null;
          int? nullable1 = new int?();
          int? nullable2 = new int?();
          int? nullable3 = new int?();
          string str5 = (string) null;
          switch ((enmOwnerPersonType) Convert.ToInt32(this.rblPersonTypeOwner.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
          {
            case enmOwnerPersonType.Natural:
              enmTypeCollect int32_2 = (enmTypeCollect) Convert.ToInt32(this.rblTypeRecabante.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
              nullable3 = new int?(1);
              switch (int32_2)
              {
                case enmTypeCollect.Propietario:
                  enmOwnerType int32_3 = (enmOwnerType) Convert.ToInt32(this.rblcurrentOption.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
                  nullable2 = new int?(1);
                  switch (int32_3)
                  {
                    case enmOwnerType.Actual:
                      str4 = this.wddOwner.SelectedItem.Text;
                      nullable1 = new int?(Convert.ToInt32(this.wddDocumentDescription.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
                      str5 = this.txtNumeroRecabante.Text;
                      break;
                    case enmOwnerType.Nuevo:
                      str4 = this.txtNombresRecabante.Text;
                      nullable1 = new int?(Convert.ToInt32(this.wddDocumentDescription.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
                      str5 = this.txtNumeroRecabante.Text;
                      break;
                  }
                  break;
                case enmTypeCollect.Apoderado:
                  nullable2 = new int?(2);
                  str4 = this.txtNombresRecabante.Text;
                  nullable1 = new int?(Convert.ToInt32(this.wddDocumentDescription.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
                  str5 = this.txtNumeroRecabante.Text;
                  break;
              }
              break;
            case enmOwnerPersonType.Juridica:
              enmTypeCollect int32_4 = (enmTypeCollect) Convert.ToInt32(this.rblTypeRecabante.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
              enmPersonTypecollect int32_5 = (enmPersonTypecollect) Convert.ToInt32(this.rblPersonTypeCollect.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
              nullable3 = new int?(2);
              if (int32_4 == enmTypeCollect.Apoderado)
              {
                nullable2 = new int?(2);
                if (int32_5 == enmPersonTypecollect.Otros)
                {
                  str4 = this.txtNombresRecabante.Text;
                  nullable1 = new int?(Convert.ToInt32(this.wddDocumentDescription.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
                  str5 = this.txtNumeroRecabante.Text;
                }
                else if (int32_5 == enmPersonTypecollect.RepresentanteLegal)
                {
                  str4 = this.txtNombresRecabante.Text;
                  nullable1 = new int?(Convert.ToInt32(this.wddDocumentDescription.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
                  str5 = this.txtNumeroRecabante.Text;
                }
                break;
              }
              break;
          }
          int pintUpdateUserId = this.Session["SystemUser"] != null ? (this.Session["SystemUser"] as SystemUser).i_SystemUserId : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - PlateDeliveryItem.aspx");
          DateTime? nullable4 = new DateTime?();
          DateTime? nullable5 = new DateTime?();
          string str6 = (string) null;
          string str7 = (string) null;
          if (this.chklRequisiteExchangeMode.Items[4].Selected)
          {
            nullable4 = new DateTime?(this.wdpConsMTCtacha.Value);
            str6 = this.txtReferMTCtacha.Text;
          }
          if (this.chklRequisiteExchangeMode.Items[5].Selected)
          {
            nullable5 = new DateTime?(this.wdpConsMTCtacha.Value);
            str7 = this.txtReferMTCtacha.Text;
          }
          if (this.ViewState["i_IsClaim"].ToString() == "0")
          {
            PlateDeliverManagementBL deliverManagementBl = new PlateDeliverManagementBL();
            PlateDeliverData pobjPlateDeliverDataBE = new PlateDeliverData();
            pobjPlateDeliverDataBE.v_ExchangeModeId = str1;
            pobjPlateDeliverDataBE.v_ReferenceNumber = string.IsNullOrEmpty(this.txtReferencesNumber.Text) ? (string) null : this.txtReferencesNumber.Text;
            pobjPlateDeliverDataBE.d_ComplaintDate = new DateTime?(this.wdpDateDenuncia.Value);
            pobjPlateDeliverDataBE.v_ComplaintDependency = string.IsNullOrEmpty(this.txtDependency.Text) ? (string) null : this.txtDependency.Text;
            pobjPlateDeliverDataBE.v_ComplaintPartNumber = string.IsNullOrEmpty(this.txtParte.Text) ? (string) null : this.txtParte.Text;
            pobjPlateDeliverDataBE.v_CollectName = str4;
            pobjPlateDeliverDataBE.i_CollectDocumentType = nullable1;
            pobjPlateDeliverDataBE.v_CollectDocumentNumber = str5;
            pobjPlateDeliverDataBE.v_CollectNotaryName = string.IsNullOrEmpty(this.txtNotariaRecabante.Text) ? (string) null : this.txtNotariaRecabante.Text;
            pobjPlateDeliverDataBE.v_CollectDeparture = string.IsNullOrEmpty(this.txtPartidaRecabante.Text) ? (string) null : this.txtPartidaRecabante.Text;
            pobjPlateDeliverDataBE.v_DocumentsRequiredId = str3;
            pobjPlateDeliverDataBE.i_RequirementPlateId = new int?(int32_1);
            pobjPlateDeliverDataBE.v_ProductCompositionId = str2;
            pobjPlateDeliverDataBE.i_CollectTypeId = nullable2;
            pobjPlateDeliverDataBE.i_CollectPersonTypeId = nullable3;
            pobjPlateDeliverDataBE.i_RepresentativeCollectTypeId = string.IsNullOrEmpty(this.rblPersonTypeCollect.SelectedValue) ? new int?() : new int?(Convert.ToInt32(this.rblPersonTypeCollect.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
            pobjPlateDeliverDataBE.i_InsertUserId = new int?(pintUpdateUserId);
            pobjPlateDeliverDataBE.d_ConsMTCDate = nullable4;
            pobjPlateDeliverDataBE.d_TachaSUNARPDate = nullable5;
            pobjPlateDeliverDataBE.v_ReferenceNumberConsMTC = str6;
            pobjPlateDeliverDataBE.v_ReferenceNumberTachaSUNARP = str7;
            pobjPlateDeliverDataBE.v_PlateTacha = this.txtNroPlacaTachada.Text;
            int int32_6 = Convert.ToInt32(this.hdiPlateTypeId.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
            deliverManagementBl.PlateDeliverDataInsert(pobjPlateDeliverDataBE, int32_6);
          }
          else
            new PlateDeliverManagementBL().PlateDeliverDataUpdate(new PlateDeliverData()
            {
              v_ExchangeModeId = str1,
              v_ReferenceNumber = string.IsNullOrEmpty(this.txtReferencesNumber.Text) ? (string) null : this.txtReferencesNumber.Text,
              d_ComplaintDate = new DateTime?(this.wdpDateDenuncia.Value),
              v_ComplaintDependency = string.IsNullOrEmpty(this.txtDependency.Text) ? (string) null : this.txtDependency.Text,
              v_ComplaintPartNumber = string.IsNullOrEmpty(this.txtParte.Text) ? (string) null : this.txtParte.Text,
              v_CollectName = str4,
              i_CollectDocumentType = nullable1,
              v_CollectDocumentNumber = str5,
              v_CollectNotaryName = string.IsNullOrEmpty(this.txtNotariaRecabante.Text) ? (string) null : this.txtNotariaRecabante.Text,
              v_CollectDeparture = string.IsNullOrEmpty(this.txtPartidaRecabante.Text) ? (string) null : this.txtPartidaRecabante.Text,
              v_DocumentsRequiredId = str3,
              i_RequirementPlateId = new int?(int32_1),
              v_ProductCompositionId = str2,
              i_CollectTypeId = nullable2,
              i_CollectPersonTypeId = nullable3,
              i_RepresentativeCollectTypeId = string.IsNullOrEmpty(this.rblPersonTypeCollect.SelectedValue) ? new int?() : new int?(Convert.ToInt32(this.rblPersonTypeCollect.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture)),
              i_InsertUserId = new int?(pintUpdateUserId),
              d_ConsMTCDate = nullable4,
              d_TachaSUNARPDate = nullable5,
              v_ReferenceNumberConsMTC = str6,
              v_ReferenceNumberTachaSUNARP = str7,
              v_PlateTacha = this.txtNroPlacaTachada.Text
            });
          new PlateDeliverManagementBL().SearchIdsInWarehouseControlDetailUpdate(int32_1, pintUpdateUserId);
          this.InterfaceWithWarehouse();
          this.UpdateOtherProcess();
          Message.SetMessage(this.lblMessage, new HandledException(2, "Proceso de Entrega Registrado con exito."));
          this.wibFinalize.Visible = false;
          this.wibCancel.Visible = false;
          this.wibClain.Visible = false;
          this.wibAceptar.Visible = true;
          transactionScope.Complete();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void InterfaceWithWarehouse()
    {
      try
      {
        int int32_1 = Convert.ToInt32(this.hddfRequirementPlateId.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        DataTable dataTable1 = this.ViewState["vsRequisiteForExchangeMode"] as DataTable;
        DataTable dataTable2 = this.ViewState["vsdtPlateDeliverItem"] as DataTable;
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - PlateDeliveryItem.aspx");
        int iLocationId = systemUser.i_LocationId;
        int iSystemUserId = systemUser.i_SystemUserId;
        int int32_2 = Convert.ToInt32(new WarehouseQueriesBL().GetWarehouseBy(0, "", iLocationId, 10).Rows[0]["i_WarehouseId"], (IFormatProvider) CultureInfo.CurrentCulture);
        string str = string.Empty;
        DataTable dataTable3 = new DTStockMovementDetail().DataTableStockMovementDetail();
        foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
        {
          if (Convert.ToInt32(row["b_IsRequiered"], (IFormatProvider) CultureInfo.CurrentCulture) == 1)
            str = str + row["i_ObjectId"]?.ToString() + "/";
        }
        if (this.chklRequisiteExchangeMode.Items[0].Selected)
          str += "14/15";
        else if (this.chklRequisiteExchangeMode.Items[1].Selected && this.chklRequisiteExchangeMode.Items[2].Selected)
          str += "14/13";
        string[] strArray = str.Split('/');
        for (int index = 0; index < strArray.Length; ++index)
        {
          if (string.IsNullOrEmpty(strArray[index]))
          {
            DataRowCollection rows = dataTable3.Rows;
            object[] objArray = new object[22];
            objArray[3] = (object) strArray[index];
            objArray[6] = (object) 1;
            objArray[8] = (object) int32_1;
            objArray[17] = (object) strArray[index];
            objArray[20] = (object) this.txtPlateNew.Text;
            rows.Add(objArray);
          }
        }
        if (dataTable3.Rows.Count > 0)
        {
          StockMovementManagementBL movementManagementBl = new StockMovementManagementBL();
          StockMovement pobjStockMovement = new StockMovement();
          pobjStockMovement.i_WarehouseId = new int?(int32_2);
          pobjStockMovement.i_MotiveMovementId = new int?(14);
          pobjStockMovement.i_SupplierId = new int?();
          pobjStockMovement.i_DocumentTypeId = new int?();
          pobjStockMovement.v_DocumentNumber = (string) null;
          pobjStockMovement.i_UserId = new int?(iSystemUserId);
          pobjStockMovement.b_Checked = new bool?(false);
          pobjStockMovement.v_Observation = string.Empty;
          pobjStockMovement.d_InsertDate = new DateTime?(this.ViewState["i_IsClaim"].ToString() == "1" ? Convert.ToDateTime(this.ViewState["d_DeliveryDate"], (IFormatProvider) CultureInfo.CurrentCulture) : DateTime.Now);
          pobjStockMovement.i_ProductionOrderId = new int?();
          DataTable pdtStockMovementDetail = dataTable3;
          movementManagementBl.StockMovementInsertInterchangeObject(pobjStockMovement, pdtStockMovementDetail);
        }
        dataTable3.Rows.Clear();
        int int32_3 = Convert.ToInt32(new WarehouseQueriesBL().GetWarehouseBy(0, "", iLocationId, 1).Rows[0]["i_WarehouseId"], (IFormatProvider) CultureInfo.CurrentCulture);
        int num = 49;
        int? nullable1 = dataTable2.Rows[0]["i_StockMovementDetailId"] is DBNull ? new int?() : new int?(Convert.ToInt32(dataTable2.Rows[0]["i_StockMovementDetailId"], (IFormatProvider) CultureInfo.CurrentCulture));
        int? nullable2 = dataTable2.Rows[0]["i_StockMovementId"] is DBNull ? new int?() : new int?(Convert.ToInt32(dataTable2.Rows[0]["i_StockMovementId"], (IFormatProvider) CultureInfo.CurrentCulture));
        int? nullable3 = dataTable2.Rows[0]["i_LocationWarehouseId"] is DBNull ? new int?() : new int?(Convert.ToInt32(dataTable2.Rows[0]["i_LocationWarehouseId"], (IFormatProvider) CultureInfo.CurrentCulture));
        int? nullable4 = dataTable2.Rows[0]["i_ProductId"] is DBNull ? new int?() : new int?(Convert.ToInt32(dataTable2.Rows[0]["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture));
        DataRowCollection rows1 = dataTable3.Rows;
        object[] objArray1 = new object[22];
        objArray1[0] = (object) nullable1;
        objArray1[1] = (object) nullable2;
        objArray1[2] = (object) nullable3;
        objArray1[3] = (object) nullable4;
        objArray1[6] = (object) 1;
        objArray1[8] = (object) int32_1;
        objArray1[20] = (object) this.txtPlateNew.Text;
        rows1.Add(objArray1);
        StockMovementManagementBL movementManagementBl1 = new StockMovementManagementBL();
        StockMovement pobjStockMovement1 = new StockMovement();
        pobjStockMovement1.i_WarehouseId = new int?(int32_3);
        pobjStockMovement1.i_MotiveMovementId = new int?(num);
        pobjStockMovement1.i_SupplierId = new int?();
        pobjStockMovement1.i_DocumentTypeId = new int?();
        pobjStockMovement1.v_DocumentNumber = (string) null;
        pobjStockMovement1.i_UserId = new int?(iSystemUserId);
        pobjStockMovement1.b_Checked = new bool?(false);
        pobjStockMovement1.v_Observation = string.Empty;
        pobjStockMovement1.d_InsertDate = new DateTime?(this.ViewState["i_IsClaim"].ToString() == "1" ? Convert.ToDateTime(this.ViewState["d_DeliveryDate"], (IFormatProvider) CultureInfo.CurrentCulture) : DateTime.Now);
        pobjStockMovement1.i_ProductionOrderId = new int?();
        DataTable pdtStockMovementDetail1 = dataTable3;
        movementManagementBl1.StockMovementInsertDeliberyPlate(pobjStockMovement1, pdtStockMovementDetail1);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void UpdateOtherProcess()
    {
      try
      {
        int int32 = Convert.ToInt32(this.hddfUserId.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        DataTable dataTable = this.ViewState["vsdtPlateDeliverItem"] as DataTable;
        string[] platePreviousOld = new VehicleRegistrationQueriesBL().GetPlatePreviousOld(new VehicleRegistration()
        {
          v_PlateCurrent = dataTable.Rows[0]["v_PlateNew"].ToString(),
          v_PlateQuestion = dataTable.Rows[0]["v_PlateOld"].ToString(),
          v_SerialNumber = dataTable.Rows[0]["v_SerialNumber"].ToString(),
          v_TitleNumber = dataTable.Rows[0]["v_TitleNumber"].ToString(),
          v_PlateOld = string.Empty,
          v_PlatePrevious = string.Empty
        });
        new VehicleRegistrationManagementBL().VehicleRegistrationInsert(new VehicleRegistration()
        {
          i_VehicleCategoryId = dataTable.Rows[0]["i_VehicleCategoryId"] is DBNull ? new int?() : (int?) dataTable.Rows[0]["i_VehicleCategoryId"],
          i_VehicleUseId = dataTable.Rows[0]["i_VehicleTypeUseId"] is DBNull ? new int?() : (int?) dataTable.Rows[0]["i_VehicleTypeUseId"],
          v_PlateNew = dataTable.Rows[0]["v_PlateNew"].ToString(),
          v_PlateOld = platePreviousOld[0],
          v_PlatePrevious = platePreviousOld[1],
          v_TitleNumber = dataTable.Rows[0]["v_TitleNumber"].ToString(),
          v_Brand = dataTable.Rows[0]["v_Brand"].ToString(),
          v_Model = dataTable.Rows[0]["v_Model"].ToString(),
          v_SerialNumber = dataTable.Rows[0]["v_SerialNumber"].ToString(),
          v_BlankCode1 = dataTable.Rows[0]["v_BlankCode1"].ToString(),
          v_BlankCode2 = dataTable.Rows[0]["v_BlankCode2"].ToString(),
          v_RFIDCode = dataTable.Rows[0]["v_RFIDCode"].ToString(),
          v_TIDCode = dataTable.Rows[0]["v_TIDCode"].ToString(),
          v_EPCCode = dataTable.Rows[0]["v_EPCCode"].ToString(),
          v_OptimalNumberCode = dataTable.Rows[0]["v_OptimalNumberCode"].ToString(),
          v_OwnerCompleteName = dataTable.Rows[0]["v_OwnerCompleteName"].ToString(),
          v_OwnerDocumentType = dataTable.Rows[0]["v_OwnerDocumentType"].ToString(),
          v_OwnerDocumentDescription = dataTable.Rows[0]["v_OwnerDocumentDescription"].ToString(),
          v_OwnerDocumentNumber = dataTable.Rows[0]["v_OwnerDocumentNumber"].ToString(),
          i_Status = new int?(1),
          i_InsertUserId = new int?(int32),
          d_InsertDate = new DateTime?(DateTime.Now),
          i_UpdateUserId = new int?(int32),
          d_UpdateDate = new DateTime?(DateTime.Now),
          i_UpdateAll = true
        });
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void CreatePopUp(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    private void IsValidSelectProductComposition()
    {
      try
      {
        bool flag = true;
        string empty = string.Empty;
        if (!this.chkItems.Checked)
          flag = false;
        if (!flag)
          throw new HandledException(1, "Datos del Producto : <br>__________________<br>" + "Debe aceptar todos los componentes del KIT.");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void IsValidInputDataExchange()
    {
      try
      {
        if (this.rblIsHavePlateOld.SelectedIndex == -1)
          return;
        string empty = string.Empty;
        switch ((enmHavePlateOld) Convert.ToInt32(this.rblIsHavePlateOld.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
        {
          case enmHavePlateOld.Si:
            if (!this.chklRequisiteExchangeMode.Items[0].Selected && !this.chklRequisiteExchangeMode.Items[1].Selected && !this.chklRequisiteExchangeMode.Items[2].Selected && !this.chklRequisiteExchangeMode.Items[3].Selected && !this.chklRequisiteExchangeMode.Items[4].Selected && !this.chklRequisiteExchangeMode.Items[5].Selected)
              throw new HandledException(1, "Datos Intercambio <br>____________________<br> Debe seleccionar por lo menos (1) Modo de Intercambio.");
            if (this.chklRequisiteExchangeMode.Items[1].Selected && this.chklRequisiteExchangeMode.Items[2].Selected)
            {
              if (this.txtDependency.Text == string.Empty)
                throw new HandledException(1, "Datos de Denuncia: <br>_______________<br> Debe ingresar una dependencia válida.");
              if (this.txtParte.Text == string.Empty)
                throw new HandledException(1, "Datos de Denuncia: <br>_______________<br> Debe ingresar Nro de parte válido.");
            }
            if (this.chklRequisiteExchangeMode.Items[2].Selected)
            {
              if (this.txtDependency.Text == string.Empty)
                throw new HandledException(1, "Datos de Denuncia: <br>_______________<br> Debe ingresar una dependencia válida.");
              if (this.txtParte.Text == string.Empty)
                throw new HandledException(1, "Datos de Denuncia: <br>_______________<br> Debe ingresar Nro de parte válido.");
            }
            if (this.chklRequisiteExchangeMode.Items[3].Selected && this.txtReferencesNumber.Text == string.Empty)
              throw new HandledException(1, "Datos Intercambio <br>____________________<br> Debe Ingresar Nro de Referencia válido para la entrega especial.");
            break;
          case enmHavePlateOld.No:
            if (!this.chklRequisiteExchangeMode.Items[2].Selected && !this.chklRequisiteExchangeMode.Items[3].Selected && !this.chklRequisiteExchangeMode.Items[4].Selected && !this.chklRequisiteExchangeMode.Items[5].Selected)
              throw new HandledException(1, "Datos Intercambio <br>____________________<br> Debe seleccionar por lo menos (1) Modo de Intercambio.");
            if (this.chklRequisiteExchangeMode.Items[1].Selected && this.chklRequisiteExchangeMode.Items[2].Selected)
            {
              if (string.IsNullOrEmpty(this.txtDependency.Text))
                throw new HandledException(1, "Datos de Denuncia: <br>_______________<br> Debe ingresar una Dependencia válida.");
              if (string.IsNullOrEmpty(this.txtParte.Text))
                throw new HandledException(1, "Datos de Denuncia: <br>_______________<br> Debe Ingresar Nro de parte válido.");
            }
            if (this.chklRequisiteExchangeMode.Items[2].Selected)
            {
              if (string.IsNullOrEmpty(this.txtDependency.Text))
                throw new HandledException(1, "Datos de Denuncia: <br>_______________<br> Debe ingresar una dependencia válida.");
              if (string.IsNullOrEmpty(this.txtParte.Text))
                throw new HandledException(1, "Datos de Denuncia: <br>_______________<br> Debe ingresar Nro de parte válido.");
            }
            if (this.chklRequisiteExchangeMode.Items[3].Selected && string.IsNullOrEmpty(this.txtReferencesNumber.Text))
              throw new HandledException(1, "Datos Intercambio <br>____________________<br> Debe ingresar Nro de Referencia válido.");
            if (this.chklRequisiteExchangeMode.Items[4].Selected && string.IsNullOrEmpty(this.txtReferMTCtacha.Text))
              throw new HandledException(1, "Datos Intercambio <br>____________________<br> Debe ingresar Nro de Referencia MTC válido.");
            if (this.chklRequisiteExchangeMode.Items[5].Selected)
            {
              if (string.IsNullOrEmpty(this.txtReferMTCtacha.Text))
                throw new HandledException(1, "Datos Intercambio <br>____________________<br> Debe ingresar Nro de Referencia de tacha válido.");
              if (string.IsNullOrEmpty(this.txtNroPlacaTachada.Text))
                throw new HandledException(1, "Datos Intercambio <br>____________________<br> Debe ingresar Nro de Placa tachada.");
            }
            break;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void IsValidInputDataCollect()
    {
      try
      {
        string empty = string.Empty;
        enmTypeCollect int32_1 = (enmTypeCollect) Convert.ToInt32(this.rblTypeRecabante.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        switch ((enmOwnerPersonType) Convert.ToInt32(this.rblPersonTypeOwner.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
        {
          case enmOwnerPersonType.Natural:
            if (int32_1 == enmTypeCollect.Apoderado)
            {
              if (string.IsNullOrEmpty(this.txtNombresRecabante.Text))
                empty += "<br> Debe ingresar Nombre del apoderado.";
              if (this.wddDocumentDescription.SelectedValue == "-1")
                empty += "<br> Debe seleccionar Tipo de documento.";
              if (string.IsNullOrEmpty(this.txtNumeroRecabante.Text))
                empty += "<br> Debe ingresar Nro documento.";
              if (string.IsNullOrEmpty(this.txtNotariaRecabante.Text))
                empty += "<br> Debe ingresar Notaría.";
              break;
            }
            break;
          case enmOwnerPersonType.Juridica:
            enmPersonTypecollect int32_2 = (enmPersonTypecollect) Convert.ToInt32(this.rblPersonTypeCollect.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
            switch (int32_1)
            {
              case enmTypeCollect.Propietario:
                if (string.IsNullOrEmpty(this.txtNombresRecabante.Text))
                  empty += "<br> Debe ingresar Nombre del apoderado.";
                if (this.wddDocumentDescription.SelectedValue == "-1")
                  empty += "<br> Debe seleccionar Tipo de documento.";
                if (string.IsNullOrEmpty(this.txtNumeroRecabante.Text))
                  empty += "<br> Debe ingresar Nro documento.";
                if (string.IsNullOrEmpty(this.txtNotariaRecabante.Text))
                {
                  empty += "<br> Debe ingresar Notaría.";
                  break;
                }
                break;
              case enmTypeCollect.Apoderado:
                if (int32_2 == enmPersonTypecollect.RepresentanteLegal)
                {
                  if (string.IsNullOrEmpty(this.txtNombresRecabante.Text))
                    empty += "<br> Debe ingresar Nombre del apoderado.";
                  if (this.wddDocumentDescription.SelectedValue == "-1")
                    empty += "<br> Debe seleccionar Tipo de documento.";
                  if (string.IsNullOrEmpty(this.txtNumeroRecabante.Text))
                    empty += "<br> Debe ingresar Nro documento.";
                  if (string.IsNullOrEmpty(this.txtPartidaRecabante.Text))
                    empty += "<br> Debe ingresar Partida.";
                }
                else if (int32_2 == enmPersonTypecollect.Otros)
                {
                  if (string.IsNullOrEmpty(this.txtNombresRecabante.Text))
                    empty += "<br> Debe ingresar Nombre del apoderado.";
                  if (this.wddDocumentDescription.SelectedValue == "-1")
                    empty += "<br> Debe seleccionar Tipo de documento.";
                  if (string.IsNullOrEmpty(this.txtNumeroRecabante.Text))
                    empty += "<br> Debe ingresar Nro documento.";
                  if (string.IsNullOrEmpty(this.txtNotariaRecabante.Text))
                    empty += "<br> Debe ingresar Notaría.";
                  if (string.IsNullOrEmpty(this.txtPartidaRecabante.Text))
                    empty += "<br> Debe ingresar Partida.";
                }
                break;
            }
            break;
        }
        if (empty != string.Empty)
          throw new HandledException(1, "Datos del Recabante : (Apoderado) <br>______________________________________<br>" + empty);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void IsValidInputDataNewOwner()
    {
      try
      {
        string empty = string.Empty;
        switch ((enmOwnerPersonType) Convert.ToInt32(this.rblPersonTypeOwner.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
        {
          case enmOwnerPersonType.Natural:
            if (Convert.ToInt32(this.rblTypeRecabante.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture) == 1 && Convert.ToInt32(this.rblcurrentOption.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture) == 2)
            {
              if (this.txtNombresRecabante.Text == string.Empty)
                empty += "<br> Debe ingresar Nombre del nuevo Propietario.";
              if (this.wddDocumentDescription.SelectedValue == "-1")
                empty += "<br> Debe seleccionar Tipo de documento del nuevo Propietario.";
              if (this.txtNumeroRecabante.Text == string.Empty)
                empty += "<br> Debe ingresar Nro documento del nuevo Propietario.";
              break;
            }
            break;
        }
        if (empty != string.Empty)
          throw new HandledException(1, "Datos del Recabante : (Nuevo Propietario)<br>_____________________________________<br>" + empty);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void IsValidRequiredDocuments(int pintProcessTypeId, int pintOwnerPersonType)
    {
      try
      {
        string str = this.hddfUseCorrespondence.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        int int32_1 = Convert.ToInt32(this.rblTypeRecabante.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        enmOwnerPersonType int32_2 = (enmOwnerPersonType) Convert.ToInt32(this.rblPersonTypeOwner.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        if (pintProcessTypeId == 5)
        {
          if (int32_1 == 1)
          {
            if (pintOwnerPersonType == 1)
            {
              if (!this.chklRequisiteToDeliver.Items[0].Selected)
                throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de la Orden de Giro (OG)/Boleta Informativa.");
              if (!this.chklRequisiteToDeliver.Items[1].Selected)
                throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de DNI vigente (Original).");
            }
          }
          else if (int32_1 == 2)
          {
            if (pintOwnerPersonType == 1)
            {
              if (!this.chklRequisiteToDeliver.Items[0].Selected)
                throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de la Orden de Giro (OG)/Boleta Informativa.");
              if (!this.chklRequisiteToDeliver.Items[1].Selected)
                throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Carta Poder Legalizada.");
            }
            else if (pintOwnerPersonType == 2)
            {
              switch ((enmPersonTypecollect) Convert.ToInt32(this.rblPersonTypeCollect.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
              {
                case enmPersonTypecollect.Otros:
                  if (!this.chklRequisiteToDeliver.Items[0].Selected)
                    throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de la Orden de Giro (OG)/Boleta Informativa.");
                  if (!this.chklRequisiteToDeliver.Items[1].Selected)
                    throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de DNI vigente (Original).");
                  if (!this.chklRequisiteToDeliver.Items[2].Selected)
                    throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Carta Poder Juridica Legalizada.");
                  break;
                case enmPersonTypecollect.RepresentanteLegal:
                  if (!this.chklRequisiteToDeliver.Items[0].Selected)
                    throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de la Orden de Giro (OG)/Boleta Informativa.");
                  if (!this.chklRequisiteToDeliver.Items[1].Selected)
                    throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de DNI vigente (Original).");
                  if (!this.chklRequisiteToDeliver.Items[2].Selected)
                    throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Vigencia de Poder (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[3].Selected)
                    throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Vigencia de Poder (Original).");
                  break;
              }
            }
          }
        }
        if (pintProcessTypeId == 2 || pintProcessTypeId == 3 || pintProcessTypeId == 7)
        {
          if (pintOwnerPersonType == 1)
          {
            if (int32_1 == 1)
            {
              if (!this.chklRequisiteToDeliver.Items[0].Selected)
                throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de la Orden de Giro (OG)/Boleta Informativa.");
              if (!this.chklRequisiteToDeliver.Items[1].Selected)
                throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de DNI vigente (Original).");
            }
            else if (int32_1 == 2)
            {
              if (!this.chklRequisiteToDeliver.Items[0].Selected)
                throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de la Orden de Giro (OG)/Boleta Informativa.");
              if (!this.chklRequisiteToDeliver.Items[1].Selected)
                throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Carta Poder Legalizada.");
              if (!this.chklRequisiteToDeliver.Items[2].Selected)
                throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Copia Legalizada DNI Propietario.");
            }
          }
          else if (pintOwnerPersonType == 2 && int32_1 == 2)
          {
            switch ((enmPersonTypecollect) Convert.ToInt32(this.rblPersonTypeCollect.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
            {
              case enmPersonTypecollect.Otros:
                if (!this.chklRequisiteToDeliver.Items[0].Selected)
                  throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de la Orden de Giro (OG)/Boleta Informativa.");
                if (!this.chklRequisiteToDeliver.Items[1].Selected)
                  throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de DNI vigente (Original).");
                if (!this.chklRequisiteToDeliver.Items[2].Selected)
                  throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Carta Poder Juridica Legalizada.");
                if (!this.chklRequisiteToDeliver.Items[3].Selected)
                  throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Copia Legalizada DNI del Representante Legal.");
                break;
              case enmPersonTypecollect.RepresentanteLegal:
                if (!this.chklRequisiteToDeliver.Items[0].Selected)
                  throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de la Orden de Giro (OG)/Boleta Informativa.");
                if (!this.chklRequisiteToDeliver.Items[1].Selected)
                  throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de DNI vigente (Original).");
                if (!this.chklRequisiteToDeliver.Items[2].Selected)
                  throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Vigencia de Poder(Copia).");
                if (!this.chklRequisiteToDeliver.Items[3].Selected)
                  throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de de Vigencia de Poder(Original).");
                break;
            }
          }
        }
        if (pintProcessTypeId == 4)
        {
          if (pintOwnerPersonType == 1)
          {
            if (int32_1 == 1)
            {
              if (!this.chklRequisiteToDeliver.Items[0].Selected)
                throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de DNI vigente (Copia).");
              if (!this.chklRequisiteToDeliver.Items[1].Selected)
                throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Boleta Informativa.");
            }
            else if (int32_1 == 2)
            {
              if (!this.chklRequisiteToDeliver.Items[0].Selected)
                throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de DNI vigente (Copia).");
              if (!this.chklRequisiteToDeliver.Items[1].Selected)
                throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Carta Poder Legalizada.");
              if (!this.chklRequisiteToDeliver.Items[2].Selected)
                throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Boleta Informativa.");
              if (!this.chklRequisiteToDeliver.Items[3].Selected)
                throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Copia Legalizada DNI Propietario.");
            }
          }
          else if (pintOwnerPersonType == 2)
          {
            switch ((enmPersonTypecollect) Convert.ToInt32(this.rblPersonTypeCollect.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
            {
              case enmPersonTypecollect.Otros:
                if (!this.chklRequisiteToDeliver.Items[0].Selected)
                  throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de DNI vigente (Original).");
                if (!this.chklRequisiteToDeliver.Items[1].Selected)
                  throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de DNI Vigente (Copia).");
                if (!this.chklRequisiteToDeliver.Items[2].Selected)
                  throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Carta Poder Juridica Legalizada.");
                if (!this.chklRequisiteToDeliver.Items[3].Selected)
                  throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Copia Legalizada DNI Representante Legal.");
                if (!this.chklRequisiteToDeliver.Items[4].Selected)
                  throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Boleta Informativa.");
                break;
              case enmPersonTypecollect.RepresentanteLegal:
                if (!this.chklRequisiteToDeliver.Items[0].Selected)
                  throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de DNI vigente (Original).");
                if (!this.chklRequisiteToDeliver.Items[1].Selected)
                  throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de DNI Vigente (Copia).");
                if (!this.chklRequisiteToDeliver.Items[2].Selected)
                  throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Vigencia de Poder (Copia).");
                if (!this.chklRequisiteToDeliver.Items[3].Selected)
                  throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Vigencia de Poder (Original).");
                if (!this.chklRequisiteToDeliver.Items[4].Selected)
                  throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Boleta Informativa.");
                break;
            }
          }
        }
        if (pintProcessTypeId == 1)
        {
          if (pintOwnerPersonType == 1)
          {
            if (int32_1 == 1)
            {
              if (!this.chklRequisiteToDeliver.Items[0].Selected)
                throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de DNI vigente (Copia).");
              if (!this.chklRequisiteToDeliver.Items[1].Selected)
                throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Boleta Informativa.");
            }
            else if (int32_1 == 2)
            {
              if (!this.chklRequisiteToDeliver.Items[0].Selected)
                throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de DNI vigente (Copia).");
              if (!this.chklRequisiteToDeliver.Items[1].Selected)
                throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Carta Poder Legalizada.");
              if (!this.chklRequisiteToDeliver.Items[2].Selected)
                throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Boleta Informativa.");
              if (!this.chklRequisiteToDeliver.Items[3].Selected)
                throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Copia Legalizada DNI Propietario.");
            }
          }
          else if (pintOwnerPersonType == 2)
          {
            switch ((enmPersonTypecollect) Convert.ToInt32(this.rblPersonTypeCollect.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
            {
              case enmPersonTypecollect.Otros:
                if (!this.chklRequisiteToDeliver.Items[0].Selected)
                  throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de DNI vigente (Original).");
                if (!this.chklRequisiteToDeliver.Items[1].Selected)
                  throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de DNI Vigente (Copia).");
                if (!this.chklRequisiteToDeliver.Items[2].Selected)
                  throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Carta Poder Juridica Legalizada.");
                if (!this.chklRequisiteToDeliver.Items[3].Selected)
                  throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Boleta Informativa.");
                if (!this.chklRequisiteToDeliver.Items[4].Selected)
                  throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Copia Legalizada DNI Representante Legal.");
                break;
              case enmPersonTypecollect.RepresentanteLegal:
                if (!this.chklRequisiteToDeliver.Items[0].Selected)
                  throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de DNI vigente (Original).");
                if (!this.chklRequisiteToDeliver.Items[1].Selected)
                  throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de DNI Vigente (Copia).");
                if (!this.chklRequisiteToDeliver.Items[2].Selected)
                  throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Vigencia de Poder (Copia).");
                if (!this.chklRequisiteToDeliver.Items[3].Selected)
                  throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Vigencia de Poder (Original).");
                if (!this.chklRequisiteToDeliver.Items[4].Selected)
                  throw new HandledException(0, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Boleta Informativa.");
                break;
            }
          }
        }
        if (pintProcessTypeId != 6)
          return;
        if (str == "3,4")
        {
          switch (int32_2)
          {
            case enmOwnerPersonType.Natural:
              switch ((enmTypeCollect) Convert.ToInt32(this.rblTypeRecabante.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
              {
                case enmTypeCollect.Propietario:
                  if (!this.chklRequisiteToDeliver.Items[0].Selected)
                    throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Certificado de Habilitación Municipal Vigente (Original)");
                  if (!this.chklRequisiteToDeliver.Items[1].Selected)
                    throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Certificado de Habilitación Municipal Vigente (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[3].Selected)
                    throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Certificado de Habilitación Municipal Vigente (Original)");
                  if (!this.chklRequisiteToDeliver.Items[4].Selected)
                    throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Certificado de Habilitación Municipal Vigente (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[5].Selected)
                    throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de DNI Vigente (Original).");
                  break;
                case enmTypeCollect.Apoderado:
                  if (!this.chklRequisiteToDeliver.Items[0].Selected)
                    throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Certificado de Habilitación Municipal Vigente (Original)");
                  if (!this.chklRequisiteToDeliver.Items[1].Selected)
                    throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Certificado de Habilitación Municipal Vigente (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[3].Selected)
                    throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Certificado de Habilitación Municipal Vigente (Original)");
                  if (!this.chklRequisiteToDeliver.Items[4].Selected)
                    throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Certificado de Habilitación Municipal Vigente (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[5].Selected)
                    throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de DNI Vigente (Original).");
                  if (!this.chklRequisiteToDeliver.Items[6].Selected)
                    throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Carta poder legalizada.");
                  break;
              }
              break;
            case enmOwnerPersonType.Juridica:
              switch ((enmPersonTypecollect) Convert.ToInt32(this.rblPersonTypeCollect.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
              {
                case enmPersonTypecollect.Otros:
                  if (!this.chklRequisiteToDeliver.Items[0].Selected)
                    throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Certificado de Habilitación Municipal Vigente (Original)");
                  if (!this.chklRequisiteToDeliver.Items[1].Selected)
                    throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Certificado de Habilitación Municipal Vigente (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[3].Selected)
                    throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Certificado de Habilitación Municipal Vigente (Original)");
                  if (!this.chklRequisiteToDeliver.Items[4].Selected)
                    throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Certificado de Habilitación Municipal Vigente (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[5].Selected)
                    throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de DNI Vigente (Original).");
                  if (!this.chklRequisiteToDeliver.Items[6].Selected)
                    throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Carta Poder Juridica Legalizada.");
                  break;
                case enmPersonTypecollect.RepresentanteLegal:
                  if (!this.chklRequisiteToDeliver.Items[0].Selected)
                    throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Certificado de Habilitación Municipal Vigente (Original)");
                  if (!this.chklRequisiteToDeliver.Items[1].Selected)
                    throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Certificado de Habilitación Municipal Vigente (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[3].Selected)
                    throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Certificado de Habilitación Municipal Vigente (Original)");
                  if (!this.chklRequisiteToDeliver.Items[4].Selected)
                    throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Certificado de Habilitación Municipal Vigente (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[5].Selected)
                    throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de DNI Vigente (Original).");
                  if (!this.chklRequisiteToDeliver.Items[6].Selected)
                    throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Vigencia de Poder (Original)");
                  if (!this.chklRequisiteToDeliver.Items[7].Selected)
                    throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Vigencia de Poder (Copia)");
                  break;
              }
              break;
          }
        }
        if (str == "4,3")
        {
          switch (int32_2)
          {
            case enmOwnerPersonType.Natural:
              switch ((enmTypeCollect) Convert.ToInt32(this.rblTypeRecabante.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
              {
                case enmTypeCollect.Propietario:
                  if (!this.chklRequisiteToDeliver.Items[0].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Declaración Jurada Simple del Cambio de Uso.");
                  if (!this.chklRequisiteToDeliver.Items[2].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Original).");
                  if (!this.chklRequisiteToDeliver.Items[3].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[4].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de DNI Vigente (Original).");
                  break;
                case enmTypeCollect.Apoderado:
                  if (!this.chklRequisiteToDeliver.Items[0].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Declaración Jurada Simple del Cambio de Uso.");
                  if (!this.chklRequisiteToDeliver.Items[2].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Original).");
                  if (!this.chklRequisiteToDeliver.Items[3].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[4].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de DNI Vigente (Original).");
                  if (!this.chklRequisiteToDeliver.Items[5].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Carta Poder Legalizada.");
                  break;
              }
              break;
            case enmOwnerPersonType.Juridica:
              switch ((enmPersonTypecollect) Convert.ToInt32(this.rblPersonTypeCollect.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
              {
                case enmPersonTypecollect.Otros:
                  if (!this.chklRequisiteToDeliver.Items[0].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Declaración Jurada Simple del Cambio de Uso.");
                  if (!this.chklRequisiteToDeliver.Items[2].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Original).");
                  if (!this.chklRequisiteToDeliver.Items[3].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[4].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de DNI Vigente (Original).");
                  if (!this.chklRequisiteToDeliver.Items[5].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Carta Poder Juridica Legalizada.");
                  break;
                case enmPersonTypecollect.RepresentanteLegal:
                  if (!this.chklRequisiteToDeliver.Items[0].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Declaración Jurada Simple del Cambio de Uso.");
                  if (!this.chklRequisiteToDeliver.Items[2].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Original).");
                  if (!this.chklRequisiteToDeliver.Items[3].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[4].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de DNI Vigente (Original).");
                  if (!this.chklRequisiteToDeliver.Items[5].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Vigencia de Poder (Original).");
                  if (!this.chklRequisiteToDeliver.Items[5].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Vigencia de Poder (Copia).");
                  break;
              }
              break;
          }
        }
        if (str == "3,5")
        {
          switch (int32_2)
          {
            case enmOwnerPersonType.Natural:
              switch ((enmTypeCollect) Convert.ToInt32(this.rblTypeRecabante.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
              {
                case enmTypeCollect.Propietario:
                  if (!this.chklRequisiteToDeliver.Items[0].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de circulación MTC (Original).");
                  if (!this.chklRequisiteToDeliver.Items[1].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de circulación MTC (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[2].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Original).");
                  if (!this.chklRequisiteToDeliver.Items[3].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[5].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de DNI Vigente (Original).");
                  break;
                case enmTypeCollect.Apoderado:
                  if (!this.chklRequisiteToDeliver.Items[0].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de circulación MTC (Original).");
                  if (!this.chklRequisiteToDeliver.Items[1].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de circulación MTC (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[2].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Original).");
                  if (!this.chklRequisiteToDeliver.Items[3].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[5].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de DNI Vigente (Original).");
                  if (!this.chklRequisiteToDeliver.Items[6].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Carta Poder Legalizada");
                  break;
              }
              break;
            case enmOwnerPersonType.Juridica:
              switch ((enmPersonTypecollect) Convert.ToInt32(this.rblPersonTypeCollect.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
              {
                case enmPersonTypecollect.Otros:
                  if (!this.chklRequisiteToDeliver.Items[0].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de circulación MTC (Original).");
                  if (!this.chklRequisiteToDeliver.Items[1].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de circulación MTC (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[2].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Original).");
                  if (!this.chklRequisiteToDeliver.Items[3].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[5].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de DNI Vigente (Original).");
                  if (!this.chklRequisiteToDeliver.Items[6].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Carta Poder Juridica Legalizada");
                  break;
                case enmPersonTypecollect.RepresentanteLegal:
                  if (!this.chklRequisiteToDeliver.Items[0].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de circulación MTC (Original).");
                  if (!this.chklRequisiteToDeliver.Items[1].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de circulación MTC (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[2].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Original).");
                  if (!this.chklRequisiteToDeliver.Items[3].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[5].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de DNI Vigente (Original).");
                  if (!this.chklRequisiteToDeliver.Items[6].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Vigencia de Poder (Original)");
                  if (!this.chklRequisiteToDeliver.Items[7].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Vigencia de Poder (Copia)");
                  break;
              }
              break;
          }
        }
        if (str == "5,3")
        {
          switch (int32_2)
          {
            case enmOwnerPersonType.Natural:
              switch ((enmTypeCollect) Convert.ToInt32(this.rblTypeRecabante.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
              {
                case enmTypeCollect.Propietario:
                  if (!this.chklRequisiteToDeliver.Items[0].Selected)
                    throw new HandledException(1, "Por favor confirme recepción Declaración Jurada Simple del Cambio de Uso.");
                  if (!this.chklRequisiteToDeliver.Items[1].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Original).");
                  if (!this.chklRequisiteToDeliver.Items[2].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[4].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de DNI Vigente (Original) <br> y/o Voucher Original.");
                  break;
                case enmTypeCollect.Apoderado:
                  if (!this.chklRequisiteToDeliver.Items[0].Selected)
                    throw new HandledException(1, "Por favor confirme recepción Declaración Jurada Simple del Cambio de Uso.");
                  if (!this.chklRequisiteToDeliver.Items[1].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Original).");
                  if (!this.chklRequisiteToDeliver.Items[2].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[4].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de DNI Vigente (Original).");
                  if (!this.chklRequisiteToDeliver.Items[5].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Carta Poder Legalizada");
                  break;
              }
              break;
            case enmOwnerPersonType.Juridica:
              switch ((enmPersonTypecollect) Convert.ToInt32(this.rblPersonTypeCollect.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
              {
                case enmPersonTypecollect.Otros:
                  if (!this.chklRequisiteToDeliver.Items[0].Selected)
                    throw new HandledException(1, "Por favor confirme recepción Declaración Jurada Simple del Cambio de Uso.");
                  if (!this.chklRequisiteToDeliver.Items[1].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Original).");
                  if (!this.chklRequisiteToDeliver.Items[2].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[4].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de DNI Vigente (Original).");
                  if (!this.chklRequisiteToDeliver.Items[5].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Carta Poder Juridica Legalizada");
                  break;
                case enmPersonTypecollect.RepresentanteLegal:
                  if (!this.chklRequisiteToDeliver.Items[0].Selected)
                    throw new HandledException(1, "Por favor confirme recepción Declaración Jurada Simple del Cambio de Uso.");
                  if (!this.chklRequisiteToDeliver.Items[1].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Original).");
                  if (!this.chklRequisiteToDeliver.Items[2].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[4].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de DNI Vigente (Original).");
                  if (!this.chklRequisiteToDeliver.Items[5].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Vigencia de Poder (Original)");
                  if (!this.chklRequisiteToDeliver.Items[6].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Vigencia de Poder (Copia)");
                  break;
              }
              break;
          }
          if (int32_1 != 1 && int32_1 == 2)
          {
            if (!this.chklRequisiteToDeliver.Items[0].Selected && !this.chklRequisiteToDeliver.Items[1].Selected && !this.chklRequisiteToDeliver.Items[2].Selected && !this.chklRequisiteToDeliver.Items[4].Selected && !this.chklRequisiteToDeliver.Items[3].Selected)
              throw new HandledException(1, "Por favor confirme recepción Declaración Jurada Simple del Cambio de Uso <br> y/o Tarjeta de Propiedad (Original) <br> y/o Tarjeta de Propiedad (Copia) <br> y/o DNI Vigente (Original) <br> y/o Voucher Original.");
            if (!this.chklRequisiteToDeliver.Items[5].Selected)
              throw new HandledException(1, "Por favor confirme recepción de Carta Poder Legalizada.");
          }
        }
        if (str == "3,6")
        {
          switch (int32_2)
          {
            case enmOwnerPersonType.Natural:
              switch ((enmTypeCollect) Convert.ToInt32(this.rblTypeRecabante.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
              {
                case enmTypeCollect.Propietario:
                  if (!this.chklRequisiteToDeliver.Items[0].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de circulación MTC (Original) .");
                  if (!this.chklRequisiteToDeliver.Items[1].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de circulación MTC (Copia) .");
                  if (!this.chklRequisiteToDeliver.Items[2].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Original).");
                  if (!this.chklRequisiteToDeliver.Items[3].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[4].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de DNI Vigente (Original) .");
                  break;
                case enmTypeCollect.Apoderado:
                  if (!this.chklRequisiteToDeliver.Items[0].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de circulación MTC (Original) .");
                  if (!this.chklRequisiteToDeliver.Items[1].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de circulación MTC (Copia) .");
                  if (!this.chklRequisiteToDeliver.Items[2].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Original).");
                  if (!this.chklRequisiteToDeliver.Items[3].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[4].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de DNI Vigente (Original) .");
                  if (!this.chklRequisiteToDeliver.Items[5].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Carta Poder Legalizada.");
                  break;
              }
              break;
            case enmOwnerPersonType.Juridica:
              switch ((enmPersonTypecollect) Convert.ToInt32(this.rblPersonTypeCollect.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
              {
                case enmPersonTypecollect.Otros:
                  if (!this.chklRequisiteToDeliver.Items[0].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de circulación MTC (Original) .");
                  if (!this.chklRequisiteToDeliver.Items[1].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de circulación MTC (Copia) .");
                  if (!this.chklRequisiteToDeliver.Items[2].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Original).");
                  if (!this.chklRequisiteToDeliver.Items[3].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[4].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de DNI Vigente (Original) .");
                  if (!this.chklRequisiteToDeliver.Items[5].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Carta Poder Juridica Legalizada.");
                  break;
                case enmPersonTypecollect.RepresentanteLegal:
                  if (!this.chklRequisiteToDeliver.Items[0].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de circulación MTC (Original) .");
                  if (!this.chklRequisiteToDeliver.Items[1].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de circulación MTC (Copia) .");
                  if (!this.chklRequisiteToDeliver.Items[2].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Original).");
                  if (!this.chklRequisiteToDeliver.Items[3].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[4].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de DNI Vigente (Original) .");
                  if (!this.chklRequisiteToDeliver.Items[5].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Vigencia de Poder (Original).");
                  if (!this.chklRequisiteToDeliver.Items[6].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Vigencia de Poder (Copia).");
                  break;
              }
              break;
          }
        }
        if (str == "6,3")
        {
          switch (int32_2)
          {
            case enmOwnerPersonType.Natural:
              switch ((enmTypeCollect) Convert.ToInt32(this.rblTypeRecabante.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
              {
                case enmTypeCollect.Propietario:
                  if (!this.chklRequisiteToDeliver.Items[0].Selected)
                    throw new HandledException(1, "Por favor confirme recepción <br> Declaración Jurada Simple del Cambio de Uso.");
                  if (!this.chklRequisiteToDeliver.Items[1].Selected)
                    throw new HandledException(1, "Por favor confirme recepción Tarjeta de Propiedad (Original).");
                  if (!this.chklRequisiteToDeliver.Items[2].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[4].Selected)
                    throw new HandledException(1, "Por favor confirme recepción DNI Vigente (Original).");
                  break;
                case enmTypeCollect.Apoderado:
                  if (!this.chklRequisiteToDeliver.Items[0].Selected)
                    throw new HandledException(1, "Por favor confirme recepción <br> Declaración Jurada Simple del Cambio de Uso.");
                  if (!this.chklRequisiteToDeliver.Items[1].Selected)
                    throw new HandledException(1, "Por favor confirme recepción Tarjeta de Propiedad (Original).");
                  if (!this.chklRequisiteToDeliver.Items[2].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[4].Selected)
                    throw new HandledException(1, "Por favor confirme recepción DNI Vigente (Original).");
                  if (!this.chklRequisiteToDeliver.Items[5].Selected)
                    throw new HandledException(1, "Por favor confirme recepción Carta Poder Legalizada.");
                  break;
              }
              break;
            case enmOwnerPersonType.Juridica:
              switch ((enmPersonTypecollect) Convert.ToInt32(this.rblPersonTypeCollect.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
              {
                case enmPersonTypecollect.Otros:
                  if (!this.chklRequisiteToDeliver.Items[0].Selected)
                    throw new HandledException(1, "Por favor confirme recepción <br> Declaración Jurada Simple del Cambio de Uso.");
                  if (!this.chklRequisiteToDeliver.Items[1].Selected)
                    throw new HandledException(1, "Por favor confirme recepción Tarjeta de Propiedad (Original).");
                  if (!this.chklRequisiteToDeliver.Items[2].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[4].Selected)
                    throw new HandledException(1, "Por favor confirme recepción DNI Vigente (Original).");
                  if (!this.chklRequisiteToDeliver.Items[5].Selected)
                    throw new HandledException(1, "Por favor confirme recepción Carta Poder juridica Legalizada.");
                  break;
                case enmPersonTypecollect.RepresentanteLegal:
                  if (!this.chklRequisiteToDeliver.Items[0].Selected)
                    throw new HandledException(1, "Por favor confirme recepción <br> Declaración Jurada Simple del Cambio de Uso.");
                  if (!this.chklRequisiteToDeliver.Items[1].Selected)
                    throw new HandledException(1, "Por favor confirme recepción Tarjeta de Propiedad (Original).");
                  if (!this.chklRequisiteToDeliver.Items[2].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Tarjeta de Propiedad (Copia).");
                  if (!this.chklRequisiteToDeliver.Items[4].Selected)
                    throw new HandledException(1, "Por favor confirme recepción DNI Vigente (Original).");
                  if (!this.chklRequisiteToDeliver.Items[5].Selected)
                    throw new HandledException(1, "Por favor confirme recepción Vigencia de Poder (Original).");
                  if (!this.chklRequisiteToDeliver.Items[6].Selected)
                    throw new HandledException(1, "Por favor confirme recepción de Vigencia de Poder (Copia).");
                  break;
              }
              break;
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
