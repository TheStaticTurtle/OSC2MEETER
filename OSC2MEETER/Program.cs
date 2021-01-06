using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;





namespace OSC2MEETER {
	class Program {
        

        static void Main(string[] args) {

            VoicemeeterRemote remote = new VoicemeeterRemote();
            remote.Login();

            Console.WriteLine("Successfully connect to " + remote.GetVMType() + " version: " + remote.GetVMVersion());
            Console.WriteLine(remote.GetParameterFloat("Strip[0].gain"));
            Console.WriteLine(remote.GetParameterStringA("Bus[0].device.name"));

            for(int i=0; i<10; i++) { 
                remote.IsParametersDirty();
                Console.WriteLine(remote.GetParameterStringA("Bus[0].device.name"));
                System.Threading.Thread.Sleep(100);
            }

           
            remote.Logout();

            Console.ReadLine();
        }
	}
}
