using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;

namespace PBR_Material_Maker
{
    class Utils
    {
        public static Bitmap CombineChannels(Bitmap r, Bitmap g, Bitmap b)
        {
            int width = r.Width;
            int height = r.Height;
            return CombineChannels(r, g, b, width, height);
        }

        public static Bitmap CombineChannels(Bitmap r, Bitmap g, Bitmap b, int width, int height)
        {
            PixelFormat format = PixelFormat.Format32bppRgb;
            var merged = new Bitmap(width, height, format);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int rIntensity = r.GetPixel(x % r.Width, y % r.Height).R;
                    int gIntensity = g.GetPixel(x % g.Width, y % g.Height).G;
                    int bIntensity = b.GetPixel(x % b.Width, y % b.Height).B;

                    merged.SetPixel(x, y, Color.FromArgb(rIntensity, gIntensity, bIntensity));
                }
            }
            return merged;
        }


        public static Bitmap GenerateSolidColor(int r, int g, int b)
        {
            Bitmap solidColor = new Bitmap(1, 1, PixelFormat.Format32bppRgb);
            solidColor.SetPixel(0, 0, Color.FromArgb(r, g, b));

            return solidColor;
        }

        public static Bitmap CombineChannels(Bitmap r, Bitmap g, Bitmap b, Bitmap a)
        {
            int width = r.Width;
            int height = r.Height;

            var merged = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int rIntensity = r.GetPixel(x % r.Width, y % r.Height).R;
                    int gIntensity = g.GetPixel(x % g.Width, y % g.Height).G;
                    int bIntensity = b.GetPixel(x % b.Width, y % b.Height).B;
                    // This uses the generated previews, so the alpha preview has 255 alpha throughout. However, any other channel has the alpha value.
                    int aIntensity = a.GetPixel(x % a.Width, y % a.Height).R;

                    merged.SetPixel(x, y, Color.FromArgb(aIntensity, rIntensity, gIntensity, bIntensity));
                }
            }
            return merged;
        }

        [DllImport("shlwapi.dll", EntryPoint = "PathRelativePathTo")]
        protected static extern bool PathRelativePathTo(StringBuilder lpszDst,
          string from, UInt32 attrFrom,
          string to, UInt32 attrTo);

        public static string GetRelativePath(string from, string to)
        {
            StringBuilder builder = new StringBuilder(1024);
            bool result = PathRelativePathTo(builder, from, 0, to, 0);
            return builder.ToString();
        }
    }
}
