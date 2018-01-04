﻿namespace EmotionalAppraisalWF
{
    partial class AddOrEditAppraisalRuleForm
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
            this.components = new System.ComponentModel.Container();
            this.addOrEditButton = new System.Windows.Forms.Button();
            this.addBeliefErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.emotionalAppraisalAssetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.comboBoxEventType = new System.Windows.Forms.ComboBox();
            this.textBoxTarget = new System.Windows.Forms.TextBox();
            this.labelTarget = new System.Windows.Forms.Label();
            this.textBoxObject = new System.Windows.Forms.TextBox();
            this.labelObject = new System.Windows.Forms.Label();
            this.textBoxSubject = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxDesirability = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPraiseworthiness = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.addBeliefErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emotionalAppraisalAssetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // addOrEditButton
            // 
            this.addOrEditButton.Location = new System.Drawing.Point(155, 540);
            this.addOrEditButton.Margin = new System.Windows.Forms.Padding(4);
            this.addOrEditButton.Name = "addOrEditButton";
            this.addOrEditButton.Size = new System.Drawing.Size(100, 28);
            this.addOrEditButton.TabIndex = 20;
            this.addOrEditButton.Text = "Add";
            this.addOrEditButton.UseVisualStyleBackColor = true;
            this.addOrEditButton.Click += new System.EventHandler(this.addOrEditButton_Click_1);
            // 
            // addBeliefErrorProvider
            // 
            this.addBeliefErrorProvider.ContainerControl = this;
            // 
            // emotionalAppraisalAssetBindingSource
            // 
            this.emotionalAppraisalAssetBindingSource.DataSource = typeof(EmotionalAppraisal.EmotionalAppraisalAsset);
            // 
            // comboBoxEventType
            // 
            this.comboBoxEventType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEventType.FormattingEnabled = true;
            this.comboBoxEventType.Location = new System.Drawing.Point(43, 60);
            this.comboBoxEventType.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxEventType.Name = "comboBoxEventType";
            this.comboBoxEventType.Size = new System.Drawing.Size(343, 24);
            this.comboBoxEventType.TabIndex = 32;
            this.comboBoxEventType.SelectedIndexChanged += new System.EventHandler(this.comboBoxEventType_SelectedIndexChanged);
            // 
            // textBoxTarget
            // 
            this.textBoxTarget.Location = new System.Drawing.Point(43, 308);
            this.textBoxTarget.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxTarget.Name = "textBoxTarget";
            this.textBoxTarget.Size = new System.Drawing.Size(343, 22);
            this.textBoxTarget.TabIndex = 39;
            // 
            // labelTarget
            // 
            this.labelTarget.AutoSize = true;
            this.labelTarget.Location = new System.Drawing.Point(43, 276);
            this.labelTarget.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTarget.Name = "labelTarget";
            this.labelTarget.Size = new System.Drawing.Size(125, 16);
            this.labelTarget.TabIndex = 38;
            this.labelTarget.Text = "Target | New Value:";
            // 
            // textBoxObject
            // 
            this.textBoxObject.Location = new System.Drawing.Point(43, 226);
            this.textBoxObject.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxObject.Name = "textBoxObject";
            this.textBoxObject.Size = new System.Drawing.Size(343, 22);
            this.textBoxObject.TabIndex = 37;
            // 
            // labelObject
            // 
            this.labelObject.AutoSize = true;
            this.labelObject.Location = new System.Drawing.Point(43, 194);
            this.labelObject.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelObject.Name = "labelObject";
            this.labelObject.Size = new System.Drawing.Size(108, 16);
            this.labelObject.TabIndex = 36;
            this.labelObject.Text = "Action | Property:";
            this.labelObject.Click += new System.EventHandler(this.labelObject_Click);
            // 
            // textBoxSubject
            // 
            this.textBoxSubject.Location = new System.Drawing.Point(43, 146);
            this.textBoxSubject.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxSubject.Name = "textBoxSubject";
            this.textBoxSubject.Size = new System.Drawing.Size(343, 22);
            this.textBoxSubject.TabIndex = 35;
            this.textBoxSubject.TextChanged += new System.EventHandler(this.textBoxSubject_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 114);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 34;
            this.label4.Text = "Subject:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(43, 28);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 16);
            this.label6.TabIndex = 31;
            this.label6.Text = "Type:";
            // 
            // textBoxDesirability
            // 
            this.textBoxDesirability.Location = new System.Drawing.Point(43, 396);
            this.textBoxDesirability.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxDesirability.Name = "textBoxDesirability";
            this.textBoxDesirability.Size = new System.Drawing.Size(343, 22);
            this.textBoxDesirability.TabIndex = 41;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 364);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 16);
            this.label1.TabIndex = 40;
            this.label1.Text = "Desirability:";
            // 
            // textBoxPraiseworthiness
            // 
            this.textBoxPraiseworthiness.Location = new System.Drawing.Point(44, 485);
            this.textBoxPraiseworthiness.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxPraiseworthiness.Name = "textBoxPraiseworthiness";
            this.textBoxPraiseworthiness.Size = new System.Drawing.Size(343, 22);
            this.textBoxPraiseworthiness.TabIndex = 43;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 453);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 16);
            this.label2.TabIndex = 42;
            this.label2.Text = "Praiseworthiness:";
            // 
            // AddOrEditAppraisalRuleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(437, 583);
            this.Controls.Add(this.textBoxPraiseworthiness);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxDesirability);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxEventType);
            this.Controls.Add(this.textBoxTarget);
            this.Controls.Add(this.labelTarget);
            this.Controls.Add(this.textBoxObject);
            this.Controls.Add(this.labelObject);
            this.Controls.Add(this.textBoxSubject);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.addOrEditButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "AddOrEditAppraisalRuleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Appraisal Rule";
            this.Load += new System.EventHandler(this.AddOrEditAppraisalRuleForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddOrEditAppraisalRuleForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.addBeliefErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emotionalAppraisalAssetBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource emotionalAppraisalAssetBindingSource;
        private System.Windows.Forms.Button addOrEditButton;
        private System.Windows.Forms.ErrorProvider addBeliefErrorProvider;
        private System.Windows.Forms.ComboBox comboBoxEventType;
        private System.Windows.Forms.TextBox textBoxTarget;
        private System.Windows.Forms.Label labelTarget;
        private System.Windows.Forms.TextBox textBoxObject;
        private System.Windows.Forms.Label labelObject;
        private System.Windows.Forms.TextBox textBoxSubject;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxPraiseworthiness;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxDesirability;
        private System.Windows.Forms.Label label1;
    }
}