using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;

namespace DOL.WEBAPP.Extension.GenerateDocument
{
    public class PaperGeneration
    {
        FontCollection collection = new();
        FontFamily family;
        Image paper;
        Font font;
        string path = "resource";


        public PaperGeneration(string templateName)
        {
            using (var client = new WebClient())
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                byte[] bytes_fonts = client.DownloadData($"{path}/fonts/THSarabunNew/THSarabunNew Bold.ttf");
                Stream stream_fonts = new MemoryStream(bytes_fonts);
                family = collection.Add(stream_fonts);

                byte[] bytes_images = client.DownloadData($"{path}/images/{templateName}");
                Stream stream_images = new MemoryStream(bytes_images);
                paper = Image.Load(stream_images);
                font = family.CreateFont(16, FontStyle.Italic);
            }
        }

        public void addText(string text, float x, float y)
        {
            paper.Mutate(r => r.DrawText(text, font, Color.Black, new PointF(x, y)));
        }

        public void addImage(string imagePath, int x, int y)
        {
            using (var client = new WebClient())
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                byte[] bytes_images = client.DownloadData($"{path}/images/{imagePath}");
                Stream stream_images = new MemoryStream(bytes_images);
                Image image = Image.Load(stream_images);
                paper.Mutate(r => r.DrawImage(image, new Point(x, y), 1f));
            }
        }

        public byte[] getPaper()
        {
            byte[] result;
            using (MemoryStream stream = new MemoryStream())
            {
                paper.Save(stream, paper.Metadata.DecodedImageFormat);
                stream.Position = 0;
                result = stream.ToArray();
                var after = stream.Length;
            }
            return result;
        }

        public void setFontSize(float size)
        {
            font = family.CreateFont(size, FontStyle.Italic);
        }

        public void showXY()
        {
            setFontSize(10);
            for (int i = 0; i < 800; i += 50)
            {
                for (int j = 0; j < 1200; j += 25)
                {
                    addText("" + i + ":" + j, i, j);
                }
            }
        }
    }
}

