using SumAllocation.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SumAllocation
{
    public static class FinancialOperationsHelper
    {
        /// <summary>
        /// Распределяет указанную сумму пропорционально запрошенным
        /// </summary>
        private static double[] ProportionalSumAllocation(double sum, double[] requiredSums)
        {
            //переменная для распределенных сумм
            double[] allocatedSums = new double[requiredSums.Count()];

            //цикл по запрошенным суммам
            for (int i = 0; i < requiredSums.Count(); i++)
            {
                //выделяем сумму пропорционально запрошенной
                var sumToAlloc = ((double)requiredSums[i] / requiredSums.Sum()) * sum;

                allocatedSums[i] = (Math.Round(sumToAlloc, 2)); //округление до 2 знаков после точки
            }

            double spentOnAllocSum = allocatedSums.Sum();

            if (spentOnAllocSum != sum) //если остался остаток от округления
            {
                var lastSum = allocatedSums[^1] + (sum - allocatedSums.Sum()); //добавим остаток к последней сумме
                allocatedSums[^1] = Math.Round(lastSum, 2); //округляем до 2 знаков

            }

            return allocatedSums;
        }

        /// <summary>
        /// Распределяет указанную сумму в счет первых
        /// </summary>
        private static double[] FirstPrioritySumAllocation(double sum, double[] requiredSums)
        {
            //переменная для распределенных сумм
            double[] allocatedSums = new double[requiredSums.Count()];

            double sumLeft = sum; //отстаток суммы

            //цикл по запрошенным суммам
            for (int i = 0; i < requiredSums.Count(); i++)
            {
                if (sumLeft >= requiredSums[i]) //если остаток суммы на счету больше необходимой - выделяем полностью
                {
                    sumLeft -= requiredSums[i]; //вычитаем выделенную из остатка

                    allocatedSums[i] = requiredSums[i];
                }
                else //если остаток меньше необходимой - выделяем остаток
                {
                    allocatedSums[i] = sumLeft;

                    if (sumLeft != 0)
                        sumLeft = 0;
                }
            }

            return allocatedSums;
        }

        /// <summary>
        /// Распределяет указанную сумму в счет последних
        /// </summary>
        private static double[] LastPrioritySumAllocation(double sum, double[] requiredSums)
        {
            //переменная для распределенных сумм
            double[] allocatedSums = new double[requiredSums.Count()];

            double sumLeft = sum; //отстаток суммы

            //цикл по запрошенным суммам
            for (int i = requiredSums.Count() - 1; i >= 0; i--)
            {
                if (sumLeft >= requiredSums[i]) //если остаток суммы на счету больше необходимой - выделяем полностью
                {
                    sumLeft -= requiredSums[i]; //вычитаем выделенную из остатка

                    allocatedSums[i] = requiredSums[i];
                }
                else //если остаток меньше необходимой - выделяем остаток
                {
                    allocatedSums[i] = sumLeft;

                    if (sumLeft != 0)
                        sumLeft = 0;
                }
            }

            return allocatedSums;
        }

        
        /// <summary>
        /// Распределяет указанную сумму с учетом типа распределения 
        /// </summary>
        public static string SumAllocation(string allocType, double sum, string sumString)
        {
            //формируем со строки массив чисел с плавающей точкой 
            double[] requiredSummsArray = sumString?.Split(';', StringSplitOptions.RemoveEmptyEntries)?
                                                    .Select(double.Parse)?
                                                    .ToArray();

            //выполняем распределение в зависимости от типа
            //с массива полученных сумм формируем строку через символ ';'
            return allocType switch
            {
                AllocationTypes.Proportional => string.Join(";", ProportionalSumAllocation(sum, requiredSummsArray)),
                AllocationTypes.FirstPriority => string.Join(";", FirstPrioritySumAllocation(sum, requiredSummsArray)),
                AllocationTypes.LastPriority => string.Join(";", LastPrioritySumAllocation(sum, requiredSummsArray)),
                _ => throw new Exception(Errors.InvalidAllocationType)
            };
        }
    }
}
