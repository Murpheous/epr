using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EprGrapics
{
    enum AnalyzeMethod { Rotation = 0, Phasors = 1 };
    enum AnalyzeChannel { Alice = 0, Bob = 1};

    class clPhasor
    {
 
        double _axis;
        double _azimuth;
        double _phase;
        bool _isClockwise;
        
        double _phasorResult;
        double _axisResult;
        double _phaseCieling;
        double _phaseFloor;
        int    _axisFlip;

        public double PhasorResult
        {
        	get {return _phasorResult;}
        }

        public AnalyzeChannel ReferenceChannel
        {
            get { return (_axisFlip % 2 == 0) ? AnalyzeChannel.Alice : AnalyzeChannel.Bob; }
        }
        public double PhaseCieling
        {
            get { return _phaseCieling; }
        }
        public double AxisResult
        {
            get { return _axisResult; }
        }
        public double PhaseFloor
        {
            get { return _phaseFloor; }
        }

        public bool IsClockwise
        {
            get { return _isClockwise; }
            set { _isClockwise = value; }
        }
        public int PhaseSense
        {
            get { return _isClockwise ? 1 : -1; }
        }
        public double Phase
        {
            get {return  _phase;}
            set { _phase = EprMath.Limit180(value); }
        }

        public double Axis
        {
            get { return _axis; }
            set { _axis = EprMath.Limit180(value);}
        }
        public double sourceAxisDeg
        {
            get { return _axis*180.0/Math.PI; }
        }

        public double sourceAzimuth
        {
            get { return _azimuth; }
            set { _azimuth = EprMath.Limit90(value); }

        }

        public double sourceAzimuthDeg
        {
            get { return _azimuth * 180.0 / Math.PI; }
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

        public bool AnalyzeLinear(clFilter analyzer)
        {
            bool bResult = true;
            // Check the difference is less than 90 degrees, if so, tweak to keep in +- 90
            double analyzerAxis = EprMath.Limit90(analyzer.Axis);
            double incidentAxis = EprMath.Limit90(Axis);
            double axisDelta = 2.0 * (incidentAxis - analyzerAxis);


            // ************** From here we deal with the analyzer map
            // Now we can calculate the phasor on the 2D Yes/No map of the analyzer from the spin axis vector and the mapped phase.
            double phaseRange = EprMath.halfPI + EprMath.halfPI * (1 - Math.Pow(Math.Cos(sourceAzimuth), 2.0));
            // Phase zero and +-180 maps to centre, +- 90 to upper lower 
            _phaseCieling = phaseRange + axisDelta;
            _phaseFloor = -phaseRange + axisDelta;
            _axisResult = EprMath.ExtendedAsin(axisDelta / EprMath.halfPI);
            double phaseMin = EprMath.ExtendedSine(_phaseFloor);
            double phaseMax = EprMath.ExtendedSine(_phaseCieling);
            double phaseDelta = phaseMax - phaseMin; // We scale phase +- 180 to 
            double phaseMappedDown = ((Phase + Math.PI) / EprMath.twoPI);
            phaseMappedDown = (phaseMappedDown * phaseDelta) + phaseMin;
            _phasorResult = EprMath.Limit180(EprMath.ExtendedAsin(phaseMappedDown));
            bResult = ((_phasorResult > (-EprMath.halfPI)) && (_phasorResult <= EprMath.halfPI));
            return bResult;

        }

        public bool AnalyzeCircular(clFilter analyzer)
        {
           bool bResult = true;


           // Check the difference is less than 90 degrees, if so, tweak to keep in +- 90
           double analyzerAxis = EprMath.Limit90(analyzer.Axis);
           double incidentAxis = EprMath.Limit90(Axis);
           double axisDelta = EprMath.Limit90(incidentAxis - analyzerAxis);
           _axisFlip = 0;
           if (axisDelta < -EprMath.quarterPI)
               _axisFlip = 1;
           else if (axisDelta >= EprMath.quarterPI)
               _axisFlip = -1;
           axisDelta += EprMath.halfPI * _axisFlip;
           // Generate new 'axis' vector aligned either with Analyzer A or B axis, depending on whether one or other is closest
          
            _axisResult = _axisFlip * Math.PI;
           double axisDeltaDeg = axisDelta * 180 / Math.PI; // For debug
           // Calculate axisDelta as a fraction of 90
           double shiftSinSq = EprMath.ExtendedSineSq(axisDelta)*Math.PI;
           double phaseDelta = (shiftSinSq - shiftSinSq * PhaseSense)/2.0;
           double effectivePhase = Phase*PhaseSense + phaseDelta/2.0;
           _phasorResult = EprMath.Limit180(_axisResult + EprMath.ExtendedSineSq(effectivePhase) * Math.PI);
            bResult = ((_phasorResult <= EprMath.halfPI) && (_phasorResult > -EprMath.halfPI));
            return bResult;
        }

        public bool Analyze( clFilter analyzer, AnalyzeMethod method)
        {
            switch (method)
            {
                case AnalyzeMethod.Rotation:
                    {
                    return AnalyzeLinear(analyzer);
                    }
                case AnalyzeMethod.Phasors:
                    {
                        return AnalyzeCircular(analyzer);
                    }
                default:
                    break;
            }

             return false;
        }

        public clPhasor(double srcAxis, double srcAzimuth, bool IsClockwise, double srcPhase)
        {
            _isClockwise = IsClockwise;
            Phase = srcPhase;
            Axis = srcAxis;
            sourceAzimuth = srcAzimuth;
        }

     }

    class clPhoton
    {
        List<clPhasor> Phasors = new List<clPhasor>(); 
               
        AnalyzeMethod _method = AnalyzeMethod.Rotation;

        public AnalyzeMethod Method
        {
            set
            {
                if (AnalyzeMethod.IsDefined(typeof(AnalyzeMethod), value))
                    _method = value;
                else
                    _method = AnalyzeMethod.Rotation;
            }
            get { return _method; }
        }

        public void MakeElliptical(double axis, double sourceAzimuth, double sourcePhase, bool sourceSense)
        {
            if (Method == AnalyzeMethod.Rotation)
            { 
                if (Phasors.Count() > 0)
                    Phasors.Clear();
                Phasors.Add(new clPhasor(axis, sourceAzimuth, sourceSense, sourcePhase));
            }
            else if (Method == AnalyzeMethod.Phasors)
            {
                if (Phasors.Count() > 0)
                    Phasors.Clear();
                if (sourceAzimuth == 0.0)
                {
                    Phasors.Add(new clPhasor(axis, EprMath.halfPI, sourceSense, sourcePhase));
                    Phasors.Add(new clPhasor(axis, -EprMath.halfPI, !sourceSense, sourcePhase));
                }
                if (Math.Abs(sourceAzimuth) == EprMath.halfPI)
                {
                    Phasors.Add(new clPhasor(axis, EprMath.halfPI, sourceSense, sourcePhase));
                }
            }
        }
        public void MakeLinear(double axis, double sourcePhase)
        {
            MakeElliptical(axis, 0.0, sourcePhase, true);
        }
        public void MakeLinear(double axis, bool isClockwise, double sourcePhase)
        {
            MakeElliptical(axis, 0.0, sourcePhase, isClockwise);
        }
        public void MakeCircular(double axis, bool isClockwise, double sourcePhase)
        {
           int tmpSign = (isClockwise ? 1 : -1);
           MakeElliptical(axis, EprMath.halfPI * tmpSign, sourcePhase, isClockwise);
        }

        public bool Analyze(clFilter Target, bool bShow, bool ShowLimits, Label lblPhasor)
        {
            List <int> answers = new List<int>(Phasors.Count());
            AnalyzeChannel refChannel = AnalyzeChannel.Alice;
            foreach (clPhasor phasor in Phasors)
            { 
                answers.Add(phasor.Analyze(Target, Method) ? 1 : -1);
                refChannel = phasor.ReferenceChannel;
            }
            int finalanswer = 1;
            foreach (int n in answers)
            {
                finalanswer *= n;
            }
            if (bShow && Target.GotPicture())
            {
                Target.ShowMapping(Phasors, ShowLimits, lblPhasor, Method);
            }
            bool isAlice = (finalanswer > 0);
            if (refChannel != AnalyzeChannel.Alice)
                isAlice = !isAlice;
            return isAlice;
        }

        public bool Analyze(clFilter Target, bool bShow, Label lblPhasor)
        {
            return (Analyze(Target,bShow,true, lblPhasor));
        }

        public clPhoton(AnalyzeMethod analyzeMethod)
        {
            Method = analyzeMethod;
        }
        public clPhoton()
        {
            Method = AnalyzeMethod.Rotation;
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
        public void ShowMapping(List<clPhasor> mappedPhasors, bool ShowAll, Label lblPhasor, AnalyzeMethod method)
        {
            int nCentreX, nCentreY, nRadius, nX, nY;
            Pen MyPenB = new Pen(Color.LightCyan, 2);
            if ((MyPicture == null) || (mappedPhasors.Count() < 1))
                    return;
            System.Drawing.Bitmap MyBitmap = new Bitmap(MyPicture.Image);
            System.Drawing.Graphics MyGraphics = System.Drawing.Graphics.FromImage(MyBitmap);
            nCentreX = MyPicture.ClientSize.Width / 2;
            nCentreY = MyPicture.ClientSize.Height / 2;
            Point PtCentre = new Point(nCentreX, nCentreY);
            nRadius = Math.Min(nCentreY, nCentreX) - 3;
            Point PtEnd;
            foreach (clPhasor ph in mappedPhasors)
            {
                nX = (int)(Math.Round(nRadius * Math.Sin(dFilterAxis * 2.0 + ph.PhasorResult)));
                nY = (int)(Math.Round(nRadius * Math.Cos(dFilterAxis * 2.0 + ph.PhasorResult)));
                PtEnd = new Point(nCentreX + nX, nCentreY - nY);
                MyGraphics.DrawLine(MyPenB, PtCentre, PtEnd);
            }
            if (!ShowAll) 
                return;
            nX = (int)(Math.Round(nRadius * Math.Sin(dFilterAxis * 2.0 + mappedPhasors[0].AxisResult)));
            nY = (int)(Math.Round(nRadius * Math.Cos(dFilterAxis * 2.0 + mappedPhasors[0].AxisResult)));
            PtEnd = new Point(nCentreX + nX, nCentreY - nY);
            if ((mappedPhasors[0].AxisResult > -EprMath.halfPI) && (mappedPhasors[0].AxisResult <= EprMath.halfPI))
                MyPenB.Color = Color.GreenYellow;
            else
                MyPenB.Color = Color.Orange;
            MyGraphics.DrawLine(MyPenB, PtCentre, PtEnd);
            if (method == AnalyzeMethod.Rotation)
            {
                if (lblPhasor != null)
                    lblPhasor.Text = string.Format("{0:F2}°", (mappedPhasors[0].PhasorResult * 180) / Math.PI);
                MyPenB.Color = Color.BlueViolet;
                nX = (int)(Math.Round(nRadius * Math.Sin(dFilterAxis * 2.0 + mappedPhasors[0].PhaseFloor)));
                nY = (int)(Math.Round(nRadius * Math.Cos(dFilterAxis * 2.0 + mappedPhasors[0].PhaseFloor)));
                PtEnd = new Point(nCentreX + nX, nCentreY - nY);
                MyGraphics.DrawLine(MyPenB, PtCentre, PtEnd);
                MyPenB.Color = Color.Bisque;
                nX = (int)(Math.Round(nRadius * Math.Sin(dFilterAxis * 2.0 + mappedPhasors[0].PhaseCieling)));
                nY = (int)(Math.Round(nRadius * Math.Cos(dFilterAxis * 2.0 + mappedPhasors[0].PhaseCieling)));
                PtEnd = new Point(nCentreX + nX, nCentreY - nY);
                MyGraphics.DrawLine(MyPenB, PtCentre, PtEnd);
            }
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
            for (thetaDeg = -90; thetaDeg <= 89.999; thetaDeg += 2.8125)
            {
                fracTheta =EprMath.ExtendedSineSq(thetaDeg*Math.PI/180.0);
                mappedThetaRad = fracTheta * Math.PI;
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
