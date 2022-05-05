using System;
namespace Cbr_SystemGlobalServices.ServerModels
{
    /// <summary>
    /// Model for currency
    /// </summary>
    public class Currency
    {
        public string ID { get; set; } = "Unknown";
        public string NumCode { get; set; } = "Unknown";
        public string CharCode { get; set; } = "Unknown";
        public int Nominal { get; set; }
        public string Name { get; set; } = "Unknown";
        public double Value { get; set; }
        public double Previous { get; set; }
    }
}

