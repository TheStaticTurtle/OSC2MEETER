﻿using System;
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

        delegate void DVM_Login();
        delegate void DVM_LogOut();
        delegate int DVM_GetVoicemeeterType(long* vmType);
        delegate int DVM_GetVoicemeeterVersion(long* vmVersion);

        DVM_Login VM_Login;
        DVM_LogOut VM_Logout;
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
            VM_GetVoicemeeterType = Marshal.GetDelegateForFunctionPointer(GetProcAddress(Handle, "VBVMR_GetVoicemeeterType"), typeof(DVM_GetVoicemeeterType)) as DVM_GetVoicemeeterType;
            VM_GetVoicemeeterVersion = Marshal.GetDelegateForFunctionPointer(GetProcAddress(Handle, "VBVMR_GetVoicemeeterVersion"), typeof(DVM_GetVoicemeeterVersion)) as DVM_GetVoicemeeterVersion;
        }

        public void Login() {
            VM_Login.Invoke();
        }

        public void Logout() {
            VM_Logout.Invoke();
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
            else return "Unknown type";
        }
    }
}