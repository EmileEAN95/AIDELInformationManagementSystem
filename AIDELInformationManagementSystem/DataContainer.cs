﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AIDELInformationManagementSystem
{
    public sealed class DataContainer
    {
        public static DataContainer Instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = new DataContainer();

                return m_instance;
            }
        }

        private DataContainer()
        {
            FileNames = new List<string>()
            {
                "BaseCriterion.csv",
                "Color.csv",
                "Course.csv",
                "CourseBase.csv",
                "CourseGroup.csv",
                "CourseGroup_StudentInfo.csv",
                "EvaluationColorSet.csv",
                "EvaluationColorSet_ValueColor.csv",
                "ExamType.csv",
                "Faculty.csv",
                "FullEvaluation.csv",
                "FullEvaluationCriteria.csv",
                "FullName.csv",
                "Major.csv",
                "Name.csv",
                "NonInstitutionalExam.csv",
                "PermissionsType.csv",
                "QualitativeEvaluation.csv",
                "QualitativeEvaluationCriteria.csv",
                "QualitativeEvaluationCriteriaSet.csv",
                "QualitativeEvaluationCriteria_Criterion.csv",
                "QualitativeEvaluationCriterion.csv",
                "QualitativeEvaluationSet.csv",
                "QualitativeEvaluationSetCollection.csv",
                "QualitativeEvaluationSet_Evaluation.csv",
                "QuantitativeEvaluation.csv",
                "QuantitativeEvaluationCriteria.csv",
                "QuantitativeEvaluationCriteriaSet.csv",
                "QuantitativeEvaluationCriteriaWeight.csv",
                "QuantitativeEvaluationCriteria_CriterionWeight.csv",
                "QuantitativeEvaluationCriterion.csv",
                "QuantitativeEvaluationCriterionWeight.csv",
                "QuantitativeEvaluationSet.csv",
                "QuantitativeEvaluationSetCollection.csv",
                "QuantitativeEvaluationSet_Evaluation.csv",
                "Semester.csv",
                "Student.csv",
                "StudentInfo.csv",
                "Surname.csv",
                "Term.csv",
                "ValueColor.csv",
                "ValueType.csv",
                "Year.csv"
            };

            FilePath_SystemData = CoreValues.ExecutableDirectoryPath + "SystemData.csv";

            SetFilePaths();

            SystemDataDictionary = new Dictionary<string, string>();

            PreviousFormLog = new List<Form>();

            RegistrationInfos_External = new List<RegistrationInfo_External>();

            NameDictionary = new Dictionary<int, string>();
            SurnameDictionary = new Dictionary<int, string>();
            FullNameDictionary = new Dictionary<int, FullName>();

            MajorList = new List<Major>();

            YearDictionary = new Dictionary<int, int>();
            TermDictionary = new Dictionary<int, eTerm>();
            SemesterDictionary = new Dictionary<int, Semester>();

            StudentList = new List<Student>();

            PermissionsTypeDictionary = new Dictionary<int, ePermissionsType>();
            FacultyList = new List<Faculty>();

            BaseCriterionDictionary = new Dictionary<int, string>();

            QuantitativeEvaluationCriterionDictionary = new Dictionary<int, Criterion_QuantitativeEvaluation>();
            QuantitativeEvaluationCriterionWeightDictionary = new Dictionary<int, CriterionWeight_QuantitativeEvaluation>();
            QuantitativeEvaluationCriteria_CriterionWeightMappingList = new List<Tuple<int, int>>();
            QuantitativeEvaluationCriteriaDictionary = new Dictionary<int, Criteria_QuantitativeEvaluation>();
            QuantitativeEvaluationCriteriaWeightDictionary = new Dictionary<int, CriteriaWeight_QuantitativeEvaluation>();
            QuantitativeEvaluationCriteriaSetDictionary = new Dictionary<int, CriteriaSet_QuantitativeEvaluation>();

            ColorDictionary = new Dictionary<int, Color>();
            ValueColorDictionary = new Dictionary<int, ValueColor>();
            EvaluationColorSet_ValueColorMappingList = new List<Tuple<int, int>>();
            EvaluationColorSetDictionary = new Dictionary<int, EvaluationColorSet>();

            ValueTypeDictionary = new Dictionary<int, eValueType>();
            QualitativeEvaluationCriterionDictionary = new Dictionary<int, Criterion_QualitativeEvaluation>();
            QualitativeEvaluationCriteria_CriterionMappingList = new List<Tuple<int, int>>();
            QualitativeEvaluationCriteriaDictionary = new Dictionary<int, Criteria_QualitativeEvaluation>();
            QualitativeEvaluationCriteriaSetDictionary = new Dictionary<int, CriteriaSet_QualitativeEvaluation>();

            FullEvaluationCriteriaDictionary = new Dictionary<int, FullEvaluationCriteria>();

            QuantitativeEvaluationDictionary = new Dictionary<int, QuantitativeEvaluation>();
            QuantitativeEvaluationSet_EvaluationMappingList = new List<Tuple<int, int>>();
            QuantitativeEvaluationSetDictionary = new Dictionary<int, List<QuantitativeEvaluation>>();
            QuantitativeEvaluationSetCollectionDictionary = new Dictionary<int, EvaluationSetCollection_Quantitative>();

            QualitativeEvaluationDictionary = new Dictionary<int, QualitativeEvaluation>();
            QualitativeEvaluationSet_EvaluationMappingList = new List<Tuple<int, int>>();
            QualitativeEvaluationSetDictionary = new Dictionary<int, List<QualitativeEvaluation>>();
            QualitativeEvaluationSetCollectionDictionary = new Dictionary<int, EvaluationSetCollection_Qualitative>();

            FullEvaluationDictionary = new Dictionary<int, FullEvaluation>();

            StudentInfoDictionary = new Dictionary<int, StudentInfo>();
            CourseBaseList = new List<CourseBase>();
            CourseDictionary = new Dictionary<int, Course>();
            CourseGroup_StudentInfoMappingList = new List<Tuple<int, int>>();
            CourseGroupDictionary = new Dictionary<int, CourseGroup>();

            ExamTypeDictionary = new Dictionary<int, eExamType>();
            NonInstitutionalExamDictionary = new Dictionary<int, NonInstitutionalExam>();
        }

        private static DataContainer m_instance = null;

        public string FilePath_RegistrationInfo_External { get; } = CoreValues.ExecutableDirectoryPath + "RegistrationInfo.csv";
        public List<RegistrationInfo_External> RegistrationInfos_External { get; }

        #region CSV File Name
        public List<string> FileNames { get; }
        #endregion

        #region CSV File Path
        public string FilePath_SystemData { get; }

        public string FilePath_Name { get; set; }
        public string FilePath_Surname { get; set; }
        public string FilePath_FullName { get; set; }
        public string FilePath_Major { get; set; }
        public string FilePath_Student { get; set; }

        public string FilePath_PermissionsType { get; set; }
        public string FilePath_Faculty { get; set; }

        public string FilePath_BaseCriterion { get; set; }

        public string FilePath_QuantitativeEvaluationCriterion { get; set; }
        public string FilePath_QuantitativeEvaluationCriterionWeight { get; set; }
        public string FilePath_QuantitativeEvaluationCriteria_CriterionWeight { get; set; }
        public string FilePath_QuantitativeEvaluationCriteria { get; set; }
        public string FilePath_QuantitativeEvaluationCriteriaWeight { get; set; }
        public string FilePath_QuantitativeEvaluationCriteriaSet { get; set; }

        public string FilePath_Color { get; set; }
        public string FilePath_ValueColor { get; set; }
        public string FilePath_EvaluationColorSet_ValueColor { get; set; }
        public string FilePath_EvaluationColorSet { get; set; }

        public string FilePath_ValueType { get; set; }
        public string FilePath_QualitativeEvaluationCriterion { get; set; }
        public string FilePath_QualitativeEvaluationCriteria_Criterion { get; set; }
        public string FilePath_QualitativeEvaluationCriteria { get; set; }
        public string FilePath_QualitativeEvaluationCriteriaSet { get; set; }

        public string FilePath_FullEvaluationCriteria { get; set; }

        public string FilePath_QuantitativeEvaluation { get; set; }
        public string FilePath_QuantitativeEvaluationSet_Evaluation { get; set; }
        public string FilePath_QuantitativeEvaluationSet { get; set; }
        public string FilePath_QuantitativeEvaluationSetCollection { get; set; }

        public string FilePath_QualitativeEvaluation { get; set; }
        public string FilePath_QualitativeEvaluationSet_Evaluation { get; set; }
        public string FilePath_QualitativeEvaluationSet { get; set; }
        public string FilePath_QualitativeEvaluationSetCollection { get; set; }

        public string FilePath_FullEvaluation { get; set; }

        public string FilePath_StudentInfo { get; set; }
        public string FilePath_Year { get; set; }
        public string FilePath_Term { get; set; }
        public string FilePath_Semester { get; set; }
        public string FilePath_CourseBase { get; set; }
        public string FilePath_Course { get; set; }
        public string FilePath_CourseGroup_StudentInfo { get; set; }
        public string FilePath_CourseGroup { get; set; }

        public string FilePath_ExamType { get; set; }
        public string FilePath_NonInstitutionalExam { get; set; }
        #endregion

        #region System Data
        public Dictionary<string, string> SystemDataDictionary { get; }
        public void SetFileModificationStatus(string _filePath, bool _modified)
        {
            string fileName = Path.GetFileName(_filePath);
            int fileIndex = FileNames.IndexOf(fileName);
            SetFileModificationStatus(fileIndex, _modified);
        }
        public void SetFileModificationStatus(int _fileIndex, bool _modified)
        {
            try
            {
                SystemDataDictionary["CSVModified_" + (_fileIndex + 1).ToString()] = _modified.ToString();
                DocumentLoader.AddSystemDataToCSV();
            }
            catch (Exception)
            {
                MessageBox.Show("No CSV file matches the file name!");
            }
        }
        public void ResetAllFileModificationStatus()
        {
            for (int i = 0; i < FileNames.Count; i++)
            {
                SetFileModificationStatus(i, false);
            }
        }

        public int NumOfModifiedFiles()
        {
            int count = 0;

            string trueString = true.ToString();
            for (int i = 1; i <= FileNames.Count; i++)
            {
                if (SystemDataDictionary["CSVModified_" + i.ToString()] == trueString)
                    count++;
            }

            return count;
        }

        public Faculty CurrentUser { get; set; }
        #endregion

        #region Transition Data
        public List<Form> PreviousFormLog { get; set; }
        public CourseGroup SelectedGroup { get; set; }
        public Student SelectedStudent { get; set; }
        public Faculty SelectedFaculty { get; set; }
        #endregion

        #region Data
        public Dictionary<int, string> NameDictionary { get; }
        public Dictionary<int, string> SurnameDictionary { get; }
        public Dictionary<int, FullName> FullNameDictionary { get; }

        public List<Major> MajorList { get; }

        public Dictionary<int, int> YearDictionary { get; }
        public Dictionary<int, eTerm> TermDictionary { get; }
        public Dictionary<int, Semester> SemesterDictionary { get; }

        public List<Student> StudentList { get; }

        public Dictionary<int, ePermissionsType> PermissionsTypeDictionary { get; }
        public List<Faculty> FacultyList { get; }

        public Dictionary<int, string> BaseCriterionDictionary { get; }

        #region Evaluation Criteria
        #region Quantitative Evaluation
        public Dictionary<int, eValueType> ValueTypeDictionary { get; }
        public Dictionary<int, Criterion_QuantitativeEvaluation> QuantitativeEvaluationCriterionDictionary { get; }
        public Dictionary<int, CriterionWeight_QuantitativeEvaluation> QuantitativeEvaluationCriterionWeightDictionary { get; }
        public List<Tuple<int, int>> QuantitativeEvaluationCriteria_CriterionWeightMappingList { get; }
        public Dictionary<int, Criteria_QuantitativeEvaluation> QuantitativeEvaluationCriteriaDictionary { get; }
        public Dictionary<int, CriteriaWeight_QuantitativeEvaluation> QuantitativeEvaluationCriteriaWeightDictionary { get; }
        public Dictionary<int, CriteriaSet_QuantitativeEvaluation> QuantitativeEvaluationCriteriaSetDictionary { get; }
        #endregion
        #region Qualitative Evaluation
        public Dictionary<int, Color> ColorDictionary { get; }
        public Dictionary<int, ValueColor> ValueColorDictionary { get; }
        public List<Tuple<int, int>> EvaluationColorSet_ValueColorMappingList { get; }
        public Dictionary<int, EvaluationColorSet> EvaluationColorSetDictionary { get; }

        public Dictionary<int, Criterion_QualitativeEvaluation> QualitativeEvaluationCriterionDictionary { get; }
        public List<Tuple<int, int>> QualitativeEvaluationCriteria_CriterionMappingList { get; }
        public Dictionary<int, Criteria_QualitativeEvaluation> QualitativeEvaluationCriteriaDictionary { get; }
        public Dictionary<int, CriteriaSet_QualitativeEvaluation> QualitativeEvaluationCriteriaSetDictionary { get; }
        #endregion
        public Dictionary<int, FullEvaluationCriteria> FullEvaluationCriteriaDictionary { get; }
        #endregion

        #region Evaluation
        #region Quantitative Evaluation
        public Dictionary<int, QuantitativeEvaluation> QuantitativeEvaluationDictionary { get; }
        public List<Tuple<int, int>> QuantitativeEvaluationSet_EvaluationMappingList { get; }
        public Dictionary<int, List<QuantitativeEvaluation>> QuantitativeEvaluationSetDictionary { get; }
        public Dictionary<int, EvaluationSetCollection_Quantitative> QuantitativeEvaluationSetCollectionDictionary { get; }
        #endregion
        #region Qualitative Evaluation
        public Dictionary<int, QualitativeEvaluation> QualitativeEvaluationDictionary { get; }
        public List<Tuple<int, int>> QualitativeEvaluationSet_EvaluationMappingList { get; }
        public Dictionary<int, List<QualitativeEvaluation>> QualitativeEvaluationSetDictionary { get; }
        public Dictionary<int, EvaluationSetCollection_Qualitative> QualitativeEvaluationSetCollectionDictionary { get; }
        #endregion
        public Dictionary<int, FullEvaluation> FullEvaluationDictionary { get; }
        #endregion

        public Dictionary<int, StudentInfo> StudentInfoDictionary { get; }
        public List<CourseBase> CourseBaseList { get; }
        public Dictionary<int, Course> CourseDictionary { get; }
        public List<Tuple<int, int>> CourseGroup_StudentInfoMappingList { get; }
        public Dictionary<int, CourseGroup> CourseGroupDictionary { get; }

        public Dictionary<int, eExamType> ExamTypeDictionary { get; }
        public Dictionary<int, NonInstitutionalExam> NonInstitutionalExamDictionary { get; }
        #endregion

        private void SetFilePaths()
        {
            List<string> filePaths = new List<string>();
            string executableDirectoryPath = CoreValues.ExecutableDirectoryPath;
            foreach (var fileName in FileNames)
            {
                filePaths.Add(executableDirectoryPath + fileName);
            }

            FilePath_BaseCriterion = filePaths[0];
            FilePath_Color = filePaths[1];
            FilePath_Course = filePaths[2];
            FilePath_CourseBase = filePaths[3];
            FilePath_CourseGroup = filePaths[4];
            FilePath_CourseGroup_StudentInfo = filePaths[5];
            FilePath_EvaluationColorSet = filePaths[6];
            FilePath_EvaluationColorSet_ValueColor = filePaths[7];
            FilePath_ExamType = filePaths[8];
            FilePath_Faculty = filePaths[9];
            FilePath_FullEvaluation = filePaths[10];
            FilePath_FullEvaluationCriteria = filePaths[11];
            FilePath_FullName = filePaths[12];
            FilePath_Major = filePaths[13];
            FilePath_Name = filePaths[14];
            FilePath_NonInstitutionalExam = filePaths[15];
            FilePath_PermissionsType = filePaths[16];
            FilePath_QualitativeEvaluation = filePaths[17];
            FilePath_QualitativeEvaluationCriteria = filePaths[18];
            FilePath_QualitativeEvaluationCriteriaSet = filePaths[19];
            FilePath_QualitativeEvaluationCriteria_Criterion = filePaths[20];
            FilePath_QualitativeEvaluationCriterion = filePaths[21];
            FilePath_QualitativeEvaluationSet = filePaths[22];
            FilePath_QualitativeEvaluationSetCollection = filePaths[23];
            FilePath_QualitativeEvaluationSet_Evaluation = filePaths[24];
            FilePath_QuantitativeEvaluation = filePaths[25];
            FilePath_QuantitativeEvaluationCriteria = filePaths[26];
            FilePath_QuantitativeEvaluationCriteriaSet = filePaths[27];
            FilePath_QuantitativeEvaluationCriteriaWeight = filePaths[28];
            FilePath_QuantitativeEvaluationCriteria_CriterionWeight = filePaths[29];
            FilePath_QuantitativeEvaluationCriterion = filePaths[30];
            FilePath_QuantitativeEvaluationCriterionWeight = filePaths[31];
            FilePath_QuantitativeEvaluationSet = filePaths[32];
            FilePath_QuantitativeEvaluationSetCollection = filePaths[33];
            FilePath_QuantitativeEvaluationSet_Evaluation = filePaths[34];
            FilePath_Semester = filePaths[35];
            FilePath_Student = filePaths[36];
            FilePath_StudentInfo = filePaths[37];
            FilePath_Surname = filePaths[38];
            FilePath_Term = filePaths[39];
            FilePath_ValueColor = filePaths[40];
            FilePath_ValueType = filePaths[41];
            FilePath_Year = filePaths[42];
        }
    }
}
