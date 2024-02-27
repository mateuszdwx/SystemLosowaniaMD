using DrawSystem.Controls;
using DrawSystem.Models;
using DrawSystem.Services;
using System.Collections.ObjectModel;

namespace DrawSystem.Views
{
    public partial class ClassPage : ContentPage
    {
        private readonly FileServices _fileServices;
        private readonly DrawService _drawService;

        public ObservableCollection<Student> Students { get; set; }
        public int LuckyNumber => _drawService.GetLuckyNumber();
        public string ClassName { get; }


        public ClassPage(string className)
        {
            InitializeComponent();
            _fileServices = new FileServices();
            ClassName = className;
            _drawService = new DrawService();

            Students = new ObservableCollection<Student>(_fileServices.ReadClassStudents(className));
            BindingContext = this;
        }

        private async void OnEditStudent_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var student = (Student)button.BindingContext;

            if (student != null)
            {
                await Navigation.PushAsync(new StudentPage(student, ClassName));
            }
            else
            {
                await DisplayAlert("Student", "Student not avaiable", "Ok");
            }
        }
        private async void OnDeleteStudent_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var student = (Student)button.BindingContext;

            if (student != null)
            {
                var confirmation = await DisplayAlert("Confirm", $"Do you really want to delete {student.Name} {student.Surname}?", "Yes", "Cancel");

                if (confirmation)
                {
                    Students.Remove(student);
                    _fileServices.SaveStudentData(ClassName, Students.ToList());
                }
            }
            else
            {
                await DisplayAlert("Student", "Student not avaiable", "Ok");
            }
        }

        private async void OnDraw_Clicked(object sender, EventArgs e)
        {
            var eligibleStudents = Students.Where(s => s.IsPresent).ToList();

            var drawnStudent = _drawService.DrawStudent(eligibleStudents);

            if (drawnStudent != null)
            {
                if (drawnStudent.Id == _drawService.GetLuckyNumber())
                {
                    await DisplayAlert("Drawing", $"Student with a luckynumber has been drawed: {drawnStudent.Name} {drawnStudent.Surname}", "OK");
                }
                else
                {
                    await DisplayAlert("Drawing", $"Student drawed: {drawnStudent.Name} {drawnStudent.Surname}", "OK");
                }
            }
            else
            {
                await DisplayAlert("Drawing", "No students avaiable to draw", "OK");
            }

        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkbox = (CheckBox)sender;
            var student = (Student)checkbox.BindingContext;

            if (student != null)
            {
                var stackLayout = (sender as View).Parent as StackLayout;

                if (stackLayout != null)
                {
                    if (e.Value)
                    {
                        stackLayout.BackgroundColor = Color.FromHex("#FFFFFF");
                    }
                    else
                    {
                        stackLayout.BackgroundColor = Color.FromRgb(169, 169, 169); 
                    }
                }
            }
        }

        private async void OnAddStudent_Clicked(object sender, EventArgs e)
        {
            var newStudent = new Student();
            Students.Add(newStudent);
            await Navigation.PushAsync(new StudentPage(newStudent, ClassName));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Students.Clear();
            var updatedStudents = _fileServices.ReadClassStudents(ClassName);
            foreach (var student in updatedStudents)
            {
                Students.Add(student);
            }
        }
    }
}


