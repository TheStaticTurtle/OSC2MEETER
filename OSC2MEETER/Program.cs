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

            Console.WriteLine(remote.GetVMType());
            Console.WriteLine(remote.GetVMVersion());

            remote.Logout();

            Console.ReadLine();
        }
	}
}
