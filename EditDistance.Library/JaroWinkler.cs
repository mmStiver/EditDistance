using System;
using System.Collections.Generic;
using System.Text;

namespace EditDistance.Library {
    /// <summary>
    /// https://stackoverflow.com/questions/19123506/jaro-winkler-distance-algorithm-in-c-sharp
    /// </summary>
    public class JaroWinkler : IEditDistance<double>
    {
        /* The Winkler modification will not be applied unless the 
         * percent match was at or above the mWeightThreshold percent 
         * without the modification. 
         * Winkler's paper used a default value of 0.7
         */
        private readonly double mWeightThreshold = 0.7;
    
        /* Size of the prefix to be concidered by the Winkler modification. 
         * Winkler's paper used a default value of 4
         */
        private readonly int mNumChars = 4;
    
    
        /// <summary>
        /// Returns the Jaro-Winkler distance between the specified  
        /// strings. The distance is symmetric and will fall in the 
        /// range 0 (perfect match) to 1 (no match). 
        /// </summary>
        /// <param name="aString1">First String</param>
        /// <param name="aString2">Second String</param>
        /// <returns></returns>
        public double Distance(string aString1, string aString2) {
            return 1.0 - ComputeDistance(aString1,aString2);
        }
    
    
        /// <summary>
        /// Returns the Jaro-Winkler distance between the specified  
        /// strings. The distance is symmetric and will fall in the 
        /// range 0 (no match) to 1 (perfect match). 
        /// </summary>
        /// <param name="aString1">First String</param>
        /// <param name="aString2">Second String</param>
        /// <returns></returns>
        public double ComputeDistance(string aString1, string aString2)
        {
            int lLen1 = aString1.Length;
            int lLen2 = aString2.Length;
            if (lLen1 == 0)
                return lLen2 == 0 ? 1.0 : 0.0;
    
            int  lSearchRange = Math.Max(0,Math.Max(lLen1,lLen2)/2 - 1);
    
            // default initialized to false
            bool[] lMatched1 = new bool[lLen1];
            bool[] lMatched2 = new bool[lLen2];
    
            int lNumCommon = 0;
            for (int i = 0; i < lLen1; ++i) {
                int lStart = Math.Max(0,i-lSearchRange);
                int lEnd = Math.Min(i+lSearchRange+1,lLen2);
                for (int j = lStart; j < lEnd; ++j) {
                    if (lMatched2[j]) continue;
                    if (aString1[i] != aString2[j])
                        continue;
                    lMatched1[i] = true;
                    lMatched2[j] = true;
                    ++lNumCommon;
                    break;
                }
            }
            if (lNumCommon == 0) return 0.0;
    
            int lNumHalfTransposed = 0;
            int k = 0;
            for (int i = 0; i < lLen1; ++i) {
                if (!lMatched1[i]) continue;
                while (!lMatched2[k]) ++k;
                if (aString1[i] != aString2[k])
                    ++lNumHalfTransposed;
                ++k;
            }
            // System.Diagnostics.Debug.WriteLine("numHalfTransposed=" + numHalfTransposed);
            int lNumTransposed = lNumHalfTransposed/2;
    
            // System.Diagnostics.Debug.WriteLine("numCommon=" + numCommon + " numTransposed=" + numTransposed);
            double lNumCommonD = lNumCommon;
            double lWeight = (lNumCommonD/lLen1
                             + lNumCommonD/lLen2
                             + (lNumCommon - lNumTransposed)/lNumCommonD)/3.0;
    
            if (lWeight <= mWeightThreshold) return lWeight;
            int lMax = Math.Min(mNumChars,Math.Min(aString1.Length,aString2.Length));
            int lPos = 0;
            while (lPos < lMax && aString1[lPos] == aString2[lPos])
                ++lPos;
            if (lPos == 0) return lWeight;
            return lWeight + 0.1 * lPos * (1.0 - lWeight);
    

        }

        public double ComputeDistance(ReadOnlySpan<char> source, ReadOnlySpan<char> target) {
            if (source.Length == 0)
                return target.Length == 0 ? 1.0 : 0.0;
    
            int lLen1 = source.Length;
            int lLen2 = target.Length;
    
            ushort lSearchRange = (ushort)Math.Max(0,Math.Max(lLen1,lLen2)/2 - 1);
    
            // default initialized to false
            Span<bool> buffer = lLen1 + lLen2 <= 1024 ? stackalloc bool[lLen1 + lLen2] : new bool[lLen1 + lLen2];
            Span<bool> lMatched1 = buffer.Slice(0, lLen1);
            Span<bool> lMatched2 = buffer.Slice(lLen1, lLen2);
            
            byte lNumCommon = 0;
            for (int i = 0; i < lLen1; ++i) {
                ushort lStart = (ushort)Math.Max(0,i-lSearchRange);
                ushort lEnd   = (ushort)Math.Min(i+lSearchRange+1,lLen2);
                for (int j = lStart; j < lEnd; ++j) {
                    if (lMatched2[j]) continue;
                    if (source[i] != target[j])
                        continue;
                    lMatched1[i] = true;
                    lMatched2[j] = true;
                    ++lNumCommon;
                    break;
                }
            }
            if (lNumCommon == 0) return 0.0;
    
            int lNumHalfTransposed = 0;
            int k = 0;
            for (int i = 0; i < lLen1; ++i) {
                if (!lMatched1[i]) continue;
                while (!lMatched2[k]) ++k;
                if (source[i] != target[k])
                    ++lNumHalfTransposed;
                ++k;
            }
            // System.Diagnostics.Debug.WriteLine("numHalfTransposed=" + numHalfTransposed);
            int lNumTransposed = lNumHalfTransposed/2;
    
            // System.Diagnostics.Debug.WriteLine("numCommon=" + numCommon + " numTransposed=" + numTransposed);
            double lNumCommonD = lNumCommon;
            double lWeight = (lNumCommonD/lLen1
                             + lNumCommonD/lLen2
                             + (lNumCommon - lNumTransposed)/lNumCommonD)/3.0;
    
            if (lWeight <= mWeightThreshold) return lWeight;
            int lMax = Math.Min(mNumChars,Math.Min(source.Length,target.Length));
            int lPos = 0;
            while (lPos < lMax && source[lPos] == target[lPos])
                ++lPos;
            if (lPos == 0) return lWeight;
            return lWeight + 0.1 * lPos * (1.0 - lWeight);
        }
    }
}