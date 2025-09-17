namespace SchoolJournal
{
    partial class EditGradeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            label1 = new Label();
            cmbStudents = new ComboBox();
            label2 = new Label();
            cmbSubject = new ComboBox();
            label3 = new Label();
            numericGrade = new NumericUpDown();
            btnSave = new Button();
            btnCancel = new Button();
            dtpDate = new DateTimePicker();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)numericGrade).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 20);
            label1.Name = "label1";
            label1.Size = new Size(47, 13);
            label1.TabIndex = 0;
            label1.Text = "Студент";
            // 
            // cmbStudents
            // 
            cmbStudents.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStudents.FormattingEnabled = true;
            cmbStudents.Location = new Point(120, 17);
            cmbStudents.Name = "cmbStudents";
            cmbStudents.Size = new Size(200, 21);
            cmbStudents.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 50);
            label2.Name = "label2";
            label2.Size = new Size(52, 13);
            label2.TabIndex = 2;
            label2.Text = "Предмет";
            // 
            // cmbSubject
            // 
            cmbSubject.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSubject.FormattingEnabled = true;
            cmbSubject.Items.AddRange(new object[] { "Математика", "Русский язык", "Литература", "Окружающий мир", "Физкультура", "Музыка", "ИЗО" });
            cmbSubject.Location = new Point(120, 47);
            cmbSubject.Name = "cmbSubject";
            cmbSubject.Size = new Size(200, 21);
            cmbSubject.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 80);
            label3.Name = "label3";
            label3.Size = new Size(45, 13);
            label3.TabIndex = 4;
            label3.Text = "Оценка";
            // 
            // numericGrade
            // 
            numericGrade.Location = new Point(120, 78);
            numericGrade.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericGrade.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            numericGrade.Name = "numericGrade";
            numericGrade.Size = new Size(60, 20);
            numericGrade.TabIndex = 5;
            numericGrade.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // btnSave
            // 
            btnSave.Location = new Point(120, 140);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(80, 30);
            btnSave.TabIndex = 6;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(210, 140);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(80, 30);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // dtpDate
            // 
            dtpDate.Location = new Point(120, 105);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(200, 20);
            dtpDate.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 110);
            label4.Name = "label4";
            label4.Size = new Size(33, 13);
            label4.TabIndex = 9;
            label4.Text = "Дата";
            // 
            // EditGradeForm
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(350, 180);
            Controls.Add(label4);
            Controls.Add(dtpDate);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(numericGrade);
            Controls.Add(label3);
            Controls.Add(cmbSubject);
            Controls.Add(label2);
            Controls.Add(cmbStudents);
            Controls.Add(label1);
            Name = "EditGradeForm";
            Text = "Редактирование оценки";
            Load += EditGradeForm_Load;
            ((System.ComponentModel.ISupportInitialize)numericGrade).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbStudents;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSubject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericGrade;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label4;
    }
}