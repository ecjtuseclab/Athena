USE [Athena]
GO
/****** Object:  Table [dbo].[workflownodeoperator]    Script Date: 01/28/2018 20:04:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[workflownodeoperator](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[nodeactionid] [int] NOT NULL,
	[operatorid] [int] NOT NULL,
	[operatortype] [int] NOT NULL,
	[begintime] [datetime] NULL,
	[endtime] [datetime] NULL,
	[operatorstatus] [int] NULL,
	[operatorlock] [int] NULL,
	[nodeoperator] [nvarchar](256) NULL,
 CONSTRAINT [PK_workflownodeoperator] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[workflownodeaction]    Script Date: 01/28/2018 20:04:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[workflownodeaction](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[wfid] [int] NULL,
	[actionid] [int] NULL,
	[nodeactionname] [nvarchar](50) NULL,
	[nodeactionmemo] [nvarchar](256) NULL,
	[currentnodeid] [int] NOT NULL,
	[nextnodeid] [int] NOT NULL,
	[nastatus] [int] NULL,
	[begintime] [datetime] NULL,
	[endtime] [datetime] NULL,
	[nacondition] [nvarchar](1024) NULL,
	[nalock] [int] NULL,
	[nodeactioncode] [int] NULL,
	[nodetype] [int] NULL,
 CONSTRAINT [PK_wfstep_auth] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[workflownode]    Script Date: 01/28/2018 20:04:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[workflownode](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[wfid] [int] NOT NULL,
	[wfnodename] [nvarchar](128) NOT NULL,
	[wfnodememo] [nvarchar](128) NOT NULL,
	[wfnodeflag] [int] NULL,
	[wfnodebegintime] [datetime] NULL,
	[wfnodeendtime] [datetime] NULL,
	[wfnodestatus] [int] NULL,
	[wfnodelock] [int] NULL,
 CONSTRAINT [PK_wfstepdefine] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[workflowinstancetracings]    Script Date: 01/28/2018 20:04:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[workflowinstancetracings](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[instanceid] [int] NOT NULL,
	[startnode] [nvarchar](256) NOT NULL,
	[endnode] [nvarchar](256) NOT NULL,
	[executeaction] [nvarchar](256) NULL,
	[executer] [nvarchar](256) NULL,
	[executetime] [datetime] NULL,
	[remark] [nvarchar](1024) NULL,
 CONSTRAINT [PK_workflowinstancetracings] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[workflowinstances]    Script Date: 01/28/2018 20:04:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[workflowinstances](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[wfid] [int] NOT NULL,
	[ownertabledataid] [int] NOT NULL,
	[currentnodeid] [int] NOT NULL,
	[status] [int] NULL,
	[datalock] [int] NULL,
	[nodcode] [int] NULL,
 CONSTRAINT [PK_workflowinstances] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[workflow]    Script Date: 01/28/2018 20:04:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[workflow](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[wfname] [nvarchar](50) NOT NULL,
	[wfmemo] [nvarchar](1024) NULL,
	[wfbegintime] [datetime] NULL,
	[wfstoptime] [datetime] NULL,
	[wfflag] [int] NOT NULL,
	[wfownertable] [nvarchar](128) NULL,
	[wfinstancestable] [nvarchar](128) NULL,
	[wfstatus] [int] NULL,
	[wflock] [int] NULL,
	[wffieldname] [nvarchar](256) NULL,
	[wfjsonstr] [nvarchar](max) NULL,
 CONSTRAINT [PK_wfdefine] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user_role]    Script Date: 01/28/2018 20:04:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_role](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[userid] [int] NOT NULL,
	[roleid] [int] NOT NULL,
	[rolescode] [int] NOT NULL,
 CONSTRAINT [PK_user_role] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user_group]    Script Date: 01/28/2018 20:04:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_group](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[userid] [int] NOT NULL,
	[groupid] [int] NOT NULL,
 CONSTRAINT [PK_user_group] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 01/28/2018 20:04:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[username] [nvarchar](64) NOT NULL,
	[password] [nvarchar](128) NOT NULL,
	[rolelist] [nvarchar](50) NULL,
	[grouplist] [nvarchar](50) NULL,
	[pubkey] [nvarchar](512) NULL,
	[prikey] [nvarchar](512) NULL,
	[photo] [nchar](512) NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role_resource]    Script Date: 01/28/2018 20:04:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role_resource](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[roleid] [int] NOT NULL,
	[resoureceid] [int] NOT NULL,
 CONSTRAINT [PK_role_resource] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role_action]    Script Date: 01/28/2018 20:04:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role_action](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[roleid] [int] NOT NULL,
	[actionid] [int] NOT NULL,
	[controlername] [nvarchar](50) NULL,
	[actionscode] [int] NULL,
 CONSTRAINT [PK_role_action] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role]    Script Date: 01/28/2018 20:04:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[rolename] [nvarchar](50) NOT NULL,
	[rolecode] [int] NOT NULL,
	[roleexpiretime] [datetime] NULL,
 CONSTRAINT [PK_role] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[resource]    Script Date: 01/28/2018 20:04:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[resource](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[resourcename] [nvarchar](50) NOT NULL,
	[resourcetype] [int] NOT NULL,
	[resourceurl] [nvarchar](1024) NULL,
	[resourcescript] [nvarchar](1024) NULL,
	[resourceowner] [nvarchar](1024) NULL,
	[resourceleval] [int] NULL,
	[resourceleftico] [nvarchar](50) NULL,
	[resourcerightico] [nvarchar](50) NULL,
	[resourceclass] [nvarchar](50) NULL,
	[resourcenumber] [int] NULL,
	[order] [int] NULL,
	[description] [nvarchar](max) NULL,
 CONSTRAINT [PK__resource__3213E83F30F848ED] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[propertymapping]    Script Date: 01/28/2018 20:04:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[propertymapping](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[propertyName] [nvarchar](50) NULL,
	[propertyValue] [nvarchar](50) NULL,
	[propertyMeaning] [nvarchar](50) NULL,
	[remark] [nvarchar](2048) NULL,
	[parentId] [int] NULL,
 CONSTRAINT [PK_propertymapping] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[menu]    Script Date: 01/28/2018 20:04:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[menu](
	[Id] [varchar](50) NOT NULL,
	[ParentId] [varchar](50) NULL,
	[EnCode] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NULL,
	[Icon] [nvarchar](50) NULL,
	[UrlAddress] [nvarchar](500) NULL,
	[OpenTarget] [nvarchar](50) NULL,
	[IsMenu] [bit] NOT NULL,
	[IsExpand] [bit] NOT NULL,
	[IsPublic] [bit] NOT NULL,
	[SortCode] [int] NULL,
	[Description] [nvarchar](500) NULL,
	[CreationTime] [datetime] NOT NULL,
	[CreateUserId] [varchar](50) NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserId] [varchar](50) NULL,
	[IsEnabled] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeleteTime] [datetime] NULL,
	[DeleteUserId] [varchar](50) NULL,
 CONSTRAINT [PK_SYS_MODULE] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'模块主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'menu', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'menu', @level2type=N'COLUMN',@level2name=N'ParentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'menu', @level2type=N'COLUMN',@level2name=N'EnCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'menu', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图标' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'menu', @level2type=N'COLUMN',@level2name=N'Icon'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'连接' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'menu', @level2type=N'COLUMN',@level2name=N'UrlAddress'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'目标' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'menu', @level2type=N'COLUMN',@level2name=N'OpenTarget'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'menu', @level2type=N'COLUMN',@level2name=N'IsMenu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'展开' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'menu', @level2type=N'COLUMN',@level2name=N'IsExpand'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公共' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'menu', @level2type=N'COLUMN',@level2name=N'IsPublic'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'menu', @level2type=N'COLUMN',@level2name=N'SortCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'menu', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'menu', @level2type=N'COLUMN',@level2name=N'CreationTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'menu', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'menu', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'menu', @level2type=N'COLUMN',@level2name=N'LastModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'有效标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'menu', @level2type=N'COLUMN',@level2name=N'IsEnabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'menu', @level2type=N'COLUMN',@level2name=N'IsDeleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'menu', @level2type=N'COLUMN',@level2name=N'DeleteTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'menu', @level2type=N'COLUMN',@level2name=N'DeleteUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统模块' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'menu'
GO
/****** Object:  Table [dbo].[group]    Script Date: 01/28/2018 20:04:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[group](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[groupname] [nvarchar](50) NOT NULL,
	[groupcode] [int] NULL,
 CONSTRAINT [PK_group] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[backup]    Script Date: 01/28/2018 20:04:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[backup](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[databasename] [nvarchar](50) NOT NULL,
	[backupname] [nvarchar](50) NOT NULL,
	[backupsize] [nvarchar](50) NULL,
	[backuptime] [datetime] NULL,
	[backuppersonnel] [nvarchar](50) NULL,
	[instructions] [nvarchar](max) NULL,
	[absolutepath] [nvarchar](max) NULL,
	[relativepath] [nvarchar](max) NULL,
 CONSTRAINT [PK_backup] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[article]    Script Date: 01/28/2018 20:04:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[article](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](50) NOT NULL,
	[content] [nvarchar](max) NULL,
	[inserttime] [datetime] NULL,
	[edittime] [datetime] NULL,
	[type] [int] NULL,
	[SortID] [int] NULL,
	[author] [nvarchar](50) NULL,
 CONSTRAINT [PK_article] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[area]    Script Date: 01/28/2018 20:04:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[area](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[parentid] [nvarchar](max) NULL,
	[layers] [int] NULL,
	[encode] [nvarchar](max) NULL,
	[fullname] [nvarchar](max) NULL,
 CONSTRAINT [PK_area] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[action]    Script Date: 01/28/2018 20:04:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[action](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[actionname] [nvarchar](50) NOT NULL,
	[actionurl] [nvarchar](1024) NULL,
	[actionparam] [nvarchar](1024) NULL,
	[actiontype] [int] NULL,
	[actionowner] [nvarchar](50) NULL,
	[actioncode] [int] NOT NULL,
	[controlername] [nvarchar](50) NOT NULL,
	[actiondescription] [nvarchar](50) NULL,
 CONSTRAINT [PK_action] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
