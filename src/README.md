Unbound Droplist - Src
======================

Copy the App_Config
-------------------
 Move the ./src/App_Config/Include/Fishtank.ControlSources.config to your Sitecore project's  "/App_Config/Include" folder.

Install the package
------------------- 
The package ./src/Package/2013-06-03-UnboundDroplist-Core.zip installs the following items:

/core/sitecore/system/Field types/Custom Types (folder)
/core/sitecore/system/Field types/Custom Types/Unbound Droplist (field)

DLL
---
 
Make sure your Sitecore project references this project and is set to "Copy To Local."  

Additionally, this project references Sitecore.Kernel.dll and Sitecore.Client.dll in the "./lib" folder.  These libraries cannot be included for licensing reasons.  Please attach references to these files locally.

email: dan@getfishtank.ca
url: http://www.getfishtank.ca