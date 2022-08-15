//...
using System.Reflections;

namespace RenpyLauncher
{
	public class LauncherMain : MonoBehaviour
	{
		//....
		private void Awake()
		{
			//...
			
			try    
			{    
			 	Debug.Log("Loading the injector assembly...");     
				Assembly assembly = Assembly.LoadFrom("DDLCPlusInjector.dll");     
				
				if (assembly == null)     
					Debug.LogError("Can't find assembly!");     
				
				Debug.Log("Searching for the 'DDLCPlusInjector.Injector' class and calling the 'Setup' function!");     
				Type type = assembly.GetType("DDLCPlusInjector.Injector", true);     
				if (type == null)          
					 Debug.LogError("Can't find 'DDLCPlusInjector.Injector' type!");     

				MethodInfo method = type.GetMethod("Setup", BindingFlags.Static | BindingFlags.Public);     
				if (method == null)           
					Debug.LogError("Can't find method!");     
			
				method.Invoke(null, null);    
			}    
			catch (Exception ex)    
			{     
				Debug.LogError("Injection failed!, Error: " + ex.Message);    
			}
		}
		
		//...
		
		public void OnLauncherLoaded()
		{
			//...
			
			try    
			{    
				Assembly assembly = Assembly.LoadFrom("DDLCPlusInjector.dll");     
				
				if (assembly == null)     
					Debug.LogError("Can't find assembly!");     
			
				Type type = assembly.GetType("DDLCPlusInjector.Injector", true);     
				if (type == null)          
					 Debug.LogError("Can't find 'DDLCPlusInjector.Injector' type!");     

				MethodInfo method = type.GetMethod("OnLauncherLoaded", BindingFlags.Static | BindingFlags.Public);     
				if (method == null)           
					Debug.LogError("Can't find method!");     
			
				method.Invoke(null, null);    
			}    
			catch (Exception ex)    
			{     
				Debug.LogError("Injection failed!, Error: " + ex.Message);    
			}		
		}
		//...
	}
}
