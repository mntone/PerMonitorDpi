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

## License
MIT Licence

## Screen shot
![screen shot No. 1](http://mntone.net/i/pmd.png)

## Donwload (binary)
1.0.0.0 [http://mntone.net/download.php?sid=pmd&vid=v1000](http://mntone.net/download.php?sid=pmd&vid=v1000)
