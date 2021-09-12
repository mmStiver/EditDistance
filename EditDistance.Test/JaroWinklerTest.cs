using EditDistance.Library;
using System;
using Xunit;

namespace EditDistance.Test {
    public class JaroWinklerTest {
        public IEditDistance <double> CreateJaroWinkler() {
            return new JaroWinkler();
        }
        
         [Theory]
        [InlineData("projection", "projection", 1)]
        [InlineData("dentist", "dentist", 1)]
        [InlineData("infrastructure", "infrastructure", 1)]
        [InlineData("a", "a", 1)]
         public void JaroWinkler_ComputeDistance_SameStrings(string source, string target, double diff) {
            Assert.Equal(diff.ToString("{0:0.00}"), CreateJaroWinkler().ComputeDistance(source.AsSpan(), target).ToString("{0:0.00}"));
        }

        [Theory]
        [InlineData("nose", "sitten", 0.611)]
        [InlineData("new", "swear", 0.52)]
        [InlineData("wilderness", "discriminate", 0.57)]
        [InlineData("swear", "wheel", 0.6)]
        [InlineData("dentist", "routine", 0.523)]
        [InlineData("projection", "captivate", 0.531)]
        [InlineData("circumstance", "circumcision", 0.88)]
        public void JaroWinkler_ComputeDistance_Transformations(string source, string target, double diff) {
            Assert.Equal(diff.ToString("{0:0.00}"), CreateJaroWinkler().ComputeDistance(source.AsSpan(), target).ToString("{0:0.00}"));
        }

        [Theory]
        [InlineData("stests", "test", 0.81)]
        [InlineData("west", "western", 0.91)]
        [InlineData("point", "pointing", 0.93)]
        [InlineData("part", "partners", 0.9)]
        [InlineData("part", "partnership", 0.87)]
        public void JaroWinkler_ComputeDistance_Insertions(string source, string target, double diff) {
            Assert.Equal(diff.ToString("{0:0.00}"), CreateJaroWinkler().ComputeDistance(source.AsSpan(), target).ToString("{0:0.00}") );
        }
        
        [Theory]
        [InlineData("tst", "test", 0.93)]
        [InlineData("te", "test", 0.87)]
        [InlineData("projection", "project", 0.94)]
        [InlineData("shirtless", "shirt", 0.91)]
        [InlineData("shirtless", "less", 0.45)]
        public void JaroWinkler_ComputeDistance_Deletions(string source, string target, double diff) {
            Assert.Equal(diff.ToString("{0:0.00}"), CreateJaroWinkler().ComputeDistance(source.AsSpan(), target).ToString("{0:0.00}"));
        }

        
        [Theory]
        [InlineData("fast", "test", 0.67)]
        [InlineData("kitten", "sitten", 0.89)]
        [InlineData("schmuck", "shttuck", 0.828)]
        [InlineData("Misiispipie", "Mississippi", 0.86)]
        public void JaroWinkler_ComputeDistance_Substitutions(string source, string target, double diff) {
            Assert.Equal(diff.ToString("{0:0.00}"), CreateJaroWinkler().ComputeDistance(source.AsSpan(), target).ToString("{0:0.00}"));
        }

        [Theory]
        [InlineData("tset", "test", 0.93)]
        [InlineData("etts", "test", 0.83)]
        [InlineData("teams", "teasm", 0.95)]
        [InlineData("infrastructure", "iffnrasrtuctree", 0.89)]
        [InlineData("infrastructure", "infrsatrcuture", 0.97)]
        [InlineData("ghostwriter", "ghoswtritre", 0.96)]
        public void JaroWinkler_ComputeDistance_Transpositions(string source, string target, double diff) {
            Assert.Equal(diff.ToString("{0:0.00}"), CreateJaroWinkler().ComputeDistance(source.AsSpan(), target).ToString("{0:0.00}"));
        }
    }
}
