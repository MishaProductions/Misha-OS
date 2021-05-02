using MishaOS.Drivers;
using System.Collections.Generic;
using System.IO;

namespace MishaOS
{
    class MishaOSConfig
    {
        public static bool IsMishaOSInstalled { get; private set; }
        public static string BootVolume { get; private set; }
        public static string ConfigFilePath { get; private set; }
        internal static bool IsInstalled()
        {
            return IsMishaOSInstalled;
        }
        public static string ReadConfig(string Key, string defaultValue = "")
        {
            if (!IsMishaOSInstalled)
                return defaultValue;

            //File format example
            //DnsAdress=8.8.8.8
            var lines = File.ReadAllLines(ConfigFilePath);
            foreach (var item in lines)
            {
                if (item.StartsWith(Key + "="))
                {
                    var value = item.Replace(Key + "=", "");
                    return value;
                }
            }
            return defaultValue;
        }
        public static void WriteConfig(string Key, string value)
        {
            if (!IsMishaOSInstalled)
                return;

            //File format example
            //DnsAdress=8.8.8.8
            List<string> lines = ArrayToList(File.ReadAllLines(ConfigFilePath));
            for (int i = 0; i < lines.Count; i++)
            {
                var item = lines[i];
                var newValue = Key + "=" + value;
                if (item.StartsWith(Key + "="))
                {
                    lines[i] = newValue;
                    return;
                }
            }
            lines.Add(Key + "=" + value);
            File.WriteAllLines(ConfigFilePath, lines.ToArray());
            return;
        }
        private static List<string> ArrayToList(string[] s)
        {
            List<string> x = new List<string>();
            foreach (var item in s)
            {
                x.Add(item);
            }
            return x;
        }
        public static void Init()
        {
            if (BootManager.EnableFileSystem)
            {
                foreach (var item in Kernel.FS.GetVolumes())
                {
                    Kernel.PrintDebug("Detected volume: " + item.mFullPath);

                    if (Directory.Exists(item.mFullPath + @"MishaOS"))
                    {
                        if (File.Exists(item.mFullPath + @"MishaOS\system.cfg"))
                        {
                            IsMishaOSInstalled = true;
                            ConfigFilePath = item.mFullPath + @"MishaOS\system.cfg";
                            BootVolume = item.mFullPath;
                            Kernel.PrintDebug("Found boot volume: " + item.mFullPath);
                            break;
                        }
                    }
                }

            }
            else
            {
                IsMishaOSInstalled = false;
                BootVolume = "?";
            }
        }
    }
}
