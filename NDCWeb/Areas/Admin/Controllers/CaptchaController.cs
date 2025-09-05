using NDCWeb.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.SessionState;

namespace NDCWeb.Areas.Admin.Controllers
{
    [SessionState(SessionStateBehavior.Default)]
    public class CaptchaController : Controller
    {
        // GET: Admin/Captcha
        public ActionResult Index()
        {
            return View();
        }
        public FileResult GetCaptchaImage()
        {
            string text = secConst.GetRandomText(); //Session["CAPTCHA"].ToString();
            secConst.cCaptext = text;
            Session["CAPTCHA"] = text;
            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            Font font = new Font("Arial", 22);
            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width + 50, (int)textSize.Height + 20);
            drawing = Graphics.FromImage(img);

            Pen redPen = new Pen(Color.Red, 1);
            Pen bluePen = new Pen(Color.LightGray, 2);
            Pen greenPen = new Pen(Color.Green, 3);
            Pen greyPen = new Pen(Color.Gray, 4);

            Color backColor = Color.Black;
            Color textColor = Color.LightGray;
            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 20, 10);
            //int X1 = 1, Y1 = 1, X2 = 100, Y2 = 100;
            //drawing.DrawLine(redPen, X1, Y1, X2, Y2);
            PointF ptf1 = new PointF(10.0F, 10.0f);
            PointF ptf2 = new PointF(400.0F, 80.0f);

            PointF ptf3 = new PointF(10.0F, 40.0f);
            PointF ptf4 = new PointF(200.0F, 0.0f);

            PointF ptf5 = new PointF(5.0F, 30.0f);
            PointF ptf6 = new PointF(100.0F, 0.0f);

            PointF ptf7 = new PointF(20.0F, 30.0f);
            PointF ptf8 = new PointF(200.0F, 0.0f);

            drawing.DrawLine(bluePen, ptf1, ptf2);
            drawing.DrawLine(redPen, ptf3, ptf4);
            drawing.DrawLine(greenPen, ptf5, ptf6);
            drawing.DrawLine(greenPen, ptf5, ptf6);

            drawing.Save();

            font.Dispose();
            textBrush.Dispose();
            drawing.Dispose();
            redPen.Dispose();
            bluePen.Dispose();
            greenPen.Dispose();
            greyPen.Dispose();
   
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            img.Dispose();

            return File(ms.ToArray(), "image/png");
        }

    }
}