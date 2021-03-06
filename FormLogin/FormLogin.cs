﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace FormLogin
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        public string GetMD5(string text)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                str.Append(result[i].ToString("x2"));
            }
            return str.ToString();
        }  

        private void button1_Click(object sender, EventArgs e)
        {

            string resMD5 = GetMD5(this.passtext.Text);

            try{
                MySqlConnection connect;
                string cs = @"server=localhost;userid=root;database=akademik";
                connect = new MySqlConnection(cs);

                MySqlCommand SelectCom = new MySqlCommand("select * from akademik.login where user_id = '" + this.usertext.Text + "' and pass = '" + this.passtext.Text + "';", connect);

                MySqlDataReader myRead;

                connect.Open();
                myRead = SelectCom.ExecuteReader();
                int count = 0;
                while(myRead.Read()){
                    count += 1;
                }
                if(count==1){
                    myRead.Close();
                    MySqlCommand Cmd = new MySqlCommand("select * from akademik.login where user_id='" + this.usertext.Text + "' and pass='" + resMD5 + "' and left('" + this.usertext.Text + "',2)='01';", connect);

                    MySqlDataReader myRead2;

                    myRead2 = Cmd.ExecuteReader();
                    int count2 = 0;
                    while (myRead2.Read())
                    {
                        count2 += 1;
                    }
                    if (count2 == 1)
                    {
                        Formsiswa formsw = new Formsiswa();
                        connect.Close();
                        formsw.Show();
                        this.Hide();
                    }
                    else
                    {
                        myRead2.Close();
                        MySqlCommand Cmd2 = new MySqlCommand("select * from akademik.login where user_id='" + this.usertext.Text + "' and pass='" + resMD5 + "' and left('" + this.usertext.Text + "',2)='02';", connect);

                        MySqlDataReader myRead3;

                        myRead3 = Cmd2.ExecuteReader();
                        int count3 = 0;
                        while (myRead3.Read())
                        {
                            count3 += 1;
                        }
                        if (count3 == 1)
                        {
                            Formguru formgr = new Formguru();
                            connect.Close();
                            formgr.Show();
                            this.Hide();
                        }
                        else
                        {
                            myRead3.Close();
                            MySqlCommand Cmd3 = new MySqlCommand("select * from akademik.login where user_id='" + this.usertext.Text + "' and pass='" + this.passtext.Text + "' and left('" + this.usertext.Text + "',2)='03';", connect);

                            MySqlDataReader myRead4;

                            myRead4 = Cmd3.ExecuteReader();
                            int count4 = 0;
                            while (myRead4.Read())
                            {
                                count4 += 1;
                            }
                            if (count4 == 1)
                            {
                                Formstaff formstf = new Formstaff();
                                connect.Close();
                                formstf.Show();
                                this.Hide();
                            }
                            else
                            {
                                myRead4.Close();
                                MySqlCommand Cmd4 = new MySqlCommand("select * from akademik.login where user_id='" + this.usertext.Text + "' and pass='" + resMD5 + "' and left('" + this.usertext.Text + "',2)='04';", connect);

                                MySqlDataReader myRead5;

                                myRead5 = Cmd4.ExecuteReader();
                                int count5 = 0;
                                while (myRead5.Read())
                                {
                                    count5 += 1;
                                }
                                if (count5 == 1)
                                {
                                    Formkepalasekolah formkepsek = new Formkepalasekolah();
                                    connect.Close();
                                    formkepsek.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("Username or Password are Incorrect");
                                }
                            }
                        }
                    }
                }
                else{
                    MessageBox.Show("Username or Password are Incorrect");
                }
            }
            catch(Exception ex){
                MessageBox.Show(ex.Message);
            }
        }
    }
}
