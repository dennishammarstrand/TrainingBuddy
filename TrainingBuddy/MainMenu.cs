using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace TrainingBuddy
{
    class MainMenu : Form
    {
        private Label TabZero = new Label();

        public MainMenu()
        {
            //Outer form editing
            ShowIcon = false;
            Text = "Training";
            Size = new Size(250, 250);
            Padding = new Padding(15);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            //Main menu grid with all paths
            TableLayoutPanel menuGrid = new TableLayoutPanel { ColumnCount = 1, Dock = DockStyle.Fill };
            Button statsMenu = new Button { Width = 150, Font = new Font("San Serif", 15f), Text = "Pr Statistics", AutoSize = true, Anchor = AnchorStyles.Top };
            Button timerMenu = new Button { Width = 150, Font = new Font("San Serif", 15f), Text = "Timer Menu", AutoSize = true, Anchor = AnchorStyles.Top };
            Button wodMenu = new Button { Width = 150, Font = new Font("San Serif", 15f), Text = "Wod Menu", AutoSize = true, Anchor = AnchorStyles.Top };
            Controls.Add(menuGrid);
            menuGrid.Controls.Add(statsMenu);
            menuGrid.Controls.Add(timerMenu);
            menuGrid.Controls.Add(wodMenu);
            menuGrid.Controls.Add(AddImage(@"C:\Users\Dennis\OneDrive\Dokument\C#\1200x630wa.png"), 0, 4);
            menuGrid.Controls.Add(TabZero, 0, 5);
            ActiveControl = TabZero;
            //Events
            statsMenu.Click += GoToStatsMenuHandler;
            timerMenu.Click += GoToTimerMenuHandler;
            wodMenu.Click += GoToWodMenuHandler;
        }
        //Event handlers
        private void GoToWodMenuHandler(object sender, EventArgs e)
        {
            Hide();
            WodMenu menu = new WodMenu();
            menu.ShowDialog();
            Close();
        }
        private void GoToStatsMenuHandler(object sender, EventArgs e)
        {
            Hide();
            Stats menu = new Stats();
            menu.ShowDialog();
            Close();
        }
        private void GoToTimerMenuHandler(object sender, EventArgs e)
        {
            Hide();
            TimerMenu menu = new TimerMenu();
            menu.ShowDialog();
            Close();
        }
        //Adding image method
        private PictureBox AddImage(string imageLocation)
        {
            return new PictureBox { ImageLocation = imageLocation, Size = new Size(55, 55), SizeMode = PictureBoxSizeMode.Zoom, Anchor = AnchorStyles.Top };
        }
    }
}
