namespace MSO_P3_Forms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            textBoxProgram = new TextBox();
            buttonRun = new Button();
            buttonMetrics = new Button();
            textBoxOutput = new TextBox();
            comboBoxLoadProgram = new ComboBox();
            worldGrid = new TableLayoutPanel();
            SuspendLayout();
            // 
            // textBoxProgram
            // 
            textBoxProgram.Location = new Point(291, 97);
            textBoxProgram.Multiline = true;
            textBoxProgram.Name = "textBoxProgram";
            textBoxProgram.Size = new Size(244, 245);
            textBoxProgram.TabIndex = 0;
            // 
            // buttonRun
            // 
            buttonRun.BackColor = Color.FromArgb(0, 194, 0);
            buttonRun.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            buttonRun.ForeColor = Color.White;
            buttonRun.Location = new Point(291, 348);
            buttonRun.Name = "buttonRun";
            buttonRun.Size = new Size(64, 38);
            buttonRun.TabIndex = 1;
            buttonRun.Text = "Run";
            buttonRun.UseVisualStyleBackColor = false;
            buttonRun.Click += buttonRun_Click;
            // 
            // buttonMetrics
            // 
            buttonMetrics.BackColor = Color.FromArgb(0, 128, 255);
            buttonMetrics.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            buttonMetrics.ForeColor = Color.White;
            buttonMetrics.Location = new Point(361, 348);
            buttonMetrics.Name = "buttonMetrics";
            buttonMetrics.Size = new Size(78, 38);
            buttonMetrics.TabIndex = 2;
            buttonMetrics.Text = "Metrics";
            buttonMetrics.UseVisualStyleBackColor = false;
            buttonMetrics.Click += buttonMetrics_Click;
            // 
            // textBoxOutput
            // 
            textBoxOutput.Location = new Point(291, 407);
            textBoxOutput.Multiline = true;
            textBoxOutput.Name = "textBoxOutput";
            textBoxOutput.ReadOnly = true;
            textBoxOutput.Size = new Size(244, 115);
            textBoxOutput.TabIndex = 3;
            textBoxOutput.Text = "<output>";
            // 
            // comboBoxLoadProgram
            // 
            comboBoxLoadProgram.FormattingEnabled = true;
            comboBoxLoadProgram.Items.AddRange(new object[] { "Basic", "Advanced", "Expert", "From file..." });
            comboBoxLoadProgram.Location = new Point(12, 56);
            comboBoxLoadProgram.Name = "comboBoxLoadProgram";
            comboBoxLoadProgram.Size = new Size(151, 28);
            comboBoxLoadProgram.TabIndex = 4;
            comboBoxLoadProgram.Tag = "";
            comboBoxLoadProgram.Text = "Load Program";
            comboBoxLoadProgram.SelectedIndexChanged += comboBoxLoadProgram_SelectedIndexChanged;
            // 
            // worldGrid
            // 
            worldGrid.BackColor = SystemColors.ControlLight;
            worldGrid.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            worldGrid.ColumnCount = 4;
            worldGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            worldGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            worldGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            worldGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            worldGrid.Location = new Point(587, 97);
            worldGrid.Name = "worldGrid";
            worldGrid.RowCount = 4;
            worldGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            worldGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            worldGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            worldGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            worldGrid.Size = new Size(200, 200);
            worldGrid.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(915, 587);
            Controls.Add(worldGrid);
            Controls.Add(comboBoxLoadProgram);
            Controls.Add(textBoxOutput);
            Controls.Add(buttonMetrics);
            Controls.Add(buttonRun);
            Controls.Add(textBoxProgram);
            Name = "Form1";
            Text = "Learn to program!";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxProgram;
        private Button buttonRun;
        private Button buttonMetrics;
        private TextBox textBoxOutput;
        private ComboBox comboBoxLoadProgram;
        private TableLayoutPanel worldGrid;
    }
}