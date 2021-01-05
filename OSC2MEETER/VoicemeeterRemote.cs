using OSC2MEETER.exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OSC2MEETER {
	unsafe class VoicemeeterRemote {

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

        DVM_Login VM_Login;
        DVM_LogOut VM_Logout;
        DVM_RunVoicemeeter VM_RunVoicemeeter;
        DVM_GetVoicemeeterType VM_GetVoicemeeterType;
        DVM_GetVoicemeeterVersion VM_GetVoicemeeterVersion;

        public VoicemeeterRemote() {
            IntPtr Handle = LoadLibrary(@"C:\Program Files (x86)\VB\Voicemeeter\VoicemeeterRemote.dll");
            if (Handle == IntPtr.Zero) {
                int errorCode = Marshal.GetLastWin32Error();
                throw new Exception(string.Format("Failed to load library (ErrorCode: {0})", errorCode));
            }

            VM_Login = Marshal.GetDelegateForFunctionPointer(GetProcAddress(Handle, "VBVMR_Login"), typeof(DVM_Login)) as DVM_Login;
            VM_Logout = Marshal.GetDelegateForFunctionPointer(GetProcAddress(Handle, "VBVMR_Logout"), typeof(DVM_LogOut)) as DVM_LogOut;
            VM_RunVoicemeeter = Marshal.GetDelegateForFunctionPointer(GetProcAddress(Handle, "VBVMR_RunVoicemeeter"), typeof(DVM_RunVoicemeeter)) as DVM_RunVoicemeeter;
            VM_GetVoicemeeterType = Marshal.GetDelegateForFunctionPointer(GetProcAddress(Handle, "VBVMR_GetVoicemeeterType"), typeof(DVM_GetVoicemeeterType)) as DVM_GetVoicemeeterType;
            VM_GetVoicemeeterVersion = Marshal.GetDelegateForFunctionPointer(GetProcAddress(Handle, "VBVMR_GetVoicemeeterVersion"), typeof(DVM_GetVoicemeeterVersion)) as DVM_GetVoicemeeterVersion;
        }

        public void Login() {
            int res = VM_Login.Invoke();
            if (res == -1) throw new ConnectionException("Can not get client");
            if (res == -2) throw new ConnectionException("Unexpected login (logout was excepted before");
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
        public String GetVMType() {
            long vmType;
            int rep = VM_GetVoicemeeterType.Invoke(&vmType);
            if (rep == 0) {
                switch (vmType) {
                    case 1:
                        return "Voicemeeter";
                    case 2:
                        return "Voicemeeter Banana";
                    case 3:
                        return "Voicemeeter Poatato";
                    default:
                        return "Unknown type (" + vmType.ToString() + ")";
                }
            }
            else if (rep == -1) throw new ConnectionException("Could not get the voicemeeter client");
            else if (rep == -2) throw new ConnectionException("Could not get the server");
            else return "Unknown type";
        }
    }
}
