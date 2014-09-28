﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;

namespace EprGrapics
{
    public static class EprMath
    {
        public const double dTwoPi = Math.PI * 2.0;
        public const double dHalfPi = Math.PI / 2.0;
        public const double dQuarterPi = Math.PI / 4.0;
        public static double Limit180(double ArgTheta)
        {
            long nPi;
            nPi = 0;
            if (ArgTheta >= Math.PI) 
            {
                nPi = (long)(Math.Truncate(ArgTheta / Math.PI));
                nPi = (nPi +1)/2;
                ArgTheta = ArgTheta - (nPi * dTwoPi);
            }
            else if (ArgTheta <= -Math.PI)
            {
                nPi = (long)(Math.Truncate(ArgTheta / Math.PI));
                nPi = (nPi -1)/2;
                ArgTheta = ArgTheta - (nPi * dTwoPi);
            }
            return ArgTheta;
        }
        public static double Limit90(double ArgTheta)
        {
            ArgTheta = Limit180(ArgTheta);
            if (ArgTheta > dHalfPi)
                return ArgTheta - Math.PI;
            if (ArgTheta < -dHalfPi)
                return ArgTheta + Math.PI;
            return ArgTheta;
        }

        public static double ExtendedAsin(double ArgInput)
        {
            double dFracPart, dResult;
            long nOffset, nIntpart;

            nOffset = 0;
            nIntpart = (long)Math.Truncate(ArgInput);
            if (nIntpart >= 1)
                nOffset = (nIntpart + 1) / 2;
            else if (nIntpart <= -1)
                nOffset = (nIntpart - 1) / 2;
            nOffset *= 2;
            dFracPart = ArgInput - nOffset;
            dResult = Math.Asin(dFracPart) + ((nOffset/2) * Math.PI);
            return dResult;
        }
        public static double ExtendedSineSq(double ArgTheta)
        {
// Function GetShift(Theta As Double) As Double
//Dim IntPart As Double, Fracpart As Double, Result As Double
            long nIntpart;
            double dFracPart;
            double dSine;
            long nOffset;
            double dOffset;
            int nSineSign = 1;
            nIntpart = (long)Math.Truncate(ArgTheta / dHalfPi);
            nOffset = 0;
            if (nIntpart >= 1)
                nOffset = (nIntpart + 1) / 2;
            else if (nIntpart <= -1)
                nOffset = (nIntpart - 1) / 2;
            nOffset *= 2;
            dOffset = nOffset * dHalfPi;
            dFracPart = ArgTheta - dOffset;
            dSine = Math.Sin(dFracPart);
            if (dSine < 0.0)
                nSineSign = -1;
            dSine *= (dSine * nSineSign);
            return dSine + nOffset;
        }
    }

    class clPhasor
    {
        double _phaseAngle;
        bool _bPhaseSense;
        double _spinAxis;
        double _spinAzimuth;
        
        double _phasorMapped;
        public double MappedPhasor
        {
        	get {return _phasorMapped;}
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

        public bool Analyze( clFilter MyAnalyzer)
        {
            bool bResult = true;
           // Choose Y axis as up, X axis as Right, and Z axis is into analyzer.
            Vector3 spinAxisVector = new Vector3(0, 1, 0);
            // Start by setting the 'spin axis' in space
            spinAxisVector.Roll(spinAxisDeg);
            // Now set the photon Axis relative to the Analyzer by roataing back in the opposite direction to the Analyzer axis in space.
            spinAxisVector.Roll(-MyAnalyzer.AxisDeg);
            // Now get the phase vector by first setting it to the zero phase position (i.e aligned with the spin axis).
            Vector3 phaseVector = new Vector3(spinAxisVector);
            // Now rotate the phaseVector about the spin Axis by the Azimuth angle of the photon
            phaseVector.Yaw(spinAzimuthDeg);
            // Now rotate the phaseVector about its Z axis by the phase angle
            phaseVector.Roll(phaseAngleDeg);
            // Now we need a new 'phase' angle which is the angle betweem the phase vector and the analyer Z vector.
            Vector3 zVect = new Vector3(0,0,1);
            double dot = Vector3.DotProduct(zVect, phaseVector);
            double mappedPhase = Math.Acos(dot);
            if (dot != 0.0)
            {
                Vector3 cross = Vector3.CrossProduct(zVect, phaseVector);
                if (Vector3.DotProduct(zVect, phaseVector) < 0.0)
                    mappedPhase = -mappedPhase;
            }
            return bResult;
        }

        public clPhasor(double SpinAxis, double SpinAzimuth, bool PhaseSense, double dArgPhase)
        {
            _bPhaseSense = PhaseSense;
            phaseAngle = dArgPhase;
            spinAxis = SpinAxis;
            spinAzimuth = SpinAzimuth;
        }

     }
    class clPhoton
    {
        private clPhasor phasor;

        System.Collections.Generic.List<clPhasor> Phasors = new System.Collections.Generic.List<clPhasor>();
        public void MakeLinear(double argSourceAxis, double dArgPhase)
        {
            phasor = new clPhasor(argSourceAxis,0.0, true, dArgPhase);
        }
       public void MakeCircular( double argSourceAxis, bool argbPhaseSense, double dArgPhase)
        {
           
           phasor = new clPhasor( argSourceAxis, EprMath.dHalfPi, argbPhaseSense, dArgPhase);
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
            nX = (int)(Math.Round(nRadius * Math.Sin(dFilterAxis*2.0 - MappedPhasor.MappedPhasor)));
            nY = (int)(Math.Round(nRadius * Math.Cos(dFilterAxis*2.0 - MappedPhasor.MappedPhasor)));
            Point PtEnd = new Point(nCentreX + nX, nCentreY - nY);
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
            int nTheta;
            double dTheta, dMappedTheta;
            int nCentreX, nCentreY, nRadius, nX, nY;
            Pen MyPenA = new Pen(Color.Coral,1);
            Pen MyPenB = new Pen(Color.Cyan, 1);

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
            for (nTheta = -180; nTheta <= 179; nTheta+=3)
            {
                dTheta = (nTheta * Math.PI)/180.0;
                dMappedTheta =(EprGrapics.EprMath.ExtendedSineSq(dTheta))*Math.PI+dFilterAxis*2.0;
                nX = (int)(Math.Round(nRadius * Math.Sin(dMappedTheta)));
                nY = (int)(Math.Round(nRadius * Math.Cos(dMappedTheta)));
                Point PtEnd = new Point(nCentreX + nX, nCentreY - nY);
                //MyBitmap.SetPixel(nCentreX + nX, nCentreY - nY, Color.Coral);
                MyGraphics.DrawLine(MyPenA, PtCentre, PtEnd);
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
