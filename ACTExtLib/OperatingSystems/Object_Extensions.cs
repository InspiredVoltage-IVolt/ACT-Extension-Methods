using System.Text.RegularExpressions;

namespace ACT.Core.Extensions.OperatingSystems
{
    public static class Object_Extensions
    {
        /// <summary>
        /// Horrible TODO Do Better
        /// </summary>
        /// <param name="x"></param>
        /// <returns>Crappy Linux ID</returns>
        public static string GetLinuxMachineID(this object x)
        {
            string _tmpReturn = "";
            var _info = new LinuxInfo();
            _info.GetValues();

            var _cpuinfo = new LinuxCpuInfo();
            _cpuinfo.GetValues();

            try { _tmpReturn += _info.MemTotal.ToString(""); } catch { }
            try { _tmpReturn += _cpuinfo.CacheSize.NullOrEmpty() ? "" : _cpuinfo.ModelName; } catch { }
            try { _tmpReturn += _cpuinfo.CpuFamily.ToString(""); } catch { }
            try { _tmpReturn += _cpuinfo.MHz.ToString(""); } catch { }
            try { _tmpReturn += _cpuinfo.Model.ToString(""); } catch { }
            try { _tmpReturn += _cpuinfo.ModelName.NullOrEmpty() ? "" : _cpuinfo.ModelName; } catch { }
            try { _tmpReturn += _cpuinfo.Stepping.ToString(""); } catch { }
            try { _tmpReturn += _cpuinfo.VendorId.NullOrEmpty() ? "" : _cpuinfo.VendorId; } catch { }

            return _tmpReturn.ComputeHash(Hashing.eHashType.MD5);
        }

    }


    /// <summary>
    /// FreeCSharp: quick implementation of free command (kind of) using C#
    /// </summary>
    internal class LinuxInfo
    {
        public long MemTotal { get; private set; }
        public long MemFree { get; private set; }
        public long Buffers { get; private set; }
        public long Cached { get; private set; }

        public void GetValues()
        {
            string[] memInfoLines = File.ReadAllLines(@"/proc/meminfo");

            MemInfoMatch[] memInfoMatches =
            {
                new MemInfoMatch(@"^Buffers:\s+(\d+)", value => Buffers = Convert.ToInt64(value)),
                new MemInfoMatch(@"^Cached:\s+(\d+)", value => Cached = Convert.ToInt64(value)),
                new MemInfoMatch(@"^MemFree:\s+(\d+)", value => MemFree = Convert.ToInt64(value)),
                new MemInfoMatch(@"^MemTotal:\s+(\d+)", value => MemTotal = Convert.ToInt64(value))
            };

            foreach (string memInfoLine in memInfoLines)
            {
                foreach (MemInfoMatch memInfoMatch in memInfoMatches)
                {
                    Match match = memInfoMatch.regex.Match(memInfoLine);
                    if (match.Groups[1].Success)
                    {
                        string value = match.Groups[1].Value;
                        memInfoMatch.updateValue(value);
                    }
                }
            }
        }

        public class MemInfoMatch
        {
            public Regex regex;
            public Action<string> updateValue;

            public MemInfoMatch(string pattern, Action<string> update)
            {
                this.regex = new Regex(pattern, RegexOptions.Compiled);
                this.updateValue = update;
            }
        }
    }

    /// <summary>
    /// Reads /proc/cpuinfo to obtain common values
    /// </summary>
    internal class LinuxCpuInfo
    {
        public string VendorId { get; private set; }
        public int CpuFamily { get; private set; }
        public int Model { get; private set; }
        public string ModelName { get; private set; }
        public int Stepping { get; private set; }
        public double MHz { get; private set; }
        public string CacheSize { get; private set; }

        public void GetValues()
        {
            string[] cpuInfoLines = File.ReadAllLines(@"/proc/cpuinfo");

            CpuInfoMatch[] cpuInfoMatches =
            {
                new CpuInfoMatch(@"^vendor_id\s+:\s+(.+)", value => VendorId = Convert.ToString(value)),
                new CpuInfoMatch(@"^cpu family\s+:\s+(.+)", value => CpuFamily = Convert.ToInt32(value)),
                new CpuInfoMatch(@"^model\s+:\s+(.+)", value => Model = Convert.ToInt32(value)),
                new CpuInfoMatch(@"^model name\s+:\s+(.+)", value => ModelName = Convert.ToString(value)),
                new CpuInfoMatch(@"^stepping\s+:\s+(.+)", value => Stepping = Convert.ToInt32(value)),
                new CpuInfoMatch(@"^cpu MHz\s+:\s+(.+)", value => MHz = Convert.ToDouble(value)),
                new CpuInfoMatch(@"^cache size\s+:\s+(.+)", value => CacheSize = Convert.ToString(value))
            };

            foreach (string cpuInfoLine in cpuInfoLines)
            {
                foreach (CpuInfoMatch cpuInfoMatch in cpuInfoMatches)
                {
                    Match match = cpuInfoMatch.regex.Match(cpuInfoLine);
                    if (match.Groups[0].Success)
                    {
                        string value = match.Groups[1].Value;
                        cpuInfoMatch.updateValue(value);
                    }
                }
            }
        }

        public class CpuInfoMatch
        {
            public Regex regex;
            public Action<string> updateValue;

            public CpuInfoMatch(string pattern, Action<string> update)
            {
                this.regex = new Regex(pattern, RegexOptions.Compiled);
                this.updateValue = update;
            }
        }
    }
}

