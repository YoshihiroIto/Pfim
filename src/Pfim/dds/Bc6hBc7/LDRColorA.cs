﻿using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Pfim.dds.Bc6hBc7
{
    internal struct LDRColorA
    {
        public byte r, g, b, a;

        public LDRColorA(byte _r, byte _g, byte _b, byte _a)
        {
            r = _r;
            g = _g;
            b = _b;
            a = _a;
        }

        public byte this[int uElement]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                switch (uElement)
                {
                    case 0: return r;
                    case 1: return g;
                    case 2: return b;
                    case 3: return a;
                    default:
                        Debug.Assert(false);
                        return r;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                switch (uElement)
                {
                    case 0: r = value; break;
                    case 1: g = value; break;
                    case 2: b = value; break;
                    case 3: a = value; break;
                    default:
                        Debug.Assert(false);
                        r = value;
                        break;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InterpolateRGB(LDRColorA c0, LDRColorA c1, int wc, int wcprec, ref LDRColorA outt)
        {
            int[] aWeights = null;
            switch (wcprec)
            {
                case 2:
                    aWeights = Constants.g_aWeights2;
                    Debug.Assert(wc < 4);
                    break;
                case 3:
                    aWeights = Constants.g_aWeights3;
                    Debug.Assert(wc < 8);
                    break;
                case 4:
                    aWeights = Constants.g_aWeights4;
                    Debug.Assert(wc < 16);
                    break;
                default:
                    Debug.Assert(false);
                    outt.r = outt.g = outt.b = 0;
                    return;
            }

            outt.r = (byte)(((uint)(c0.r) * (uint)(Constants.BC67_WEIGHT_MAX - aWeights[wc]) +
                             (uint)(c1.r) * (uint)(aWeights[wc]) + Constants.BC67_WEIGHT_ROUND) >>
                            Constants.BC67_WEIGHT_SHIFT);
            outt.g = (byte)(((uint)(c0.g) * (uint)(Constants.BC67_WEIGHT_MAX - aWeights[wc]) +
                             (uint)(c1.g) * (uint)(aWeights[wc]) + Constants.BC67_WEIGHT_ROUND) >>
                            Constants.BC67_WEIGHT_SHIFT);
            outt.b = (byte)(((uint)(c0.b) * (uint)(Constants.BC67_WEIGHT_MAX - aWeights[wc]) +
                             (uint)(c1.b) * (uint)(aWeights[wc]) + Constants.BC67_WEIGHT_ROUND) >>
                            Constants.BC67_WEIGHT_SHIFT);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InterpolateA(LDRColorA c0, LDRColorA c1, int wa, int waprec, ref LDRColorA outt)
        {
            int[] aWeights = null;
            switch (waprec)
            {
                case 2:
                    aWeights = Constants.g_aWeights2;
                    Debug.Assert(wa < 4);
                    break;
                case 3:
                    aWeights = Constants.g_aWeights3;
                    Debug.Assert(wa < 8);
                    break;
                case 4:
                    aWeights = Constants.g_aWeights4;
                    Debug.Assert(wa < 16);
                    break;
                default:
                    Debug.Assert(false);
                    outt.a = 0;
                    return;
            }

            outt.a = (byte)(((uint)(c0.a) * (uint)(Constants.BC67_WEIGHT_MAX - aWeights[wa]) +
                             (uint)(c1.a) * (uint)(aWeights[wa]) + Constants.BC67_WEIGHT_ROUND) >>
                            Constants.BC67_WEIGHT_SHIFT);
        }

        public static void Interpolate(LDRColorA c0, LDRColorA c1, int wc, int wa, int wcprec, int waprec,
            ref LDRColorA outt)
        {
            InterpolateRGB(c0, c1, wc, wcprec, ref outt);
            InterpolateA(c0, c1, wa, waprec, ref outt);
        }
    }
}
