using SearchImage.AForge;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchImage.Classes
{
    class CommandEvent
    {
        public static int getScale(string path)
        {
            Bitmap bm = new Bitmap(path);
            int minSize = bm.Height > bm.Width ? bm.Width : bm.Height;
            bm.Dispose();

            int scale;
            if (minSize > 500)
                scale = 10;
            else if (minSize > 100)
                scale = 5;
            else if (minSize > 50)
                scale = 2;
            else
                scale = 1;



            return scale;
        }
        static public async void Read(string Commands)
        {
            try
            {
                string[] arrayCommand = Commands.Split('\n');
                foreach (string command in arrayCommand)
                {
                    string[] words = command.Split(' ');
                    switch (words[0])
                    {
                        case "Move":// Move x y sleep
                            CommandMouse.Move(Convert.ToInt32(words[1]), Convert.ToInt32(words[2]));
                            System.Threading.Thread.Sleep(Convert.ToInt32(words[3]));
                            break;
                        case "MoveTime":// MoveTime x y time sleep
                            CommandMouse.MoveTime(Convert.ToInt32(words[1]), Convert.ToInt32(words[2]), Convert.ToInt32(words[3]));
                            System.Threading.Thread.Sleep(Convert.ToInt32(words[4]));
                            break;
                        case "MoveVector":// MoveVector x y sleep
                            CommandMouse.MoveVector(Convert.ToInt32(words[1]), Convert.ToInt32(words[2]));
                            System.Threading.Thread.Sleep(Convert.ToInt32(words[3]));
                            break;
                        case "MoveVectorTime":// MoveVectorTime x y time sleep
                            CommandMouse.MoveVectorTime(Convert.ToInt32(words[1]), Convert.ToInt32(words[2]), Convert.ToInt32(words[3]));
                            System.Threading.Thread.Sleep(Convert.ToInt32(words[4]));
                            break;
                        case "Find":// Find image sleep
                            {
                                Bitmap BM = new Bitmap(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width,
                                    System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                                Graphics GH = Graphics.FromImage(BM);
                                AforgeService service;
                                while (true)
                                {
                                    GH.CopyFromScreen(0, 0, 0, 0, BM.Size);
                                    service =
                                        await CommandAForge.GetServiceAsync(BM, new Bitmap(words[1]));

                                    if (service.CountMatchings > 0)
                                    {
                                        CommandMouse.Move(
                                            (int)(service.GetPlaces()[0].Left + service.GetPlaces()[0].Width / 2) * getScale(words[1]),
                                            (int)(service.GetPlaces()[0].Top + service.GetPlaces()[0].Height / 2) * getScale(words[1]));
                                        break;
                                    }
                                }
                                BM.Dispose();
                                System.Threading.Thread.Sleep(Convert.ToInt32(words[2]));
                            }
                            break;
                        case "FindTime":// Find image time sleep
                            {

                                Bitmap BM = new Bitmap(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width,
                                    System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                                Graphics GH = Graphics.FromImage(BM);
                                AforgeService service;
                                while (true)
                                {

                                    GH.CopyFromScreen(0, 0, 0, 0, BM.Size);
                                    service =
                                        await CommandAForge.GetServiceAsync(BM, new Bitmap(words[1]));

                                    if (service.CountMatchings > 0)
                                    {

                                        CommandMouse.MoveTime(
                                            (int)(service.GetPlaces()[0].Left + service.GetPlaces()[0].Width / 2) * getScale(words[1]),
                                            (int)(service.GetPlaces()[0].Top + service.GetPlaces()[0].Height / 2) * getScale(words[1]),
                                            Convert.ToInt32(words[2]));
                                        break;
                                    }
                                }
                                BM.Dispose();
                                System.Threading.Thread.Sleep(Convert.ToInt32(words[3]));
                            }
                            break;
                        case "LeftDown":
                            CommandMouse.LeftDown();
                            System.Threading.Thread.Sleep(Convert.ToInt32(words[1]));
                            break;
                        case "LeftUp":
                            CommandMouse.LeftUp();
                            System.Threading.Thread.Sleep(Convert.ToInt32(words[1]));
                            break;
                        case "LeftClick":
                            CommandMouse.LeftClick();
                            System.Threading.Thread.Sleep(Convert.ToInt32(words[1]));
                            break;
                        case "MiddleClick":
                            CommandMouse.MiddleClick();
                            System.Threading.Thread.Sleep(Convert.ToInt32(words[1]));
                            break;
                        case "RightDown":
                            CommandMouse.RightDown();
                            System.Threading.Thread.Sleep(Convert.ToInt32(words[1]));
                            break;
                        case "RightUp":
                            CommandMouse.RightUp();
                            System.Threading.Thread.Sleep(Convert.ToInt32(words[1]));
                            break;
                        case "RightClick":
                            CommandMouse.RightClick();
                            System.Threading.Thread.Sleep(Convert.ToInt32(words[1]));
                            break;
                        case "Sleep":
                            System.Threading.Thread.Sleep(Convert.ToInt32(words[1]));
                            break;
                        case "Keys":
                            CommandWin.Keys(words[1]);
                            System.Threading.Thread.Sleep(Convert.ToInt32(words[2]));
                            break;
                        default:

                            System.Windows.MessageBox.Show($"Команда не найдена {words[0]}\n {string.Join(" ", words)}");

                            break;
                    }

                }
            }
            catch (Exception ex)
            {

                System.Windows.MessageBox.Show("Ошибка в написании команд\n" + ex);
            }
        }
    }
}
