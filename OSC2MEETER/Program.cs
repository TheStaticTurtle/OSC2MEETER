using CommandLine;
using SharpOSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using VoicemeeterRemote;



namespace OSC2MEETER {
	class Program {
        public class Options {
            [Option('l', "listen", Default = 8000, Required = false, HelpText = "Port on which to listen to OSC message")]
            public int ListenPort { get; set; }
            [Option('p', "port", Default = 9000, Required = false, HelpText = "Port on which to send to OSC message")]
            public int SendPort { get; set; }
            [Option('i', "sendip", Default = "192.168.2.255", Required = false, HelpText = "Ip to send OSC messages to")]
            public string SendIp { get; set; }
            [Option('v', "verbose", Default = false, Required = false, HelpText = "Verbose incoming OSC messages")]
            public bool Verbose { get; set; }
        }

        static VoicemeeterRemote.Remote remote = new VoicemeeterRemote.Remote();

        static string stringOR(string a, string b) {
            return a.Length > 0 ? a : b;
        }

        static void RunOptions(Options o) {
            Console.WriteLine("OSC will listen on port " + o.ListenPort + " and send to " + o.SendIp + ":" + o.SendPort + "");

            remote.Login();

            var listener = new UDPListener(o.ListenPort);
            var sender = new UDPSender(o.SendIp, o.SendPort);

            VoicemeeterType vmType = remote.GetVMType();
            Console.WriteLine("Successfully connect to " + vmType + " version: " + remote.GetVMVersion());

            while (true) {
                try {
                    OscMessage messageReceived = (OscMessage)listener.Receive();
                    if (messageReceived != null) {
                        // /vm/type/channel/cmd
                        if (messageReceived.Address.StartsWith("/vm/")) {
                            String[] d = messageReceived.Address.Split('/');
                            if (d[2] == "strip" || d[2] == "bus") {
                                switch (d[4]) {
                                    case "gain":
                                        remote.SetParameterFloat(d[2] + "[" + d[3] + "].Gain", (float)messageReceived.Arguments[0]);
                                        break;
                                    case "mute":
                                        remote.SetParameterFloat(d[2] + "[" + d[3] + "].Mute", (float)messageReceived.Arguments[0]);
                                        break;
                                }
                            }
                        }
                       if(o.Verbose) Console.WriteLine(messageReceived.Address + " = " + messageReceived.Arguments[0].ToString());
                    }
                }
                catch (Exception e) { Console.WriteLine("Unknown exception occured: " + e.Message); }

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
                    sender.Send(new SharpOSC.OscMessage("/vm/strip/" + i.ToString() + "/label", stringOR(remote.GetParameterStringUNICODE("Strip[" + i.ToString() + "].Label"), "A" + (i + 1).ToString())));
                    sender.Send(new SharpOSC.OscMessage("/vm/strip/" + i.ToString() + "/level", 1, remote.GetLevel(VoicemeeterLevelType.INPUT_POST_MUTE, (VoicemeeterChannels)(i < vmType.PhysicalCount ? (i * vmType.PhysicalInputChannelCount + 0) : (vmType.PhysicalCount * 2 + (i - vmType.PhysicalCount) * vmType.VirtualChannelCount + 0)))));
                    sender.Send(new SharpOSC.OscMessage("/vm/strip/" + i.ToString() + "/level", 2, remote.GetLevel(VoicemeeterLevelType.INPUT_POST_MUTE, (VoicemeeterChannels)(i < vmType.PhysicalCount ? (i * vmType.PhysicalInputChannelCount + 1) : (vmType.PhysicalCount * 2 + (i - vmType.PhysicalCount) * vmType.VirtualChannelCount + 1)))));
                }

                System.Threading.Thread.Sleep(10);
            }
        }

        static void HandleParseError(IEnumerable<Error> errs) {
            foreach(Error err in errs) {
                Console.WriteLine(err.Tag + " " + err.ToString());
            }
        }

        static void Main(string[] args) {
            Console.CancelKeyPress += new ConsoleCancelEventHandler((object s, ConsoleCancelEventArgs a) => {
                remote.Logout();
                Console.WriteLine("Exiting");
                System.Environment.Exit(0);
            });

            CommandLine.Parser.Default.ParseArguments<Options>(args)
              .WithParsed(RunOptions)
              .WithNotParsed(HandleParseError);

            try {
                remote.Logout();
            }
            catch (ConnectionException ignored) { }
            //Console.ReadLine();
        }
    }
}
