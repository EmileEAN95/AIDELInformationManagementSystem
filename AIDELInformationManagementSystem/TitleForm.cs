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
    public partial class TitleForm : Form
    {
        public TitleForm()
        {
            InitializeComponent();
        }

        private void TitleForm_Load(object sender, EventArgs e)
        {
            if (!DocumentLoader.ImportSystemDataDictionaryFromCSV())
            {
                MessageBox.Show("Error initiating application!");
                btn_courseAndStudentInfo.Enabled = false;
                btn_nonInstitutionalExamInfo.Enabled = false;
            }
        }

        private void btn_courseAndStudentInfo_Click(object sender, EventArgs e)
        {
            this.LogAndSwitchTo(new DataSelectionForm());
        }

        private void btn_nonInstitutionalExamInfo_Click(object sender, EventArgs e)
        {
            this.LogAndSwitchTo(new LoginForm_NonInstitutionalExam());
        }
    }
}
