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
    class WodMenu : Form
    {
        private DataGridView WodDisplay = new DataGridView { ColumnHeadersVisible = false, Enabled = false, Font = new Font("San Serif", 15f), Dock = DockStyle.Fill, ReadOnly = true, ColumnCount = 1, AutoSize = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, RowHeadersVisible = false, BackgroundColor = SystemColors.Control, BorderStyle = BorderStyle.None, AllowUserToAddRows = false, AllowUserToDeleteRows = false, AllowUserToResizeColumns = false, AllowUserToResizeRows = false };
        private ComboBox GetWod = new ComboBox { Font = new Font("San Serif", 15f), Dock = DockStyle.Right, AutoCompleteMode = AutoCompleteMode.SuggestAppend, AutoCompleteSource = AutoCompleteSource.ListItems };
        private Label TabZero = new Label();
        private string[] wod = File.ReadAllLines(@"C:\Users\Dennis\OneDrive\Dokument\C#\WodDatabase.txt");
        
        public WodMenu()
        {
            //Outer form editing
            ShowIcon = false;
            Text = "Wods";
            Size = new Size(500, 500);
            Padding = new Padding(5);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            foreach (string s in wod)
            {
                string[] split = s.Split(',');
                GetWod.Items.AddRange(new object[] { split[0] });
            }

            TableLayoutPanel mainGrid = new TableLayoutPanel { RowCount = 5, ColumnCount = 2, Dock = DockStyle.Fill };
            mainGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
            mainGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            mainGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            mainGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            mainGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            mainGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            mainGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            Controls.Add(mainGrid);
            Button mainMenu = new Button { Text = "Main Menu", Dock = DockStyle.Fill, AutoSize = true, Anchor = AnchorStyles.Right };
            Label chooseWod = new Label { Font = new Font("San Serif", 10f), Text = "Select workout", Anchor = AnchorStyles.Bottom, AutoSize = true };
            Button timeWorkout = new Button { Font = new Font("San Serif", 10f), Text = "Time Workout", Anchor = AnchorStyles.Right, AutoSize = true };
            mainGrid.Controls.Add(WodDisplay);
            WodDisplay.RowTemplate.Height = 30;
            mainGrid.SetRowSpan(WodDisplay, 3);
            WodDisplay.TabStop = false;
            WodDisplay.ClearSelection();
            mainGrid.Controls.Add(chooseWod, 1, 0);
            mainGrid.Controls.Add(GetWod, 1, 1);
            mainGrid.Controls.Add(timeWorkout, 1, 2);
            mainGrid.Controls.Add(TabZero);
            ActiveControl = TabZero;
            mainGrid.Controls.Add(mainMenu, 1, 5);

            mainMenu.Click += ReturnToMainWindow;
            GetWod.SelectedIndexChanged += ComboBoxChanged;
        }
        private void ComboBoxChanged(object sender, EventArgs e)
        {
            WodDisplay.Rows.Clear();
            string[] split = wod[GetWod.SelectedIndex].Split(',');
            object[] every = new object[split.Length];
            for (int i = 0; i < split.Length; i++)
            {
                every[i] = split[i];
                WodDisplay.Rows.Add(every[i]);
            }
        }
        private void ReturnToMainWindow(object sender, EventArgs e)
        {
            Hide();
            MainMenu menu = new MainMenu();
            menu.ShowDialog();
            Close();
        }
        private void TabSelectTextBox(object sender, EventArgs e)
        {
            TextBox tab = (TextBox)sender;
            tab.Select(0, tab.Text.Length);
        }
    }
}
