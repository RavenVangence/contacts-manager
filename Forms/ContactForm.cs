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
        }

        public ContactForm(Contact contact)
        {
            InitializeComponent();
            _isEditMode = true;
            Contact = contact;
            this.Text = "Edit Contact";
            LoadContactData();
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

            // Validate Name
            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                errorProvider1.SetError(textBoxName, "Name is required");
                isValid = false;
            }

            // Validate Surname
            if (string.IsNullOrWhiteSpace(textBoxSurname.Text))
            {
                errorProvider1.SetError(textBoxSurname, "Surname is required");
                isValid = false;
            }

            // Validate Phone Number format (basic validation)
            if (!string.IsNullOrWhiteSpace(textBoxPhoneNumber.Text))
            {
                var phoneNumber = textBoxPhoneNumber.Text.Trim();
                if (phoneNumber.Length < 10 || phoneNumber.Length > 15)
                {
                    errorProvider1.SetError(textBoxPhoneNumber, "Phone number should be between 10-15 characters");
                    isValid = false;
                }
            }

            return isValid;
        }

        private void ButtonSave_Click(object? sender, EventArgs e)
        {
            if (ValidateForm())
            {
                if (Contact != null)
                {
                    Contact.Name = textBoxName.Text.Trim();
                    Contact.Surname = textBoxSurname.Text.Trim();
                    Contact.PhoneNumber = string.IsNullOrWhiteSpace(textBoxPhoneNumber.Text) ? null : textBoxPhoneNumber.Text.Trim();
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

        private void ButtonCancel_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
