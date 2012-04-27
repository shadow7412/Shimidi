using System;
using System.Collections.Generic;
using Midi;
namespace Shmidi
{
    class Soundboard
    {
        InputDevice inputDevice;
        OutputDevice outputDevice;
        Dictionary<Channel,Operation> operations;
        List<Pitch> notesDown;
        Channel currentChannel;
        private int output = 0;
        private int input = 0;

        public Soundboard()
        {
            notesDown = new List<Pitch>();
            operations = new Dictionary<Channel,Operation>();
            operations.Add(Channel.Channel1, new Natural());
            operations.Add(Channel.Channel2, new BassBoost());
            operations.Add(Channel.Channel3, new ABC());
            operations.Add(Channel.Channel4, new Guess());

            NextInput();
            NextOutput();
        }

        ~Soundboard()
        {
            if(outputDevice!=null) //shut it up (otherwise the notes just stays held until device is unplugged
                outputDevice.SilenceAllNotes();
        }

        private void Display()
        {
            Console.SetCursorPosition(0, 0);
            if (notesDown.Count == 0)
                Console.BackgroundColor = ConsoleColor.Black;
            else if (notesDown.Count < Enum.GetValues(typeof(ConsoleColor)).Length)
                Console.BackgroundColor = (ConsoleColor)Enum.GetValues(typeof(ConsoleColor)).GetValue(notesDown.Count);
            else
                Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Clear();
            Console.WriteLine(" (F2) Input:  "+(inputDevice==null?"Not found":inputDevice.Name));
            Console.WriteLine(" (F3) Output: " + (outputDevice==null?"Not found":outputDevice.Name)+" (Space to silence)");
            foreach (var operation in operations)
            {
                if (operation.Key.Equals(currentChannel)) Console.Write(">"); else Console.Write(" ");
                Console.WriteLine(operation.Key.Name() + ": " + operation.Value.Description());
            }
            foreach (var note in notesDown)
            {
                Console.Write(note.Octave()+note.NotePreferringSharps().ToString()+" ");
            }
        }
        public void Shh()
        {
            outputDevice.SilenceAllNotes();
        }
        private void NoteOn(NoteOnMessage msg){
            currentChannel = msg.Channel;
            notesDown.Add(msg.Pitch);
            try
            {
                operations[msg.Channel].NoteOn(msg, outputDevice);
            }
            catch (KeyNotFoundException) { }
            finally
            {
                Display();
            }
        }

        private void NoteOff(NoteOffMessage msg)
        {
            notesDown.Remove(msg.Pitch);
            try
            {
                operations[msg.Channel].NoteOff(msg, outputDevice);
            }
            catch (KeyNotFoundException) { }
            finally
            {
                Display();
            }

        }

        public void NextInput(){
            try
            {
                inputDevice.StopReceiving();
                inputDevice.Close();
            }
            catch (NullReferenceException) { }
            if (++input < InputDevice.InstalledDevices.Count)
                inputDevice = InputDevice.InstalledDevices[input];
            else if (InputDevice.InstalledDevices.Count != 0)
                inputDevice = InputDevice.InstalledDevices[input = 0];
            else
            {
                inputDevice = null;
                return;
            }
            inputDevice.Open();
            inputDevice.NoteOn += new InputDevice.NoteOnHandler(NoteOn);
            inputDevice.NoteOff += new InputDevice.NoteOffHandler(NoteOff);
            inputDevice.StartReceiving(null);
            Display();
        }
        public void NextOutput()
        {
            try
            {
                outputDevice.SilenceAllNotes();
                outputDevice.Close();
            }
            catch (NullReferenceException) { }
            if (++output < OutputDevice.InstalledDevices.Count)
                outputDevice = OutputDevice.InstalledDevices[output];
            else if (OutputDevice.InstalledDevices.Count != 0)
                outputDevice = OutputDevice.InstalledDevices[output = 0];
            else
            {
                outputDevice = null;
                return;
            }
            
            outputDevice.Open();
            Display();
        }
    }
}
