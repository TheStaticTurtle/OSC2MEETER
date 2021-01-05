using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSC2MEETER.exceptions {
	class NotInstalledException : Exception {
        public NotInstalledException() : base() { }
        public NotInstalledException(string message) : base(message) { }
        public NotInstalledException(string message, Exception inner) : base(message, inner) { }
    }
}
