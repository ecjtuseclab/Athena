USE [Athena]
GO
/****** Object:  Table [dbo].[workflownodeoperator]    Script Date: 01/28/2018 20:05:33 ******/
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
SET IDENTITY_INSERT [dbo].[workflownodeoperator] ON
INSERT [dbo].[workflownodeoperator] ([id], [nodeactionid], [operatorid], [operatortype], [begintime], [endtime], [operatorstatus], [operatorlock], [nodeoperator]) VALUES (70, 47, 0, 1, NULL, NULL, 1, 1, N'28')
INSERT [dbo].[workflownodeoperator] ([id], [nodeactionid], [operatorid], [operatortype], [begintime], [endtime], [operatorstatus], [operatorlock], [nodeoperator]) VALUES (71, 48, 0, 1, NULL, NULL, 1, 1, N'26')
INSERT [dbo].[workflownodeoperator] ([id], [nodeactionid], [operatorid], [operatortype], [begintime], [endtime], [operatorstatus], [operatorlock], [nodeoperator]) VALUES (72, 49, 0, 1, NULL, NULL, 1, 1, N'41')
SET IDENTITY_INSERT [dbo].[workflownodeoperator] OFF
/****** Object:  Table [dbo].[workflownodeaction]    Script Date: 01/28/2018 20:05:33 ******/
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
SET IDENTITY_INSERT [dbo].[workflownodeaction] ON
INSERT [dbo].[workflownodeaction] ([id], [wfid], [actionid], [nodeactionname], [nodeactionmemo], [currentnodeid], [nextnodeid], [nastatus], [begintime], [endtime], [nacondition], [nalock], [nodeactioncode], [nodetype]) VALUES (47, 40, 34, NULL, NULL, 137, 138, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[workflownodeaction] ([id], [wfid], [actionid], [nodeactionname], [nodeactionmemo], [currentnodeid], [nextnodeid], [nastatus], [begintime], [endtime], [nacondition], [nalock], [nodeactioncode], [nodetype]) VALUES (48, 40, 35, NULL, NULL, 138, 139, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[workflownodeaction] ([id], [wfid], [actionid], [nodeactionname], [nodeactionmemo], [currentnodeid], [nextnodeid], [nastatus], [begintime], [endtime], [nacondition], [nalock], [nodeactioncode], [nodetype]) VALUES (49, 40, 35, NULL, NULL, 139, 140, NULL, NULL, NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[workflownodeaction] OFF
/****** Object:  Table [dbo].[workflownode]    Script Date: 01/28/2018 20:05:33 ******/
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
SET IDENTITY_INSERT [dbo].[workflownode] ON
INSERT [dbo].[workflownode] ([id], [wfid], [wfnodename], [wfnodememo], [wfnodeflag], [wfnodebegintime], [wfnodeendtime], [wfnodestatus], [wfnodelock]) VALUES (137, 40, N'状态1', N'状态1', NULL, NULL, NULL, 1, 1)
INSERT [dbo].[workflownode] ([id], [wfid], [wfnodename], [wfnodememo], [wfnodeflag], [wfnodebegintime], [wfnodeendtime], [wfnodestatus], [wfnodelock]) VALUES (138, 40, N'状态2', N'状态2', NULL, NULL, NULL, 1, 1)
INSERT [dbo].[workflownode] ([id], [wfid], [wfnodename], [wfnodememo], [wfnodeflag], [wfnodebegintime], [wfnodeendtime], [wfnodestatus], [wfnodelock]) VALUES (139, 40, N'状态3', N'状态3', NULL, NULL, NULL, 1, 1)
INSERT [dbo].[workflownode] ([id], [wfid], [wfnodename], [wfnodememo], [wfnodeflag], [wfnodebegintime], [wfnodeendtime], [wfnodestatus], [wfnodelock]) VALUES (140, 40, N'状态5', N'状态5', NULL, NULL, NULL, 1, 1)
SET IDENTITY_INSERT [dbo].[workflownode] OFF
/****** Object:  Table [dbo].[workflowinstancetracings]    Script Date: 01/28/2018 20:05:33 ******/
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
/****** Object:  Table [dbo].[workflowinstances]    Script Date: 01/28/2018 20:05:33 ******/
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
/****** Object:  Table [dbo].[workflow]    Script Date: 01/28/2018 20:05:33 ******/
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
SET IDENTITY_INSERT [dbo].[workflow] ON
INSERT [dbo].[workflow] ([id], [wfname], [wfmemo], [wfbegintime], [wfstoptime], [wfflag], [wfownertable], [wfinstancestable], [wfstatus], [wflock], [wffieldname], [wfjsonstr]) VALUES (40, N'workflowTest', N'工作流测试', CAST(0x0000A85B00000000 AS DateTime), CAST(0x0000A87500000000 AS DateTime), 0, N'TableTest', N'WorkflowInstance', 1, 1, N'auditState', N'{"states":{"rect1":{"type":"state","text":{"text":"状态1"},"attr":{"x":72,"y":132,"width":100,"height":50},"props":{"text":{"value":"状态1"},"id":{"value":"137"},"wfnodename":{"value":"状态1"},"wfnodememo":{"value":"状态1"},"wfnodestatus":{"value":"1"},"wfnodelock":{"value":"1"}}},"rect2":{"type":"task","text":{"text":"操作动作2"},"attr":{"x":277,"y":175,"width":100,"height":50},"props":{"text":{"value":"操作动作2"},"id":{"value":"47"},"actionid":{"value":"34"},"nastatus":{"value":"1"},"nalock":{"value":"1"},"nodetype":{"value":"1"},"opid":{"value":"70"},"operatortype":{"value":"1"},"nodeoperator":{"value":"28"},"operatorstatus":{"value":"1"},"operatorlock":{"value":"1"}}},"rect3":{"type":"state","text":{"text":"状态2"},"attr":{"x":420,"y":126,"width":100,"height":50},"props":{"text":{"value":"状态2"},"id":{"value":"138"},"wfnodename":{"value":"状态2"},"wfnodememo":{"value":"状态2"},"wfnodestatus":{"value":"1"},"wfnodelock":{"value":"1"}}},"rect4":{"type":"task","text":{"text":"操作动作"},"attr":{"x":459,"y":266,"width":100,"height":50},"props":{"text":{"value":"操作动作"},"id":{"value":"48"},"actionid":{"value":"35"},"nastatus":{"value":"1"},"nalock":{"value":"1"},"nodetype":{"value":"1"},"opid":{"value":"71"},"operatortype":{"value":"1"},"nodeoperator":{"value":"26"},"operatorstatus":{"value":"1"},"operatorlock":{"value":"1"}}},"rect5":{"type":"state","text":{"text":"状态3"},"attr":{"x":718,"y":168,"width":100,"height":50},"props":{"text":{"value":"状态3"},"id":{"value":"139"},"wfnodename":{"value":"状态3"},"wfnodememo":{"value":"状态3"},"wfnodestatus":{"value":"1"},"wfnodelock":{"value":"1"}}},"rect6":{"type":"task","text":{"text":"操作动作"},"attr":{"x":741,"y":286,"width":100,"height":50},"props":{"text":{"value":"操作动作"},"id":{"value":"49"},"actionid":{"value":"35"},"nastatus":{"value":"1"},"nalock":{"value":"1"},"nodetype":{"value":"1"},"opid":{"value":"72"},"operatortype":{"value":"1"},"nodeoperator":{"value":"41"},"operatorstatus":{"value":"1"},"operatorlock":{"value":"1"}}},"rect7":{"type":"state","text":{"text":"状态5"},"attr":{"x":884,"y":401,"width":100,"height":50},"props":{"text":{"value":"状态5"},"id":{"value":"140"},"wfnodename":{"value":"状态5"},"wfnodememo":{"value":"状态5"},"wfnodestatus":{"value":"1"},"wfnodelock":{"value":"1"}}}},"paths":{"path8":{"from":"rect1","to":"rect2","dots":[],"text":{"text":"TO 操作动作2"},"textPos":{"x":0,"y":-10},"props":{"text":{"value":""}}},"path9":{"from":"rect2","to":"rect3","dots":[],"text":{"text":"TO 状态2"},"textPos":{"x":0,"y":-10},"props":{"text":{"value":""}}},"path10":{"from":"rect3","to":"rect4","dots":[],"text":{"text":"TO 操作动作"},"textPos":{"x":0,"y":-10},"props":{"text":{"value":""}}},"path11":{"from":"rect4","to":"rect5","dots":[],"text":{"text":"TO 状态3"},"textPos":{"x":0,"y":-10},"props":{"text":{"value":""}}},"path12":{"from":"rect5","to":"rect6","dots":[],"text":{"text":"TO 操作动作"},"textPos":{"x":0,"y":-10},"props":{"text":{"value":""}}},"path13":{"from":"rect6","to":"rect7","dots":[],"text":{"text":"TO 状态5"},"textPos":{"x":0,"y":-10},"props":{"text":{"value":""}}}},"props":{"props":{"id":{"value":"40"},"wfname":{"value":"workflowTest"},"wfmemo":{"value":"工作流测试"},"wfownertable":{"value":"TableTest"},"wffieldname":{"value":"auditState"},"wfbegintime":{"value":"2018-01-01 00:00:00"},"wfstoptime":{"value":"2018-01-27 00:00:00"},"wfstatus":{"value":"1"},"wflock":{"value":"1"}}}}')
SET IDENTITY_INSERT [dbo].[workflow] OFF
/****** Object:  Table [dbo].[user_role]    Script Date: 01/28/2018 20:05:33 ******/
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
SET IDENTITY_INSERT [dbo].[user_role] ON
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (37, 1, 1, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (38, 3, 25, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (39, 12, 32, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (40, 13, 32, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (41, 18, 32, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (42, 21, 34, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (43, 51, 34, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (44, 52, 32, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (45, 53, 32, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (46, 54, 34, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (49, 57, 34, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (51, 59, 32, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (52, 60, 34, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (54, 69, 32, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (56, 85, 26, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (57, 86, 32, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (59, 87, 25, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (60, 87, 32, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (61, 87, 41, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (65, 89, 25, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (67, 89, 41, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (70, 92, 25, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (71, 92, 28, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (72, 89, 34, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (75, 94, 1, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (76, 94, 25, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (78, 96, 41, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (87, 104, 28, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (88, 104, 32, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (89, 105, 28, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (90, 106, 1, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (91, 106, 28, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (92, 107, 41, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (93, 108, 32, 0)
SET IDENTITY_INSERT [dbo].[user_role] OFF
/****** Object:  Table [dbo].[user_group]    Script Date: 01/28/2018 20:05:33 ******/
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
SET IDENTITY_INSERT [dbo].[user_group] ON
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (55, 12, 113)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (56, 13, 113)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (57, 18, 113)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (58, 21, 113)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (59, 51, 113)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (60, 52, 113)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (61, 53, 113)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (62, 54, 102)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (65, 57, 111)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (67, 59, 111)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (68, 60, 113)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (70, 69, 108)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (72, 85, 7)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (73, 86, 9)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (76, 12, 113)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (77, 13, 113)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (80, 3, 7)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (81, 18, 113)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (82, 21, 113)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (83, 51, 113)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (84, 52, 113)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (85, 53, 113)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (86, 54, 102)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (89, 57, 111)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (91, 59, 111)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (92, 60, 113)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (96, 85, 7)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (97, 86, 9)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (98, 1, 6)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (99, 3, 8)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (101, 3, 7)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (102, 3, 8)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (103, 3, 9)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (104, 51, 113)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (105, 21, 113)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (106, 12, 113)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (107, 87, 7)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (108, 87, 102)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (118, 92, 102)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (121, 94, 6)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (123, 96, 106)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (128, 92, 102)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (134, 104, 8)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (135, 104, 9)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (136, 105, 9)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (137, 106, 7)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (139, 106, 7)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (140, 106, 105)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (141, 107, 105)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (142, 108, 105)
SET IDENTITY_INSERT [dbo].[user_group] OFF
/****** Object:  Table [dbo].[user]    Script Date: 01/28/2018 20:05:33 ******/
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
SET IDENTITY_INSERT [dbo].[user] ON
INSERT [dbo].[user] ([id], [username], [password], [rolelist], [grouplist], [pubkey], [prikey], [photo]) VALUES (1, N'Athena', N'IHz0EFMvkqR97iRc6bEf9x9Xjr12PrO76kTr0EPQGPs=', N'超级管理员', N'管理部,', N'66B709AC0C131CD43B176C2BCBF7083531742C926D802BC786D434803BAAA6AA210B03AC9C1681FD5DA849BFF8A3F1766F2219B35E395A0F2C9082F2B554430C', N'24AEAFF1C779D11A33AEAE80EECA4F99AB89C88BA34B7B8FCB44F821CF7E82C5', NULL)
INSERT [dbo].[user] ([id], [username], [password], [rolelist], [grouplist], [pubkey], [prikey], [photo]) VALUES (3, N'Athenaz', N'IHz0EFMvkqR97iRc6bEf9x9Xjr12PrO76kTr0EPQGPs=', N'管理员', N'人事部,技术部,人事部,技术部,生产部,', NULL, NULL, NULL)
INSERT [dbo].[user] ([id], [username], [password], [rolelist], [grouplist], [pubkey], [prikey], [photo]) VALUES (12, N'ztt', N'IHz0EFMvkqR97iRc6bEf9x9Xjr12PrO76kTr0EPQGPs=', N' 白银会员', N' 售后服务部', NULL, NULL, NULL)
INSERT [dbo].[user] ([id], [username], [password], [rolelist], [grouplist], [pubkey], [prikey], [photo]) VALUES (13, N'wl', N'IHz0EFMvkqR97iRc6bEf9x9Xjr12PrO76kTr0EPQGPs=', N' 白银会员', N' 售后服务部', NULL, NULL, NULL)
INSERT [dbo].[user] ([id], [username], [password], [rolelist], [grouplist], [pubkey], [prikey], [photo]) VALUES (18, N'wl_test', N'IHz0EFMvkqR97iRc6bEf9x9Xjr12PrO76kTr0EPQGPs=', N' 白银会员', N' 售后服务部', NULL, NULL, NULL)
INSERT [dbo].[user] ([id], [username], [password], [rolelist], [grouplist], [pubkey], [prikey], [photo]) VALUES (21, N'zq', N'IHz0EFMvkqR97iRc6bEf9x9Xjr12PrO76kTr0EPQGPs=', N' 普通会员', N' 售后服务部', NULL, NULL, NULL)
INSERT [dbo].[user] ([id], [username], [password], [rolelist], [grouplist], [pubkey], [prikey], [photo]) VALUES (51, N'zyj', N'IHz0EFMvkqR97iRc6bEf9x9Xjr12PrO76kTr0EPQGPs=', N'普通会员', N'售后服务部,售后服务部,售后服务部,', NULL, NULL, NULL)
INSERT [dbo].[user] ([id], [username], [password], [rolelist], [grouplist], [pubkey], [prikey], [photo]) VALUES (52, N'zrf', N'IHz0EFMvkqR97iRc6bEf9x9Xjr12PrO76kTr0EPQGPs=', N' 白银会员', N' 售后服务部', NULL, NULL, NULL)
INSERT [dbo].[user] ([id], [username], [password], [rolelist], [grouplist], [pubkey], [prikey], [photo]) VALUES (53, N'zll', N'IHz0EFMvkqR97iRc6bEf9x9Xjr12PrO76kTr0EPQGPs=', N' 白银会员', N' 售后服务部', NULL, NULL, NULL)
INSERT [dbo].[user] ([id], [username], [password], [rolelist], [grouplist], [pubkey], [prikey], [photo]) VALUES (54, N'cxj', N'IHz0EFMvkqR97iRc6bEf9x9Xjr12PrO76kTr0EPQGPs=', N' 普通会员', N' 财政部', NULL, NULL, NULL)
INSERT [dbo].[user] ([id], [username], [password], [rolelist], [grouplist], [pubkey], [prikey], [photo]) VALUES (57, N'gah', N'IHz0EFMvkqR97iRc6bEf9x9Xjr12PrO76kTr0EPQGPs=', N' 普通会员', N' 后勤部', NULL, NULL, NULL)
INSERT [dbo].[user] ([id], [username], [password], [rolelist], [grouplist], [pubkey], [prikey], [photo]) VALUES (59, N'hzf', N'IHz0EFMvkqR97iRc6bEf9x9Xjr12PrO76kTr0EPQGPs=', N' 白银会员', N' 后勤部', NULL, NULL, NULL)
INSERT [dbo].[user] ([id], [username], [password], [rolelist], [grouplist], [pubkey], [prikey], [photo]) VALUES (60, N'lhy', N'IHz0EFMvkqR97iRc6bEf9x9Xjr12PrO76kTr0EPQGPs=', N' 普通会员', N' 售后服务部', NULL, NULL, NULL)
INSERT [dbo].[user] ([id], [username], [password], [rolelist], [grouplist], [pubkey], [prikey], [photo]) VALUES (106, N'wxx', N'IHz0EFMvkqR97iRc6bEf9x9Xjr12PrO76kTr0EPQGPs=', N' 超级管理员,白金会员', N' 人事部,人力资源部', NULL, NULL, NULL)
INSERT [dbo].[user] ([id], [username], [password], [rolelist], [grouplist], [pubkey], [prikey], [photo]) VALUES (107, N'hh', N'IHz0EFMvkqR97iRc6bEf9x9Xjr12PrO76kTr0EPQGPs=', N' 游客', N' 人力资源部', NULL, NULL, NULL)
INSERT [dbo].[user] ([id], [username], [password], [rolelist], [grouplist], [pubkey], [prikey], [photo]) VALUES (108, N'cll123', N'IHz0EFMvkqR97iRc6bEf9x9Xjr12PrO76kTr0EPQGPs=', N'白银会员', N'人力资源部,', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[user] OFF
/****** Object:  Table [dbo].[role_resource]    Script Date: 01/28/2018 20:05:33 ******/
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
SET IDENTITY_INSERT [dbo].[role_resource] ON
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (2, 4, 1)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (3, 4, 5)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (4, 4, 7)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (6, 4, 9)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (7, 4, 8)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (8, 4, 10)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (16, 29, 3)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (17, 29, 14)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (18, 29, 17)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (19, 29, 19)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (23, 28, 1)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (25, 28, 7)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (27, 28, 9)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (31, 28, 3)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (32, 28, 14)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (33, 28, 17)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (34, 28, 19)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (35, 28, 172)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (36, 28, 4)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (37, 28, 20)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (38, 28, 22)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (39, 28, 23)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (40, 28, 24)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (41, 26, 1)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (42, 26, 5)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (43, 26, 7)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (44, 26, 8)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (45, 26, 9)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (46, 26, 10)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (47, 26, 11)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (48, 26, 12)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (49, 25, 1)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (50, 25, 5)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (51, 25, 7)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (52, 25, 8)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (53, 25, 9)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (54, 25, 10)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (55, 25, 11)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (56, 25, 12)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (57, 32, 1)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (58, 32, 5)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (59, 32, 7)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (60, 32, 8)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (64, 34, 1)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (65, 34, 5)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (66, 34, 7)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (67, 34, 8)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (69, 35, 5)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (70, 1, 14)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (71, 1, 17)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (72, 1, 19)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (73, 1, 172)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (74, 37, 1)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (75, 37, 12)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (76, -1, 1)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (77, -1, 7)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (78, -1, 12)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (79, -1, 3)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (80, -1, 19)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (87, 41, 1)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (88, 41, 5)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (89, 41, 7)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (90, 41, 8)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (92, 42, 8)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (93, 42, 10)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (95, 42, 14)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (96, 1, 3)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (98, 43, 14)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (99, 43, 17)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (100, 43, 19)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (101, 43, 172)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (103, 44, 5)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (105, 45, 5)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (107, 46, 5)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (109, 47, 14)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (116, 50, 1)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (117, 50, 5)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (118, 50, 7)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (119, 50, 3)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (120, 50, 14)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (121, 50, 8)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (122, 51, 1)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (123, 51, 5)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (124, 51, 7)
SET IDENTITY_INSERT [dbo].[role_resource] OFF
/****** Object:  Table [dbo].[role_action]    Script Date: 01/28/2018 20:05:33 ******/
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
SET IDENTITY_INSERT [dbo].[role_action] ON
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (3, 32, 1, NULL, 0)
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (5, 26, 1, NULL, 0)
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (6, 26, 3, NULL, 0)
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (8, 32, 4, NULL, 0)
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (12, 25, 1, NULL, 0)
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (13, 25, 3, NULL, 0)
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (14, 25, 4, NULL, 0)
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (15, 32, 3, NULL, 0)
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (16, 1, 16, NULL, 0)
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (17, 1, 15, NULL, 0)
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (18, 1, 17, NULL, 0)
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (19, 1, 18, NULL, 0)
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (20, 1, 19, NULL, 0)
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (21, 1, 20, NULL, 0)
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (22, 37, 23, N'Role', 0)
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (23, 45, 15, NULL, 0)
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (24, 46, 15, NULL, 0)
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (25, 47, 15, NULL, 0)
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (28, 50, 15, NULL, 0)
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (29, 50, 16, NULL, 0)
SET IDENTITY_INSERT [dbo].[role_action] OFF
/****** Object:  Table [dbo].[role]    Script Date: 01/28/2018 20:05:33 ******/
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
SET IDENTITY_INSERT [dbo].[role] ON
INSERT [dbo].[role] ([id], [rolename], [rolecode], [roleexpiretime]) VALUES (1, N'超级管理员', 1, CAST(0x0000A76600000000 AS DateTime))
INSERT [dbo].[role] ([id], [rolename], [rolecode], [roleexpiretime]) VALUES (25, N'管理员', 33, CAST(0x0000A77100000000 AS DateTime))
INSERT [dbo].[role] ([id], [rolename], [rolecode], [roleexpiretime]) VALUES (26, N'黄金会员', 33, CAST(0x0000A77700000000 AS DateTime))
INSERT [dbo].[role] ([id], [rolename], [rolecode], [roleexpiretime]) VALUES (28, N'白金会员', 33, CAST(0x0000A85B00000000 AS DateTime))
INSERT [dbo].[role] ([id], [rolename], [rolecode], [roleexpiretime]) VALUES (32, N'白银会员', 0, CAST(0x0000A78500000000 AS DateTime))
INSERT [dbo].[role] ([id], [rolename], [rolecode], [roleexpiretime]) VALUES (34, N'普通会员', 1111, CAST(0x0000A86000000000 AS DateTime))
INSERT [dbo].[role] ([id], [rolename], [rolecode], [roleexpiretime]) VALUES (41, N'游客', 8, CAST(0x0000A87A00000000 AS DateTime))
INSERT [dbo].[role] ([id], [rolename], [rolecode], [roleexpiretime]) VALUES (50, N'测试', 1, CAST(0x0000A86100000000 AS DateTime))
INSERT [dbo].[role] ([id], [rolename], [rolecode], [roleexpiretime]) VALUES (51, N'VIP', 64, CAST(0x0000A85E00000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[role] OFF
/****** Object:  Table [dbo].[resource]    Script Date: 01/28/2018 20:05:33 ******/
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
SET IDENTITY_INSERT [dbo].[resource] ON
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (1, N'系统管理', 1, N'&nbsp;', N'1sss', NULL, 1, NULL, NULL, NULL, NULL, 0, N'&nbsp;')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (3, N'工作流', 1, N'', N'1', NULL, 1, N'fa fa-bar-chart-o', N'fa arrow', N'r1', 0, 0, NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (4, N'文章', 1, N'', N'1', NULL, 1, N'fa fa-tags', N'fa arrow', N'r3', 0, 0, NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (5, N'用户管理', 1, N'/SystemManage/User/Index', N'1', N'1', 2, N'发短信', NULL, N'r01', NULL, NULL, NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (7, N'角色管理', 1, N'/SystemManage/Role/Index', N'1', N'1', 2, NULL, NULL, N'r02', NULL, NULL, NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (8, N'动作管理', 1, N'/SystemManage/Action/Index', N'1', N'1', 2, NULL, NULL, N'r03', NULL, NULL, NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (9, N'资源管理', 1, N'/SystemManage/Resource/Index', N'1', N'1', 2, NULL, NULL, NULL, NULL, 1, N'&nbsp;')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (10, N'分组管理', 1, N'/SystemManage/Group/Index', N'1', N'1', 2, NULL, NULL, N'r05', NULL, NULL, NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (11, N'数据字典', 1, N'/SystemManage/PropertyMapping/Index', N'1', N'1', 2, NULL, NULL, N'r06', NULL, NULL, NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (12, N'区域管理', 1, N'/SystemManage/Area/Index', N'1', N'1', 2, NULL, NULL, N'r07', NULL, NULL, NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (13, N'数据库备份', 1, N'/SystemManage/Backup/Index', N'1', N'2', 2, NULL, NULL, N'r08', NULL, NULL, NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (14, N'工作流管理', 1, N'/WorkflowManage/Workflow/Index', N'1', N'3', 2, NULL, NULL, N'r11', 0, 0, NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (17, N'节点动作', 1, N'/WorkflowManage/Workflownodeaction/Index', N'1', N'3', 2, N'', N'', N'r12', 0, 0, NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (19, N'操作者管理', 1, N'/WorkflowManage/Workflownodeoperator/Index', N'1', N'3', 2, N'', N'', N'r13', 0, 0, NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (172, N'节点管理', 1, N'/WorkflowManage/Workflownode/Index', N'1', N'3', 2, N'', N'', N'r14', 0, 0, NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (192, N'文章管理', 1, N'/ArticleManage/Article/Index', N'1', N'4', 2, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[resource] OFF
/****** Object:  Table [dbo].[propertymapping]    Script Date: 01/28/2018 20:05:33 ******/
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
SET IDENTITY_INSERT [dbo].[propertymapping] ON
INSERT [dbo].[propertymapping] ([id], [propertyName], [propertyValue], [propertyMeaning], [remark], [parentId]) VALUES (7, N'其他角色', N'2020', N'基本信息管理', NULL, 0)
INSERT [dbo].[propertymapping] ([id], [propertyName], [propertyValue], [propertyMeaning], [remark], [parentId]) VALUES (8, N'系统角色', N'systemRole', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[propertymapping] OFF
/****** Object:  Table [dbo].[menu]    Script Date: 01/28/2018 20:05:33 ******/
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
INSERT [dbo].[menu] ([Id], [ParentId], [EnCode], [Name], [Icon], [UrlAddress], [OpenTarget], [IsMenu], [IsExpand], [IsPublic], [SortCode], [Description], [CreationTime], [CreateUserId], [LastModifyTime], [LastModifyUserId], [IsEnabled], [IsDeleted], [DeleteTime], [DeleteUserId]) VALUES (N'e72c75d0-3a69-41ad-b220-13c9a62ec788', N'73FD1267-79BA-4E23-A152-744AF73117E9', NULL, N'数据备份', NULL, N'/SystemSecurity/DbBackup/Index', N'iframe', 1, 0, 0, 1, NULL, CAST(0x0000A6A500CF5CB3 AS DateTime), NULL, CAST(0x0000A75A00C82683 AS DateTime), N'9f2ec0797d0f4fe290ab8b09a8302aba', 1, 0, NULL, NULL)
INSERT [dbo].[menu] ([Id], [ParentId], [EnCode], [Name], [Icon], [UrlAddress], [OpenTarget], [IsMenu], [IsExpand], [IsPublic], [SortCode], [Description], [CreationTime], [CreateUserId], [LastModifyTime], [LastModifyUserId], [IsEnabled], [IsDeleted], [DeleteTime], [DeleteUserId]) VALUES (N'462027E0-0848-41DD-BCC3-025DCAE65555', NULL, NULL, N'系统管理', N'fa fa-gears', NULL, N'expand', 0, 1, 0, 2, NULL, CAST(0x0000A6A500CF5CB3 AS DateTime), NULL, CAST(0x0000A64B010409F6 AS DateTime), N'9f2ec079-7d0f-4fe2-90ab-8b09a8302aba', 1, 0, NULL, NULL)
INSERT [dbo].[menu] ([Id], [ParentId], [EnCode], [Name], [Icon], [UrlAddress], [OpenTarget], [IsMenu], [IsExpand], [IsPublic], [SortCode], [Description], [CreationTime], [CreateUserId], [LastModifyTime], [LastModifyUserId], [IsEnabled], [IsDeleted], [DeleteTime], [DeleteUserId]) VALUES (N'73FD1267-79BA-4E23-A152-744AF73117E9', NULL, NULL, N'系统安全', N'fa fa-desktop', NULL, N'expand', 0, 1, 0, 1, NULL, CAST(0x0000A6A500CF5CB3 AS DateTime), NULL, CAST(0x0000A75A00C836C0 AS DateTime), N'9f2ec0797d0f4fe290ab8b09a8302aba', 1, 0, NULL, NULL)
INSERT [dbo].[menu] ([Id], [ParentId], [EnCode], [Name], [Icon], [UrlAddress], [OpenTarget], [IsMenu], [IsExpand], [IsPublic], [SortCode], [Description], [CreationTime], [CreateUserId], [LastModifyTime], [LastModifyUserId], [IsEnabled], [IsDeleted], [DeleteTime], [DeleteUserId]) VALUES (N'e7e1cfb2856d492faeaadc8e2962ac76', NULL, NULL, N'Wiki管理', N'fa fa-gears', NULL, N'expand', 0, 0, 0, 2, NULL, CAST(0x0000A6CD017CAB62 AS DateTime), N'9f2ec079-7d0f-4fe2-90ab-8b09a8302aba', CAST(0x0000A6CD017EE3E2 AS DateTime), N'9f2ec079-7d0f-4fe2-90ab-8b09a8302aba', 0, 0, NULL, NULL)
INSERT [dbo].[menu] ([Id], [ParentId], [EnCode], [Name], [Icon], [UrlAddress], [OpenTarget], [IsMenu], [IsExpand], [IsPublic], [SortCode], [Description], [CreationTime], [CreateUserId], [LastModifyTime], [LastModifyUserId], [IsEnabled], [IsDeleted], [DeleteTime], [DeleteUserId]) VALUES (N'6e5b779e849e459f957f3abef2a277e6', N'e7e1cfb2856d492faeaadc8e2962ac76', NULL, N'文档管理', NULL, N'/WikiManage/WikiDocument/Index', N'iframe', 1, 0, 0, 1, NULL, CAST(0x0000A6CD017D479E AS DateTime), N'9f2ec079-7d0f-4fe2-90ab-8b09a8302aba', CAST(0x0000A6CD017D5B3B AS DateTime), N'9f2ec079-7d0f-4fe2-90ab-8b09a8302aba', 1, 0, NULL, NULL)
INSERT [dbo].[menu] ([Id], [ParentId], [EnCode], [Name], [Icon], [UrlAddress], [OpenTarget], [IsMenu], [IsExpand], [IsPublic], [SortCode], [Description], [CreationTime], [CreateUserId], [LastModifyTime], [LastModifyUserId], [IsEnabled], [IsDeleted], [DeleteTime], [DeleteUserId]) VALUES (N'a3a4742d-ca39-42ec-b95a-8552a6fae579', N'73FD1267-79BA-4E23-A152-744AF73117E9', NULL, N'区域管理', NULL, N'/SystemManage/Area/Index', N'iframe', 1, 0, 0, 2, NULL, CAST(0x0000A6A500CF5CB3 AS DateTime), NULL, CAST(0x0000A649010C141D AS DateTime), NULL, 1, 0, NULL, NULL)
INSERT [dbo].[menu] ([Id], [ParentId], [EnCode], [Name], [Icon], [UrlAddress], [OpenTarget], [IsMenu], [IsExpand], [IsPublic], [SortCode], [Description], [CreationTime], [CreateUserId], [LastModifyTime], [LastModifyUserId], [IsEnabled], [IsDeleted], [DeleteTime], [DeleteUserId]) VALUES (N'64A1C550-2C61-4A8C-833D-ACD0C012260F', N'462027E0-0848-41DD-BCC3-025DCAE65555', NULL, N'系统菜单', NULL, N'/SystemManage/Module/Index', N'iframe', 1, 0, 0, 7, N'测试', CAST(0x0000A6A500CF5CB3 AS DateTime), NULL, CAST(0x0000A65000B291CC AS DateTime), N'9f2ec079-7d0f-4fe2-90ab-8b09a8302aba', 1, 0, NULL, NULL)
INSERT [dbo].[menu] ([Id], [ParentId], [EnCode], [Name], [Icon], [UrlAddress], [OpenTarget], [IsMenu], [IsExpand], [IsPublic], [SortCode], [Description], [CreationTime], [CreateUserId], [LastModifyTime], [LastModifyUserId], [IsEnabled], [IsDeleted], [DeleteTime], [DeleteUserId]) VALUES (N'38CA5A66-C993-4410-AF95-50489B22939C', N'462027E0-0848-41DD-BCC3-025DCAE65555', NULL, N'用户管理', NULL, N'/SystemManage/User/Index', N'iframe', 1, 0, 0, 4, NULL, CAST(0x0000A6A500CF5CB3 AS DateTime), NULL, CAST(0x0000A643010DEE59 AS DateTime), NULL, 1, 0, NULL, NULL)
INSERT [dbo].[menu] ([Id], [ParentId], [EnCode], [Name], [Icon], [UrlAddress], [OpenTarget], [IsMenu], [IsExpand], [IsPublic], [SortCode], [Description], [CreationTime], [CreateUserId], [LastModifyTime], [LastModifyUserId], [IsEnabled], [IsDeleted], [DeleteTime], [DeleteUserId]) VALUES (N'252229DB-35CA-47AE-BDAE-C9903ED5BA7B', N'462027E0-0848-41DD-BCC3-025DCAE65555', NULL, N'数据字典', NULL, N'/SystemManage/PropertyMapping/Index', N'iframe', 1, 0, 0, 1, NULL, CAST(0x0000A6A500CF5CB3 AS DateTime), NULL, CAST(0x0000A6CD017ABF3C AS DateTime), N'9f2ec079-7d0f-4fe2-90ab-8b09a8302aba', 1, 0, NULL, NULL)
INSERT [dbo].[menu] ([Id], [ParentId], [EnCode], [Name], [Icon], [UrlAddress], [OpenTarget], [IsMenu], [IsExpand], [IsPublic], [SortCode], [Description], [CreationTime], [CreateUserId], [LastModifyTime], [LastModifyUserId], [IsEnabled], [IsDeleted], [DeleteTime], [DeleteUserId]) VALUES (N'91A6CFAD-B2F9-4294-BDAE-76DECF412C6C', N'462027E0-0848-41DD-BCC3-025DCAE65555', NULL, N'角色管理', NULL, N'/SystemManage/role/Index', N'iframe', 1, 0, 0, 2, N'&nbsp;', CAST(0x0000A6A500CF5CB3 AS DateTime), NULL, CAST(0x0000A6AE00B5AF21 AS DateTime), N'9f2ec079-7d0f-4fe2-90ab-8b09a8302aba', 1, 0, NULL, NULL)
INSERT [dbo].[menu] ([Id], [ParentId], [EnCode], [Name], [Icon], [UrlAddress], [OpenTarget], [IsMenu], [IsExpand], [IsPublic], [SortCode], [Description], [CreationTime], [CreateUserId], [LastModifyTime], [LastModifyUserId], [IsEnabled], [IsDeleted], [DeleteTime], [DeleteUserId]) VALUES (N'a7f1f2f73ac74b5ba8421ed9b3840439', N'e7e1cfb2856d492faeaadc8e2962ac76', NULL, N'菜单管理', NULL, N'/WikiManage/WikiMenu/Index', N'iframe', 1, 0, 0, 2, NULL, CAST(0x0000A6CD017D9164 AS DateTime), N'9f2ec079-7d0f-4fe2-90ab-8b09a8302aba', NULL, NULL, 1, 0, NULL, NULL)
INSERT [dbo].[menu] ([Id], [ParentId], [EnCode], [Name], [Icon], [UrlAddress], [OpenTarget], [IsMenu], [IsExpand], [IsPublic], [SortCode], [Description], [CreationTime], [CreateUserId], [LastModifyTime], [LastModifyUserId], [IsEnabled], [IsDeleted], [DeleteTime], [DeleteUserId]) VALUES (N'F298F868-B689-4982-8C8B-9268CBF0308D', N'462027E0-0848-41DD-BCC3-025DCAE65555', NULL, N'用户组管理', NULL, N'/SystemManage/Group/Index', N'iframe', 1, 0, 0, 3, NULL, CAST(0x0000A6A500CF5CB3 AS DateTime), NULL, CAST(0x0000A643010DE701 AS DateTime), NULL, 1, 0, NULL, NULL)
INSERT [dbo].[menu] ([Id], [ParentId], [EnCode], [Name], [Icon], [UrlAddress], [OpenTarget], [IsMenu], [IsExpand], [IsPublic], [SortCode], [Description], [CreationTime], [CreateUserId], [LastModifyTime], [LastModifyUserId], [IsEnabled], [IsDeleted], [DeleteTime], [DeleteUserId]) VALUES (N'96EE855E-8CD2-47FC-A51D-127C131C9FB9', N'73FD1267-79BA-4E23-A152-744AF73117E9', NULL, N'资源管理', NULL, N'/SystemManage/Resource/Index', N'iframe', 1, 0, 0, 3, NULL, CAST(0x0000A6A500CF5CB3 AS DateTime), NULL, CAST(0x0000A649010C27B8 AS DateTime), NULL, 1, 0, NULL, NULL)
/****** Object:  Table [dbo].[group]    Script Date: 01/28/2018 20:05:33 ******/
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
SET IDENTITY_INSERT [dbo].[group] ON
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (6, N'管理部', 2)
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (7, N'人事部', 4)
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (8, N'技术部', 8)
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (9, N'生产部', 16)
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (102, N'财政部', 32)
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (105, N'人力资源部', 64)
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (106, N'信息部', 128)
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (107, N'证券部', 256)
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (108, N'营销部', 512)
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (109, N'安全监察部', 1024)
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (110, N'组织部', 2048)
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (111, N'后勤部', 4096)
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (112, N'保卫部', 8192)
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (113, N'售后服务部', 16384)
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (114, N'采购部', 32768)
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (115, N'督察部', 65536)
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (116, N'研发部', 131072)
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (117, N'企划部', 262144)
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (118, N'总经办', 524288)
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (119, N'公关部', 1048576)
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (120, N'人事部', 2097152)
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (122, N'教务处', 4194304)
SET IDENTITY_INSERT [dbo].[group] OFF
/****** Object:  Table [dbo].[backup]    Script Date: 01/28/2018 20:05:33 ******/
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
/****** Object:  Table [dbo].[article]    Script Date: 01/28/2018 20:05:33 ******/
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
SET IDENTITY_INSERT [dbo].[article] ON
INSERT [dbo].[article] ([id], [title], [content], [inserttime], [edittime], [type], [SortID], [author]) VALUES (1, N'test', N'<p>测试</p>', NULL, CAST(0x0000A87601167E88 AS DateTime), NULL, NULL, N'测试')
INSERT [dbo].[article] ([id], [title], [content], [inserttime], [edittime], [type], [SortID], [author]) VALUES (24, N'红楼梦', N'<p>甄士隐梦幻识通灵	贾雨村风尘怀闺秀</p>', CAST(0xFFFF9EFB00000000 AS DateTime), CAST(0x0000088F00000000 AS DateTime), NULL, NULL, N'曹雪芹')
INSERT [dbo].[article] ([id], [title], [content], [inserttime], [edittime], [type], [SortID], [author]) VALUES (25, N'不能承受的生命之轻', N'<p>小说描写了托马斯与特丽莎、萨丽娜之间的感情生活。但它不是一个男人和两个女人的三角性爱故事，它是一部哲理小说，小说从“永恒轮回”的讨论开始，把读者带入了对一系列问题的思考中，比如轻与重、灵与肉</p>', CAST(0xFFFF742E00000000 AS DateTime), CAST(0xFFFF770800000000 AS DateTime), NULL, NULL, N'米兰昆德拉')
SET IDENTITY_INSERT [dbo].[article] OFF
/****** Object:  Table [dbo].[area]    Script Date: 01/28/2018 20:05:33 ******/
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
SET IDENTITY_INSERT [dbo].[area] ON
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (2, NULL, 1, N'000001', N'湖南省')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (3, N'2', 2, N'000001', N'长沙市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (4, NULL, 1, N'000002', N'江西省')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (14, NULL, 1, N'110000', N'北京')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (15, NULL, 1, N'120000', N'天津')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (16, N'15', 2, N'120100', N'天津市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (17, NULL, 1, N'130000', N'河北省')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (18, N'17', 2, N'130100', N'石家庄市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (19, N'17', 2, N'130200', N'唐山市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (20, N'17', 2, N'130300', N'秦皇岛市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (21, N'17', 2, N'130400', N'邯郸市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (22, N'17', 2, N'130500', N'邢台市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (23, N'17', 2, N'130600', N'保定市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (24, N'17', 2, N'130700', N'张家口市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (25, N'17', 2, N'130800', N'承德市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (26, N'17', 2, N'130900', N'沧州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (27, N'17', 2, N'131000', N'廊坊市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (28, N'17', 2, N'131100', N'衡水市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (29, NULL, 1, N'140000', N'山西省')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (30, N'29', 2, N'140100', N'太原市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (31, N'29', 2, N'140200', N'大同市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (32, N'29', 2, N'140300', N'阳泉市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (33, N'29', 2, N'140400', N'长治市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (34, N'29', 2, N'140500', N'晋城市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (35, N'29', 2, N'140600', N'朔州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (36, N'29', 2, N'140700', N'晋中市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (37, N'29', 2, N'140800', N'运城市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (38, N'29', 2, N'140900', N'忻州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (39, N'29', 2, N'141000', N'临汾市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (40, N'29', 2, N'141100', N'吕梁市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (43, N'14', 2, N'141100', N'北京市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (44, N'4', 2, N'000012', N'南昌市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (45, N'4', 2, N'000017', N'九江市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (46, NULL, 1, N'150000', N'陕西省')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (47, N'46', 2, N'150001', N'西安市')
SET IDENTITY_INSERT [dbo].[area] OFF
/****** Object:  Table [dbo].[action]    Script Date: 01/28/2018 20:05:33 ******/
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
SET IDENTITY_INSERT [dbo].[action] ON
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (15, N'Edit', NULL, N'fa fa-pencil-square-o', 0, N'1', 111, N'User', N'修改')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (16, N'OpenRevisePasswordDialog', NULL, N'fa fa-key', 0, N'1', 111, N'User', N'重置密码')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (17, N'Delete', NULL, N'fa fa-trash-o', 0, N'1', 111, N'User', N'删除')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (18, N'SaveUserGroupMap', NULL, N'1111', 0, N'1', 111, N'User', N'保存用户分组')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (19, N'SaveUserRoleMap', NULL, N'1111', 0, N'1', 111, N'User', N'保存用户角色')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (20, N'GetModels', NULL, N'1111', 0, N'1', 111, N'User', N'111')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (21, N'Details', NULL, N'fa fa-pencil-square-o', 0, N'1', 1, N'User', N'111')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (23, N'Add', NULL, N'fa fa-plus', 0, N'1', 1, N'User', N'新增')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (34, N'Submit', NULL, NULL, 2, N'workflowTest', 2, N'workflow', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (35, N'Audit', NULL, NULL, 2, N'workflowTest', 4, N'workflow', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (39, N'Details', NULL, N'fa fa-pencil-square-o', 0, N'1', 1, N'Action', N'111')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (42, N'Add', NULL, N'fa fa-plus', 0, N'1', 1, N'Area', N'新增')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (46, N'Add', NULL, N'fa fa-plus', 0, N'1', 1, N'Group', N'新增')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (47, N'Details', NULL, N'fa fa-pencil-square-o', 0, N'1', 1, N'Group', N'详情')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (48, N'Edit', NULL, N'fa fa-pencil-square-o', 0, N'1', 111, N'Propertymapping', N'修改')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (49, N'Delete', NULL, N'fa fa-trash-o', 0, N'1', 111, N'Propertymapping', N'删除')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (50, N'Add', NULL, N'fa fa-plus', 0, N'1', 1, N'Propertymapping', N'新增')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (51, N'Details', NULL, N'fa fa-pencil-square-o', 0, N'1', 1, N'Propertymapping', N'详情')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (52, N'Edit', NULL, N'fa fa-pencil-square-o', 0, N'1', 111, N'Resource', N'修改')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (53, N'Delete', NULL, N'fa fa-trash-o', 0, N'1', 111, N'Resource', N'删除')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (54, N'Add', NULL, N'fa fa-plus', 0, N'1', 1, N'Resource', N'新增')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (55, N'Details', NULL, N'fa fa-pencil-square-o', 0, N'1', 1, N'Resource', N'详情')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (56, N'Edit', NULL, N'fa fa-pencil-square-o', 0, N'1', 111, N'Role', N'修改')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (57, N'Delete', NULL, N'fa fa-trash-o', 0, N'1', 111, N'Role', N'删除')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (58, N'Add', NULL, N'fa fa-plus', 0, N'1', 1, N'Role', N'新增')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (59, N'Details', NULL, N'fa fa-pencil-square-o', 0, N'1', 1, N'Role', N'详情')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (62, N'GetPermissionTree', NULL, NULL, 1, N'1', 1, N'UserAuthorize', N'用户角色树加载')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (63, N'GetPermissionGroupTree', NULL, NULL, 1, N'1', 1, N'UserAuthorize', N'用户分组树加载')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (65, N'Add', NULL, NULL, 2, N'wf1', 1, N'workflow', N'新增')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (66, N'Submit', NULL, NULL, 2, N'wf1', 2, N'workflow', N'提交')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (67, N'Audit', NULL, NULL, 2, N'wf1', 4, N'workflow', N'审核')
SET IDENTITY_INSERT [dbo].[action] OFF
