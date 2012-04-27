using System;
using Midi;
namespace Shmidi
{
    class BassBoost : Operation
    {
        public void NoteOn(NoteOnMessage msg, OutputDevice output)
        {
            if(msg.Pitch.Octave()*12+msg.Pitch.PositionInOctave() < 41)
                output.SendNoteOn(msg.Channel, msg.Pitch - 12, msg.Velocity);
        }
        public void NoteOff(NoteOffMessage msg, OutputDevice output)
        {
            if (msg.Pitch.Octave() * 12 + msg.Pitch.PositionInOctave() < 41)
                output.SendNoteOff(msg.Channel, msg.Pitch - 12, msg.Velocity);
        }
        public String Description(){
            return "Boosts the lower notes to add more 'bass'";
        }
    }
}
