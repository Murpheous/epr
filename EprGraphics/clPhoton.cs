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
        double _spinAxis;
        double _spinAzimuth;
        
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

        public double spinAxis
        {
            get { return _spinAxis; }
            set { _spinAxis = EprMath.Limit180(value);}
        }
        public double spinAxisDeg
        {
            get { return _spinAxis*180.0/Math.PI; }
        }

        public double spinAzimuth
        {
            get { return _spinAzimuth; }
            set { _spinAzimuth = EprMath.Limit90(value); }

        }

        public double spinAzimuthDeg
        {
            get { return _spinAzimuth * 180.0 / Math.PI; }
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
            int nFlip = 0;
            Vector3 upAxis = new Vector3(0, 1, 0);
            Vector3 sideAxis = new Vector3(0, 0, 1);
            Vector3 throughAxis = new Vector3(1, 0, 0);

           // Choose Y axis as up, Z axis as Left, and X axis is into analyzer.
            Vector3 spinAxisVector = new Vector3(upAxis);
            // Start by setting the 'spin axis' in space
            spinAxisVector.RotateAroundThrough(spinAxis);
            // Now set the photon Axis relative to the Analyzer by rotataing back in the opposite direction to the Analyzer axis in space.
            spinAxisVector.RotateAroundThrough(-analyzer.Axis);
            // We need to treat the vector as an Axis, so if it points downward (below Y), flip it 180 degrees in the XY plane.
            if (spinAxisVector.Y < 0)
            {  // Flip Axis vector 180 degrees around X by changing sign of Y & Z
                spinAxisVector.Y = -spinAxisVector.Y;
                spinAxisVector.Z = -spinAxisVector.Z;
            }
            // Now if the Axis is outside +- 45º we flip to reference the B side of the analyzer
            double zLim = 1.0/Math.Sqrt(2.0);
            if (spinAxisVector.Z > zLim)
                nFlip = 1;
            else if (spinAxisVector.Z < -zLim)
                nFlip = -1;
            if (nFlip != 0)
            { // Rotate 90 degrees by Tansposing Y and Z, keeping Y positive.
                double tmpZ;
                tmpZ = spinAxisVector.Y * -nFlip;
                spinAxisVector.Y = spinAxisVector.Z * nFlip;
                spinAxisVector.Z = tmpZ;
            }
            // Now get the phase vector by first setting it to the zero phase position (i.e aligned with the spin axis).
            Vector3 phaseVector = new Vector3(spinAxisVector);
            // Now rotate the phaseVector about the spin Axis by the Azimuth angle of the photon
            if (Math.Abs(spinAzimuth) < EprMath.halfPI)
                phaseVector.RotateAroundUp(Math.Sign(spinAzimuth)*(EprMath.halfPI - Math.Abs(spinAzimuth)));
            // Now rotate the phaseVector about its local 'Through' axis by the phase angle
            phaseVector.RotateAroundZ(phaseAngle);

            // ************** From here we deal with the analyzer map
            // Now we can calculate the phasor on the 2D Yes/No map of the analyzer from the spin axis vector and the mapped phase.
            double thetaOffset = 2.0*(spinAxis - analyzer.Axis);
            double phaseRange = EprMath.halfPI + Math.Abs(spinAzimuth);
            // Phase zero and +-180 maps to centre, +- 90 to upper lower 
            _phaseTopOnAnalyzer = phaseRange + thetaOffset;
            _phaseLowOnAnalyzer = -phaseRange + thetaOffset;
            _phaseMidOnAnalyzer = EprMath.ExtendedAsin(thetaOffset/EprMath.halfPI);
            double phaseMin = EprMath.ExtendedSin(_phaseLowOnAnalyzer);
            double phaseMax = EprMath.ExtendedSin(_phaseTopOnAnalyzer);
            double phaseDelta = phaseMax - phaseMin; // We scale phase +- 180 to 
            double phaseMappedDown = ((phaseAngle + Math.PI)/EprMath.twoPI);
            phaseMappedDown = (phaseMappedDown * phaseDelta) + phaseMin;
            _phasorMapped = EprMath.ExtendedAsin(phaseMappedDown);
            bResult = ((_phasorMapped > (-EprMath.halfPI)) && (_phasorMapped <= EprMath.halfPI));
            return bResult;
        }

        public clPhasor(double SpinAxis, double SpinAzimuth, bool PhaseSense, double sourcePhase)
        {
            _bPhaseSense = PhaseSense;
            phaseAngle = sourcePhase;
            spinAxis = SpinAxis;
            spinAzimuth = SpinAzimuth;
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
                Target.ShowMapping(phasor, (nAnswer > 0) ? PenColour : Color.Red);
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
            Pen MyPenB = new Pen(PenColor, 1);
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
            MyPenB.Color = Color.OrangeRed;
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
