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
                var rand = new Random(); //random integers from 50 to 100. rand.Next(50, 101)
                string[] arrayCommand = Commands.Split('\n');

                foreach (string command in arrayCommand)
                {
                    string[] words = command.Split(' ');
                    switch (words[0])
                    {
                        case "M":
                        case "Move":// Move x y sleep
                            CommandMouse.Move(Convert.ToInt32(words[1]), Convert.ToInt32(words[2]));
                            System.Threading.Thread.Sleep(Convert.ToInt32(words[3]));
                            break;
                        case "MT":
                        case "MoveTime":// MoveTime x y time sleep
                            CommandMouse.MoveTime(Convert.ToInt32(words[1]), Convert.ToInt32(words[2]), Convert.ToInt32(words[3]));
                            System.Threading.Thread.Sleep(Convert.ToInt32(words[4]));
                            break;
                        case "MTR":
                        case "MoveTimeRandom":// MoveTimeRandom x y time randomFrom randomTo sleep
                            {
                                int baseTime = Convert.ToInt32(words[3]);
                                int randTime = rand.Next(Convert.ToInt32(words[4]) + Convert.ToInt32(words[5]) + 1);
                                CommandMouse.MoveTime(Convert.ToInt32(words[1]), Convert.ToInt32(words[2]), baseTime + randTime);

                                System.Threading.Thread.Sleep(Convert.ToInt32(words[6]));
                                break;
                            }
                        case "MV":
                        case "MoveVector":// MoveVector x y sleep
                            CommandMouse.MoveVector(Convert.ToInt32(words[1]), Convert.ToInt32(words[2]));
                            System.Threading.Thread.Sleep(Convert.ToInt32(words[3]));
                            break;
                        case "MVT":
                        case "MoveVectorTime":// MoveVectorTime x y time sleep
                            CommandMouse.MoveVectorTime(Convert.ToInt32(words[1]), Convert.ToInt32(words[2]), Convert.ToInt32(words[3]));
                            System.Threading.Thread.Sleep(Convert.ToInt32(words[4]));
                            break;
                        case "MVTR":
                        case "MoveVectorTimeRandom":// MoveVectorTimeRandom x y time randomFrom randomTo sleep
                            {
                                int baseTime = Convert.ToInt32(words[3]);
                                int randTime = rand.Next(Convert.ToInt32(words[4]) + Convert.ToInt32(words[5]) + 1);
                                CommandMouse.MoveVectorTime(Convert.ToInt32(words[1]), Convert.ToInt32(words[2]), baseTime + randTime);
                                System.Threading.Thread.Sleep(Convert.ToInt32(words[6]));
                                break;
                            }
                        case "F":
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
                        case "FT":
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
                        case "FTR":
                        case "FindTimeRandom":// Find image time randomFrom randomTo sleep
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
                                        int baseTimeMove = Convert.ToInt32(words[2]);
                                        int randTimeMove = rand.Next(Convert.ToInt32(words[3]), Convert.ToInt32(words[4]) + 1);
                                        CommandMouse.MoveTime(
                                            (int)(service.GetPlaces()[0].Left + service.GetPlaces()[0].Width / 2) * getScale(words[1]),
                                            (int)(service.GetPlaces()[0].Top + service.GetPlaces()[0].Height / 2) * getScale(words[1]),
                                            baseTimeMove + randTimeMove);
                                        break;
                                    }
                                }
                                BM.Dispose();
                                System.Threading.Thread.Sleep(Convert.ToInt32(words[6]));
                            }
                            break;
                        case "LD":
                        case "LeftDown":
                            CommandMouse.LeftDown();
                            System.Threading.Thread.Sleep(Convert.ToInt32(words[1]));
                            break;
                        case "LU":
                        case "LeftUp":
                            CommandMouse.LeftUp();
                            System.Threading.Thread.Sleep(Convert.ToInt32(words[1]));
                            break;
                        case "LC":
                        case "LeftClick":
                            CommandMouse.LeftClick();
                            System.Threading.Thread.Sleep(Convert.ToInt32(words[1]));
                            break;
                        case "MC":
                        case "MiddleClick":
                            CommandMouse.MiddleClick();
                            System.Threading.Thread.Sleep(Convert.ToInt32(words[1]));
                            break;
                        case "RD":
                        case "RightDown":
                            CommandMouse.RightDown();
                            System.Threading.Thread.Sleep(Convert.ToInt32(words[1]));
                            break;
                        case "RU":
                        case "RightUp":
                            CommandMouse.RightUp();
                            System.Threading.Thread.Sleep(Convert.ToInt32(words[1]));
                            break;
                        case "RC":
                        case "RightClick":
                            CommandMouse.RightClick();
                            System.Threading.Thread.Sleep(Convert.ToInt32(words[1]));
                            break;
                        case "S":
                        case "Sleep":
                            System.Threading.Thread.Sleep(Convert.ToInt32(words[1]));
                            break;
                        case "SR":
                        case "SleepRandom":
                            {
                                int baseTimeSleep = Convert.ToInt32(words[1]);
                                int randomTimeSleep = rand.Next(Convert.ToInt32(words[2]) + Convert.ToInt32(words[3]) + 1);
                                System.Threading.Thread.Sleep(baseTimeSleep + randomTimeSleep);
                                break;
                            }
                        case "K":
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
