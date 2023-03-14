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
            this.TreeViewGameObjects = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ListBoxScenes = new System.Windows.Forms.ListBox();
            this.Inspector = new DevExpress.XtraEditors.SidePanel();
            this.SidePanelGameObjects.SuspendLayout();
            this.SuspendLayout();
            // 
            // SidePanelGameObjects
            // 
            this.SidePanelGameObjects.Controls.Add(this.TreeViewGameObjects);
            this.SidePanelGameObjects.Controls.Add(this.label2);
            this.SidePanelGameObjects.Controls.Add(this.label1);
            this.SidePanelGameObjects.Controls.Add(this.ListBoxScenes);
            this.SidePanelGameObjects.Dock = System.Windows.Forms.DockStyle.Left;
            this.SidePanelGameObjects.Location = new System.Drawing.Point(0, 0);
            this.SidePanelGameObjects.Name = "SidePanelGameObjects";
            this.SidePanelGameObjects.Size = new System.Drawing.Size(205, 545);
            this.SidePanelGameObjects.TabIndex = 0;
            this.SidePanelGameObjects.Text = "RotationX";
            // 
            // TreeViewGameObjects
            // 
            this.TreeViewGameObjects.Location = new System.Drawing.Point(4, 138);
            this.TreeViewGameObjects.Name = "TreeViewGameObjects";
            this.TreeViewGameObjects.Size = new System.Drawing.Size(197, 97);
            this.TreeViewGameObjects.TabIndex = 2;
            this.TreeViewGameObjects.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewGameObjects_AfterSelect);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Game objects";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Scenes";
            // 
            // ListBoxScenes
            // 
            this.ListBoxScenes.FormattingEnabled = true;
            this.ListBoxScenes.Location = new System.Drawing.Point(3, 24);
            this.ListBoxScenes.Name = "ListBoxScenes";
            this.ListBoxScenes.Size = new System.Drawing.Size(198, 95);
            this.ListBoxScenes.TabIndex = 2;
            this.ListBoxScenes.SelectedValueChanged += new System.EventHandler(this.ListBoxScenes_SelectedValueChanged);
            // 
            // Inspector
            // 
            this.Inspector.AutoScroll = true;
            this.Inspector.Dock = System.Windows.Forms.DockStyle.Right;
            this.Inspector.Location = new System.Drawing.Point(580, 0);
            this.Inspector.Name = "Inspector";
            this.Inspector.Size = new System.Drawing.Size(288, 545);
            this.Inspector.TabIndex = 1;
            this.Inspector.Text = "sidePanel1";
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 545);
            this.Controls.Add(this.Inspector);
            this.Controls.Add(this.SidePanelGameObjects);
            this.Name = "Form";
            this.Text = "Form";
            this.MouseEnter += new System.EventHandler(this.Form_MouseEnter);
            this.SidePanelGameObjects.ResumeLayout(false);
            this.SidePanelGameObjects.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SidePanel SidePanelGameObjects;
        private DevExpress.XtraEditors.SidePanel Inspector;
        private System.Windows.Forms.ListBox ListBoxScenes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView TreeViewGameObjects;
    }
}