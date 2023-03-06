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
            this.FloatPropLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.RotationZ = new System.Windows.Forms.TextBox();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.RotationY = new System.Windows.Forms.TextBox();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.RotationX = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GameObjectList = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.TransformX = new System.Windows.Forms.TextBox();
            this.TransformY = new System.Windows.Forms.TextBox();
            this.TransformZ = new System.Windows.Forms.TextBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.label2 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.TrackBarFloatProp = new System.Windows.Forms.TrackBar();
            this.SidePanelGameObjects.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarFloatProp)).BeginInit();
            this.SuspendLayout();
            // 
            // SidePanelGameObjects
            // 
            this.SidePanelGameObjects.Controls.Add(this.tableLayoutPanel1);
            this.SidePanelGameObjects.Dock = System.Windows.Forms.DockStyle.Left;
            this.SidePanelGameObjects.Location = new System.Drawing.Point(0, 0);
            this.SidePanelGameObjects.Name = "SidePanelGameObjects";
            this.SidePanelGameObjects.Size = new System.Drawing.Size(183, 465);
            this.SidePanelGameObjects.TabIndex = 0;
            this.SidePanelGameObjects.Text = "RotationX";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.FloatPropLabel, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.GameObjectList, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.TrackBarFloatProp, 0, 6);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 152F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(176, 459);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // FloatPropLabel
            // 
            this.FloatPropLabel.AutoSize = true;
            this.FloatPropLabel.Location = new System.Drawing.Point(3, 242);
            this.FloatPropLabel.Name = "FloatPropLabel";
            this.FloatPropLabel.Size = new System.Drawing.Size(76, 13);
            this.FloatPropLabel.TabIndex = 19;
            this.FloatPropLabel.Text = "Float property";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel3.Controls.Add(this.labelControl6, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.RotationZ, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.labelControl5, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.RotationY, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.labelControl4, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.RotationX, 0, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 192);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(170, 47);
            this.tableLayoutPanel3.TabIndex = 16;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(3, 3);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(6, 13);
            this.labelControl6.TabIndex = 10;
            this.labelControl6.Text = "X";
            // 
            // RotationZ
            // 
            this.RotationZ.Location = new System.Drawing.Point(115, 22);
            this.RotationZ.Name = "RotationZ";
            this.RotationZ.Size = new System.Drawing.Size(52, 21);
            this.RotationZ.TabIndex = 15;
            this.RotationZ.TextChanged += new System.EventHandler(this.RotationZ_TextChanged);
            this.RotationZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RotationZ_KeyPress);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(59, 3);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(6, 13);
            this.labelControl5.TabIndex = 11;
            this.labelControl5.Text = "Y";
            // 
            // RotationY
            // 
            this.RotationY.Location = new System.Drawing.Point(59, 22);
            this.RotationY.Name = "RotationY";
            this.RotationY.Size = new System.Drawing.Size(50, 21);
            this.RotationY.TabIndex = 14;
            this.RotationY.TextChanged += new System.EventHandler(this.RotationY_TextChanged);
            this.RotationY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RotationY_KeyPress);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(115, 3);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(6, 13);
            this.labelControl4.TabIndex = 12;
            this.labelControl4.Text = "Z";
            // 
            // RotationX
            // 
            this.RotationX.Location = new System.Drawing.Point(3, 22);
            this.RotationX.Name = "RotationX";
            this.RotationX.Size = new System.Drawing.Size(50, 21);
            this.RotationX.TabIndex = 13;
            this.RotationX.TextChanged += new System.EventHandler(this.RotationX_TextChanged);
            this.RotationX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RotationX_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Position";
            // 
            // GameObjectList
            // 
            this.GameObjectList.FormattingEnabled = true;
            this.GameObjectList.Location = new System.Drawing.Point(3, 3);
            this.GameObjectList.Name = "GameObjectList";
            this.GameObjectList.Size = new System.Drawing.Size(170, 82);
            this.GameObjectList.TabIndex = 18;
            this.GameObjectList.SelectedIndexChanged += new System.EventHandler(this.GameObjectList_SelectedIndexChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.Controls.Add(this.TransformX, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.TransformY, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.TransformZ, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelControl1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelControl2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelControl3, 2, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 118);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43.06569F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 56.93431F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(170, 46);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // TransformX
            // 
            this.TransformX.Location = new System.Drawing.Point(3, 22);
            this.TransformX.Name = "TransformX";
            this.TransformX.Size = new System.Drawing.Size(47, 21);
            this.TransformX.TabIndex = 7;
            this.TransformX.TextChanged += new System.EventHandler(this.TransformX_TextChanged);
            this.TransformX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TransformX_KeyPress);
            // 
            // TransformY
            // 
            this.TransformY.Location = new System.Drawing.Point(59, 22);
            this.TransformY.Name = "TransformY";
            this.TransformY.Size = new System.Drawing.Size(50, 21);
            this.TransformY.TabIndex = 8;
            this.TransformY.TextChanged += new System.EventHandler(this.TransformY_TextChanged);
            this.TransformY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TransformY_KeyPress);
            // 
            // TransformZ
            // 
            this.TransformZ.Location = new System.Drawing.Point(115, 22);
            this.TransformZ.Name = "TransformZ";
            this.TransformZ.Size = new System.Drawing.Size(41, 21);
            this.TransformZ.TabIndex = 9;
            this.TransformZ.TextChanged += new System.EventHandler(this.TransformZ_TextChanged);
            this.TransformZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TransformZ_KeyPress);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(3, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(6, 13);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "X";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(59, 3);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(6, 13);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "Y";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(115, 3);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(6, 13);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "Z";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Rotation";
            // 
            // TrackBarFloatProp
            // 
            this.TrackBarFloatProp.Location = new System.Drawing.Point(3, 268);
            this.TrackBarFloatProp.Name = "TrackBarFloatProp";
            this.TrackBarFloatProp.Size = new System.Drawing.Size(170, 36);
            this.TrackBarFloatProp.TabIndex = 21;
            this.TrackBarFloatProp.ValueChanged += new System.EventHandler(this.TrackBarFloatProp_ValueChanged);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 465);
            this.Controls.Add(this.SidePanelGameObjects);
            this.Name = "Form";
            this.Text = "Form";
            this.MouseEnter += new System.EventHandler(this.Form_MouseEnter);
            this.SidePanelGameObjects.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarFloatProp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SidePanel SidePanelGameObjects;
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox GameObjectList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label FloatPropLabel;
        private System.Windows.Forms.TrackBar TrackBarFloatProp;
    }
}