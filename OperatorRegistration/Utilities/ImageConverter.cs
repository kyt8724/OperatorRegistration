using System;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace OperatorRegistration.Utilities
{
    /// <summary>
    /// Scripter : YONGTOK KIM
    /// Scripted Date : 17 Jan 2012  
    /// Image Converter
    /// </summary>
    /// 
    public class ImageConverter
    {
        private string root = Application.StartupPath;
        private FileStream fs;

        // Get Image From Database
        public BitmapImage FromDBToImage(Byte[] fromDB)
        {
            if (fromDB.Length > 0)
            {
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.CreateOptions = BitmapCreateOptions.None;
                bi.CacheOption = BitmapCacheOption.Default;
                bi.StreamSource = new MemoryStream(fromDB);
                bi.EndInit();
                return bi;
            }
            return null;
        }

        // Get Image From file
        public BitmapImage FromFileToImage(string filename)
        {
            string file = Path.Combine(root, filename);
            if (File.Exists(file))
            {
                byte[] buffer = File.ReadAllBytes(file);

                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = new MemoryStream(buffer);
                bi.EndInit();

                return bi;
            }

            return null;
        }

        // Save image to file
        public void SaveImageToFile(string filename, BitmapSource bi)
        {
            string file = Path.Combine(root, filename);
            fs = new FileStream(file, FileMode.Create);

            BitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bi));
            encoder.Save(fs);
            fs.Close();
        }

        // Get byte from file
        public Byte[] FromFileToByte(string filename)
        {
            string file = Path.Combine(root, filename);
            Byte[] buffer = null;
            if (File.Exists(file))
                buffer = File.ReadAllBytes(file);

            return buffer;
        }
    }
}
