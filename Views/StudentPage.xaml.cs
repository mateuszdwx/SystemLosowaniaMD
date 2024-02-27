using DrawSystem.Services;
using DrawSystem.Models;


namespace DrawSystem.Views;

public partial class StudentPage : ContentPage
{
    private readonly Student _student;
    private readonly string _className;
    private readonly FileServices _fileServices;

    public StudentPage(Student student, string className)
    {
        InitializeComponent();
        _fileServices = new FileServices();
        _student = student;
        _className = className;
        BindingContext = _student;
    }

    private async void OnSave_Clicked(object sender, EventArgs e)
    {
        var classStudents = _fileServices.ReadClassStudents(_className);
        if (_student.Id == 0)
        {
            _student.Id = classStudents.Count + 1;
            classStudents.Add(_student);
        }
        else
        {
            var existingStudent = classStudents.FirstOrDefault(s => s.Id == _student.Id);
            if (existingStudent != null)
            {
                existingStudent.Name = _student.Name;
                existingStudent.Surname = _student.Surname;
                existingStudent.IsPresent = _student.IsPresent;
            }
        }

        _fileServices.SaveStudentData(_className, classStudents);

        await Navigation.PopAsync();
    }

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        _student.IsPresent = e.Value;
        if (_student.IsPresent)
        {
            stackLayout.BackgroundColor = Color.FromHex("#FFFFFF");
        }
        else
        { 
            stackLayout.BackgroundColor = Color.FromRgb(169, 169, 169);
        }
    }



}