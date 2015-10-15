using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using OperatorRegistration.Utilities;

namespace OperatorRegistration
{
    /// <summary>
    /// Scripter : YONGTOK KIM
    /// SCRIPTED DATE : 20 Jan 2012
    /// MOVE TO MAIN WINDOW AFTER LOGIN
    /// </summary>
    public partial class CapturePhoto : Window
    {
        public BitmapSource bitmap;
        public CapturePhoto()
        {
            InitializeComponent();
        }

        private IDSWebCam idsWebCam;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            idsWebCam = new IDSWebCam();
            idsWebCam.InitializeWebCam(ref imgVideo);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            idsWebCam.Start();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            idsWebCam.Stop();
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            //idsWebCam.Continue();
            idsWebCam.Start();
        }

        private void btnCapture_Click(object sender, RoutedEventArgs e)
        {
            bitmap = (BitmapSource)imgVideo.Source;
            if (bitmap != null)
            {
                Bitmap toBitmap = ToBitmap(bitmap);
                bitmap = BitmapSourceFromBitmap(toBitmap);
            }
            idsWebCam.Stop();
            Close();
        }


        private static Bitmap ToBitmap(BitmapSource bitmapsource)
        {
            Bitmap toBitmap;
            using (MemoryStream stream = new MemoryStream())
            {
                // from System.Media.BitmapImage to System.Drawing.Bitmap
                PngBitmapEncoder enc = new PngBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                enc.Save(stream);
                toBitmap = new System.Drawing.Bitmap(stream);
            }


            Rectangle cropArea = new Rectangle(70, 0, 180, 240);
            Bitmap bmpCrop = toBitmap.Clone(cropArea, toBitmap.PixelFormat);

            return bmpCrop;
        }

        private static BitmapSource BitmapSourceFromBitmap(Bitmap fromBitmap)
        {
            BitmapSource bitmapSource;

            IntPtr hBitmap = fromBitmap.GetHbitmap();
            BitmapSizeOptions sizeOptions = BitmapSizeOptions.FromEmptyOptions();
            bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, sizeOptions);
            bitmapSource.Freeze();

            return bitmapSource;
        }
    }
}
