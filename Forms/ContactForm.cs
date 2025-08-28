using GeniusContactManager.Models;

namespace GeniusContactManager.Forms
{
    public partial class ContactForm : Form
    {
        public Contact? Contact { get; private set; }
        private bool _isEditMode;

        public ContactForm()
        {
            InitializeComponent();
            _isEditMode = false;
            Contact = new Contact();
            this.Text = "Add New Contact";
            AddValidationEvents();
        }

        public ContactForm(Contact contact)
        {
            InitializeComponent();
            _isEditMode = true;
            Contact = contact;
            this.Text = "Edit Contact";
            AddValidationEvents();
            LoadContactData();
        }

        private void AddValidationEvents()
        {
            // Add real-time validation events
            textBoxName.Leave += TextBoxName_Leave;
            textBoxSurname.Leave += TextBoxSurname_Leave;
            textBoxPhoneNumber.Leave += TextBoxPhoneNumber_Leave;

            // Add KeyPress events to restrict input
            textBoxName.KeyPress += TextBoxName_KeyPress;
            textBoxSurname.KeyPress += TextBoxSurname_KeyPress;
            textBoxPhoneNumber.KeyPress += TextBoxPhoneNumber_KeyPress;
        }

        private void TextBoxName_Leave(object? sender, EventArgs e)
        {
            ValidateNameField();
        }

        private void TextBoxSurname_Leave(object? sender, EventArgs e)
        {
            ValidateSurnameField();
        }

        private void TextBoxPhoneNumber_Leave(object? sender, EventArgs e)
        {
            ValidatePhoneField();
        }

        private void TextBoxName_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // Allow letters, spaces, hyphens, apostrophes, dots, and control keys
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) &&
                e.KeyChar != ' ' && e.KeyChar != '-' && e.KeyChar != '\'' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void TextBoxSurname_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // Allow letters, spaces, hyphens, apostrophes, dots, and control keys
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) &&
                e.KeyChar != ' ' && e.KeyChar != '-' && e.KeyChar != '\'' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void TextBoxPhoneNumber_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // Allow digits, spaces, hyphens, plus signs, parentheses, and control keys
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                e.KeyChar != ' ' && e.KeyChar != '-' && e.KeyChar != '+' &&
                e.KeyChar != '(' && e.KeyChar != ')')
            {
                e.Handled = true;
            }
        }

        private void ValidateNameField()
        {
            errorProvider1.SetError(textBoxName, "");

            if (!string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                var tempContact = new Contact { Name = textBoxName.Text.Trim() };
                var errors = tempContact.ValidateContact();
                var nameError = errors.FirstOrDefault(e => e.Contains("Name"));
                if (nameError != null)
                {
                    errorProvider1.SetError(textBoxName, nameError);
                }
            }
        }

        private void ValidateSurnameField()
        {
            errorProvider1.SetError(textBoxSurname, "");

            if (!string.IsNullOrWhiteSpace(textBoxSurname.Text))
            {
                var tempContact = new Contact { Surname = textBoxSurname.Text.Trim() };
                var errors = tempContact.ValidateContact();
                var surnameError = errors.FirstOrDefault(e => e.Contains("Surname"));
                if (surnameError != null)
                {
                    errorProvider1.SetError(textBoxSurname, surnameError);
                }
            }
        }

        private void ValidatePhoneField()
        {
            errorProvider1.SetError(textBoxPhoneNumber, "");

            var tempContact = new Contact
            {
                Name = "Test", // Set dummy values to avoid other validation errors
                Surname = "Test",
                PhoneNumber = string.IsNullOrWhiteSpace(textBoxPhoneNumber.Text) ? string.Empty : textBoxPhoneNumber.Text.Trim()
            };
            var errors = tempContact.ValidateContact();
            var phoneError = errors.FirstOrDefault(e => e.Contains("Phone"));
            if (phoneError != null)
            {
                errorProvider1.SetError(textBoxPhoneNumber, phoneError);
            }
        }

        private void LoadContactData()
        {
            if (Contact != null)
            {
                textBoxName.Text = Contact.Name;
                textBoxSurname.Text = Contact.Surname;
                textBoxPhoneNumber.Text = Contact.PhoneNumber;
                checkBoxUsed.Checked = Contact.Used;
            }
        }

        private bool ValidateForm()
        {
            // Clear previous error states
            errorProvider1.Clear();

            bool isValid = true;

            // Create a temporary contact to validate
            var tempContact = new Contact
            {
                Name = textBoxName.Text?.Trim() ?? string.Empty,
                Surname = textBoxSurname.Text?.Trim() ?? string.Empty,
                PhoneNumber = textBoxPhoneNumber.Text?.Trim() ?? string.Empty,
                Used = checkBoxUsed.Checked
            };

            // Get validation errors from the contact model
            var validationErrors = tempContact.ValidateContact();

            // Set error indicators for each field
            foreach (var error in validationErrors)
            {
                if (error.Contains("Name"))
                {
                    errorProvider1.SetError(textBoxName, error);
                    isValid = false;
                }
                else if (error.Contains("Surname"))
                {
                    errorProvider1.SetError(textBoxSurname, error);
                    isValid = false;
                }
                else if (error.Contains("Phone"))
                {
                    errorProvider1.SetError(textBoxPhoneNumber, error);
                    isValid = false;
                }
            }

            // Additional UI-specific validations
            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                if (errorProvider1.GetError(textBoxName) == string.Empty)
                    errorProvider1.SetError(textBoxName, "Name is required");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(textBoxSurname.Text))
            {
                if (errorProvider1.GetError(textBoxSurname) == string.Empty)
                    errorProvider1.SetError(textBoxSurname, "Surname is required");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(textBoxPhoneNumber.Text))
            {
                if (errorProvider1.GetError(textBoxPhoneNumber) == string.Empty)
                    errorProvider1.SetError(textBoxPhoneNumber, "Phone number is required");
                isValid = false;
            }

            // Show summary message if there are validation errors
            if (!isValid)
            {
                MessageBox.Show("Please correct the validation errors before saving.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return isValid;
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                if (Contact != null)
                {
                    Contact.Name = textBoxName.Text.Trim();
                    Contact.Surname = textBoxSurname.Text.Trim();
                    Contact.PhoneNumber = textBoxPhoneNumber.Text?.Trim() ?? string.Empty;
                    Contact.Used = checkBoxUsed.Checked;

                    if (_isEditMode)
                    {
                        Contact.ModifiedDate = DateTime.Now;
                    }
                    else
                    {
                        Contact.CreatedDate = DateTime.Now;
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
