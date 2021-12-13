namespace AIDELInformationManagementSystem
{
    partial class TitleForm
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
            this.btn_courseAndStudentInfo = new System.Windows.Forms.Button();
            this.btn_nonInstitutionalExamInfo = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_courseAndStudentInfo
            // 
            this.btn_courseAndStudentInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_courseAndStudentInfo.Location = new System.Drawing.Point(220, 324);
            this.btn_courseAndStudentInfo.Name = "btn_courseAndStudentInfo";
            this.btn_courseAndStudentInfo.Size = new System.Drawing.Size(240, 85);
            this.btn_courseAndStudentInfo.TabIndex = 2;
            this.btn_courseAndStudentInfo.Text = "Course and Student Info";
            this.btn_courseAndStudentInfo.UseVisualStyleBackColor = true;
            this.btn_courseAndStudentInfo.Click += new System.EventHandler(this.btn_courseAndStudentInfo_Click);
            // 
            // btn_nonInstitutionalExamInfo
            // 
            this.btn_nonInstitutionalExamInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_nonInstitutionalExamInfo.Location = new System.Drawing.Point(820, 324);
            this.btn_nonInstitutionalExamInfo.Name = "btn_nonInstitutionalExamInfo";
            this.btn_nonInstitutionalExamInfo.Size = new System.Drawing.Size(240, 85);
            this.btn_nonInstitutionalExamInfo.TabIndex = 3;
            this.btn_nonInstitutionalExamInfo.Text = "Non-institutional Exam Info";
            this.btn_nonInstitutionalExamInfo.UseVisualStyleBackColor = true;
            this.btn_nonInstitutionalExamInfo.Click += new System.EventHandler(this.btn_nonInstitutionalExamInfo_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AIDELInformationManagementSystem.Properties.Resources.AIDELLogo;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(354, 74);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // TitleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.btn_nonInstitutionalExamInfo);
            this.Controls.Add(this.btn_courseAndStudentInfo);
            this.Controls.Add(this.pictureBox1);
            this.Name = "TitleForm";
            this.Text = "Title";
            this.Load += new System.EventHandler(this.TitleForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_courseAndStudentInfo;
        private System.Windows.Forms.Button btn_nonInstitutionalExamInfo;
    }
}