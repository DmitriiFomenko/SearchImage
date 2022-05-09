
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchImage.AForge
{
    public class SearchModel
    {
        public int Id { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Similarity { get; set; }

        public string Info()
        {
            return $"ID: {Id}, Left: {Left}, Top: {Top}, Width: {Width}, Height: {Height}, Similarity: {Similarity}";
        }
    }
}
//List<SearchModel> places;

//AforgeService service = new AforgeService();

//try
//{
//    using (OverrideCursor cursor = OverrideCursor.GetWaitOverrideCursor())
//    {
//        bool isContains = await service.IsContains("assets/original.jpg", "assets/image.jpg");
//        if (isContains)
//        {
//            places = service.GetPlaces();
//        }
//        else
//        {
//            places = new List<SearchModel>();
//        }
//    }
//}
//catch (Exception ex)
//{
//    var message = $"Возникла ошибка: {ex.Message}";
//    System.Windows.MessageBox.Show(message, "Ошибка");
//}
//finally
//{
//    System.Windows.MessageBox.Show($"Найдено мест: {service.CountMatchings},\n\n {service.GetPlaces()[0].Info()},\n {service.GetPlaces()[1].Info()},");
//}