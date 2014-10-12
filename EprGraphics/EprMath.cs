using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EprGrapics
{
    public static class EprMath
    {
        public const double dTwoPi = Math.PI * 2.0;
        public const double dHalfPi = Math.PI / 2.0;
        public const double dQuarterPi = Math.PI / 4.0;
        public static double Limit180(double theta)
        {
            long nPi;
            nPi = 0;
            if (theta >= Math.PI)
            {
                nPi = (long)(Math.Truncate(theta / Math.PI));
                nPi = (nPi + 1) / 2;
                theta = theta - (nPi * dTwoPi);
            }
            else if (theta <= -Math.PI)
            {
                nPi = (long)(Math.Truncate(theta / Math.PI));
                nPi = (nPi - 1) / 2;
                theta = theta - (nPi * dTwoPi);
            }
            return theta;
        }
        public static double Limit90(double theta)
        {
            theta = Limit180(theta);
            if (theta > dHalfPi)
                return theta - Math.PI;
            if (theta < -dHalfPi)
                return theta + Math.PI;
            return theta;
        }

        public static double ExtendedAsin(double value)
        {
            double fractionPart, result;
            long nOffset, integerPart;

            nOffset = 0;
            integerPart = (long)Math.Truncate(value);
            if (integerPart >= 1)
                nOffset = (integerPart + 1) / 2;
            else if (integerPart <= -1)
                nOffset = (integerPart - 1) / 2;
            nOffset *= 2;
            fractionPart = value - nOffset;
            result = Math.Asin(fractionPart) + ((nOffset / 2) * Math.PI);
            return result;
        }

        public static double ExtendedSin(double theta)
        {
            double fractionPart, thetaNormalised, result;
            long integerPart;
            thetaNormalised = (theta + EprMath.dHalfPi) / Math.PI;
            integerPart = (long)Math.Truncate(thetaNormalised);
            if (thetaNormalised < 0)
                integerPart--;
            fractionPart = theta - (integerPart * Math.PI);
            result = Math.Sin(fractionPart) + integerPart * 2;
            return result;
        }
        public static double ExtendedArcSinSq(double value)
        {
            long intpart = (long)Math.Floor(value);
            double fracpart = value - intpart;
            return Math.Asin(Math.Sqrt(fracpart)) + EprMath.dHalfPi * intpart;
        }

        public static double ExtendedArcCosSq(double value)
        {
            return ExtendedArcSinSq(value + 0.5) - EprMath.dQuarterPi;
        }

        public static double ExtendedSineSq(double theta)
        {
            // Function GetShift(Theta As Double) As Double
            //Dim IntPart As Double, Fracpart As Double, Result As Double
            long integerPart;
            double fractionPart;
            double dSine;
            long nOffset;
            double dOffset;
            int nSineSign = 1;
            integerPart = (long)Math.Truncate(theta / dHalfPi);
            nOffset = 0;
            if (integerPart >= 1)
                nOffset = (integerPart + 1) / 2;
            else if (integerPart <= -1)
                nOffset = (integerPart - 1) / 2;
            nOffset *= 2;
            dOffset = nOffset * dHalfPi;
            fractionPart = theta - dOffset;
            dSine = Math.Sin(fractionPart);
            if (dSine < 0.0)
                nSineSign = -1;
            dSine *= (dSine * nSineSign);
            return dSine + nOffset;
        }
    }
}
