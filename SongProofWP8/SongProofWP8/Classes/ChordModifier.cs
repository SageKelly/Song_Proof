using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongProofWP8.Classes
{
    public class ChordModifier
    {
        public enum Alternations
        {
            Sharp,
            Natural,
            Flat
        }

        public Alternations Alternation { get; set; }
        public string ModifierName { get; set; }
        public string ModifierSymbol { get; set; }
        public int[] NotesToAdd { get; set; }
        public int[] NotesToRemove { get; set; }
    }
}
