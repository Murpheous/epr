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
        double sourceAxis;
        double sourceAzimuthDeg;
        double sourcePhaseDeg;
        clFilter Analyzer_A;
        clFilter Analyzer_B;
        public ControlPanel()
        {
            InitializeComponent();
            UpdateUi();
        }
        public void UpdateUi()
        {
            lblSourceAxis.Text = sourceAxis.ToString();
            lblPhi.Text = string.Format("{0}°", sourcePhaseDeg);
            lblSourceAzimuth.Text = string.Format("{0}°", sourceAzimuthDeg);
        }

        private void ControlPanel_Load(object sender, EventArgs e)
        {
            Analyzer_A = new clFilter(0);
            Analyzer_B = new clFilter(0);
            Analyzer_A.ShowDial(pictureBox1);
            Analyzer_B.ShowDial(pictureBox2);
            trackBarTheta.Value = (trackBarTheta.Maximum - trackBarTheta.Minimum)/2;
            UpdateUi();
        }

        private void trackBarTheta_Scroll(object sender, EventArgs e)
        {
            sourceAxis = ((double)trackBarTheta.Value - trackBarTheta.Maximum / 2.0) / 2.0;
            UpdateUi();
            UpDatePhasorDisplay(sourcePhaseDeg, sourceAzimuthDeg);
        }
        private void tbPhase_Scroll(object sender, EventArgs e)
        {
            sourcePhaseDeg = (tbPhase.Value - 360.0) / 2.0;
            UpdateUi();
            UpDatePhasorDisplay(sourcePhaseDeg, sourceAzimuthDeg);
        }

        private void trackPhi2_Scroll(object sender, EventArgs e)
        {
            Analyzer_B.AxisDeg = (double)(trackPhi2.Value - 180.0) / 2.0;
            lblFilterAz2.Text = (Analyzer_B.AxisDeg).ToString();
            Analyzer_B.ShowDial(pictureBox2);
            UpDatePhasorDisplay(sourcePhaseDeg, sourceAzimuthDeg);
        }

        private void trackPhi1_Scroll(object sender, EventArgs e)
        {
            Analyzer_A.AxisDeg = (double)(trackPhi1.Value - 180) / 2.0;
            lblFilterAz1.Text = (Analyzer_A.AxisDeg).ToString();
            Analyzer_A.ShowDial(pictureBox1);
            UpDatePhasorDisplay(sourcePhaseDeg, sourceAzimuthDeg);
        }

        private void tbTwist_Scroll(object sender, EventArgs e)
        {
            sourceAzimuthDeg = (tbTwist.Value);
            UpdateUi();
            UpDatePhasorDisplay(sourcePhaseDeg, sourceAzimuthDeg);
        }

 
        private void UpDatePhasorDisplay(double srcPhaseDeg, double srcAzDeg )
        {
            this.SuspendLayout();
            clPhoton PhotonA = new clPhoton();
            clPhoton PhotonB = new clPhoton();
             // Now do an EPR visualisation
            PhotonA.MakeElliptical(sourceAxis * (Math.PI / 180.0), srcAzDeg * (Math.PI / 180.0), srcPhaseDeg * (Math.PI / 180.0), true);
            PhotonB.MakeElliptical(sourceAxis * (Math.PI / 180.0), srcAzDeg * (Math.PI / 180.0), srcPhaseDeg * (Math.PI / 180.0), true);
            Analyzer_A.ShowDial();
            int nResultA = PhotonA.Analyze(Analyzer_A, true, Color.Azure);
            Analyzer_B.ShowDial();
            int nResultB = PhotonB.Analyze(Analyzer_B, true, Color.Azure);
            lblAnalyzer1Result.Text = nResultA.ToString();
            lblAnalyzer2Result.Text = nResultB.ToString();
            this.ResumeLayout();
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
            clPhoton MyPhoton = new clPhoton();
            for (nAxisSteps = 0; nAxisSteps <= nHorizPixels; nAxisSteps++)
            {

                dThetaAxis = (double)nAxisSteps / (double)nHorizPixels;
                dThetaAxis *= 180;
                Analyzer_A.AxisDeg = dThetaAxis;
                int nNo = 0;
                int nYes = 0;
                if (Analyzer_A.AxisDeg > 90)
                {
                    nYes = 0;
                }
                for (nPhiSteps = 0; nPhiSteps < 3600; nPhiSteps++)
                {
                    double dPhi = (double)nPhiSteps / 10.0;
                    MyPhoton.MakeLinear(0, dPhi * (Math.PI / 180.0));
                    if (MyPhoton.Analyze(Analyzer_A, false) > 0)
                        nYes++;
                    else
                        nNo++;
                }
                nX = 1 + nAxisSteps;
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
            clPhoton MyPhotonAlice = new clPhoton();
            clPhoton MyPhotonBob = new clPhoton();
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
                bool bShow = ((nAxisSteps % 100) == 0);
                for (int nPhotonAngle = 0; nPhotonAngle < 3600; nPhotonAngle++)
                {
                    double dPhotonAngle = (double)nPhotonAngle / 10.0;
                    bool bShowPhasor = (bShow && ((nPhotonAngle % 10) == 0));
                    //for (int nPhotonPhase = 0; nPhotonPhase < 360; nPhotonPhase++)
                    //	double dPhotonPhase = (double)nPhotonPhase/ 1.0;
                    MyPhotonAlice.MakeCircular(dPhotonAngle * (Math.PI / 180.0), true, 0);//dPhotonPhase);
                    MyPhotonBob.MakeCircular((dPhotonAngle) * (Math.PI / 180.0), false, 0);//dPhotonPhase);
                    bResultAlice = (MyPhotonAlice.Analyze(Analyzer_A, bShowPhasor) > 0);
                    bResultBob = (MyPhotonBob.Analyze(Analyzer_B, bShowPhasor) > 0);
                    if (bResultAlice == bResultBob)
                        nYes++;
                    else
                        nNo++;
                    if (bShowPhasor)
                        System.Threading.Thread.Sleep(1);
                }
                nX = 1 + nAxisSteps;
                nY = nVertPixels - ((nYes * nVertPixels) / (3600));
                ScreenBitmap.SetPixel(nX, nY, Color.Coral);
                Application.DoEvents();
                if ((nAxisSteps % 5) == 0)
                {
                    picBoxGraph.Image = ScreenBitmap;
                    picBoxGraph.Refresh();
                    if (bShow)
                    {
                        Analyzer_A.ShowDial();
                        Analyzer_B.ShowDial();
                    }
                }
            }
            picBoxGraph.Image = ScreenBitmap;
            picBoxGraph.Refresh();
            Analyzer_B.AxisDeg = 0;
        }

        private void btnDiffraction_Click(object sender, EventArgs e)
        {
        }

        private void btnRunScatter_Click(object sender, EventArgs e)
        {
        }

    }
}
