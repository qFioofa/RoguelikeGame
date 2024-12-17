using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Noise{
    public class PerlinNoise{
        private readonly int xLen, yLen;
        private readonly long Seed, Harmonic_Spread, Harmonic;
        private readonly double Offset, Amplitude, Exponent;

        public PerlinNoise(int xLen, int yLen, long seed, double offset, double amplitude, double exponent, long harmonicSpread, long harmonic){
            this.xLen = xLen;
            this.yLen = yLen;
            this.Offset = offset;
            this.Amplitude = amplitude;
            this.Exponent = exponent;
            this.Harmonic_Spread = harmonicSpread; 
            this.Harmonic = harmonic;
            this.Seed = seed;
        }

        public PerlinNoise(int xLen, int yLen, long seed, double offset, double amplitude, double exponent, long harmonicSpread)
            : this(xLen, yLen, seed, offset, amplitude, exponent, harmonicSpread, 1) {}

        public PerlinNoise(int xLen, int yLen, long seed, double offset, double amplitude, double exponent)
            : this(xLen, yLen, seed, offset, amplitude, exponent, 1) {}

        public PerlinNoise(int xLen, int yLen, long seed, double offset, double amplitude)
            : this(xLen, yLen, seed, offset, amplitude, 1) {}

        public PerlinNoise(int xLen, int yLen, long seed, double offset)
            : this(xLen, yLen, seed, offset, 0.5) {}

        public PerlinNoise(int xLen, int yLen, long seed)
            : this(xLen, yLen, seed, 0.5) {}

        public PerlinNoise(long seed)
            : this(1, 1, seed) {}

        public static PerlinNoise getDefault(long seed){
            return new PerlinNoise(100,100,seed);
        }
        public long GetSeed() => Seed;

        public double[,] GetGrid(){
            var resGrid = new double[xLen, yLen];

            for (int i = 0; i < xLen; i++){
                for (int j = 0; j < yLen; j++){
                    resGrid[i, j] = Perlin(i / Amplitude, j / Amplitude);
                }
            }

            return resGrid;
        }

        private double Perlin(double x, double y){
            int x0 = (int)x - 1;
            int y0 = (int)y - 1;
            int x1 = x0 + 1;
            int y1 = y0 + 1;

            double sx = x - x0;
            double sy = y - y0;

            double n0 = DotGridGrad(x0, y0, x, y);
            double n1 = DotGridGrad(x1, y0, x, y);
            double ix0 = Interpolate(n0, n1, sx * Exponent);

            n0 = DotGridGrad(x0, y1, x, y);
            n1 = DotGridGrad(x1, y1, x, y);
            double ix1 = Interpolate(n0, n1, sx * Exponent);

            double fx0 = Fx(sx);
            double fx1 = Fx(sy);

            double res0 = Interpolate(ix0, ix1, sy) * fx0;
            double res1 = Interpolate(ix0, ix1, sy) * fx1;

            double res = Round(Interpolate(res0 + Offset, res1 + Offset, 1), 3);
            return res;
        }

        private (double x, double y) RandGrad(int ix, int iy){
            const uint w = 8 * sizeof(uint);
            const uint s = w / 2;

            uint a = (uint)ix;
            uint b = (uint)iy;

            a *= (uint)Seed;
            b ^= a << (int)s | a >> (int)(w - s);
            b *= (uint)Seed;
            a ^= b << (int)s | b >> (int)(w - s);
            a *= (uint)Seed;

            double r = a * (Math.PI / ~(~0u >> 1));
            return (Math.Cos(r), Math.Sin(r));
        }

        private double DotGridGrad(int ix, int iy, double x, double y){
            var grad = RandGrad(ix, iy);
            double dx = x - ix;
            double dy = y - iy;
            return dx * grad.x + dy * grad.y;
        }

        private double Interpolate(double n1, double n2, double w){
            return (n1 - n2) * (3.0 - w * 2) * w * w + n1;
        }

        private double Fx(double sx){
            return Math.Pow(Harmonic_Spread / Harmonic, Exponent) * Math.Pow(sx, Exponent - 1);
        }

        private double Round(double value, int precision){
            double factor = Math.Pow(10, precision);
            return Math.Round(value * factor) / factor;
        }
    }
}
