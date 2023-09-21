# EAMC Log Lookup Tool Changelog

## 3.0.0.4

- Fixed bug in Badge Return due to stored procedure returning invalid value.  Fixed bug in Badge Return error message due to reused code.

## 3.0.0.3

- Fixed bug that improperly showed badge issuances that were closed due to improper filtering of the view.

## 3.0.0.2

- Fixed Title for Badge Return form. Lock files now contain hostname of invoking machine.

## 3.0.0.1

- The data grid view control on the Badge Return screen now defaults to showing all issued badges.

## 3.0.0.0

- Added iAccess Badge Issuance module

## 2.3.2.3

- Fixed a bug where copying data to clipboard on the Login Data form could cause an unhandled exception.

## 2.3.2.2

- Fixed a bug where a hostname with 5 or 6 consecutive digits was detected as an ECN.

## 2.3.2.1

- Fixed a bug where the program would not lookup Hardware Stat Info for machines not in AD.

## 2.3.2.0

- Added msDS-PhoeneticCompanyName to user AD Info display
- Added mapping of above field to department

## 2.3.1.0

- Added button to relaunch process with elevated credentials
- Added parameter to Application Title to display version and process owner
 
## 2.3.0.0

- Moves lokfile location to a specialized directory.  This allows primary application directory to have locked down permissions
 
## 2.2.3.0

- EUD Search now supports searching by Serial Number.
 
## 2.2.2.0

- EUD Context menu adds option for DENTAC Post-Image tasks
 
## 2.2.1.0

- Bug fix to allow lookup for machines with valid logs, but that are no longer in AD
 
## 2.2.0.0

- At launch, application tests for membership in a Tiered Admins group
- If the process owner is in a Tiered Admin group, adds options to Enabled/Disable or Delete Object to EUD Context Menu
    - Note that these operations can take 10-20 seconds before they hit the DC. The app will not auto-refresh after these actions occur, and no confirmation will be displayed.
  
## 2.1.1.0

- Adds support for Server lookups
 
## 2.1.0.0

- Due to STIG requirements, first run of this and later versions must be with elevated privileges to create the Application log for this application.
- This version creates a number of log entries for program launch, program exit, and various database actions
 
## 2.0.1.0

- This version converts any modal windows to non-modal windows
- Adds hooks to WM_ENDSESSION and WM_QUERYENDSESSION to properly remove lock file during unattended system restart
 
## 2.0.0.0

- Adds AD account membership check, and will not launch if user is not a member of one of a few designated groups
- This change qualifies the application to be labeled as SSO PKI enabled
 
## 1.2.2.2

- Fixes a bug in base32 encoding returning an unexpected number of digits
- Rearranges error catching code in EUDs in Custody screen to be more tolerant of potentially bad database
 
## 1.2.2.1

- Fixes UX lock associated with large number of AD lookups on Custody list.  Now stores AD data on record create
 
## 1.2.1.0

- Allows pickup customer to be different from drop off customer in Custody module
 
## 1.2.0.1

- Fixes bug in Custody module that occured when DoDID was entered directly without a CAC Scan
 
## 1.2.0.0

- Adds Custody module for CSC
- Adds the following UX interactions to User Lookup screen:
    - Double clicking on a user will automatically open the Login Activity screen for that User
    - Right clicking on a user will give a context menu with the options to view Login Activity, or do a EUD Custody search for that user
  
## 1.1.0.0 

- If searching EUD by ECN and no log data is found, will attempt to permit display of DMLSS data
 
## 1.0.10.2

- Bug fix. Prevents error when searching by ECN for a machine that exists in DMLSS but has no logs.
 
## 1.0.10.1

- Adds sound interaction during export to reassure user that application is not frozen
 
## 1.0.10.0

- Application tests for valid connection to SQL server at startup
 
## 1.0.9.0

- EUD Hardware Information now displays Last Boot Timestamp, if available
 
## 1.0.8.0

- EUD Log window now has Context Menu.  Available options:
    - Ping
    - Jump To - Launch Bomgar and initiate jump to selected machine.
  
## 1.0.7.0
 
- Fixes bug that truncates inputs in Inventory Export to 1024 characters.  Text box has been locked to 4096 characters, and allowed input size expanded to 4096 characters.
- Adds message boxes before and after exports
