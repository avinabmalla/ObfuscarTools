# ObfuscarTools
Obfuscar Tools for Visual Studio

Obfuscar for Visual Studio integrates the free and open source Obfuscar protector into Visual Studio for easy automatic obfuscation of release builds and ClickOnce applications.

Obfuscar 2.2.31.0 is included with this extension. Obfuscar is available from https://github.com/obfuscar/obfuscar

Assembly search paths are added automatically. ClickOnce Applications are also obfuscated before publishing.

Debugging will not work on obfuscated executables or libraries. Use Ctrl+F5 to run without debugging.

## How to Use

Download and install the extension from the Visual Studio Marketplace

[https://marketplace.visualstudio.com/items?itemName=AvinabMalla.obfuscarvs](https://marketplace.visualstudio.com/items?itemName=AvinabMalla.obfuscarvs)

Three new items are added to the Tools menu.

![Menu Items](https://github.com/avinabmalla/ObfuscarTools/blob/master/ObfuscarTools/Resources/Obfuscar1.png?raw=true)

Click "Protect Using Obfuscar" to set the current project's current configuration for obfuscation after release builds.

Click "Remove Obfuscar Protection" to stop automatic obfuscation for the current configuration.

"Obfuscar Settings" Will let you tune the obfuscar settings.

![Obfuscar Settings](https://github.com/avinabmalla/ObfuscarTools/blob/master/ObfuscarTools/Resources/Obfuscar2.png?raw=true)
