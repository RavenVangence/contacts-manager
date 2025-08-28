namespace GeniusContactManager.Forms
{
    partial class ContactForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Label labelName;
        private Label labelSurname;
        private Label labelPhoneNumber;
        private Label labelUsed;
        private TextBox textBoxName;
        private TextBox textBoxSurname;
        private TextBox textBoxPhoneNumber;
        private CheckBox checkBoxUsed;
        private Button buttonSave;
        private Button buttonCancel;
        private ErrorProvider errorProvider1;
        private TableLayoutPanel tableLayoutPanel1;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.labelName = new Label();
            this.labelSurname = new Label();
            this.labelPhoneNumber = new Label();
            this.labelUsed = new Label();
            this.textBoxName = new TextBox();
            this.textBoxSurname = new TextBox();
            this.textBoxPhoneNumber = new TextBox();
            this.checkBoxUsed = new CheckBox();
            this.buttonSave = new Button();
            this.buttonCancel = new Button();
            this.errorProvider1 = new ErrorProvider(this.components);
            this.tableLayoutPanel1 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();

            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.labelName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelSurname, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxSurname, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelPhoneNumber, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxPhoneNumber, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelUsed, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxUsed, 1, 3);
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Location = new Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new Padding(20, 20, 20, 80);
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new Size(400, 250);
            this.tableLayoutPanel1.TabIndex = 0;

            // 
            // labelName
            // 
            this.labelName.Anchor = AnchorStyles.Left;
            this.labelName.AutoSize = true;
            this.labelName.Location = new Point(23, 28);
            this.labelName.Name = "labelName";
            this.labelName.Size = new Size(42, 15);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Name:";

            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.textBoxName.Location = new Point(143, 24);
            this.textBoxName.MaxLength = 50;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new Size(234, 23);
            this.textBoxName.TabIndex = 1;

            // 
            // labelSurname
            // 
            this.labelSurname.Anchor = AnchorStyles.Left;
            this.labelSurname.AutoSize = true;
            this.labelSurname.Location = new Point(23, 63);
            this.labelSurname.Name = "labelSurname";
            this.labelSurname.Size = new Size(57, 15);
            this.labelSurname.TabIndex = 2;
            this.labelSurname.Text = "Surname:";

            // 
            // textBoxSurname
            // 
            this.textBoxSurname.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.textBoxSurname.Location = new Point(143, 59);
            this.textBoxSurname.MaxLength = 50;
            this.textBoxSurname.Name = "textBoxSurname";
            this.textBoxSurname.Size = new Size(234, 23);
            this.textBoxSurname.TabIndex = 3;

            // 
            // labelPhoneNumber
            // 
            this.labelPhoneNumber.Anchor = AnchorStyles.Left;
            this.labelPhoneNumber.AutoSize = true;
            this.labelPhoneNumber.Location = new Point(23, 98);
            this.labelPhoneNumber.Name = "labelPhoneNumber";
            this.labelPhoneNumber.Size = new Size(91, 15);
            this.labelPhoneNumber.TabIndex = 4;
            this.labelPhoneNumber.Text = "Phone Number:";

            // 
            // textBoxPhoneNumber
            // 
            this.textBoxPhoneNumber.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.textBoxPhoneNumber.Location = new Point(143, 94);
            this.textBoxPhoneNumber.MaxLength = 15;
            this.textBoxPhoneNumber.Name = "textBoxPhoneNumber";
            this.textBoxPhoneNumber.Size = new Size(234, 23);
            this.textBoxPhoneNumber.TabIndex = 5;

            // 
            // labelUsed
            // 
            this.labelUsed.Anchor = AnchorStyles.Left;
            this.labelUsed.AutoSize = true;
            this.labelUsed.Location = new Point(23, 133);
            this.labelUsed.Name = "labelUsed";
            this.labelUsed.Size = new Size(37, 15);
            this.labelUsed.TabIndex = 6;
            this.labelUsed.Text = "Used:";

            // 
            // checkBoxUsed
            // 
            this.checkBoxUsed.Anchor = AnchorStyles.Left;
            this.checkBoxUsed.AutoSize = true;
            this.checkBoxUsed.Location = new Point(143, 132);
            this.checkBoxUsed.Name = "checkBoxUsed";
            this.checkBoxUsed.Size = new Size(90, 19);
            this.checkBoxUsed.TabIndex = 7;
            this.checkBoxUsed.Text = "Contact used";
            this.checkBoxUsed.UseVisualStyleBackColor = true;

            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.buttonSave.Location = new Point(225, 215);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new Size(75, 23);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "&Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new EventHandler(this.ButtonSave_Click);

            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.buttonCancel.DialogResult = DialogResult.Cancel;
            this.buttonCancel.Location = new Point(306, 215);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new Size(75, 23);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new EventHandler(this.ButtonCancel_Click);

            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;

            // 
            // ContactForm
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new Size(400, 250);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ContactForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Contact Form";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion
    }
}