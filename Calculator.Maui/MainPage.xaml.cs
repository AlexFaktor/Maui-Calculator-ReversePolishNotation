namespace Calculator.Maui
{
    public partial class MainPage : ContentPage
    {
        private MainVM _vm;

        public MainPage()
        {
            InitializeComponent();
            _vm = (MainVM)BindingContext;
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            _vm.Expression += "1";
        }
        private void Button_Clicked_2(object sender, EventArgs e)
        {
            _vm.Expression += "2";
        }
        private void Button_Clicked_3(object sender, EventArgs e)
        {
            _vm.Expression += "3";
        }
        private void Button_Clicked_4(object sender, EventArgs e)
        {
            _vm.Expression += "4";
        }
        private void Button_Clicked_5(object sender, EventArgs e)
        {
            _vm.Expression += "5";
        }
        private void Button_Clicked_6(object sender, EventArgs e)
        {
            _vm.Expression += "6";
        }
        private void Button_Clicked_7(object sender, EventArgs e)
        {
            _vm.Expression += "7";
        }
        private void Button_Clicked_8(object sender, EventArgs e)
        {
            _vm.Expression += "8";
        }
        private void Button_Clicked_9(object sender, EventArgs e)
        {
            _vm.Expression += "9";
        }
        private void Button_Clicked_0(object sender, EventArgs e)
        {
            _vm.Expression += "0";
        }
        private void Button_Clicked_Plus(object sender, EventArgs e)
        {
            _vm.Expression += "+";
        }
        private void Button_Clicked_Minus(object sender, EventArgs e)
        {
            _vm.Expression += "-";
        }
        private void Button_Clicked_Multiplication(object sender, EventArgs e)
        {
            _vm.Expression += "*";
        }
        private void Button_Clicked_Division(object sender, EventArgs e)
        {
            _vm.Expression += "/";
        }
        private void Button_Clicked_Pow(object sender, EventArgs e)
        {
            _vm.Expression += "^";
        }
        private void Button_Clicked_Clear(object sender, EventArgs e)
        {
            _vm.Expression = "";
        }
        private void Button_Clicked_OpeningBracket(object sender, EventArgs e)
        {
            _vm.Expression += "(";
        }
        private void Button_Clicked_CloseBracket(object sender, EventArgs e)
        {
            _vm.Expression += ")";
        }
        private void Button_Clicked_Dot(object sender, EventArgs e)
        {
            _vm.Expression += ".";
        }
        private void Button_Clicked_Result(object sender, EventArgs e)
        {
            try
            {
                string result = PolishNotationCalculator.Calculate(_vm.Expression).ToString();
                _vm.Result = result;
            }
            catch (Exception ex)
            {
                _vm.Result = ex.Message;
            }
        }
        private void Button_Clicked_Backspace(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_vm.Expression))
            {
                _vm.Expression = _vm.Expression.Remove(_vm.Expression.Length - 1);
            }

        }
    }

}
