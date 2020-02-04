using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

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
			if (openFileDialog1.FileNames == null)
			{
				MessageBox.Show("Please select images");
				return;
			}
			int fileCount = openFileDialog1.FileNames.Length * 2;
			int index = 1;
			string newFileName = openFileDialog1.FileNames[0];
			string outputDirectoryName = Path.Combine(Path.GetDirectoryName(newFileName), "Splitter output");
			Directory.CreateDirectory(outputDirectoryName);
			SplitAllImages(ref fileCount, ref index, outputDirectoryName);
			MessageBox.Show($"All files successfully created.");
			Process.Start("explorer.exe", outputDirectoryName);
		}

		private void SplitAllImages(ref int fileCount, ref int index, string outputDirectoryName)
		{
			Bitmap image;
			foreach (var fileName in openFileDialog1.FileNames)
			{
				image = new Bitmap(fileName);
				RectangleF rect1 = new RectangleF(0, 0, image.Width / 2, image.Height);
				RectangleF rect2 = new RectangleF(image.Width / 2, 0, image.Width / 2, image.Height);
				System.Drawing.Imaging.PixelFormat format = image.PixelFormat;
				Bitmap left = image.Clone(rect1, format);
				Bitmap right = image.Clone(rect2, format);
				left.Save(Path.Combine(outputDirectoryName, Path.ChangeExtension(fileCount.ToString(), "png")));
				--fileCount;
				right.Save(Path.Combine(outputDirectoryName, Path.ChangeExtension(index.ToString(), "png")));
				index++;
			}
		}
	}
}
