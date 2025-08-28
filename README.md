# Genius Contact Manager

A .NET 8.0 Windows Forms application for managing contacts with Excel import/export functionality and color-coded display based on contact usage status.

## Features

### ✅ Main Menu
- Provides navigation to all main sections of the application through menu bar and toolbar buttons.

### ✅ Browse Box (DataGridView)
- Displays a scrollable list of contact records.
- Supports sorting by clicking on column headers (ascending/descending).
- **Color-coded rows**: 
  - 🟢 Green background for unused contacts (Used = false)
  - 🔴 Red background for used contacts (Used = true)

### ✅ Record Management
- **Add Contact** – Opens an update form to create a new contact record.
- **Delete Contact** – Removes a selected contact from the database with confirmation.
- **Edit Contact** – Opens an update form to modify an existing contact record.
- **Double-click** on any row to edit the contact.

### ✅ Update Screen (ContactForm)
- Clean form layout for adding new contacts or editing existing ones.
- Input validation with error provider feedback.
- Required fields: Name and Surname.
- Optional fields: Cell Number, Email, Address.
- Used checkbox to mark contact as used/unused.

### ✅ Import & Export
- **Import from Excel** – Reads data from any .xlsx file and inserts it into the database.
- **Import SA Contacts** – Special import for the `sa_contacts.xlsx` file format.
- **Export to Excel** – Exports all current contacts to Excel format with timestamp.

## Technical Implementation

### Database
- **Entity Framework Core 9.0** with SQL Server LocalDB
- **Contact Model** with fields: Id, Name, Surname, CellNo, Email, Address, Used, CreatedDate, ModifiedDate
- **Database migrations** for schema management
- **Seed data** with sample contacts

### Excel Operations
- **EPPlus 8.1** library for reading and writing Excel files
- Support for multiple Excel formats and error handling
- **Color-coded export** maintains the visual distinction

### UI Components
- **Windows Forms** with modern .NET 8.0
- **DataGridView** with custom row coloring
- **MenuStrip** and **ToolStrip** for navigation
- **Modal dialogs** for contact editing
- **Error validation** with ErrorProvider

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- SQL Server LocalDB (usually included with Visual Studio)
- Windows 10/11

### Running the Application

1. **Clone or download** this repository
2. **Navigate** to the project directory
3. **Restore packages** and **build**:
   ```bash
   dotnet restore
   dotnet build
   ```
4. **Run the application**:
   ```bash
   dotnet run
   ```

### Database Setup
The application will automatically:
- Create the LocalDB database on first run
- Apply migrations to set up the schema
- Insert sample data (John Doe and Jane Smith)

## Usage

### Adding a Contact
1. Click **"Add Contact"** button or use the menu `Edit > Add Contact`
2. Fill in the required fields (Name, Surname)
3. Optionally add Cell Number, Email, Address
4. Check "Used" if the contact has been used
5. Click **Save**

### Editing a Contact
1. **Select** a contact in the grid or **double-click** on any row
2. Click **"Edit Contact"** button or use the menu `Edit > Edit Contact`
3. Modify the fields as needed
4. Click **Save**

### Importing Contacts
- **From Excel**: Click **"Import Excel"** and select any .xlsx file
- **SA Contacts**: Click **"Import SA Contacts"** to import the specific `sa_contacts.xlsx` file
- The app will show a preview of contacts found and ask for confirmation

### Exporting Contacts
1. Click **"Export Excel"** button
2. Choose save location
3. File will be saved with timestamp (e.g., `Contacts_Export_20250828_143000.xlsx`)

### Visual Indicators
- 🟢 **Green rows** = Unused contacts (Available for use)
- 🔴 **Red rows** = Used contacts (Already utilized)

## Project Structure

```
GeniusContactManager/
├── Data/
│   ├── ContactContext.cs          # EF Core DbContext
│   └── ContactContextFactory.cs   # Design-time factory
├── Forms/
│   ├── ContactForm.cs             # Add/Edit contact dialog
│   └── ContactForm.Designer.cs    # UI layout
├── Models/
│   └── Contact.cs                 # Contact entity model
├── Services/
│   └── ExcelService.cs            # Excel import/export logic
├── Migrations/                    # EF Core migrations
├── Form1.cs                       # Main application form
├── Form1.Designer.cs              # Main form UI layout
└── Program.cs                     # Application entry point
```

## Future Enhancements

- [ ] Search and filter functionality
- [ ] Pagination for large data sets
- [ ] Role-based access control
- [ ] Contact categories/groups
- [ ] Export to CSV format
- [ ] Backup and restore functionality
- [ ] Contact photo support
- [ ] Print contact list

## License

This project is for educational and non-commercial use. EPPlus library is used under the non-commercial license.

## Support

For issues or questions, please check the code comments and Entity Framework documentation for database-related questions.
