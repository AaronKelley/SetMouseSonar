# SetMouseSonar
A simple command-line tool which can be used to enable or disable the "mouse sonar" setting in Windows.

The "mouse sonar" setting can also be changed in the Mouse applet in the Control Panel.  On the "Pointer Options" tab, the checkbox "Show location of pointer when I press the CTRL key" is the toggle for this feature.  This program allows for it to be enabled or disabled from the command line.  This allows for it to be controlled from a scheduled task, another program, and so on.  (I created this program because I found that a certain game was disabling this setting on launch, and I wanted to just have it re-enabled automatically.)

Usage is simple.  Simply execute the program with a parameter of 0 if you want to disable mouse sonar, and 1 if you want to enable it.

To disable mouse sonar:
```
SetMouseSonar.exe 0
```

To enable mouse sonar:
```
SetMouseSonar.exe 1
```
