using System;
using System.Collections.Generic;
using System.Text;

namespace OrderProcessor
{
   public class ImportFeeCalculator: IImportFeeCalculator
    {
        public double CalculateImportFee(double rowPrice)
        {
            double result = (rowPrice * 5) / 100;
            return Math.Ceiling(result / 0.05) * 0.05;
        }
    }
}
