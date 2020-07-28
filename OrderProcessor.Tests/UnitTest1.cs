using System;
using Xunit;

namespace OrderProcessor.Tests
{
    public class OrderProcessorTests
    {
        [Fact]
        public void Process_Order_Output1()
        {
            var orderprocessor = new OrderProcessor(new FakeTaxClculator(), new FakeImportFeesCalculator());
            var order1 = new Order
            {
                quantity = 2,
                orderName = "Book",
                rowPrice = Convert.ToDouble(12.49),
                imported = false,
                taxable = false
            };
            var order2 = new Order
            {
                quantity = 1,
                orderName = "Music CD",
                rowPrice = Convert.ToDouble(14.99),
                imported = false,
                taxable = true
            };
            var order3 = new Order
            {
                quantity = 1,
                orderName = "Chocolate bar",
                rowPrice = Convert.ToDouble(0.85),
                imported = false,
                taxable = false
            };
            //testing 2 Books @ 24.98 
            Order processedOrder1 = orderprocessor.ProcessOrder(order1);
            Assert.Equal(processedOrder1.finalPrice, Convert.ToDouble(24.98).ToString("0.00"));
            //testing Music CD: 16.49
            Order processedOrder2 = orderprocessor.ProcessOrder(order2);
            Assert.Equal(processedOrder2.finalPrice, Convert.ToDouble(16.49).ToString());
            //testing Chocolate bar at 0.85
            Order processedOrder3 = orderprocessor.ProcessOrder(order3);
            Assert.Equal(processedOrder3.finalPrice, Convert.ToDouble(0.85).ToString());
            //Sales Taxes: 1.50
              Assert.Equal(Convert.ToDouble(processedOrder2.taxes ).ToString("0.00"), Convert.ToDouble(1.50).ToString("0.00"));
            //Total: 42.32
             Assert.Equal((Convert.ToDouble(processedOrder1.finalPrice) + Convert.ToDouble(processedOrder2.finalPrice) + Convert.ToDouble(processedOrder3.finalPrice)).ToString("0.00"), Convert.ToDouble(42.32).ToString("0.00"));
        }
        [Fact]
        public void Process_Order_Output2()
        {
            var orderprocessor = new OrderProcessor(new FakeTaxClculator(), new FakeImportFeesCalculator());
            var order1 = new Order
            {
                quantity = 1,
                orderName = "Imported box of chocolates",
                rowPrice = Convert.ToDouble(10.00),
                imported = true,
                taxable = false
            };
            var order2 = new Order
            {
                quantity = 1,
                orderName = "Imported bottle of perfume",
                rowPrice = Convert.ToDouble(47.50),
                imported = true,
                taxable = true
            };
            //testing Imported box of chocolates: 10.50
            Order processedOrder1 = orderprocessor.ProcessOrder(order1);
            Assert.Equal(processedOrder1.finalPrice, Convert.ToDouble(10.50).ToString("0.00"));
            //testing Imported bottle of perfume: 54.65
            Order processedOrder2 = orderprocessor.ProcessOrder(order2);
            Assert.Equal(processedOrder2.finalPrice, Convert.ToDouble(54.65).ToString());
            //Sales Taxes: 7.65
            Assert.Equal(Convert.ToDouble(processedOrder1.taxes + processedOrder2.taxes).ToString("0.00"), Convert.ToDouble(7.65).ToString("0.00"));
            //Total: 65.15
            Assert.Equal((Convert.ToDouble(processedOrder1.finalPrice) + Convert.ToDouble(processedOrder2.finalPrice)).ToString("0.00"), Convert.ToDouble(65.15).ToString("0.00"));
        }
        [Fact]
        public void Process_Order_Output3()
        {

            var orderprocessor = new OrderProcessor(new FakeTaxClculator(), new FakeImportFeesCalculator());
            var order1 = new Order
            {
                quantity = 1,
                orderName = "Imported bottle of perfume",
                rowPrice = Convert.ToDouble(27.99),
                imported = true,
                taxable = true
            };
            var order2 = new Order
            {
                quantity = 1,
                orderName = "Bottle of perfume",
                rowPrice = Convert.ToDouble(18.99),
                imported = false,
                taxable = true
            };
            var order3 = new Order
            {
                quantity = 1,
                orderName = " Packet of headache pills",
                rowPrice = Convert.ToDouble(9.75),
                imported = false,
                taxable = false
            };
            var order4 = new Order
            {
                quantity = 2,
                orderName = "Imported box of chocolates",
                rowPrice = Convert.ToDouble(11.25),
                imported = true,
                taxable = false
            };
            //testing Imported bottle of perfume: 32.19
            Order processedOrder1 = orderprocessor.ProcessOrder(order1);
            Assert.Equal(processedOrder1.finalPrice, Convert.ToDouble(32.19).ToString("0.00"));
            //testing Bottle of perfume: 20.89
            Order processedOrder2 = orderprocessor.ProcessOrder(order2);
            Assert.Equal(processedOrder2.finalPrice, Convert.ToDouble(20.89).ToString());
            //testing Packet of headache pills: 9.75
            Order processedOrder3 = orderprocessor.ProcessOrder(order3);
            Assert.Equal(processedOrder3.finalPrice, Convert.ToDouble(9.75).ToString());
            //testing Imported box of chocolates: 23.70(2 @ 11.85)
            Order processedOrder4 = orderprocessor.ProcessOrder(order4);
            Assert.Equal(processedOrder4.finalPrice, Convert.ToDouble(23.70).ToString("0.00"));
        }
        public class FakeImportFeesCalculator : IImportFeeCalculator
        {
            public double CalculateImportFee(double rowPrice)
            {
                double result = (rowPrice * 5) / 100;
                return Math.Ceiling(result / 0.05) * 0.05;
            }
        }
        public class FakeTaxClculator : ITaxCalculator
        {
            public double CalculateSalesTax(double rowPrice)
            {
                double result = (rowPrice * 10) / 100;
                return Math.Ceiling(result / 0.05) * 0.05;
            }
        }
    }
}
