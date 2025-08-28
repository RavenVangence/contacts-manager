using Microsoft.EntityFrameworkCore;
using GeniusContactManager.Data;

namespace GeniusContactManager;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        // Ensure database is created
        try
        {
            using var context = new ContactContext(new DbContextOptionsBuilder<ContactContext>().Options);
            context.Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Database initialization failed: {ex.Message}", "Database Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        Application.Run(new Form1());
    }
}