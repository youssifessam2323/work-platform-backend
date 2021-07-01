using System;

namespace work_platform_backend.Exceptions
{
    public class DateTimeException : Exception
    {
        public DateTimeException(){}

        public DateTimeException(string date1,string date2)
        :base(string.Format("PlannedStartDate {0} is later than PlannedEndDate {1}",date1,date2))
        {

        }
    }
}