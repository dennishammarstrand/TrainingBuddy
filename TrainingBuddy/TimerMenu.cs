using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace TrainingBuddy
{
    class TimerMenu : Form
    {
        private Timer Stopwatch = new Timer();
        private TextBox Watch = new TextBox { Font = new Font("San Serif", 50f), Text = "00:00:00:00", ReadOnly = true, Dock = DockStyle.Fill, BackColor = Color.White, Anchor = AnchorStyles.Top, Enabled = false, TextAlign = HorizontalAlignment.Center };
        private Button Start = new Button { Font = new Font("San Serif", 20f), Text = "Start", Anchor = AnchorStyles.Top, AutoSize = true, Dock = DockStyle.Fill };
        private DataGridView SplitTimes = new DataGridView { Visible = false, ColumnHeadersVisible = false, ColumnCount = 1, AutoSize = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, RowHeadersVisible = false, BackgroundColor = SystemColors.Control, BorderStyle = BorderStyle.None };
        int ms, sec, min, h;
        bool on = true;
        public Label TabZeroWorkoutChange = new Label { Font = new Font("San Serif", 15f), Anchor = AnchorStyles.Left, AutoSize = true, Dock = DockStyle.Fill };
        public Button saveRecord = new Button { Visible = false, Text = "Save Record", Dock = DockStyle.Fill, AutoSize = true, Anchor = AnchorStyles.Left };
        private WodSavedRecord record = new WodSavedRecord();
        private List<WodSavedRecord> SaveRecordsToFile = new List<WodSavedRecord>();

        public TimerMenu()
        {
            //Outer form editing
            ShowIcon = false;
            Size = new Size(400, 400);
            Padding = new Padding(5);
            Text = "Timer";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            //Timer grid with all it's controls added
            TableLayoutPanel timerGrid = new TableLayoutPanel { RowCount = 6, ColumnCount = 2, Dock = DockStyle.Fill, AutoSize = true };
            timerGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            timerGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            timerGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 30));
            timerGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            timerGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            timerGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            timerGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            timerGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            Controls.Add(timerGrid);
            timerGrid.Controls.Add(Watch);
            timerGrid.SetColumnSpan(Watch, 2);
            Watch.Width = 400;
            timerGrid.Controls.Add(Start, 0, 1);
            Button reset = new Button { Font = new Font("San Serif", 20f), Text = "Reset", Anchor = AnchorStyles.Left, AutoSize = true, Dock = DockStyle.Fill };
            Button split = new Button { Font = new Font("San Serif", 20f), Text = "Split", Anchor = AnchorStyles.Top, AutoSize = true };
            Button mainMenu = new Button { Text = "Main Menu", Dock = DockStyle.Fill, AutoSize = true, Anchor = AnchorStyles.Right };
            timerGrid.Controls.Add(reset, 1, 1);
            timerGrid.Controls.Add(split, 0, 2);
            timerGrid.SetColumnSpan(split, 2);
            timerGrid.Controls.Add(SplitTimes, 0, 3);
            timerGrid.SetRowSpan(SplitTimes, 3);
            timerGrid.Controls.Add(mainMenu, 1, 5);
            timerGrid.Controls.Add(TabZeroWorkoutChange, 1, 3);
            ActiveControl = TabZeroWorkoutChange;
            timerGrid.Controls.Add(saveRecord, 1, 4);

            //Event handlers
            Start.Click += StartTimer;
            Stopwatch.Tick += StopwatchTick;
            reset.Click += ResetStopwatch;
            mainMenu.Click += ReturnToMainWindow;
            split.Click += SplitTimesHandler;
            saveRecord.Click += SaveRecordClickHandler;
        }
        private void SaveRecordClickHandler(object sender, EventArgs e)
        {
            record = new WodSavedRecord { WodName = TabZeroWorkoutChange.Text, WodRecordTime = Watch.Text };
            SaveRecordsToFile.Add(record);
            string[] saveFile = new string[SaveRecordsToFile.Count];
            int counter = 0;
            foreach (WodSavedRecord item in SaveRecordsToFile)
            {
                saveFile[counter] = item.WodName + "," + item.WodRecordTime;
                counter++;
            }
            File.WriteAllLines(@"C:\Users\Dennis\OneDrive\Dokument\C#\TrainingBuddy\WodRecords.txt", saveFile);
        }
        //Stopwatch methods
        private void ReturnToMainWindow(object sender, EventArgs e)
        {
            Hide();
            MainMenu menu = new MainMenu();
            menu.ShowDialog();
            Close();
        }
        private void SplitTimesHandler(object sender, EventArgs e)
        {
            SplitTimes.Visible = true;
            SplitTimes.Rows.Add(Watch.Text);
        }
        private void ResetStopwatch(object sender, EventArgs e)
        {
            ms = 0;
            sec = 0;
            min = 0;
            h = 0;
            Watch.Text = "00:00:00:00";
            SplitTimes.Rows.Clear();
            SplitTimes.Visible = false;
        }
        private void StartTimer(object sender, EventArgs e)
        {
            if (on)
            {
                Start.Text = "Stop";
                Stopwatch.Start();
                Stopwatch.Interval = 100;
                on = false;
            }
            else
            {
                Start.Text = "Start";
                Stopwatch.Stop();
                on = true;
            }
        }
        private void StopwatchTick(object sender, EventArgs e)
        {
            ms++;
            if (ms == 10)
            {
                ms = 0;
                sec++;
            }
            if (sec == 60)
            {
                sec = 0;
                min++;
            }
            if (min == 60)
            {
                min = 0;
                h++;
            }
            Watch.Text = string.Format("{0}:{1}:{2}:{3}", h.ToString().PadLeft(2, '0'), min.ToString().PadLeft(2, '0'), sec.ToString().PadLeft(2, '0'), ms.ToString().PadRight(2, '0'));
        }
    }
}
