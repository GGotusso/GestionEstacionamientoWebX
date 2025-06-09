using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Services.Facade;
using Services.DOMAIN;

namespace GestionEstacionamiento
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Funcion para que tome los modulos que entra el usuario y los mande a la bitacora con LogService
            if (!IsPostBack && Session["Usuario"] is Usuario user)
            {
                string modulo = Page.Title;
                if (string.IsNullOrWhiteSpace(modulo))
                {
                    modulo = System.IO.Path.GetFileNameWithoutExtension(Request.AppRelativeCurrentExecutionFilePath);
                }
                LogService.WriteLog(TraceLevel.Info, $"{user.UserName} ingreso al modulo {modulo}");
            }
        }
    }
}