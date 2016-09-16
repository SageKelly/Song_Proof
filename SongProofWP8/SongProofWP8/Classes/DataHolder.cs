using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongProofWP8
{
    public static class DataHolder
    {
        public enum ProofingTypes
        {
            Dummy,
            BuildTheScale,
            PlacingTheNote,
            FindTheVoice,
            BuildTheChord,
            HW3,
            GrabBag,
            ScaleWriting
        }

        /// <summary>
        /// Denote the type of the intended proofing Used with the Main Page and SessionSetup
        /// </summary>
        public static ProofingTypes ProofType;
        public static SessionManager SM;
        public static bool ShowSharp;
        /// <summary>
        /// Denotes that there has been a proofing chosen before
        /// </summary>
        public static bool PTNHasBeenSet { get; private set; }
        public static bool HW3HasBeenSet { get; private set; }

        public static string Key { get; private set; }
        public static string ScaleGroup { get; private set; }
        public static KVTuple<string, string> Scale { get; private set; }
        public static ScaleResources.Difficulties Diff { get; private set; }
        public static int NoteCount { get; private set; }

        static DataHolder()
        {
            PTNHasBeenSet = HW3HasBeenSet = false;
            ProofType = ProofingTypes.Dummy;
        }


        public static void SetupPTNTest(ProofingTypes proofingType, string key, string scaleGroup, KVTuple<string, string> scale, bool sharp, ScaleResources.Difficulties diff, int note_count)
        {
            ProofType = proofingType;
            Key = key;
            ScaleGroup = scaleGroup;
            Scale = scale;
            ShowSharp = sharp;
            Diff = diff;
            NoteCount = note_count;
            Scale temp = ScaleResources.MakeScale(key, scale, sharp);
            SM = new SessionManager(new Session(ProofType, Diff, temp,
                sharp ? ScaleResources.PianoSharp : ScaleResources.PianoFlat,
                ScaleResources.MakePTNQuiz(temp, note_count)));
            SM.ParsedScale = ScaleResources.ParseScale(Scale);
            ShowSharp = sharp;
            PTNHasBeenSet = true;
        }

        private static void SetupPTNTest()
        {
            if (Scale != null)
            {
                SetupPTNTest(ProofType, Key, ScaleGroup, Scale, ShowSharp, Diff, NoteCount);
            }
        }

        public static void SetupHW3Test(ProofingTypes proofingType, bool sharp, ScaleResources.Difficulties diff, int note_count)
        {
            ProofType = proofingType;
            ShowSharp = sharp;
            Diff = diff;
            NoteCount = note_count;
            string[] piano = sharp ? ScaleResources.PianoSharp : ScaleResources.PianoFlat;
            SM = new SessionManager(new Session(ProofType, Diff,
                piano,
                ScaleResources.MakeHW3Quiz(note_count)));
            HW3HasBeenSet = true;
        }

        private static void SetupHW3Test()
        {
            SetupHW3Test(ProofType, ShowSharp, Diff, NoteCount);
        }

        public static void ResetTest()
        {
            switch (ProofType)
            {
                case ProofingTypes.PlacingTheNote:
                    SetupPTNTest();
                    break;
                case ProofingTypes.HW3:
                    SetupHW3Test();
                    break;
            }
        }
    }
}
