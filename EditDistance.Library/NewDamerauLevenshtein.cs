using System;
using System.Collections.Generic;
using System.Text;

namespace EditDistance.Library {
    public class NewDamerauLevenshtein : IEditDistance<int> {
    public int ComputeDistance(string source, string target) {
            if (source.Length == 0)
			    return target.Length;
			
			if (target.Length == 0)
			    return source.Length;
			
            var height = source.Length + 1;
            var width = target.Length + 1 ;
        
            int[,] matrix = new int[height, width];
        
            if(height == width) { 
                for (int i = 0; i < height; i++) { matrix[i, 0] = matrix[0, 1] = i; };    
            } else {
                for (int h = 0; h < height; h++) { matrix[h, 0] = h; };
                for (int w = 0; w < width; w++) { matrix[0, w] = w; };
            }
        
            for (int h = 1; h < height; h++)
            {
                for (int w = 1; w < width; w++)
                {
                    int cost = (source[h - 1] == target[w - 1]) ? 0 : 1;
                    int insertion = matrix[h, w - 1] + 1;  
                    int deletion = matrix[h - 1, w] + 1;
                    int substitution = matrix[h - 1, w - 1] + cost;
        
                    int distance = Math.Min(insertion, Math.Min(deletion, substitution));
        
                    if (h > 1 && w > 1 && source[h - 1] == target[w - 2] && source[h - 2] == target[w - 1])
                    {
                        distance = Math.Min(distance, matrix[h - 2, w - 2] + cost);
                    }
        
                    matrix[h, w] = distance;
                }
            }
        
            return matrix[height - 1, width - 1];
        }
    public int ComputeDistance(ReadOnlySpan<char> source, ReadOnlySpan<Char> target) {
            if (source.Length == 0)
			    return target.Length;
			
			if (target.Length == 0)
			    return source.Length;
			
            var height = source.Length + 1;
            var width = target.Length + 1 ;
        
            int[,] matrix = new int[height, width];
        
            if(height == width) { 
                for (int i = 0; i < height; i++) { matrix[i, 0] = matrix[0, 1] = i; };    
            } else {
                for (int h = 0; h < height; h++) { matrix[h, 0] = h; };
                for (int w = 0; w < width; w++) { matrix[0, w] = w; };
            }
        
            for (int h = 1; h < height; h++)
            {
                for (int w = 1; w < width; w++)
                {
                    int cost = (source[h - 1] == target[w - 1]) ? 0 : 1;
                    int insertion = matrix[h, w - 1] + 1;  
                    int deletion = matrix[h - 1, w] + 1;
                    int substitution = matrix[h - 1, w - 1] + cost;
        
                    int distance = Math.Min(insertion, Math.Min(deletion, substitution));
        
                    if (h > 1 && w > 1 && source[h - 1] == target[w - 2] && source[h - 2] == target[w - 1])
                    {
                        distance = Math.Min(distance, matrix[h - 2, w - 2] + cost);
                    }
        
                    matrix[h, w] = distance;
                }
            }
        
            return matrix[height - 1, width - 1];

        }
    }
}
