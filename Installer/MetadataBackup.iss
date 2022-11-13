; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#include "scripts\products.iss"
#include "scripts\winversion.iss"
#include "scripts\dotnetfx20.iss"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{CD15CC1B-8563-4209-A52B-DEB7DE2162B8}
AppName=Metadata Backup
AppVerName=Metadata Backup 1.2
AppPublisher=Dale Preston & Tim De Baets
AppPublisherURL=http://sourceforge.net/projects/metadatabackup
AppSupportURL=http://sourceforge.net/projects/metadatabackup
AppUpdatesURL=http://sourceforge.net/projects/metadatabackup
DisableDirPage=auto
DefaultDirName={pf}\Metadata Backup
DefaultGroupName=Metadata Backup
DisableProgramGroupPage=true
InfoBeforeFile=..\MetadataBackupReadMe.rtf
OutputBaseFilename=MetadataBackup-1.2
Compression=lzma
SolidCompression=true
OutputDir=.
VersionInfoVersion=1.2
VersionInfoCompany=Dale Preston & Tim De Baets
VersionInfoTextVersion=1.2
VersionInfoCopyright=Copyright � 2007 Dale Preston. Portions copyright � 2010 Tim De Baets. All rights reserved.
VersionInfoProductName=Metadata Backup
VersionInfoProductVersion=1.2
AppCopyright=Copyright � 2007 Dale Preston. Portions copyright � 2010 Tim De Baets. All rights reserved.
AppMutex=PrestonMediaMetadataBackupMutex
ShowLanguageDialog=no
WizardImageFile=compiler:WizModernImage-IS.bmp
WizardSmallImageFile=compiler:WizModernSmallImage-IS.bmp
AppVersion=1.2
UninstallDisplayIcon={app}\MetadataBackup.exe
MinVersion=4.1,5.0

[Languages]
Name: en; MessagesFile: compiler:Default.isl

[Tasks]
Name: desktopicon; Description: {cm:CreateDesktopIcon}; GroupDescription: {cm:AdditionalIcons}; Flags: unchecked; Languages: 
Name: quicklaunchicon; Description: {cm:CreateQuickLaunchIcon}; GroupDescription: {cm:AdditionalIcons}; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: ..\MetaDataBackup\bin\Release\MetadataBackup.exe; DestDir: {app}; Flags: replacesameversion
Source: ..\MetaDataBackup\bin\Release\Interop.WMPLib.dll; DestDir: {app}; Flags: replacesameversion
Source: ..\MetaDataBackup\bin\Release\MetadataBackup.exe.config; DestDir: {app}; Flags: ignoreversion
Source: ..\MetadataBackupHelp.rtf; DestDir: {app}; Flags: ignoreversion
Source: ..\MetadataBackupReadMe.txt; DestDir: {app}; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: {commonprograms}\Metadata Backup; Filename: {app}\MetadataBackup.exe; Comment: Back up or restore all the information in the Windows Media Player library, including ratings, play counts, and custom fields.; IconIndex: 0; Tasks: ; Languages: 
Name: {commondesktop}\Metadata Backup; Filename: {app}\MetadataBackup.exe; Tasks: desktopicon; Comment: Back up or restore all the information in the Windows Media Player library, including ratings, play counts, and custom fields.; IconIndex: 0; Languages: 
Name: {userappdata}\Microsoft\Internet Explorer\Quick Launch\Metadata Backup; Filename: {app}\MetadataBackup.exe; Tasks: quicklaunchicon; Comment: Back up or restore all the information in the Windows Media Player library, including ratings, play counts, and custom fields.; IconIndex: 0; Languages: 

[Run]
Filename: {app}\MetadataBackup.exe; Description: {cm:LaunchNow,Metadata Backup}; Flags: nowait postinstall skipifsilent; Tasks: ; Languages: 

[CustomMessages]
LaunchNow=&Run %1 now
win2000sp3_title=Windows 2000 Service Pack 3
winxpsp2_title=Windows XP Service Pack 2

[_ISTool]
OutputExeFilename=G:\Projecten\Other\MetaDataBackup\Installer\MetadataBackup-1.2.exe
[Code]
function InitializeSetup(): Boolean;
begin
	//init windows version
	initwinversion();

	//check if dotnetfx20 can be installed on this OS
	if not minwinspversion(5, 0, 3) then begin
		MsgBox(FmtMessage(CustomMessage('depinstall_missing'), [CustomMessage('win2000sp3_title')]), mbError, MB_OK);
		exit;
	end;
	if not minwinspversion(5, 1, 2) then begin
		MsgBox(FmtMessage(CustomMessage('depinstall_missing'), [CustomMessage('winxpsp2_title')]), mbError, MB_OK);
		exit;
	end;

	//install .netfx 2.0 if not installed yet
	dotnetfx20();

	Result := true;
end;