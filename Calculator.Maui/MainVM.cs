using CommunityToolkit.Mvvm.ComponentModel;

namespace Calculator.Maui;

public partial class MainVM : ObservableObject
{
    [ObservableProperty]
    string _expression = "";
    [ObservableProperty]
    string _result = "";
}
