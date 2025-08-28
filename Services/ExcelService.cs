using GeniusContactManager.Models;
using ClosedXML.Excel;

namespace GeniusContactManager.Services
{
    public class ExcelService
    {
        public List<Contact> ImportFromExcel(string filePath)
        {
            var contacts = new List<Contact>();

            using var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheet(1); // First worksheet

            if (worksheet == null)
                throw new InvalidOperationException("No worksheets found in the Excel file.");

            // Get the range of data
            var rowCount = worksheet.LastRowUsed()?.RowNumber() ?? 0;

            if (rowCount <= 1) // No data rows (only header)
                return contacts;

            // Read data starting from row 2 (assuming row 1 has headers)
            for (int row = 2; row <= rowCount; row++)
            {
                // Map columns based on simplified structure
                var name = worksheet.Cell(row, 1).Value.ToString()?.Trim();
                var surname = worksheet.Cell(row, 2).Value.ToString()?.Trim();
                var phoneNumber = worksheet.Cell(row, 3).Value.ToString()?.Trim();
                var usedValue = worksheet.Cell(row, 4).Value.ToString()?.Trim();

                // Skip empty rows
                if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(surname))
                    continue;

                var contact = new Contact
                {
                    Name = name ?? "",
                    Surname = surname ?? "",
                    PhoneNumber = phoneNumber,
                    Used = ParseBooleanValue(usedValue),
                    CreatedDate = DateTime.Now
                };

                contacts.Add(contact);
            }

            return contacts;
        }

        public void ExportToExcel(List<Contact> contacts, string filePath)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Contacts");

            // Add headers for simplified model
            worksheet.Cell(1, 1).Value = "Name";
            worksheet.Cell(1, 2).Value = "Surname";
            worksheet.Cell(1, 3).Value = "Phone Number";
            worksheet.Cell(1, 4).Value = "Used";
            worksheet.Cell(1, 5).Value = "Created Date";
            worksheet.Cell(1, 6).Value = "Modified Date";

            // Format header row
            var headerRange = worksheet.Range(1, 1, 1, 6);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

            // Add data
            for (int i = 0; i < contacts.Count; i++)
            {
                var row = i + 2;
                var contact = contacts[i];

                worksheet.Cell(row, 1).Value = contact.Name;
                worksheet.Cell(row, 2).Value = contact.Surname;
                worksheet.Cell(row, 3).Value = contact.PhoneNumber;
                worksheet.Cell(row, 4).Value = contact.Used;
                worksheet.Cell(row, 5).Value = contact.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss");
                worksheet.Cell(row, 6).Value = contact.ModifiedDate?.ToString("yyyy-MM-dd HH:mm:ss");
            }

            // Auto-fit columns
            worksheet.ColumnsUsed().AdjustToContents();

            // Save the file
            workbook.SaveAs(filePath);
        }

        private bool ParseBooleanValue(string? value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            value = value.ToLower().Trim();

            // Handle various boolean representations
            return value switch
            {
                "true" or "1" or "yes" or "y" or "on" or "enabled" => true,
                "false" or "0" or "no" or "n" or "off" or "disabled" => false,
                _ => false
            };
        }

        public List<Contact> ImportFromSaContacts(string filePath)
        {
            // Special method for importing from sa_contacts.xlsx with specific format
            var contacts = new List<Contact>();

            using var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheet(1);

            if (worksheet == null)
                throw new InvalidOperationException("No worksheets found in sa_contacts.xlsx file.");

            var rowCount = worksheet.LastRowUsed()?.RowNumber() ?? 0;

            if (rowCount <= 1)
                return contacts;

            // Import from sa_contacts.xlsx format (simplified)
            for (int row = 2; row <= rowCount; row++)
            {
                var name = worksheet.Cell(row, 1).Value.ToString()?.Trim();
                var surname = worksheet.Cell(row, 2).Value.ToString()?.Trim();
                var phoneNumber = worksheet.Cell(row, 3).Value.ToString()?.Trim();
                var usedValue = worksheet.Cell(row, 4).Value.ToString()?.Trim();

                if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(surname))
                    continue;

                var contact = new Contact
                {
                    Name = name ?? "",
                    Surname = surname ?? "",
                    PhoneNumber = phoneNumber,
                    Used = ParseBooleanValue(usedValue),
                    CreatedDate = DateTime.Now
                };

                contacts.Add(contact);
            }

            return contacts;
        }
    }
}
