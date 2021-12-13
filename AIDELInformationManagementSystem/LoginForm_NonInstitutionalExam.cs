using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIDELInformationManagementSystem
{
    public partial class LoginForm_NonInstitutionalExam : Form
    {
        public LoginForm_NonInstitutionalExam()
        {
            InitializeComponent();
        }

        private void LoginForm_NonInstitutionalExam_Load(object sender, EventArgs e)
        {
            phTxtBx_password.ActivatePlaceHolderEvents();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            DataContainer container = DataContainer.Instance;
            if (CoreValues.ExternalExamRegistrationInfo_Password != phTxtBx_password.Text)
                MessageBox.Show("Wrong password!");
            else
                this.LogAndSwitchTo(new RegistrationInfoForm());
        }

        private void btn_return_Click(object sender, EventArgs e)
        {
            this.SwitchToPrevious();
        }
    }
}
