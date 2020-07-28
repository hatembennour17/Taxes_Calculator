namespace OrderProcessor
{
    public interface ITaxCalculator
    {
        double CalculateSalesTax(double rowPrice);
    }
}
