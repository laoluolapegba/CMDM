namespace CMdm.BackService
{
    partial class CmdmBackService
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.eventLog1 = new System.Diagnostics.EventLog();
            this.tmrJobs = new System.Windows.Forms.Timer(this.components);
            this.CheckPending = new System.Timers.Timer();
            this.DoAlerts = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckPending)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DoAlerts)).BeginInit();
            // 
            // eventLog1
            // 
            this.eventLog1.Log = "CmdmBackServiceLog";
            this.eventLog1.Source = "CmdmBackService";
            // 
            // tmrJobs
            // 
            this.tmrJobs.Enabled = true;
            this.tmrJobs.Interval = 120000;
            this.tmrJobs.Tick += new System.EventHandler(this.tmrJobs_Tick);
            // 
            // CheckPending
            // 
            this.CheckPending.Enabled = true;
            this.CheckPending.Interval = 120000D;
            this.CheckPending.Elapsed += new System.Timers.ElapsedEventHandler(this.CheckPending_Elapsed);
            // 
            // DoAlerts
            // 
            this.DoAlerts.Enabled = true;
            // 
            // CmdmBackService
            // 
            this.ServiceName = "CmdmBackService";
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckPending)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DoAlerts)).EndInit();

        }

        #endregion

        private System.Diagnostics.EventLog eventLog1;
        private System.Windows.Forms.Timer tmrJobs;
        private System.Timers.Timer CheckPending;
        private System.Timers.Timer DoAlerts;
    }
}
