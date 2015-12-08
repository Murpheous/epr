using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EprGrapics
{
    enum AnalyzeMethod { Orientation = 0, Phasors = 1 };
    enum AnalyzeChannel { Alice = 0, Bob = 1};
    
    // A 2D  phase vector in the plane of the analyzer's 2D state space
    class clPhasor
    {
 
        double _phaseAngle;
        bool _isClockwise;
        
        double _phasorResult;

        public double PhasorResult
        {
        	get {return _phasorResult;}
        }

        public bool IsClockWise
        {
            get { return _isClockwise; }
            set { _isClockwise = value; }
        }
        public int Sense
        {
            get { return _isClockwise ? 1 : -1; }
        }
        public double PhaseAngle
        {
            get {return  _phaseAngle;}
            set { _phaseAngle = EprMath.Limit180(value); }
        }

        public bool Analyze(clFilter analyzer, double IncidentAxis)
        {
           bool bResult = true;
           // Check the difference is less than 90 degrees, if so, tweak to keep in +- 90
           double analyzerAxis = EprMath.Limit90(analyzer.Inclination);
           double incidentAxis = EprMath.Limit90(IncidentAxis);
           double axisDelta = EprMath.Limit90(incidentAxis - analyzerAxis);
           double shiftSinSq = EprMath.ExtendedSineSq(axisDelta) * Math.PI;
           double phaseDelta = (axisDelta - axisDelta * Sense);
           double incidentPhase = EprMath.Limit180(PhaseAngle - IncidentAxis);
           double analyzerPhase = EprMath.Limit180(PhaseAngle - analyzerAxis);
           /* 
           // Calculate axisDelta as a fraction of 90
           double shiftSinSq = EprMath.ExtendedSineSq(axisDelta)*Math.PI;
           double effectivePhase = PhaseAngle*Sense + phaseDelta/2.0;
           */
           double effectivePhase = incidentPhase + phaseDelta;  
           double mappedResult = EprMath.ExtendedSineSq(effectivePhase);
           _phasorResult = EprMath.Limit180(mappedResult* Math.PI);
           if ((_phasorResult <= EprMath.halfPI) && (_phasorResult > -EprMath.halfPI))
               bResult = true;
           else
               bResult = false;
            return bResult;
        }


        public clPhasor(double srcAxis, bool Sense, double srcPhase)
        {
            _isClockwise = Sense;
            PhaseAngle = EprMath.Limit180(srcAxis + srcPhase * (Sense? 1.0: -1.0));
        }

     }


    /* clPhoton: Photon object as a rotating vector
     * Choose Y axis as 'UP', Z axis as 'LEFT', and X axis along velocity vector (into analyzer).
     * This is simply chosen consistent with the local coordinates in the game engine analyzer prefab I built, and  Physicists beware!!!!, 
     *  >>>>Unity3d has a left-handed coordinate system. Get over it, as I did<<<
     *  
     * Viewning the photon as a twirling baton rotating end-for end. The baton 0 phase is directed in the Y (Up) direction and rotates about the Spin Inclination (the photon's local Z axis)
     * A linearly polarized photon has the spin axis in the Y-Z plane of the analyzer. 
     * At 0/180 degrees polarization the photon's spin (Z) axis is along the analyzer's Z axis, and at +/- 90 degrees
     * it is along the analyzer's Y Inclination. 
     * The photon Phase is the rotation angle about the the photon's Z axis.
     * For a linear polarization, the the tip of the photon's Y vector is through the +-x direction. A bit like a knife thrown by a circus performer.
     * A circular polarization is one where the photon Y vector is rotating in a circle in the analyer's Y-Z plane, the spin axis is along the +- X direction.
     * To calculate a linear photon's state wrt a target, rotate the Z axis about the X axis by the polarization angle 
    */

    class clPhoton
    {
        // Directions used in game engine where analyzer through axis is along X direction, with Y up and Z right.
        static Vector3 worldUp = new Vector3(0, 1, 0);
        static Vector3 worldCross = new Vector3(0, 0, 1);
        static Vector3 worldThrough = new Vector3(1, 0, 0);
        List<clPhasor> Phasors = new List<clPhasor>();

        /*
                   // Choose Y axis as up, Z axis as Left, and X axis is into analyzer.
        /* Three properties define the photon in space wrt lab up = y and direction of travel = x.  
         * (i) Inclination of the spin z axis wrt the Lab x-z plane, 
         * (ii) Azimuth of the Spin Inclination around Lab Up (y), 
         * (iii) the phase about the  spin Inclination.
         * the phase is the rotation of Photon's UP vector around the photon's Spin Inclination.
         * To get the spin axis (0 or 180 Azimuth), first rotate around X by the spinAxisInclination angle, then rotate around photon Z by the phase.
         */
        AnalyzeMethod _method = AnalyzeMethod.Orientation;
        double _spinAxisInclination;
        double _spinAxisAzimuth;
        double _spinPhase;

        // Spin Axis is the axis of rotation of the phase vector in the polarized beam
        Vector3 _spinAxisVector;
        Vector3 _phaseZeroVector; // phase zero vector is intersection between planes defined by normal to analyzer face (worldThrough), and the plane of spin (normal to spin axis) of the polarized beam.
        Vector3 _phaseVector;         // phaseVector is the actual instantaneous phase of the particular photon
        // Vector analysis stuff

        private void setPoyntingVector()
        {
            // Start State is linear, rotation axis = world cross, zero phase = up
            _phaseZeroVector = new Vector3(worldUp);
            _phaseZeroVector.RotateAroundAxis(worldThrough, _spinAxisInclination);
            _spinAxisVector = new Vector3(worldCross);
            _spinAxisVector.RotateAroundAxis(worldThrough, _spinAxisInclination);
            // Start by setting the 'spin axis' azimuth in space
            if (_spinAxisAzimuth != 0.0)
            {
                _spinAxisVector.RotateAroundAxis(_phaseZeroVector,_spinAxisAzimuth);
            }
            // Now finally rotate phase vector about spin Axis
            // Intersect between plane of photon rotation and analyzer face.
            _phaseVector = Vector3.RotateAroundAxis(_phaseZeroVector,_spinAxisVector, _spinPhase);
        }        


        // This is a flag that allows different analyzer methods to be compared.
        public AnalyzeMethod Method
        {
            set
            {
                if (AnalyzeMethod.IsDefined(typeof(AnalyzeMethod), value))
                    _method = value;
                else
                    _method = AnalyzeMethod.Orientation;
            }
            get { return _method; }
        }

       /* public void MakeElliptical(double spinAxisInclination, double spinAxisAzimuth, double spinPhase)
        {
            _spinAxisAzimuth = EprMath.Limit90(spinAxisAzimuth);
            _spinPhase = EprMath.Limit180(spinPhase);
            _spinAxisInclination = EprMath.Limit90(spinAxisInclination);
        } */


        public void MakeLinear(double spinAxisInclination, bool Sense, double spinPhase)
        {
            if (Sense)
                _spinAxisAzimuth = 0.0;
            else
                _spinAxisAzimuth = -Math.PI;
            _spinAxisInclination = EprMath.Limit90(spinAxisInclination);
            _spinPhase = EprMath.Limit180(spinPhase);
            setPoyntingVector();

            Phasors.Clear();
            Phasors.Add(new clPhasor(spinAxisInclination, Sense, spinPhase));
            Phasors.Add(new clPhasor(spinAxisInclination, !Sense, spinPhase));

        }

        public void MakeLinear(double spinAxisInclination, double spinPhase)
        {
            MakeLinear(spinAxisInclination, true, spinPhase);
        }

        public void MakeCircular(double spinAxisInclination, bool Sense, double spinPhase)
        {
            if (Sense)
            {
                _spinAxisAzimuth = EprMath.halfPI;
            }
            else
            {
                _spinAxisAzimuth = -EprMath.halfPI;
            }
            _spinAxisInclination = 0.0;
            //EprMath.Limit90(spinAxisInclination);
            _spinPhase = spinPhase;
            setPoyntingVector();

            Phasors.Clear();
            Phasors.Add(new clPhasor(spinAxisInclination, Sense, spinPhase));
            
        }

        public bool Analyze(clFilter analyzer, bool bShow, bool ShowLimits, Label lblPhasor1, Label lblPhasor2)
        {
            // Phasors are used when projecting a photon on to a linear analyzer. A linear polarization looks like two contra-rotating circular phasors.
            // Idea is to take an incident photon and project it as a superposition of two circular phosors in the analyzer state-space
            bool isAlice = true;
            setPoyntingVector();
            if (Method == AnalyzeMethod.Orientation)
            {
                // Check the difference is less than 90 degrees, if so, tweak to keep in +- 90
                double analyzerAxis = EprMath.Limit90(analyzer.Inclination);
                double incidentAxis = EprMath.Limit90(_spinAxisInclination);
                double axisDelta = 2.0 * (incidentAxis - analyzerAxis);
                // ************** From here we deal with the analyzer map
                // Now we can calculate the phasor on the 2D Yes/No map of the analyzer from the spin axis vector and the mapped phase.
                double phaseRange = EprMath.halfPI + EprMath.halfPI * (1 - Math.Pow(Math.Cos(_spinAxisAzimuth), 2.0));
                // Phase zero and +-180 maps to centre, +- 90 to upper lower 
                double phaseCieling = phaseRange + axisDelta;
                double phaseFloor = -phaseRange + axisDelta;
                double axisResult = EprMath.ExtendedAsin(axisDelta / EprMath.halfPI);
                double phaseMin = EprMath.ExtendedSine(phaseFloor);
                double phaseMax = EprMath.ExtendedSine(phaseCieling);
                double phaseDelta = phaseMax - phaseMin; // We scale phase +- 180 to 
                double phaseMappedDown = ((_spinPhase + Math.PI) / EprMath.twoPI);
                phaseMappedDown = (phaseMappedDown * phaseDelta) + phaseMin;
                double _phasorResult = EprMath.Limit180(EprMath.ExtendedAsin(phaseMappedDown));
                isAlice = ((_phasorResult > (-EprMath.halfPI)) && (_phasorResult <= EprMath.halfPI));
                if (bShow && analyzer.GotPicture())
                {
                    analyzer.ShowMapping(_phasorResult,axisResult,phaseCieling,phaseFloor, lblPhasor1, lblPhasor2);
                }
                return isAlice;
            }
            else if (Method == AnalyzeMethod.Phasors)
            {
                List<int> answers = new List<int>();
                int finalanswer = 1;
                int answer;
                if (Phasors.Count == 0)
                    return true;
                if (Phasors.Count == 1)
                    Phasors.Add(new clPhasor(analyzer.Inclination, !Phasors[0].IsClockWise, 0));
                double mappedAxis = 0;
                foreach (clPhasor phasor in Phasors)
                {
                    mappedAxis += EprMath.Limit180(phasor.PhaseAngle);
                }
                mappedAxis = mappedAxis/Phasors.Count;
                mappedAxis = EprMath.Limit90(mappedAxis);
 
                foreach (clPhasor phasor in Phasors)
                {
                    answer = phasor.Analyze(analyzer,mappedAxis) ? 1 : -1;
                    answers.Add(answer);
                    finalanswer *= answer;
                }
                if (bShow && analyzer.GotPicture())
                {
                    analyzer.ShowMapping(Phasors, ShowLimits, lblPhasor1, lblPhasor2, Method);
                }
                isAlice = (finalanswer > 0);
            }
            return isAlice;
        }

        public bool Analyze(clFilter Target, bool bShow, Label lblPhasor1, Label lblPhasor2)
        {
            return (Analyze(Target,bShow,true, lblPhasor1, lblPhasor2));
        }

        public clPhoton(AnalyzeMethod analyzeMethod)
        {
            Method = analyzeMethod;
        }
        public clPhoton()
        {
            Method = AnalyzeMethod.Orientation;
        }
    }

    class clFilter
    {
        double _analyzerAxis;
        System.Windows.Forms.PictureBox MyPicture;

        public double Inclination
        {
            get { return _analyzerAxis; }
            set { _analyzerAxis = EprMath.Limit180(value);}
        }
        public double AxisDeg
        {
            get { return _analyzerAxis*180.0/Math.PI; }
            set { _analyzerAxis = EprMath.Limit180(value*Math.PI/180.0); }
        }

        public void ShowMapping(double mappedTheta, double mappedAxis, double phaseFloor, double phaseCieling, Label lblPhasor1, Label lblPhasor2)
        {
            int nCentreX, nCentreY, nRadius, nX, nY;
            Pen MyPenB = new Pen(Color.LightCyan, 2);
            System.Drawing.Bitmap MyBitmap = new Bitmap(MyPicture.Image);
            System.Drawing.Graphics MyGraphics = System.Drawing.Graphics.FromImage(MyBitmap);
            nCentreX = MyPicture.ClientSize.Width / 2;
            nCentreY = MyPicture.ClientSize.Height / 2;
            Point PtCentre = new Point(nCentreX, nCentreY);
            nRadius = Math.Min(nCentreY, nCentreX) - 3;
            Point PtEnd;
            nX = (int)(Math.Round(nRadius * Math.Sin(_analyzerAxis * 2.0 + mappedTheta)));
            nY = (int)(Math.Round(nRadius * Math.Cos(_analyzerAxis * 2.0 + mappedTheta)));
            PtEnd = new Point(nCentreX + nX, nCentreY - nY);
            MyGraphics.DrawLine(MyPenB, PtCentre, PtEnd);
            nX = (int)(Math.Round(nRadius * Math.Sin(_analyzerAxis * 2.0 + mappedAxis)));
            nY = (int)(Math.Round(nRadius * Math.Cos(_analyzerAxis * 2.0 + mappedAxis)));
            PtEnd = new Point(nCentreX + nX, nCentreY - nY);
            if ((mappedAxis > -EprMath.halfPI) && (mappedAxis <= EprMath.halfPI))
                MyPenB.Color = Color.GreenYellow;
            else
                MyPenB.Color = Color.Orange;
            //MyGraphics.DrawLine(MyPenB, PtCentre, PtEnd);
            if (lblPhasor1 != null)
                lblPhasor1.Text = string.Format("{0:F2}°", (mappedTheta * 180) / Math.PI);
            MyPenB.Color = Color.BlueViolet;
            nX = (int)(Math.Round(nRadius * Math.Sin(_analyzerAxis * 2.0 + phaseFloor)));
            nY = (int)(Math.Round(nRadius * Math.Cos(_analyzerAxis * 2.0 + phaseFloor)));
            PtEnd = new Point(nCentreX + nX, nCentreY - nY);
            MyGraphics.DrawLine(MyPenB, PtCentre, PtEnd);
            MyPenB.Color = Color.Bisque;
            nX = (int)(Math.Round(nRadius * Math.Sin(_analyzerAxis * 2.0 + phaseCieling)));
            nY = (int)(Math.Round(nRadius * Math.Cos(_analyzerAxis * 2.0 + phaseCieling)));
            PtEnd = new Point(nCentreX + nX, nCentreY - nY);
            MyGraphics.DrawLine(MyPenB, PtCentre, PtEnd);
            MyPicture.Image = MyBitmap;
            MyPicture.Refresh();
            if (lblPhasor2 != null)
                lblPhasor2.Text = "--";

        }

        public void ShowMapping(List<clPhasor> mappedPhasors, bool ShowAll, Label lblPhasor1, Label lblPhasor2, AnalyzeMethod method)
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
                nX = (int)(Math.Round(nRadius * Math.Sin(_analyzerAxis * 2.0 + ph.PhasorResult)));
                nY = (int)(Math.Round(nRadius * Math.Cos(_analyzerAxis * 2.0 + ph.PhasorResult)));
                PtEnd = new Point(nCentreX + nX, nCentreY - nY);
                MyGraphics.DrawLine(MyPenB, PtCentre, PtEnd);
            }
            if (!ShowAll) 
                return;
            nX = (int)(Math.Round(nRadius * Math.Sin(_analyzerAxis * 2.0 + mappedPhasors[0].PhasorResult)));
            nY = (int)(Math.Round(nRadius * Math.Cos(_analyzerAxis * 2.0 + mappedPhasors[0].PhasorResult)));
            PtEnd = new Point(nCentreX + nX, nCentreY - nY);

            //MyGraphics.DrawLine(MyPenB, PtCentre, PtEnd);
            if (lblPhasor1 != null)
                lblPhasor1.Text = string.Format("{0:F2}°", (mappedPhasors[0].PhasorResult * 180) / Math.PI);
            if (lblPhasor2 != null)
                if (mappedPhasors.Count > 1)
                    lblPhasor2.Text = string.Format("{0:F2}°", (mappedPhasors[1].PhasorResult * 180) / Math.PI);
                else
                    lblPhasor2.Text = "--";
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
            Pen MyPenB = new Pen(Color.DarkSlateBlue, 1);
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
                nX = (int)(Math.Round(nRadius * Math.Sin(mappedThetaRad + 2.0 * _analyzerAxis)));
                nY = (int)(Math.Round(nRadius * Math.Cos(mappedThetaRad + 2.0 * _analyzerAxis)));
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
            _analyzerAxis = 0;
        }
        public clFilter(double ArgAxis)
        {
            _analyzerAxis = EprMath.Limit180(ArgAxis);
        }

    }
}
