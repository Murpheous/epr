namespace EprGrapics
{
    partial class ControlPanel
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
            this.trackBarTheta = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSourceAxis = new System.Windows.Forms.Label();
            this.trackPhi1 = new System.Windows.Forms.TrackBar();
            this.trackPhi2 = new System.Windows.Forms.TrackBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblFilterAz1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFilterAz2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblPhi = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbPhase = new System.Windows.Forms.TrackBar();
            this.lblAnalyzer1Result = new System.Windows.Forms.Label();
            this.picBoxGraph = new System.Windows.Forms.PictureBox();
            this.btnMalus = new System.Windows.Forms.Button();
            this.btnEpr = new System.Windows.Forms.Button();
            this.lblAnalyzer2Result = new System.Windows.Forms.Label();
            this.lblMalus = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LblEprCorrelation = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbTwist = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSourceAzimuth = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTheta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPhi1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPhi2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPhase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTwist)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBarTheta
            // 
            this.trackBarTheta.LargeChange = 1;
            this.trackBarTheta.Location = new System.Drawing.Point(131, 1);
            this.trackBarTheta.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.trackBarTheta.Maximum = 360;
            this.trackBarTheta.Name = "trackBarTheta";
            this.trackBarTheta.Size = new System.Drawing.Size(999, 45);
            this.trackBarTheta.TabIndex = 1;
            this.trackBarTheta.TickFrequency = 10;
            this.trackBarTheta.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarTheta.Value = 180;
            this.trackBarTheta.Scroll += new System.EventHandler(this.trackBarTheta_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 16);
            this.label1.MinimumSize = new System.Drawing.Size(90, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Source Axis:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSourceAxis
            // 
            this.lblSourceAxis.AutoSize = true;
            this.lblSourceAxis.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSourceAxis.Location = new System.Drawing.Point(91, 16);
            this.lblSourceAxis.MinimumSize = new System.Drawing.Size(50, 0);
            this.lblSourceAxis.Name = "lblSourceAxis";
            this.lblSourceAxis.Size = new System.Drawing.Size(50, 15);
            this.lblSourceAxis.TabIndex = 3;
            this.lblSourceAxis.Text = "0";
            // 
            // trackPhi1
            // 
            this.trackPhi1.LargeChange = 1;
            this.trackPhi1.Location = new System.Drawing.Point(153, 97);
            this.trackPhi1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.trackPhi1.Maximum = 360;
            this.trackPhi1.Name = "trackPhi1";
            this.trackPhi1.Size = new System.Drawing.Size(480, 45);
            this.trackPhi1.TabIndex = 4;
            this.trackPhi1.TickFrequency = 10;
            this.trackPhi1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackPhi1.Value = 180;
            this.trackPhi1.Scroll += new System.EventHandler(this.trackPhi1_Scroll);
            // 
            // trackPhi2
            // 
            this.trackPhi2.LargeChange = 1;
            this.trackPhi2.Location = new System.Drawing.Point(773, 97);
            this.trackPhi2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.trackPhi2.Maximum = 360;
            this.trackPhi2.Name = "trackPhi2";
            this.trackPhi2.Size = new System.Drawing.Size(480, 45);
            this.trackPhi2.TabIndex = 5;
            this.trackPhi2.TickFrequency = 10;
            this.trackPhi2.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackPhi2.Value = 180;
            this.trackPhi2.Scroll += new System.EventHandler(this.trackPhi2_Scroll);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(153, 146);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(480, 480);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(773, 146);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(480, 480);
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // lblFilterAz1
            // 
            this.lblFilterAz1.AutoSize = true;
            this.lblFilterAz1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFilterAz1.Location = new System.Drawing.Point(99, 144);
            this.lblFilterAz1.MinimumSize = new System.Drawing.Size(50, 0);
            this.lblFilterAz1.Name = "lblFilterAz1";
            this.lblFilterAz1.Size = new System.Drawing.Size(50, 15);
            this.lblFilterAz1.TabIndex = 11;
            this.lblFilterAz1.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 144);
            this.label4.MinimumSize = new System.Drawing.Size(90, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Analyzer 1";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFilterAz2
            // 
            this.lblFilterAz2.AutoSize = true;
            this.lblFilterAz2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFilterAz2.Location = new System.Drawing.Point(717, 146);
            this.lblFilterAz2.MinimumSize = new System.Drawing.Size(50, 0);
            this.lblFilterAz2.Name = "lblFilterAz2";
            this.lblFilterAz2.Size = new System.Drawing.Size(50, 15);
            this.lblFilterAz2.TabIndex = 13;
            this.lblFilterAz2.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(624, 148);
            this.label6.MinimumSize = new System.Drawing.Size(90, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Analyzer 2";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPhi
            // 
            this.lblPhi.AutoSize = true;
            this.lblPhi.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPhi.Location = new System.Drawing.Point(91, 59);
            this.lblPhi.MinimumSize = new System.Drawing.Size(50, 0);
            this.lblPhi.Name = "lblPhi";
            this.lblPhi.Size = new System.Drawing.Size(50, 15);
            this.lblPhi.TabIndex = 16;
            this.lblPhi.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(0, 59);
            this.label8.MinimumSize = new System.Drawing.Size(90, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Phase Angle:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbPhase
            // 
            this.tbPhase.LargeChange = 1;
            this.tbPhase.Location = new System.Drawing.Point(131, 45);
            this.tbPhase.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.tbPhase.Maximum = 720;
            this.tbPhase.Name = "tbPhase";
            this.tbPhase.Size = new System.Drawing.Size(999, 45);
            this.tbPhase.TabIndex = 14;
            this.tbPhase.TickFrequency = 10;
            this.tbPhase.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tbPhase.Value = 360;
            this.tbPhase.Scroll += new System.EventHandler(this.tbPhase_Scroll);
            // 
            // lblAnalyzer1Result
            // 
            this.lblAnalyzer1Result.AutoSize = true;
            this.lblAnalyzer1Result.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAnalyzer1Result.Location = new System.Drawing.Point(99, 179);
            this.lblAnalyzer1Result.MinimumSize = new System.Drawing.Size(50, 0);
            this.lblAnalyzer1Result.Name = "lblAnalyzer1Result";
            this.lblAnalyzer1Result.Size = new System.Drawing.Size(50, 15);
            this.lblAnalyzer1Result.TabIndex = 17;
            this.lblAnalyzer1Result.Text = "0";
            // 
            // picBoxGraph
            // 
            this.picBoxGraph.Location = new System.Drawing.Point(10, 637);
            this.picBoxGraph.Margin = new System.Windows.Forms.Padding(1);
            this.picBoxGraph.Name = "picBoxGraph";
            this.picBoxGraph.Size = new System.Drawing.Size(1600, 400);
            this.picBoxGraph.TabIndex = 18;
            this.picBoxGraph.TabStop = false;
            // 
            // btnMalus
            // 
            this.btnMalus.Location = new System.Drawing.Point(22, 537);
            this.btnMalus.Name = "btnMalus";
            this.btnMalus.Size = new System.Drawing.Size(96, 34);
            this.btnMalus.TabIndex = 19;
            this.btnMalus.Text = "Run Malus";
            this.btnMalus.UseVisualStyleBackColor = true;
            this.btnMalus.Click += new System.EventHandler(this.btnMalus_Click);
            // 
            // btnEpr
            // 
            this.btnEpr.Location = new System.Drawing.Point(22, 592);
            this.btnEpr.Name = "btnEpr";
            this.btnEpr.Size = new System.Drawing.Size(96, 34);
            this.btnEpr.TabIndex = 20;
            this.btnEpr.Text = "Run Epr";
            this.btnEpr.UseVisualStyleBackColor = true;
            this.btnEpr.Click += new System.EventHandler(this.BtnEprClick);
            // 
            // lblAnalyzer2Result
            // 
            this.lblAnalyzer2Result.AutoSize = true;
            this.lblAnalyzer2Result.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAnalyzer2Result.Location = new System.Drawing.Point(717, 179);
            this.lblAnalyzer2Result.MinimumSize = new System.Drawing.Size(50, 0);
            this.lblAnalyzer2Result.Name = "lblAnalyzer2Result";
            this.lblAnalyzer2Result.Size = new System.Drawing.Size(50, 15);
            this.lblAnalyzer2Result.TabIndex = 21;
            this.lblAnalyzer2Result.Text = "0";
            // 
            // lblMalus
            // 
            this.lblMalus.AutoSize = true;
            this.lblMalus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMalus.Location = new System.Drawing.Point(99, 234);
            this.lblMalus.MinimumSize = new System.Drawing.Size(50, 0);
            this.lblMalus.Name = "lblMalus";
            this.lblMalus.Size = new System.Drawing.Size(50, 15);
            this.lblMalus.TabIndex = 25;
            this.lblMalus.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 234);
            this.label5.MinimumSize = new System.Drawing.Size(90, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Malus";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblEprCorrelation
            // 
            this.LblEprCorrelation.AutoSize = true;
            this.LblEprCorrelation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblEprCorrelation.Location = new System.Drawing.Point(681, 247);
            this.LblEprCorrelation.MinimumSize = new System.Drawing.Size(50, 0);
            this.LblEprCorrelation.Name = "LblEprCorrelation";
            this.LblEprCorrelation.Size = new System.Drawing.Size(50, 15);
            this.LblEprCorrelation.TabIndex = 28;
            this.LblEprCorrelation.Text = "0";
            this.LblEprCorrelation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(660, 234);
            this.label7.MinimumSize = new System.Drawing.Size(90, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "EPR Correlation";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbTwist
            // 
            this.tbTwist.LargeChange = 1;
            this.tbTwist.Location = new System.Drawing.Point(1300, 144);
            this.tbTwist.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.tbTwist.Maximum = 90;
            this.tbTwist.Minimum = -90;
            this.tbTwist.Name = "tbTwist";
            this.tbTwist.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbTwist.Size = new System.Drawing.Size(45, 480);
            this.tbTwist.TabIndex = 30;
            this.tbTwist.TickFrequency = 10;
            this.tbTwist.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tbTwist.Scroll += new System.EventHandler(this.tbTwist_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1351, 358);
            this.label2.MinimumSize = new System.Drawing.Size(90, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "Twist";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSourceAzimuth
            // 
            this.lblSourceAzimuth.AutoSize = true;
            this.lblSourceAzimuth.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSourceAzimuth.Location = new System.Drawing.Point(1351, 376);
            this.lblSourceAzimuth.MinimumSize = new System.Drawing.Size(50, 0);
            this.lblSourceAzimuth.Name = "lblSourceAzimuth";
            this.lblSourceAzimuth.Size = new System.Drawing.Size(50, 15);
            this.lblSourceAzimuth.TabIndex = 32;
            this.lblSourceAzimuth.Text = "0";
            // 
            // ControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.lblSourceAzimuth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbTwist);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.LblEprCorrelation);
            this.Controls.Add(this.lblMalus);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblAnalyzer2Result);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnEpr);
            this.Controls.Add(this.btnMalus);
            this.Controls.Add(this.picBoxGraph);
            this.Controls.Add(this.lblAnalyzer1Result);
            this.Controls.Add(this.lblPhi);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbPhase);
            this.Controls.Add(this.lblFilterAz2);
            this.Controls.Add(this.lblFilterAz1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.trackPhi2);
            this.Controls.Add(this.trackPhi1);
            this.Controls.Add(this.lblSourceAxis);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBarTheta);
            this.Controls.Add(this.label6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "ControlPanel";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Polarization Mapping Tests";
            this.Load += new System.EventHandler(this.ControlPanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTheta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPhi1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPhi2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPhase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTwist)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarTheta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSourceAxis;
        private System.Windows.Forms.TrackBar trackPhi1;
        private System.Windows.Forms.TrackBar trackPhi2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblFilterAz1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblFilterAz2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblPhi;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar tbPhase;
        private System.Windows.Forms.Label lblAnalyzer1Result;
        private System.Windows.Forms.PictureBox picBoxGraph;
        private System.Windows.Forms.Button btnMalus;
        private System.Windows.Forms.Button btnEpr;
        private System.Windows.Forms.Label lblAnalyzer2Result;
        private System.Windows.Forms.Label lblMalus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label LblEprCorrelation;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar tbTwist;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSourceAzimuth;
    }
}

