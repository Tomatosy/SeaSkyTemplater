USE [master]
GO
/****** Object:  Database [DB_SyTest]    Script Date: 2020/12/18 14:09:22 ******/
CREATE DATABASE [DB_SyTest]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DB_SyTest', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\DB_SyTest.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DB_SyTest_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\DB_SyTest_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DB_SyTest] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DB_SyTest].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DB_SyTest] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DB_SyTest] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DB_SyTest] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DB_SyTest] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DB_SyTest] SET ARITHABORT OFF 
GO
ALTER DATABASE [DB_SyTest] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DB_SyTest] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DB_SyTest] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DB_SyTest] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DB_SyTest] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DB_SyTest] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DB_SyTest] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DB_SyTest] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DB_SyTest] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DB_SyTest] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DB_SyTest] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DB_SyTest] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DB_SyTest] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DB_SyTest] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DB_SyTest] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DB_SyTest] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DB_SyTest] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DB_SyTest] SET RECOVERY FULL 
GO
ALTER DATABASE [DB_SyTest] SET  MULTI_USER 
GO
ALTER DATABASE [DB_SyTest] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DB_SyTest] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DB_SyTest] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DB_SyTest] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [DB_SyTest] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DB_SyTest', N'ON'
GO
USE [DB_SyTest]
GO
/****** Object:  Table [dbo].[data_dict]    Script Date: 2020/12/18 14:09:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[data_dict](
	[table_name] [varchar](256) NULL,
	[table_name_c] [varchar](256) NULL,
	[table_name_en] [varchar](256) NULL,
	[field_name] [varchar](256) NULL,
	[field_name_c] [varchar](256) NULL,
	[field_name_en] [varchar](256) NULL,
	[field_sequence] [smallint] NULL,
	[data_type] [varchar](16) NULL,
	[width] [smallint] NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NULL,
	[TableGUID] [uniqueidentifier] NULL,
	[B_PK] [tinyint] NULL,
	[XmlAbreviation] [varchar](16) NULL,
	[ValidForReadAPI] [tinyint] NULL,
	[ValidForCreateAPI] [tinyint] NULL,
	[ValidForUpdateAPI] [tinyint] NULL,
	[DisplayMask] [int] NULL,
	[ReferencedEntityObjectTypeCode] [int] NULL,
	[Description] [varchar](100) NULL,
	[b_sys] [tinyint] NULL,
	[defaultvalue] [varchar](256) NULL,
	[attributetype] [varchar](20) NULL,
	[LookupType] [varchar](30) NULL,
	[DisableFilter] [tinyint] NULL,
	[b_null] [tinyint] NULL,
	[b_identity] [int] NULL,
	[b_identify] [int] NULL,
 CONSTRAINT [PK__data_dict__id__4C6B5938] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Log]    Script Date: 2020/12/18 14:09:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Log](
	[ID] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Thread] [varchar](100) NULL,
	[Level] [varchar](100) NULL,
	[Logger] [varchar](200) NULL,
	[Operator] [int] NULL,
	[Message] [text] NULL,
	[ActionType] [int] NULL,
	[Operand] [varchar](300) NULL,
	[IP] [varchar](20) NULL,
	[MachineName] [varchar](100) NULL,
	[Browser] [varchar](50) NULL,
	[Location] [text] NULL,
	[Exception] [text] NULL,
 CONSTRAINT [PK_tb_Log] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Model]    Script Date: 2020/12/18 14:09:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Model](
	[ModelID] [uniqueidentifier] NOT NULL,
	[ModelCode] [nvarchar](50) NOT NULL,
	[ModelName] [nvarchar](50) NOT NULL,
	[MouldID] [uniqueidentifier] NULL,
	[OrderNum] [int] NULL,
	[IsDelete] [bit] NOT NULL,
	[GmtCreateUser] [nvarchar](50) NOT NULL,
	[GmtCreateDate] [datetime] NOT NULL,
	[GmtUpdateUser] [nvarchar](50) NOT NULL,
	[GmtUpdateDate] [datetime] NOT NULL,
	[TimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ModelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_ModelDetail]    Script Date: 2020/12/18 14:09:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_ModelDetail](
	[ModelDetailID] [uniqueidentifier] NOT NULL,
	[ModelID] [uniqueidentifier] NOT NULL,
	[ColIndex] [int] NOT NULL,
	[ColName] [nvarchar](50) NOT NULL,
	[ColMemo] [nvarchar](max) NULL,
	[ColType] [nvarchar](50) NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[GmtCreateUser] [nvarchar](50) NOT NULL,
	[GmtCreateDate] [datetime] NOT NULL,
	[GmtUpdateUser] [nvarchar](50) NOT NULL,
	[GmtUpdateDate] [datetime] NOT NULL,
	[TimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ModelDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_testDetail]    Script Date: 2020/12/18 14:09:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_testDetail](
	[testDetailID] [uniqueidentifier] NULL,
	[IsDelete] [bit] NULL,
	[dynamicID] [nvarchar](256) NULL
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_tb_Model]    Script Date: 2020/12/18 14:09:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_tb_Model]
 AS  
select ModelID,
ModelCode,
ModelName,
MouldID,
OrderNum,
IsDelete,
GmtCreateUser,
GmtCreateDate,
GmtUpdateUser,
GmtUpdateDate,
TimeStamp
 FROM tb_Model	
GO
/****** Object:  View [dbo].[View_tb_ModelDetail]    Script Date: 2020/12/18 14:09:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[View_tb_ModelDetail]
as
select		md.*,ModelCode,mo.ModelName from tb_ModelDetail md
inner join  tb_Model mo
on md.ModelID=mo.ModelID
GO
SET IDENTITY_INSERT [dbo].[data_dict] ON 
GO
INSERT [dbo].[data_dict] ([table_name], [table_name_c], [table_name_en], [field_name], [field_name_c], [field_name_en], [field_sequence], [data_type], [width], [id], [GUID], [TableGUID], [B_PK], [XmlAbreviation], [ValidForReadAPI], [ValidForCreateAPI], [ValidForUpdateAPI], [DisplayMask], [ReferencedEntityObjectTypeCode], [Description], [b_sys], [defaultvalue], [attributetype], [LookupType], [DisableFilter], [b_null], [b_identity], [b_identify]) VALUES (N'tb_Model', N'模块表', N'ModelModel', N'ModelID', N'主键ID', N'ModelID', 1, N'uniqueidentifier', 0, 1, N'344986e3-bd7f-4ee9-a502-5d693b3ae488', N'5056f230-3299-4e99-9d49-ccd7419067bf', 0, NULL, NULL, NULL, NULL, NULL, NULL, N'(ModelID)主键ID', NULL, N'0', NULL, NULL, 0, 1, 0, NULL)
GO
INSERT [dbo].[data_dict] ([table_name], [table_name_c], [table_name_en], [field_name], [field_name_c], [field_name_en], [field_sequence], [data_type], [width], [id], [GUID], [TableGUID], [B_PK], [XmlAbreviation], [ValidForReadAPI], [ValidForCreateAPI], [ValidForUpdateAPI], [DisplayMask], [ReferencedEntityObjectTypeCode], [Description], [b_sys], [defaultvalue], [attributetype], [LookupType], [DisableFilter], [b_null], [b_identity], [b_identify]) VALUES (N'tb_Model', N'模块表', N'ModelModel', N'ModelCode', N'主键编号', N'ModelCode', 2, N'nvarchar(50)', 50, 2, N'6d6f7c60-5d65-4182-b437-71c689e6cb35', N'5056f230-3299-4e99-9d49-ccd7419067bf', 0, NULL, NULL, NULL, NULL, NULL, NULL, N'(ModelCode)主键编号', NULL, N'0', NULL, NULL, 0, 1, 0, NULL)
GO
INSERT [dbo].[data_dict] ([table_name], [table_name_c], [table_name_en], [field_name], [field_name_c], [field_name_en], [field_sequence], [data_type], [width], [id], [GUID], [TableGUID], [B_PK], [XmlAbreviation], [ValidForReadAPI], [ValidForCreateAPI], [ValidForUpdateAPI], [DisplayMask], [ReferencedEntityObjectTypeCode], [Description], [b_sys], [defaultvalue], [attributetype], [LookupType], [DisableFilter], [b_null], [b_identity], [b_identify]) VALUES (N'tb_Model', N'模块表', N'ModelModel', N'ModelName', N'项目名称', N'ModelName', 3, N'nvarchar(50)', 50, 3, N'e75a2541-a8f5-4879-a1a7-0f09f09a8632', N'5056f230-3299-4e99-9d49-ccd7419067bf', 0, NULL, NULL, NULL, NULL, NULL, NULL, N'(ModelName)项目名称', NULL, N'0', NULL, NULL, 0, 1, 0, NULL)
GO
INSERT [dbo].[data_dict] ([table_name], [table_name_c], [table_name_en], [field_name], [field_name_c], [field_name_en], [field_sequence], [data_type], [width], [id], [GUID], [TableGUID], [B_PK], [XmlAbreviation], [ValidForReadAPI], [ValidForCreateAPI], [ValidForUpdateAPI], [DisplayMask], [ReferencedEntityObjectTypeCode], [Description], [b_sys], [defaultvalue], [attributetype], [LookupType], [DisableFilter], [b_null], [b_identity], [b_identify]) VALUES (N'tb_Model', N'模块表', N'ModelModel', N'MouldID', N'明细表模板ID（需要明细表时）', N'MouldID', 4, N'uniqueidentifier', 0, 4, N'e5f6cf08-d39e-4930-b0fe-fabebdfa6b69', N'5056f230-3299-4e99-9d49-ccd7419067bf', 0, NULL, NULL, NULL, NULL, NULL, NULL, N'(MouldID)明细表模板ID（需要明细表时）', NULL, N'0', NULL, NULL, 0, 0, 0, NULL)
GO
INSERT [dbo].[data_dict] ([table_name], [table_name_c], [table_name_en], [field_name], [field_name_c], [field_name_en], [field_sequence], [data_type], [width], [id], [GUID], [TableGUID], [B_PK], [XmlAbreviation], [ValidForReadAPI], [ValidForCreateAPI], [ValidForUpdateAPI], [DisplayMask], [ReferencedEntityObjectTypeCode], [Description], [b_sys], [defaultvalue], [attributetype], [LookupType], [DisableFilter], [b_null], [b_identity], [b_identify]) VALUES (N'tb_Model', N'模块表', N'ModelModel', N'OrderNum', N'排序号', N'OrderNum', 5, N'int', 0, 5, N'5169db87-cfa1-4b1c-8aae-34233c3c666f', N'5056f230-3299-4e99-9d49-ccd7419067bf', 0, NULL, NULL, NULL, NULL, NULL, NULL, N'(OrderNum)排序号', NULL, N'0', NULL, NULL, 0, 0, 0, NULL)
GO
INSERT [dbo].[data_dict] ([table_name], [table_name_c], [table_name_en], [field_name], [field_name_c], [field_name_en], [field_sequence], [data_type], [width], [id], [GUID], [TableGUID], [B_PK], [XmlAbreviation], [ValidForReadAPI], [ValidForCreateAPI], [ValidForUpdateAPI], [DisplayMask], [ReferencedEntityObjectTypeCode], [Description], [b_sys], [defaultvalue], [attributetype], [LookupType], [DisableFilter], [b_null], [b_identity], [b_identify]) VALUES (N'tb_Model', N'模块表', N'ModelModel', N'IsDelete', N'是否删除', N'IsDelete', 6, N'bit', 0, 6, N'1d886197-b809-43f8-9ea8-c33b459027af', N'5056f230-3299-4e99-9d49-ccd7419067bf', 0, NULL, NULL, NULL, NULL, NULL, NULL, N'(IsDelete)是否删除', NULL, N'0', NULL, NULL, 0, 1, 0, NULL)
GO
INSERT [dbo].[data_dict] ([table_name], [table_name_c], [table_name_en], [field_name], [field_name_c], [field_name_en], [field_sequence], [data_type], [width], [id], [GUID], [TableGUID], [B_PK], [XmlAbreviation], [ValidForReadAPI], [ValidForCreateAPI], [ValidForUpdateAPI], [DisplayMask], [ReferencedEntityObjectTypeCode], [Description], [b_sys], [defaultvalue], [attributetype], [LookupType], [DisableFilter], [b_null], [b_identity], [b_identify]) VALUES (N'tb_Model', N'模块表', N'ModelModel', N'GmtCreateUser', N'创建人', N'GmtCreateUser', 7, N'nvarchar(50)', 50, 7, N'8763db35-3dc7-4aa4-9c91-2468a0631b47', N'5056f230-3299-4e99-9d49-ccd7419067bf', 0, NULL, NULL, NULL, NULL, NULL, NULL, N'(GmtCreateUser)创建人', NULL, N'0', NULL, NULL, 0, 1, 0, NULL)
GO
INSERT [dbo].[data_dict] ([table_name], [table_name_c], [table_name_en], [field_name], [field_name_c], [field_name_en], [field_sequence], [data_type], [width], [id], [GUID], [TableGUID], [B_PK], [XmlAbreviation], [ValidForReadAPI], [ValidForCreateAPI], [ValidForUpdateAPI], [DisplayMask], [ReferencedEntityObjectTypeCode], [Description], [b_sys], [defaultvalue], [attributetype], [LookupType], [DisableFilter], [b_null], [b_identity], [b_identify]) VALUES (N'tb_Model', N'模块表', N'ModelModel', N'GmtCreateDate', N'创建时间', N'GmtCreateDate', 8, N'datetime', 0, 8, N'4afe1876-84bb-4529-8aff-43e448d169c5', N'5056f230-3299-4e99-9d49-ccd7419067bf', 0, NULL, NULL, NULL, NULL, NULL, NULL, N'(GmtCreateDate)创建时间', NULL, N'0', NULL, NULL, 0, 1, 0, NULL)
GO
INSERT [dbo].[data_dict] ([table_name], [table_name_c], [table_name_en], [field_name], [field_name_c], [field_name_en], [field_sequence], [data_type], [width], [id], [GUID], [TableGUID], [B_PK], [XmlAbreviation], [ValidForReadAPI], [ValidForCreateAPI], [ValidForUpdateAPI], [DisplayMask], [ReferencedEntityObjectTypeCode], [Description], [b_sys], [defaultvalue], [attributetype], [LookupType], [DisableFilter], [b_null], [b_identity], [b_identify]) VALUES (N'tb_Model', N'模块表', N'ModelModel', N'GmtUpdateUser', N'修改人', N'GmtUpdateUser', 9, N'nvarchar(50)', 50, 9, N'5334bf99-4073-4cad-9bd8-5e6745949614', N'5056f230-3299-4e99-9d49-ccd7419067bf', 0, NULL, NULL, NULL, NULL, NULL, NULL, N'(GmtUpdateUser)修改人', NULL, N'0', NULL, NULL, 0, 1, 0, NULL)
GO
INSERT [dbo].[data_dict] ([table_name], [table_name_c], [table_name_en], [field_name], [field_name_c], [field_name_en], [field_sequence], [data_type], [width], [id], [GUID], [TableGUID], [B_PK], [XmlAbreviation], [ValidForReadAPI], [ValidForCreateAPI], [ValidForUpdateAPI], [DisplayMask], [ReferencedEntityObjectTypeCode], [Description], [b_sys], [defaultvalue], [attributetype], [LookupType], [DisableFilter], [b_null], [b_identity], [b_identify]) VALUES (N'tb_Model', N'模块表', N'ModelModel', N'GmtUpdateDate', N'修改时间', N'GmtUpdateDate', 10, N'datetime', 0, 10, N'15337994-c93d-4301-8e5e-16ec61ca4342', N'5056f230-3299-4e99-9d49-ccd7419067bf', 0, NULL, NULL, NULL, NULL, NULL, NULL, N'(GmtUpdateDate)修改时间', NULL, N'0', NULL, NULL, 0, 1, 0, NULL)
GO
INSERT [dbo].[data_dict] ([table_name], [table_name_c], [table_name_en], [field_name], [field_name_c], [field_name_en], [field_sequence], [data_type], [width], [id], [GUID], [TableGUID], [B_PK], [XmlAbreviation], [ValidForReadAPI], [ValidForCreateAPI], [ValidForUpdateAPI], [DisplayMask], [ReferencedEntityObjectTypeCode], [Description], [b_sys], [defaultvalue], [attributetype], [LookupType], [DisableFilter], [b_null], [b_identity], [b_identify]) VALUES (N'tb_Model', N'模块表', N'ModelModel', N'TimeStamp', N'版本号', N'TimeStamp', 11, N'timestamp', 0, 11, N'239374fe-9013-4dbe-bda7-fb8481c9f8b2', N'5056f230-3299-4e99-9d49-ccd7419067bf', 0, NULL, NULL, NULL, NULL, NULL, NULL, N'(TimeStamp)版本号', NULL, N'0', NULL, NULL, 0, 1, 0, NULL)
GO
INSERT [dbo].[data_dict] ([table_name], [table_name_c], [table_name_en], [field_name], [field_name_c], [field_name_en], [field_sequence], [data_type], [width], [id], [GUID], [TableGUID], [B_PK], [XmlAbreviation], [ValidForReadAPI], [ValidForCreateAPI], [ValidForUpdateAPI], [DisplayMask], [ReferencedEntityObjectTypeCode], [Description], [b_sys], [defaultvalue], [attributetype], [LookupType], [DisableFilter], [b_null], [b_identity], [b_identify]) VALUES (N'tb_ModelDetail', N'模块明细表', N'ModelDetailModel', N'ModelDetailID', N'主键ID', N'ModelDetailID', 1, N'uniqueidentifier', 0, 12, N'b6179af3-371f-4f87-9c76-b438b04bf68f', N'4a76c3b9-3587-46b7-9c1a-e6c8de2b38bd', 0, NULL, NULL, NULL, NULL, NULL, NULL, N'(ModelDetailID)主键ID', NULL, N'0', NULL, NULL, 0, 1, 0, NULL)
GO
INSERT [dbo].[data_dict] ([table_name], [table_name_c], [table_name_en], [field_name], [field_name_c], [field_name_en], [field_sequence], [data_type], [width], [id], [GUID], [TableGUID], [B_PK], [XmlAbreviation], [ValidForReadAPI], [ValidForCreateAPI], [ValidForUpdateAPI], [DisplayMask], [ReferencedEntityObjectTypeCode], [Description], [b_sys], [defaultvalue], [attributetype], [LookupType], [DisableFilter], [b_null], [b_identity], [b_identify]) VALUES (N'tb_ModelDetail', N'模块明细表', N'ModelDetailModel', N'ModelID', N'项目编号', N'ModelID', 2, N'uniqueidentifier', 0, 13, N'4c683afd-3e20-4920-863c-ac01ad92b06b', N'4a76c3b9-3587-46b7-9c1a-e6c8de2b38bd', 0, NULL, NULL, NULL, NULL, NULL, NULL, N'(ModelID)项目编号', NULL, N'0', NULL, NULL, 0, 1, 0, NULL)
GO
INSERT [dbo].[data_dict] ([table_name], [table_name_c], [table_name_en], [field_name], [field_name_c], [field_name_en], [field_sequence], [data_type], [width], [id], [GUID], [TableGUID], [B_PK], [XmlAbreviation], [ValidForReadAPI], [ValidForCreateAPI], [ValidForUpdateAPI], [DisplayMask], [ReferencedEntityObjectTypeCode], [Description], [b_sys], [defaultvalue], [attributetype], [LookupType], [DisableFilter], [b_null], [b_identity], [b_identify]) VALUES (N'tb_ModelDetail', N'模块明细表', N'ModelDetailModel', N'ColIndex', N'列序号', N'ColIndex', 3, N'int', 0, 14, N'0d343515-ab8d-4bff-b6d7-82bf46f57247', N'4a76c3b9-3587-46b7-9c1a-e6c8de2b38bd', 0, NULL, NULL, NULL, NULL, NULL, NULL, N'(ColIndex)列序号', NULL, N'0', NULL, NULL, 0, 1, 0, NULL)
GO
INSERT [dbo].[data_dict] ([table_name], [table_name_c], [table_name_en], [field_name], [field_name_c], [field_name_en], [field_sequence], [data_type], [width], [id], [GUID], [TableGUID], [B_PK], [XmlAbreviation], [ValidForReadAPI], [ValidForCreateAPI], [ValidForUpdateAPI], [DisplayMask], [ReferencedEntityObjectTypeCode], [Description], [b_sys], [defaultvalue], [attributetype], [LookupType], [DisableFilter], [b_null], [b_identity], [b_identify]) VALUES (N'tb_ModelDetail', N'模块明细表', N'ModelDetailModel', N'ColName', N'列名', N'ColName', 4, N'nvarchar(50)', 50, 15, N'be7a2e11-f089-4a47-ac27-c7da1f4d3147', N'4a76c3b9-3587-46b7-9c1a-e6c8de2b38bd', 0, NULL, NULL, NULL, NULL, NULL, NULL, N'(ColName)列名', NULL, N'0', NULL, NULL, 0, 1, 0, NULL)
GO
INSERT [dbo].[data_dict] ([table_name], [table_name_c], [table_name_en], [field_name], [field_name_c], [field_name_en], [field_sequence], [data_type], [width], [id], [GUID], [TableGUID], [B_PK], [XmlAbreviation], [ValidForReadAPI], [ValidForCreateAPI], [ValidForUpdateAPI], [DisplayMask], [ReferencedEntityObjectTypeCode], [Description], [b_sys], [defaultvalue], [attributetype], [LookupType], [DisableFilter], [b_null], [b_identity], [b_identify]) VALUES (N'tb_ModelDetail', N'模块明细表', N'ModelDetailModel', N'ColMemo', N'文本备注', N'ColMemo', 5, N'nvarchar(max)', 4000, 16, N'3298c660-1a01-4367-86e6-452353f4daa8', N'4a76c3b9-3587-46b7-9c1a-e6c8de2b38bd', 0, NULL, NULL, NULL, NULL, NULL, NULL, N'(ColMemo)文本备注', NULL, N'0', NULL, NULL, 0, 0, 0, NULL)
GO
INSERT [dbo].[data_dict] ([table_name], [table_name_c], [table_name_en], [field_name], [field_name_c], [field_name_en], [field_sequence], [data_type], [width], [id], [GUID], [TableGUID], [B_PK], [XmlAbreviation], [ValidForReadAPI], [ValidForCreateAPI], [ValidForUpdateAPI], [DisplayMask], [ReferencedEntityObjectTypeCode], [Description], [b_sys], [defaultvalue], [attributetype], [LookupType], [DisableFilter], [b_null], [b_identity], [b_identify]) VALUES (N'tb_ModelDetail', N'模块明细表', N'ModelDetailModel', N'ColType', N'字段类型（文本、数字、日期、布尔、下拉框、附件）', N'ColType', 6, N'nvarchar(50)', 50, 17, N'a580955d-315c-4aa9-8814-486e024b8e1e', N'4a76c3b9-3587-46b7-9c1a-e6c8de2b38bd', 0, NULL, NULL, NULL, NULL, NULL, NULL, N'(ColType)字段类型（文本、数字、日期、布尔、下拉框、附件）', NULL, N'0', NULL, NULL, 0, 1, 0, NULL)
GO
INSERT [dbo].[data_dict] ([table_name], [table_name_c], [table_name_en], [field_name], [field_name_c], [field_name_en], [field_sequence], [data_type], [width], [id], [GUID], [TableGUID], [B_PK], [XmlAbreviation], [ValidForReadAPI], [ValidForCreateAPI], [ValidForUpdateAPI], [DisplayMask], [ReferencedEntityObjectTypeCode], [Description], [b_sys], [defaultvalue], [attributetype], [LookupType], [DisableFilter], [b_null], [b_identity], [b_identify]) VALUES (N'tb_ModelDetail', N'模块明细表', N'ModelDetailModel', N'IsDelete', N'是否删除', N'IsDelete', 7, N'bit', 0, 18, N'de165b62-f9c5-45c1-8989-7f7ee167f50b', N'4a76c3b9-3587-46b7-9c1a-e6c8de2b38bd', 0, NULL, NULL, NULL, NULL, NULL, NULL, N'(IsDelete)是否删除', NULL, N'0', NULL, NULL, 0, 1, 0, NULL)
GO
INSERT [dbo].[data_dict] ([table_name], [table_name_c], [table_name_en], [field_name], [field_name_c], [field_name_en], [field_sequence], [data_type], [width], [id], [GUID], [TableGUID], [B_PK], [XmlAbreviation], [ValidForReadAPI], [ValidForCreateAPI], [ValidForUpdateAPI], [DisplayMask], [ReferencedEntityObjectTypeCode], [Description], [b_sys], [defaultvalue], [attributetype], [LookupType], [DisableFilter], [b_null], [b_identity], [b_identify]) VALUES (N'tb_ModelDetail', N'模块明细表', N'ModelDetailModel', N'GmtCreateUser', N'创建人', N'GmtCreateUser', 8, N'nvarchar(50)', 50, 19, N'395951a1-cbdb-47e6-bb3b-e021f23f07ec', N'4a76c3b9-3587-46b7-9c1a-e6c8de2b38bd', 0, NULL, NULL, NULL, NULL, NULL, NULL, N'(GmtCreateUser)创建人', NULL, N'0', NULL, NULL, 0, 1, 0, NULL)
GO
INSERT [dbo].[data_dict] ([table_name], [table_name_c], [table_name_en], [field_name], [field_name_c], [field_name_en], [field_sequence], [data_type], [width], [id], [GUID], [TableGUID], [B_PK], [XmlAbreviation], [ValidForReadAPI], [ValidForCreateAPI], [ValidForUpdateAPI], [DisplayMask], [ReferencedEntityObjectTypeCode], [Description], [b_sys], [defaultvalue], [attributetype], [LookupType], [DisableFilter], [b_null], [b_identity], [b_identify]) VALUES (N'tb_ModelDetail', N'模块明细表', N'ModelDetailModel', N'GmtCreateDate', N'创建时间', N'GmtCreateDate', 9, N'datetime', 0, 20, N'35937f43-b212-48da-b70d-48b28cba92f7', N'4a76c3b9-3587-46b7-9c1a-e6c8de2b38bd', 0, NULL, NULL, NULL, NULL, NULL, NULL, N'(GmtCreateDate)创建时间', NULL, N'0', NULL, NULL, 0, 1, 0, NULL)
GO
INSERT [dbo].[data_dict] ([table_name], [table_name_c], [table_name_en], [field_name], [field_name_c], [field_name_en], [field_sequence], [data_type], [width], [id], [GUID], [TableGUID], [B_PK], [XmlAbreviation], [ValidForReadAPI], [ValidForCreateAPI], [ValidForUpdateAPI], [DisplayMask], [ReferencedEntityObjectTypeCode], [Description], [b_sys], [defaultvalue], [attributetype], [LookupType], [DisableFilter], [b_null], [b_identity], [b_identify]) VALUES (N'tb_ModelDetail', N'模块明细表', N'ModelDetailModel', N'GmtUpdateUser', N'修改人', N'GmtUpdateUser', 10, N'nvarchar(50)', 50, 21, N'04a35841-3407-4a49-9d9c-90d0fba3849f', N'4a76c3b9-3587-46b7-9c1a-e6c8de2b38bd', 0, NULL, NULL, NULL, NULL, NULL, NULL, N'(GmtUpdateUser)修改人', NULL, N'0', NULL, NULL, 0, 1, 0, NULL)
GO
INSERT [dbo].[data_dict] ([table_name], [table_name_c], [table_name_en], [field_name], [field_name_c], [field_name_en], [field_sequence], [data_type], [width], [id], [GUID], [TableGUID], [B_PK], [XmlAbreviation], [ValidForReadAPI], [ValidForCreateAPI], [ValidForUpdateAPI], [DisplayMask], [ReferencedEntityObjectTypeCode], [Description], [b_sys], [defaultvalue], [attributetype], [LookupType], [DisableFilter], [b_null], [b_identity], [b_identify]) VALUES (N'tb_ModelDetail', N'模块明细表', N'ModelDetailModel', N'GmtUpdateDate', N'修改时间', N'GmtUpdateDate', 11, N'datetime', 0, 22, N'c5060919-098f-45b9-ad33-522491bdbbc1', N'4a76c3b9-3587-46b7-9c1a-e6c8de2b38bd', 0, NULL, NULL, NULL, NULL, NULL, NULL, N'(GmtUpdateDate)修改时间', NULL, N'0', NULL, NULL, 0, 1, 0, NULL)
GO
INSERT [dbo].[data_dict] ([table_name], [table_name_c], [table_name_en], [field_name], [field_name_c], [field_name_en], [field_sequence], [data_type], [width], [id], [GUID], [TableGUID], [B_PK], [XmlAbreviation], [ValidForReadAPI], [ValidForCreateAPI], [ValidForUpdateAPI], [DisplayMask], [ReferencedEntityObjectTypeCode], [Description], [b_sys], [defaultvalue], [attributetype], [LookupType], [DisableFilter], [b_null], [b_identity], [b_identify]) VALUES (N'tb_ModelDetail', N'模块明细表', N'ModelDetailModel', N'TimeStamp', N'版本号', N'TimeStamp', 12, N'timestamp', 0, 23, N'55d62682-8cfc-4809-906f-790866f16b1d', N'4a76c3b9-3587-46b7-9c1a-e6c8de2b38bd', 0, NULL, NULL, NULL, NULL, NULL, NULL, N'(TimeStamp)版本号', NULL, N'0', NULL, NULL, 0, 1, 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[data_dict] OFF
GO
INSERT [dbo].[tb_testDetail] ([testDetailID], [IsDelete], [dynamicID]) VALUES (N'42066a39-fd5b-4328-aa98-e75b36ba04ab', 0, N'414485f4-694b-4f7a-ab57-e6f5018f6869')
GO
ALTER TABLE [dbo].[data_dict] ADD  CONSTRAINT [DF_data_dict_DisableFilter]  DEFAULT ((0)) FOR [DisableFilter]
GO
ALTER TABLE [dbo].[data_dict] ADD  CONSTRAINT [DF_data_dict_b_identity]  DEFAULT ((0)) FOR [b_identity]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据库表的名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'data_dict', @level2type=N'COLUMN',@level2name=N'table_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'表中文名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'data_dict', @level2type=N'COLUMN',@level2name=N'table_name_c'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'程序表的英文名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'data_dict', @level2type=N'COLUMN',@level2name=N'table_name_en'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据库表字段英文名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'data_dict', @level2type=N'COLUMN',@level2name=N'field_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字段中文名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'data_dict', @level2type=N'COLUMN',@level2name=N'field_name_c'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'程序字段英文名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'data_dict', @level2type=N'COLUMN',@level2name=N'field_name_en'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'data_dict', @level2type=N'COLUMN',@level2name=N'field_sequence'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'data_dict', @level2type=N'COLUMN',@level2name=N'data_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'宽度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'data_dict', @level2type=N'COLUMN',@level2name=N'width'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'GUID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'data_dict', @level2type=N'COLUMN',@level2name=N'GUID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'表GUID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'data_dict', @level2type=N'COLUMN',@level2name=N'TableGUID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'data_dict', @level2type=N'COLUMN',@level2name=N'B_PK'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'XML' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'data_dict', @level2type=N'COLUMN',@level2name=N'XmlAbreviation'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'查询校验' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'data_dict', @level2type=N'COLUMN',@level2name=N'ValidForReadAPI'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'新增校验' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'data_dict', @level2type=N'COLUMN',@level2name=N'ValidForCreateAPI'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新校验' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'data_dict', @level2type=N'COLUMN',@level2name=N'ValidForUpdateAPI'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示掩码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'data_dict', @level2type=N'COLUMN',@level2name=N'DisplayMask'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'参考实体对象' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'data_dict', @level2type=N'COLUMN',@level2name=N'ReferencedEntityObjectTypeCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'data_dict', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否系统' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'data_dict', @level2type=N'COLUMN',@level2name=N'b_sys'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'默认值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'data_dict', @level2type=N'COLUMN',@level2name=N'defaultvalue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'属性值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'data_dict', @level2type=N'COLUMN',@level2name=N'attributetype'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'查询类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'data_dict', @level2type=N'COLUMN',@level2name=N'LookupType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'禁止参与过滤' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'data_dict', @level2type=N'COLUMN',@level2name=N'DisableFilter'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'允许为空' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'data_dict', @level2type=N'COLUMN',@level2name=N'b_null'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据字典' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'data_dict'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(ModelID)主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Model', @level2type=N'COLUMN',@level2name=N'ModelID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(ModelCode)主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Model', @level2type=N'COLUMN',@level2name=N'ModelCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(ModelName)项目名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Model', @level2type=N'COLUMN',@level2name=N'ModelName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(MouldID)明细表模板ID（需要明细表时）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Model', @level2type=N'COLUMN',@level2name=N'MouldID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(OrderNum)排序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Model', @level2type=N'COLUMN',@level2name=N'OrderNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(IsDelete)是否删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Model', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(GmtCreateUser)创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Model', @level2type=N'COLUMN',@level2name=N'GmtCreateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(GmtCreateDate)创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Model', @level2type=N'COLUMN',@level2name=N'GmtCreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(GmtUpdateUser)修改人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Model', @level2type=N'COLUMN',@level2name=N'GmtUpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(GmtUpdateDate)修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Model', @level2type=N'COLUMN',@level2name=N'GmtUpdateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(TimeStamp)版本号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Model', @level2type=N'COLUMN',@level2name=N'TimeStamp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(ModelModel)模块表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Model'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(ModelDetailID)主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ModelDetail', @level2type=N'COLUMN',@level2name=N'ModelDetailID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(ModelID)项目编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ModelDetail', @level2type=N'COLUMN',@level2name=N'ModelID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(ColIndex)列序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ModelDetail', @level2type=N'COLUMN',@level2name=N'ColIndex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(ColName)列名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ModelDetail', @level2type=N'COLUMN',@level2name=N'ColName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(ColMemo)文本备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ModelDetail', @level2type=N'COLUMN',@level2name=N'ColMemo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(ColType)字段类型（文本、数字、日期、布尔、下拉框、附件）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ModelDetail', @level2type=N'COLUMN',@level2name=N'ColType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(IsDelete)是否删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ModelDetail', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(GmtCreateUser)创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ModelDetail', @level2type=N'COLUMN',@level2name=N'GmtCreateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(GmtCreateDate)创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ModelDetail', @level2type=N'COLUMN',@level2name=N'GmtCreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(GmtUpdateUser)修改人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ModelDetail', @level2type=N'COLUMN',@level2name=N'GmtUpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(GmtUpdateDate)修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ModelDetail', @level2type=N'COLUMN',@level2name=N'GmtUpdateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(TimeStamp)版本号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ModelDetail', @level2type=N'COLUMN',@level2name=N'TimeStamp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(ModelDetailModel)模块明细表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ModelDetail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_testDetail', @level2type=N'COLUMN',@level2name=N'testDetailID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_testDetail', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'测试子表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_testDetail'
GO
USE [master]
GO
ALTER DATABASE [DB_SyTest] SET  READ_WRITE 
GO
