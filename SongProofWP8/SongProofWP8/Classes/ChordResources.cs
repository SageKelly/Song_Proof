using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongProofWP8.Classes
{
    public static class ChordResources
    {

        public enum ChordScales
        {
            Augmented,
            Bebop,//Used for Suspened 2/4
            BebopMinor1,
            BebopMinor2,
            Blues,
            DiminishedH,
            DiminishedW,
            DiminishedWholeTone,
            Dorian,
            HarmonicMajor,
            HarmonicMinor,
            HarmonicMinorMode6,
            Hindu,
            Ionian,
            Locrian1,
            Locrian2,
            Lydian,
            LydianAugmented,
            LydianDominant,
            MelodicMinorAscending,
            Mixolydian,
            PentatonicMajor,
            PentatonicMinor,
            Phrygian,
            SpanishJewish,
            WholeTone
        }

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
            SharpFour,

        }

        public static List<KVTuple<ChordScales, Chord>> Chords { get; set; }
        public static List<KVTuple<ChordTypes, string>> ChordNames { get; set; }
        public static List<KVTuple<ChordTypes, string>> ChordSymbols { get; set; }

        public static List<KVTuple<Modifiers, ChordModifier>> ChordModifiers { get; set; }


    }
}
