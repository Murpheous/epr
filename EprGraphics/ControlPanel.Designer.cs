﻿namespace EprGrapics
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
            this.lblAzimuth = new System.Windows.Forms.Label();
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
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.lblAnalyzer1Result = new System.Windows.Forms.Label();
            this.picBoxGraph = new System.Windows.Forms.PictureBox();
            this.btnMalus = new System.Windows.Forms.Button();
            this.btnEpr = new System.Windows.Forms.Button();
            this.lblAnalyzer2Result = new System.Windows.Forms.Label();
            this.lblMapped = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMalus = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LblEprCorrelation = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTheta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPhi1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPhi2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxGraph)).BeginInit();
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
            // lblAzimuth
            // 
            this.lblAzimuth.AutoSize = true;
            this.lblAzimuth.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAzimuth.Location = new System.Drawing.Point(91, 16);
            this.lblAzimuth.MinimumSize = new System.Drawing.Size(50, 0);
            this.lblAzimuth.Name = "lblAzimuth";
            this.lblAzimuth.Size = new System.Drawing.Size(50, 15);
            this.lblAzimuth.TabIndex = 3;
            this.lblAzimuth.Text = "0";
            // 
            // trackPhi1
            // 
            this.trackPhi1.LargeChange = 1;
            this.trackPhi1.Location = new System.Drawing.Point(160, 101);
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
            this.trackPhi2.Location = new System.Drawing.Point(772, 101);
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
            this.pictureBox1.Location = new System.Drawing.Point(152, 172);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(480, 480);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(772, 172);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(480, 480);
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // lblFilterAz1
            // 
            this.lblFilterAz1.AutoSize = true;
            this.lblFilterAz1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFilterAz1.Location = new System.Drawing.Point(98, 170);
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
            this.label4.Location = new System.Drawing.Point(3, 170);
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
            this.lblFilterAz2.Location = new System.Drawing.Point(716, 172);
            this.lblFilterAz2.MinimumSize = new System.Drawing.Size(50, 0);
            this.lblFilterAz2.Name = "lblFilterAz2";
            this.lblFilterAz2.Size = new System.Drawing.Size(50, 15);
            this.lblFilterAz2.TabIndex = 13;
            this.lblFilterAz2.Text = "0";
            this.lblFilterAz2.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(623, 174);
            this.label6.MinimumSize = new System.Drawing.Size(90, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Analyzer 2";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Click += new System.EventHandler(this.label6_Click);
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
            this.label8.Size = new System.Drawing.Size(112, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Initial phaseAngle:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 1;
            this.trackBar1.Location = new System.Drawing.Point(131, 45);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.trackBar1.Maximum = 720;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(999, 45);
            this.trackBar1.TabIndex = 14;
            this.trackBar1.TickFrequency = 10;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar1.Value = 360;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // lblAnalyzer1Result
            // 
            this.lblAnalyzer1Result.AutoSize = true;
            this.lblAnalyzer1Result.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAnalyzer1Result.Location = new System.Drawing.Point(98, 205);
            this.lblAnalyzer1Result.MinimumSize = new System.Drawing.Size(50, 0);
            this.lblAnalyzer1Result.Name = "lblAnalyzer1Result";
            this.lblAnalyzer1Result.Size = new System.Drawing.Size(50, 15);
            this.lblAnalyzer1Result.TabIndex = 17;
            this.lblAnalyzer1Result.Text = "0";
            this.lblAnalyzer1Result.Click += new System.EventHandler(this.lblAnalyzer1Result_Click);
            // 
            // picBoxGraph
            // 
            this.picBoxGraph.Location = new System.Drawing.Point(152, 681);
            this.picBoxGraph.Name = "picBoxGraph";
            this.picBoxGraph.Size = new System.Drawing.Size(1100, 345);
            this.picBoxGraph.TabIndex = 18;
            this.picBoxGraph.TabStop = false;
            // 
            // btnMalus
            // 
            this.btnMalus.Location = new System.Drawing.Point(21, 563);
            this.btnMalus.Name = "btnMalus";
            this.btnMalus.Size = new System.Drawing.Size(96, 34);
            this.btnMalus.TabIndex = 19;
            this.btnMalus.Text = "Run Malus";
            this.btnMalus.UseVisualStyleBackColor = true;
            this.btnMalus.Click += new System.EventHandler(this.btnMalus_Click);
            // 
            // btnEpr
            // 
            this.btnEpr.Location = new System.Drawing.Point(21, 618);
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
            this.lblAnalyzer2Result.Location = new System.Drawing.Point(716, 205);
            this.lblAnalyzer2Result.MinimumSize = new System.Drawing.Size(50, 0);
            this.lblAnalyzer2Result.Name = "lblAnalyzer2Result";
            this.lblAnalyzer2Result.Size = new System.Drawing.Size(50, 15);
            this.lblAnalyzer2Result.TabIndex = 21;
            this.lblAnalyzer2Result.Text = "0";
            // 
            // lblMapped
            // 
            this.lblMapped.AutoSize = true;
            this.lblMapped.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMapped.Location = new System.Drawing.Point(1136, 31);
            this.lblMapped.MinimumSize = new System.Drawing.Size(50, 0);
            this.lblMapped.Name = "lblMapped";
            this.lblMapped.Size = new System.Drawing.Size(50, 15);
            this.lblMapped.TabIndex = 22;
            this.lblMapped.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1136, 9);
            this.label3.MinimumSize = new System.Drawing.Size(90, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "SinSq to Degrees";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMalus
            // 
            this.lblMalus.AutoSize = true;
            this.lblMalus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMalus.Location = new System.Drawing.Point(98, 260);
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
            this.label5.Location = new System.Drawing.Point(3, 260);
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
            this.LblEprCorrelation.Location = new System.Drawing.Point(680, 273);
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
            this.label7.Location = new System.Drawing.Point(659, 260);
            this.label7.MinimumSize = new System.Drawing.Size(90, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "EPR Correlation";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 1053);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.LblEprCorrelation);
            this.Controls.Add(this.lblMalus);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblMapped);
            this.Controls.Add(this.lblAnalyzer2Result);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnEpr);
            this.Controls.Add(this.btnMalus);
            this.Controls.Add(this.picBoxGraph);
            this.Controls.Add(this.lblAnalyzer1Result);
            this.Controls.Add(this.lblPhi);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.lblFilterAz2);
            this.Controls.Add(this.lblFilterAz1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.trackPhi2);
            this.Controls.Add(this.trackPhi1);
            this.Controls.Add(this.lblAzimuth);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBarTheta);
            this.Controls.Add(this.label6);
            this.MinimumSize = new System.Drawing.Size(50, 39);
            this.Name = "ControlPanel";
            this.Text = "Polarization Mapping Tests";
            this.Load += new System.EventHandler(this.ControlPanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTheta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPhi1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPhi2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxGraph)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarTheta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAzimuth;
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
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label lblAnalyzer1Result;
        private System.Windows.Forms.PictureBox picBoxGraph;
        private System.Windows.Forms.Button btnMalus;
        private System.Windows.Forms.Button btnEpr;
        private System.Windows.Forms.Label lblAnalyzer2Result;
        private System.Windows.Forms.Label lblMapped;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMalus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label LblEprCorrelation;
        private System.Windows.Forms.Label label7;
    }
}
