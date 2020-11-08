using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SSE.BOL;
using SSE.BLL;

namespace SSE.UIL
{
    public partial class FrmMain : Form
    {
        int selectedRow;
        StudentBLL bll;
        List<Student> students;
        public FrmMain()
        {
            this.bll = new StudentBLL();
            this.students = new List<Student>();
            InitializeComponent();
            RefreshData();
        }

        private void FrmMain_Paint(object sender, PaintEventArgs e)
        {
            Util.PaintDropShadow(sender, e, this, pnlShadowTop, 40, 20);
        }

        private void pnlTranslucien_Paint(object sender, PaintEventArgs e)
        {
            Util.PaintDropShadow(sender, e, this, pnlStudents, 20, 20);
        }
        public void RefreshData()
        {
            try
            {
                List<Student> sts = bll.Read();
                lblQuantity.Text = sts.Count + " Estudientes";
                if (txtFilter.Text.Length > 0)
                {
                    this.students = bll.ReadByFilter(txtFilter.Text);
                }
                else
                {
                    this.students = sts;
                }
                this.dgvStudents.DataSource = this.students;
                selectedRow = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void SendStudentToSon(bool modify)
        {
            try
            {
                if (students.Count > 0)
                {
                    Student s = new Student();
                    s.Id = (long)dgvStudents.Rows[selectedRow].Cells[0].Value;
                    s.Code = (string)dgvStudents.Rows[selectedRow].Cells[1].Value;
                    s.Name = (string)dgvStudents.Rows[selectedRow].Cells[2].Value;
                    s.LastName = (string)dgvStudents.Rows[selectedRow].Cells[3].Value;
                    s.IdNumber = (string)dgvStudents.Rows[selectedRow].Cells[4].Value;
                    s.Genre = (char)dgvStudents.Rows[selectedRow].Cells[5].Value;
                    s.Section = (string)dgvStudents.Rows[selectedRow].Cells[6].Value;
                    s.PersonalPhoto = (Image)dgvStudents.Rows[selectedRow].Cells[7].Value;
                    s.SectionPhoto = (Image)dgvStudents.Rows[selectedRow].Cells[8].Value;
                    s.PLunch = (bool)dgvStudents.Rows[selectedRow].Cells[9].Value;
                    s.PFreeLessons = (bool)dgvStudents.Rows[selectedRow].Cells[10].Value;
                    s.PFreeAfternoon = (bool)dgvStudents.Rows[selectedRow].Cells[11].Value;
                    FrmStudent frm = new FrmStudent(this, s, modify);
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowForm();
                }
                else
                {
                    MessageBox.Show("Debe haber un registro seleccionado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Delete()
        {
            try
            {
                if (students.Count > 0)
                {
                    long id = (long)dgvStudents.Rows[selectedRow].Cells[0].Value;
                    string name = (string)dgvStudents.Rows[selectedRow].Cells[2].Value;

                    if (MessageBox.Show("¿Desea eliminar a " + name + "?",
                        "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        bll.Delete(id);
                    }

                }
                else
                {
                    MessageBox.Show("Debe haber un registro seleccionado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                RefreshData();
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            FrmStudent frm = new FrmStudent(this);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowForm();
        }

        private void FrmMain_SizeChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void dgvStudents_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            selectedRow = dgv.CurrentCell.RowIndex;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SendStudentToSon(true);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            SendStudentToSon(false);
        }

        private void pnlStudents_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
