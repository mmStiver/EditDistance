using System;

namespace EditDistance.Library {
    public interface IEditDistance<T> {
        T ComputeDistance(string source, string target);
        T ComputeDistance(ReadOnlySpan<char> source, ReadOnlySpan<Char> target) ;
    }
}
