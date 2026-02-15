using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace RandomVideoStopper
{
    public class GradientedBorderTextBox : UserControl
    {
        private RichTextBox insideRichTextBox = new RichTextBox();

        [Category("Appearance Custom")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ColorTop { get; set; } = Color.FromArgb(60, 60, 60);

        [Category("Appearance Custom")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ColorBottom { get; set; } = Color.FromArgb(40, 40, 40);

        [Category("Appearance Custom")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int BorderRadius { get; set; } = 15;

        [Category("Appearance Custom")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int BorderThickness { get; set; } = 3;

        [Category("Appearance Custom")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public float GradientAngle { get; set; } = 45f;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        public override string Text
        {
            get => insideRichTextBox.Text;
            set
            {
                if (insideRichTextBox.Text == value) return;

                int selectionStart = insideRichTextBox.SelectionStart;
                insideRichTextBox.Text = value;

                if (selectionStart <= insideRichTextBox.Text.Length)
                    insideRichTextBox.SelectionStart = selectionStart;
                else insideRichTextBox.SelectionStart = insideRichTextBox.Text.Length;

                UpdateLayout();
                Invalidate();
            }
        }

        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                if (insideRichTextBox != null)
                    insideRichTextBox.BackColor = value;
                Invalidate();
            }
        }

        public override Color ForeColor
        {
            get => insideRichTextBox.ForeColor;
            set => insideRichTextBox.ForeColor = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ReadOnly
        {
            get => insideRichTextBox.ReadOnly;
            set
            {
                insideRichTextBox.ReadOnly = value;
                insideRichTextBox.ForeColor = value ? Color.Gray : Color.White;
                insideRichTextBox.TabStop = !value;
                Invalidate();
            }
        }

        public GradientedBorderTextBox()
        {
            this.DoubleBuffered = true;
            this.Size = new Size(120, 40);
            this.BackColor = Color.FromArgb(30, 30, 30);

            insideRichTextBox.BorderStyle = BorderStyle.None;
            insideRichTextBox.ScrollBars = RichTextBoxScrollBars.None;
            insideRichTextBox.Multiline = false;
            insideRichTextBox.BackColor = this.BackColor;
            insideRichTextBox.ForeColor = Color.White;

            this.Controls.Add(insideRichTextBox);

            insideRichTextBox.HandleCreated += (s, e) => AlignTextCenter();
            insideRichTextBox.TextChanged += (s, e) => AlignTextCenter();
        }

        private void AlignTextCenter()
        {
            int start = insideRichTextBox.SelectionStart;
            insideRichTextBox.SelectAll();
            insideRichTextBox.SelectionAlignment = HorizontalAlignment.Center;

            insideRichTextBox.SelectionStart = start;
            insideRichTextBox.SelectionLength = 0;
        }

        private void UpdateLayout()
        {
            if (insideRichTextBox == null) return;

            int textHeight = TextRenderer.MeasureText("0", insideRichTextBox.Font).Height;
            int xPos = (BorderRadius / 2) + BorderThickness;
            int width = this.Width - (xPos * 2);

            int yPos = (this.Height - textHeight) / 2 + 1;

            insideRichTextBox.Location = new Point(xPos, yPos);
            insideRichTextBox.Size = new Size(width, textHeight);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateLayout();
        }
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            insideRichTextBox.Font = this.Font;
            UpdateLayout();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;

            if (this.Parent != null)
                using (SolidBrush parentBrush = new SolidBrush(this.Parent.BackColor))
                    g.FillRectangle(parentBrush, 0, 0, this.Width, this.Height);

            float penWidth = BorderThickness;
            RectangleF rect = new RectangleF(penWidth / 2, penWidth / 2, this.Width - penWidth, this.Height - penWidth);
            float r = Math.Min(BorderRadius, rect.Height / 2);

            using (GraphicsPath path = GetRoundedPath(rect, r))
            {
                using (SolidBrush bgBrush = new SolidBrush(this.BackColor))
                    g.FillPath(bgBrush, path);

                if (penWidth > 0)
                
                    using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, ColorTop, ColorBottom, GradientAngle))
                        using (Pen pen = new Pen(brush, penWidth))
                        {
                            pen.Alignment = PenAlignment.Center;
                            g.DrawPath(pen, path);
                        }
            }
        }

        private GraphicsPath GetRoundedPath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float d = radius * 2;
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}