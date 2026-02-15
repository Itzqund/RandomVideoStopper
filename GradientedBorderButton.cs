using System.Drawing.Drawing2D;
using System.ComponentModel;

public class GradientedBorderButton : Button
{
    [Category("Appearance Custom")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ColorFrom { get; set; } = Color.FromArgb(60, 60, 60);

    [Category("Appearance Custom")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ColorTo { get; set; } = Color.FromArgb(40, 40, 40);

    [Category("Appearance Custom")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int BorderThickness { get; set; } = 2;

    [Category("Appearance Custom")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int BorderRadius { get; set; } = 10;

    [Category("Appearance Custom")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public float GradientAngle { get; set; } = 90F;

    private bool isHovered = false;
    private bool isPressed = false;

    public GradientedBorderButton()
    {
        this.DoubleBuffered = true;
        this.FlatStyle = FlatStyle.Flat;
        this.FlatAppearance.BorderSize = 0;
        this.Size = new Size(150, 40);
        this.BackColor = Color.FromArgb(32, 32, 32);
        this.ForeColor = Color.White;
        this.SetStyle(ControlStyles.Selectable, false);

        this.MouseEnter += (s, e) => { isHovered = true; Invalidate(); };
        this.MouseLeave += (s, e) => { isHovered = false; Invalidate(); };
        this.MouseDown += (s, e) => { isPressed = true; Invalidate(); };
        this.MouseUp += (s, e) => { isPressed = false; Invalidate(); };
    }

    private Color AdjustBrightness(Color color, float factor)
    {
        return Color.FromArgb(color.A,
            (int)Math.Min(255, color.R * factor),
            (int)Math.Min(255, color.G * factor),
            (int)Math.Min(255, color.B * factor));
    }

    protected override void OnPaint(PaintEventArgs pevent)
    {
        Graphics g = pevent.Graphics;
        g.SmoothingMode = SmoothingMode.HighQuality;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;

        if (this.Parent != null)
            using (SolidBrush parentBrush = new SolidBrush(this.Parent.BackColor))
                g.FillRectangle(parentBrush, 0, 0, this.Width, this.Height);

        float penWidth = BorderThickness;
        RectangleF rect = new RectangleF(penWidth / 2, penWidth / 2, this.Width - penWidth, this.Height - penWidth);
        float r = Math.Min(BorderRadius, rect.Height / 2);

        float factor = !this.Enabled ? 0.5f : (isPressed ? 0.8f : (isHovered ? 1.2f : 1.0f));
        Color cTop = AdjustBrightness(ColorFrom, factor);
        Color cBottom = AdjustBrightness(ColorTo, factor);
        Color cBack = AdjustBrightness(this.BackColor, factor);
        Color cText = !this.Enabled ? Color.Gray : this.ForeColor;

        using (GraphicsPath path = GetRoundedPath(rect, r))
        {
            using (SolidBrush bodyBrush = new SolidBrush(cBack))
                g.FillPath(bodyBrush, path);

            if (penWidth > 0)
                using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, cTop, cBottom, GradientAngle))
                    using (Pen pen = new Pen(brush, penWidth))
                    {
                        pen.Alignment = PenAlignment.Center;
                        g.DrawPath(pen, path);
                    }
        }

        if (this.Image != null)
        {
            Rectangle imgRect = CalculateImageRect(this.Image);

            if (!this.Enabled)
            {
                float[][] matrixItems ={
                    new float[] {1, 0, 0, 0, 0},
                    new float[] {0, 1, 0, 0, 0},
                    new float[] {0, 0, 1, 0, 0},
                    new float[] {0, 0, 0, 0.5f, 0},
                    new float[] {0, 0, 0, 0, 1}};
                System.Drawing.Imaging.ColorMatrix colorMatrix = new System.Drawing.Imaging.ColorMatrix(matrixItems);
                using (var ia = new System.Drawing.Imaging.ImageAttributes())
                {
                    ia.SetColorMatrix(colorMatrix);
                    g.DrawImage(this.Image, imgRect, 0, 0, this.Image.Width, this.Image.Height, GraphicsUnit.Pixel, ia);
                }
            }
            else g.DrawImage(this.Image, imgRect);
        }

        TextRenderer.DrawText(g, this.Text, this.Font, this.ClientRectangle, cText,
            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
    }

    private Rectangle CalculateImageRect(Image img)
    {
        int x = (this.Width - img.Width) / 2;
        int y = (this.Height - img.Height) / 2;
        if (!string.IsNullOrEmpty(this.Text))
            x = 10;
        return new Rectangle(x, y, img.Width, img.Height);
    }

    private GraphicsPath GetRoundedPath(RectangleF rect, float radius)
    {
        GraphicsPath path = new GraphicsPath();
        float d = Math.Max(radius * 2f, 1f);
        if (d > rect.Width) d = rect.Width;
        if (d > rect.Height) d = rect.Height;

        path.StartFigure();
        path.AddArc(rect.X, rect.Y, d, d, 180, 90);
        path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
        path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
        path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
        path.CloseFigure();
        return path;
    }
}