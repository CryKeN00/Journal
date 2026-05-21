namespace SchoolJournal
{
    partial class ParentForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblParentName;
        private System.Windows.Forms.Label lblSelectedChild;
        private System.Windows.Forms.Label lblAverageGrade;
        private System.Windows.Forms.ComboBox cmbChildren;
        private System.Windows.Forms.DataGridView dataGridViewGrades;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblNoChildren;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelBottom;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblParentName = new System.Windows.Forms.Label();
            this.lblSelectedChild = new System.Windows.Forms.Label();
            this.lblAverageGrade = new System.Windows.Forms.Label();
            this.cmbChildren = new System.Windows.Forms.ComboBox();
            this.dataGridViewGrades = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblNoChildren = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelBottom = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGrades)).BeginInit();
            this.panelTop.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();

            this.panelTop.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.panelTop.Controls.Add(this.lblParentName);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(984, 60);
            this.panelTop.TabIndex = 0;

            this.lblParentName.AutoSize = true;
            this.lblParentName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblParentName.ForeColor = System.Drawing.Color.White;
            this.lblParentName.Location = new System.Drawing.Point(20, 18);
            this.lblParentName.Name = "lblParentName";
            this.lblParentName.Size = new System.Drawing.Size(150, 25);
            this.lblParentName.TabIndex = 0;
            this.lblParentName.Text = "Добро пожаловать";

            this.lblSelectedChild.AutoSize = true;
            this.lblSelectedChild.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSelectedChild.Location = new System.Drawing.Point(20, 80);
            this.lblSelectedChild.Name = "lblSelectedChild";
            this.lblSelectedChild.Size = new System.Drawing.Size(120, 20);
            this.lblSelectedChild.TabIndex = 1;
            this.lblSelectedChild.Text = "Выберите ребенка";

            this.cmbChildren.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChildren.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbChildren.FormattingEnabled = true;
            this.cmbChildren.Location = new System.Drawing.Point(24, 105);
            this.cmbChildren.Name = "cmbChildren";
            this.cmbChildren.Size = new System.Drawing.Size(300, 25);
            this.cmbChildren.TabIndex = 2;
            this.cmbChildren.SelectedIndexChanged += new System.EventHandler(this.cmbChildren_SelectedIndexChanged);

            this.lblNoChildren.AutoSize = true;
            this.lblNoChildren.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNoChildren.ForeColor = System.Drawing.Color.Red;
            this.lblNoChildren.Location = new System.Drawing.Point(24, 110);
            this.lblNoChildren.Name = "lblNoChildren";
            this.lblNoChildren.Size = new System.Drawing.Size(0, 19);
            this.lblNoChildren.TabIndex = 3;
            this.lblNoChildren.Visible = false;

            this.lblAverageGrade.AutoSize = true;
            this.lblAverageGrade.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblAverageGrade.Location = new System.Drawing.Point(700, 105);
            this.lblAverageGrade.Name = "lblAverageGrade";
            this.lblAverageGrade.Size = new System.Drawing.Size(100, 21);
            this.lblAverageGrade.TabIndex = 4;
            this.lblAverageGrade.Text = "Средний балл:";

            this.dataGridViewGrades.AllowUserToAddRows = false;
            this.dataGridViewGrades.AllowUserToDeleteRows = false;
            this.dataGridViewGrades.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewGrades.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewGrades.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewGrades.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewGrades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewGrades.EnableHeadersVisualStyles = false;
            this.dataGridViewGrades.Location = new System.Drawing.Point(24, 150);
            this.dataGridViewGrades.Name = "dataGridViewGrades";
            this.dataGridViewGrades.ReadOnly = true;
            this.dataGridViewGrades.RowHeadersVisible = false;
            this.dataGridViewGrades.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewGrades.Size = new System.Drawing.Size(936, 420);
            this.dataGridViewGrades.TabIndex = 5;

            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.panelBottom.Controls.Add(this.btnRefresh);
            this.panelBottom.Controls.Add(this.btnLogout);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 600);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(984, 50);
            this.panelBottom.TabIndex = 6;

            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(24, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 32);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(840, 10);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(120, 32);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "Выход";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 650);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.dataGridViewGrades);
            this.Controls.Add(this.lblAverageGrade);
            this.Controls.Add(this.lblNoChildren);
            this.Controls.Add(this.cmbChildren);
            this.Controls.Add(this.lblSelectedChild);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ParentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Электронный журнал - Родитель";
            this.Load += new System.EventHandler(this.ParentForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGrades)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}