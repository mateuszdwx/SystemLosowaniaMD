namespace DrawSystem.Controls;

public partial class StudentControl : ContentView
{
    private static readonly BindableProperty IdProperty =
        BindableProperty.Create(nameof(Id), typeof(int), typeof(StudentControl), 0);

    private static readonly BindableProperty NameProperty =
        BindableProperty.Create(nameof(Name), typeof(string), typeof(StudentControl), string.Empty);

    private static readonly BindableProperty SurnameProperty =
        BindableProperty.Create(nameof(Surname), typeof(string), typeof(StudentControl), string.Empty);

    public static readonly BindableProperty IsPresentProperty =
            BindableProperty.Create(nameof(IsPresent), typeof(bool), typeof(StudentControl), true, BindingMode.TwoWay);

    public int Id
    {
        get => (int)GetValue(IdProperty);
        set => SetValue(IdProperty, value);
    }

    public string Name
    {
        get => (string)GetValue(NameProperty);
        set => SetValue(NameProperty, value);
    }

    public string Surname
    {
        get => (string)GetValue(SurnameProperty);
        set => SetValue(SurnameProperty, value);
    }

    public bool IsPresent
    {
        get => (bool)GetValue(IsPresentProperty);
        set => SetValue(IsPresentProperty, value);
    }

    public StudentControl()
    {
        InitializeComponent();
        BindingContext = this;

    }

    private void EditStudent_Clicked(object sender, EventArgs e)
    {
        EditStudentClicked?.Invoke(this, EventArgs.Empty);
    }

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        IsPresent = e.Value;
        if (IsPresent)
        {
            stackLayout.BackgroundColor = Color.FromHex("#FFFFFF");
        }
        else
        {
            stackLayout.BackgroundColor = Color.FromRgb(169, 169, 169);
        }
    }



    public event EventHandler EditStudentClicked;

}
