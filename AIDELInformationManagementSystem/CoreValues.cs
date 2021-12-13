using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIDELInformationManagementSystem
{
    public static class CoreValues
    {
        public static string ExecutableDirectoryPath { get; } = Path.GetDirectoryName(Application.ExecutablePath) + @"\";

        public static decimal MaxQuantitativeEvaluationValue { get; } = 10;

        public static int AccountNumberLength { get; } = 6;
        public static string AccountNumberStringFormat { get; } = "D" + AccountNumberLength.ToString();
        public static int CourseIdLength { get; } = 3;
        public static string CourseIdStringFormat { get; } = "D" + CourseIdLength.ToString();
        public static int PhoneNumberLength { get; } = 10;

        public static int PercentageDecimalPlaces { get; } = 2;
        public static string PercentageStringFormat { get; } = "P" + PercentageDecimalPlaces.ToString();

        public static string ComboBox_DefaultString = "--Select an item--";

        public static string ExternalExamRegistrationInfo_Password = "???";
    }
}
