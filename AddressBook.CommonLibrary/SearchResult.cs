using System.Text;

namespace AddressBook.CommonLibrary
{
    public class SearchResult(Employee[] employees)
    {
        public Employee[] Employees { get; } = employees;

        public void SaveToCsv(FileInfo csvFile, string delimiter = "\t")
        {
            try
            {
                StringBuilder csvContent = new();
                csvContent.AppendLine("Name" + delimiter + "MainWorkplace" + delimiter + "Workplace" + delimiter +
                                      "Room" + delimiter + "Phone" + delimiter + "Email" + delimiter + "Position");

                foreach (var employee in Employees)
                {
                    csvContent.AppendLine(
                        $"{employee.Name}{delimiter}{employee.MainWorkPlace}{delimiter}{employee.WorkPlace}{delimiter}" +
                        $"{employee.Room}{delimiter}{employee.Phone}{delimiter}{employee.Email}{delimiter}{employee.Position}");
                }
                File.WriteAllText(csvFile.FullName, csvContent.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CHYBA PRI ZAPISOVANÍ ZAMESTNANCOV DO .CSV SÚBORU!: {ex.Message}");
            }
        }

        public void WriteToConsole()
        {
            try
            {
                StringBuilder text = new();

                for (int i = 0; i < Employees.Length; i++)
                {
                    text.AppendLine(
                        $"[{i + 1}]\t\t{Employees[i].Name}\nPracovisko:\t{Employees[i].MainWorkPlace}\nMiestnosť:\t{Employees[i].Room}\n"+
                        $"Telefón:\t{Employees[i].Phone}\nE-mail:\t\t{Employees[i].Email}\nFunkcia:\t{Employees[i].Position}\n");
                }
                Console.WriteLine(text.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CHYBA PRI VYPISOVANÍ ZAMESTNANCOV!: {ex.Message}");
            }
        }
    }

}
