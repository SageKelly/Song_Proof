using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongProofWP8.Classes
{
    public static class ChordResources
    {
        public enum ChordTypes
        {
            Augmented,
            Suspended,
            Major,
            Minor,
            HalfDiminished,
            Minor7Flat5,
            Diminished
        }

        public enum Modifiers
        {
            AddTwo,
            AddNine,
            AddFour,
            AddFive,
            AddSix,
            AddEleven,
            AddThirteen,
            SusTwo,
            SusFour,
            Five,
            Sixth,
            Seventh,
            Ninth,
            Eleventh,
            Thirteenth,
            FlatFive,
            SharpNine,
            SharpFive,
            SharpFour
        }

        public static List<KVTuple<ChordTypes, string>> ChordNames { get; set; }
        public static List<KVTuple<ChordTypes, string>> ChordSymbols { get; set; }

        public static List<KVTuple<Modifiers, ChordModifier>> ChordModifiers { get; set; }


    }
}
