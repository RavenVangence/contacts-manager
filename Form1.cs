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
        // Ensure columns are initialized
        InitializeDataGridView();

        // Save current column widths before rebinding
        SaveColumnWidths();

        // Bind the data without recreating columns
        dataGridViewContacts.DataSource = null;
        dataGridViewContacts.DataSource = _currentContacts;

        // Restore column widths after rebinding
        RestoreColumnWidths();

        dataGridViewContacts.Refresh();
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

    private void ApplyRowColoring()
    {
        if (dataGridViewContacts.Rows.Count == 0) return;

        foreach (DataGridViewRow row in dataGridViewContacts.Rows)
        {
            if (row.DataBoundItem is Contact contact)
            {
                row.DefaultCellStyle.BackColor = contact.Used ? Color.LightCoral : Color.LightGreen;
            }
        }
        dataGridViewContacts.Refresh();
    }

    private Func<Contact, object> GetSortExpression()
    {
        return _sortColumn switch
        {
            "Name" => c => c.Name,
            "Surname" => c => c.Surname,
            "PhoneNumber" => c => c.PhoneNumber ?? "",
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
            _context.Contacts.Add(form.Contact!);
            _context.SaveChanges();
            LoadContacts();
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
                _context.Entry(selectedContact).State = EntityState.Modified;
                _context.SaveChanges();
                LoadContacts();
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
                _context.Contacts.Remove(selectedContact);
                _context.SaveChanges();
                LoadContacts();
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
                    _context.Contacts.AddRange(contacts);
                    _context.SaveChanges();
                    LoadContacts();

                    MessageBox.Show($"Successfully imported {contacts.Count} contacts.",
                        "Import Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                int savedCount = 0;
                int duplicateCount = 0;

                // Get all existing contacts from database for duplicate checking
                var existingContacts = _context.Contacts.ToList();

                foreach (var contact in _currentContacts)
                {
                    // Check for duplicates based on Name, Surname, and PhoneNumber
                    var isDuplicate = existingContacts.Any(ec =>
                        ec.Name.Equals(contact.Name, StringComparison.OrdinalIgnoreCase) &&
                        ec.Surname.Equals(contact.Surname, StringComparison.OrdinalIgnoreCase) &&
                        ((string.IsNullOrEmpty(ec.PhoneNumber) && string.IsNullOrEmpty(contact.PhoneNumber)) ||
                         (!string.IsNullOrEmpty(ec.PhoneNumber) && !string.IsNullOrEmpty(contact.PhoneNumber) &&
                          ec.PhoneNumber.Equals(contact.PhoneNumber, StringComparison.OrdinalIgnoreCase))));

                    if (!isDuplicate)
                    {
                        // Check if the contact is already being tracked by EF
                        var existingEntry = _context.Entry(contact);
                        if (existingEntry.State == EntityState.Detached)
                        {
                            // Set dates if they're not already set
                            if (contact.CreatedDate == default)
                                contact.CreatedDate = DateTime.Now;

                            contact.ModifiedDate = DateTime.Now;

                            _context.Contacts.Add(contact);
                            savedCount++;
                        }
                        else if (existingEntry.State == EntityState.Modified)
                        {
                            // Contact is already tracked and modified
                            contact.ModifiedDate = DateTime.Now;
                            savedCount++;
                        }
                    }
                    else
                    {
                        duplicateCount++;
                    }
                }

                _context.SaveChanges();
                LoadContacts(); // Refresh the display

                MessageBox.Show($"Save complete!\nSaved: {savedCount} contacts\nSkipped duplicates: {duplicateCount}",
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
        if (e.ColumnIndex == dataGridViewContacts.Columns["Used"].Index && e.RowIndex >= 0)
        {
            if (dataGridViewContacts.Rows[e.RowIndex].DataBoundItem is Contact contact)
            {
                contact.Used = (bool)dataGridViewContacts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                contact.ModifiedDate = DateTime.Now;
                _context.Entry(contact).State = EntityState.Modified;
                _context.SaveChanges();
                ApplyRowColoring(); // Refresh the row colors
            }
        }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _context?.Dispose();
        base.OnFormClosing(e);
    }
}
