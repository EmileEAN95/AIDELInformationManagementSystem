using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIDELInformationManagementSystem
{
    public partial class RegistrationInfoForm : Form
    {
        public RegistrationInfoForm()
        {
            InitializeComponent();
        }

        private void RegistrationInfoForm_Load(object sender, EventArgs e)
        {
            DocumentLoader.ImportRegistrationInfoListFromCSV();

            InitializeMainViewTable();

            RefreshMainViewTable();
            dgv_main.ClearSelection();
            dgv_main.SelectionChanged += (_sender, _e) => dgv_main.ClearSelection();
        }

        private Faculty m_faculty;

        #region Component Events
        private void btn_import_Click(object sender, EventArgs e)
        {
            try
            {
                if (opnFlDlg_importingFile.ShowDialog() == DialogResult.OK)
                {
                    if (DocumentLoader.LoadRegistrationInfo_ExternalFromExcel(opnFlDlg_importingFile.FileName))
                    {
                        MessageBox.Show("Imported data successfully!");
                        RefreshMainViewTable();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown Error: " + ex.Message + "Please try it again.");
            }
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Would you really like to upload the registration info?", "Caution!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                SharePointConnectionManager.Instance.UploadFile(DataContainer.Instance.FilePath_RegistrationInfo_External, true);
            }
        }

        private void txtBx_filter_main_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter_Main();
        }

        private void dgv_main_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = (DataGridView)sender;

            int columnIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;
            var column = dgv.Columns[columnIndex];
            if (column is DataGridViewButtonColumn && rowIndex >= 0) // If it is a button column and it is not the header cell
            {
                var cells = dgv.Rows[rowIndex].Cells;

                int id = Convert.ToInt32((string)(cells["Id"].Value));

                DataContainer container = DataContainer.Instance;
                var registrationInfo = container.RegistrationInfos_External.First(x => x.Id == id);

                if (column.DataPropertyName == "Identification1")
                {
                    string documentURL = registrationInfo.DocumentUrl1;
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = documentURL,
                        UseShellExecute = true
                    });
                }

                if (column.DataPropertyName == "Identification2")
                {
                    string documentURL = registrationInfo.DocumentUrl2;
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = documentURL,
                        UseShellExecute = true
                    });
                }

                if (column.DataPropertyName == "PaymentEvidence")
                {
                    string documentURL = registrationInfo.DocumentUrl3;
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = documentURL,
                        UseShellExecute = true
                    });
                }
            }
        }
        #endregion 

        #region Component Initialization
        private void InitializeMainViewTable()
        {
            // Set columns
            if (dgv_main.ColumnCount == 0)
            {
                var columns = dgv_main.Columns;
                columns.Add("Id", "Id");
                columns.Add("Start", "Start");
                columns.Add("Completion", "Completion");
                columns.Add("Exam", "Exam");
                columns.Add("ExamDate", "Exam Date");
                columns.Add("AccountNumber", "Account Number");
                columns.Add("Name", "Name");
                columns.Add("OrganizationEmail", "Organization Email");
                columns.Add("PreferredEmail", "Preferred Email");

                Font font = new Font("Palatino Linotype", 12F, GraphicsUnit.Pixel);

                var identification1Column = new DataGridViewButtonColumn();
                identification1Column.DataPropertyName = "Identification1";
                identification1Column.Name = "Identification 1";
                identification1Column.CellTemplate.Style.Font = font;
                columns.Add(identification1Column);

                var identification2Column = new DataGridViewButtonColumn();
                identification2Column.DataPropertyName = "Identification2";
                identification2Column.Name = "Identification 2";
                identification2Column.CellTemplate.Style.Font = font;
                columns.Add(identification2Column);

                var paymentEvidenceColumn = new DataGridViewButtonColumn();
                paymentEvidenceColumn.DataPropertyName = "PaymentEvidence";
                paymentEvidenceColumn.Name = "Payment Evidence";
                paymentEvidenceColumn.CellTemplate.Style.Font = font;
                columns.Add(paymentEvidenceColumn);
            }
        }
        #endregion

        #region Component Refreshment
        private void RefreshMainViewTable()
        {
            // Set rows
            ApplyFilter_Main();
        }
        #endregion

        #region Data Filtration
        private void ApplyFilter_Main()
        {
            string filterText = txtBx_filter_main.Text;

            DataContainer container = DataContainer.Instance;

            var regstrationInfos = container.RegistrationInfos_External;
            var targetDictionary = string.IsNullOrWhiteSpace(filterText) ? regstrationInfos : regstrationInfos.Where(x => "TOEFL".Contains(filterText)
                                                                                                                    || x.ExamDate.ToString().Contains(filterText)
                                                                                                                    || x.AccountNumber.ToString().Contains(filterText)
                                                                                                                    || (x.FirstName + " " + x.MiddleName + " " + x.PaternalSurname + " " + x.MaternalSurname).Contains(filterText)
                                                                                                                    || x.OrganizationEmail.Contains(filterText)
                                                                                                                    || x.PreferredEmail.Contains(filterText));
            int selectingRowIndex = -1;
            var rows = dgv_main.Rows;
            {
                rows.Clear();
                foreach (var registrationInfo in targetDictionary)
                {

                    string name = registrationInfo.FirstName + " " + registrationInfo.MiddleName + " " + registrationInfo.PaternalSurname + " " + registrationInfo.MaternalSurname;

                    var row = new string[] { registrationInfo.Id.ToString(), registrationInfo.StartTime.ToString(), registrationInfo.CompletionTime.ToString(), "TOEFL", registrationInfo.ExamDate.Date.ToString(), registrationInfo.AccountNumber,
                                                name, registrationInfo.OrganizationEmail, registrationInfo.PreferredEmail, "View", "View", "View" };
                    rows.Add(row);
                }
            }

            if (selectingRowIndex != -1)
                rows[selectingRowIndex].Selected = true;
            else if (rows.Count != 0)
                rows[rows.GetLastRow(DataGridViewElementStates.None)].Selected = true;
        }
        #endregion

        #region Shared

        private class RowComparer : System.Collections.IComparer
        {
            private static int m_sortOrderModifier = 1;

            public RowComparer(SortOrder _sortOrder)
            {
                if (_sortOrder == SortOrder.Descending)
                    m_sortOrderModifier = -1;
                else if (_sortOrder == SortOrder.Ascending)
                    m_sortOrderModifier = 1;
            }

            public int Compare(object x, object y)
            {
                DataGridViewRow dataGridViewRow1 = (DataGridViewRow)x;
                DataGridViewRow dataGridViewRow2 = (DataGridViewRow)y;

                // Try to sort based on the Value column.
                int compareResult = Convert.ToInt32(dataGridViewRow1.Cells[0].Value.ToString())
                                            .CompareTo(Convert.ToInt32(dataGridViewRow2.Cells[0].Value.ToString()));

                return compareResult * m_sortOrderModifier;
            }
        }
        #endregion

        private void btn_return_Click(object sender, EventArgs e)
        {
            this.SwitchToPrevious();
        }
    }
}