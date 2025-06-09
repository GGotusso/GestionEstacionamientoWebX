using Services.Facade;
using System;
using System.Web.UI;

namespace GestionEstacionamiento.WebForms
{
    public partial class PanelAdministrador : Page
    {
        protected void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                string path = txtRuta.Text.Trim();
                BackupService.BackupDatabase("User_Conecction", path);
                BackupService.BackupDatabase("Log_Conecction", path);
                lblMensaje.Text = "Backup realizado";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = $"Error al realizar backup: {ex.Message}";
            }
        }

        protected void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                string path = txtRuta.Text.Trim();
                BackupService.RestoreDatabase("User_Conecction", path);
                BackupService.RestoreDatabase("Log_Conecction", path);
                lblMensaje.Text = "Restauración completada";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = $"Error al restaurar: {ex.Message}";
            }
        }
    }
}
