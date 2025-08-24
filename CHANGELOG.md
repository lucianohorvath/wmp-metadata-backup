# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added


## April 18, 2010 – Version 1.2

* Fixed: "directory could not be located" error when a root folder (such as C:\) is selected.
* Fixed: "invalid character" errors during backup. 
* Fixed: System.InvalidOperationException error during backup.
* Metadata Backup now skips subfolders that can't be accessed (like System Volume Information), instead of failing the whole backup operation.
* The Restore and Cancel buttons now require confirmation first.
* Created an installer for Metadata Backup.
* Various small changes.

## May 28, 2007 - Version 1.1

UTF-8 XML data does not like null characters (bytes containing the value of zero). Some ID3 tag editing programs insert a null character to divide lists such as composers or genres. Version 1.1 of Metadata Backup fixes an issue that occurred when encountering those null characters by replacing the null character with the string "<NULL>" when backing up and replacing the string "<NULL>" with a null character when restoring.

Also, there was added some detailed logging, turned off by default, that can be used to troubleshoot further data compatibility issues.

## March 18, 2007 - Initial release
