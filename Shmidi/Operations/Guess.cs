using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Midi;

namespace Shmidi
{
    class Guess : Operation
    {
        int score = 0;
        int total = 0;
        Pitch currentNote = Pitch.A0;
        List<Pitch> availableNotes;
        Random random;
        Clock clock;
        public Guess()
        {
            random = new Random();
            clock = new Clock(480);
            clock.Start();
            availableNotes = new List<Pitch>();
            availableNotes.Add(Pitch.C3);
            availableNotes.Add(Pitch.CSharp3);
            availableNotes.Add(Pitch.D3);
            availableNotes.Add(Pitch.DSharp3);
            availableNotes.Add(Pitch.E3);
            availableNotes.Add(Pitch.F3);
            availableNotes.Add(Pitch.FSharp3);
            availableNotes.Add(Pitch.G3);
            availableNotes.Add(Pitch.GSharp3);
            availableNotes.Add(Pitch.A3);
            availableNotes.Add(Pitch.ASharp3);
            availableNotes.Add(Pitch.B4);
            availableNotes.Add(Pitch.C4);
            availableNotes.Add(Pitch.CSharp4);
            availableNotes.Add(Pitch.D4);
            availableNotes.Add(Pitch.DSharp4);
            availableNotes.Add(Pitch.E4);
            availableNotes.Add(Pitch.F4);
            availableNotes.Add(Pitch.FSharp4);
            availableNotes.Add(Pitch.G4);
            availableNotes.Add(Pitch.GSharp4);
            availableNotes.Add(Pitch.A4);
            availableNotes.Add(Pitch.ASharp4);
            availableNotes.Add(Pitch.B4);
        }
        public void NoteOn(NoteOnMessage msg, OutputDevice output)
        {
            output.SilenceAllNotes();

            if (!currentNote.Equals(Pitch.A0))
            {
                total++;
                if (msg.Pitch.PositionInOctave().Equals(currentNote.PositionInOctave()))
                {
                    score++;
                    currentNote = availableNotes[random.Next(availableNotes.Count)];
                    clock.Schedule(new NoteOnOffMessage(output,msg.Channel,Pitch.G5,127,clock.Time,clock,1));
                    clock.Schedule(new NoteOnOffMessage(output,msg.Channel,Pitch.C6,127,clock.Time+1,clock,1));
                }
            }
            else
            {
                currentNote = availableNotes[random.Next(availableNotes.Count)];
            }

        }

        public void NoteOff(NoteOffMessage msg, OutputDevice output)
        {
            output.SendNoteOn(msg.Channel, currentNote, 127);
        }

        public string Description()
        {
            if (currentNote.Equals(Pitch.A0))
            {
                return "Guess. (Play a note in this channel to start)";
            }
            else
            {
                return "Guess. "+score+"/"+total;
            }
        }
    }
}
