﻿using System;
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
			Login login = new Login();
			login.ShowDialog();
		}

		private void button2_Click(object sender,EventArgs e) {
			Register register = new Register();
			register.ShowDialog();
		}
	}
}
