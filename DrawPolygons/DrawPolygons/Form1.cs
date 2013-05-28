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

namespace DrawPolygons
{
    public partial class Form1 : Form
    {
        private LineDocument lineDoc;
        private PolygonDocument polygonDoc;

        //дали е кликнато за да почне да се оформува фигура
        private bool isClicked;
        //тековна позиција на маусот
        private Point currentPoint;
        //првата точка од правата
        private Point firstLinePoint;
        //првата точка од каде што фигурата почнала да се оформува
        private Point firstClickedPoint;

        //податоци за малото квадратче
        private SmallSquare square;
        private static readonly int WIDTH = 16;

        //името под кое ќе се зачува фајлот
        private string FileName;

        private static readonly int DX = 5;
        private static readonly int DY = 5;
        public Form1()
        {
            InitializeComponent();
            polygonDoc = new PolygonDocument();
            isClicked = false;
            this.DoubleBuffered = true;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if(!isClicked)
            {
                currentPoint.X = e.X;
                currentPoint.Y = e.Y;
                firstLinePoint.X = e.X;
                firstLinePoint.Y = e.Y;
                firstClickedPoint.X = e.X;
                firstClickedPoint.Y = e.Y;
                lineDoc = new LineDocument();
                square = new SmallSquare(WIDTH, e.X, e.Y);
                square.Flag = false;
                isClicked = true;
            }
            else 
            {
                if (square.isHit(e.Location))
                {
                    lineDoc.Lines.Add(new Line(firstLinePoint, firstClickedPoint));
                    polygonDoc.Polygons.Add(new Polygon(lineDoc.Lines, new Random()));
                    firstLinePoint = new Point();
                    currentPoint = new Point();
                    lineDoc = null;
                    isClicked = false;
                }
                else 
                {
                    lineDoc.Lines.Add(new Line(firstLinePoint, e.Location));
                    firstLinePoint.X = e.X;
                    firstLinePoint.Y = e.Y;
                }
            }
            Invalidate(true);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!currentPoint.IsEmpty)
            {
                currentPoint.X = e.X;
                currentPoint.Y = e.Y;
                if (isNearFirstPoint(currentPoint))
                {
                    square.Flag = true;
                }
                else
                {
                    square.Flag = false;
                }
                Invalidate(true);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);

            if (lineDoc != null)
            {
                lineDoc.Draw(e.Graphics);
            }

            polygonDoc.Draw(e.Graphics);
            e.Graphics.DrawLine(new Pen(Color.Black, 2), firstLinePoint, currentPoint);
            if (isClicked)
            {
                if (square.Flag == true)
                {
                    square.Draw(e.Graphics);
                }
            }
        }

        //дали тековната точка е во близина на почетната точка
        private bool isNearFirstPoint(Point point)
        {
            if (Math.Abs(firstClickedPoint.X - point.X) <= (WIDTH / 2) && Math.Abs(firstClickedPoint.Y - point.Y) <= (WIDTH / 2))
            {
                return true;
            }
            return false;
        }

        private void save()
        {
            if (FileName == null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Polygons file doc (*.poly)|*.poly";
                saveFileDialog.Title = "Save polygons file doc";
                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FileName = saveFileDialog.FileName;
                }
            }

            if (FileName != null)
            {
                using (FileStream fileStream = new FileStream(FileName, FileMode.Create))
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fileStream, polygonDoc);
                }
            }
        }

        private void open()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Polygons doc file (*.poly)|*.poly";
            openFileDialog.Title = "Open polygons doc";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileName = openFileDialog.FileName;
                try
                {
                    using (FileStream fileStream = new FileStream(FileName, FileMode.Open))
                    {
                        IFormatter formatter = new BinaryFormatter();
                        polygonDoc = (PolygonDocument)formatter.Deserialize(fileStream);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not read file: " + FileName);
                    FileName = null;
                    return;
                }
                Invalidate(true);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            save();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            open();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            open();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!isClicked)
            {
                if (e.KeyCode == Keys.Up)
                {
                    polygonDoc.Move(0, -DY);
                    Invalidate(true);
                }

                if (e.KeyCode == Keys.Down)
                {
                    polygonDoc.Move(0, DY);
                    Invalidate(true);
                }
                if (e.KeyCode == Keys.Left)
                {
                    polygonDoc.Move(-DX, 0);
                    Invalidate(true);
                }
                if (e.KeyCode == Keys.Right)
                {
                    polygonDoc.Move(DX, 0);
                    Invalidate(true);
                }
            }
        }
    }
}
