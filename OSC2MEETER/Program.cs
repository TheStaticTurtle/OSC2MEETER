using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;





namespace OSC2MEETER {
	class Program {

        static VoicemeeterRemote remote = new VoicemeeterRemote();

        protected static void myHandler(object sender, ConsoleCancelEventArgs args) {
            remote.Logout();

            Console.WriteLine("Exiting");
            System.Environment.Exit(0);
        }

        static void Main(string[] args) {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(myHandler);
            remote.Login();

            Console.WriteLine("Successfully connect to " + remote.GetVMType() + " version: " + remote.GetVMVersion());
            Console.WriteLine(remote.GetParameterFloat("Strip[0].gain"));
            Console.WriteLine(remote.GetParameterStringA("Bus[0].device.name"));

            while(true){ 
                remote.IsParametersDirty();
                Console.WriteLine(remote.getLevel(VoicemeeterLevelType.OUTPUT, VoicemeeterChannels.VOICEMEETER_POATATO_OUTPUT_A1_01));
                System.Threading.Thread.Sleep(10);
            }

           
            //remote.Logout();

            //Console.ReadLine();
        }
	}
}
