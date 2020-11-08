using SSE.UIL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SSE.BLL;
using SSE.BOL;

namespace SSE
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSingIn_Click(object sender, EventArgs e)
        {
            try
            {
                UserBLL bll = new UserBLL();
                User u = new User();
                u.Name = txtUser.Text;
                u.Password = txtPassword.Text;
                u=bll.Login(u);
                if (u.Id > 0)
                {
                    FrmMain frm = new FrmMain(u,this);
                    this.txtUser.Text = "";
                    this.txtPassword.Text = "";
                    frm.Visible = true;
                    this.Visible = false;
                }
                else
                {
                    throw new Exception("Las credenciales no coinciden");
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}
