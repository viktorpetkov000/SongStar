using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SongStar {
	public partial class Add:Form {
		public Add() {
			InitializeComponent();
		}

		Main main;
		public Add(Main f) {
			InitializeComponent();
			main = f;
		}

		private void button1_Click(object sender,EventArgs e) {
			string title = textBox1.Text;
			string artist = textBox2.Text;
			string lengthS = textBox4.Text;
			string yearS = textBox3.Text;
			string genre = textBox5.Text;
			int length = -1;
			int year = -1;
			if (!string.IsNullOrEmpty(lengthS)) {
				if (!Regex.IsMatch(lengthS,@"^\d+$")) {
					label4.Text = "Length must be in seconds";
					return;
				}
				length = int.Parse(lengthS);
				if (length < 0) {
					label4.Text = "Length must be a positive number";
					return;
				}
			}
			if (!string.IsNullOrEmpty(yearS)) {
				if (!Regex.IsMatch(yearS,@"^\d+$")) {
					label4.Text = "Year must be a number";
					return;
				}
				year = int.Parse(yearS);
				if (year < 0) {
					label4.Text = "Year must be a positive number";
					return;
				}
			}
			string conString, query;
			SqlConnection conn;
			SqlCommand command;
			conString = @"Data Source=.\SQLEXPRESS;Initial Catalog=songstar;Integrated Security=True;Pooling=False";
			conn = new SqlConnection(conString);
			conn.Open();
			query = "INSERT INTO songs(title,artist,length,year,genre) VALUES" +
				" ('" + title + "','" + artist + "','" + length + "','" + year + "','" + genre + "')";
			command = new SqlCommand(query,conn);
			int result = command.ExecuteNonQuery();
			if (result >= 0) {
				main.ChangeMain("You added " + title);
				conn.Close();
				Close();
				return;
			}
			conn.Close();
			label4.Text = "Error";
			return;
		}
	}
}
