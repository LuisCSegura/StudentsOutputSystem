using SSE.BOL;
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

namespace SSE.UIL
{
    public partial class FrmStudent : Form
    {
        Student student;
        StudentBLL bll;
        FrmMain parent;
        public FrmStudent(FrmMain p)
        {
            parent = p;
            this.student = new Student();
            this.bll = new StudentBLL();
            InitializeComponent();
            rdbMan.Checked = true;
            btnReady.Visible = false;

        }
        public FrmStudent(FrmMain p, Student s, bool modify)
        {
            parent = p;
            this.student = s;
            this.bll = new StudentBLL();
            InitializeComponent();
            lblStudent.Text = modify?"ACTUALIZAR ESTUDIENTE": "CONSULTAR ESTUDIANTE";
            txtCode.Text=this.student.Code;
            txtName.Text=this.student.Name;
            txtLastName.Text=this.student.LastName;
            txtIdNumber.Text=this.student.IdNumber;
            this.rdbMan.Checked=this.student.Genre == 'M';
            this.rdbFemale.Checked=this.student.Genre == 'F';
            txtSection.Text=this.student.Section;
            pbxPersonalPhoto.Image=this.student.PersonalPhoto;
            pbxSectionPhoto.Image=this.student.SectionPhoto;
            cbxPLunch.Checked=this.student.PLunch;
            cbxPFreeLessons.Checked=this.student.PFreeLessons;
            cbxPFreeAfternoon.Checked=this.student.PFreeAfternoon;
            pnlData.Enabled = modify;
            pnlPermits.Enabled = modify;
            btnSelPersonalPhoto.Visible = modify;
            btnSelSectionPhoto.Visible = modify;
            btnCancel.Visible = modify;
            btnSave.Visible = modify;
            btnReady.Visible = !modify;

        }

        private void FrmStudent_Paint(object sender, PaintEventArgs e)
        {
            Util.PaintDropShadow(sender, e, this, pnlBottom, 30, 15);
            Util.PaintDropShadow(sender, e, this, pnlTop, 30, 15);
        }
        public void ShowForm()
        {
            ShowDialog(parent);

        }
        private void Save()
        {
            try
            {
                this.student.Code = txtCode.Text.Trim();
                this.student.Name = txtName.Text.Trim();
                this.student.LastName = txtLastName.Text.Trim();
                this.student.IdNumber = txtIdNumber.Text.Trim();
                this.student.Genre = rdbMan.Checked? 'M': 'F';
                this.student.Section = txtSection.Text.Trim();
                this.student.PersonalPhoto = pbxPersonalPhoto.Image;
                this.student.SectionPhoto = pbxSectionPhoto.Image;
                this.student.PLunch = cbxPLunch.Checked;
                this.student.PFreeLessons = cbxPFreeLessons.Checked;
                this.student.PFreeAfternoon = cbxPFreeAfternoon.Checked;

                if (this.student.Id == 0)
                {
                    bll.Create(this.student);
                }
                else
                {
                    bll.Update(student);
                }
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                parent.RefreshData();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            
        }

        private void btnSelPersonalPhoto_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                if (open.ShowDialog() == DialogResult.OK)
                { 
                    pbxPersonalPhoto.Image = new Bitmap(open.FileName);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnSelSectionPhoto_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                if (open.ShowDialog() == DialogResult.OK)
                {
                    pbxSectionPhoto.Image = new Bitmap(open.FileName);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void pnlPictures_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnReady_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSeeSectionPhoto_Click(object sender, EventArgs e)
        {
            FrmPicture frm = new FrmPicture(this.pbxSectionPhoto.Image);
            frm.Visible = true;
        }

        private void btnSeePersonalPhoto_Click(object sender, EventArgs e)
        {
            FrmPicture frm = new FrmPicture(this.pbxPersonalPhoto.Image);
            frm.Visible = true;
        }
    }
}
