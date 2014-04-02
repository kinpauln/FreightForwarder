select * from dbo.RouteInformationItems

delete from RouteInformationItems where id>80001

insert into RouteInformationItems(ShipName,StartPort,
DestinationPort,StartDay,[20GP],[40GP],[40HQ],Nonstop,SailDayLength,ValidDate,Remarks,IsSingleContainer,CompanyId,CreateDate,UpdateDate,DeleteDate,IsDeleted
)
select ShipName,StartPort,DestinationPort,StartDay,20GP,40GP,40HQ,Nonstop,SailDayLength,ValidDate,Remarks,IsSingleContainer,CompanyId,CreateDate,UpdateDate,DeleteDate,IsDeleted
from RouteInformationItems

SELECT 
    [Extent1].[Id] AS [Id], 
    [Extent1].[ShipName] AS [ShipName], 
    [Extent1].[StartPort] AS [StartPort], 
    [Extent1].[DestinationPort] AS [DestinationPort], 
    [Extent1].[StartDay] AS [StartDay], 
    [Extent1].[20GP] AS [20GP], 
    [Extent1].[40GP] AS [40GP], 
    [Extent1].[40HQ] AS [40HQ], 
    [Extent1].[Nonstop] AS [Nonstop], 
    [Extent1].[SailDayLength] AS [SailDayLength], 
    [Extent1].[ValidDate] AS [ValidDate], 
    [Extent1].[Remarks] AS [Remarks], 
    [Extent1].[IsSingleContainer] AS [IsSingleContainer], 
    [Extent1].[CompanyId] AS [CompanyId], 
    [Extent1].[CreateDate] AS [CreateDate], 
    [Extent1].[UpdateDate] AS [UpdateDate], 
    [Extent1].[DeleteDate] AS [DeleteDate], 
    [Extent1].[IsDeleted] AS [IsDeleted], 
    [Extent2].[Id] AS [Id1], 
    [Extent2].[Code] AS [Code], 
    [Extent2].[Name] AS [Name]
    FROM  [dbo].[RouteInformationItems] AS [Extent1]
    INNER JOIN [dbo].[Company] AS [Extent2] ON [Extent1].[CompanyId] = [Extent2].[Id]
    WHERE [Extent1].[IsDeleted] <> cast(1 as bit)