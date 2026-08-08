using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace RenPy.Pickle
{
    /// <summary>
    /// Reads the .rpyc container: a "RENPY RPC2" magic followed by a table of
    /// (slot, offset, length) triples, each pointing at a zlib-compressed pickle.
    /// Slot 1 holds the script AST. Files older than RPYC2 are a bare zlib stream.
    /// </summary>
    public static class RpycFile
    {
        static readonly byte[] Rpyc2Header = Encoding.ASCII.GetBytes("RENPY RPC2");

        /// <summary>Returns the decompressed pickle bytes for a slot, or null if absent.</summary>
        public static byte[] ReadSlot(byte[] file, int slot)
        {
            if (file == null || file.Length < 2) return null;

            if (!HasHeader(file))
            {
                // Legacy single-slot format.
                return slot == 1 ? Inflate(file, 0, file.Length) : null;
            }

            int pos = Rpyc2Header.Length;
            while (pos + 12 <= file.Length)
            {
                int slotNo = ReadInt32(file, pos);
                int start = ReadInt32(file, pos + 4);
                int length = ReadInt32(file, pos + 8);

                if (slotNo == slot)
                {
                    if (start < 0 || length < 0 || start + length > file.Length)
                        throw new PickleException("rpyc slot " + slot + " is out of bounds");
                    return Inflate(file, start, length);
                }

                if (slotNo == 0) return null;
                pos += 12;
            }

            return null;
        }

        public static bool HasHeader(byte[] file)
        {
            if (file.Length < Rpyc2Header.Length) return false;
            for (int i = 0; i < Rpyc2Header.Length; i++)
                if (file[i] != Rpyc2Header[i]) return false;
            return true;
        }

        static int ReadInt32(byte[] b, int offset)
        {
            return b[offset] | (b[offset + 1] << 8) | (b[offset + 2] << 16) | (b[offset + 3] << 24);
        }

        /// <summary>
        /// Inflates a zlib stream. DeflateStream only understands raw deflate, so the
        /// 2-byte zlib header (and any preset dictionary id) is skipped first.
        /// </summary>
        public static byte[] Inflate(byte[] data, int offset, int count)
        {
            int start = offset;

            byte cmf = data[start];
            byte flg = data[start + 1];
            bool isZlib = (cmf & 0x0f) == 8 && ((cmf << 8) | flg) % 31 == 0;

            if (isZlib)
            {
                start += 2;
                if ((flg & 0x20) != 0) start += 4; // FDICT
            }

            int available = count - (start - offset);

            using (var input = new MemoryStream(data, start, available, false))
            using (var deflate = new DeflateStream(input, CompressionMode.Decompress))
            using (var output = new MemoryStream(Math.Max(4096, count * 4)))
            {
                var buffer = new byte[65536];
                int read;
                while ((read = deflate.Read(buffer, 0, buffer.Length)) > 0)
                    output.Write(buffer, 0, read);
                return output.ToArray();
            }
        }

        /// <summary>Loads and unpickles slot 1 of a .rpyc file.</summary>
        public static object LoadScript(byte[] file, Func<string, string, object> resolver)
        {
            byte[] pickle = ReadSlot(file, 1);
            if (pickle == null) throw new PickleException("rpyc file has no slot 1");
            return PickleReader.Load(pickle, resolver);
        }
    }
}
