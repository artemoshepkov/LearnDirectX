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
            this.RotationZ = new System.Windows.Forms.TextBox();
            this.RotationY = new System.Windows.Forms.TextBox();
            this.RotationX = new System.Windows.Forms.TextBox();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.TransformZ = new System.Windows.Forms.TextBox();
            this.TransformY = new System.Windows.Forms.TextBox();
            this.TransformX = new System.Windows.Forms.TextBox();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.GameObjectList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SidePanelGameObjects.SuspendLayout();
            this.SuspendLayout();
            // 
            // SidePanelGameObjects
            // 
            this.SidePanelGameObjects.Controls.Add(this.label2);
            this.SidePanelGameObjects.Controls.Add(this.label1);
            this.SidePanelGameObjects.Controls.Add(this.RotationZ);
            this.SidePanelGameObjects.Controls.Add(this.RotationY);
            this.SidePanelGameObjects.Controls.Add(this.RotationX);
            this.SidePanelGameObjects.Controls.Add(this.labelControl4);
            this.SidePanelGameObjects.Controls.Add(this.labelControl5);
            this.SidePanelGameObjects.Controls.Add(this.labelControl6);
            this.SidePanelGameObjects.Controls.Add(this.TransformZ);
            this.SidePanelGameObjects.Controls.Add(this.TransformY);
            this.SidePanelGameObjects.Controls.Add(this.TransformX);
            this.SidePanelGameObjects.Controls.Add(this.labelControl3);
            this.SidePanelGameObjects.Controls.Add(this.labelControl2);
            this.SidePanelGameObjects.Controls.Add(this.labelControl1);
            this.SidePanelGameObjects.Controls.Add(this.GameObjectList);
            this.SidePanelGameObjects.Dock = System.Windows.Forms.DockStyle.Left;
            this.SidePanelGameObjects.Location = new System.Drawing.Point(0, 0);
            this.SidePanelGameObjects.Name = "SidePanelGameObjects";
            this.SidePanelGameObjects.Size = new System.Drawing.Size(183, 465);
            this.SidePanelGameObjects.TabIndex = 0;
            this.SidePanelGameObjects.Text = "RotationX";
            // 
            // RotationZ
            // 
            this.RotationZ.Location = new System.Drawing.Point(127, 288);
            this.RotationZ.Name = "RotationZ";
            this.RotationZ.Size = new System.Drawing.Size(52, 21);
            this.RotationZ.TabIndex = 15;
            this.RotationZ.TextChanged += new System.EventHandler(this.RotationZ_TextChanged);
            this.RotationZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RotationZ_KeyPress);
            // 
            // RotationY
            // 
            this.RotationY.Location = new System.Drawing.Point(64, 288);
            this.RotationY.Name = "RotationY";
            this.RotationY.Size = new System.Drawing.Size(57, 21);
            this.RotationY.TabIndex = 14;
            this.RotationY.TextChanged += new System.EventHandler(this.RotationY_TextChanged);
            this.RotationY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RotationY_KeyPress);
            // 
            // RotationX
            // 
            this.RotationX.Location = new System.Drawing.Point(3, 288);
            this.RotationX.Name = "RotationX";
            this.RotationX.Size = new System.Drawing.Size(55, 21);
            this.RotationX.TabIndex = 13;
            this.RotationX.TextChanged += new System.EventHandler(this.RotationX_TextChanged);
            this.RotationX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RotationX_KeyPress);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(138, 269);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(6, 13);
            this.labelControl4.TabIndex = 12;
            this.labelControl4.Text = "Z";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(84, 269);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(6, 13);
            this.labelControl5.TabIndex = 11;
            this.labelControl5.Text = "Y";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(34, 269);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(6, 13);
            this.labelControl6.TabIndex = 10;
            this.labelControl6.Text = "X";
            // 
            // TransformZ
            // 
            this.TransformZ.Location = new System.Drawing.Point(127, 218);
            this.TransformZ.Name = "TransformZ";
            this.TransformZ.Size = new System.Drawing.Size(52, 21);
            this.TransformZ.TabIndex = 9;
            this.TransformZ.TextChanged += new System.EventHandler(this.TransformZ_TextChanged);
            this.TransformZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TransformZ_KeyPress);
            // 
            // TransformY
            // 
            this.TransformY.Location = new System.Drawing.Point(64, 218);
            this.TransformY.Name = "TransformY";
            this.TransformY.Size = new System.Drawing.Size(57, 21);
            this.TransformY.TabIndex = 8;
            this.TransformY.TextChanged += new System.EventHandler(this.TransformY_TextChanged);
            this.TransformY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TransformY_KeyPress);
            // 
            // TransformX
            // 
            this.TransformX.Location = new System.Drawing.Point(3, 218);
            this.TransformX.Name = "TransformX";
            this.TransformX.Size = new System.Drawing.Size(55, 21);
            this.TransformX.TabIndex = 7;
            this.TransformX.TextChanged += new System.EventHandler(this.TransformX_TextChanged);
            this.TransformX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TransformX_KeyPress);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(138, 199);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(6, 13);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "Z";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(84, 199);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(6, 13);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "Y";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(34, 199);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(6, 13);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "X";
            // 
            // GameObjectList
            // 
            this.GameObjectList.FormattingEnabled = true;
            this.GameObjectList.Location = new System.Drawing.Point(3, 3);
            this.GameObjectList.Name = "GameObjectList";
            this.GameObjectList.Size = new System.Drawing.Size(176, 173);
            this.GameObjectList.TabIndex = 0;
            this.GameObjectList.SelectedIndexChanged += new System.EventHandler(this.GameObjectList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 180);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Position";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 242);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Rotation";
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 465);
            this.Controls.Add(this.SidePanelGameObjects);
            this.Name = "Form";
            this.Text = "Form";
            this.SidePanelGameObjects.ResumeLayout(false);
            this.SidePanelGameObjects.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SidePanel SidePanelGameObjects;
        private System.Windows.Forms.ListBox GameObjectList;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.TextBox TransformX;
        private System.Windows.Forms.TextBox TransformY;
        private System.Windows.Forms.TextBox TransformZ;
        private System.Windows.Forms.TextBox RotationZ;
        private System.Windows.Forms.TextBox RotationY;
        private System.Windows.Forms.TextBox RotationX;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}