using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SongStar {
	public partial class Login:Form {
		public Login() {
			InitializeComponent();
		}

		Main main;
		public Login(Main f) {
			InitializeComponent();
			main = f;
		}

		private void button1_Click(object sender,EventArgs e) {
			string username = textBox1.Text;
			string password = textBox2.Text;
			if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) {
				label3.Text = "You have empty fields";
				return;
			}
			string conString, query;
			SqlConnection conn;
			SqlCommand command;
			SqlDataReader reader;
			conString = @"Data Source=.\SQLEXPRESS;Initial Catalog=songstar;Integrated Security=True;Pooling=False";
			conn = new SqlConnection(conString);
			query = "SELECT * FROM Accounts WHERE username = '" + username + "'";
			conn.Open();
			command = new SqlCommand(query,conn);
			reader = command.ExecuteReader();
			var provider = MD5.Create();
			string salt, hashedPass, hashFirst, dbPass;
			byte[] bytes, bytesFirst;
			if (reader.Read()) {
				salt = reader.GetString(reader.GetOrdinal("salt"));
				dbPass = reader.GetString(reader.GetOrdinal("password"));
				bytesFirst = provider.ComputeHash(Encoding.ASCII.GetBytes(password));
				hashFirst = BitConverter.ToString(bytesFirst).Replace("-","").ToLower();
				bytes = provider.ComputeHash(Encoding.ASCII.GetBytes(hashFirst + salt));
				hashedPass = BitConverter.ToString(bytes).Replace("-","").ToLower();
				if (hashedPass == dbPass) {
					conn.Close();
					main.ChangeMain("Welcome " + username);
					Close();
				} else {
					conn.Close();
					label3.Text = "Incorrect username or password";
				}
			}
			conn.Close();
			label3.Text = "Error";
		}
	}
}
