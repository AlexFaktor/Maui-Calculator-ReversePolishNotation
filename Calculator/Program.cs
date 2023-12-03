namespace Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(LineCalculator.Calculate("100 * (456*75+33-(322-22--77+(3+3)*34))"));
        }
    }
}
