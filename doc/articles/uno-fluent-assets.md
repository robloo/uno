# Uno Fluent UI assets
> **IMPORTANT** \
This is a breaking change on most platform the font needs to be changed or most control will display incorrectly.

Uno has a new multiplatform font. The new font must be added manually to each platform to access the new symbols.

## Font files
The font is in this repository https://github.com/unoplatform/uno.fonts. The necessary files can be downloaded here: https://github.com/unoplatform/uno.fonts/tree/master/webfonts

## Changes

### iOS & macOS 
---
The `info.plist` file should be updated for both platforms, replacing the string `Fonts/winjs-symbols.ttf` with `Fonts/uno-fluentui-assets.ttf` in the file.

On iOS and macOS, Uno looks for a font named 'Symbols' (A font's name is not necessarily the name of the file). For this font to be available, the font file needs to be placed in the `Resources/Fonts` folder. The old `winjs-symbols.ttf` file can safely be deleted.  \  \
\
![image](Assets/font-ios.png) \
\
![image](Assets/font-macos.png)  
\
Open the `.csproj` file (`YourApp.iOS.csproj` or `YourApp.macOS.csproj`). Replace the string `winjs-symbols.ttf` with `uno-fluentui-assets.ttf`.

### Android 
---
Once Uno has been updated, it will start looking for a font file named uno-fluentui-assets.ttf in the assets folder: \
\
![image](Assets/font-droid.png)
\
Open the `.csproj` (`YourApp.Droid.csproj`). Replace the string `Fonts/winjs-symbols.ttf` with `Fonts/uno-fluentui-assets.ttf`.
### WebAssembly 
---
WASM won't break after the update, but to access the new symbols the file Font.css should be changed. The font is passed as a base64 string: \ \
\
![image](Assets/font-wasm.png) 
\
Simply replace the contents of Font.css in your app with those of the `Font.css` linked to above.

## Known issues
On iOS and macOS the indeterminate state for a CheckBox is not the right color.


## Related Topics
- [3011](https://github.com/unoplatform/uno/issues/3011)
- [967](https://github.com/unoplatform/uno/issues/967)
