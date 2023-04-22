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
        List<string> modDllFiles = new List<string>();
        Dictionary<string, Assembly> modAssemblies = new Dictionary<string, Assembly>();

        bool draw = true;

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

                    var nameField = type.GetField("name", BindingFlags.Static | BindingFlags.Public);
                    if (nameField == null)
                    {
                        PrintError("Can't find the 'name' property! Skipping mod...");
                        continue;
                    }

                    string modName = nameField.GetValue(null) as string;
                    if(modName == null)
                    {
                        PrintError("Couldn't find the name of the mod! Skipping...");
                        continue;
                    }

                    MethodInfo method = type.GetMethod("Start", BindingFlags.Static | BindingFlags.Public);
                    if (method == null)
                    {
                        PrintError("Can't find the 'Start' method!");
                        continue;
                    }

                    method.Invoke(null, null);

                    modAssemblies.Add(modName, assembly);
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

                return;
            }

            draw = false;
        }

        private void OnGUI()
        {
            if (!draw) return;

            scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.Height(Screen.height - 50));
            {
                GUILayout.TextArea(consoleOut, GUILayout.ExpandHeight(true));
            }
            GUILayout.EndScrollView();

            userInput = GUILayout.TextField(userInput, GUILayout.MinWidth(800));

            if(GUILayout.Button("Execute"))
            {
                string[] input = userInput.Split(' ');
                Parse(input[0], input);
            }
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.C))
                draw = !draw;
        }

        void Parse(string command, string[] args)
        {
            switch(command.ToLower())
            {
                case "print":
                    {
                        switch(args[1].ToLower())
                        {
                            case "scene":
                                Print(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
                                break;

                            default:
                                Print($"Unknown function/variable: {args[1]}!");
                                break;
                        }
                    }

                    break;

                case "execute":
                    {
                        Execute(args[1], args[2]);
                    }

                    break;

                default:
                    Print($"Unknown command: {command}!");
                    break;
            }
        }

        void Execute(string source, string command)
        {
            if(modAssemblies.TryGetValue(source, out var assembly))
            {
                Type type = assembly.GetType("DDLCPlus.Mod", true);
                MethodInfo method = type.GetMethod(command, BindingFlags.Static | BindingFlags.Public);
                method.Invoke(null, null);

                return;
            }

            PrintError($"Can't find mod/assembly: {source}");
        }
    }

    public class Injector
    {
        public static bool _Inject()
        {
            Debug.Log("Starting injector...");

            GameObject injectorConsole = new GameObject();
            injectorConsole.AddComponent<InjectorConsole>();

            if(injectorConsole == null)
            {
                Debug.LogError("Failed to load the console!");
                return false;
            }

            GameObject.DontDestroyOnLoad(injectorConsole);

            return true;
        }
    }
}
