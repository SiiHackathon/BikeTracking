using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace BikeTracker.Helpers
{
	public class ImageHelper
	{
		public static Image CropToCircle(Image srcImage, Color backGround)
		{
			Image dstImage = new Bitmap(60, 60, PixelFormat.Format32bppArgb);
			Graphics g = Graphics.FromImage(dstImage);
			using (Brush br = new SolidBrush(backGround))
			{
				g.FillRectangle(br, 0, 0, dstImage.Width, dstImage.Height);
			}
			GraphicsPath path = new GraphicsPath();
			path.AddEllipse(0, 0, dstImage.Width, dstImage.Height);
			g.SetClip(path);
			g.DrawImage(srcImage, 0, 0);

			return dstImage;
		}
	}
}