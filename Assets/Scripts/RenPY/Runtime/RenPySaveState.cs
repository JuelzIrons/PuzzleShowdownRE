using System;
using System.Collections.Generic;
using RenPy.Python;

namespace RenPy.Runtime
{
    /// <summary>
    /// A snapshot of a running game: where execution is, what the store holds, and
    /// what is on screen.
    ///
    /// Only encodable values are captured. Functions, Characters and displayables
    /// are rebuilt by re-running init on load, so saving them would be both
    /// impossible and pointless — restoring overwrites just the game-state names and
    /// leaves the init-defined ones alone.
    /// </summary>
    public sealed class RenPySaveState
    {
        /// <summary>Identity of the statement to resume at.</summary>
        public string NodeId;

        /// <summary>Identities of the pending `call` return points, outermost first.</summary>
        public readonly List<string> CallStack = new List<string>();

        /// <summary>Encodable store variables.</summary>
        public readonly PyDict Store = new PyDict();

        /// <summary>Images to re-show, in draw order.</summary>
        public readonly List<SavedImage> Scene = new List<SavedImage>();

        /// <summary>Label the save landed in, for display in a slot.</summary>
        public string LabelHint;

        /// <summary>Local time the save was written, for display.</summary>
        public string SavedAt;

        /// <summary>Last line of dialogue, for display in a slot.</summary>
        public string Caption;

        public sealed class SavedImage
        {
            public string Layer;
            public string Tag;
            public string Name;
            public int ZOrder;
        }
    }
}
