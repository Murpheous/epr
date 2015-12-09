using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EprGrapics
{
    public static class EprMath
    {
        public const double twoPI = Math.PI * 2.0;
        public const double halfPI = Math.PI / 2.0;
        public const double quarterPI = Math.PI / 4.0;
        public static double Limit180(double theta)
        {
            long nPi;
            nPi = 0;
            if (theta >= Math.PI)
            {
                nPi = (long)(Math.Truncate(theta / Math.PI));
                nPi = (nPi + 1) / 2;
                theta = theta - (nPi * twoPI);
            }
            else if (theta <= -Math.PI)
            {
                nPi = (long)(Math.Truncate(theta / Math.PI));
                nPi = (nPi - 1) / 2;
                theta = theta - (nPi * twoPI);
            }
            return theta;
        }
        public static double Limit90(double theta)
        {
            theta = Limit180(theta);
            if (theta > halfPI)
                return theta - Math.PI;
            if (theta < -halfPI)
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

        public static double ExtendedSine(double theta)
        {
            double fractionPart, thetaNormalised, result;
            long integerPart;
            thetaNormalised = (theta + EprMath.halfPI) / Math.PI;
            integerPart = (long)Math.Truncate(thetaNormalised);
            if (thetaNormalised < 0)
                integerPart--;
            fractionPart = theta - (integerPart * Math.PI);
            result = Math.Sin(fractionPart) + integerPart * 2;
            return result;
        }

        public static double ExtendedArcSinSq(double value)
        {
            double Sign = 1.0;
            if (value < 0.0)
            {
                value = -value;
                Sign = -1.0;
            }
            long intpart = (long)Math.Floor(value);
            double fracpart = value - intpart;
            double result = Math.Asin(Math.Sqrt(fracpart)) + EprMath.halfPI * intpart;
            return result * Sign;
        }

        public static double ExtendedArcCosSq(double value)
        {
            return ExtendedArcSinSq(value + 0.5) - EprMath.quarterPI;
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
            integerPart = (long)Math.Truncate(theta / halfPI);
            nOffset = 0;
            if (integerPart >= 1)
                nOffset = (integerPart + 1) / 2;
            else if (integerPart <= -1)
                nOffset = (integerPart - 1) / 2;
            nOffset *= 2;
            dOffset = nOffset * halfPI;
            fractionPart = theta - dOffset;
            dSine = Math.Sin(fractionPart);
            if (dSine < 0.0)
                nSineSign = -1;
            dSine *= (dSine * nSineSign);
            return dSine + nOffset;
        }
    }
}
