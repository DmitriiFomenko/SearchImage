using SearchImage.AForge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchImage.Classes
{
    class CommandAForge
    {
        static public async Task<AforgeService> GetServiceAsync(System.Drawing.Bitmap image, System.Drawing.Bitmap needFind)
        {
            AforgeService service = new AforgeService();
            try
            {
                await service.IsContains(image, needFind);
            }
            catch (Exception ex)
            {
                var message = $"Возникла ошибка: {ex.Message}";
                System.Windows.MessageBox.Show(message, "Ошибка");
            }
            finally
            {
                //System.Windows.MessageBox.Show($"Найдено мест: {service.CountMatchings},\n\n {service.GetPlaces()[0].Info()}");
            }
            return service;
        }
    }
}
