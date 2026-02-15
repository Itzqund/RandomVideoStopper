namespace RandomVideoStopper
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            labelStatus = new Label();
            labelTimer = new Label();
            _logicTimer = new System.Windows.Forms.Timer(components);
            labelFrom = new Label();
            labelTo = new Label();
            infoLabel1 = new Label();
            startTimerButton = new GradientedBorderButton();
            pauseTimerButton = new GradientedBorderButton();
            stopTimerButton = new GradientedBorderButton();
            hideTimerButton = new GradientedBorderButton();
            pinButton = new GradientedBorderButton();
            lowerLimitTextBox = new GradientedBorderTextBox();
            upperLimitTextBox = new GradientedBorderTextBox();
            SuspendLayout();
            // 
            // labelStatus
            // 
            labelStatus.AutoSize = true;
            labelStatus.Font = new Font("Cousine", 12F, FontStyle.Bold);
            labelStatus.ForeColor = Color.White;
            labelStatus.Location = new Point(7, 153);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(168, 18);
            labelStatus.TabIndex = 0;
            labelStatus.Text = "Статус: Ожидание";
            // 
            // labelTimer
            // 
            labelTimer.AutoSize = true;
            labelTimer.Font = new Font("Cousine", 12F, FontStyle.Bold);
            labelTimer.ForeColor = Color.White;
            labelTimer.Location = new Point(7, 174);
            labelTimer.Name = "labelTimer";
            labelTimer.Size = new Size(158, 18);
            labelTimer.TabIndex = 1;
            labelTimer.Text = "Таймер: --- сек";
            // 
            // _logicTimer
            // 
            _logicTimer.Tick += LogicTimer_Tick;
            // 
            // labelFrom
            // 
            labelFrom.AutoSize = true;
            labelFrom.Font = new Font("Cousine", 12F, FontStyle.Bold);
            labelFrom.ForeColor = Color.White;
            labelFrom.Location = new Point(7, 42);
            labelFrom.Name = "labelFrom";
            labelFrom.Size = new Size(28, 18);
            labelFrom.TabIndex = 4;
            labelFrom.Text = "от";
            // 
            // labelTo
            // 
            labelTo.AutoSize = true;
            labelTo.Font = new Font("Cousine", 12F, FontStyle.Bold);
            labelTo.ForeColor = Color.White;
            labelTo.Location = new Point(145, 42);
            labelTo.Name = "labelTo";
            labelTo.Size = new Size(28, 18);
            labelTo.TabIndex = 5;
            labelTo.Text = "до";
            // 
            // infoLabel1
            // 
            infoLabel1.AutoSize = true;
            infoLabel1.Font = new Font("Cousine", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            infoLabel1.ForeColor = Color.White;
            infoLabel1.Location = new Point(7, 9);
            infoLabel1.Name = "infoLabel1";
            infoLabel1.Size = new Size(328, 18);
            infoLabel1.TabIndex = 6;
            infoLabel1.Text = "Выбор тайминга срабатывания (мс)";
            // 
            // startTimerButton
            // 
            startTimerButton.BackColor = Color.FromArgb(32, 32, 32);
            startTimerButton.BorderRadius = 5;
            startTimerButton.BorderThickness = 2;
            startTimerButton.ColorFrom = Color.FromArgb(121, 255, 150);
            startTimerButton.ColorTo = Color.FromArgb(73, 153, 90);
            startTimerButton.FlatStyle = FlatStyle.Flat;
            startTimerButton.Font = new Font("Cousine", 12F, FontStyle.Bold);
            startTimerButton.ForeColor = Color.White;
            startTimerButton.GradientAngle = 45F;
            startTimerButton.Location = new Point(10, 75);
            startTimerButton.Name = "startTimerButton";
            startTimerButton.Size = new Size(155, 30);
            startTimerButton.TabIndex = 14;
            startTimerButton.TabStop = false;
            startTimerButton.Text = "Старт";
            startTimerButton.UseVisualStyleBackColor = false;
            startTimerButton.Click += startTimerButton_Click;
            // 
            // pauseTimerButton
            // 
            pauseTimerButton.BackColor = Color.FromArgb(32, 32, 32);
            pauseTimerButton.BorderRadius = 5;
            pauseTimerButton.BorderThickness = 2;
            pauseTimerButton.ColorFrom = Color.FromArgb(255, 224, 121);
            pauseTimerButton.ColorTo = Color.FromArgb(153, 134, 73);
            pauseTimerButton.FlatStyle = FlatStyle.Flat;
            pauseTimerButton.Font = new Font("Cousine", 12F, FontStyle.Bold);
            pauseTimerButton.ForeColor = Color.White;
            pauseTimerButton.GradientAngle = 45F;
            pauseTimerButton.Location = new Point(175, 75);
            pauseTimerButton.Name = "pauseTimerButton";
            pauseTimerButton.Size = new Size(155, 30);
            pauseTimerButton.TabIndex = 15;
            pauseTimerButton.TabStop = false;
            pauseTimerButton.Text = "Пауза";
            pauseTimerButton.UseVisualStyleBackColor = false;
            pauseTimerButton.Click += pauseTimerButton_Click;
            // 
            // stopTimerButton
            // 
            stopTimerButton.BackColor = Color.FromArgb(32, 32, 32);
            stopTimerButton.BorderRadius = 5;
            stopTimerButton.BorderThickness = 2;
            stopTimerButton.ColorFrom = Color.FromArgb(255, 121, 121);
            stopTimerButton.ColorTo = Color.FromArgb(153, 73, 73);
            stopTimerButton.FlatStyle = FlatStyle.Flat;
            stopTimerButton.Font = new Font("Cousine", 12F, FontStyle.Bold);
            stopTimerButton.ForeColor = Color.White;
            stopTimerButton.GradientAngle = 45F;
            stopTimerButton.Location = new Point(10, 115);
            stopTimerButton.Name = "stopTimerButton";
            stopTimerButton.Size = new Size(320, 30);
            stopTimerButton.TabIndex = 16;
            stopTimerButton.TabStop = false;
            stopTimerButton.Text = "Стоп";
            stopTimerButton.UseVisualStyleBackColor = false;
            stopTimerButton.Click += stopTimerButton_Click;
            // 
            // hideTimerButton
            // 
            hideTimerButton.BackColor = Color.FromArgb(48, 77, 54);
            hideTimerButton.BorderRadius = 5;
            hideTimerButton.BorderThickness = 2;
            hideTimerButton.ColorFrom = Color.FromArgb(121, 255, 150);
            hideTimerButton.ColorTo = Color.FromArgb(121, 255, 150);
            hideTimerButton.FlatStyle = FlatStyle.Flat;
            hideTimerButton.ForeColor = Color.White;
            hideTimerButton.GradientAngle = 45F;
            hideTimerButton.Image = Properties.Resources.show;
            hideTimerButton.Location = new Point(300, 159);
            hideTimerButton.Name = "hideTimerButton";
            hideTimerButton.Size = new Size(30, 30);
            hideTimerButton.TabIndex = 17;
            hideTimerButton.TabStop = false;
            hideTimerButton.TextImageRelation = TextImageRelation.ImageAboveText;
            hideTimerButton.UseVisualStyleBackColor = false;
            hideTimerButton.Click += hideTimerButton_Click;
            // 
            // pinButton
            // 
            pinButton.BackColor = Color.FromArgb(52, 37, 50);
            pinButton.BorderRadius = 5;
            pinButton.BorderThickness = 2;
            pinButton.ColorFrom = Color.FromArgb(123, 75, 117);
            pinButton.ColorTo = Color.FromArgb(123, 75, 117);
            pinButton.FlatStyle = FlatStyle.Flat;
            pinButton.ForeColor = Color.White;
            pinButton.GradientAngle = 45F;
            pinButton.Image = Properties.Resources.unpin;
            pinButton.Location = new Point(300, 35);
            pinButton.Name = "pinButton";
            pinButton.Size = new Size(30, 30);
            pinButton.TabIndex = 18;
            pinButton.TabStop = false;
            pinButton.TextImageRelation = TextImageRelation.ImageAboveText;
            pinButton.UseVisualStyleBackColor = false;
            pinButton.Click += pinButton_Click;
            // 
            // lowerLimitTextBox
            // 
            lowerLimitTextBox.BackColor = Color.FromArgb(32, 32, 32);
            lowerLimitTextBox.BorderRadius = 5;
            lowerLimitTextBox.BorderThickness = 2;
            lowerLimitTextBox.ColorBottom = Color.FromArgb(136, 136, 136);
            lowerLimitTextBox.ColorTop = Color.FromArgb(255, 255, 255);
            lowerLimitTextBox.Font = new Font("Cousine", 12F, FontStyle.Bold);
            lowerLimitTextBox.GradientAngle = 45F;
            lowerLimitTextBox.Location = new Point(44, 35);
            lowerLimitTextBox.Margin = new Padding(5, 8, 5, 8);
            lowerLimitTextBox.Name = "lowerLimitTextBox";
            lowerLimitTextBox.Padding = new Padding(20);
            lowerLimitTextBox.ReadOnly = false;
            lowerLimitTextBox.Size = new Size(90, 30);
            lowerLimitTextBox.TabIndex = 19;
            lowerLimitTextBox.TabStop = false;
            lowerLimitTextBox.Text = "25000";
            // 
            // upperLimitTextBox
            // 
            upperLimitTextBox.BackColor = Color.FromArgb(32, 32, 32);
            upperLimitTextBox.BorderRadius = 5;
            upperLimitTextBox.BorderThickness = 2;
            upperLimitTextBox.ColorBottom = Color.FromArgb(136, 136, 136);
            upperLimitTextBox.ColorTop = Color.FromArgb(255, 255, 255);
            upperLimitTextBox.Font = new Font("Cousine", 12F, FontStyle.Bold);
            upperLimitTextBox.GradientAngle = 45F;
            upperLimitTextBox.Location = new Point(180, 35);
            upperLimitTextBox.Margin = new Padding(5, 8, 5, 8);
            upperLimitTextBox.Name = "upperLimitTextBox";
            upperLimitTextBox.Padding = new Padding(20);
            upperLimitTextBox.ReadOnly = false;
            upperLimitTextBox.Size = new Size(90, 30);
            upperLimitTextBox.TabIndex = 20;
            upperLimitTextBox.TabStop = false;
            upperLimitTextBox.Text = "50000";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(48, 48, 48);
            ClientSize = new Size(340, 201);
            Controls.Add(upperLimitTextBox);
            Controls.Add(lowerLimitTextBox);
            Controls.Add(pinButton);
            Controls.Add(hideTimerButton);
            Controls.Add(stopTimerButton);
            Controls.Add(pauseTimerButton);
            Controls.Add(startTimerButton);
            Controls.Add(infoLabel1);
            Controls.Add(labelTo);
            Controls.Add(labelFrom);
            Controls.Add(labelTimer);
            Controls.Add(labelStatus);
            Font = new Font("Futura-ExtraBold", 8.25F, FontStyle.Bold);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            Text = "Рандомные паузы видео";
            FormClosing += MainForm_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelStatus;
        private Label labelTimer;
        private System.Windows.Forms.Timer _logicTimer;
        private Label labelFrom;
        private Label labelTo;
        private Label infoLabel1;
        private GradientedBorderButton startTimerButton;
        private GradientedBorderButton pauseTimerButton;
        private GradientedBorderButton stopTimerButton;
        private GradientedBorderButton hideTimerButton;
        private GradientedBorderButton pinButton;
        private GradientedBorderTextBox lowerLimitTextBox;
        private GradientedBorderTextBox upperLimitTextBox;
    }
}