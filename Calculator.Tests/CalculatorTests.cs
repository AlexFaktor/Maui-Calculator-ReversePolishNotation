using System.Diagnostics;

namespace Calculator.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        private string? tempFilePathFrom;
        private string? tempFilePathTo;
        private string? pathToExe;

        private static string RunConsoleApp(string pathToExe, string? args, string? consoleInput)
        {
            var psi = new ProcessStartInfo
            {
                FileName = pathToExe,
                Arguments = args,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            var process = Process.Start(psi)!;

            if (!string.IsNullOrEmpty(consoleInput))
            {
                process.StandardInput.WriteLine(consoleInput);
            }

            var reader = process.StandardOutput;
            var actualOutput = reader.ReadToEnd();

            process.WaitForExit();

            return actualOutput;
        }
        
        [TestInitialize]
        public void Initialize()
        {
            pathToExe = Path.Combine(Directory.GetCurrentDirectory(), "Calculator.exe");
            tempFilePathTo = Path.Combine(Path.GetTempPath(), "tempFileTo.txt");
            tempFilePathFrom = Path.Combine(Path.GetTempPath(), "tempFileFrom.txt");
            File.WriteAllText(tempFilePathFrom, "(-3)*(200*1.5+400-(50+230)-(30)*(445/50*30))\n1+2*(3+2)\n2+15/3+4*2\n1+x+4\n4/0\n10+(3-3))");

        }

        [TestMethod]
        public void Main_WithValidInputWithArguments_ShouldReturnExpectedValue() 
        {
            string expectedOutput = "(-3)*(200*1.5+400-(50+230)-(30)*(445/50*30)) -> 22770\r\n1+2*(3+2) -> 11\r\n2+15/3+4*2 -> 15\r\n1+x+4 -> Exception. Incorrect input format.\r\n4/0 -> Exception. Not divisible by 0.\r\n10+(3-3)) -> Exception. Not all brackets are closed.\r\n";
            RunConsoleApp($"{pathToExe}", args: $"{tempFilePathFrom} {tempFilePathTo}", consoleInput: null);

            string actualOutput = File.ReadAllText($"{tempFilePathTo}");

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void Main_WithValidInputWithoutArguments_ShouldReturnExpectedValue()
        {
            string expectedOutput = "Enter the expression -> 3+3*3-(10/2) -> 7\r\n";
            
            string actualOutput = RunConsoleApp($"{pathToExe}", args: null, consoleInput: "3+3*3-(10/2)");

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (File.Exists(tempFilePathFrom))
                File.Delete(tempFilePathFrom);
            if (File.Exists(tempFilePathTo))
                File.Delete(tempFilePathTo);
        }
    }
}