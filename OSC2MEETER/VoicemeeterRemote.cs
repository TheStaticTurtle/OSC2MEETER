using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VoicemeeterRemote {
    class ConnectionException : Exception {
        public ConnectionException() : base() { }
        public ConnectionException(string message) : base(message) { }
        public ConnectionException(string message, Exception inner) : base(message, inner) { }
    }
    class NotInstalledException : Exception {
        public NotInstalledException() : base() { }
        public NotInstalledException(string message) : base(message) { }
        public NotInstalledException(string message, Exception inner) : base(message, inner) { }
    }
    class StructutreMisMatchException : Exception {
        public StructutreMisMatchException() : base() { }
        public StructutreMisMatchException(string message) : base(message) { }
        public StructutreMisMatchException(string message, Exception inner) : base(message, inner) { }
    }

    public class VoicemeeterType {
        private VoicemeeterType(string value) { Value = value; }

        public string Value { get; set; }
        override public string ToString() { return this.Value; }

        public static VoicemeeterType VOICEMEETER         { get { return new VoicemeeterType("Voicemeeter"); } }
        public static VoicemeeterType VOICEMEETER_BANANA  { get { return new VoicemeeterType("Voicemeeter Banana"); } }
        public static VoicemeeterType VOICEMEETER_POATATO { get { return new VoicemeeterType("Voicemeeter Poatato"); } }
        public static VoicemeeterType VOICEMEETER_UNKNOWN { get { return new VoicemeeterType("Unknown Voicemeeter"); } }
    }
    
    enum VoicemeeterLevelType {
        INPUT_PRE = 0,
        INPUT_POST_FADER = 1,
        INPUT_POST_MUTE = 2,
        OUTPUT = 3
    }
    enum VoicemeeterChannels {
        VOICEMEETER_STRIP_1_LEFT  = 0,
        VOICEMEETER_STRIP_1_RIGHT = 1,
        VOICEMEETER_STRIP_2_LEFT  = 2,
        VOICEMEETER_STRIP_2_RIGHT = 3,
        VOICEMEETER_VIRTUAL_LEFT = 4,
        VOICEMEETER_VIRTUAL_RIGHT = 5,
        VOICEMEETER_VIRTUAL_03 = 6,
        VOICEMEETER_VIRTUAL_04 = 7,
        VOICEMEETER_VIRTUAL_05 = 8,
        VOICEMEETER_VIRTUAL_06 = 9,
        VOICEMEETER_VIRTUAL_07 = 10,
        VOICEMEETER_VIRTUAL_08 = 11,

        VOICEMEETER_OUTPUT_A1A2_LEFT = 0,
        VOICEMEETER_OUTPUT_A1A2_RIGHT = 1,
        VOICEMEETER_OUTPUT_A1A2_03 = 2,
        VOICEMEETER_OUTPUT_A1A2_04 = 3,
        VOICEMEETER_OUTPUT_A1A2_05 = 4,
        VOICEMEETER_OUTPUT_A1A2_06 = 5,
        VOICEMEETER_OUTPUT_A1A2_07 = 6,
        VOICEMEETER_OUTPUT_A1A2_08 = 7,
        VOICEMEETER_OUTPUT_VIRT_LEFT = 8,
        VOICEMEETER_OUTPUT_VIRT_RIGHT = 9,
        VOICEMEETER_OUTPUT_VIRT_03 = 10,
        VOICEMEETER_OUTPUT_VIRT_04 = 11,
        VOICEMEETER_OUTPUT_VIRT_05 = 12,
        VOICEMEETER_OUTPUT_VIRT_06 = 13,
        VOICEMEETER_OUTPUT_VIRT_07 = 14,
        VOICEMEETER_OUTPUT_VIRT_08 = 15,


        VOICEMEETER_BANANA_STRIP_01_LEFT = 0,
        VOICEMEETER_BANANA_STRIP_01_RIGHT = 1,
        VOICEMEETER_BANANA_STRIP_01_01 = 0,
        VOICEMEETER_BANANA_STRIP_01_02 = 1,
        VOICEMEETER_BANANA_STRIP_02_LEFT = 2,
        VOICEMEETER_BANANA_STRIP_02_RIGHT = 3,
        VOICEMEETER_BANANA_STRIP_02_01 = 2,
        VOICEMEETER_BANANA_STRIP_02_02 = 3,
        VOICEMEETER_BANANA_STRIP_03_LEFT = 4,
        VOICEMEETER_BANANA_STRIP_03_RIGHT = 5,
        VOICEMEETER_BANANA_STRIP_03_01 = 4,
        VOICEMEETER_BANANA_STRIP_03_02 = 5,
        VOICEMEETER_BANANA_VIRTUAL_01_LEFT = 6,
        VOICEMEETER_BANANA_VIRTUAL_01_RIGHT = 7,
        VOICEMEETER_BANANA_VIRTUAL_01_01 = 6,
        VOICEMEETER_BANANA_VIRTUAL_01_02 = 7,
        VOICEMEETER_BANANA_VIRTUAL_01_03 = 8,
        VOICEMEETER_BANANA_VIRTUAL_01_04 = 9,
        VOICEMEETER_BANANA_VIRTUAL_01_05 = 10,
        VOICEMEETER_BANANA_VIRTUAL_01_06 = 11,
        VOICEMEETER_BANANA_VIRTUAL_01_07 = 12,
        VOICEMEETER_BANANA_VIRTUAL_01_08 = 13,
        VOICEMEETER_BANANA_VIRTUAL_02_LEFT = 14,
        VOICEMEETER_BANANA_VIRTUAL_02_RIGHT = 15,
        VOICEMEETER_BANANA_VIRTUAL_02_01 = 14,
        VOICEMEETER_BANANA_VIRTUAL_02_02 = 15,
        VOICEMEETER_BANANA_VIRTUAL_02_03 = 16,
        VOICEMEETER_BANANA_VIRTUAL_02_04 = 17,
        VOICEMEETER_BANANA_VIRTUAL_02_05 = 18,
        VOICEMEETER_BANANA_VIRTUAL_02_06 = 19,
        VOICEMEETER_BANANA_VIRTUAL_02_07 = 20,
        VOICEMEETER_BANANA_VIRTUAL_02_08 = 21,

        VOICEMEETER_BANANA_OUTPUT_A1_LEFT = 0,
        VOICEMEETER_BANANA_OUTPUT_A1_RIGHT = 1,
        VOICEMEETER_BANANA_OUTPUT_A1_01 = 0,
        VOICEMEETER_BANANA_OUTPUT_A1_02 = 1,
        VOICEMEETER_BANANA_OUTPUT_A1_03 = 2,
        VOICEMEETER_BANANA_OUTPUT_A1_04 = 3,
        VOICEMEETER_BANANA_OUTPUT_A1_05 = 4,
        VOICEMEETER_BANANA_OUTPUT_A1_06 = 5,
        VOICEMEETER_BANANA_OUTPUT_A1_07 = 6,
        VOICEMEETER_BANANA_OUTPUT_A1_08 = 7,
        VOICEMEETER_BANANA_OUTPUT_A2_LEFT = 8,
        VOICEMEETER_BANANA_OUTPUT_A2_RIGHT = 9,
        VOICEMEETER_BANANA_OUTPUT_A2_01 = 8,
        VOICEMEETER_BANANA_OUTPUT_A2_02 = 9,
        VOICEMEETER_BANANA_OUTPUT_A2_03 = 10,
        VOICEMEETER_BANANA_OUTPUT_A2_04 = 11,
        VOICEMEETER_BANANA_OUTPUT_A2_05 = 12,
        VOICEMEETER_BANANA_OUTPUT_A2_06 = 13,
        VOICEMEETER_BANANA_OUTPUT_A2_07 = 14,
        VOICEMEETER_BANANA_OUTPUT_A2_08 = 15,
        VOICEMEETER_BANANA_OUTPUT_A3_LEFT = 16,
        VOICEMEETER_BANANA_OUTPUT_A3_RIGHT = 17,
        VOICEMEETER_BANANA_OUTPUT_A3_01 = 16,
        VOICEMEETER_BANANA_OUTPUT_A3_02 = 17,
        VOICEMEETER_BANANA_OUTPUT_A3_03 = 18,
        VOICEMEETER_BANANA_OUTPUT_A3_04 = 19,
        VOICEMEETER_BANANA_OUTPUT_A3_05 = 20,
        VOICEMEETER_BANANA_OUTPUT_A3_06 = 21,
        VOICEMEETER_BANANA_OUTPUT_A3_07 = 22,
        VOICEMEETER_BANANA_OUTPUT_A3_08 = 23,
        VOICEMEETER_BANANA_OUTPUT_VIRT01_LEFT = 24,
        VOICEMEETER_BANANA_OUTPUT_VIRT01_RIGHT = 25,
        VOICEMEETER_BANANA_OUTPUT_VIRT01_01 = 24,
        VOICEMEETER_BANANA_OUTPUT_VIRT01_02 = 25,
        VOICEMEETER_BANANA_OUTPUT_VIRT01_03 = 26,
        VOICEMEETER_BANANA_OUTPUT_VIRT01_04 = 27,
        VOICEMEETER_BANANA_OUTPUT_VIRT01_05 = 28,
        VOICEMEETER_BANANA_OUTPUT_VIRT01_06 = 29,
        VOICEMEETER_BANANA_OUTPUT_VIRT01_07 = 30,
        VOICEMEETER_BANANA_OUTPUT_VIRT01_08 = 31,
        VOICEMEETER_BANANA_OUTPUT_VIRT02_LEFT = 32,
        VOICEMEETER_BANANA_OUTPUT_VIRT02_RIGHT = 33,
        VOICEMEETER_BANANA_OUTPUT_VIRT02_01 = 32,
        VOICEMEETER_BANANA_OUTPUT_VIRT02_02 = 33,
        VOICEMEETER_BANANA_OUTPUT_VIRT02_03 = 34,
        VOICEMEETER_BANANA_OUTPUT_VIRT02_04 = 35,
        VOICEMEETER_BANANA_OUTPUT_VIRT02_05 = 36,
        VOICEMEETER_BANANA_OUTPUT_VIRT02_06 = 37,
        VOICEMEETER_BANANA_OUTPUT_VIRT02_07 = 38,
        VOICEMEETER_BANANA_OUTPUT_VIRT02_08 = 39,


        VOICEMEETER_POATATO_STRIP_01_LEFT = 0,
        VOICEMEETER_POATATO_STRIP_01_RIGHT = 1,
        VOICEMEETER_POATATO_STRIP_01_01 = 0,
        VOICEMEETER_POATATO_STRIP_01_02 = 1,
        VOICEMEETER_POATATO_STRIP_02_LEFT = 2,
        VOICEMEETER_POATATO_STRIP_02_RIGHT = 3,
        VOICEMEETER_POATATO_STRIP_02_01 = 2,
        VOICEMEETER_POATATO_STRIP_02_02 = 3,
        VOICEMEETER_POATATO_STRIP_03_LEFT = 4,
        VOICEMEETER_POATATO_STRIP_03_RIGHT = 5,
        VOICEMEETER_POATATO_STRIP_03_01 = 4,
        VOICEMEETER_POATATO_STRIP_03_02 = 5,
        VOICEMEETER_POATATO_STRIP_04_LEFT = 6,
        VOICEMEETER_POATATO_STRIP_04_RIGHT = 7,
        VOICEMEETER_POATATO_STRIP_04_01 = 6,
        VOICEMEETER_POATATO_STRIP_04_02 = 7,
        VOICEMEETER_POATATO_STRIP_05_LEFT = 8,
        VOICEMEETER_POATATO_STRIP_05_RIGHT = 9,
        VOICEMEETER_POATATO_STRIP_05_01 = 8,
        VOICEMEETER_POATATO_STRIP_05_02 = 9,
        VOICEMEETER_POATATO_VIRTUAL_01_LEFT = 10,
        VOICEMEETER_POATATO_VIRTUAL_01_RIGHT = 11,
        VOICEMEETER_POATATO_VIRTUAL_01_01 = 10,
        VOICEMEETER_POATATO_VIRTUAL_01_02 = 11,
        VOICEMEETER_POATATO_VIRTUAL_01_03 = 12,
        VOICEMEETER_POATATO_VIRTUAL_01_04 = 13,
        VOICEMEETER_POATATO_VIRTUAL_01_05 = 14,
        VOICEMEETER_POATATO_VIRTUAL_01_06 = 15,
        VOICEMEETER_POATATO_VIRTUAL_01_07 = 16,
        VOICEMEETER_POATATO_VIRTUAL_01_08 = 17,
        VOICEMEETER_POATATO_VIRTUAL_02_LEFT = 18,
        VOICEMEETER_POATATO_VIRTUAL_02_RIGHT = 19,
        VOICEMEETER_POATATO_VIRTUAL_02_01 = 18,
        VOICEMEETER_POATATO_VIRTUAL_02_02 = 19,
        VOICEMEETER_POATATO_VIRTUAL_02_03 = 20,
        VOICEMEETER_POATATO_VIRTUAL_02_04 = 21,
        VOICEMEETER_POATATO_VIRTUAL_02_05 = 22,
        VOICEMEETER_POATATO_VIRTUAL_02_06 = 23,
        VOICEMEETER_POATATO_VIRTUAL_02_07 = 24,
        VOICEMEETER_POATATO_VIRTUAL_02_08 = 25,
        VOICEMEETER_POATATO_VIRTUAL_03_LEFT = 26,
        VOICEMEETER_POATATO_VIRTUAL_03_RIGHT = 27,
        VOICEMEETER_POATATO_VIRTUAL_03_01 = 26,
        VOICEMEETER_POATATO_VIRTUAL_03_02 = 27,
        VOICEMEETER_POATATO_VIRTUAL_03_03 = 28,
        VOICEMEETER_POATATO_VIRTUAL_03_04 = 29,
        VOICEMEETER_POATATO_VIRTUAL_03_05 = 30,
        VOICEMEETER_POATATO_VIRTUAL_03_06 = 31,
        VOICEMEETER_POATATO_VIRTUAL_03_07 = 32,
        VOICEMEETER_POATATO_VIRTUAL_03_08 = 33,

        VOICEMEETER_POATATO_OUTPUT_A1_LEFT = 0,
        VOICEMEETER_POATATO_OUTPUT_A1_RIGHT = 1,
        VOICEMEETER_POATATO_OUTPUT_A1_01 = 0,
        VOICEMEETER_POATATO_OUTPUT_A1_02 = 1,
        VOICEMEETER_POATATO_OUTPUT_A1_03 = 2,
        VOICEMEETER_POATATO_OUTPUT_A1_04 = 3,
        VOICEMEETER_POATATO_OUTPUT_A1_05 = 4,
        VOICEMEETER_POATATO_OUTPUT_A1_06 = 5,
        VOICEMEETER_POATATO_OUTPUT_A1_07 = 6,
        VOICEMEETER_POATATO_OUTPUT_A1_08 = 7,
        VOICEMEETER_POATATO_OUTPUT_A2_LEFT = 8,
        VOICEMEETER_POATATO_OUTPUT_A2_RIGHT = 9,
        VOICEMEETER_POATATO_OUTPUT_A2_01 = 8,
        VOICEMEETER_POATATO_OUTPUT_A2_02 = 9,
        VOICEMEETER_POATATO_OUTPUT_A2_03 = 10,
        VOICEMEETER_POATATO_OUTPUT_A2_04 = 11,
        VOICEMEETER_POATATO_OUTPUT_A2_05 = 12,
        VOICEMEETER_POATATO_OUTPUT_A2_06 = 13,
        VOICEMEETER_POATATO_OUTPUT_A2_07 = 14,
        VOICEMEETER_POATATO_OUTPUT_A2_08 = 15,
        VOICEMEETER_POATATO_OUTPUT_A3_LEFT = 16,
        VOICEMEETER_POATATO_OUTPUT_A3_RIGHT = 17,
        VOICEMEETER_POATATO_OUTPUT_A3_01 = 16,
        VOICEMEETER_POATATO_OUTPUT_A3_02 = 17,
        VOICEMEETER_POATATO_OUTPUT_A3_03 = 18,
        VOICEMEETER_POATATO_OUTPUT_A3_04 = 19,
        VOICEMEETER_POATATO_OUTPUT_A3_05 = 20,
        VOICEMEETER_POATATO_OUTPUT_A3_06 = 21,
        VOICEMEETER_POATATO_OUTPUT_A3_07 = 22,
        VOICEMEETER_POATATO_OUTPUT_A3_08 = 23,
        VOICEMEETER_POATATO_OUTPUT_A4_LEFT = 24,
        VOICEMEETER_POATATO_OUTPUT_A4_RIGHT = 25,
        VOICEMEETER_POATATO_OUTPUT_A4_01 = 24,
        VOICEMEETER_POATATO_OUTPUT_A4_02 = 25,
        VOICEMEETER_POATATO_OUTPUT_A4_03 = 26,
        VOICEMEETER_POATATO_OUTPUT_A4_04 = 27,
        VOICEMEETER_POATATO_OUTPUT_A4_05 = 28,
        VOICEMEETER_POATATO_OUTPUT_A4_06 = 29,
        VOICEMEETER_POATATO_OUTPUT_A4_07 = 30,
        VOICEMEETER_POATATO_OUTPUT_A4_08 = 31,
        VOICEMEETER_POATATO_OUTPUT_A5_LEFT = 32,
        VOICEMEETER_POATATO_OUTPUT_A5_RIGHT = 33,
        VOICEMEETER_POATATO_OUTPUT_A5_01 = 32,
        VOICEMEETER_POATATO_OUTPUT_A5_02 = 33,
        VOICEMEETER_POATATO_OUTPUT_A5_03 = 34,
        VOICEMEETER_POATATO_OUTPUT_A5_04 = 35,
        VOICEMEETER_POATATO_OUTPUT_A5_05 = 36,
        VOICEMEETER_POATATO_OUTPUT_A5_06 = 37,
        VOICEMEETER_POATATO_OUTPUT_A5_07 = 38,
        VOICEMEETER_POATATO_OUTPUT_A5_08 = 39,
        VOICEMEETER_POATATO_OUTPUT_VIRT01_LEFT = 40,
        VOICEMEETER_POATATO_OUTPUT_VIRT01_RIGHT = 41,
        VOICEMEETER_POATATO_OUTPUT_VIRT01_01 = 40,
        VOICEMEETER_POATATO_OUTPUT_VIRT01_02 = 41,
        VOICEMEETER_POATATO_OUTPUT_VIRT01_03 = 42,
        VOICEMEETER_POATATO_OUTPUT_VIRT01_04 = 43,
        VOICEMEETER_POATATO_OUTPUT_VIRT01_05 = 44,
        VOICEMEETER_POATATO_OUTPUT_VIRT01_06 = 45,
        VOICEMEETER_POATATO_OUTPUT_VIRT01_07 = 46,
        VOICEMEETER_POATATO_OUTPUT_VIRT01_08 = 47,
        VOICEMEETER_POATATO_OUTPUT_VIRT02_LEFT = 48,
        VOICEMEETER_POATATO_OUTPUT_VIRT02_RIGHT = 49,
        VOICEMEETER_POATATO_OUTPUT_VIRT02_01 = 48,
        VOICEMEETER_POATATO_OUTPUT_VIRT02_02 = 49,
        VOICEMEETER_POATATO_OUTPUT_VIRT02_03 = 50,
        VOICEMEETER_POATATO_OUTPUT_VIRT02_04 = 51,
        VOICEMEETER_POATATO_OUTPUT_VIRT02_05 = 52,
        VOICEMEETER_POATATO_OUTPUT_VIRT02_06 = 53,
        VOICEMEETER_POATATO_OUTPUT_VIRT02_07 = 54,
        VOICEMEETER_POATATO_OUTPUT_VIRT02_08 = 55,
        VOICEMEETER_POATATO_OUTPUT_VIRT03_LEFT = 56,
        VOICEMEETER_POATATO_OUTPUT_VIRT03_RIGHT = 57,
        VOICEMEETER_POATATO_OUTPUT_VIRT03_01 = 56,
        VOICEMEETER_POATATO_OUTPUT_VIRT03_02 = 57,
        VOICEMEETER_POATATO_OUTPUT_VIRT03_03 = 58,
        VOICEMEETER_POATATO_OUTPUT_VIRT03_04 = 59,
        VOICEMEETER_POATATO_OUTPUT_VIRT03_05 = 60,
        VOICEMEETER_POATATO_OUTPUT_VIRT03_06 = 61,
        VOICEMEETER_POATATO_OUTPUT_VIRT03_07 = 62,
        VOICEMEETER_POATATO_OUTPUT_VIRT03_08 = 63,

    }

    unsafe class Remote {
        static String RegUnInstallDirKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
        static String InstallerUnInstKey = "VB:Voicemeeter {17359A74-1236-5467}";

        [DllImport("kernel32.dll")]
        static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr LoadLibrary(string libname);


        [DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        delegate int DVM_Login();
        delegate int DVM_LogOut();
        delegate int DVM_RunVoicemeeter(long* res);
        delegate int DVM_GetVoicemeeterType(long* vmType);
        delegate int DVM_GetVoicemeeterVersion(long* vmVersion);

        delegate int DVBVMR_IsParametersDirty();
        delegate int DVBVMR_GetParameterFloat(string param, float* value);
        delegate int DVBVMR_GetParameterStringA(string param, IntPtr value);
        delegate int DVBVMR_GetParameterStringW(string param, IntPtr value);
        delegate int DVBVMR_GetLevel(int nType, int nuChannel, float* res);
        delegate int DVBVMR_GetMidiMessage(IntPtr buffer, int maxLength);

        delegate int DVBVMR_SetParameterFloat(string param, float value);
        delegate int DVBVMR_SetParameterStringA(string param, IntPtr value);
        delegate int DVBVMR_SetParameterStringW(string param, IntPtr value);
        delegate int DVBVMR_SetParameters(IntPtr script);
        delegate int DVBVMR_SetParametersW(IntPtr script);

        //Connection
        DVM_Login VM_Login;
        DVM_LogOut VM_Logout;
        DVM_RunVoicemeeter VM_RunVoicemeeter;
        DVM_GetVoicemeeterType VM_GetVoicemeeterType;
        DVM_GetVoicemeeterVersion VM_GetVoicemeeterVersion;

        // Get parameters
        DVBVMR_IsParametersDirty VBVMR_IsParametersDirty;
        DVBVMR_GetParameterFloat VBVMR_GetParameterFloat;
        DVBVMR_GetParameterStringA VBVMR_GetParameterStringA;
        DVBVMR_GetParameterStringW VBVMR_GetParameterStringW;
        DVBVMR_GetLevel VBVMR_GetLevel;
        DVBVMR_GetMidiMessage VBVMR_GetMidiMessage;

        // Set parametters
        DVBVMR_SetParameterFloat VBVMR_SetParameterFloat;
        DVBVMR_SetParameterStringA VBVMR_SetParameterStringA;
        DVBVMR_SetParameterStringW VBVMR_SetParameterStringW;
        DVBVMR_SetParameters VBVMR_SetParameters;
        DVBVMR_SetParametersW VBVMR_SetParametersW;


        static String FindVoiceMeeterDLL() {
            //return @"C:\Program Files (x86)\VB\Voicemeeter\VoicemeeterRemote.dll";
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(RegUnInstallDirKey+"\\"+InstallerUnInstKey)) {
                if (key != null) {
                    Object o = key.GetValue("UninstallString");
                    if (o != null) {
                        String ex = o.ToString();
                        String[] ps = ex.Split('\\');
                        ps = ps.Take(ps.Count() - 1).ToArray();
                        String installPath = String.Join("\\", ps);
                        String dllPath = installPath + "\\VoicemeeterRemote" + (IntPtr.Size == 8 ? "64" : "") + ".dll";
                        return dllPath;
                    }
                }
            }
            return null;
        }

        public Remote() {
            String dllPath = FindVoiceMeeterDLL();
            if (dllPath == null) throw new DllNotFoundException("Failed to find the voicemeeter dll");

            IntPtr Handle = LoadLibrary(dllPath);
            if (Handle == IntPtr.Zero) {
                int errorCode = Marshal.GetLastWin32Error();
                throw new Exception(string.Format("Failed to load library (ErrorCode: {0})", errorCode));
            }

            VM_Login = Marshal.GetDelegateForFunctionPointer(GetProcAddress(Handle, "VBVMR_Login"), typeof(DVM_Login)) as DVM_Login;
            VM_Logout = Marshal.GetDelegateForFunctionPointer(GetProcAddress(Handle, "VBVMR_Logout"), typeof(DVM_LogOut)) as DVM_LogOut;
            VM_RunVoicemeeter = Marshal.GetDelegateForFunctionPointer(GetProcAddress(Handle, "VBVMR_RunVoicemeeter"), typeof(DVM_RunVoicemeeter)) as DVM_RunVoicemeeter;
            VM_GetVoicemeeterType = Marshal.GetDelegateForFunctionPointer(GetProcAddress(Handle, "VBVMR_GetVoicemeeterType"), typeof(DVM_GetVoicemeeterType)) as DVM_GetVoicemeeterType;
            VM_GetVoicemeeterVersion = Marshal.GetDelegateForFunctionPointer(GetProcAddress(Handle, "VBVMR_GetVoicemeeterVersion"), typeof(DVM_GetVoicemeeterVersion)) as DVM_GetVoicemeeterVersion;

            VBVMR_IsParametersDirty = Marshal.GetDelegateForFunctionPointer(GetProcAddress(Handle, "VBVMR_IsParametersDirty"), typeof(DVBVMR_IsParametersDirty)) as DVBVMR_IsParametersDirty;
            VBVMR_GetParameterFloat = Marshal.GetDelegateForFunctionPointer(GetProcAddress(Handle, "VBVMR_GetParameterFloat"), typeof(DVBVMR_GetParameterFloat)) as DVBVMR_GetParameterFloat;
            VBVMR_GetParameterStringA = Marshal.GetDelegateForFunctionPointer(GetProcAddress(Handle, "VBVMR_GetParameterStringA"), typeof(DVBVMR_GetParameterStringA)) as DVBVMR_GetParameterStringA;
            VBVMR_GetParameterStringW = Marshal.GetDelegateForFunctionPointer(GetProcAddress(Handle, "VBVMR_GetParameterStringW"), typeof(DVBVMR_GetParameterStringW)) as DVBVMR_GetParameterStringW;
            VBVMR_GetLevel = Marshal.GetDelegateForFunctionPointer(GetProcAddress(Handle, "VBVMR_GetLevel"), typeof(DVBVMR_GetLevel)) as DVBVMR_GetLevel;
            VBVMR_GetMidiMessage = Marshal.GetDelegateForFunctionPointer(GetProcAddress(Handle, "VBVMR_GetMidiMessage"), typeof(DVBVMR_GetMidiMessage)) as DVBVMR_GetMidiMessage;

            VBVMR_SetParameterFloat = Marshal.GetDelegateForFunctionPointer(GetProcAddress(Handle, "VBVMR_SetParameterFloat"), typeof(DVBVMR_SetParameterFloat)) as DVBVMR_SetParameterFloat;
            VBVMR_SetParameterStringA = Marshal.GetDelegateForFunctionPointer(GetProcAddress(Handle, "VBVMR_SetParameterStringA"), typeof(DVBVMR_SetParameterStringA)) as DVBVMR_SetParameterStringA;
            VBVMR_SetParameterStringW = Marshal.GetDelegateForFunctionPointer(GetProcAddress(Handle, "VBVMR_SetParameterStringW"), typeof(DVBVMR_SetParameterStringW)) as DVBVMR_SetParameterStringW;
            VBVMR_SetParameters = Marshal.GetDelegateForFunctionPointer(GetProcAddress(Handle, "VBVMR_SetParameters"), typeof(DVBVMR_SetParameters)) as DVBVMR_SetParameters;
            VBVMR_SetParametersW = Marshal.GetDelegateForFunctionPointer(GetProcAddress(Handle, "VBVMR_SetParametersW"), typeof(DVBVMR_SetParametersW)) as DVBVMR_SetParametersW;
        }

        public void Login() {
            int rep = VM_Login.Invoke();
            if (rep == -1) throw new ConnectionException("Can not get client");
            if (rep == -2) throw new ConnectionException("Unexpected login (logout was excepted before");
        }
        public void Logout() {
            int res = VM_Logout.Invoke();
            if (res != 0) throw new ConnectionException("Failed to logout");
        }
        public void RunVoicemeeter() {
            long res;
            VM_RunVoicemeeter.Invoke(&res);
            if (res == 0) return;
            else throw new NotInstalledException("Voicemeeter is not installed but for some reason you have the DLL");
        }
        public String GetVMVersion() {
            long vmVersion;
            int rep = VM_GetVoicemeeterVersion.Invoke(&vmVersion);
            if (rep == 0) {
                long v1 = (vmVersion & 0xFF000000) >> 24;
                long v2 = (vmVersion & 0x00FF0000) >> 16;
                long v3 = (vmVersion & 0x0000FF00) >> 8;
                long v4 = vmVersion & 0x000000FF;
                return v1.ToString() + "." + v2.ToString() + "." + v3.ToString() + "." + v4.ToString();
            }
            else if (rep == -1) throw new ConnectionException("Could not get the voicemeeter client");
            else if (rep == -2) throw new ConnectionException("Could not get the server");
            else throw new Exception("Failed to retrive VoiceMeeter version code, err " + rep.ToString());
        }
        public VoicemeeterType GetVMType() {
            long vmType;
            int rep = VM_GetVoicemeeterType.Invoke(&vmType);
            if (rep == 0) {
                switch (vmType) {
                    case 1:
                        return VoicemeeterType.VOICEMEETER;
                    case 2:
                        return VoicemeeterType.VOICEMEETER_BANANA;
                    case 3:
                        return VoicemeeterType.VOICEMEETER_POATATO;
                    default:
                        return VoicemeeterType.VOICEMEETER_UNKNOWN;
                }
            }
            else if (rep == -1) throw new ConnectionException("Could not get the voicemeeter client");
            else if (rep == -2) throw new ConnectionException("Could not get the server");
            else return VoicemeeterType.VOICEMEETER_UNKNOWN; ;
        }

        public bool IsParametersDirty() {
            int rep = VBVMR_IsParametersDirty.Invoke();
            if (rep == 0)  return true;
            if (rep == -1) throw new ConnectionException("Can not get client");
            if (rep == -2) throw new ConnectionException("Unexpected login (logout was excepted before");
            if (rep == -3) throw new ArgumentException("Unknown parametter");
            if (rep == -5) throw new StructutreMisMatchException("Unknown parametter");
            return false;
        }
        public float GetParameterFloat(String name) {
            float res;
            int rep = VBVMR_GetParameterFloat.Invoke(name, &res);
            if (rep == 0) return res;
            if (rep == -1) throw new ConnectionException("Can not get client");
            if (rep == -2) throw new ConnectionException("Unexpected login (logout was excepted before");
            if (rep == -3) throw new ArgumentException("Unknown parametter");
            if (rep == -5) throw new StructutreMisMatchException("Unknown parametter");
            throw new Exception("Unknown error");
        }
        IntPtr getParmStringAResponse = Marshal.AllocHGlobal(512); //I don't know why GetParameterStringA work now, it crashed all evening with the "Access violation" error code whil exiting
        public string GetParameterStringASCII(String name) {
            int rep = VBVMR_GetParameterStringA.Invoke(name, getParmStringAResponse);
            if (rep == 0) {
                return Marshal.PtrToStringAnsi((IntPtr)getParmStringAResponse);
            }
            if (rep == -1) throw new ConnectionException("Can not get client");
            if (rep == -2) throw new ConnectionException("Unexpected login (logout was excepted before");
            if (rep == -3) throw new ArgumentException("Unknown parametter");
            if (rep == -5) throw new StructutreMisMatchException("Unknown parametter");
            throw new Exception("Unknown error");
        }
        public string GetParameterStringUNICODE(String name) {
            int rep = VBVMR_GetParameterStringW.Invoke(name, getParmStringAResponse);
            if (rep == 0) return Marshal.PtrToStringUni((IntPtr)getParmStringAResponse);
            if (rep == -1) throw new ConnectionException("Can not get client");
            if (rep == -2) throw new ConnectionException("Unexpected login (logout was excepted before");
            if (rep == -3) throw new ArgumentException("Unknown parametter");
            if (rep == -5) throw new StructutreMisMatchException("Unknown parametter");
            throw new Exception("Unknown error");
        }
        public float GetLevel(VoicemeeterLevelType type, VoicemeeterChannels channel) {
            float res;
            int rep = VBVMR_GetLevel.Invoke((int)type, (int)channel, &res);
            if (rep == 0) return res;
            if (rep == -1) throw new ConnectionException("Can not get client");
            if (rep == -2) throw new ConnectionException("Unexpected login (logout was excepted before");
            if (rep == -3) throw new ArgumentException("No level availible");
            if (rep == -4) throw new ArgumentOutOfRangeException("Channel "+channel+" is out of range for type");
            throw new Exception("Unknown error");
        }
        public char[] GetMidiMessage() {
            throw new NotImplementedException("");
        }

        public void SetParameterFloat(String name, float value) {
            int rep = VBVMR_SetParameterFloat.Invoke(name, value);
            if (rep == 0) return;
            if (rep == -1) throw new ArgumentException("Unknown error");
            if (rep == -2) throw new ConnectionException("No server");
            if (rep == -3) throw new ArgumentException("Unknown parametter");
            throw new Exception("Unknown error");
        }
        public void SetParameterStringASCII(String name, String value) {
            int rep = VBVMR_SetParameterStringA.Invoke(name, Marshal.StringToHGlobalAnsi(value));
            if (rep == 0) return;
            if (rep == -1) throw new ArgumentException("Unknown error");
            if (rep == -2) throw new ConnectionException("No server");
            if (rep == -3) throw new ArgumentException("Unknown parametter");
            throw new Exception("Unknown error");
        }
        public void SetParameterStringUNICODE(String name, String value) {
            int rep = VBVMR_SetParameterStringW.Invoke(name, Marshal.StringToHGlobalUni(value));
            if (rep == 0) return;
            if (rep == -1) throw new ArgumentException("Unknown error");
            if (rep == -2) throw new ConnectionException("No server");
            if (rep == -3) throw new ArgumentException("Unknown parametter");
            throw new Exception("Unknown error");
        }
        public void SetParameterScriptASCII(String script) {
            int rep = VBVMR_SetParameters.Invoke(Marshal.StringToHGlobalAnsi(script));
            if (rep > 0) throw new ArgumentException("Error in script, line: "+rep.ToString());
            if (rep == 0) return;
            if (rep == -1) throw new ArgumentException("Unknown error");
            if (rep == -2) throw new ConnectionException("No server");
            if (rep == -3) throw new Exception("Unexpected error");
            if (rep == -4) throw new Exception("Unexpected error");
            throw new Exception("Unknown error");
        }
        public void SetParameterScriptUNICODE(String script) {
            int rep = VBVMR_SetParametersW.Invoke(Marshal.StringToHGlobalUni(script));
            if (rep > 0) throw new ArgumentException("Error in script, line: " + rep.ToString());
            if (rep == 0) return;
            if (rep == -1) throw new ArgumentException("Unknown error");
            if (rep == -2) throw new ConnectionException("No server");
            if (rep == -3) throw new Exception("Unexpected error");
            if (rep == -4) throw new Exception("Unexpected error");
            throw new Exception("Unknown error");
        }


    }
}
