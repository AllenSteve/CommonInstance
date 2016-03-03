/****** Object:  Table [dbo].[N_Order_Base]    Script Date: 03/03/2016 09:21:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[N_Order_Base](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [nvarchar](50) NOT NULL,
	[SourceId] [int] NOT NULL,
	[SourceName] [nvarchar](20) NOT NULL,
	[OrderType] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[StatusName] [nvarchar](10) NOT NULL,
	[UserRank] [int] NOT NULL,
	[CityId] [int] NOT NULL,
	[CityName] [nvarchar](10) NOT NULL,
	[TrueName] [nvarchar](50) NOT NULL,
	[SoufunId] [bigint] NOT NULL,
	[SoufunName] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[IdentityNumber] [nvarchar](20) NOT NULL,
	[Sex] [int] NOT NULL,
	[Age] [int] NOT NULL,
	[FamilyType] [int] NOT NULL,
	[DistrictId] [int] NOT NULL,
	[DistrictName] [nvarchar](50) NOT NULL,
	[EstateId] [bigint] NOT NULL,
	[EstateName] [nvarchar](50) NOT NULL,
	[BuildingNO] [nvarchar](20) NOT NULL,
	[UnitNO] [nvarchar](20) NOT NULL,
	[RoomNO] [nvarchar](20) NOT NULL,
	[Area] [decimal](18, 4) NOT NULL,
	[HouseStatus] [int] NOT NULL,
	[HouseUse] [int] NOT NULL,
	[HouseType] [int] NOT NULL,
	[PreferStyle] [int] NOT NULL,
	[DeliveryTime] [datetime2](7) NOT NULL,
	[GrabOrderDate] [datetime2](7) NOT NULL,
	[SignContractDate] [datetime2](7) NOT NULL,
	[CompletionDate] [datetime2](7) NULL,
	[IsEarnest] [int] NOT NULL,
	[CreateTime] [datetime2](7) NOT NULL,
	[IsDel] [int] NOT NULL,
	[CustomerDel] [int] NOT NULL,
	[StartConstructTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK__Order_Ba__C3905BCF6DA2FB28] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'来源编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'SourceId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'来源名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'SourceName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单类型，0搜房直销，1合作联盟' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'OrderType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'StatusId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'StatusName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户评级，-2VIP,0A+,1A,2B,3C,4D,5E,-1未选择' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'UserRank'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'城市编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'CityId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'城市名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'CityName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业主姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'TrueName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业主通行证ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'SoufunId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业主通行证名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'SoufunName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业主电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'Phone'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业主身份证' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'IdentityNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'性别，-1未选择，0女，1男' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'Sex'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'年龄段，0未选择，1－25以下，2－25到34，3－35到44，4－45到54，5－55以上' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'Age'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'家庭类型，0未选择，1单身，2两人，3三人，4四人，5其他' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'FamilyType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业主行政区编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'DistrictId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'行政区名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'DistrictName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'楼盘号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'EstateId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'楼盘名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'EstateName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'楼栋号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'BuildingNO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单元号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'UnitNO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'房室号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'RoomNO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'面积，单位平米' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'Area'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'房屋状况，1新房，2老房' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'HouseStatus'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'房屋用途，1自主，2投资，3度假，0未选择' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'HouseUse'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'户型，1小户型,2别墅,3复式,4一居室,5二居室,6三居室,7四居室,8跃层' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'HouseType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'偏好风格，0未选择，现代简约1,田园风格2,中式古典3,西式古典4,欧美风情5,东南亚风格6,混合型风格7,日韩风格8,中式风格9,简欧风格10,新古典风格11,混搭风格12,地中海风格13,其它14' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'PreferStyle'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'交房时间，默认1900-01-01' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'DeliveryTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'抢到订单时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'GrabOrderDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'签约时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'SignContractDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单竣工时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'CompletionDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是定金客户：0不是，1是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'IsEarnest'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'伪删除标记,0否,1是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base', @level2type=N'COLUMN',@level2name=N'IsDel'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'新模式：订单基础信息表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'N_Order_Base'
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF__Order_Bas__Sourc__6F8B439A]  DEFAULT ((0)) FOR [SourceId]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF__Order_Bas__Sourc__707F67D3]  DEFAULT ('') FOR [SourceName]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF_N_Order_Base_OrderType]  DEFAULT ((0)) FOR [OrderType]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF__Order_Bas__Statu__71738C0C]  DEFAULT ((0)) FOR [StatusId]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF__Order_Bas__Statu__7267B045]  DEFAULT ('') FOR [StatusName]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF_N_Order_Base_UserRank]  DEFAULT ((-1)) FOR [UserRank]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF__Order_Bas__CityI__735BD47E]  DEFAULT ((0)) FOR [CityId]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF__Order_Bas__CityN__744FF8B7]  DEFAULT ('') FOR [CityName]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF__Order_Bas__TrueN__75441CF0]  DEFAULT ('') FOR [TrueName]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF__Order_Bas__Soufu__76384129]  DEFAULT ((0)) FOR [SoufunId]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF__Order_Bas__Soufu__772C6562]  DEFAULT ('') FOR [SoufunName]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF__Order_Bas__Phone__7820899B]  DEFAULT ('') FOR [Phone]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF__Order_Bas__Ident__7914ADD4]  DEFAULT ('') FOR [IdentityNumber]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF_N_Order_Base_Sex]  DEFAULT ((-1)) FOR [Sex]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF_N_Order_Base_Age]  DEFAULT ((0)) FOR [Age]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF_N_Order_Base_FamilyType]  DEFAULT ((0)) FOR [FamilyType]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF__Order_Bas__Distr__7A08D20D]  DEFAULT ((0)) FOR [DistrictId]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF__Order_Bas__Distr__7AFCF646]  DEFAULT ('') FOR [DistrictName]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF__Order_Bas__Estat__7BF11A7F]  DEFAULT ((0)) FOR [EstateId]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF__Order_Bas__Estat__7CE53EB8]  DEFAULT ('') FOR [EstateName]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF__Order_Bas__Build__7DD962F1]  DEFAULT ('') FOR [BuildingNO]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF__Order_Bas__UnitN__7ECD872A]  DEFAULT ('') FOR [UnitNO]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF__Order_Bas__RoomN__7FC1AB63]  DEFAULT ('') FOR [RoomNO]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF_N_Order_Base_Area]  DEFAULT ((0)) FOR [Area]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF_N_Order_Base_HouseState]  DEFAULT ((0)) FOR [HouseStatus]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF_N_Order_Base_HouseUse]  DEFAULT ((0)) FOR [HouseUse]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF_N_Order_Base_HouseType]  DEFAULT ((0)) FOR [HouseType]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF_N_Order_Base_PreferStyle]  DEFAULT ((0)) FOR [PreferStyle]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF_N_Order_Base_DeliveryTime]  DEFAULT ('1900-01-01') FOR [DeliveryTime]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF_N_Order_Base_GrabOrderDate]  DEFAULT ('1900-01-01') FOR [GrabOrderDate]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF_N_Order_Base_SignContractDate]  DEFAULT ('1900-01-01') FOR [SignContractDate]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF_N_Order_Base_CompletionDate]  DEFAULT ('1900-01-01') FOR [CompletionDate]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF_N_Order_Base_IsEarnest]  DEFAULT ((0)) FOR [IsEarnest]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF__Order_Bas__Creat__00B5CF9C]  DEFAULT (GETDATE()) FOR [CreateTime]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  CONSTRAINT [DF__Order_Bas__IsDel__01A9F3D5]  DEFAULT ((0)) FOR [IsDel]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  DEFAULT ((0)) FOR [CustomerDel]
GO

ALTER TABLE [dbo].[N_Order_Base] ADD  DEFAULT ('1900-01-01') FOR [StartConstructTime]
GO


