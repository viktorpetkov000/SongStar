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
	public partial class Register:Form {
		public Register() {
			InitializeComponent();
		}

		private void Register_Load(object sender,EventArgs e) {

		}

		private void button1_Click(object sender,EventArgs e) {
			string username = textBox1.Text;
			string password = textBox2.Text;
			string cpassword = textBox3.Text;
			if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(cpassword)) {
				label4.Text = "You have empty fields";
				return;
			}
			if (username.Length < 3) {
				label4.Text = "Username must be at least 3 symbols long";
				return;
			}
			if (password.Length < 8) {
				label4.Text = "Password must be at least 8 symbols long";
				return;
			}
			if (password != cpassword) {
				label4.Text = "Passwords don't match";
				return;
			}
			string conString, query;
			SqlConnection conn;
			SqlCommand command;
			SqlDataReader reader;
			string salt = Guid.NewGuid().ToString().Replace("-","").ToLower();
			var provider = MD5.Create();
			byte[] bytesFirst = provider.ComputeHash(Encoding.ASCII.GetBytes(password));
			string hashFirst = BitConverter.ToString(bytesFirst).Replace("-","").ToLower();
			byte[] bytes = provider.ComputeHash(Encoding.ASCII.GetBytes(hashFirst + salt));
			string hashedPass = BitConverter.ToString(bytes).Replace("-","").ToLower();
			conString = @"Data Source=.\SQLEXPRESS;Initial Catalog=songstar;Integrated Security=True;Pooling=False";
			conn = new SqlConnection(conString);
			conn.Open();
			query = "SELECT * FROM accounts WHERE username = '" + username + "'";
			command = new SqlCommand(query,conn);
			reader = command.ExecuteReader();
			if (reader.Read()) {
				conn.Close();
				label4.Text = "This username already exists";
			}
			conn.Close();
			conn.Open();
			query = "INSERT INTO accounts(username,password,salt) VALUES" +
				" ('" + username + "','" + hashedPass + "','" + salt + "')";
			command = new SqlCommand(query,conn);
			int result = command.ExecuteNonQuery();
			if (result >= 0) {
				conn.Close();
				Close();
			}
			conn.Close();
			label4.Text = "Error";
		}
	}
}
