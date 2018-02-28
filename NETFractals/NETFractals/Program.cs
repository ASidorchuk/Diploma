using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu;
using System.IO;

namespace NETFractals
{
    class Program
    {
        static void Main(string[] args)
        {
            String win1 = "Test Window"; //The name of the window
            CvInvoke.NamedWindow(win1); //Create the window using the specific name

            string fileName = @"..\..\Images\lena.bmp";
            Image<Gray, byte> img = new Image<Gray, byte>(fileName);

            Console.WriteLine(Directory.GetCurrentDirectory());

            CvInvoke.Imshow(win1, img); //Show the image
            CvInvoke.WaitKey(0);  //Wait for the key pressing event
            CvInvoke.DestroyWindow(win1); //Destroy the window if key is pressed

        }

        //static void RunProgram(string imageName, int)
        //{

        //}
    }
}
