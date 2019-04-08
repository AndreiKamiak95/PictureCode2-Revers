/*
 * Создано в SharpDevelop.
 * Пользователь: KAMIAK
 * Дата: 06.08.2018
 * Время: 14:39
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PictureCode2_Revers
{
	public partial class MainForm : Form
	{
		Bitmap pict;
		Bitmap pict2;
		List<byte> values_list;
		
		public MainForm()
		{
			InitializeComponent();
			txtHeight.Enabled = false;
			txtWidth.Enabled = false;
			values_list = new List<byte>();
			comboBox1.SelectedIndex = 0;
		}
		void BtnConvertClick(object sender, EventArgs e)
		{
			if(comboBox1.SelectedIndex == 0)
			{
				if(string.IsNullOrEmpty(txtCode.Text)==true)
				{
					MessageBox.Show("Пустое поле ввода", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				string ful_str = txtCode.Text;
				ful_str = ful_str.Trim(',');
				ful_str = ful_str.Trim();
				string[] numbers_str = ful_str.Split(',');
				for(int i = 0; i < numbers_str.Length; i++)
				{
					numbers_str[i] = numbers_str[i].Trim(Environment.NewLine.ToCharArray());
					numbers_str[i] = numbers_str[i].Trim();
				}
				try 
				{
					if(checkBox1.Checked == false)
					{
						int height = Convert.ToInt32(numbers_str[0], 16);
						int buf = Convert.ToInt32(numbers_str[1], 16);
						height = height | (buf<<8);
						int width = Convert.ToInt32(numbers_str[2], 16)*8;
						width = width | ((Convert.ToInt32(numbers_str[3], 16)*8)<<8);
						pict = new Bitmap(width, height);
						
						for(int i = 4; i < numbers_str.Length; i++)
							values_list.Add(Convert.ToByte(numbers_str[i], 16));
					}
					else
					{
						pict = new Bitmap(Convert.ToInt32(txtWidth.Text), Convert.ToInt32(txtHeight.Text));
						for(int i = 0; i < numbers_str.Length; i++)
							values_list.Add(Convert.ToByte(numbers_str[i], 16));
					}
					
					int cur_x=0, cur_y=0;
					for(int i = 0; i < values_list.Count; i++)
					{
						byte bit8 = values_list[i];
						UInt16 m = 128;
						for(int k = 0; k < 8; k++)
						{
							if((bit8&m)==0)
								pict.SetPixel(cur_x+k, cur_y, Color.White);
							else
								pict.SetPixel(cur_x+k, cur_y, Color.Black);
							m/=2;
						}
						cur_x+=8;
						if(cur_x>=pict.Width)
						{
							cur_x=0;
							cur_y++;
						}
					}
				}
				catch(Exception)
				{	
					MessageBox.Show("Неверная строка ввода/не введены размеры изображения", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				pictureBox.Image = pict;
				values_list.Clear();
			}
			if(comboBox1.SelectedIndex == 1)
			{
				if(string.IsNullOrEmpty(txtCode.Text)==true)
				{
					MessageBox.Show("Пустое поле ввода", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				string ful_str = txtCode.Text;
				ful_str = ful_str.Trim(',');
				ful_str = ful_str.Trim();
				string[] numbers_str = ful_str.Split(',');
				for(int i = 0; i < numbers_str.Length; i++)
				{
					numbers_str[i] = numbers_str[i].Trim(Environment.NewLine.ToCharArray());
					numbers_str[i] = numbers_str[i].Trim();
				}
				try {
					
					for(int i = 0; i < numbers_str.Length; i++)
						values_list.Add(Convert.ToByte(numbers_str[i], 16));
					
					int width = Convert.ToInt32(txtWidth.Text);
					int height = Convert.ToInt32(txtHeight.Text);
					
					pict2 = new Bitmap(width, height);
					
					int cur_x = 0, cur_y = 0;
					
					for(int i = 0; i < numbers_str.Length; i++)
					{
						byte bit8 = values_list[i];
						UInt16 m = 128;
						if(i==300)
							i = 300;
						for(int k = 0; k < 8; k++)
						{
							if((bit8&m)==0)
								pict2.SetPixel(cur_x, cur_y+7-k, Color.White);
							else
								pict2.SetPixel(cur_x, cur_y+7-k, Color.Black);
							m/=2;
						}
						cur_x++;
						if(cur_x>=width)
						{
							cur_x=0;
							cur_y+=8;
							if(cur_y >= height)
								break;
						}
					}
					
				} catch (Exception) {
					
					MessageBox.Show("Неверная строка ввода/не введены размеры изображения", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				pictureBox.Image = pict2;
				values_list.Clear();
			}
		}
		void BtnSaveClick(object sender, EventArgs e)
		{
			saveFileDialog1.Filter = "Bitmap|*.bmp";
			if(saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				if(comboBox1.SelectedIndex == 1)
					pict = pict2;
				pict.Save(saveFileDialog1.FileName);
			}
		}
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			if(checkBox1.Checked == true)
			{
				txtWidth.Enabled = true;
				txtHeight.Enabled = true;
			}
			else
			{
				txtWidth.Text = "";
				txtHeight.Text = "";
				txtWidth.Enabled = false;
				txtHeight.Enabled = false;
			}
		}
	}
}
