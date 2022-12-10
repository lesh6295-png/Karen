![sprite](https://raw.githubusercontent.com/lesh6295-png/Karen/binarys_to_installer/IMG_20220908_214054-removebg-preview_waifu2x_art_noise0_scale_tta_1.png "p-please...")

# Karen
now i use sprites miku from everlasting summer, because im and design - incompatible things


## Build
To succesful build you need: git, 7z(7z.exe must to add to PATH) and msbuild. Now building can only on Windows-working machine(Windows 10 is preferable).<br/>
### Commands to build:<br/>
```
git clone https://github.com/lesh6295-png/Karen/
cd Karen/
msbuild Karen.sln /property:Configuration=Release /p:DebugType=None 
```
You can change `Release` to `Debug` or `Testing` configuration. `/p:DebugType=None` disable generation .pdb files.
