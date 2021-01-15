namespace ShoppingCart.Models
{
    public class ExchangeRate
    {
        public string SourceCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public double Rate { get; set; }
    }
}
