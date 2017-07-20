using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongProofWP8.Classes
{
    public class Chord
    {
        /// <summary>
        /// The scale in use for the chord
        /// </summary>
        public Scale UsedScale { get; set; }
        
        /// <summary>
        /// The list of base notes for the scale without modifiers/adds/rems
        /// </summary>
        public Dictionary<int,string> BaseChordNotes { get; set; }

        /// <summary>
        /// The list of notes after all adds/rems/mods have been applied
        /// </summary>
        public List<string> ResultingChord { get; set; }

        /// <summary>
        /// The list of mods to be applied to the chord
        /// </summary>
        public List<ChordModifier> Mods { get; set; }

        /// <summary>
        /// The list of notes to be added to the chord
        /// </summary>
        public List<ChordAdd> Adds { get; set; }

        /// <summary>
        /// The list of notes to be removed from the chord
        /// </summary>
        public List<ChordRemove> Rems { get; set; }

        /// <summary>
        /// The inversion applied to the chord
        /// </summary>
        public ChordInversion Inversion { get; set; }

        /// <summary>
        /// The name of the chord given after all adds/rems/mods have been done
        /// </summary>
        public string Name { get; set; }
    }
}
