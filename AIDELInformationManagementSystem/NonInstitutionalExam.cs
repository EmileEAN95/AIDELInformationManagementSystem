using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDELInformationManagementSystem
{
    public class NonInstitutionalExam
    {
        public NonInstitutionalExam(DateTime _date, eExamType _type, Student _examinee, int _score)
        {
            Date = _date;
            Type = _type;
            Examinee = _examinee;
            Score = _score;
        }
        
        public DateTime Date { get; }
        public eExamType Type { get; }
        public Student Examinee { get; }
        public int Score { get; }
    }

    public enum eExamType
    {
        TOEFL,
        TOEIC,
        Diagnostic
    }
}
