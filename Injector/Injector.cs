using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace DDLCInjector
{
    class InjectorConsole : MonoBehaviour
    {
        string consoleOut = "Welcome!\n";
        string userInput = "";
        Vector2 scrollPos = Vector2.zero;
        private static List<string> modDllFiles = new List<string>();

        bool LoadMods()
        {
            modDllFiles.AddRange(Directory.GetFiles("mods/", "*.dll", SearchOption.AllDirectories));

            foreach (string dll in modDllFiles)
            {
                Print($"Found mod: {dll}");

                try
                {
                    Assembly assembly = Assembly.LoadFrom(dll);
                    if (assembly == null)
                    {
                        PrintError("Can't find the assembly!");
                        continue;
                    }

                    Type type = assembly.GetType("DDLCPlus.Mod", true);
                    if (type == null)
                    {
                        PrintError("Can't find 'DDLCPlus.Mod' type!");
                        continue;
                    }

                    MethodInfo method = type.GetMethod("Start", BindingFlags.Static | BindingFlags.Public);
                    if (method == null)
                    {
                        PrintError("Can't find method!");
                        continue;
                    }

                    method.Invoke(null, null);
                }
                catch (Exception ex)
                {
                    PrintError("Injection failed!, Error: " + ex.Message);
                    return false;
                }
            }

            return true;
        }

        private void Start()
        {
            Print("Adding listener to scene changes...");
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += (current, next) =>
            {
                Print($"Changed scene from: {current.name}. to: {next.name}");
            };

            Print("Loading Mods...");
            if (!LoadMods())
            {
                PrintError("Looks like there was an error while loading a mod!");
                Print("Please, check that your mods are alright or delete them.");
            }
        }

        private void OnGUI()
        {
            scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.Height(Screen.height - 50));
            {
                GUILayout.TextArea(consoleOut, GUILayout.ExpandHeight(true));
            }
            GUILayout.EndScrollView();

            userInput = GUILayout.TextField(userInput, GUILayout.MinWidth(800));
        }

        void Print(string text)
        {
            consoleOut += $"{text}\n";
            Debug.Log(text);
        }

        void PrintError(string text)
        {
            consoleOut += $"{text}\n";
            Debug.LogError(text);
        }
    }

    public class Injector
    {
        public static bool _Inject()
        {
            Debug.Log("Hello, World from the injector!");

            GameObject injectorConsole = new GameObject();
            injectorConsole.AddComponent<InjectorConsole>();

            if(injectorConsole == null)
            {
                Debug.LogError("Failed to load the console!");
                return false;
            }

            return true;
        }
    }
}
