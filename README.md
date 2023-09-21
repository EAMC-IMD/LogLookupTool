# LogLookupTool

This is an interface for gathering and displaying data created by [Logon Script](https://github.com/EAMC-IMD/Logon_Script).  

## Data Structures

It relies on the data structures outlined and created by that project.  For completeness, an SQL script to create the primary database is included at [EUDLogging.sql](./EUDLogging.sql).

Additionally, various database objects assume the presence of a database named pcInventory with a table named DMLSS.  This table is required to map ECNs to serial numbers and hostnames.  The minimum structure of DMLSS is as follows:

```
CREATE TABLE [dbo].[DMLSS](
	[Ecn5] [nvarchar](255) NULL,
	[MedicalEquipmentMeId] [nvarchar](255) NULL,
	[Ecn] [nvarchar](255) NULL,
	[MedicalEquipmentDeviceClsCode] [nvarchar](255) NULL,
	[MedicalEquipmentDeviceCd] [nvarchar](255) NULL,
	[MedicalEquipmentManufOrgSerial] [nvarchar](255) NULL,
	[MfrSerialNo] [nvarchar](255) NULL,
	[MedicalEquipmentSystemTypeCd] [nvarchar](255) NULL,
	[MedicalEquipmentDeleteInd] [nvarchar](255) NULL,
	[MedicalEquipmentManufMdlSerId] [nvarchar](255) NULL,
	[NameplateModel] [nvarchar](255) NULL,
	[MedicalEquipmentEqpmtOwnerTypCd] [nvarchar](255) NULL,
	[MedicalEquipmentMaSerial] [nvarchar](255) NULL,
	[MedicalEquipmentMaintSrcOrgSer] [nvarchar](255) NULL,
	[MedicalEquipmentUsmantSrcOrgSer] [nvarchar](255) NULL,
	[MedicalEquipmentOtherGovtSosSer] [nvarchar](255) NULL,
	[MedicalEquipmentPltPermLocSer] [nvarchar](255) NULL,
	[MedicalEquipmentMeWrmPeactmCd] [nvarchar](255) NULL,
	[MedicalEquipmentMeAcqCostQty] [nvarchar](255) NULL,
	[MedicalEquipmentMeDiscountAmt] [money] NULL,
	[MedicalEquipmentMeTradeInAmt] [money] NULL,
	[MedicalEquipmentMeTransportAmt] [money] NULL,
	[MedicalEquipmentMeInstalltionAmt] [money] NULL,
	[MedicalEquipmentMeOtherMiscAmt] [money] NULL,
	[MedicalEquipmentMeUpgradeCstAmt] [money] NULL,
	[AcqDate] [date] NULL,
	[MedicalEquipmentCustomerOrgSer] [nvarchar](255) NULL,
	[MedicalEquipmentMeOnloanCd] [nvarchar](255) NULL,
	[ItemId] [nvarchar](255) NULL,
	[EquipItemEiAccntblCd] [nvarchar](255) NULL,
	[EquipItemEiMaintReqrCd] [nvarchar](255) NULL,
	[ContractServiceContractorSosSer] [nvarchar](255) NULL,
	[Nomenclature] [nvarchar](255) NULL,
	[LifeExp] [nvarchar](255) NULL,
	[Manufacturer] [nvarchar](255) NULL,
	[CommonModel] [nvarchar](255) NULL,
	[EquipmentLocation] [nvarchar](255) NULL,
	[Ownership] [nvarchar](255) NULL,
	[MaintAct] [nvarchar](255) NULL,
	[SchedTeam] [nvarchar](255) NULL,
	[UnschedTeam] [nvarchar](255) NULL,
	[Contractor] [nvarchar](255) NULL,
	[OrgViewMtfSerial] [nvarchar](255) NULL,
	[MtfOrgId] [nvarchar](255) NULL,
	[MtfOrgNm] [nvarchar](255) NULL,
	[OrgViewCustOrgSer] [nvarchar](255) NULL,
	[CustomerId] [nvarchar](255) NULL,
	[CustomerName] [nvarchar](255) NULL,
	[CustodianViewCustdnPocSer] [nvarchar](255) NULL,
	[CustodianName] [nvarchar](255) NULL,
	[AssemblageOrgSerial] [nvarchar](255) NULL,
	[AssemblageOrgNm] [nvarchar](255) NULL,
	[AssemblageDesc] [nvarchar](255) NULL,
	[AssemblageNo] [nvarchar](255) NULL,
	[AssemblageOrgId] [nvarchar](255) NULL,
	[AcqCostLow] [money] NULL,
	[AcqCostHigh] [money] NULL,
	[RunDate] [datetime] NULL
) ON [PRIMARY]
```

This table needs to be populated by hand using an export from DMLSS.

## More Info

Finally, there are a number of variables in Resources.resx that apply certain local settings (such as the connection string to your SQL server) that need to be set properly prior to compilation.  See (slightly outdated) [User Guide](.\LogLookupTool_Manual_1.0.8.0.pdf) and [pre-Github changelog](.\changelog.md) for more information.
