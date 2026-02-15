using System.Runtime.InteropServices;

namespace RandomVideoStopper
{
    public partial class MainForm : Form
    {
        #region Переменные
        private DateTime _lastAutoPressTime;

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        private const int DWMWA_CAPTION_COLOR = 35;
        private const int DWMWA_TEXT_COLOR = 36;

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        private const int HOTKEY_ID = 9000;
        private const uint VK_F10 = 0x79;

        private Random _random = new Random();

        private bool _isSystemRunning = false;
        private bool _isWaitingForUser = false;

        private int _timeLeftMs = 0;
        private int _lowerLimit = 0;
        private int _upperLimit = 0;
        #endregion

        public MainForm()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.Dpi;
            ApplyCustomFont();

            if (!this.DesignMode)
            {
                ApplyDarkTitleBar();
                RegisterHotKey(this.Handle, HOTKEY_ID, 0, VK_F10);
                UpdateButtonsState(false);
            }
        }

        private void ApplyCustomFont()
        {
            Font customFont = CustomFonts.MyCustomFont(12);
            this.Font = customFont;

            foreach (Control c in this.Controls)
            {
                c.Font = customFont;
                if (c is GradientedBorderTextBox tb) tb.Font = customFont;
                if (c is GradientedBorderButton btn) btn.Font = customFont;
            }
        }

        #region Логика таймера
        private void LogicTimer_Tick(object sender, EventArgs e)
        {
            if (!_isSystemRunning) return;

            if (_isWaitingForUser)
            {
                if ((DateTime.Now - _lastAutoPressTime).TotalMilliseconds < 500) return;

                short state = GetAsyncKeyState(Keys.Space);
                bool spaceWasPressed = (state & 0x8000) != 0 || (state & 0x0001) != 0;

                if (spaceWasPressed)
                {
                    GetAsyncKeyState(Keys.Space);
                    StartRandomCountdown();
                }
                return;
            }

            if (_timeLeftMs > 0)
            {
                _timeLeftMs -= _logicTimer.Interval;
                UpdateTimerLabel();

                if (_timeLeftMs <= 0)
                    PerformAutoSpace();
            }
        }

        private void UpdateTimerLabel()
        {
            if (labelTimer == null) return;

            if (labelTimer.Enabled)
                labelTimer.Text = $"Таймер: {Math.Max(0, _timeLeftMs / 1000.0):F1} сек";
            else
                labelTimer.Text = "Таймер: ??? сек";
        }

        private void StartRandomCountdown()
        {
            _isWaitingForUser = false;

            UpdateButtonsState(true);
            bool isLowerOk = int.TryParse(lowerLimitTextBox.Text, out int lower);
            bool isUpperOk = int.TryParse(upperLimitTextBox.Text, out int upper);
            if (isLowerOk && isUpperOk && lower < upper && lower > 0)
            {
                _lowerLimit = lower;
                _upperLimit = upper;
            }

            _timeLeftMs = _random.Next(_lowerLimit, _upperLimit + 1);

            if (labelStatus != null) labelStatus.Text = "Статус: Отсчет...";
        }

        private void PerformAutoSpace()
        {
            UpdateButtonsState(false);
            _timeLeftMs = 0;
            if (labelTimer != null) labelTimer.Text = "Таймер: --- сек";
            if (labelStatus != null) labelStatus.Text = "Статус: Нажато";

            SendKeys.SendWait(" ");
            _lastAutoPressTime = DateTime.Now;
            GetAsyncKeyState(Keys.Space);

            _isWaitingForUser = true;
        }

        private void StartSystem()
        {
            bool isLowerOk = int.TryParse(lowerLimitTextBox.Text, out int lower);
            bool isUpperOk = int.TryParse(upperLimitTextBox.Text, out int upper);

            if (isLowerOk && isUpperOk && lower > 0 && lower < upper)
            {
                _lowerLimit = lower;
                _upperLimit = upper;
                _isSystemRunning = true;

                UpdateButtonsState(true);
                StartRandomCountdown();
                _logicTimer.Start();
            }
            else MessageBox.Show("Нижняя граница должна быть больше 0 и меньше верхней!", "Ошибка интервалов");
        }

        private void StopSystem()
        {
            _isSystemRunning = false;
            _isWaitingForUser = false;
            _logicTimer.Stop();
            UpdateButtonsState(false);

            if (pauseTimerButton != null) pauseTimerButton.Text = "Пауза";
            if (labelTimer != null) labelTimer.Text = "Таймер: --- сек";
            if (labelStatus != null) labelStatus.Text = "Статус: Остановлено";
        }

        private void UpdateButtonsState(bool isRunning)
        {
            startTimerButton.Enabled = !isRunning;
            stopTimerButton.Enabled = isRunning;
            pauseTimerButton.Enabled = isRunning;
            lowerLimitTextBox.ReadOnly = isRunning;
            upperLimitTextBox.ReadOnly = isRunning;
        }
        #endregion

        #region Методы кнопок
        private void pinButton_Click(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;

            if (this.TopMost)
            {
                pinButton.ColorFrom = Color.FromArgb(255, 121, 237);
                pinButton.ColorTo = Color.FromArgb(255, 121, 237);
                pinButton.BackColor = Color.FromArgb(86, 54, 81);
                pinButton.Image = Properties.Resources.pin;
            }
            else
            {
                pinButton.ColorFrom = Color.FromArgb(123, 75, 117);
                pinButton.ColorTo = Color.FromArgb(123, 75, 117);
                pinButton.BackColor = Color.FromArgb(52, 37, 50);
                pinButton.Image = Properties.Resources.unpin;
            }
        }

        private void hideTimerButton_Click(object sender, EventArgs e)
        {
            labelTimer.Enabled = !labelTimer.Enabled;
            if (labelTimer.Enabled)
            {
                hideTimerButton.ColorFrom = Color.FromArgb(121, 255, 150);
                hideTimerButton.ColorTo = hideTimerButton.ColorFrom;
                hideTimerButton.BackColor = Color.FromArgb(48, 77, 54);
                hideTimerButton.Image = Properties.Resources.show;
            }
            else
            {
                hideTimerButton.ColorFrom = Color.FromArgb(255, 121, 121);
                hideTimerButton.ColorTo = hideTimerButton.ColorFrom;
                hideTimerButton.BackColor = Color.FromArgb(108, 46, 46);
                hideTimerButton.Image = Properties.Resources.hide;
            }
        }
        private void startTimerButton_Click(object sender, EventArgs e)
        {
            StartSystem();
        }

        private void pauseTimerButton_Click(object sender, EventArgs e)
        {
            if (!_isSystemRunning) return;

            if (_logicTimer.Enabled)
            {
                _logicTimer.Stop();
                pauseTimerButton.Text = "Продолжить";
                if (labelStatus != null) labelStatus.Text = "Статус: Пауза";
            }
            else
            {
                _logicTimer.Start();
                pauseTimerButton.Text = "Пауза";
                if (labelStatus != null) labelStatus.Text = "Статус: Отсчёт...";
            }
        }

        private void stopTimerButton_Click(object sender, EventArgs e)
        {
            StopSystem();
        }
        #endregion

        #region Прочие методы и Hotkeys
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Space)
            {
                if (!_isSystemRunning)
                {
                    StartSystem();
                    return true;
                }
                if (_isSystemRunning && _isWaitingForUser)
                {
                    StartRandomCountdown();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312 && m.WParam.ToInt32() == HOTKEY_ID)
            {
                if (_isSystemRunning)
                    StopSystem();
                else StartSystem();
            }
            base.WndProc(ref m);
        }

        private void ApplyDarkTitleBar()
        {
            int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;
            if (Environment.OSVersion.Version.Build < 17763) DWMWA_USE_IMMERSIVE_DARK_MODE = 19;
            int trueValue = 1;
            DwmSetWindowAttribute(this.Handle, DWMWA_USE_IMMERSIVE_DARK_MODE, ref trueValue, sizeof(int));

            if (Environment.OSVersion.Version.Build >= 22000)
            {
                int darkColor = ColorTranslator.ToWin32(Color.FromArgb(30, 30, 30));
                int whiteText = ColorTranslator.ToWin32(Color.White);
                DwmSetWindowAttribute(this.Handle, DWMWA_CAPTION_COLOR, ref darkColor, sizeof(int));
                DwmSetWindowAttribute(this.Handle, DWMWA_TEXT_COLOR, ref whiteText, sizeof(int));
            }

            this.BackColor = Color.FromArgb(45, 45, 48);
            this.ForeColor = Color.White;
        }
        #endregion

        #region Методы формы
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterHotKey(this.Handle, HOTKEY_ID);
            Application.Exit();
        }
        #endregion
    }
}