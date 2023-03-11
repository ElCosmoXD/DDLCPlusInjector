class LauncherMain
{
    private void Awake()
    {
        ///...

        try
        {
            Debug.Log("Loading the injector assembly...");
            Assembly assembly = Assembly.LoadFrom("DDLC+ Injector.dll");
            if (assembly == null)
            {
                Debug.LogError("Can't find assembly!");
            }

            Debug.Log("Searching for the 'DDLCInjector.Injector' class and calling the '_Inject' function!");
            Type type = assembly.GetType("DDLCInjector.Injector", true);
            if (type == null)
            {
                Debug.LogError("Can't find 'DDLCInjector.Injector' type!");
            }

            MethodInfo method = type.GetMethod("_Inject", BindingFlags.Static | BindingFlags.Public);
            if (method == null)
            {
                Debug.LogError("Can't find '_Inject' method!");
            }

            method.Invoke(null, null);
        }
        catch (Exception ex)
        {
            Debug.LogError("Injection failed!, Error: " + ex.Message);
        }
    }

    //...
}