using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace ImageDivider
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			openFileDialog1.ShowDialog();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Bitmap image;
			if (openFileDialog1.FileNames == null)
			{
				MessageBox.Show("Please select images");
				return;
			}
			int fileCount = openFileDialog1.FileNames.Length * 2;
			int index = 0;
			string newFileName = String.Empty;
			foreach (var fileName in openFileDialog1.FileNames)
			{
				image = new Bitmap(fileName);
				newFileName = fileName;
				newFileName = newFileName.Remove(newFileName.LastIndexOf('\\'));
				newFileName = newFileName + "\\Splitter output";
				RectangleF rect1 = new RectangleF(0, 0, image.Width / 2, image.Height);
				RectangleF rect2 = new RectangleF(image.Width / 2, 0, image.Width / 2, image.Height);
				System.Drawing.Imaging.PixelFormat format = image.PixelFormat;
				Bitmap left = image.Clone(rect1, format);
				Bitmap right = image.Clone(rect2, format);
				Directory.CreateDirectory(newFileName);
				left.Save(newFileName + "\\" + fileCount + ".png");
				--fileCount;
				right.Save(newFileName + "\\" + index + ".png");
				index++;
			}
			MessageBox.Show($"All files successfully created.");
			Process.Start("explorer.exe", newFileName);
		}
	}
}
