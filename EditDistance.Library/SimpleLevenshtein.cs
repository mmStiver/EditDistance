using System;
using System.Collections.Generic;
using System.Text;

namespace EditDistance.Library {
    public class SimpleLevenshtein : IEditDistance<int>{
        public int ComputeDistance(string source, string target) {
			if (source.Length == 0)
			    return target.Length;
			
			if (target.Length == 0)
			    return source.Length;
			
            int height = source.Length;
			int length = target.Length;
			int[,] matrix = new int[height + 1, length + 1];
			for (int i = 0; i <= height; matrix[i, 0] = i++){}
			for (int j = 0; j <= length; matrix[0, j] = j++){}
			for (int i = 1; i <= height; i++)
			{
			    for (int j = 1; j <= length; j++)
			    {
					int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

					matrix[i, j] = Math.Min(
						Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
						matrix[i - 1, j - 1] + cost);
			    }
			}
			return matrix[height, length];
		}

        public int ComputeDistance(ReadOnlySpan<char> source, ReadOnlySpan<char> target) {
            throw new NotImplementedException();
        }
    }
}