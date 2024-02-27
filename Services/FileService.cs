using DrawSystem.Models;

namespace DrawSystem.Services
{
    internal class FileServices
    {
        private readonly string _classesFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Classes");

        private string GetClassFilePath(string className)
        {
            return Path.Combine(_classesFolder, $"{className}.txt");
        }

        public List<string> GetClassList()
        {
            try
            {
                if (!Directory.Exists(_classesFolder))
                {
                    Directory.CreateDirectory(_classesFolder);
                }

                string[] classFiles = Directory.GetFiles(_classesFolder, "*.txt");
                List<string> classNames = new List<string>();

                foreach (string filePath in classFiles)
                {
                    string className = Path.GetFileNameWithoutExtension(filePath);
                    classNames.Add(className);
                }

                return classNames;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<string>();
            }
        }


        public List<Student> ReadClassStudents(string className)
        {
            try
            {
                string filePath = GetClassFilePath(className);
                List<Student> students = new List<Student>();

                if (File.Exists(filePath))
                {
                    var lines = File.ReadAllLines(filePath);
                    foreach (var line in lines)
                    {
                        var parts = line.Split(',');
                        if (parts.Length == 4)
                        {
                            students.Add(new Student
                            {
                                Id = int.Parse(parts[0]),
                                Name = parts[1],
                                Surname = parts[2],
                                IsPresent = bool.Parse(parts[3])
                            });
                        }
                    }
                }
                else
                {
                    File.Create(filePath).Close();
                }

                return students;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<Student>();
            }
        }

        public void WriteClassStudents(string className, List<string> students)
        {
            try
            {
                string filePath = GetClassFilePath(className);

                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    foreach (var student in students)
                    {
                        writer.WriteLine(student);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void SaveStudentData(string className, List<Student> students)
        {
            try
            {
                string filePath = GetClassFilePath(className);

                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    foreach (var student in students)
                    {
                        writer.WriteLine($"{student.Id},{student.Name},{student.Surname},{student.IsPresent}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public int GetMaxStudentId()
        {
            int maxId = 0;
            foreach (var className in GetClassList())
            {
                var students = ReadClassStudents(className);
                if (students.Any())
                {
                    int classMaxId = students.Max(s => s.Id);
                    maxId = Math.Max(maxId, classMaxId);
                }
            }
            return maxId;
        }
    }
}