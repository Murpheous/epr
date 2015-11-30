using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EprGrapics
{
    public partial class ControlPanel : Form
    {
        double _photonAxis;
        double _photonAzimuthDeg;
        double sourcePhaseDeg;
        clFilter Analyzer_A;
        clFilter Analyzer_B;
        AnalyzeMethod analyzeMethod = AnalyzeMethod.Orientation;
        Boolean isLinear = true;

        public double PhotonAxisDeg
        {
            get { return _photonAxis * 180.0/ Math.PI;}
            set {
                _photonAxis = EprMath.Limit180((value * Math.PI) / 180.0);
                }
        }
        public double PhotonAxis
        {
            get { return _photonAxis;}
            set {
                _photonAxis = EprMath.Limit180(value);
                }
         }
        public ControlPanel()
        {
            InitializeComponent();
            Analyzer_A = new clFilter(0);
            Analyzer_B = new clFilter(0);
        }
        public void UpdateUi()
        {
            lblPhotonAxis.Text = string.Format("{0:F2}°",PhotonAxisDeg);
            lblPhi.Text = string.Format("{0:F2}°", sourcePhaseDeg);
            lblSourceAzimuth.Text = string.Format("{0:F2}°", _photonAzimuthDeg);
            rbRotation.Checked = (analyzeMethod == AnalyzeMethod.Orientation);
            rbPhasors.Checked = (analyzeMethod == AnalyzeMethod.Phasors);
        }

        private void ControlPanel_Load(object sender, EventArgs e)
        {
            Analyzer_A.ShowDial(pictureBox1);
            Analyzer_B.ShowDial(pictureBox2);
            trackBarTheta.Value = (trackBarTheta.Maximum - trackBarTheta.Minimum)/2;
            UpdateUi();
        }

        private void trackBarTheta_Scroll(object sender, EventArgs e)
        {
            PhotonAxisDeg = ((double)trackBarTheta.Value - trackBarTheta.Maximum / 2.0) / 2.0;
            UpdateUi();
            UpDatePhasorDisplay(sourcePhaseDeg, _photonAzimuthDeg);
        }
        private void tbPhase_Scroll(object sender, EventArgs e)
        {
            sourcePhaseDeg = (tbPhase.Value - 360.0) / 2.0;
            UpdateUi();
            UpDatePhasorDisplay(sourcePhaseDeg, _photonAzimuthDeg);
        }

        private void trackPhi2_Scroll(object sender, EventArgs e)
        {
            Analyzer_B.AxisDeg = (double)(trackPhi2.Value - 180.0) / 2.0;
            lblFilterAz2.Text = (Analyzer_B.AxisDeg).ToString();
            Analyzer_B.ShowDial(pictureBox2);
            UpDatePhasorDisplay(sourcePhaseDeg, _photonAzimuthDeg);
        }

        private void trackPhi1_Scroll(object sender, EventArgs e)
        {
            Analyzer_A.AxisDeg = (double)(trackPhi1.Value - 180) / 2.0;
            lblFilterAz1.Text = (Analyzer_A.AxisDeg).ToString();
            Analyzer_A.ShowDial(pictureBox1);
            UpDatePhasorDisplay(sourcePhaseDeg, _photonAzimuthDeg);
        }

        private void tbTwist_Scroll(object sender, EventArgs e)
        {
            _photonAzimuthDeg = (tbTwist.Value);
            UpdateUi();
            UpDatePhasorDisplay(sourcePhaseDeg, _photonAzimuthDeg);
        }

 
        private void UpDatePhasorDisplay(double srcPhaseDeg, double srcAzDeg )
        {
            this.SuspendLayout();
            clPhoton PhotonA = new clPhoton(analyzeMethod);
            clPhoton PhotonB = new clPhoton(analyzeMethod);
            double phi = srcPhaseDeg * (Math.PI / 180.0);
            double azimuth = srcAzDeg * (Math.PI / 180.0);
             // Now do a phasor visualisation on two analyzers
            //PhotonA.MakeElliptical(PhotonAxis, azimuth, phi, true);
            //PhotonB.MakeElliptical(PhotonAxis, azimuth, phi, false);
            if (isLinear)
            {
                PhotonA.MakeLinear(PhotonAxis, phi);
                PhotonB.MakeLinear(PhotonAxis, phi);
            }
            else
            {
                PhotonA.MakeCircular(PhotonAxis, true, phi);
                PhotonB.MakeCircular(PhotonAxis, false, phi);
            }
            Analyzer_A.ShowDial();
            bool bResultA = PhotonA.Analyze(Analyzer_A,true, lblPhasorA1Theta, lblPhasorA2Theta);
            if (bResultA)
            { }
            else
            { }

                Analyzer_B.ShowDial();
            bool nResultB = PhotonB.Analyze(Analyzer_B, true,lblPhasorB1Theta, lblPhasorB2Theta);
            lblAnalyzer1Result.Text = bResultA ? "Alice" : "Bob";
            lblAnalyzer2Result.Text = nResultB ? "Alice" : "Bob";
            this.ResumeLayout();

            int concurCount = 0;
            int dissentCount = 0;
          
            for (int i = 0; i < 3600; i++)
            {
                double testAxis = i * Math.PI / 1800.0;
                PhotonA.MakeCircular(testAxis,true, EprMath.quarterPI);
                PhotonB.MakeCircular(testAxis, false, EprMath.quarterPI);
                if (PhotonA.Analyze(Analyzer_A, false,null,null) == PhotonB.Analyze(Analyzer_B, false,null,null))
                    concurCount++;
                else
                    dissentCount++;
            }
            LblEprCorrelation.Text = string.Format("{0:F1}%", ((double)concurCount / 36.0));
          
        }

        private void btnMalus_Click(object sender, EventArgs e)
        {
            int nHorizPixels, nVertPixels, nX, nY;
            Pen MyPenB = new Pen(Color.Cyan, 1);
            System.Drawing.Bitmap ScreenBitmap = new Bitmap(picBoxGraph.ClientSize.Width, picBoxGraph.ClientSize.Height);
            System.Drawing.Graphics DisplayGraphics = System.Drawing.Graphics.FromImage(ScreenBitmap);
            DisplayGraphics.Clear(Color.DarkCyan);

            nHorizPixels = picBoxGraph.ClientSize.Width - 2;
            nVertPixels = picBoxGraph.ClientSize.Height - 2;
            // Loop through from -PI/2 to + PI/2
            int nAxisSteps, nPhiSteps;
            double dThetaAxis;
            clPhoton MyPhoton = new clPhoton(analyzeMethod);
            for (nAxisSteps = 0; nAxisSteps <= nHorizPixels; nAxisSteps++)
            {

                dThetaAxis = (double)nAxisSteps / (double)nHorizPixels;
                dThetaAxis *= 180;
                Analyzer_A.AxisDeg = dThetaAxis;
                int nNo = 0;
                int nYes = 0;
                for (nPhiSteps = 0; nPhiSteps < 3600; nPhiSteps++)
                {
                    double dPhi = (double)nPhiSteps / 10.0;
                    //MyPhoton.MakeLinear(0.0, dPhi*Math.PI/180);
                    MyPhoton.MakeLinear(0.0,dPhi * (Math.PI / 180));  
                    if (MyPhoton.Analyze(Analyzer_A, false, false, null, null))
                        nYes++;
                    else
                        nNo++;
                }
                nX = 1 + nAxisSteps;
                double sin2theta = (Math.Cos(dThetaAxis * Math.PI/90));
                sin2theta = 1.0 - ((sin2theta + 1.0) / 2);
                nY = (int)(nVertPixels * sin2theta);
                if (nY > 0)
                    ScreenBitmap.SetPixel(nX, nY-1, Color.Black);
                if (nY < nVertPixels)
                    ScreenBitmap.SetPixel(nX, nY+1, Color.Black);
                ScreenBitmap.SetPixel(nX, nY, Color.Black);
            
                nY = nVertPixels - ((nYes * nVertPixels) / 3600);
                ScreenBitmap.SetPixel(nX, nY, Color.Coral);
                if ((nAxisSteps % 5) == 0)
                {
                    picBoxGraph.Image = ScreenBitmap;
                    picBoxGraph.Refresh();
                }
            }
            picBoxGraph.Image = ScreenBitmap;
            picBoxGraph.Refresh();
            Analyzer_A.AxisDeg = 0;
        }



        void BtnEprClick(object sender, EventArgs e)
        {
            int nHorizPixels, nVertPixels, nX, nY;
            bool bResultAlice, bResultBob;
            Pen MyPenB = new Pen(Color.Cyan, 1);
            System.Drawing.Bitmap ScreenBitmap = new Bitmap(picBoxGraph.ClientSize.Width, picBoxGraph.ClientSize.Height);
            System.Drawing.Graphics DisplayGraphics = System.Drawing.Graphics.FromImage(ScreenBitmap);
            DisplayGraphics.Clear(Color.DarkSlateBlue);
            picBoxGraph.Image = ScreenBitmap;
            nHorizPixels = picBoxGraph.ClientSize.Width - 2;
            nVertPixels = picBoxGraph.ClientSize.Height - 2;
            picBoxGraph.Refresh();
            clPhoton MyPhotonAlice = new clPhoton(analyzeMethod);
            clPhoton MyPhotonBob = new clPhoton(analyzeMethod);
            double dThetaAliceAxis = 0;
            Analyzer_A.AxisDeg = dThetaAliceAxis;
            double dThetaBobAxis = 0;
            for (int nAxisSteps = 0; nAxisSteps <= nHorizPixels; nAxisSteps++)
            {
                dThetaBobAxis = (double)nAxisSteps / (double)nHorizPixels;
                dThetaBobAxis *= 180;
                Analyzer_B.AxisDeg = dThetaBobAxis;
                int nYes = 0;
                int nNo = 0;
                //bool bShow = ((nAxisSteps % 100) == 0);
                for (int nPhotonAngle = 0; nPhotonAngle < 3600; nPhotonAngle++)
                {
                    double dPhotonAngle = ((double)nPhotonAngle)*Math.PI/ 1800.0;
                    //bool bShowPhasor = (bShow && ((nPhotonAngle % 10) == 0));
                    //for (int nPhotonPhase = 0; nPhotonPhase < 360; nPhotonPhase++)
                    //	double dPhotonPhase = (double)nPhotonPhase/ 1.0;
                    
                    MyPhotonAlice.MakeCircular(dPhotonAngle, true,0);
                    MyPhotonBob.MakeCircular(dPhotonAngle, false,0);
                    
                    //MyPhotonAlice.MakeElliptical(dPhotonAngle, EprMath.halfPI, Math.PI/2.0, true);
                    //MyPhotonBob.MakeElliptical(dPhotonAngle, EprMath.halfPI, Math.PI, false);
                    bResultAlice = (MyPhotonAlice.Analyze(Analyzer_A, false,false,null,null));
                    bResultBob = (MyPhotonBob.Analyze(Analyzer_B, false, false, null,null));
                    if (bResultAlice == bResultBob)
                        nYes++;
                    else
                        nNo++;
                }
                nX = 1 + nAxisSteps;
                double sin2theta = (Math.Cos(dThetaBobAxis * Math.PI / 90));
                sin2theta = ((sin2theta + 1.0) / 2);
                nY = (int)(nVertPixels * sin2theta);
                if (nY > 0)
                    ScreenBitmap.SetPixel(nX, nY - 1, Color.Black);
                if (nY < nVertPixels)
                    ScreenBitmap.SetPixel(nX, nY + 1, Color.Black);
                ScreenBitmap.SetPixel(nX, nY, Color.Black);
                nY = nVertPixels - ((nYes * nVertPixels) / (3600));
                ScreenBitmap.SetPixel(nX, nY, Color.Coral);
                Application.DoEvents();
                if ((nAxisSteps % 5) == 0)
                {
                    picBoxGraph.Image = ScreenBitmap;
                    picBoxGraph.Refresh();
                }
            }
            picBoxGraph.Image = ScreenBitmap;
            picBoxGraph.Refresh();
            Analyzer_B.AxisDeg = 0;
        }

// Select Polarization Analyzer Model
        private void rbRotation_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRotation.Checked)
            {
                rbPhasors.Checked = false;
                analyzeMethod = AnalyzeMethod.Orientation;
                UpDatePhasorDisplay(sourcePhaseDeg, _photonAzimuthDeg);
            }
        }

        private void rbPhasors_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPhasors.Checked)
            {
                rbRotation.Checked = false;
                analyzeMethod = AnalyzeMethod.Phasors;
                UpDatePhasorDisplay(sourcePhaseDeg, _photonAzimuthDeg);
            }

        }

        private void rbLinear_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLinear.Checked)
            {
                rbCircular.Checked = false;
                isLinear = true;
                UpDatePhasorDisplay(sourcePhaseDeg, _photonAzimuthDeg);
            }
        }

        private void rbCircular_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCircular.Checked)
            {
                rbLinear.Checked = false;
                isLinear = false;
                UpDatePhasorDisplay(sourcePhaseDeg, _photonAzimuthDeg);
            }
        }

        private void lblPhasor2Theta_Click(object sender, EventArgs e)
        {

        }

    }
}
