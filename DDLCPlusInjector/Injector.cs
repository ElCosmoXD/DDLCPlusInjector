using System;
using System.Collections.Generic;
using System.IO;

namespace DDLCPlusInjector
{
    public class Injector
    {
        private static List<string> dll_files = new List<string>();

        public static void Setup()
        {
            dll_files.AddRange(Directory.GetFiles("mods/", "*.dll", SearchOption.AllDirectories));

            foreach(string dll in dll_files)
            {
                UnityEngine.Debug.Log("Importing dll " + dll);
            }
        }
    }
}
