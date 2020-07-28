namespace OrderProcessor
{
    public interface IImportFeeCalculator
    {
        double CalculateImportFee(double rowPrice);
    }
}