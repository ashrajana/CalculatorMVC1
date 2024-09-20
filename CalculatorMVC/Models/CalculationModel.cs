using System.ComponentModel.DataAnnotations;

namespace CalculatorMVC.Models
{
    public class CalculationModel
    {
        public decimal Number1 { get; set; }
        public decimal Number2 { get; set; }
        public string Operation { get; set; }
        public decimal Result { get; set; }
    }
}