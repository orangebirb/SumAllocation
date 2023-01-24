using SumAllocation.Constants;
using System;

namespace SumAllocation
{
    class Program
    {
        static void Main(string[] args)
        {
            var initialSum = 10000;
            var requiredSums = "1000;2000;3000;5000;8000;5000";

            var allocationResult = FinancialOperationsHelper.SumAllocation(AllocationTypes.Proportional, initialSum, requiredSums);
            
            Console.WriteLine(allocationResult);
        }
    }
}
