using System;
using System.Collections.Generic;
using System.Text;

namespace SumAllocation.Constants
{
    public static class AllocationTypes
    {
        public const string Proportional = "ПРОП";
        public const string FirstPriority = "ПЕРВ";
        public const string LastPriority = "ПОСЛ";
    }     

    public static class Errors
    {
        public const string InvalidAllocationType = "Недопустимый тип распределения";
    }
}
