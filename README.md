# k-aGv2

Compiling:Please read our [Manifest](https://github.com/k-aGv/manifest)



Adding Awesomium:

1-Add DLLs included in Awesomium folder , as references
*Awesomium.Core.dll
*Awesomium.Windows.Forms.dll

2-Select build/clean solution to make sure that you will build clearly

Optional:To remove the warning but restrict the build platform:

2-Build/Configuration manager/

    Find your project in the list, under Platform it will say "Any CPU"
    Select the "Any CPU" option from the drop down and then select <New..>
    From that dialog, select x86 from the "New Platform" drop down and make sure "Any CPU" is selected in the "Copy settings from" drop down.
    Hit OK/Close
    You will want to select x86 for both the Debug and Release configurations.

This will cause to create an x32 product!!
