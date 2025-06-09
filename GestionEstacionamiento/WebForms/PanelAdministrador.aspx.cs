using Services.Facade;
using System;
using System.IO;
using System.Web.UI;

namespace GestionEstacionamiento.WebForms
{
    public partial class PanelAdministrador : Page
    {
        protected void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                string folder = txtRuta.Text.Trim();
                if (string.IsNullOrWhiteSpace(folder))
                {
                    lblMensaje.Text = "Debe ingresar la carpeta donde guardar el backup";
                    return;
                }

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                string userFile = Path.Combine(folder, $"Usuario_{timestamp}.bak");
                string logFile = Path.Combine(folder, $"Log_{timestamp}.bak");

                BackupService.BackupDatabase("User_Conecction", userFile);
                BackupService.BackupDatabase("Log_Conecction", logFile);

                lblMensaje.Text = $"Backup realizado en {folder}";
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
                if (!fuRestore.HasFile)
                {
                    lblMensaje.Text = "Debe seleccionar un archivo de backup";
                    return;
                }

                string tempPath = Server.MapPath($"~/App_Data/{Path.GetFileName(fuRestore.FileName)}");
                fuRestore.SaveAs(tempPath);

                BackupService.RestoreDatabase("User_Conecction", tempPath);
                BackupService.RestoreDatabase("Log_Conecction", tempPath);

                File.Delete(tempPath);

                lblMensaje.Text = "Restauración completada";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = $"Error al restaurar: {ex.Message}";
            }
        }
    }
}
