using Midi;
namespace Shmidi
{
    class Natural : Operation
    {
        public void NoteOn(NoteOnMessage msg, OutputDevice output)
        {
            
        }

        public void NoteOff(NoteOffMessage msg, OutputDevice output)
        {
            
        }

        public string Description()
        {
            return "Does nothing";
        }
    }
}
