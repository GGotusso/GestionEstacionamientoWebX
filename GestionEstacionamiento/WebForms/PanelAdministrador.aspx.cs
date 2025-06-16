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

        //Utilizado para recalcular el DVH de todos los usuarios si cambio la logica del DVH o se agregaron nuevos campos
        protected void btnRecalcularDVH_Click(object sender, EventArgs e)
        {
            try
            {
                UserService.RecalcularDVHUsuarios();
                lblMensaje.Text = "DVH de todos los usuarios recalculado correctamente.";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = $"Error al recalcular DVH: {ex.Message}";
            }
        }
        protected void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                if (!fuRestoreUsuario.HasFile || !fuRestoreLog.HasFile)
                {
                    lblMensaje.Text = "Debe seleccionar ambos archivos de backup (Usuario y Log)";
                    return;
                }

                // Guardamos temporalmente los dos archivos
                string tempPathUsuario = Server.MapPath($"~/App_Data/{Path.GetFileName(fuRestoreUsuario.FileName)}");
                string tempPathLog = Server.MapPath($"~/App_Data/{Path.GetFileName(fuRestoreLog.FileName)}");

                fuRestoreUsuario.SaveAs(tempPathUsuario);
                fuRestoreLog.SaveAs(tempPathLog);

                // Restauramos cada base con su archivo correspondiente
                BackupService.RestoreDatabase("User_Conecction", tempPathUsuario);
                BackupService.RestoreDatabase("Log_Conecction", tempPathLog);

                // Borramos los archivos temporales
                File.Delete(tempPathUsuario);
                File.Delete(tempPathLog);

                lblMensaje.Text = "Restauración completada correctamente.";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = $"Error al restaurar: {ex.Message}";
            }
        }
    }
}
