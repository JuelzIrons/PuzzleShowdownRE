using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;
using RenPy.Python;
using RenPy.Runtime;

namespace RenPy.Unity
{
    /// <summary>
    /// Saves and restores Ren'Py's `persistent` store through PlayerPrefs.
    ///
    /// Persistent data is what survives between sessions — unlocked endings, "seen"
    /// flags, preferences. Flip Side's ending tracker is built on it, so without this
    /// the gallery resets every launch.
    ///
    /// Values are encoded with a one-character type tag so ints, floats, strings,
    /// bools, lists and sets all come back as the type the script stored.
    /// </summary>
    public static class RenPyPersistence
    {
        const string IndexKey = "renpy.persistent.__keys__";
        const string Prefix = "renpy.persistent.";

        /// <summary>Loads saved values into the persistent store.</summary>
        public static void Load(PyDict persistent, string gameId)
        {
            string index = PlayerPrefs.GetString(Key(gameId, IndexKey), "");
            if (index.Length == 0) return;

            foreach (var name in index.Split(''))
            {
                if (name.Length == 0) continue;

                string encoded = PlayerPrefs.GetString(Key(gameId, Prefix + name), null);
                if (encoded == null) continue;

                object value;
                if (ValueCodec.Decode(encoded, out value)) persistent.Set(name, value);
            }
        }

        /// <summary>Writes the persistent store out.</summary>
        public static void Save(PyDict persistent, string gameId)
        {
            var names = new List<string>();

            foreach (var kv in persistent)
            {
                string name = PyOps.ToStr(kv.Key);
                if (name.StartsWith("_", StringComparison.Ordinal)) continue;

                string encoded;
                if (!ValueCodec.Encode(kv.Value, out encoded)) continue;

                PlayerPrefs.SetString(Key(gameId, Prefix + name), encoded);
                names.Add(name);
            }

            PlayerPrefs.SetString(Key(gameId, IndexKey), string.Join("", names.ToArray()));
            PlayerPrefs.Save();
        }

        public static void Clear(string gameId)
        {
            string index = PlayerPrefs.GetString(Key(gameId, IndexKey), "");
            foreach (var name in index.Split(''))
                if (name.Length > 0) PlayerPrefs.DeleteKey(Key(gameId, Prefix + name));

            PlayerPrefs.DeleteKey(Key(gameId, IndexKey));
            PlayerPrefs.Save();
        }

        static string Key(string gameId, string name)
        {
            return string.IsNullOrEmpty(gameId) ? name : gameId + "/" + name;
        }

    }
}
