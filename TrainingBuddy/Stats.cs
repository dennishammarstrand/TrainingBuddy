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
    class Stats : Form
    {
        private NumericUpDown IdValue = new NumericUpDown { Dock = DockStyle.Fill, Maximum = 10000 };
        private ComboBox ExerciseName = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, Sorted = true };
        private NumericUpDown WeightValue = new NumericUpDown { Dock = DockStyle.Fill, Maximum = 10000 };
        private DataGridView stats = new DataGridView { Font = new Font("San Serif", 9f), ReadOnly = true, ColumnCount = 3, AutoSize = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, RowHeadersVisible = false, BackgroundColor = SystemColors.Control, BorderStyle = BorderStyle.None, AllowUserToAddRows = false, AllowUserToDeleteRows = false, AllowUserToResizeColumns = false, AllowUserToResizeRows = false };
        private NumericUpDown RemoveId = new NumericUpDown { Dock = DockStyle.Fill };
        private TextBox percent1 = new TextBox { Dock = DockStyle.Left };
        private Label percent1Result = new Label { Anchor = AnchorStyles.Right, AutoSize = true };
        private TextBox percent2 = new TextBox { Dock = DockStyle.Left };
        private Label percent2Result = new Label { Anchor = AnchorStyles.Right, AutoSize = true };
        private TextBox percent3 = new TextBox { Dock = DockStyle.Left };
        private Label percent3Result = new Label { Anchor = AnchorStyles.Right, AutoSize = true };
        private int selectedRow;
        private Label TabZero = new Label();

        public Stats()
        {
            System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            //Outer form editing
            ShowIcon = false;
            Size = new Size(700, 500);
            Padding = new Padding(5);
            Text = "Pr Statistics";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            //Main form with statistics table and getting stats from database
            string[] training = File.ReadAllLines(@"C:\Users\Dennis\OneDrive\Dokument\C#\Statistik.txt");
            TableLayoutPanel mainGrid = new TableLayoutPanel { ColumnCount = 2, Dock = DockStyle.Fill };
            mainGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            mainGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
            Controls.Add(mainGrid);
            stats.Columns[0].Name = "Id";
            stats.Columns[1].Name = "Exercise";
            stats.Columns[2].Name = "Weight(kg)";
            stats.Columns[0].Width = 30;
            stats.Columns[1].Width = 110;
            foreach (string s in training)
            {
                string[] split = s.Split(',');
                object[] every = new object[split.Length];
                every[0] = int.Parse(split[0]);
                every[1] = split[1];
                every[2] = int.Parse(split[2]);
                stats.Rows.Add(every);
            }
            mainGrid.Controls.Add(stats);
            stats.TabStop = false;
            stats.ClearSelection();

            //Add/remove exercises Grid
            TableLayoutPanel innerGrid = new TableLayoutPanel { RowCount = 20, ColumnCount = 3, Dock = DockStyle.Fill, AutoSize = true };
            innerGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            innerGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            innerGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            mainGrid.Controls.Add(innerGrid);
            Label id = new Label { Text = "Id", Anchor = AnchorStyles.Right, AutoSize = true };
            Label exercise = new Label { Text = "Exercise", Anchor = AnchorStyles.Right, AutoSize = true };
            Label weight = new Label { Text = "Weight(kg)", Anchor = AnchorStyles.Right, AutoSize = true };
            Button addStat = new Button { Text = "Add", Anchor = AnchorStyles.Right, AutoSize = true };
            Button updateStat = new Button { Text = "Update", Anchor = AnchorStyles.Left, AutoSize = true };
            Label removeLabel = new Label { Text = "Enter Id to remove", Anchor = AnchorStyles.Right, AutoSize = true };
            Button remove = new Button { Text = "Remove", Anchor = AnchorStyles.Left, AutoSize = true };
            innerGrid.Controls.Add(id, 1, 0);
            innerGrid.Controls.Add(IdValue, 2, 0);
            innerGrid.Controls.Add(exercise, 1, 1);
            innerGrid.Controls.Add(ExerciseName, 2, 1);
            ExerciseName.Items.AddRange(new object[] { "Backsquat", "Benchpress", "Clean", "Clean & Jerk", "Deadlift", "Frontsquat", "Power Clean", "Power Snatch", "Pullup", "Push Jerk", "Push Press", "Snatch", "Split Jerk" });
            innerGrid.Controls.Add(weight, 1, 2);
            innerGrid.Controls.Add(WeightValue, 2, 2);
            innerGrid.Controls.Add(addStat, 1, 3);
            innerGrid.Controls.Add(updateStat, 2, 3);
            innerGrid.Controls.Add(removeLabel, 1, 5);
            innerGrid.Controls.Add(RemoveId, 2, 5);
            innerGrid.Controls.Add(remove, 2, 6);
            //Percentages calculating section
            Label percentages = new Label { Text = "Enter percentages you want to calculate", Dock = DockStyle.Fill, AutoSize = true };
            Button calculate = new Button { Text = "Calculate", Anchor = AnchorStyles.Left, AutoSize = true };
            Button mainMenu = new Button { Text = "Main Menu", Anchor = AnchorStyles.Top, AutoSize = true };
            innerGrid.Controls.Add(percentages, 2, 8);
            innerGrid.Controls.Add(percent1Result, 1, 9);
            innerGrid.Controls.Add(percent1, 2, 9);
            innerGrid.Controls.Add(percent2Result, 1, 10);
            innerGrid.Controls.Add(percent2, 2, 10);
            innerGrid.Controls.Add(percent3Result, 1, 11);
            innerGrid.Controls.Add(percent3, 2, 11);
            innerGrid.Controls.Add(calculate, 2, 12);
            innerGrid.Controls.Add(mainMenu, 0, 14);
            innerGrid.Controls.Add(TabZero, 0, 16);
            ActiveControl = TabZero;

            //Pictures
            innerGrid.Controls.Add(AddImage("https://static.thenounproject.com/png/882-200.png"), 0, 1);
            innerGrid.Controls.Add(AddImage("https://image.flaticon.com/icons/png/512/185/185590.png"), 0, 2);
            innerGrid.Controls.Add(AddImage("http://www.athleticcompound.com.au/wp-content/uploads/2017/09/weightlifting-icon-286x300.png"), 0, 3);

            //Events
            addStat.Click += AddStatClick;
            updateStat.Click += UpdateStatClick;
            remove.Click += RemoveStatClick;
            calculate.Click += CalculatePercentClick;
            FormClosed += SaveWhenExit;
            stats.CellClick += DataGridCellClick;
            mainMenu.Click += OpenTimerMenu;
            //Tab events
            IdValue.Enter += TabSelectNumeric;
            WeightValue.Enter += TabSelectNumeric;
            RemoveId.Enter += TabSelectNumeric;
            percent1.Enter += TabSelectTextBox;
            percent2.Enter += TabSelectTextBox;
            percent3.Enter += TabSelectTextBox;
        }
        //Tab event handler
        private void TabSelectNumeric(object sender, EventArgs e)
        {
            NumericUpDown tab = (NumericUpDown)sender;
            tab.Select(0, tab.Text.Length);
        }
        private void TabSelectTextBox(object sender, EventArgs e)
        {
            TextBox tab = (TextBox)sender;
            tab.Select(0, tab.Text.Length);
        }
        //Event handler methods
        private void OpenTimerMenu(object sender, EventArgs e)
        {
            SaveDataGridViewToCSV();
            Hide();
            MainMenu menu = new MainMenu();
            menu.ShowDialog();
            Close();
        }
        private void DataGridCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedRow = e.RowIndex;
                DataGridViewRow row = stats.Rows[selectedRow];
                IdValue.Value = (int)row.Cells[0].Value;
                ExerciseName.Text = row.Cells[1].Value.ToString();
                WeightValue.Value = (int)row.Cells[2].Value;
            }
        }
        private void CalculatePercentClick(object sender, EventArgs e)
        {
            string p1 = "0." + percent1.Text;
            string p2 = "0." + percent2.Text;
            string p3 = "0." + percent3.Text;
            DataGridViewRow percentRow = stats.Rows[selectedRow];
            percent1Result.Text = ((int)percentRow.Cells[2].Value * double.Parse(p1)).ToString();
            percent2Result.Text = ((int)percentRow.Cells[2].Value * double.Parse(p2)).ToString();
            percent3Result.Text = ((int)percentRow.Cells[2].Value * double.Parse(p3)).ToString();
        }
        private void UpdateStatClick(object sender, EventArgs e)
        {
            DataGridViewRow updatedRow = stats.Rows[selectedRow];
            updatedRow.Cells[0].Value = (int)IdValue.Value;
            updatedRow.Cells[1].Value = ExerciseName.Text;
            updatedRow.Cells[2].Value = (int)WeightValue.Value;

        }
        private void RemoveStatClick(object sender, EventArgs e)
        {
            try
            {
                stats.Rows.RemoveAt((int) (RemoveId.Value - 1));
            }
            catch
            {

            }
        }
        private void AddStatClick(object sender, EventArgs e)
        {
            if (IdValue.Value > 0 && ExerciseName.Text != "" && WeightValue.Value > 0)
            {
                stats.Rows.Add(((int)IdValue.Value), ExerciseName.Text, ((int)WeightValue.Value));
            }
        }
        private void SaveWhenExit(object sender, FormClosedEventArgs e)
        {
            SaveDataGridViewToCSV();
        }
        //Save data method
        private void SaveDataGridViewToCSV()
        {
            // Choose whether to write header. Use EnableWithoutHeaderText instead to omit header.
            stats.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            // Select all the cells
            stats.SelectAll();
            // Copy (set clipboard)
            Clipboard.SetDataObject(stats.GetClipboardContent());
            // Paste (get the clipboard and serialize it to a file)
            File.WriteAllText(@"C:\Users\Dennis\OneDrive\Dokument\C#\Statistik.txt", Clipboard.GetText(TextDataFormat.CommaSeparatedValue));
        }
        //Add picture given location method
        private PictureBox AddImage(string imageLocation)
        {
            return new PictureBox { ImageLocation = imageLocation, Size = new Size(20, 20), SizeMode = PictureBoxSizeMode.Zoom, Anchor = AnchorStyles.Left };                
        }
    }
}
