# DDLCPlusInjector
Mod/DLL support for Doki Doki Literature Club Plus.

# Installing
1. Download a patched ```Assembly-CSharp.dll``` from the [Releases](https://github.com/ElCosmoXD/DDLCPlusInjector/releases) page
2. Put the patched ```Assembly-CSharp.dll``` in the folder ```Doki Doki Literature Club Plus_Data/Managed/``` of the game
3. Download the ```DDLCPlusInjector.dll``` file from [Releases](https://github.com/ElCosmoXD/DDLCPlusInjector/releases) or compile it by yourself [cloning this repository]() and compiling the DDLCPlusInjector solution
4. Put the downloaded file in the root of the folder of the game
5. In the root of the game folder create the ```mods/``` folder
6. Install mods and put them into the ```mods/``` folder

Now the DDLC Plus folder should look like this :)

![Installed](https://user-images.githubusercontent.com/37759352/184675275-f85e90f2-1bd6-4898-a4db-5fd0d322ef79.png)

# For Developers
This is just a little guide of how to develop mods for DDLC Plus in a easy way.

1. Create a C# solution (Specifically a .NET Standard solution) using Visual Studio (You can use any IDE but I haven't tested with others)
2. In the project dependencies add the *.DLL's that are in the DDLC Plus folder (```Doki Doki Literature Club Plus_Data/Managed```)
3. Also, In the project dependencies add the DDLCPlusModAPI.dll, You can download this file in the [Releases](https://github.com/ElCosmoXD/DDLCPlusInjector/releases) page or you can compile it by yourself [cloning this repository]() and compiling the proect in the folder [DDLCPlusModAPI]().
4. Add your code
5. Compile the project
6. After compiling, Put the DLL file in a folder called ```mods/``` in the DDLC Plus base folder
7. Test (Obviously for testing you have to install the Injector first)

# Patching the game
This step is necessary when you want to patch the ```Assembly-CSharp.dll``` file by yourself. But you have to know that you will have to make many modifications to the file for making the mods work.

1. Install [dnSpy](https://github.com/dnSpy/dnSpy/releases)
2. With [dnSpy](https://github.com/dnSpy/dnSpy/releases) open the ```Assembly-CSharp.dll``` file (That is in ```Doki Doki Literature Club Plus_Data/Managed/Assembly-CSharp.dll```)
3. Patch the clases.... (As a note, I haven't patched all the game yet, So I'll write a markdown with the classes and functions to patch.)
4. Go to ```File->Save All...``` and ```File->Save Module...```
5. There it is, Now the game is patched

# Credits

[Team Salvato](https://teamsalvato.com/): The creators and the owners of "[Doki Doki Literature Club Plus](https://ddlc.plus/)" and "Doki Doki Literature Club".
