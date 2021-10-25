using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Modules
    {
        // Method calculation for the self study hours
        public static double selfStudyHours(int NumOfCredits, int SemesterWeeks, int HoursPerWeek)
        {
            double result = 0;
            result = ((NumOfCredits * 10) / SemesterWeeks) - HoursPerWeek;
            return result;
        }
    }
}
