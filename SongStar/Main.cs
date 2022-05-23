using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SongStar {
	public partial class Main:Form {
		public Main() {
			InitializeComponent();
		}

		private void button1_Click(object sender,EventArgs e) {
			Login login = new Login(this);
			login.ShowDialog();
		}

		private void button2_Click(object sender,EventArgs e) {
			Register register = new Register(this);
			register.ShowDialog();
		}

		public void ChangeMain(string text) {
			button1.Hide();
			button2.Hide();
			label1.Text = text;
			button3.Show();
			button5.Show();
		}

		private void button3_Click(object sender,EventArgs e) {
			Choose choose = new Choose(this);
			choose.ShowDialog();
		}

		private void button5_Click(object sender,EventArgs e) {
			Add add = new Add(this);
			add.ShowDialog();
		}
	}
}
