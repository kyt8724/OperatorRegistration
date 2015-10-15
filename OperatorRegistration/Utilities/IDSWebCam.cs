using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCam_Capture;
using System.Windows.Controls;
using System.Threading;

namespace OperatorRegistration.Utilities
{
    public class IDSWebCam
    {
        /// <summary>
        /// Scripter : YONGTOK KIM
        /// Scripted Date : 20 Jan 2012
        /// Description : Control to show the image to the Image Control
        /// <summany>

        private WebCamCapture webcam;
        private Image frameImage;
        private int frameNumber = 30;

        public void InitializeWebCam(ref Image ImageControl)
        {
            webcam = new WebCamCapture();
            webcam.FrameNumber = ((ulong)(0ul));
            webcam.TimeToCapture_milliseconds = frameNumber;
            webcam.ImageCaptured +=new WebCamCapture.WebCamEventHandler(webcam_ImageCaptured);
            frameImage = ImageControl;
        }

        void webcam_ImageCaptured(object source, WebcamEventArgs e)
        {
            frameImage.Source = IDSWebCamHelper.LoadBitmap((System.Drawing.Bitmap)e.WebCamImage);
        }

        public void Start()
        {
            Thread.Sleep(500);
            webcam.TimeToCapture_milliseconds = frameNumber;
            webcam.Start(0);
        }

        public void Stop()
        {
            webcam.Stop();
        }

        public void Continue()
        {
            // Change the capture time frame
            webcam.TimeToCapture_milliseconds = frameNumber;

            // resume the video capture from the stop
            webcam.Start(this.webcam.FrameNumber);
        }

        public void ResolutionSetting()
        {
            webcam.Config();
        }

        public void AdvanceSetting()
        {
            webcam.Config2();
        }
    }
}
