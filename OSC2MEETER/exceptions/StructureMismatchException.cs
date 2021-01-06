using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSC2MEETER.exceptions {
	class StructutreMisMatchException : Exception {
        public StructutreMisMatchException() : base() { }
        public StructutreMisMatchException(string message) : base(message) { }
        public StructutreMisMatchException(string message, Exception inner) : base(message, inner) { }
    }
}
