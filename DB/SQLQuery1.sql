select * from dbo.RouteInformationItems

delete from RouteInformationItems where id>80001

insert into RouteInformationItems(ShipName,StartPort,
DestinationPort,StartDay,[20GP],[40GP],[40HQ],Nonstop,SailDayLength,ValidDate,Remarks,IsSingleContainer,CompanyId,CreateDate,UpdateDate,DeleteDate,IsDeleted
)
select ShipName,StartPort,DestinationPort,StartDay,20GP,40GP,40HQ,Nonstop,SailDayLength,ValidDate,Remarks,IsSingleContainer,CompanyId,CreateDate,UpdateDate,DeleteDate,IsDeleted
from RouteInformationItems


-- ’Àı»’÷æ
USE FFDB;
GO
-- Truncate the log by changing the database recovery model to SIMPLE.
ALTER DATABASE FFDB
SET RECOVERY SIMPLE;
GO
-- Shrink the truncated log file to 1 MB.
DBCC SHRINKFILE (FFDB_Log, 1);
GO
-- Reset the database recovery model.
ALTER DATABASE FFDB
SET RECOVERY FULL;
GO