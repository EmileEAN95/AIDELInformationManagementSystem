using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIDELInformationManagementSystem
{
    public partial class CourseStudentsForm: Form
    {
        public CourseStudentsForm()
        {
            InitializeComponent();
        }

        private void CourseStudentsForm_Load(object sender, EventArgs e)
        {
            m_group = DataContainer.Instance.SelectedGroup;
            var course = m_group.Course;
            var semester = course.Semester;
            var courseBase = course.Base;
            lbl_course.Text = courseBase.ToString() + " " + semester.ToString() + " Group " + m_group.Name;

            InitializeMainViewTable();

            RefreshMainViewTable();
            dgv_main.ClearSelection();
            dgv_main.SelectionChanged += (_sender, _e) => dgv_main.ClearSelection();
        }

        private CourseGroup m_group;

        #region Component Events
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
                if (column.DataPropertyName == "QuantitativeEvaluation")
                {
                    var cells = dgv.Rows[rowIndex].Cells;

                    int accountNumber = Convert.ToInt32((string)(cells["AccountNumber"].Value));
                    DataContainer container = DataContainer.Instance;
                    container.SelectedStudent = container.StudentList.First(x => x.AccountNumber == accountNumber);

                    this.LogAndSwitchTo(new QuantitativeEvaluationForm());
                }

                if (column.DataPropertyName == "QualitativeEvaluation")
                {
                    var cells = dgv.Rows[rowIndex].Cells;

                    int accountNumber = Convert.ToInt32((string)(cells["AccountNumber"].Value));
                    DataContainer container = DataContainer.Instance;
                    container.SelectedStudent = container.StudentList.First(x => x.AccountNumber == accountNumber);

                    this.LogAndSwitchTo(new QualitativeEvaluationForm());
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
                columns.Add("AccountNumber", "Account Number");
                columns.Add("FirstName", "First Name");
                columns.Add("MiddleName", "Middle Name");
                columns.Add("PaternalSurname", "Paternal Surname");
                columns.Add("MaternalSurname", "Maternal Surname");

                Font font = new Font("Palatino Linotype", 12F, GraphicsUnit.Pixel);

                var quantitativeEvaluationColumn = new DataGridViewButtonColumn();
                quantitativeEvaluationColumn.DataPropertyName = "QuantitativeEvaluation";
                quantitativeEvaluationColumn.Name = "Quantitative Evaluation";
                quantitativeEvaluationColumn.CellTemplate.Style.Font = font;
                columns.Add(quantitativeEvaluationColumn);

                var qualitativeEvaluationColumn = new DataGridViewButtonColumn();
                qualitativeEvaluationColumn.DataPropertyName = "QualitativeEvaluation";
                qualitativeEvaluationColumn.Name = "Qualitative Evaluation";
                qualitativeEvaluationColumn.CellTemplate.Style.Font = font;
                columns.Add(qualitativeEvaluationColumn);
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

            var studentInfos = m_group.StudentInfos;
            var targetDictionary = string.IsNullOrWhiteSpace(filterText) ? studentInfos : studentInfos.Where(x => x.Student.AccountNumber.ToString().Contains(filterText)
                                                                                                                || x.Student.Name.ToString().Contains(filterText));
            int selectingRowIndex = -1;
            var rows = dgv_main.Rows;
            {
                rows.Clear();
                foreach (var element in targetDictionary)
                {
                    var group = container.SelectedGroup;
                    var student = element.Student;
                    var fullEvaluation = element.FullEvaluation;
                    var studentName = student.Name;
                    var quantitativeAverage = EvaluationCalculator.QuantitativeEvaluationPointAverage(group, student);
                    var qualitativeAverage = EvaluationCalculator.QualitativeEvaluationPointAverage(group, student);
                    var row = new string[] { student.AccountNumber.ToString(), studentName.FirstName, studentName.MiddleName, studentName.PaternalSurname, studentName.MaternalSurname, quantitativeAverage.ToString(), qualitativeAverage.ToString() };
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
