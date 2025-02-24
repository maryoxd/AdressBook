# 📖 Employee Address Book – C# Contact Management System  

**Employee Address Book** is a **C# application** designed for managing, searching, and exporting employee contact details. The project consists of a **class library, a console application, and two WPF desktop applications**, allowing users to **view, edit, and filter employee records** based on workplace, position, and name.  

## 📌 Features  
- **Employee contact management** – Store and retrieve detailed information.  
- **Console application for quick search** – Filter employees based on workplace, position, or name.  
- **WPF Viewer application** – Displays and filters employee records dynamically.  
- **WPF Editor application** – Modify and save employee records in JSON format.  
- **CSV Export support** – Save filtered results for external use.  
- **Data persistence with JSON** – Ensures easy storage and retrieval.  

## 🛠️ Technologies Used  
- **C# (.NET Core / .NET Framework)**  
- **WPF (Windows Presentation Foundation)**  
- **JSON Serialization for Data Storage**  
- **CSV Export & File Handling**  
- **MVVM Architecture (WPF Applications)**  

## 🚀 How to Install & Run  
1. **Clone the repository:**  
   ```bash
   git clone https://github.com/maryoxd/EmployeeAddressBook.git  
   cd EmployeeAddressBook
2. **Build and run the Console Application:**
   ```bash
   dotnet build  
   dotnet run --project AddressBookConsole  
3. **Run the WPF Viewer Application:**
   ```bash
   dotnet run --project AddressBookViewer
4. **Run the WPF Editor Application:**
   ```bash
   dotnet run --project AddressBookEditor  
