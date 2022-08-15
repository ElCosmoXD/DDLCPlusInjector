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
                Debug.Log($"Found mod: {dll}");

                try
                {
                    Assembly assembly = Assembly.LoadFrom(dll);
                    if (assembly == null)
                    {
                        Debug.LogError("Can't find the assembly!");
                        continue;
                    }

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

        public static void Call(string func, object[]? args) 
        {
            foreach (string dll in dll_files)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFrom(dll);
                    if (assembly == null)
                    {
                        Debug.LogError("Can't find the assembly!");
                        continue;
                    }

                    Type type = assembly.GetType("DDLCPlus.Mod", true);
                    if (type == null)
                    {
                        Debug.LogError("Can't find 'DDLCPlus.Mod' type!");
                        continue;
                    }

                    MethodInfo method = type.GetMethod(func, BindingFlags.Static | BindingFlags.Public);
                    if (method == null)
                    {
                        Debug.LogError("Can't find method!");
                        continue;
                    }

                    method.Invoke(null, args);
                }
                catch (Exception ex)
                {
                    Debug.LogError("Injection failed!, Error: " + ex.Message);
                }
            }
        }

        public static void OnLauncherLoaded()
        {
            Call("OnLauncherLoaded", null);
        }
    }
}
