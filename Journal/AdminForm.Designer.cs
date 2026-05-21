namespace SchoolJournal
{
    partial class AdminForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblAdminName;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageStudents;
        private System.Windows.Forms.TabPage tabPageTeachers;
        private System.Windows.Forms.TabPage tabPageParents;
        private System.Windows.Forms.DataGridView dataGridViewStudents;
        private System.Windows.Forms.DataGridView dataGridViewTeachers;
        private System.Windows.Forms.DataGridView dataGridViewParents;
        private System.Windows.Forms.Button btnAddStudent;
        private System.Windows.Forms.Button btnEditStudent;
        private System.Windows.Forms.Button btnDeleteStudent;
        private System.Windows.Forms.Button btnAddTeacher;
        private System.Windows.Forms.Button btnEditTeacher;
        private System.Windows.Forms.Button btnDeleteTeacher;
        private System.Windows.Forms.Button btnManageSubjects;
        private System.Windows.Forms.Button btnAddParent;
        private System.Windows.Forms.Button btnEditParent;
        private System.Windows.Forms.Button btnDeleteParent;
        private System.Windows.Forms.Button btnLinkChildren;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnLogout;
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblAdminName = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageStudents = new System.Windows.Forms.TabPage();
            this.tabPageTeachers = new System.Windows.Forms.TabPage();
            this.tabPageParents = new System.Windows.Forms.TabPage();
            this.dataGridViewStudents = new System.Windows.Forms.DataGridView();
            this.dataGridViewTeachers = new System.Windows.Forms.DataGridView();
            this.dataGridViewParents = new System.Windows.Forms.DataGridView();
            this.btnAddStudent = new System.Windows.Forms.Button();
            this.btnEditStudent = new System.Windows.Forms.Button();
            this.btnDeleteStudent = new System.Windows.Forms.Button();
            this.btnAddTeacher = new System.Windows.Forms.Button();
            this.btnEditTeacher = new System.Windows.Forms.Button();
            this.btnDeleteTeacher = new System.Windows.Forms.Button();
            this.btnManageSubjects = new System.Windows.Forms.Button();
            this.btnAddParent = new System.Windows.Forms.Button();
            this.btnEditParent = new System.Windows.Forms.Button();
            this.btnDeleteParent = new System.Windows.Forms.Button();
            this.btnLinkChildren = new System.Windows.Forms.Button();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();

            this.panelTop.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageStudents.SuspendLayout();
            this.tabPageTeachers.SuspendLayout();
            this.tabPageParents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTeachers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParents)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();

            // panelTop
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.panelTop.Controls.Add(this.lblAdminName);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1000, 60);
            this.panelTop.TabIndex = 0;

            // lblAdminName
            this.lblAdminName.AutoSize = true;
            this.lblAdminName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblAdminName.ForeColor = System.Drawing.Color.White;
            this.lblAdminName.Location = new System.Drawing.Point(20, 18);
            this.lblAdminName.Name = "lblAdminName";
            this.lblAdminName.Size = new System.Drawing.Size(190, 25);
            this.lblAdminName.TabIndex = 0;
            this.lblAdminName.Text = "Панель администратора";

            // tabControl
            this.tabControl.Controls.Add(this.tabPageStudents);
            this.tabControl.Controls.Add(this.tabPageTeachers);
            this.tabControl.Controls.Add(this.tabPageParents);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabControl.Location = new System.Drawing.Point(0, 60);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1000, 500);
            this.tabControl.TabIndex = 1;

            // tabPageStudents
            this.tabPageStudents.Controls.Add(this.dataGridViewStudents);
            this.tabPageStudents.Controls.Add(this.btnAddStudent);
            this.tabPageStudents.Controls.Add(this.btnEditStudent);
            this.tabPageStudents.Controls.Add(this.btnDeleteStudent);
            this.tabPageStudents.Location = new System.Drawing.Point(4, 26);
            this.tabPageStudents.Name = "tabPageStudents";
            this.tabPageStudents.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStudents.Size = new System.Drawing.Size(992, 470);
            this.tabPageStudents.TabIndex = 0;
            this.tabPageStudents.Text = "Студенты";
            this.tabPageStudents.UseVisualStyleBackColor = true;

            // dataGridViewStudents
            this.dataGridViewStudents.AllowUserToAddRows = false;
            this.dataGridViewStudents.AllowUserToDeleteRows = false;
            this.dataGridViewStudents.AutoGenerateColumns = true;
            this.dataGridViewStudents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewStudents.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewStudents.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStudents.EnableHeadersVisualStyles = false;
            this.dataGridViewStudents.Location = new System.Drawing.Point(10, 50);
            this.dataGridViewStudents.Name = "dataGridViewStudents";
            this.dataGridViewStudents.ReadOnly = true;
            this.dataGridViewStudents.RowHeadersVisible = false;
            this.dataGridViewStudents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewStudents.Size = new System.Drawing.Size(972, 410);
            this.dataGridViewStudents.TabIndex = 0;

            // btnAddStudent
            this.btnAddStudent.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnAddStudent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddStudent.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddStudent.ForeColor = System.Drawing.Color.White;
            this.btnAddStudent.Location = new System.Drawing.Point(10, 10);
            this.btnAddStudent.Name = "btnAddStudent";
            this.btnAddStudent.Size = new System.Drawing.Size(100, 30);
            this.btnAddStudent.TabIndex = 1;
            this.btnAddStudent.Text = "Добавить";
            this.btnAddStudent.UseVisualStyleBackColor = false;

            // btnEditStudent
            this.btnEditStudent.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnEditStudent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditStudent.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEditStudent.ForeColor = System.Drawing.Color.White;
            this.btnEditStudent.Location = new System.Drawing.Point(120, 10);
            this.btnEditStudent.Name = "btnEditStudent";
            this.btnEditStudent.Size = new System.Drawing.Size(100, 30);
            this.btnEditStudent.TabIndex = 2;
            this.btnEditStudent.Text = "Редактировать";
            this.btnEditStudent.UseVisualStyleBackColor = false;

            // btnDeleteStudent
            this.btnDeleteStudent.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDeleteStudent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteStudent.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDeleteStudent.ForeColor = System.Drawing.Color.White;
            this.btnDeleteStudent.Location = new System.Drawing.Point(230, 10);
            this.btnDeleteStudent.Name = "btnDeleteStudent";
            this.btnDeleteStudent.Size = new System.Drawing.Size(100, 30);
            this.btnDeleteStudent.TabIndex = 3;
            this.btnDeleteStudent.Text = "Удалить";
            this.btnDeleteStudent.UseVisualStyleBackColor = false;

            // tabPageTeachers
            this.tabPageTeachers.Controls.Add(this.dataGridViewTeachers);
            this.tabPageTeachers.Controls.Add(this.btnAddTeacher);
            this.tabPageTeachers.Controls.Add(this.btnEditTeacher);
            this.tabPageTeachers.Controls.Add(this.btnDeleteTeacher);
            this.tabPageTeachers.Controls.Add(this.btnManageSubjects);
            this.tabPageTeachers.Location = new System.Drawing.Point(4, 26);
            this.tabPageTeachers.Name = "tabPageTeachers";
            this.tabPageTeachers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTeachers.Size = new System.Drawing.Size(992, 470);
            this.tabPageTeachers.TabIndex = 1;
            this.tabPageTeachers.Text = "Преподаватели";
            this.tabPageTeachers.UseVisualStyleBackColor = true;

            // dataGridViewTeachers
            this.dataGridViewTeachers.AllowUserToAddRows = false;
            this.dataGridViewTeachers.AllowUserToDeleteRows = false;
            this.dataGridViewTeachers.AutoGenerateColumns = true;
            this.dataGridViewTeachers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTeachers.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewTeachers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewTeachers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTeachers.EnableHeadersVisualStyles = false;
            this.dataGridViewTeachers.Location = new System.Drawing.Point(10, 50);
            this.dataGridViewTeachers.Name = "dataGridViewTeachers";
            this.dataGridViewTeachers.ReadOnly = true;
            this.dataGridViewTeachers.RowHeadersVisible = false;
            this.dataGridViewTeachers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTeachers.Size = new System.Drawing.Size(972, 410);
            this.dataGridViewTeachers.TabIndex = 4;

            // btnAddTeacher
            this.btnAddTeacher.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnAddTeacher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddTeacher.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddTeacher.ForeColor = System.Drawing.Color.White;
            this.btnAddTeacher.Location = new System.Drawing.Point(10, 10);
            this.btnAddTeacher.Name = "btnAddTeacher";
            this.btnAddTeacher.Size = new System.Drawing.Size(100, 30);
            this.btnAddTeacher.TabIndex = 1;
            this.btnAddTeacher.Text = "Добавить";
            this.btnAddTeacher.UseVisualStyleBackColor = false;

            // btnEditTeacher
            this.btnEditTeacher.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnEditTeacher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditTeacher.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEditTeacher.ForeColor = System.Drawing.Color.White;
            this.btnEditTeacher.Location = new System.Drawing.Point(120, 10);
            this.btnEditTeacher.Name = "btnEditTeacher";
            this.btnEditTeacher.Size = new System.Drawing.Size(100, 30);
            this.btnEditTeacher.TabIndex = 2;
            this.btnEditTeacher.Text = "Редактировать";
            this.btnEditTeacher.UseVisualStyleBackColor = false;

            // btnDeleteTeacher
            this.btnDeleteTeacher.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDeleteTeacher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteTeacher.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDeleteTeacher.ForeColor = System.Drawing.Color.White;
            this.btnDeleteTeacher.Location = new System.Drawing.Point(230, 10);
            this.btnDeleteTeacher.Name = "btnDeleteTeacher";
            this.btnDeleteTeacher.Size = new System.Drawing.Size(100, 30);
            this.btnDeleteTeacher.TabIndex = 3;
            this.btnDeleteTeacher.Text = "Удалить";
            this.btnDeleteTeacher.UseVisualStyleBackColor = false;

            // btnManageSubjects
            this.btnManageSubjects.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            this.btnManageSubjects.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageSubjects.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnManageSubjects.ForeColor = System.Drawing.Color.White;
            this.btnManageSubjects.Location = new System.Drawing.Point(340, 10);
            this.btnManageSubjects.Name = "btnManageSubjects";
            this.btnManageSubjects.Size = new System.Drawing.Size(150, 30);
            this.btnManageSubjects.TabIndex = 4;
            this.btnManageSubjects.Text = "Предметы учителя";
            this.btnManageSubjects.UseVisualStyleBackColor = false;

            // tabPageParents
            this.tabPageParents.Controls.Add(this.dataGridViewParents);
            this.tabPageParents.Controls.Add(this.btnAddParent);
            this.tabPageParents.Controls.Add(this.btnEditParent);
            this.tabPageParents.Controls.Add(this.btnDeleteParent);
            this.tabPageParents.Controls.Add(this.btnLinkChildren);
            this.tabPageParents.Location = new System.Drawing.Point(4, 26);
            this.tabPageParents.Name = "tabPageParents";
            this.tabPageParents.Size = new System.Drawing.Size(992, 470);
            this.tabPageParents.TabIndex = 2;
            this.tabPageParents.Text = "Родители";
            this.tabPageParents.UseVisualStyleBackColor = true;

            // dataGridViewParents
            this.dataGridViewParents.AllowUserToAddRows = false;
            this.dataGridViewParents.AllowUserToDeleteRows = false;
            this.dataGridViewParents.AutoGenerateColumns = true;
            this.dataGridViewParents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewParents.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewParents.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewParents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewParents.EnableHeadersVisualStyles = false;
            this.dataGridViewParents.Location = new System.Drawing.Point(10, 50);
            this.dataGridViewParents.Name = "dataGridViewParents";
            this.dataGridViewParents.ReadOnly = true;
            this.dataGridViewParents.RowHeadersVisible = false;
            this.dataGridViewParents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewParents.Size = new System.Drawing.Size(972, 410);
            this.dataGridViewParents.TabIndex = 5;

            // btnAddParent
            this.btnAddParent.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnAddParent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddParent.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddParent.ForeColor = System.Drawing.Color.White;
            this.btnAddParent.Location = new System.Drawing.Point(10, 10);
            this.btnAddParent.Name = "btnAddParent";
            this.btnAddParent.Size = new System.Drawing.Size(100, 30);
            this.btnAddParent.TabIndex = 1;
            this.btnAddParent.Text = "Добавить";
            this.btnAddParent.UseVisualStyleBackColor = false;

            // btnEditParent
            this.btnEditParent.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnEditParent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditParent.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEditParent.ForeColor = System.Drawing.Color.White;
            this.btnEditParent.Location = new System.Drawing.Point(120, 10);
            this.btnEditParent.Name = "btnEditParent";
            this.btnEditParent.Size = new System.Drawing.Size(100, 30);
            this.btnEditParent.TabIndex = 2;
            this.btnEditParent.Text = "Редактировать";
            this.btnEditParent.UseVisualStyleBackColor = false;

            // btnDeleteParent
            this.btnDeleteParent.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDeleteParent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteParent.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDeleteParent.ForeColor = System.Drawing.Color.White;
            this.btnDeleteParent.Location = new System.Drawing.Point(230, 10);
            this.btnDeleteParent.Name = "btnDeleteParent";
            this.btnDeleteParent.Size = new System.Drawing.Size(100, 30);
            this.btnDeleteParent.TabIndex = 3;
            this.btnDeleteParent.Text = "Удалить";
            this.btnDeleteParent.UseVisualStyleBackColor = false;

            // btnLinkChildren
            this.btnLinkChildren.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            this.btnLinkChildren.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLinkChildren.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLinkChildren.ForeColor = System.Drawing.Color.White;
            this.btnLinkChildren.Location = new System.Drawing.Point(340, 10);
            this.btnLinkChildren.Name = "btnLinkChildren";
            this.btnLinkChildren.Size = new System.Drawing.Size(150, 30);
            this.btnLinkChildren.TabIndex = 4;
            this.btnLinkChildren.Text = "Привязать детей";
            this.btnLinkChildren.UseVisualStyleBackColor = false;

            // panelBottom
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.panelBottom.Controls.Add(this.btnRefresh);
            this.panelBottom.Controls.Add(this.btnLogout);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 560);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1000, 50);
            this.panelBottom.TabIndex = 2;

            // btnRefresh
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(20, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 32);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.UseVisualStyleBackColor = false;

            // btnLogout
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(860, 10);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(120, 32);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "Выход";
            this.btnLogout.UseVisualStyleBackColor = false;

            // AdminForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 610);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Электронный журнал - Администратор";
            this.Load += new System.EventHandler(this.AdminForm_Load);

            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPageStudents.ResumeLayout(false);
            this.tabPageTeachers.ResumeLayout(false);
            this.tabPageParents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTeachers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParents)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}