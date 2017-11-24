using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using VkNet;

namespace SearchBotVK
{

    public partial class CaptchaForm : Form
    {
        public CaptchaForm(string c)
        {
            InitializeComponent();
            pictureBox1.ImageLocation = c; // show captcha image

        }

        private void CaptchaForm_Load(object sender, EventArgs e)
        {

        }
        //this metod to return captcha key in mainForm
        public string Captcha()
        {
           
            string s = textBox1.Text;
            return s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Captcha(); // method
            this.Close(); //close CapthcaForm
        }

       
    }
}
