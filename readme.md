[Obsoulte]

I create new version support library, [Per-monitor DPI Support Library](https://github.com/mntone/Windows.PerMonitorDpiSupport).

# project Mntone.PerMonitorDpi

## What’s project?
This library supports per monitor dpi for WPF. You can use this library very easy. However, window position value (Top, Left, Height, Width) change virtual value to physical value.

## How to use?
1. Include Mntone.PerMonitorDpi.dll
2. Change base class “Window” to “PmWindow”
3. Add a manifest file.
	- From solution explorer, add a application manifest file (from Visual C# items).
	- Write
<pre>
xmlns:asmv3="urn:schemas-microsoft-com:asm.v3"
</pre>
	and
<pre>
&lt;asmv3:application&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;asmv3:windowsSettings xmlns="http://schemas.microsoft.com/SMI/2005/WindowsSettings"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;dpiAware&gt;True/PM&lt;/dpiAware&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/asmv3:windowsSettings&gt;
&lt;/asmv3:application&gt;
</pre>
4. Your application supports per monitor dpi!

## Description
Two versions is the same code.

### for .NET 4.0 (Client Profile) dll
- Windows XP or later

### for .NET 4.5 dll
- Windows Vista or later

## License
MIT Licence

## Screen shot
![screen shot No. 1](http://mntone.net/i/pmd1010.png)
![screen shot No. 2](http://mntone.net/i/pmd1020.png)

## Donwload (binary)
- 1.0.2.0 [ http://mntone.net/download.php?sid=pmd&vid=v1020]( http://mntone.net/download.php?sid=pmd&vid=v1020)
- 1.0.1.0 [ http://mntone.net/download.php?sid=pmd&vid=v1010]( http://mntone.net/download.php?sid=pmd&vid=v1010)
- 1.0.0.0 [http://mntone.net/download.php?sid=pmd&vid=v1000](http://mntone.net/download.php?sid=pmd&vid=v1000)
