using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.PerformanceData;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VkNet;
using System.Windows.Forms;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;

namespace SearchBotVK
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public long namegroup;
        public string captchakey;
        public bool flag = false;
        public long count;
        public int count1;
        public string nameurl;

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            //call form1 in case form2 close
            Form ifrm = Application.OpenForms[0];
            ifrm.StartPosition =
                FormStartPosition
                    .Manual; 
            ifrm.Left = this.Left; 
            ifrm.Top = this.Top; 
            ifrm.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // if users want set quanity post
            //if not some elements will not be show
            if (checkBox1.Checked == true)
            {
                textBox3.Visible = true; 
                button3.Visible = true;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //number of posts
            var s = textBox3.Text;
            count1 = int.Parse(s);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            VkApi vk = Singlet.Api; 
            long? user_Id = vk.UserId;
            var groups = vk.Groups.Get(new GroupsGetParams() { UserId = user_Id, Extended = true, Count = count}).ToList(); // get all users community
            try
            {
                
                foreach (var g in groups) // search necessary group in all community
                {
                    
                    label2.Text = "Сверяем..";
                    if (g.Name == textBox1.Text)
                    {
                        nameurl = g.ScreenName; // short name group
                        namegroup = g.Id; // id group
                        label2.Text = "Верно";
                        label3.Visible = true; // set visible
                        textBox2.Visible = true; // same
                        button2.Visible = true; //same
                        
                        break;
                    }

                }
            }
            catch (Exception exception) //if users is not in the community
            {
                MessageBox.Show("Вы не состоите в этой группе");

            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //That there was no empty string
            if (textBox1.Text == String.Empty) 
                button1.Visible = false;
            else
                button1.Visible = true;
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            
            VkApi vk = Singlet.Api;
            
            var userId = vk.UserId; // usersId
            //search on request
            var groups = vk.Wall.Search(new WallSearchParams() 
            {
                OwnerId = -namegroup,
                Count = count1,
                Extended = true,
                Query = textBox2.Text
            });
            string s1 = textBox2.Text;
            string pattern = s1 + @"(\w*)"; // search in received text
            long? captcha_sid = null; // id captcha
            string captcha_key = null; // entered key
            List<MediaAttachment> attachments = new List<MediaAttachment>(); // attachments
            foreach (var g in groups) 
            {
                try
                {
                    
                    var s = g.Text;
                    
                    var c = g.Attachment.Instance as Photo; // clothes photo

                    attachments.Add(c); // add in collection
                    if (Regex.IsMatch(s, pattern)) // if conformity 
                        // send message user in VK
                        vk.Messages.Send(new MessagesSendParams()
                        {

                            UserId = userId, // id user who receive the message
                            Message = s + " \n https://vk.com/" + nameurl + "?w=wall" + (-namegroup) + ("_" + g.Id), // this construction for message contains url on this clothes
                            Attachments = attachments, 
                            CaptchaSid = captcha_sid, //
                            CaptchaKey = captcha_key

                        });
                    attachments.Clear();// clear collection so that there will be no repetition
                }
                //messages are sent too fast, so there is a captcha
                catch (VkNet.Exception.CaptchaNeededException cne)
                {
                    captcha_sid = cne.Sid;
                    var c = cne.Img.AbsoluteUri;
                    var form = new CaptchaForm(c); // I transfer to the construct form captcha image
                    form.ShowDialog(); // show CapthcaForm
                    captcha_key = form.Captcha();// get captchakey from textBox in CaptchaForm
                    attachments.Clear(); // clear collection so that there will be no repetition
                    continue;

                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("При установке свыше 15 постов начнет вылезать капча, возможен повтор постов"); //some info for users
        }
    }

}


