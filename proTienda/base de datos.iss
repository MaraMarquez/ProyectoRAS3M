; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{17AD9B0B-0593-4B5D-B5C4-D880267116B8}
AppName=Base de Datos
AppVersion=0.8
;AppVerName=Base de Datos 0.8
AppPublisher=Base de datos Punto - Venta
DefaultDirName={pf}\Punto de Venta - Abarrotes
DisableDirPage=yes
DefaultGroupName=Punto de Venta - Abarrotes
AllowNoIcons=yes
OutputBaseFilename=Base de datos - setup
Compression=lzma
SolidCompression=yes

[Languages]
Name: "spanish"; MessagesFile: "compiler:Languages\Spanish.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Users\eduar\Documents\UANL-FIME-MATERIAS-2013\Enero - Junio 2014\Proyecto Integrador\protienda integrador\proTienda v.9\proTienda\proTienda\dbTienda.mdb"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\Base de Datos"; Filename: "{app}\dbTienda.mdb"
Name: "{group}\{cm:UninstallProgram,Base de Datos}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\Base de Datos"; Filename: "{app}\dbTienda.mdb"; Tasks: desktopicon

[Run]
Filename: "{app}\dbTienda.mdb"; Description: "{cm:LaunchProgram,Base de Datos}"; Flags: shellexec postinstall skipifsilent
