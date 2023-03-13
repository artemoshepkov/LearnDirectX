namespace LearnDirectX
{
    partial class Form
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
            this.SidePanelGameObjects = new DevExpress.XtraEditors.SidePanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.GameObjectList = new System.Windows.Forms.ListBox();
            this.TrackBarFloatProp = new System.Windows.Forms.TrackBar();
            this.FloatPropLabel = new System.Windows.Forms.Label();
            this.Inspector = new DevExpress.XtraEditors.SidePanel();
            this.ListBoxScenes = new System.Windows.Forms.ListBox();
            this.SidePanelGameObjects.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarFloatProp)).BeginInit();
            this.SuspendLayout();
            // 
            // SidePanelGameObjects
            // 
            this.SidePanelGameObjects.Controls.Add(this.tableLayoutPanel1);
            this.SidePanelGameObjects.Dock = System.Windows.Forms.DockStyle.Left;
            this.SidePanelGameObjects.Location = new System.Drawing.Point(0, 0);
            this.SidePanelGameObjects.Name = "SidePanelGameObjects";
            this.SidePanelGameObjects.Size = new System.Drawing.Size(205, 545);
            this.SidePanelGameObjects.TabIndex = 0;
            this.SidePanelGameObjects.Text = "RotationX";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.GameObjectList, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.FloatPropLabel, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.TrackBarFloatProp, 0, 13);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 14;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(198, 539);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // GameObjectList
            // 
            this.GameObjectList.FormattingEnabled = true;
            this.GameObjectList.Location = new System.Drawing.Point(3, 3);
            this.GameObjectList.Name = "GameObjectList";
            this.GameObjectList.Size = new System.Drawing.Size(195, 69);
            this.GameObjectList.TabIndex = 18;
            this.GameObjectList.SelectedIndexChanged += new System.EventHandler(this.GameObjectList_SelectedIndexChanged);
            // 
            // TrackBarFloatProp
            // 
            this.TrackBarFloatProp.Location = new System.Drawing.Point(3, 91);
            this.TrackBarFloatProp.Name = "TrackBarFloatProp";
            this.TrackBarFloatProp.Size = new System.Drawing.Size(192, 45);
            this.TrackBarFloatProp.TabIndex = 21;
            this.TrackBarFloatProp.ValueChanged += new System.EventHandler(this.TrackBarFloatProp_ValueChanged);
            // 
            // FloatPropLabel
            // 
            this.FloatPropLabel.AutoSize = true;
            this.FloatPropLabel.Location = new System.Drawing.Point(3, 75);
            this.FloatPropLabel.Name = "FloatPropLabel";
            this.FloatPropLabel.Size = new System.Drawing.Size(76, 13);
            this.FloatPropLabel.TabIndex = 19;
            this.FloatPropLabel.Text = "Float property";
            // 
            // Inspector
            // 
            this.Inspector.AutoScroll = true;
            this.Inspector.Dock = System.Windows.Forms.DockStyle.Right;
            this.Inspector.Location = new System.Drawing.Point(516, 0);
            this.Inspector.Name = "Inspector";
            this.Inspector.Size = new System.Drawing.Size(352, 545);
            this.Inspector.TabIndex = 1;
            this.Inspector.Text = "sidePanel1";
            // 
            // ListBoxScenes
            // 
            this.ListBoxScenes.FormattingEnabled = true;
            this.ListBoxScenes.Location = new System.Drawing.Point(207, 0);
            this.ListBoxScenes.Name = "ListBoxScenes";
            this.ListBoxScenes.Size = new System.Drawing.Size(140, 95);
            this.ListBoxScenes.TabIndex = 2;
            this.ListBoxScenes.SelectedValueChanged += new System.EventHandler(this.ListBoxScenes_SelectedValueChanged);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 545);
            this.Controls.Add(this.ListBoxScenes);
            this.Controls.Add(this.Inspector);
            this.Controls.Add(this.SidePanelGameObjects);
            this.Name = "Form";
            this.Text = "Form";
            this.MouseEnter += new System.EventHandler(this.Form_MouseEnter);
            this.SidePanelGameObjects.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarFloatProp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SidePanel SidePanelGameObjects;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox GameObjectList;
        private System.Windows.Forms.Label FloatPropLabel;
        private System.Windows.Forms.TrackBar TrackBarFloatProp;
        private DevExpress.XtraEditors.SidePanel Inspector;
        private System.Windows.Forms.ListBox ListBoxScenes;
    }
}