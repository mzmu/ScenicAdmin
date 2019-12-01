using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Npgsql;

namespace ScenicAdmin
{
    public partial class LoginForm : Form
    {
        NpgsqlConnection conn = null;
        public LoginForm()
        {
            InitializeComponent();
            conn = getConnection();
        }

        // function:connect to database.
        protected NpgsqlConnection getConnection()
        {
            var connString = "Host=127.0.0.1;Username=postgres;Password=123456;Database=ScenicAdmin";
            conn = new NpgsqlConnection(connString);
            conn.Open();
            return conn;
        }

        // function:get the information of the user.
        protected string getUserInfo(string userName)
        {
            string sql = string.Format("select password from users where name = '{0}';", userName);
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                return null;
            }
            reader.Read();
            return reader.GetString(0);
        }

        // buttonFunction:login the system.
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string password = getUserInfo(tbxUserName.Text);
            if (password != tbxPassword.Text)
            {
                string message = "提示！用户名或密码错误";  // the appearance of the popup dialog.
                string caption = "登录失败";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                MessageBox.Show(message, caption, buttons);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // buttonFunction:register a new user.
        private void btnRegister_Click(object sender, EventArgs e)
        {
            string sql = string.Format("insert into users(name,password) values('{0}','{1}');", tbxUserName.Text,tbxPassword.Text);
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            cmd.ExecuteReader();
            string message = "恭喜！注册成功，请确认后登录";  // the appearance of the popup dialog.
            string caption = "注册成功";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBox.Show(message, caption, buttons);
        }


    }
}
