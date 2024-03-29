# DDLC Plus Injector
Mod/DLL support for Doki Doki Literature Club Plus.

#### I'm working again on this project, obviously I have other things to do, so if you want to add something [any PR is welcome](https://github.com/ElCosmoXD/DDLCPlusInjector/pulls).
#### NOTE: This might be pretty obvious but for developing/using mods you need to have a copy of the game, You can buy it on https://ddlc.plus.

# Work in progress
This is just a work-in-progress, so don't expecto something too stable or a massive amount of mods.

# Installing
1. Download a patched ```DDLC.dll``` from the [Releases](https://github.com/ElCosmoXD/DDLCPlusInjector/releases) page
2. Put the patched ```DDLC.dll``` in the folder ```Doki Doki Literature Club Plus_Data/Managed/``` of the game
3. Download the ```DDLC+ Injector.dll``` file from [Releases](https://github.com/ElCosmoXD/DDLCPlusInjector/releases) or compile it by yourself [cloning this repository](https://github.com/ElCosmoXD/DDLCPlusInjector/archive/refs/heads/main.zip) and compiling the Injector solution
4. Put the downloaded file in the root of the folder of the game
5. In the root of the game folder create the ```mods/``` folder
6. Install mods and put them into the ```mods/``` folder

#### Please, before doing anything do a backup of the original ```DDLC.dll``` file, Because if something goes wrong you will have to reinstall the game.

Now the DDLC Plus folder should look like this :)

![Installed](Assets/Installed.png)

# For Developers
This is just a little guide of how to develop mods for DDLC Plus in a easy way.

1. Create a C# solution (Specifically a DLL .NET Standard solution) using Visual Studio (You can use any IDE but I haven't tested with others)
2. In the project dependencies add the *.DLL's that are in the DDLC Plus folder (```Doki Doki Literature Club Plus_Data/Managed```)
3. Add your code
4. Compile the project
5. After compiling, Put the DLL file in a folder called ```mods/``` in the DDLC Plus base folder
6. Test (Obviously for testing you have to install the Injector first)

In order to interact with the game, you should write the mod like a Unity game, with the MonoBehavieour GameObjects.

#### A little example of how write a simple mod:
``` C#
using UnityEngine;

namespace DDLCPlus
{
    public class Mod
    {
        public static void Start()
        {
            Debug.Log("Hello, World!");
            Debug.Log("This message comes from an loaded mod!");
        }
    }
}
```
Now, if you run the game and you see the Unity's 'Player.log' file, You will see this:

![HelloWorld](https://user-images.githubusercontent.com/37759352/184688088-724ac446-4436-49ff-b6bf-a2e215f76a59.png)

# Patching the game
This step is necessary when you want to patch the ```Assembly-CSharp.dll``` file by yourself. But you have to know that you will have to make many modifications to the file for making the mods work.

1. Install [dnSpy](https://github.com/dnSpy/dnSpy/releases)
2. With [dnSpy](https://github.com/dnSpy/dnSpy/releases) open the ```Assembly-CSharp.dll``` file (That is in ```Doki Doki Literature Club Plus_Data/Managed/Assembly-CSharp.dll```)
3. Patch the 'Awake' method in the 'LauncherMain' class using the code in the ```Patch``` folder
4. Compile the patched file

# Credits

[Team Salvato](https://teamsalvato.com/): The creators and the owners of "[Doki Doki Literature Club Plus](https://ddlc.plus/)" and "[Doki Doki Literature Club](http://ddlc.moe)".
