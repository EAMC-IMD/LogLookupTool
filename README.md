# LogLookupTool

This is an interface for gathering and displaying data created by [Logon Script](https://github.com/EAMC-IMD/Logon_Script).  

## Data Structures

It relies on the data structures outlined and created by that project.  For completeness, an SQL script to create the primary database is included at [EUDLogging.sql](./EUDLogging.sql).

Additionally, various database objects assume the presence of a database named pcInventory with a table named DMLSS.  This table is required to map ECNs to serial numbers and hostnames.  The minimum structure of DMLSS is as follows:

```
CREATE TABLE [dbo].[DMLSS](
	[Ecn5] [nvarchar](255) NULL,
	[Ecn] [nvarchar](255) NULL,
	[MfrSerialNo] [nvarchar](255) NULL,
	[AcqDate] [date] NULL,
	[Nomenclature] [nvarchar](255) NULL,
	[LifeExp] [nvarchar](255) NULL,
	[Manufacturer] [nvarchar](255) NULL,
	[CommonModel] [nvarchar](255) NULL,
	[EquipmentLocation] [nvarchar](255) NULL,
	[Ownership] [nvarchar](255) NULL,
	[CustomerName] [nvarchar](255) NULL,
	[CustodianViewCustdnPocSer] [nvarchar](255) NULL,
	[CustodianName] [nvarchar](255) NULL,
	[AcqCostLow] [money] NULL,
	[AcqCostHigh] [money] NULL,
	[RunDate] [datetime] NULL
) ON [PRIMARY]
```

This table needs to be populated by hand using an export from DMLSS.  The RunDate field does not come from DMLSS, but is the datetime of the import operation.

## More Info

Finally, there are a number of variables in Resources.resx that apply certain local settings (such as the connection string to your SQL server) that need to be set properly prior to compilation.  See (slightly outdated) [User Guide](.\LogLookupTool_Manual_1.0.8.0.pdf) and [pre-Github changelog](.\changelog.md) for more information.
