using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace AddressBook.CommonLibrary
{
    public class EmployeeList : ObservableCollection<Employee>
    {
        public static EmployeeList? LoadFromJson(FileInfo jsonFile)
        {
            try
            {
                EmployeeList employeeList = [];
                string json = File.ReadAllText(jsonFile.FullName);
                List<Employee>? employees = JsonConvert.DeserializeObject<List<Employee>>(json);

                if (employees != null)
                    for (int i = 0; i < employees.Count; i++)
                    {
                        employeeList.Add(employees.ElementAt(i));
                    }
                return employeeList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR PRI NAČÍTAVANÍ ZAMESTNANCOV: " + ex.Message);
                return null;
            }
        }

        public void SaveToJson(FileInfo jsonFile)
        {
            try
            {
                string json = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(jsonFile.FullName, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR PRI UKLADANÍ ZAMESTNANCOV: " + ex.Message);
            }
        }

        public IOrderedEnumerable<string?> GetPositions()
        {
            return this.Select(e => e.Position).Distinct().OrderBy(position => position);
        }

        public IOrderedEnumerable<string?> GetMainWorkPlaces()
        {
            return this.Select(e => e.MainWorkPlace).Distinct().OrderBy(mainWorkPlace => mainWorkPlace);
        }

        public SearchResult Search(string? mainWorkplace = null, string? position = null, string? name = null)
        {
            IEnumerable<Employee> result = this;

            if (mainWorkplace != null)
                result = result.Where(employee => string.Equals(employee.MainWorkPlace, mainWorkplace, StringComparison.OrdinalIgnoreCase));

            if (position != null)
                result = result.Where(employee => string.Equals(employee.Position, position, StringComparison.OrdinalIgnoreCase));

            if (name != null)
                result = result.Where(employee => employee.Name != null && employee.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

            return new SearchResult(result.ToArray());
        }
    }
}

