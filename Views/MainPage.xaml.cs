using DrawSystem.Services;
using DrawSystem.Views;
using System.Collections.ObjectModel;

namespace DrawSystem
{
    public partial class MainPage : ContentPage
    {
        private readonly FileServices _fileServices;
        public ObservableCollection<string> ClassList { get; set; }


        public MainPage()
        {
            InitializeComponent();
            _fileServices = new FileServices();
            ClassList = new ObservableCollection<string>(_fileServices.GetClassList());
            BindingContext = this;
        }

        private async void OnClass_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var className = button.Text;
            await Navigation.PushAsync(new ClassPage(className));
        }

        private async void OnAddClass_Clicked(object sender, EventArgs e)
        {
            string newClassName = await DisplayPromptAsync("New class", "Insert new class name", "Add", "Cancel", "Class name");

            if (!string.IsNullOrWhiteSpace(newClassName))
            {
                _fileServices.WriteClassStudents(newClassName, new List<string>());

                ClassList.Clear();
                var classList = _fileServices.GetClassList();
                foreach (var className in classList)
                {
                    ClassList.Add(className);
                }
            }
        }
    }
}