using Emgu.CV;
using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace image_display
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();



        }

        private void Grid_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Paste_Click(object sender, RoutedEventArgs e)
        {

        }




        public struct NIDAQ_Parameters
        {
            public string Name { set; get; }
            public int channelNumberAO { set; get; }
            public int channelNumberAI { set; get; }

            public int samplingRate { set; get; }
            public int pixelNumberX { set; get; }
            public int pixelNumberY { set; get; }
            public double centerPositionX { set; get; }
            public double centerPositionY { set; get; }

            public double pixelDwellTime { set; get; }
            public double samplesPerPixel { set; get; }

            public double scale { set; get; }
            public double responseTime { set; get; }
            public double extraPoints { set; get; }

            public double voltageStepSize { set; get; }

            public enum ModeSelection { DC, AC, TP };

            public ModeSelection modeSelection { set; get; }
            public int cycleNumber { set; get; }

            public BitmapSource BSDisplay { set; get; }

            public WriteableBitmap bmp { set; get; }

            public ImageSource imageSource { set; get; }
        }
        public struct SampleStage
        {
  
            public string Name;
            public int axisNumber;

            public enum Axis { X, Y, Z }

            public int X, Y, Z;//位置


            public int X_position { set; get; }
            public int X_initialVelocity { set; get; }
            public int X_acceleration { set; get; }
            public int X_velocity { set; get; }

            public int Y_position { set; get; }
            public int Y_initialVelocity { set; get; }
            public int Y_acceleration { set; get; }
            public int Y_velocity { set; get; }

            public int Z_position;
            public int Z_initialVelocity;
            public int Z_acceleration;
            public int Z_velocity;


        }
        public struct ControlParameters
        {
            public string Name { set; get; }
            public SampleStage Stage_SC300;
            public NIDAQ_Parameters USB6351_galvoPMT;

        }

        ControlParameters imPara;
        Mat cvImage;


        WriteableBitmap bmp;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.DateTime currentTime = new System.DateTime();
            int millisec0 = currentTime.Millisecond;

            for (int i = 0; i < 100001; i++) { 
                    
                //
                     Task.Factory.StartNew(Work);

            }


            Thread.Sleep(10000);

            MessageBox.Show(Convert.ToString(updataNum));



        }


        private void Work()
        {
            Task task = new Task((image) => Begin(this.image), this.image);

            task.Start();

        }

        private void Begin(System.Windows.Controls.Image img)
        {
            Matrix<int> matrix8Bit = new Matrix<int>(512, 512, 3);
            int[,] imgData = new int[512, 512];
            for (int i = 0; i < 512; i++)
            {
                for (int j = 0; j < 512; j++)
                {
                    // imgData[i, j] = imgData[i, j] * 256 / voltageRange;
                    if (imgData[i, j] < 0)
                    {
                        imgData[i, j] = 0;
                    }
                    // matrix8Bit.Data.SetValue(Convert.ToInt32(imgData[i, j]), i, j);

                    Random random = new Random();

                    int Num = random.Next(0, 255);


                    matrix8Bit.Data.SetValue(Convert.ToInt32(Num), i, j);
                }
            }//生产一幅图像

             cvImage = matrix8Bit.Mat;

            Action<System.Windows.Controls.Image, String> updateAction = new Action<System.Windows.Controls.Image, string>(UpdateTb);
            img.Dispatcher.BeginInvoke(updateAction, img, "");
        }

        int updataNum=0;

        private void UpdateTb(System.Windows.Controls.Image img, string text)
        {
            //tb.Text = text;

            img.Source=cvImage.ToBitmapSource();

            updataNum++;
        }



        private void imageUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {

        }
    }
}
