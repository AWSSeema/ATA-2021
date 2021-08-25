using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;


namespace TCAssociationTool.Areas.Admin.Models
{
    public class CaptchaImageResult:ActionResult
    {
        public Random rand = new Random();
        public string code = "";

        public string GetCaptchaString(int length)
        {
            int intZero = '0';
            int intNine = '9';
            int intA = 'A';
            int intZ = 'Z';
            int inta = 'a';
            int intz = 'z';
            int intCount = 0;
            int intRandomNumber = 0;
            string strCaptchaString = "";

            Random random = new Random(System.DateTime.UtcNow.Millisecond);

            while (intCount < length)
            {
                intRandomNumber = random.Next(intZero, intZ);
                if (((intRandomNumber >= intZero) && (intRandomNumber <= intNine) || (intRandomNumber >= intA) && (intRandomNumber <= intZ) || (intRandomNumber >= inta) && (intRandomNumber <= intz)))
                {
                    strCaptchaString = strCaptchaString + (char)intRandomNumber;
                    intCount = intCount + 1;
                }
            }
            return strCaptchaString;
        }


        public override void ExecuteResult(ControllerContext context)
        {
            code = GetRandomText();
            context.HttpContext.Session["captchastring"] = code;
            Bitmap bitmap = new Bitmap(200, 60, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bitmap);
            Pen pen = new Pen(Color.Yellow);
            Rectangle rect = new Rectangle(0, 0, 200, 60);
            SolidBrush blue = new SolidBrush(Color.CornflowerBlue);
            SolidBrush black = new SolidBrush(Color.Black);
            int counter = 0;
            g.DrawRectangle(pen, rect);
            g.FillRectangle(blue, rect);
            for (int i = 0; i < code.Length; i++)
            {
                g.DrawString(code[i].ToString(), new Font("Tahoma", 10 + rand.Next(15, 20), FontStyle.Italic), black, new PointF(10 + counter, 10));
                counter += 28;
            }

            DrawRandomLines(g);
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = "image/jpeg";
            bitmap.Save(response.OutputStream, ImageFormat.Gif);
            g.Dispose();
            bitmap.Dispose();



        }
        private void DrawRandomLines(Graphics g)
        {
            SolidBrush yellow = new SolidBrush(Color.Yellow);
            for (int i = 0; i < 20; i++)
            { g.DrawLines(new Pen(yellow, 1), GetRandomPoints()); }

        }
        private Point[] GetRandomPoints()
        {
            Point[] points = { new Point(rand.Next(0, 150), rand.Next(1, 150)), new Point(rand.Next(0, 200), rand.Next(1, 190)) };
            return points;
        }
        private string GetRandomText()
        {
            StringBuilder randomText = new StringBuilder();
            string alphabets = "012345679ACEFGHKLMNPRSWXZabcdefghijkhlmnopqrstuvwxyz";
            Random r = new Random();
            for (int j = 0; j <= 5; j++)
            { randomText.Append(alphabets[r.Next(alphabets.Length)]); }
            return randomText.ToString();
        }
    }
}