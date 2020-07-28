using System;
using System.Collections.Generic;

namespace OrderProcessor
{
    // this class takes an order object to process orders for outpu with necessary calculations.
    public class OrderProcessor
    {
        private readonly IImportFeeCalculator _importFeeCalculator;
        private readonly ITaxCalculator _taxCalculator;
        // SOLID Principles: Order processor class close for modification and open for extensibility
        // if we need t0 add shipping calculator for exaple we just add a new Interface 
        // seperation of concerns, open for extens/closed for modification and Interface segragation.
        public OrderProcessor(ITaxCalculator taxCalculator, IImportFeeCalculator importFeeCalculator)
        {
            _taxCalculator = taxCalculator;
            _importFeeCalculator = importFeeCalculator;
        }
        public Order ProcessOrder(Order order)
        {
            List<Order> ord = new List<Order>();
            Order prc_order = new Order();
            double _results = 0;
            double _price = order.rowPrice;
            int _quantity = order.quantity;
            double _taxValue = 0;
            double _sumTaxes = 0;
            double _importedPriceBeforeTaxes = 0;
            double _sumimportFees = 0;
            double _totaTaxes = 0;
            if (order.imported)
            {
                _importedPriceBeforeTaxes = _importFeeCalculator.CalculateImportFee(_price);

                if (order.taxable)
                {
                    _taxValue = _taxCalculator.CalculateSalesTax(_price);
                    _results = ((_price + _taxValue + _importedPriceBeforeTaxes) * _quantity);
                }
                else
                {
                    _results = (_importedPriceBeforeTaxes + _price) * _quantity;
                }
            }
            else
            {
                if (order.taxable)
                {
                    _taxValue = _taxCalculator.CalculateSalesTax(_price);
                    _results = (_taxValue + _price) * _quantity;
                }
                else
                {
                    _results = (_price * _quantity);
                }
            }
            _sumTaxes = _sumTaxes + _taxValue;
            _sumimportFees = _sumimportFees + _importedPriceBeforeTaxes;
            _totaTaxes = _sumimportFees + _sumTaxes;
            Convert.ToDouble(_results.ToString("0.00"));
            prc_order.orderName = order.orderName;
            prc_order.quantity = order.quantity;
            prc_order.rowPrice = order.rowPrice;
            prc_order.taxes = _totaTaxes;
            prc_order.finalPrice = _results.ToString("0.00");
            return prc_order;
        }
    }
}
