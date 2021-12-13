using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDELInformationManagementSystem
{
    public class RegistrationInfo_External
    {
        public RegistrationInfo_External(int _id, DateTime _startTime, DateTime _completionTime, string _organizationEmail, eExamType _type, DateTime _examDate, string _accountNumber, string _firstName, string _middleName, string _paternalSurname, string _maternalSurname, string _preferredEmail, string _documentUrl1, string _documentUrl2, string _documentUrl3)
        {
            Id = _id;
            StartTime = _startTime;
            CompletionTime = _completionTime;
            OrganizationEmail = _organizationEmail;
            Type = _type;
            ExamDate = _examDate;
            AccountNumber = _accountNumber;
            FirstName = _firstName;
            MiddleName = _middleName;
            PaternalSurname = _paternalSurname;
            MaternalSurname = _maternalSurname;
            PreferredEmail = _preferredEmail;
            DocumentUrl1 = _documentUrl1;
            DocumentUrl2 = _documentUrl2;
            DocumentUrl3 = _documentUrl3;
        }
        
        public int Id { get; }
        public DateTime StartTime { get; }
        public DateTime CompletionTime { get; }
        public string OrganizationEmail { get; }
        public eExamType Type { get; }
        public DateTime ExamDate { get; }
        public string AccountNumber { get; }
        public string FirstName { get; }
        public string MiddleName { get; }
        public string PaternalSurname { get; }
        public string MaternalSurname { get; }
        public string PreferredEmail { get; }
        public string DocumentUrl1 { get; }
        public string DocumentUrl2 { get; }
        public string DocumentUrl3 { get; }
    }
}
