﻿namespace EmotionalDecisionMakingWF
{
    partial class MainForm
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
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.buttonDuplicateReaction = new System.Windows.Forms.Button();
            this.buttonEditReaction = new System.Windows.Forms.Button();
            this.buttonAddReaction = new System.Windows.Forms.Button();
            this.buttonRemoveReaction = new System.Windows.Forms.Button();
            this.dataGridViewReactiveActions = new System.Windows.Forms.DataGridView();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.conditionSetEditor = new GAIPS.AssetEditorTools.ConditionSetEditorControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReactiveActions)).BeginInit();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.buttonDuplicateReaction);
            this.groupBox7.Controls.Add(this.buttonEditReaction);
            this.groupBox7.Controls.Add(this.buttonAddReaction);
            this.groupBox7.Controls.Add(this.buttonRemoveReaction);
            this.groupBox7.Controls.Add(this.dataGridViewReactiveActions);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Location = new System.Drawing.Point(0, 0);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox7.Size = new System.Drawing.Size(785, 304);
            this.groupBox7.TabIndex = 3;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Decision Rules";
            this.groupBox7.Enter += new System.EventHandler(this.groupBox7_Enter);
            // 
            // buttonDuplicateReaction
            // 
            this.buttonDuplicateReaction.Location = new System.Drawing.Point(189, 23);
            this.buttonDuplicateReaction.Margin = new System.Windows.Forms.Padding(4);
            this.buttonDuplicateReaction.Name = "buttonDuplicateReaction";
            this.buttonDuplicateReaction.Size = new System.Drawing.Size(93, 28);
            this.buttonDuplicateReaction.TabIndex = 10;
            this.buttonDuplicateReaction.Text = "Duplicate";
            this.buttonDuplicateReaction.UseVisualStyleBackColor = true;
            this.buttonDuplicateReaction.Click += new System.EventHandler(this.buttonDuplicateReaction_Click);
            // 
            // buttonEditReaction
            // 
            this.buttonEditReaction.Location = new System.Drawing.Point(88, 23);
            this.buttonEditReaction.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEditReaction.Name = "buttonEditReaction";
            this.buttonEditReaction.Size = new System.Drawing.Size(93, 28);
            this.buttonEditReaction.TabIndex = 9;
            this.buttonEditReaction.Text = "Edit";
            this.buttonEditReaction.UseVisualStyleBackColor = true;
            this.buttonEditReaction.Click += new System.EventHandler(this.buttonEditReaction_Click);
            // 
            // buttonAddReaction
            // 
            this.buttonAddReaction.Location = new System.Drawing.Point(8, 23);
            this.buttonAddReaction.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddReaction.Name = "buttonAddReaction";
            this.buttonAddReaction.Size = new System.Drawing.Size(72, 28);
            this.buttonAddReaction.TabIndex = 7;
            this.buttonAddReaction.Text = "Add";
            this.buttonAddReaction.UseVisualStyleBackColor = true;
            this.buttonAddReaction.Click += new System.EventHandler(this.buttonAddReaction_Click);
            // 
            // buttonRemoveReaction
            // 
            this.buttonRemoveReaction.Location = new System.Drawing.Point(291, 23);
            this.buttonRemoveReaction.Margin = new System.Windows.Forms.Padding(4);
            this.buttonRemoveReaction.Name = "buttonRemoveReaction";
            this.buttonRemoveReaction.Size = new System.Drawing.Size(93, 28);
            this.buttonRemoveReaction.TabIndex = 8;
            this.buttonRemoveReaction.Text = "Remove";
            this.buttonRemoveReaction.UseVisualStyleBackColor = true;
            this.buttonRemoveReaction.Click += new System.EventHandler(this.buttonRemoveReaction_Click);
            // 
            // dataGridViewReactiveActions
            // 
            this.dataGridViewReactiveActions.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataGridViewReactiveActions.AllowUserToAddRows = false;
            this.dataGridViewReactiveActions.AllowUserToDeleteRows = false;
            this.dataGridViewReactiveActions.AllowUserToOrderColumns = true;
            this.dataGridViewReactiveActions.AllowUserToResizeRows = false;
            this.dataGridViewReactiveActions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewReactiveActions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewReactiveActions.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewReactiveActions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReactiveActions.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridViewReactiveActions.Location = new System.Drawing.Point(8, 66);
            this.dataGridViewReactiveActions.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewReactiveActions.Name = "dataGridViewReactiveActions";
            this.dataGridViewReactiveActions.ReadOnly = true;
            this.dataGridViewReactiveActions.RowHeadersVisible = false;
            this.dataGridViewReactiveActions.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewReactiveActions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewReactiveActions.Size = new System.Drawing.Size(769, 230);
            this.dataGridViewReactiveActions.TabIndex = 2;
            this.dataGridViewReactiveActions.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewReactiveActions_CellMouseDoubleClick);
            this.dataGridViewReactiveActions.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewReactiveActions_RowEnter);
            // 
            // groupBox8
            // 
            this.groupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox8.Controls.Add(this.conditionSetEditor);
            this.groupBox8.Location = new System.Drawing.Point(4, 4);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox8.Size = new System.Drawing.Size(773, 363);
            this.groupBox8.TabIndex = 11;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Conditions";
            // 
            // conditionSetEditor
            // 
            this.conditionSetEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conditionSetEditor.Location = new System.Drawing.Point(4, 19);
            this.conditionSetEditor.Margin = new System.Windows.Forms.Padding(4);
            this.conditionSetEditor.Name = "conditionSetEditor";
            this.conditionSetEditor.Size = new System.Drawing.Size(765, 340);
            this.conditionSetEditor.TabIndex = 0;
            this.conditionSetEditor.View = null;
            this.conditionSetEditor.Load += new System.EventHandler(this.conditionSetEditor_Load);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(16, 33);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox7);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox8);
            this.splitContainer1.Size = new System.Drawing.Size(785, 681);
            this.splitContainer1.SplitterDistance = 304;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 17;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 729);
            this.Controls.Add(this.splitContainer1);
            this.EditorName = "EDM Editor";
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReactiveActions)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button buttonEditReaction;
        private System.Windows.Forms.Button buttonAddReaction;
        private System.Windows.Forms.Button buttonRemoveReaction;
        private System.Windows.Forms.DataGridView dataGridViewReactiveActions;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.SplitContainer splitContainer1;
		private GAIPS.AssetEditorTools.ConditionSetEditorControl conditionSetEditor;
        private System.Windows.Forms.Button buttonDuplicateReaction;
    }
}

