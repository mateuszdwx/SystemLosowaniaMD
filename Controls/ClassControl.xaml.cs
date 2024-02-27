namespace DrawSystem.Controls;

public partial class ClassControl : ContentView
{
    public static readonly BindableProperty ClassNameProperty =
        BindableProperty.Create(nameof(ClassName), typeof(string), typeof(ClassControl), string.Empty);

    public string ClassName
    {
        get => (string)GetValue(ClassNameProperty);
        set => SetValue(ClassNameProperty, value);
    }

    public ClassControl()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private void ShowStudents_Clicked(object sender, EventArgs e)
    {
        ShowStudentsClicked?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler ShowStudentsClicked;
}
