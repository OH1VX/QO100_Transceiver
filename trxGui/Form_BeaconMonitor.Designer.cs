namespace trxGui
{
    partial class Form_BeaconMonitor
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel_beacon_spec = new System.Windows.Forms.Panel();
            this.panel_beacon_wf = new System.Windows.Forms.Panel();
            this.label_beacon_status = new System.Windows.Forms.Label();
            this.timer_beacon = new System.Windows.Forms.Timer(this.components);
            
            this.SuspendLayout();
            
            // panel_beacon_spec
            this.panel_beacon_spec.BackColor = System.Drawing.Color.Black;
            this.panel_beacon_spec.Location = new System.Drawing.Point(12, 12);
            this.panel_beacon_spec.Name = "panel_beacon_spec";
            this.panel_beacon_spec.Size = new System.Drawing.Size(560, 150);
            this.panel_beacon_spec.TabIndex = 0;
            this.panel_beacon_spec.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_beacon_spec_Paint);
            
            // panel_beacon_wf
            this.panel_beacon_wf.BackColor = System.Drawing.Color.Black;
            this.panel_beacon_wf.Location = new System.Drawing.Point(12, 168);
            this.panel_beacon_wf.Name = "panel_beacon_wf";
            this.panel_beacon_wf.Size = new System.Drawing.Size(560, 120);
            this.panel_beacon_wf.TabIndex = 1;
            this.panel_beacon_wf.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_beacon_wf_Paint);
            
            // label_beacon_status
            this.label_beacon_status.AutoSize = true;
            this.label_beacon_status.ForeColor = System.Drawing.Color.White;
            this.label_beacon_status.Location = new System.Drawing.Point(12, 291);
            this.label_beacon_status.Name = "label_beacon_status";
            this.label_beacon_status.Size = new System.Drawing.Size(100, 13);
            this.label_beacon_status.TabIndex = 2;
            this.label_beacon_status.Text = "Beacon Status";
            
            // timer_beacon
            this.timer_beacon.Interval = 100;
            this.timer_beacon.Tick += new System.EventHandler(this.timer_beacon_Tick);
            
            // Form_BeaconMonitor
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(584, 318);
            this.Controls.Add(this.label_beacon_status);
            this.Controls.Add(this.panel_beacon_wf);
            this.Controls.Add(this.panel_beacon_spec);
            this.Name = "Form_BeaconMonitor";
            this.Text = "Beacon Lock FFT Monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_BeaconMonitor_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Panel panel_beacon_spec;
        private System.Windows.Forms.Panel panel_beacon_wf;
        private System.Windows.Forms.Label label_beacon_status;
        private System.Windows.Forms.Timer timer_beacon;
    }
}
