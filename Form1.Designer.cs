namespace GeniusContactManager;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private DataGridView dataGridViewContacts;
    private Button buttonAdd;
    private Button buttonEdit;
    private Button buttonDelete;
    private Button buttonImport;
    private Button buttonExport;
    private Button buttonSaveToDb;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem importToolStripMenuItem;
    private ToolStripMenuItem exportToolStripMenuItem;
    private ToolStripMenuItem saveToDbToolStripMenuItem;
    private ToolStripMenuItem exitToolStripMenuItem;
    private ToolStripMenuItem editToolStripMenuItem;
    private ToolStripMenuItem addContactToolStripMenuItem;
    private ToolStripMenuItem editContactToolStripMenuItem;
    private ToolStripMenuItem deleteContactToolStripMenuItem;
    private Panel panelButtons;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel toolStripStatusLabel1;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.dataGridViewContacts = new DataGridView();
        this.buttonAdd = new Button();
        this.buttonEdit = new Button();
        this.buttonDelete = new Button();
        this.buttonImport = new Button();
        this.buttonExport = new Button();
        this.buttonSaveToDb = new Button();
        this.menuStrip1 = new MenuStrip();
        this.fileToolStripMenuItem = new ToolStripMenuItem();
        this.importToolStripMenuItem = new ToolStripMenuItem();
        this.exportToolStripMenuItem = new ToolStripMenuItem();
        this.saveToDbToolStripMenuItem = new ToolStripMenuItem();
        this.exitToolStripMenuItem = new ToolStripMenuItem();
        this.editToolStripMenuItem = new ToolStripMenuItem();
        this.addContactToolStripMenuItem = new ToolStripMenuItem();
        this.editContactToolStripMenuItem = new ToolStripMenuItem();
        this.deleteContactToolStripMenuItem = new ToolStripMenuItem();
        this.panelButtons = new Panel();
        this.statusStrip1 = new StatusStrip();
        this.toolStripStatusLabel1 = new ToolStripStatusLabel();

        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewContacts)).BeginInit();
        this.menuStrip1.SuspendLayout();
        this.panelButtons.SuspendLayout();
        this.statusStrip1.SuspendLayout();
        this.SuspendLayout();

        // 
        // menuStrip1
        // 
        this.menuStrip1.Items.AddRange(new ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
        this.menuStrip1.Location = new Point(0, 0);
        this.menuStrip1.Name = "menuStrip1";
        this.menuStrip1.Size = new Size(1000, 24);
        this.menuStrip1.TabIndex = 0;
        this.menuStrip1.Text = "menuStrip1";

        // 
        // fileToolStripMenuItem
        // 
        this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.saveToDbToolStripMenuItem,
            this.exitToolStripMenuItem});
        this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        this.fileToolStripMenuItem.Size = new Size(37, 20);
        this.fileToolStripMenuItem.Text = "&File";

        // 
        // importToolStripMenuItem
        // 
        this.importToolStripMenuItem.Name = "importToolStripMenuItem";
        this.importToolStripMenuItem.Size = new Size(180, 22);
        this.importToolStripMenuItem.Text = "&Import from Excel";
        this.importToolStripMenuItem.Click += new EventHandler(this.ButtonImport_Click);

        // 
        // exportToolStripMenuItem
        // 
        this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
        this.exportToolStripMenuItem.Size = new Size(180, 22);
        this.exportToolStripMenuItem.Text = "&Export to Excel";
        this.exportToolStripMenuItem.Click += new EventHandler(this.ButtonExport_Click);

        // 
        // saveToDbToolStripMenuItem
        // 
        this.saveToDbToolStripMenuItem.Name = "saveToDbToolStripMenuItem";
        this.saveToDbToolStripMenuItem.Size = new Size(180, 22);
        this.saveToDbToolStripMenuItem.Text = "&Save to Database";
        this.saveToDbToolStripMenuItem.Click += new EventHandler(this.ButtonSaveToDb_Click);

        // 
        // exitToolStripMenuItem
        // 
        this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
        this.exitToolStripMenuItem.Size = new Size(180, 22);
        this.exitToolStripMenuItem.Text = "E&xit";
        this.exitToolStripMenuItem.Click += new EventHandler((s, e) => this.Close());

        // 
        // editToolStripMenuItem
        // 
        this.editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
        this.addContactToolStripMenuItem,
        this.editContactToolStripMenuItem,
        this.deleteContactToolStripMenuItem});
        this.editToolStripMenuItem.Name = "editToolStripMenuItem";
        this.editToolStripMenuItem.Size = new Size(39, 20);
        this.editToolStripMenuItem.Text = "&Edit";

        // 
        // addContactToolStripMenuItem
        // 
        this.addContactToolStripMenuItem.Name = "addContactToolStripMenuItem";
        this.addContactToolStripMenuItem.Size = new Size(180, 22);
        this.addContactToolStripMenuItem.Text = "&Add Contact";
        this.addContactToolStripMenuItem.Click += new EventHandler(this.ButtonAdd_Click);

        // 
        // editContactToolStripMenuItem
        // 
        this.editContactToolStripMenuItem.Name = "editContactToolStripMenuItem";
        this.editContactToolStripMenuItem.Size = new Size(180, 22);
        this.editContactToolStripMenuItem.Text = "&Edit Contact";
        this.editContactToolStripMenuItem.Click += new EventHandler(this.ButtonEdit_Click);

        // 
        // deleteContactToolStripMenuItem
        // 
        this.deleteContactToolStripMenuItem.Name = "deleteContactToolStripMenuItem";
        this.deleteContactToolStripMenuItem.Size = new Size(180, 22);
        this.deleteContactToolStripMenuItem.Text = "&Delete Contact";
        this.deleteContactToolStripMenuItem.Click += new EventHandler(this.ButtonDelete_Click);

        // 
        // panelButtons
        // 
        this.panelButtons.Controls.Add(this.buttonAdd);
        this.panelButtons.Controls.Add(this.buttonEdit);
        this.panelButtons.Controls.Add(this.buttonDelete);
        this.panelButtons.Controls.Add(this.buttonImport);
        this.panelButtons.Controls.Add(this.buttonExport);
        this.panelButtons.Controls.Add(this.buttonSaveToDb);
        this.panelButtons.Dock = DockStyle.Top;
        this.panelButtons.Location = new Point(0, 24);
        this.panelButtons.Name = "panelButtons";
        this.panelButtons.Size = new Size(1000, 50);
        this.panelButtons.TabIndex = 1;

        // 
        // buttonAdd
        // 
        this.buttonAdd.Location = new Point(12, 12);
        this.buttonAdd.Name = "buttonAdd";
        this.buttonAdd.Size = new Size(100, 30);
        this.buttonAdd.TabIndex = 0;
        this.buttonAdd.Text = "&Add Contact";
        this.buttonAdd.UseVisualStyleBackColor = true;
        this.buttonAdd.Click += new EventHandler(this.ButtonAdd_Click);

        // 
        // buttonEdit
        // 
        this.buttonEdit.Location = new Point(118, 12);
        this.buttonEdit.Name = "buttonEdit";
        this.buttonEdit.Size = new Size(100, 30);
        this.buttonEdit.TabIndex = 1;
        this.buttonEdit.Text = "&Edit Contact";
        this.buttonEdit.UseVisualStyleBackColor = true;
        this.buttonEdit.Click += new EventHandler(this.ButtonEdit_Click);

        // 
        // buttonDelete
        // 
        this.buttonDelete.Location = new Point(224, 12);
        this.buttonDelete.Name = "buttonDelete";
        this.buttonDelete.Size = new Size(120, 30);  // Increased width to 120
        this.buttonDelete.TabIndex = 2;
        this.buttonDelete.Text = "&Delete Contact";
        this.buttonDelete.UseVisualStyleBackColor = true;
        this.buttonDelete.Click += new EventHandler(this.ButtonDelete_Click);

        // 
        // buttonImport
        // 
        this.buttonImport.Location = new Point(370, 12);  // Adjusted position to account for wider Delete button
        this.buttonImport.Name = "buttonImport";
        this.buttonImport.Size = new Size(100, 30);
        this.buttonImport.TabIndex = 3;
        this.buttonImport.Text = "&Import Excel";
        this.buttonImport.UseVisualStyleBackColor = true;
        this.buttonImport.Click += new EventHandler(this.ButtonImport_Click);

        // 
        // buttonExport
        // 
        this.buttonExport.Location = new Point(476, 12);  // Adjusted position to account for wider Delete button
        this.buttonExport.Name = "buttonExport";
        this.buttonExport.Size = new Size(100, 30);
        this.buttonExport.TabIndex = 4;
        this.buttonExport.Text = "E&xport Excel";
        this.buttonExport.UseVisualStyleBackColor = true;
        this.buttonExport.Click += new EventHandler(this.ButtonExport_Click);

        // 
        // buttonSaveToDb
        // 
        this.buttonSaveToDb.Location = new Point(582, 12);
        this.buttonSaveToDb.Name = "buttonSaveToDb";
        this.buttonSaveToDb.Size = new Size(120, 30);
        this.buttonSaveToDb.TabIndex = 5;
        this.buttonSaveToDb.Text = "&Save to Database";
        this.buttonSaveToDb.UseVisualStyleBackColor = true;
        this.buttonSaveToDb.Click += new EventHandler(this.ButtonSaveToDb_Click);

        // 
        // dataGridViewContacts
        // 
        this.dataGridViewContacts.AllowUserToAddRows = false;
        this.dataGridViewContacts.AllowUserToDeleteRows = false;
        this.dataGridViewContacts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        this.dataGridViewContacts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridViewContacts.Dock = DockStyle.Fill;
        this.dataGridViewContacts.Location = new Point(0, 74);
        this.dataGridViewContacts.MultiSelect = false;
        this.dataGridViewContacts.Name = "dataGridViewContacts";
        this.dataGridViewContacts.EditMode = DataGridViewEditMode.EditOnEnter;
        this.dataGridViewContacts.RowHeadersWidth = 25;
        this.dataGridViewContacts.SelectionMode = DataGridViewSelectionMode.CellSelect;
        this.dataGridViewContacts.Size = new Size(1000, 584);
        this.dataGridViewContacts.TabIndex = 2;
        this.dataGridViewContacts.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(this.DataGridViewContacts_ColumnHeaderMouseClick);
        this.dataGridViewContacts.DoubleClick += new EventHandler(this.ButtonEdit_Click);

        // 
        // statusStrip1
        // 
        this.statusStrip1.Items.AddRange(new ToolStripItem[] {
            this.toolStripStatusLabel1});
        this.statusStrip1.Location = new Point(0, 636);
        this.statusStrip1.Name = "statusStrip1";
        this.statusStrip1.Size = new Size(1000, 22);
        this.statusStrip1.TabIndex = 3;
        this.statusStrip1.Text = "statusStrip1";

        // 
        // toolStripStatusLabel1
        // 
        this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
        this.toolStripStatusLabel1.Size = new Size(39, 17);
        this.toolStripStatusLabel1.Text = "Ready";

        // 
        // Form1
        // 
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(1000, 658);
        this.Controls.Add(this.dataGridViewContacts);
        this.Controls.Add(this.panelButtons);
        this.Controls.Add(this.menuStrip1);
        this.Controls.Add(this.statusStrip1);
        this.MainMenuStrip = this.menuStrip1;
        this.Name = "Form1";
        this.Text = "Genius Contact Manager";
        this.WindowState = FormWindowState.Maximized;

        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewContacts)).EndInit();
        this.menuStrip1.ResumeLayout(false);
        this.menuStrip1.PerformLayout();
        this.panelButtons.ResumeLayout(false);
        this.statusStrip1.ResumeLayout(false);
        this.statusStrip1.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion
}
