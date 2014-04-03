1.添加表CustomerServiceWorkList

CREATE TABLE [dbo].[CustomerServiceWorkList](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UserId] [int] NULL,
	[BusinessId] [int] NULL
	)
	
2.为TerminalReapplyBusiness表添加 Remark字段
alter table TerminalReapplyBusiness
add Remark ntext

alter table TerminalReapplyBusiness
add UpdateTime Datetime

update TerminalReapplyBusiness set UpdateTime=getdate()


3.为CustomerServiceWorkList表添加 EntryTime字段
alter table CustomerServiceWorkList
add EntryTime Datetime