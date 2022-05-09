using AForge.Imaging;
using AForge.Imaging.Filters;
using SearchImage.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchImage.AForge
{
    public class AforgeService
    {
        //найденные совпадения
        private TemplateMatch[] _matchings;

        /// <summary>
        /// Количество найденных совпадений
        /// </summary>
        public int CountMatchings
        {
            get => _matchings != null ? _matchings.Length : 0;
        }


        //ctor
        public AforgeService()
        {

        }

        /// <summary>
        /// Содержит ли исходное изображение представленый образец
        /// </summary>
        /// <param name="pathOriginalImage">путь к файлу исходного изображения</param>
        /// <param name="pathSampleImage">путь к файлу образца</param>
        /// <returns>true если содержит</returns>
        public async Task<bool> IsContains(Bitmap pathOriginalImage, Bitmap pathSampleImage)
        {
            //if (String.IsNullOrEmpty(pathOriginalImage)) throw new ArgumentNullException(nameof(pathOriginalImage));
            //if (String.IsNullOrEmpty(pathSampleImage)) throw new ArgumentNullException(nameof(pathSampleImage));

            int minSize = pathSampleImage.Height > pathSampleImage.Width ? pathSampleImage.Width : pathSampleImage.Height;

            int scale;
            if (minSize > 500)
                scale = 10;
            else if (minSize > 100)
                scale = 5;
            else if (minSize > 50)
                scale = 2;
            else
                scale = 1;

            var sample = ResizeImage(pathSampleImage, scale);
            var orig = ResizeImage(pathOriginalImage, scale);

            //sample.Save("assets/!sample.jpg");
            //orig.Save("assets/!orig.jpg");

            ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0.9f); //TODO: процентное соотношение
            _matchings = await Task.Run(() => tm.ProcessImage(orig, sample));

            sample.Dispose();
            orig.Dispose();

            return _matchings.Any();
        }

        public static Bitmap ResizeImage(Bitmap image, int scale)
        {
            var destRect = new Rectangle(0, 0, image.Width / scale, image.Height / scale);
            var destImage = new Bitmap(image.Width / scale, image.Height / scale, PixelFormat.Format24bppRgb);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        private void LowQualityLevel(string path)
        {
            // Get a bitmap. The using statement ensures objects  
            // are automatically disposed from memory after use.  
            using (Bitmap bmp1 = new Bitmap(path))
            {
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                System.Drawing.Imaging.Encoder myEncoder =
                    System.Drawing.Imaging.Encoder.Quality;

                EncoderParameters myEncoderParameters = new EncoderParameters(1);

                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 15L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                bmp1.Save("assets/orig1.jpg", jpgEncoder, myEncoderParameters);
            }
        }
        private void VaryQualityLevel(string path)
        {
            // Get a bitmap. The using statement ensures objects  
            // are automatically disposed from memory after use.  
            using (Bitmap bmp1 = new Bitmap(path))
            {
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                // Create an Encoder object based on the GUID  
                // for the Quality parameter category.  
                System.Drawing.Imaging.Encoder myEncoder =
                    System.Drawing.Imaging.Encoder.Quality;

                // Create an EncoderParameters object.  
                // An EncoderParameters object has an array of EncoderParameter  
                // objects. In this case, there is only one  
                // EncoderParameter object in the array.  
                EncoderParameters myEncoderParameters = new EncoderParameters(1);

                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                bmp1.Save(path + 1, jpgEncoder, myEncoderParameters);

                myEncoderParameter = new EncoderParameter(myEncoder, 100L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                bmp1.Save(path + 2, jpgEncoder, myEncoderParameters);

                // Save the bitmap as a JPG file with zero quality level compression.  
                myEncoderParameter = new EncoderParameter(myEncoder, 0L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                bmp1.Save(path + 3, jpgEncoder, myEncoderParameters);

            }
        }
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        /// <summary>
        /// Получение коллекции найденных мест где находится образец
        /// </summary>
        /// <returns>коллекция найденных мест</returns>
        public List<SearchModel> GetPlaces()
        {
            List<SearchModel> result = new List<SearchModel>();
            if (CountMatchings == 0) return result;

            int id = 0;
            foreach (var match in _matchings)
            {
                SearchModel place = new SearchModel
                {

                    Id = ++id,
                    Similarity = match.Similarity,
                    Top = match.Rectangle.Top,
                    Left = match.Rectangle.Left,
                    Height = match.Rectangle.Height,
                    Width = match.Rectangle.Width
                };

                result.Add(place);
            }

            return result;
        }

    }
}
