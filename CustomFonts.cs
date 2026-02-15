using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace RandomVideoStopper
{
    internal class CustomFonts
    {
        private static PrivateFontCollection _fonts = new PrivateFontCollection();
        private static Font _cachedFont;

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        public static Font MyCustomFont(float size)
        {
            if (_fonts.Families.Length == 0)
            {
                byte[] fontData = Properties.Resources.Cousine_Bold;
                int fontLen = fontData.Length;
                IntPtr fontPtr = Marshal.AllocCoTaskMem(fontLen);
                Marshal.Copy(fontData, 0, fontPtr, fontLen);

                _fonts.AddMemoryFont(fontPtr, fontLen);
                uint pcFonts = 0;
                AddFontMemResourceEx(fontPtr, (uint)fontLen, IntPtr.Zero, ref pcFonts);
            }

            if (_cachedFont == null || _cachedFont.Size != size)
            {
                _cachedFont = new Font(_fonts.Families[0], size, FontStyle.Bold);
            }
            return _cachedFont;
        }
    }
}