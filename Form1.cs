using Microsoft.EntityFrameworkCore;
using GeniusContactManager.Data;
using GeniusContactManager.Models;
using GeniusContactManager.Forms;
using GeniusContactManager.Services;

namespace GeniusContactManager;

public partial class Form1 : Form
{
    private readonly ContactContext _context;
    private readonly ExcelService _excelService;
    private List<Contact> _currentContacts = new();
    private string _sortColumn = "Name";
    private bool _sortAscending = true;
    private bool _columnsInitialized = false;
    private Dictionary<string, int> _columnWidths = new();

    public Form1()
    {
        InitializeComponent();

        // Initialize database context (uses connection string from ContactContext.OnConfiguring)
        _context = new ContactContext(new DbContextOptionsBuilder<ContactContext>().Options);
        _excelService = new ExcelService();

        // Add handlers for DataGridView events
        dataGridViewContacts.CellValueChanged += DataGridViewContacts_CellValueChanged;
        dataGridViewContacts.DataBindingComplete += DataGridViewContacts_DataBindingComplete;
        dataGridViewContacts.ColumnWidthChanged += DataGridViewContacts_ColumnWidthChanged;
        dataGridViewContacts.CurrentCellDirtyStateChanged += DataGridViewContacts_CurrentCellDirtyStateChanged;

        InitializeForm();
        InitializeDataGridView();
        LoadContacts();
    }

    private void InitializeForm()
    {
        this.Text = "Genius Contact Manager";
        this.Size = new Size(1000, 700);
        this.StartPosition = FormStartPosition.CenterScreen;
    }

    private void InitializeDataGridView()
    {
        if (_columnsInitialized) return;

        // Configure DataGridView properties
        dataGridViewContacts.AutoGenerateColumns = false;
        dataGridViewContacts.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        dataGridViewContacts.AllowUserToResizeRows = false;
        dataGridViewContacts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        // Clear existing columns
        dataGridViewContacts.Columns.Clear();

        // Add columns in specific order with explicit configuration
        dataGridViewContacts.Columns.AddRange(new DataGridViewColumn[]
        {
            new DataGridViewTextBoxColumn
            {
                Name = "Name",
                DataPropertyName = "Name",
                HeaderText = "Name",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 30
            },
            new DataGridViewTextBoxColumn
            {
                Name = "Surname",
                DataPropertyName = "Surname",
                HeaderText = "Surname",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 30
            },
            new DataGridViewTextBoxColumn
            {
                Name = "PhoneNumber",
                DataPropertyName = "PhoneNumber",
                HeaderText = "Phone Number",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 30
            },
            new DataGridViewCheckBoxColumn
            {
                Name = "Used",
                DataPropertyName = "Used",
                HeaderText = "Used",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 10
            }
        });

        // Ensure all columns are properly sized and visible
        foreach (DataGridViewColumn col in dataGridViewContacts.Columns)
        {
            col.Visible = true;
        }

        _columnsInitialized = true;
    }

    private void LoadContacts()
    {
        try
        {
            _currentContacts = _context.Contacts.OrderBy(GetSortExpression()).ToList();
            RefreshDataGridView();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading contacts: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void RefreshDataGridView()
    {
        try
        {
            // Ensure columns are initialized
            InitializeDataGridView();

            // Save current column widths before rebinding
            SaveColumnWidths();

            // Clear selection before rebinding to prevent index issues
            dataGridViewContacts.ClearSelection();

            // Bind the data without recreating columns
            dataGridViewContacts.DataSource = null;
            dataGridViewContacts.DataSource = _currentContacts;

            // Restore column widths after rebinding
            RestoreColumnWidths();

            dataGridViewContacts.Refresh();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error refreshing data grid: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void SaveColumnWidths()
    {
        if (!_columnsInitialized) return;

        foreach (DataGridViewColumn column in dataGridViewContacts.Columns)
        {
            _columnWidths[column.Name] = column.Width;
        }
    }

    private void RestoreColumnWidths()
    {
        if (!_columnsInitialized) return;

        foreach (DataGridViewColumn column in dataGridViewContacts.Columns)
        {
            if (_columnWidths.ContainsKey(column.Name))
            {
                column.Width = _columnWidths[column.Name];
            }
        }
    }

    private void DataGridViewContacts_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
    {
        // Apply color coding after data binding is complete
        ApplyRowColoring();
    }

    private void DataGridViewContacts_ColumnWidthChanged(object? sender, DataGridViewColumnEventArgs e)
    {
        // Save the new column width when user resizes columns
        if (_columnsInitialized && e.Column != null)
        {
            _columnWidths[e.Column.Name] = e.Column.Width;
        }
    }

    private void DataGridViewContacts_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
    {
        // Commit the cell value change immediately for checkboxes
        if (dataGridViewContacts.IsCurrentCellDirty)
        {
            dataGridViewContacts.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
    }

    private void ApplyRowColoring()
    {
        try
        {
            if (dataGridViewContacts.Rows.Count == 0) return;
            if (_currentContacts == null || _currentContacts.Count == 0) return;

            foreach (DataGridViewRow row in dataGridViewContacts.Rows)
            {
                if (row?.DataBoundItem is Contact contact)
                {
                    // Green when Used is true, default color when Used is false
                    row.DefaultCellStyle.BackColor = contact.Used ? Color.LightGreen : SystemColors.Window;
                }
            }
            dataGridViewContacts.Refresh();
        }
        catch (Exception ex)
        {
            // Log the error but don't crash the application
            System.Diagnostics.Debug.WriteLine($"Error in ApplyRowColoring: {ex.Message}");
        }
    }

    private Func<Contact, object> GetSortExpression()
    {
        return _sortColumn switch
        {
            "Name" => c => c.Name,
            "Surname" => c => c.Surname,
            "PhoneNumber" => c => c.PhoneNumber,
            "Used" => c => c.Used,
            "CreatedDate" => c => c.CreatedDate,
            _ => c => c.Name
        };
    }

    private void SortContacts(string column)
    {
        if (_sortColumn == column)
        {
            _sortAscending = !_sortAscending;
        }
        else
        {
            _sortColumn = column;
            _sortAscending = true;
        }

        var sortExpression = GetSortExpression();
        _currentContacts = _sortAscending
            ? _currentContacts.OrderBy(sortExpression).ToList()
            : _currentContacts.OrderByDescending(sortExpression).ToList();

        RefreshDataGridView();
    }

    private void DataGridViewContacts_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            var columnName = dataGridViewContacts.Columns[e.ColumnIndex].DataPropertyName;
            SortContacts(columnName);
        }
    }

    private void ButtonAdd_Click(object sender, EventArgs e)
    {
        var form = new ContactForm();
        if (form.ShowDialog() == DialogResult.OK)
        {
            // Set dates for new contact
            if (form.Contact!.CreatedDate == default)
                form.Contact.CreatedDate = DateTime.Now;

            form.Contact.ModifiedDate = DateTime.Now;

            // Add to the in-memory list only, don't save to database
            _currentContacts.Add(form.Contact);
            RefreshDataGridView();

            MessageBox.Show("Contact added to list. Use 'Save to Database' to persist changes.",
                "Contact Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void ButtonEdit_Click(object sender, EventArgs e)
    {
        if (dataGridViewContacts.CurrentRow?.DataBoundItem is Contact selectedContact)
        {
            var form = new ContactForm(selectedContact);
            if (form.ShowDialog() == DialogResult.OK)
            {
                selectedContact.ModifiedDate = DateTime.Now;

                // Don't auto-save to database, just refresh the display
                RefreshDataGridView();

                MessageBox.Show("Contact updated in list. Use 'Save to Database' to persist changes.",
                    "Contact Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        else
        {
            MessageBox.Show("Please select a contact to edit.", "No Selection",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void ButtonDelete_Click(object sender, EventArgs e)
    {
        if (dataGridViewContacts.CurrentRow?.DataBoundItem is Contact selectedContact)
        {
            var result = MessageBox.Show($"Are you sure you want to delete {selectedContact.FullName}?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Remove from in-memory list only, don't delete from database yet
                _currentContacts.Remove(selectedContact);
                RefreshDataGridView();

                MessageBox.Show("Contact removed from list. Use 'Save to Database' to persist changes.",
                    "Contact Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        else
        {
            MessageBox.Show("Please select a contact to delete.", "No Selection",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void ButtonImport_Click(object sender, EventArgs e)
    {
        using var openFileDialog = new OpenFileDialog
        {
            Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
            FilterIndex = 1,
            Title = "Select Excel file to import"
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                var contacts = _excelService.ImportFromExcel(openFileDialog.FileName);

                var result = MessageBox.Show($"Found {contacts.Count} contacts to import. Continue?",
                    "Import Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Validate imported contacts
                    var validContacts = new List<Contact>();
                    var invalidContacts = new List<(Contact contact, List<string> errors)>();

                    foreach (var contact in contacts)
                    {
                        var validationErrors = contact.ValidateContact();
                        if (validationErrors.Count == 0)
                        {
                            // Set dates for valid imported contacts
                            if (contact.CreatedDate == default)
                                contact.CreatedDate = DateTime.Now;
                            contact.ModifiedDate = DateTime.Now;
                            validContacts.Add(contact);
                        }
                        else
                        {
                            invalidContacts.Add((contact, validationErrors));
                        }
                    }

                    // Show validation results
                    if (invalidContacts.Count > 0)
                    {
                        var errorMessage = $"Found {invalidContacts.Count} invalid contact(s):\n\n";
                        foreach (var (contact, errors) in invalidContacts.Take(5)) // Show first 5 errors
                        {
                            errorMessage += $"• {contact.Name} {contact.Surname}: {string.Join(", ", errors)}\n";
                        }
                        if (invalidContacts.Count > 5)
                            errorMessage += $"... and {invalidContacts.Count - 5} more.\n\n";

                        errorMessage += $"Only {validContacts.Count} valid contacts will be imported. Continue?";

                        var validationResult = MessageBox.Show(errorMessage, "Validation Errors Found",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (validationResult == DialogResult.No)
                            return;
                    }

                    // Add valid contacts to the in-memory list only, don't save to database
                    _currentContacts.AddRange(validContacts);
                    RefreshDataGridView();

                    var message = $"Successfully imported {validContacts.Count} valid contacts to list.";
                    if (invalidContacts.Count > 0)
                        message += $"\n{invalidContacts.Count} invalid contacts were skipped.";
                    message += "\nUse 'Save to Database' to persist changes.";

                    MessageBox.Show(message, "Import Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error importing file: {ex.Message}", "Import Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void ButtonExport_Click(object sender, EventArgs e)
    {
        using var saveFileDialog = new SaveFileDialog
        {
            Filter = "Excel files (*.xlsx)|*.xlsx",
            FilterIndex = 1,
            Title = "Export contacts to Excel",
            FileName = $"Contacts_Export_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
        };

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                _excelService.ExportToExcel(_currentContacts, saveFileDialog.FileName);
                MessageBox.Show($"Successfully exported {_currentContacts.Count} contacts to {saveFileDialog.FileName}",
                    "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting file: {ex.Message}", "Export Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void ButtonSaveToDb_Click(object sender, EventArgs e)
    {
        try
        {
            var result = MessageBox.Show($"This will save all current contacts to the database and remove duplicates. Continue?",
                "Save to Database", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // First, validate all contacts before saving
                var validContacts = new List<Contact>();
                var invalidContacts = new List<(Contact contact, List<string> errors)>();

                foreach (var contact in _currentContacts)
                {
                    var validationErrors = contact.ValidateContact();
                    if (validationErrors.Count == 0)
                    {
                        validContacts.Add(contact);
                    }
                    else
                    {
                        invalidContacts.Add((contact, validationErrors));
                    }
                }

                // Show validation results if there are errors
                if (invalidContacts.Count > 0)
                {
                    var errorMessage = $"Found {invalidContacts.Count} invalid contact(s):\n\n";
                    foreach (var (contact, errors) in invalidContacts.Take(5)) // Show first 5 errors
                    {
                        errorMessage += $"• {contact.Name} {contact.Surname}: {string.Join(", ", errors)}\n";
                    }
                    if (invalidContacts.Count > 5)
                        errorMessage += $"... and {invalidContacts.Count - 5} more.\n\n";

                    errorMessage += $"Only {validContacts.Count} valid contacts will be saved. Continue?";

                    var validationResult = MessageBox.Show(errorMessage, "Validation Errors Found",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (validationResult == DialogResult.No)
                        return;
                }

                int savedCount = 0;
                int updatedCount = 0;
                int duplicateCount = 0;
                int skippedInvalid = invalidContacts.Count;

                // Get all existing contacts from database for duplicate checking
                var existingContacts = _context.Contacts.ToList();

                foreach (var contact in validContacts) // Only process valid contacts
                {
                    // Check if this contact exists in the database (by ID if it has one)
                    var existingContact = contact.Id > 0 ? existingContacts.FirstOrDefault(ec => ec.Id == contact.Id) : null;

                    if (existingContact != null)
                    {
                        // Update existing contact
                        existingContact.Name = contact.Name;
                        existingContact.Surname = contact.Surname;
                        existingContact.PhoneNumber = contact.PhoneNumber;
                        existingContact.Used = contact.Used;
                        existingContact.ModifiedDate = DateTime.Now;
                        _context.Entry(existingContact).State = EntityState.Modified;
                        updatedCount++;
                    }
                    else
                    {
                        // Check for duplicates based on Name, Surname, and PhoneNumber for new contacts
                        var isDuplicate = existingContacts.Any(ec =>
                            ec.Name.Equals(contact.Name, StringComparison.OrdinalIgnoreCase) &&
                            ec.Surname.Equals(contact.Surname, StringComparison.OrdinalIgnoreCase) &&
                            ec.PhoneNumber.Equals(contact.PhoneNumber, StringComparison.OrdinalIgnoreCase));

                        if (!isDuplicate)
                        {
                            // Create new contact
                            var newContact = new Contact
                            {
                                Name = contact.Name,
                                Surname = contact.Surname,
                                PhoneNumber = contact.PhoneNumber,
                                Used = contact.Used,
                                CreatedDate = contact.CreatedDate == default ? DateTime.Now : contact.CreatedDate,
                                ModifiedDate = DateTime.Now
                            };

                            _context.Contacts.Add(newContact);
                            savedCount++;
                        }
                        else
                        {
                            duplicateCount++;
                        }
                    }
                }

                _context.SaveChanges();
                LoadContacts(); // Refresh from database

                var message = $"Save complete!\n";
                if (savedCount > 0) message += $"New contacts saved: {savedCount}\n";
                if (updatedCount > 0) message += $"Existing contacts updated: {updatedCount}\n";
                if (duplicateCount > 0) message += $"Skipped duplicates: {duplicateCount}\n";
                if (skippedInvalid > 0) message += $"Skipped invalid contacts: {skippedInvalid}";

                MessageBox.Show(message.TrimEnd(),
                    "Save to Database Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error saving to database: {ex.Message}", "Save Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void DataGridViewContacts_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
    {
        try
        {
            // Validate event arguments and grid state
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dataGridViewContacts.Columns.Count == 0) return;
            if (e.RowIndex >= dataGridViewContacts.Rows.Count) return;

            // Find the "Used" column safely
            var usedColumn = dataGridViewContacts.Columns["Used"];
            if (usedColumn == null) return;

            // Check if this is the Used column
            if (e.ColumnIndex == usedColumn.Index)
            {
                var row = dataGridViewContacts.Rows[e.RowIndex];
                if (row?.DataBoundItem is Contact contact && row.Cells[e.ColumnIndex]?.Value != null)
                {
                    contact.Used = (bool)row.Cells[e.ColumnIndex].Value;
                    contact.ModifiedDate = DateTime.Now;

                    // Don't auto-save to database, just update in memory and refresh colors
                    ApplyRowColoring(); // Refresh the row colors
                }
            }
        }
        catch (Exception ex)
        {
            // Log the error but don't crash the application
            System.Diagnostics.Debug.WriteLine($"Error in CellValueChanged: {ex.Message}");
            // Optionally show a user-friendly message
            // MessageBox.Show("An error occurred while updating the contact. Please try again.", "Error", 
            //     MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _context?.Dispose();
        base.OnFormClosing(e);
    }
}
