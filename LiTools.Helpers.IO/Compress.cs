// <summary>
// Starter no Gui Worker class.
// </summary>
// <copyright file="Compress.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.IO
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// File compress helper class.
    /// </summary>
    public static class Compress
    {
        /// <summary>
        /// Compress byte.
        /// </summary>
        /// <param name="input">byte to compress.</param>
        /// <returns>true if compress was working | compress byte as return.</returns>
        public static Tuple<bool, byte[]> GzipCompress(byte[] input)
        {
            if (input == null)
            {
                return new Tuple<bool, byte[]>(false, new byte[0]);
            }

            if (input.Length < 1)
            {
                return new Tuple<bool, byte[]>(false, new byte[0]);
            }

            using System.IO.MemoryStream memory = new();
            using (System.IO.Compression.GZipStream gzip = new(memory, System.IO.Compression.CompressionMode.Compress, true))
            {
                gzip.Write(input, 0, input.Length);
            }

            return new Tuple<bool, byte[]>(true, memory.ToArray());
        }

        /// <summary>
        /// Decompress byte.
        /// </summary>
        /// <param name="input">byte to compress.</param>
        /// <returns>true if decompress was working | decompress byte as return.</returns>
        public static Tuple<bool, byte[]> GzipDecompress(byte[] input)
        {
            if (input == null)
            {
                return new Tuple<bool, byte[]>(false, new byte[0]);
            }

            if (input.Length < 1)
            {
                return new Tuple<bool, byte[]>(false, new byte[0]);
            }

            using System.IO.Compression.GZipStream stream = new(new System.IO.MemoryStream(input), System.IO.Compression.CompressionMode.Decompress);
            const int size = 4096;
            byte[] buffer = new byte[size];
            using System.IO.MemoryStream memory = new();
            int count = 0;
            do
            {
                count = stream.Read(buffer, 0, size);
                if (count > 0)
                {
                    memory.Write(buffer, 0, count);
                }
            }
            while (count > 0);
            return new Tuple<bool, byte[]>(true, memory.ToArray());
        }
    }
}
