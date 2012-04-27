using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shmidi
{
    class ABC:Operation
    {
        public void NoteOn(Midi.NoteOnMessage msg, Midi.OutputDevice output)
        {
            throw new NotImplementedException();
        }

        public void NoteOff(Midi.NoteOffMessage msg, Midi.OutputDevice output)
        {
            throw new NotImplementedException();
        }
        public String Description()
        {
            return "Takes a low note, and turns it into a chord an octave up";
        }
    }
}
