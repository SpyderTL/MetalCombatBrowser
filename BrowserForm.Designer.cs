
namespace MetalCombatBrowser
{
	partial class BrowserForm
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.TreeView = new System.Windows.Forms.TreeView();
			this.ListView = new System.Windows.Forms.ListView();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.TreeView);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.ListView);
			this.splitContainer1.Size = new System.Drawing.Size(1008, 729);
			this.splitContainer1.SplitterDistance = 336;
			this.splitContainer1.TabIndex = 0;
			// 
			// TreeView
			// 
			this.TreeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TreeView.HideSelection = false;
			this.TreeView.Location = new System.Drawing.Point(0, 0);
			this.TreeView.Name = "TreeView";
			this.TreeView.Size = new System.Drawing.Size(336, 729);
			this.TreeView.TabIndex = 0;
			// 
			// ListView
			// 
			this.ListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ListView.HideSelection = false;
			this.ListView.Location = new System.Drawing.Point(0, 0);
			this.ListView.MultiSelect = false;
			this.ListView.Name = "ListView";
			this.ListView.Size = new System.Drawing.Size(668, 729);
			this.ListView.TabIndex = 0;
			this.ListView.UseCompatibleStateImageBehavior = false;
			// 
			// BrowserForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1008, 729);
			this.Controls.Add(this.splitContainer1);
			this.DoubleBuffered = true;
			this.Name = "BrowserForm";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		public System.Windows.Forms.TreeView TreeView;
		public System.Windows.Forms.ListView ListView;
	}
}

