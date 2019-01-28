USE [WideWorldImportersDW]
GO

/****** Object:  Table [Dimension].[City]    Script Date: 28.01.2019 6:17:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Dimension].[City](
	[City Key] [int] NOT NULL,
	[WWI City ID] [int] NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[State Province] [nvarchar](50) NOT NULL,
	[Country] [nvarchar](60) NOT NULL,
	[Continent] [nvarchar](30) NOT NULL,
	[Sales Territory] [nvarchar](50) NOT NULL,
	[Region] [nvarchar](30) NOT NULL,
	[Subregion] [nvarchar](30) NOT NULL,
	[Location] [geography] NULL,
	[Latest Recorded Population] [bigint] NOT NULL,
	[Valid From] [datetime2](7) NOT NULL,
	[Valid To] [datetime2](7) NOT NULL,
	[Lineage Key] [int] NOT NULL,
 CONSTRAINT [PK_Dimension_City] PRIMARY KEY CLUSTERED 
(
	[City Key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [USERDATA]
) ON [USERDATA] TEXTIMAGE_ON [USERDATA]
GO

ALTER TABLE [Dimension].[City] ADD  CONSTRAINT [DF_Dimension_City_City_Key]  DEFAULT (NEXT VALUE FOR [Sequences].[CityKey]) FOR [City Key]
GO

