# EsapiExplorer

This is an Eclipse script for exploring the ESAPI `ScriptContext` object tree.

To use:

1. Open EsapiExplorer.sln in Visual Studio.
1. In the Visual Studio toolbar, choose the solution configuration
   according to your Eclipse version:
   - Debug-13.6
   - Debug-15.5
   - Debug-16.1.
1. In the "Solution Explorer" window, expand the EsapiExplorer project,
   then the References section.
1. If you see a warning icon on the **VMS.TPS.Common.Model.API**
   and **VMS.TPS.Common.Model.Types** references, you will need to update
   the reference paths to the correct one. To do that,
   first remove the current references (right-click, then select *Remove*).
   Then right-click on the References section and choose *Add References...*.
   Click on *Browse* on the left panel and then click on the *Browse...*
   button at the bottom of the dialog. Choose the location of the
   DLL files.
1. Build the solution.
1. Move the output files located at bin\x64\Debug-13.6 inside the
   EsapiExplorer project directory to a location that can be accessed
   from Eclipse.
1. In Eclipse, open a patient and then run the plugin script.
