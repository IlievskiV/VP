using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace EatingPies
{
    public partial class Form1 : Form
    {
        private PieDocument doc;
        private Color color;
        private Timer timer;

        private static readonly int RADIUS = 30;

        private string fileName;

        public Form1()
        {
            InitializeComponent();
            doc = new PieDocument();
            color = Color.Blue;
            timer = new Timer();
            timer.Interval = 500;
            timer.Tick += new EventHandler(timer_Tick);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            doc.ChangePiesAngle();
            for (int i = doc.Pies.Count - 1; i >= 0; i--)
            {
                if (doc.Pies[i].flag == true)
                {
                    doc.Pies.RemoveAt(i);
                }
            }
            Invalidate(true);
        }

        private void startToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            doc.Draw(e.Graphics);
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            doc.Pies.Add(new Pie(e.X, e.Y, RADIUS, 360, color));
            Invalidate(true);
        }

        private void chooseColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                color = colorDialog.Color;
            }
        }

        public void saveFile()
        {
            if (fileName == null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Save Pie doc (*.pie)|*.pie";
                saveFileDialog.Title = "Save Pie doc";
                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = saveFileDialog.FileName;
                }
            }

            if (fileName != null)
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fileStream, doc);
                }
            }
        }

        public void openFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pie doc file (*.pie)|*.pie";
            openFileDialog.Title = "Open pie doc";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileName = openFileDialog.FileName;
                try
                {
                    using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
                    {
                        IFormatter formatter = new BinaryFormatter();
                        doc = (PieDocument)formatter.Deserialize(fileStream);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not read file: " + fileName);
                    fileName = null;
                    return;
                }
                Invalidate(true);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Stop();
            openFile();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            timer.Stop();
            openFile();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Дали сакате да го зачувате документот?", "Зачувај документ", MessageBoxButtons.YesNo)
                == System.Windows.Forms.DialogResult.Yes)
            {
                saveFile();
            }
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            doc = new PieDocument();
            Invalidate(true);
        }
    }
}
