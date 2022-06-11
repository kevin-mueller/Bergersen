using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Tools;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Multimedia;
using Melanchall.DryWetMidi.MusicTheory;
using Melanchall.DryWetMidi.Standards;

/*
 * 1. extract melody from midi
 * 2. extract harmony from midi
 * 3. extract rhythm from midi
 * 
 * 4. map melody to instrument
 * 5. map harmony to instruments
 * 6. map rhythm to instruments
 * 7. generate midi file
 */


var midi = MidiFile.Read("content/testfile.mid");

new CsvConverter().ConvertMidiFileToCsv(midi, "content/testfile.csv", true);

var tracks = midi.GetTrackChunks();

var chordSettings = new ChordDetectionSettings() { NotesMinCount = 3 };

var track = tracks.Last();
var c = track.GetChords(chordSettings);
foreach (var chord in c)
{
    //get lowest note
    var notes = chord.Notes.OrderBy(x => x.NoteNumber).ToList();
    var notesDistance = new Dictionary<Melanchall.DryWetMidi.Interaction.Note, int>();
    for (int i = 0; i < notes.Count; i++)
    {
        //ignore last note.
        if (i == notes.Count - 1)
            continue;

        notesDistance.Add(notes[i], notes[i + 1].NoteNumber - notes[i].NoteNumber);
    }

}

var midiRes = new MidiFile();
midiRes.Chunks.Add(track);

midiRes.Write("content/edited.mid", true);