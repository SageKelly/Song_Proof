using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongProofWP8.Classes
{
    class ChordAdd
    {
        public enum AddNames
        {
            Add2,
            Add3,
            Add4,
            Add6,
            Add7,
            Add9,
            Add10,
            Add11,
            Add12,
            Add13,
            Add14,
        }

        private static Dictionary<AddNames, int> AddTypes;
        private static Dictionary<AddNames, string> Names;

        static ChordAdd()
        {
            AddTypes = new Dictionary<AddNames, int>()
            {
                {AddNames.Add2,2}
                ,{AddNames.Add3,3}
                ,{AddNames.Add4,4}
                ,{AddNames.Add6,6}
                ,{AddNames.Add7,7}
                ,{AddNames.Add9,2}
                ,{AddNames.Add10,10}
                ,{AddNames.Add11,11}
                ,{AddNames.Add12,12}
                ,{AddNames.Add13,13}
                ,{AddNames.Add14,14}
            };

            Names = new Dictionary<AddNames, string>()
            {
                {AddNames.Add2,"Add 2"}
                ,{AddNames.Add3,"Add 3"}
                ,{AddNames.Add4,"Add 4"}
                ,{AddNames.Add6,"Add 6"}
                ,{AddNames.Add7,"Add 7"}
                ,{AddNames.Add9,"Add 9"}
                ,{AddNames.Add10,"Add 10"}
                ,{AddNames.Add11,"Add 11"}
                ,{AddNames.Add12,"Add 12"}
                ,{AddNames.Add13,"Add 13"}
                ,{AddNames.Add14,"Add 14"}
            };
        }

        public int NoteRefNum { get; set; }

        public string Name { get; set; }

        public ChordAdd(int noteNum, Scale usedScale)
        {
            NoteRefNum = noteNum;

        }

        /// <summary>
        /// Calculates the note to be added to a scale
        /// </summary>
        /// <param name="addType">The pre-build add type</param>
        /// <param name="usedScale">The scale in use</param>
        /// <param name="chordNotes"></param>
        /// <returns>Returns chord after the addition</returns>
        public Dictionary<int, string> Add(AddNames addType, Scale usedScale, Dictionary<int, string> chordNotes)
        {
            Name = Names[addType];
            return Add(AddTypes[addType], usedScale, chordNotes);
        }

        /// <summary>
        /// Calculates the note to be added to a scale
        /// </summary>
        /// <param name="num">The relative note number to add</param>
        /// <param name="usedScale">The referenced scale</param>
        /// <param name="chordNotes">The pre-existing chordnotes</param>
        /// <returns>Returns a note relative to the add num used</returns>
        public Dictionary<int, string> Add(int num, Scale usedScale, Dictionary<int, string> chordNotes)
        {
            Dictionary<int, string> result = chordNotes;
            if (!chordNotes.Keys.Contains(num))
            {

                if (num < usedScale.Notes.Length)
                {
                    NoteRefNum = num;
                    string Note = usedScale.Notes[num % usedScale.Notes.Length];
                    result.Add(NoteRefNum, Note);
                    DetermineName(NoteRefNum);
                }
            }
            return result;
        }



        private void DetermineName(int noteNum)
        {
            bool contains = AddTypes.Values.Contains(noteNum);
            if (contains)
            {
                KeyValuePair<AddNames, int> result = AddTypes.FirstOrDefault(x => x.Value == noteNum);
                Name = Names[result.Key];
            }
            else
            {
                Name = string.Empty;
            }
        }
    }
}
