using System;

namespace GSTCalculator.Model
{
    internal class GSTCalculator
    {
        public GSTCalculator()
        {
        }

        public double CalculateGST(double? total)
        {
            return (double)total / 11;
        }

        public double CalculateTotalExcludeingGST(double? total)
        {
            return (double)(total - (total / 11));
        }
    }
}