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
	public partial class Choose:Form {
		public Choose() {
			InitializeComponent();
		}

		Main main;
		public Choose(Main f) {
			InitializeComponent();
			main = f;
		}

		private void button1_Click(object sender,EventArgs e) {
			comboBox1.Items.Clear();
			string title = textBox1.Text;
			string artist = textBox2.Text;
			string lengthS = textBox4.Text;
			string yearS = textBox3.Text;
			string genre = textBox5.Text;
			string addTitle = "";
			int length = -1;
			int year = -1;
			string search = "";
			bool f1 = false;
			int count = 0;
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
			if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(artist) && !string.IsNullOrEmpty(lengthS)
				&& !string.IsNullOrEmpty(yearS) && !string.IsNullOrEmpty(genre))
				f1 = true;

			if (!string.IsNullOrEmpty(title)) {
				search += "title LIKE '" + title + "'";
				count++;
			}
			if (!string.IsNullOrEmpty(artist)) {
				if (count > 0)
					search += " AND ";
				search += "artist LIKE '" + artist + "'";
				count++;
			}
			if (!string.IsNullOrEmpty(lengthS)) {
				if (count > 0)
					search += " AND ";
				search += "length LIKE '" + length + "'";
				count++;
			}
			if (!string.IsNullOrEmpty(yearS)) {
				if (count > 0)
					search += " AND ";
				search += "year LIKE '" + year + "'";
				count++;
			}
			if (!string.IsNullOrEmpty(genre)) {
				if (count > 0)
					search += " AND ";
				search += "genre LIKE '" + genre + "'";
				count++;
			}
			string conString, query;
			SqlConnection conn;
			SqlCommand command;
			SqlDataReader reader;
			conString = @"Data Source=.\SQLEXPRESS;Initial Catalog=songstar;Integrated Security=True;Pooling=False";
			conn = new SqlConnection(conString);
			conn.Open();
			if (f1 == true)
				query = "SELECT * FROM songs WHERE " + search;
			else
				query = "SELECT * FROM songs";
			command = new SqlCommand(query,conn);
			reader = command.ExecuteReader();
			while (reader.Read()) {
				title = reader.GetString(reader.GetOrdinal("title"));
				comboBox1.Items.Add(title);
			}
			conn.Close();
			return;
		}

		private void button2_Click(object sender,EventArgs e) {
			if (comboBox1.SelectedIndex >= 0) {
				main.ChangeMain("Now playing " + comboBox1.Text);
				Close();
			} else {
				label4.Text = "There is no selected song";
			}
		}
	}
}
