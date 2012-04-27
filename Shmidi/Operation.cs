using System;
using Midi;
namespace Shmidi
{
    interface Operation
    {
        void NoteOn(NoteOnMessage msg, OutputDevice output);
        void NoteOff(NoteOffMessage msg, OutputDevice output);
        String Description();
    }
}
