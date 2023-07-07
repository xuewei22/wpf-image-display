using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            public Bitmap bmp { set; get; }
        }
        public struct SampleStage
        {
            //    public SampleStage()
            //    {
            //        Name = "";
            //        X_position =222;//
            //        X_initialVelocity = 222;

            //    X_acceleration =222;//{ set; get; }
            //        X_velocity = 222;//{ set; get; }

            //        Y_position = 222;//{ set; get; }
            //        Y_initialVelocity = 222;// { set; get; }
            //        Y_acceleration = 222;//{ set; get; }
            //        Y_velocity = 222;//{ set; get; }

            //        Z_position = 222;//;
            //        Z_initialVelocity = 222;//;
            //        Z_acceleration = 222;//;
            //        Z_velocity = 222;//;
            //     axisNumber=2;


            //    X= Y= Z=222;//位置
            //}

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            Thread thread=new Thread(ThreadStart);  
            
            
            TEXTBLOCK.Text = "clicked";



            Matrix<int> matrix8Bit = new Matrix<int>(512, 512, 3);


            double voltageRange = 10;
            double grayScacleRange = 65536;

            //根据测量范围把double电压值转换为像素灰度值  

            if (true) {  int[,] imgData = new int[512, 512];
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
                    matrix8Bit.Data.SetValue(Convert.ToInt32(166), i, j);
                }
            }



            cvImage = matrix8Bit.Mat;
            //直方图校准
            BitmapSource bs = cvImage.ToBitmapSource();
            Bitmap bmp = cvImage.ToBitmap();
            //BitmapImage bitmapImage = cvImage.;
            //多线程调用
            imPara.USB6351_galvoPMT.BSDisplay = cvImage.ToBitmapSource();
            //imPara.USB6351_galvoPMT.BSDisplay = bmp;
            Dispatcher.Invoke(new Action(() =>
            {
                imPara.USB6351_galvoPMT.bmp = cvImage.ToBitmap();
            }));
            image.Source = imPara.USB6351_galvoPMT.BSDisplay;
       }


            }
    }
}
