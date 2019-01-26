using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class StudentComparer : IComparer<Student>
{
    public int Compare(Student x, Student y)
    {
        if (x == null)
        {
            if (y == null)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
        else
        {
            if (y == null)
            {
                return 1;
            }
            else
            {
                int retval = x.FirstName.CompareTo(y.FirstName);

                if (retval != 0)
                {
                    return retval;
                }
                else
                {
                    return int.Parse(x.ID).CompareTo(int.Parse(y.ID));
                }
            }
        }
    }
}