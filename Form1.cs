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

    public Form1()
    {
        InitializeComponent();

        // Initialize database context (uses connection string from ContactContext.OnConfiguring)
        _context = new ContactContext(new DbContextOptionsBuilder<ContactContext>().Options);
        _excelService = new ExcelService();

        // Add handler for Used checkbox changes
        dataGridViewContacts.CellValueChanged += DataGridViewContacts_CellValueChanged;
        
        InitializeForm();
        LoadContacts();
    }

    private void InitializeForm()
    {
        this.Text = "Genius Contact Manager";
        this.Size = new Size(1000, 700);
        this.StartPosition = FormStartPosition.CenterScreen;
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
        dataGridViewContacts.DataSource = null;
        dataGridViewContacts.DataSource = _currentContacts;

        // Configure columns
        if (dataGridViewContacts.Columns.Count > 0)
        {
            dataGridViewContacts.Columns["Id"].Visible = false;
            dataGridViewContacts.Columns["CreatedDate"].Visible = false;
            dataGridViewContacts.Columns["ModifiedDate"].Visible = false;
            dataGridViewContacts.Columns["FullName"].Visible = false;
            dataGridViewContacts.Columns["DisplayText"].Visible = false;

            // Set column headers
            dataGridViewContacts.Columns["Name"].HeaderText = "Name";
            dataGridViewContacts.Columns["Surname"].HeaderText = "Surname";
            dataGridViewContacts.Columns["PhoneNumber"].HeaderText = "Phone Number";
            dataGridViewContacts.Columns["Used"].HeaderText = "Used";

            // Configure Used column as checkbox
            if (dataGridViewContacts.Columns["Used"] is DataGridViewColumn usedColumn)
            {
                var checkboxColumn = new DataGridViewCheckBoxColumn
                {
                    Name = "Used",
                    HeaderText = "Used",
                    DataPropertyName = "Used",
                    Width = 80,
                    ReadOnly = false
                };
                
                int usedColumnIndex = usedColumn.Index;
                dataGridViewContacts.Columns.RemoveAt(usedColumnIndex);
                dataGridViewContacts.Columns.Insert(usedColumnIndex, checkboxColumn);
            }

            // Set column widths
            dataGridViewContacts.Columns["Name"].Width = 150;
            dataGridViewContacts.Columns["Surname"].Width = 150;
            dataGridViewContacts.Columns["PhoneNumber"].Width = 150;
        }

        // Apply color coding to rows based on Used field
        ApplyRowColoring();
    }

    private void ApplyRowColoring()
    {
        foreach (DataGridViewRow row in dataGridViewContacts.Rows)
        {
            if (row.DataBoundItem is Contact contact)
            {
                if (contact.Used)
                {
                    row.DefaultCellStyle.BackColor = Color.LightCoral; // Red for used
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen; // Green for not used
                }
            }
        }
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

    private void DataGridViewContacts_CellValueChanged(object sender, DataGridViewCellEventArgs e)
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
