using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpresStore
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.SuspendLayout();

            this.Size = new Size(1238, 914);
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;

            this.ResumeLayout(false);
        }

    }
}
