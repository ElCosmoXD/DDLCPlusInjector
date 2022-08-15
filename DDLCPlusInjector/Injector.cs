using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

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
                try
                {
                    Debug.Log("Loading the injector assembly...");
                    Assembly assembly = Assembly.LoadFrom(dll);
                    if (assembly == null)
                    {
                        Debug.LogError("Can't find the assembly!");
                        continue;
                    }

                    Debug.Log("Searching for the 'DDLCPlus.Mod' class and calling the 'Setup' function!");
                    Type type = assembly.GetType("DDLCPlus.Mod", true);
                    if (type == null)
                    {
                        Debug.LogError("Can't find 'DDLCPlus.Mod' type!");
                        continue;
                    }

                    MethodInfo method = type.GetMethod("Setup", BindingFlags.Static | BindingFlags.Public);
                    if (method == null)
                    {
                        Debug.LogError("Can't find method!");
                        continue;
                    }

                    method.Invoke(null, null);
                }
                catch (Exception ex)
                {
                    Debug.LogError("Injection failed!, Error: " + ex.Message);
                }
            }
        }
    }
}
