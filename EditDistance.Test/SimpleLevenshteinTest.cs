using EditDistance.Library;
using System;
using Xunit;

namespace EditDistance.Test {
    public class SimpleLevenshteinTest {
        public IEditDistance<int> CreateLevenshtein() {
            return new SimpleLevenshtein();
        }
        
         [Theory]
        [InlineData("projection", "projection", 0)]
        [InlineData("dentist", "dentist", 0)]
        [InlineData("partnership", "partnership", 0)]
        [InlineData("infrastructure", "infrastructure", 0)]
        [InlineData("a", "a", 0)]
        [InlineData("two", "two", 0)]
         public void SimpleLevenshtein_ComputeDistance_SameStrings(string source, string target, int diff) {
            Assert.Equal(CreateLevenshtein().ComputeDistance(source, target), diff);
        }

        [Theory]
        [InlineData("nose", "sitten", 5)]
        [InlineData("new", "swear", 4)]
        [InlineData("wilderness", "discriminate", 10)]
        [InlineData("swear", "wheel", 4)]
        [InlineData("dentist", "routine", 5)]
        [InlineData("projection", "captivate", 10)]
        [InlineData("circumstance", "circumcision", 6)]
        public void SimpleLevenshtein_ComputeDistance_Transformations(string source, string target, int diff) {
            Assert.Equal(CreateLevenshtein().ComputeDistance(source, target), diff);
        }

        [Theory]
        [InlineData("tests", "test", 1)]
        [InlineData("stest", "test", 1)]
        [InlineData("stests", "test", 2)]
        [InlineData("west", "western", 3)]
        [InlineData("point", "pointing", 3)]
        [InlineData("part", "partners", 4)]
        [InlineData("part", "partnership", 7)]
        public void SimpleLevenshtein_ComputeDistance_Insertions(string source, string target, int diff) {
            Assert.Equal(CreateLevenshtein().ComputeDistance(source, target), diff);
        }
        
        [Theory]
        [InlineData("tst", "test", 1)]
        [InlineData("te", "test", 2)]
        [InlineData("projection", "project", 3)]
        [InlineData("shirtless", "shirt", 4)]
        [InlineData("shirtless", "less", 5)]
        public void SimpleLevenshtein_ComputeDistance_Deletions(string source, string target, int diff) {
            Assert.Equal(CreateLevenshtein().ComputeDistance(source, target), diff);
        }

        
        [Theory]
        [InlineData("tast", "test", 1)]
        [InlineData("fest", "test", 1)]
        [InlineData("fast", "test", 2)]
        [InlineData("kitten", "sitten", 1)]
        [InlineData("muck", "duck", 1)]
        [InlineData("meet", "meat", 1)]
        public void SimpleLevenshtein_ComputeDistance_Substitutions(string source, string target, int diff) {
            Assert.Equal(CreateLevenshtein().ComputeDistance(source, target), diff);
        }

        [Theory]
        [InlineData("tset", "test", 2)]
        [InlineData("etts", "test", 3)]
        [InlineData("teams", "teasm", 2)]
        [InlineData("infrastructure", "iffnrasrtuctree", 6)]
        [InlineData("infrastructure", "infrsatrcuture", 4)]
        [InlineData("ghostwriter", "ghoswtritre", 4)]
        public void SimpleLevenshtein_ComputeDistance_Transpositions(string source, string target, int diff) {
            Assert.Equal(CreateLevenshtein().ComputeDistance(source, target), diff);
        }

           [Theory]
        [InlineData("projection", "", 10)]
        [InlineData("dentist", "", 7)]
        [InlineData("partnership", "", 11)]
        [InlineData("infrastructure", "", 14)]
        [InlineData("", "projection", 10)]
        [InlineData("", "dentist", 7)]
        [InlineData("", "partnership", 11)]
        [InlineData("", "infrastructure", 14)]
         public void SimpleLevenshtein_ComputeDistance_EmptyStrings(string source, string target, int diff) {
            Assert.Equal(CreateLevenshtein().ComputeDistance(source, target), diff);
        }
    }
}
