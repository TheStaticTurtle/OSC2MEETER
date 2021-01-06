using SharpOSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using VoicemeeterRemote;



namespace OSC2MEETER {
	class Program {

        static VoicemeeterRemote.Remote remote = new VoicemeeterRemote.Remote();

        static string stringOR(string a, string b) {
            return a.Length > 0 ? a : b;
        }

        protected static void myHandler(object sender, ConsoleCancelEventArgs args) {
            remote.Logout();

            Console.WriteLine("Exiting");
            System.Environment.Exit(0);
        }

        static void Main(string[] args) {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(myHandler);
            remote.Login();


            var listener = new UDPListener(8000);
            var sender = new UDPSender("192.168.2.31", 9000);

            VoicemeeterType vmType = remote.GetVMType();
            Console.WriteLine("Successfully connect to " + vmType + " version: " + remote.GetVMVersion());

            while (true) {
                try {
                    OscMessage messageReceived = (OscMessage)listener.Receive();
                    if (messageReceived != null) {
                        // /vm/type/channel/cmd
                        if (messageReceived.Address.StartsWith("/vm/")) {
                            String[] d = messageReceived.Address.Split('/');
                            if(d[2] == "strip" || d[2] == "bus") {
                                switch (d[4]) {
                                    case "gain":
                                        remote.SetParameterFloat(d[2]+"[" + d[3] + "].Gain", (float)messageReceived.Arguments[0]);
                                        break;
                                    case "mute":
                                        remote.SetParameterFloat(d[2]+"[" + d[3] + "].Mute", (float)messageReceived.Arguments[0]);
                                        break;
                                }
                            }
                        }
                        Console.WriteLine(messageReceived.Address + " = " + messageReceived.Arguments[0].ToString());
                    }
                } catch (Exception e) { Console.WriteLine("Unknown exception occured: " + e.Message); }

                if (vmType.ToString() == VoicemeeterType.VOICEMEETER.ToString()) {
                }
                if (vmType.ToString() == VoicemeeterType.VOICEMEETER_BANANA.ToString()) {
                }
                if (vmType.ToString() == VoicemeeterType.VOICEMEETER_POATATO.ToString()) {
                }

                for (int i = 0; i < (vmType.PhysicalCount + vmType.VirtualCount); i++) {
                    remote.IsParametersDirty();
                    sender.Send(new SharpOSC.OscMessage("/vm/bus/" + i.ToString() + "/gain", remote.GetParameterFloat("Bus[" + i.ToString() + "].Gain")));
                    sender.Send(new SharpOSC.OscMessage("/vm/bus/" + i.ToString() + "/mute", remote.GetParameterFloat("Bus[" + i.ToString() + "].Mute")));
                    sender.Send(new SharpOSC.OscMessage("/vm/bus/" + i.ToString() + "/level", 1, remote.GetLevel(VoicemeeterLevelType.OUTPUT, (VoicemeeterChannels)(i * vmType.PhysicalOutputChannelCount))));
                    sender.Send(new SharpOSC.OscMessage("/vm/bus/" + i.ToString() + "/level", 2, remote.GetLevel(VoicemeeterLevelType.OUTPUT, (VoicemeeterChannels)(i * vmType.PhysicalOutputChannelCount + 1))));

                    remote.IsParametersDirty();
                    sender.Send(new SharpOSC.OscMessage("/vm/strip/" + i.ToString() + "/gain", remote.GetParameterFloat("Strip[" + i.ToString() + "].Gain")));
                    sender.Send(new SharpOSC.OscMessage("/vm/strip/" + i.ToString() + "/mute", remote.GetParameterFloat("Strip[" + i.ToString() + "].Mute")));
                    sender.Send(new SharpOSC.OscMessage("/vm/strip/" + i.ToString() + "/label", stringOR(remote.GetParameterStringUNICODE("Strip[" + i.ToString() + "].Label"), "A"+(i+1).ToString())));
                    sender.Send(new SharpOSC.OscMessage("/vm/strip/" + i.ToString() + "/level", 1, remote.GetLevel(VoicemeeterLevelType.INPUT_POST_MUTE, (VoicemeeterChannels)(i < vmType.PhysicalCount ? (i * vmType.PhysicalInputChannelCount + 0) : (vmType.PhysicalCount * 2 + (i - vmType.PhysicalCount) * vmType.VirtualChannelCount + 0)))));
                    sender.Send(new SharpOSC.OscMessage("/vm/strip/" + i.ToString() + "/level", 2, remote.GetLevel(VoicemeeterLevelType.INPUT_POST_MUTE, (VoicemeeterChannels)(i < vmType.PhysicalCount ? (i * vmType.PhysicalInputChannelCount + 1) : (vmType.PhysicalCount * 2 + (i - vmType.PhysicalCount) * vmType.VirtualChannelCount + 1)))));
                }
            }


            //remote.Logout();
            //Console.ReadLine();
        }
	}
}
