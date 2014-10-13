using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;

namespace EprGrapics
{

    class clPhasor
    {
        double _phaseAngle;
        bool _bPhaseSense;
        double _sourceAxis;
        double _sourceAzimuth;
        
        double _phasorMapped;
        double _phaseLowOnAnalyzer;
        double _phaseMidOnAnalyzer;
        double _phaseTopOnAnalyzer;

        public double MappedPhasor
        {
        	get {return _phasorMapped;}
        }
        public double PhaseCieling
        {
            get { return _phaseTopOnAnalyzer; }
        }
        public double PhaseMid
        {
            get { return _phaseMidOnAnalyzer; }
        }
        public double PhaseFloor
        {
            get { return _phaseLowOnAnalyzer; }
        }

        public int nPhaseSense
		{
			get {return (_bPhaseSense) ? 1:-1;}
			set {_bPhaseSense = (value > 0);}
		}
        public bool PhaseSense
        {
            get { return _bPhaseSense; }
            set { _bPhaseSense = value; }
        }
		public int nSourceAxisSense
		{
			get {return (_bPhaseSense) ? 1:-1;}
			set {_bPhaseSense = (value >= 0);}
		}
        public double phaseAngle
        {
            get {return _phaseAngle;}
            set { _phaseAngle = EprMath.Limit180(value); }
        }

        public double phaseAngleDeg
        {
            get { return _phaseAngle * 180.0 / Math.PI; }
            set { _phaseAngle = EprMath.Limit180(((value * Math.PI)/180.0)); }
        }

        public double sourceAxis
        {
            get { return _sourceAxis; }
            set { _sourceAxis = EprMath.Limit180(value);}
        }
        public double sourceAxisDeg
        {
            get { return _sourceAxis*180.0/Math.PI; }
        }

        public double sourceAzimuth
        {
            get { return _sourceAzimuth; }
            set { _sourceAzimuth = EprMath.Limit90(value); }

        }

        public double sourceAzimuthDeg
        {
            get { return _sourceAzimuth * 180.0 / Math.PI; }
        }
        public double SignedVectorAngle(Vector3 referenceVector, Vector3 otherVector, Vector3 normalVector)
        {
            double dot = Vector3.DotProduct(referenceVector, otherVector);
            if (dot > 1.0)
                dot = 1.0;
            if (dot < -1.0)
                dot = -1.0;
            double result = Math.Acos(dot);
            if (result != 0.0)
            {
                Vector3 perpVector = Vector3.CrossProduct(normalVector, referenceVector);
                if (Vector3.DotProduct(perpVector, otherVector) < 0.0)
                    result = -result;
            }
            return result;
        }

        public bool Analyze( clFilter analyzer)
        {
            bool bResult = true;
            double analyzerAxis;
            double incidentAxis;
            Vector3 upAxis = new Vector3(0, 1, 0);
            Vector3 sideAxis = new Vector3(0, 0, 1);
            Vector3 throughAxis = new Vector3(1, 0, 0);
            // Check the difference is less than 90 degrees, if so, tweak to keep in +- 90
            analyzerAxis = EprMath.Limit90(analyzer.Axis);
            incidentAxis = EprMath.Limit90(sourceAxis);
            double thetaOffset = 2.0 * (incidentAxis - analyzerAxis);


            // ************** From here we deal with the analyzer map
            // Now we can calculate the phasor on the 2D Yes/No map of the analyzer from the spin axis vector and the mapped phase.
            double phaseRange = EprMath.halfPI + Math.Abs(sourceAzimuth);
            // Phase zero and +-180 maps to centre, +- 90 to upper lower 
            _phaseTopOnAnalyzer = phaseRange + thetaOffset;
            _phaseLowOnAnalyzer = -phaseRange + thetaOffset;
            _phaseMidOnAnalyzer = EprMath.ExtendedAsin(thetaOffset/EprMath.halfPI);
            double phaseMin = EprMath.ExtendedSin(_phaseLowOnAnalyzer);
            double phaseMax = EprMath.ExtendedSin(_phaseTopOnAnalyzer);
            double phaseDelta = phaseMax - phaseMin; // We scale phase +- 180 to 
            double phaseMappedDown = ((phaseAngle + Math.PI)/EprMath.twoPI);
            phaseMappedDown = (phaseMappedDown * phaseDelta) + phaseMin;
            _phasorMapped = EprMath.Limit180(EprMath.ExtendedAsin(phaseMappedDown));
            bResult = ((_phasorMapped > (-EprMath.halfPI)) && (_phasorMapped <= EprMath.halfPI));
            return bResult;
        }

        public clPhasor(double srcAxis, double srcAzimuth, bool PhaseSense, double srcPhase)
        {
            _bPhaseSense = PhaseSense;
            phaseAngle = srcPhase;
            sourceAxis = srcAxis;
            sourceAzimuth = srcAzimuth;
        }

     }

    class clPhoton
    {
        private clPhasor phasor;

        System.Collections.Generic.List<clPhasor> Phasors = new System.Collections.Generic.List<clPhasor>();
        public void MakeElliptical(double sourceAxis, double sourceAzimuth, double sourcePhase, bool sourceSense)
        {
            phasor = new clPhasor(sourceAxis, sourceAzimuth, sourceSense, sourcePhase);
        }
        public void MakeLinear(double sourceAxis, double sourcePhase)
        {
            MakeElliptical(sourceAxis, 0.0, sourcePhase, true);
        }
        public void MakeLinear(double sourceAxis, bool isClockwise, double sourcePhase)
        {
            MakeElliptical(sourceAxis, 0.0, sourcePhase, isClockwise);
        }
        public void MakeCircular(double sourceAxis, bool isClockwise, double sourcePhase)
        {
           int tmpSign = (isClockwise ? 1 : -1);
           MakeElliptical(sourceAxis, EprMath.halfPI * tmpSign, sourcePhase, isClockwise);
        }

        public int Analyze(clFilter Target, bool bShow, Color PenColour)
        {
            int nAnswer = 1;
             if (phasor == null)
                return 0;
             if (!phasor.Analyze(Target))
                nAnswer = -nAnswer;
             if (bShow && Target.GotPicture())
                Target.ShowMapping(phasor, (nAnswer > 0) ? PenColour : Color.Gold);
            return nAnswer;
        }

        public int Analyze(clFilter Target, bool bShow)
        {
            return (Analyze(Target,bShow,Color.Cyan));
        }

        public clPhoton()
        {
            phasor = new clPhasor(0,0,true,0);
        }
    }
    class clFilter
    {
        double dFilterAxis;
        System.Windows.Forms.PictureBox MyPicture;

        public double Axis
        {
            get { return dFilterAxis; }
            set { dFilterAxis = EprMath.Limit180(value);}
        }
        public double AxisDeg
        {
            get { return dFilterAxis*180.0/Math.PI; }
            set { dFilterAxis = EprMath.Limit180(value*Math.PI/180.0); }
        }
        public void ShowMapping(clPhasor MappedPhasor, Color PenColor)
        {
            int nCentreX, nCentreY, nRadius, nX, nY;
            Pen MyPenB = new Pen(PenColor, 2);
            if (MyPicture == null)
                return;
            System.Drawing.Bitmap MyBitmap = new Bitmap(MyPicture.Image);
            System.Drawing.Graphics MyGraphics = System.Drawing.Graphics.FromImage(MyBitmap);
            nCentreX = MyPicture.ClientSize.Width / 2;
            nCentreY = MyPicture.ClientSize.Height / 2;
            Point PtCentre = new Point(nCentreX, nCentreY);
            nRadius = Math.Min(nCentreY, nCentreX) - 3;
            nX = (int)(Math.Round(nRadius * Math.Sin(dFilterAxis*2.0 + MappedPhasor.MappedPhasor)));
            nY = (int)(Math.Round(nRadius * Math.Cos(dFilterAxis*2.0 + MappedPhasor.MappedPhasor)));
            Point PtEnd = new Point(nCentreX + nX, nCentreY - nY);
            MyGraphics.DrawLine(MyPenB, PtCentre, PtEnd);
            MyPenB.Color = Color.BlueViolet;
            nX = (int)(Math.Round(nRadius * Math.Sin(dFilterAxis * 2.0 + MappedPhasor.PhaseFloor)));
            nY = (int)(Math.Round(nRadius * Math.Cos(dFilterAxis * 2.0 + MappedPhasor.PhaseFloor)));
            PtEnd = new Point(nCentreX + nX, nCentreY - nY);
            MyGraphics.DrawLine(MyPenB, PtCentre, PtEnd);
            MyPenB.Color = Color.Bisque;
            nX = (int)(Math.Round(nRadius * Math.Sin(dFilterAxis * 2.0 + MappedPhasor.PhaseCieling)));
            nY = (int)(Math.Round(nRadius * Math.Cos(dFilterAxis * 2.0 + MappedPhasor.PhaseCieling)));
            PtEnd = new Point(nCentreX + nX, nCentreY - nY);
            MyGraphics.DrawLine(MyPenB, PtCentre, PtEnd);
            MyPicture.Image = MyBitmap;
            MyPicture.Refresh();
            
        }
        

        public bool GotPicture()
        {
            return (MyPicture != null);
        }
        public void ShowDial()
        {
            if (!GotPicture())
                return;
            ShowDial(MyPicture);
        }
        public void ShowDial(System.Windows.Forms.PictureBox ArgPicture)
        {
            double thetaDeg;
            double fracTheta, mappedThetaRad;
            int nCentreX, nCentreY, nRadius, nX, nY;
            Pen MyPenA = new Pen(Color.DarkSalmon,1);
            Pen MyPenB = new Pen(Color.DarkSeaGreen, 1);
            Pen MyPenC = new Pen(Color.Gray, 1);

            if (ArgPicture == null) 
            {
                if (MyPicture==null)
                    return;
            }
            else
                MyPicture = ArgPicture;
            System.Drawing.Bitmap MyBitmap = new System.Drawing.Bitmap(MyPicture.ClientSize.Width,MyPicture.ClientSize.Height);
            System.Drawing.Graphics MyGraphics = System.Drawing.Graphics.FromImage(MyBitmap);
            MyGraphics.Clear(Color.Black);
            nCentreX = MyPicture.ClientSize.Width / 2;
            nCentreY = MyPicture.ClientSize.Height / 2;
            Point PtCentre = new Point(nCentreX,nCentreY);
            nRadius = Math.Min(nCentreY, nCentreX) - 3;
            for (thetaDeg = -180; thetaDeg <= 179.999; thetaDeg += 5.625)
            {
                fracTheta = (thetaDeg /90);
                mappedThetaRad =EprGrapics.EprMath.ExtendedAsin(fracTheta);
                nX = (int)(Math.Round(nRadius * Math.Sin(mappedThetaRad + 2.0 * dFilterAxis)));
                nY = (int)(Math.Round(nRadius * Math.Cos(mappedThetaRad + 2.0 * dFilterAxis)));
                double mappedThetaDeg = mappedThetaRad * 180 / Math.PI;
                Point PtEnd = new Point(nCentreX + nX, nCentreY - nY);
                if ((mappedThetaDeg < -90) || (mappedThetaDeg > 90))
                    MyGraphics.DrawLine(MyPenB, PtCentre, PtEnd);
                else
                {
                    if ((mappedThetaDeg > -90) && (mappedThetaDeg < 90))
                        MyGraphics.DrawLine(MyPenA, PtCentre, PtEnd);
                    else
                        MyGraphics.DrawLine(MyPenC, PtCentre, PtEnd);
                }
            }
            MyPicture.Image = MyBitmap;
            MyPicture.Refresh();
        }
        public clFilter()
        {
            dFilterAxis = 0;
        }
        public clFilter(double ArgAxis)
        {
            dFilterAxis = EprMath.Limit180(ArgAxis);
        }

    }
}
