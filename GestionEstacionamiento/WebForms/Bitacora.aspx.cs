using Services.DOMAIN;
using Services.Facade;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace GestionEstacionamiento.WebForms
{
    public partial class Bitacora : Page
    {
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            DateTime? desde = string.IsNullOrWhiteSpace(txtDesde.Text) ? (DateTime?)null : DateTime.Parse(txtDesde.Text);
            DateTime? hasta = string.IsNullOrWhiteSpace(txtHasta.Text) ? (DateTime?)null : DateTime.Parse(txtHasta.Text);
            TraceLevel? nivel = string.IsNullOrEmpty(ddlNivel.SelectedValue) ? (TraceLevel?)null : (TraceLevel)Enum.Parse(typeof(TraceLevel), ddlNivel.SelectedValue);
            string mensaje = txtMensaje.Text.Trim();

            List<LogEntry> logs = LogService.SearchLogs(desde, hasta, nivel, mensaje);
            gvLogs.DataSource = logs;
            gvLogs.DataBind();
        }
    }
}
