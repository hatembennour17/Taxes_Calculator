using System;
using System.Collections.Generic;
using System.Text;

namespace OrderProcessor
{
   public class TaxCalculator: ITaxCalculator
    {
        public double CalculateSalesTax(double price)
        {
            double result = (price * 10) / 100;
            return Math.Ceiling(result / 0.05) * 0.05;
        }
    }
}
