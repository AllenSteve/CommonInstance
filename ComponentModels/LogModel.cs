using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentModels
{
    /// <summary>
    /// 日志文件模型类
    /// </summary>
    public class LogModel
    {
        public string interfaceUrl { get; set; }

        public string paramsStr { get; set; }

        public string interfaceName { get; set; }

        public string createTime { get; set; }

        public string imei { get; set; }

        public string appType { get; set; }

        public string status { get; set; }

        public string appv { get; set; }

        public string soufunId { get; set; }

        public LogModel()
        {
            this.appType = "3";
            this.status = "1";
        }
    }
}

/*
 * 数据库建表脚本；
 * 
 USE [workDB]
GO

USE [workDB]
GO

-- Object:  Table [dbo].[CallInterfaceLog]    Script Date: 11/05/2015 20:32:27 --
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CallInterfaceLog](
	[ID] [INT] IDENTITY(1,1) NOT NULL,
	[SoufunId] [BIGINT] NULL,
	[OrderId] [NVARCHAR](30) NULL,
	[InterfaceName] [NVARCHAR](50) NULL,
	[ResponseStartTime] [DATETIME2](7) NOT NULL,
	[ResponseEndTime] [DATETIME2](7) NOT NULL,
	[Imei] [NVARCHAR](200) NULL,
	[Appv] [NVARCHAR](20) NULL,
	[AppType] [INT] NULL,
	[CreateTime] [DATETIME2](7) NOT NULL,
	[InterfaceUrl] [NVARCHAR](500) NULL,
	[ParamsStr] [NVARCHAR](2000) NULL,
	[IsDel] [INT] NULL,
	[Status] [INT] NULL,
 CONSTRAINT [PK_CallInterfaceLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CallInterfaceLog](
	[ID] [INT] IDENTITY(1,1) NOT NULL,
	[SoufunId] [BIGINT] NULL,
	[OrderId] [NVARCHAR](30) NULL,
	[InterfaceName] [NVARCHAR](50) NULL,
	[ResponseStartTime] [DATETIME2](7) NOT NULL,
	[ResponseEndTime] [DATETIME2](7) NOT NULL,
	[Imei] [NVARCHAR](200) NULL,
	[Appv] [NVARCHAR](20) NULL,
	[AppType] [INT] NULL,
	[CreateTime] [DATETIME2](7) NOT NULL,
	[InterfaceUrl] [NVARCHAR](500) NULL,
	[ParamsStr] [NVARCHAR](2000) NULL,
	[IsDel] [INT] NULL,
	[Status] [INT] NULL,
 CONSTRAINT [PK_CallInterfaceLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



 */
