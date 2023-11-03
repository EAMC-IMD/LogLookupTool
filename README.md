# LogLookupTool

This is an interface for gathering and displaying data created by [Logon Script](https://github.com/EAMC-IMD/Logon_Script).  

## Data Structures

It relies on the data structures outlined and created by that project.  For completeness, an SQL script to create the primary database is included at [EUDLogging.sql](./EUDLogging.sql).

Additionally, various database objects assume the presence of a database named pcInventory with a table named DMLSS.  This table is required to map ECNs to serial numbers and hostnames.  

This table needs to be populated by hand using an export from DMLSS.  The RunDate field does not come from DMLSS, but is the datetime of the import operation.

## More Info

Finally, there are a number of variables in app.config specific to your site.  The best way to set these variables, and to initialize the state of your database, is to use the [LogLookupToolSetp](https://github.com/EAMC-IMD/LogLookupToolSetup).  This will handle 90% of the pre-configuration items required.

See (slightly outdated) [User Guide](.\LogLookupTool_Manual_1.0.8.0.pdf) and [pre-Github changelog](.\changelog.md) for more information.
