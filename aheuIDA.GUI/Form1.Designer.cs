namespace aheuIDA.GUI
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtStorage = new System.Windows.Forms.TextBox();
            this.btnStep = new System.Windows.Forms.Button();
            this.txtOut = new System.Windows.Forms.TextBox();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(12, 134);
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCode.Size = new System.Drawing.Size(708, 275);
            this.txtCode.TabIndex = 0;
            // 
            // txtStorage
            // 
            this.txtStorage.Location = new System.Drawing.Point(726, 12);
            this.txtStorage.Multiline = true;
            this.txtStorage.Name = "txtStorage";
            this.txtStorage.ReadOnly = true;
            this.txtStorage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStorage.Size = new System.Drawing.Size(310, 675);
            this.txtStorage.TabIndex = 0;
            // 
            // btnStep
            // 
            this.btnStep.Location = new System.Drawing.Point(12, 415);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(180, 90);
            this.btnStep.TabIndex = 1;
            this.btnStep.Text = "한단계";
            this.btnStep.UseVisualStyleBackColor = true;
            this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
            // 
            // txtOut
            // 
            this.txtOut.Location = new System.Drawing.Point(12, 511);
            this.txtOut.Multiline = true;
            this.txtOut.Name = "txtOut";
            this.txtOut.ReadOnly = true;
            this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOut.Size = new System.Drawing.Size(708, 176);
            this.txtOut.TabIndex = 2;
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(12, 12);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtInput.Size = new System.Drawing.Size(708, 116);
            this.txtInput.TabIndex = 3;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(198, 415);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(180, 90);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "초기화";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(384, 415);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(180, 90);
            this.btnRun.TabIndex = 1;
            this.btnRun.Text = "실행";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 703);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.txtOut);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.btnStep);
            this.Controls.Add(this.txtStorage);
            this.Controls.Add(this.txtCode);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtStorage;
        private System.Windows.Forms.Button btnStep;
        private System.Windows.Forms.TextBox txtOut;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnRun;
    }
}

