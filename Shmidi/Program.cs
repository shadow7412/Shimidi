using System;
using System.Collections.Generic;
namespace Shmidi
{
    class Program
    {
        //start program
        static void Main(string[] args)
        {
            Console.Title = "Midi Shmidi.";
            Soundboard sb = new Soundboard();
            ConsoleKey key; 
            Dictionary<ConsoleKey, Action> functions;
            bool more = true;
            functions = new Dictionary<ConsoleKey,Action>();
            //Console keys
            functions.Add(ConsoleKey.F2, sb.NextInput);
            functions.Add(ConsoleKey.F3, sb.NextOutput);
            functions.Add(ConsoleKey.Spacebar, sb.Shh);
            do {
                key = Console.ReadKey(true).Key;
                try{
                    functions[key]();
                } catch (KeyNotFoundException){
                    more = false;
                }
            } while(more);
        }
    }
}