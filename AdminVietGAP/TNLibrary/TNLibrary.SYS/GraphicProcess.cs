using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace TNLibrary.SYS
{
    public class GraphicProcess
    {
        public static byte[] ImageToByte(PictureBox Pic)
        {

            MemoryStream fs = new MemoryStream();
            Pic.Image.Save(fs, Pic.Image.RawFormat);
            // define te binary reader to read the bytes of image 

            BinaryReader br;

            br = new BinaryReader(fs);
            // define the byte array of filelength 

            byte[] imgbyte = new byte[fs.Length + 1];

            // read the bytes from the binary reader 
            imgbyte = fs.GetBuffer();

            br.Close();
            // close the binary reader 

            fs.Close();
            // close the  stream 

            return imgbyte;
        }
        public static void ViewImage(PictureBox Pic, byte[] arrPicture)
        {
            MemoryStream ms;

            ms = new MemoryStream(arrPicture);
            Pic.Image = Image.FromStream(ms);

            ms.Close();
        }

    }
}
