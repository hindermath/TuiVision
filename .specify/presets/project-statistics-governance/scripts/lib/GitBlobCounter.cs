// Adapted from HomeBaseline.StatisticsTextFile (MIT); reads immutable Git blobs.
using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;

namespace ProjectTransparency {
    public static class GitBlobCounter {
        public static long?[] Count(string repository, string[] ids) {
            var info = new ProcessStartInfo("git") {
                WorkingDirectory = repository, UseShellExecute = false,
                RedirectStandardInput = true, RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            foreach (var arg in new [] { "--no-replace-objects", "cat-file", "--batch" })
                info.ArgumentList.Add(arg);
            info.Environment["GIT_NO_LAZY_FETCH"] = "1";
            info.Environment["GIT_TERMINAL_PROMPT"] = "0";
            using (var process = Process.Start(info)) {
                var errors = process.StandardError.ReadToEndAsync();
                var stream = process.StandardOutput.BaseStream;
                var result = new List<long?>();
                var buffer = new byte[65536];
                try {
                    foreach (var id in ids) {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(id, "^[a-f0-9]{40}([a-f0-9]{24})?$"))
                            throw new InvalidDataException("Invalid blob ID.");
                        process.StandardInput.WriteLine(id);
                        process.StandardInput.Flush();
                        var header = new StringBuilder();
                        int current;
                        while ((current = stream.ReadByte()) != 10) {
                            if (current < 0 || header.Length > 200) throw new InvalidDataException("Missing blob.");
                            header.Append((char)current);
                        }
                        var parts = header.ToString().Split(' ');
                        long size;
                        if (parts.Length != 3 || parts[0] != id || parts[1] != "blob" ||
                            !long.TryParse(parts[2], out size) || size < 0)
                            throw new InvalidDataException("Invalid blob response.");
                        long left = size, lf = 0, cr = 0;
                        int last = -1;
                        bool binary = false;
                        var first = new List<byte>(3);
                        while (left > 0) {
                            int count = stream.Read(buffer, 0, (int)Math.Min(left, buffer.Length));
                            if (count == 0) throw new InvalidDataException("Truncated blob.");
                            for (int i = 0; i < count; i++) {
                                byte value = buffer[i];
                                if (first.Count < 3) first.Add(value);
                                if (value == 0) binary = true;
                                if (value == 10) lf++;
                                if (value == 13) cr++;
                                last = value;
                            }
                            left -= count;
                        }
                        if (stream.ReadByte() != 10) throw new InvalidDataException("Invalid blob framing.");
                        bool onlyBom = size == 3 && first[0] == 239 && first[1] == 187 && first[2] == 191;
                        result.Add(binary ? (long?)null : size == 0 || onlyBom ? 0 :
                            lf > 0 ? lf + (last == 10 ? 0 : 1) : cr + (last == 13 ? 0 : 1));
                    }
                    process.StandardInput.Close();
                    process.WaitForExit();
                    if (process.ExitCode != 0) throw new InvalidDataException("Git object read failed.");
                    return result.ToArray();
                } finally {
                    if (!process.HasExited) process.Kill();
                    errors.GetAwaiter().GetResult();
                }
            }
        }
    }
}
