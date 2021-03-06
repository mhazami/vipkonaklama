/****** Object:  Schema [Slider]    Script Date: 10/24/2019 12:30:35 PM ******/
CREATE SCHEMA [Slider]
GO
/****** Object:  Table [Slider].[Slide]    Script Date: 10/24/2019 12:30:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Slider].[Slide](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Enabled] [bit] NOT NULL,
	[IsExternal] [bit] NOT NULL,
	[EffectType] [varchar](20) NULL,
	[SlidItemTimeOut] [int] NOT NULL,
	[HeightSlide] [int] NULL,
 CONSTRAINT [PK_Slide] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Slider].[SlideItem]    Script Date: 10/24/2019 12:30:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Slider].[SlideItem](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[SlideId] [smallint] NOT NULL,
	[Title] [nvarchar](400) NULL,
	[Enabled] [bit] NOT NULL,
	[NavigateUrl] [nvarchar](250) NULL,
	[Order] [tinyint] NULL,
 CONSTRAINT [PK_SlideItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Slider].[WebDesignSlider]    Script Date: 10/24/2019 12:30:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Slider].[WebDesignSlider](
	[WebId] [uniqueidentifier] NOT NULL,
	[SlideId] [smallint] NOT NULL,
 CONSTRAINT [PK_Slider] PRIMARY KEY CLUSTERED 
(
	[WebId] ASC,
	[SlideId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [Slider].[Slide] ADD  CONSTRAINT [DF_Slide_Enabled]  DEFAULT ((0)) FOR [Enabled]
GO
ALTER TABLE [Slider].[Slide] ADD  DEFAULT ((1)) FOR [SlidItemTimeOut]
GO
ALTER TABLE [Slider].[SlideItem] ADD  CONSTRAINT [DF_SlideItem_Enabled]  DEFAULT ((1)) FOR [Enabled]
GO
ALTER TABLE [Slider].[SlideItem]  WITH CHECK ADD  CONSTRAINT [FK_SlideItem_Slide] FOREIGN KEY([SlideId])
REFERENCES [Slider].[Slide] ([Id])
GO
ALTER TABLE [Slider].[SlideItem] CHECK CONSTRAINT [FK_SlideItem_Slide]
GO
ALTER TABLE [Slider].[WebDesignSlider]  WITH CHECK ADD  CONSTRAINT [FK_Slider_WebSite] FOREIGN KEY([WebId])
REFERENCES [WebDesign].[WebSite] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Slider].[WebDesignSlider] CHECK CONSTRAINT [FK_Slider_WebSite]
GO

INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'ad735985-8288-4827-99cb-04b9461a3dc5', 0, N'لیست', N'/Slider/Slide/Index', N'1bbbcd9a-00f7-4638-a275-0e8883dab82e', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'53d94e00-3447-4410-a168-0d275a1fb1c3', 0, N'ویرایش', N'/Slider/SlideItem/Edit', N'51317e17-882b-4b92-886c-d9e00e03a4fb', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'1bbbcd9a-00f7-4638-a275-0e8883dab82e', 0, N'اسلاید', N'/Slider/Slide', NULL, 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'314d9c91-52df-4b6f-94dc-24b84aaf36b6', 0, N'جزئیات', N'/Slider/Slideitem/Details', N'51317e17-882b-4b92-886c-d9e00e03a4fb', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'f6319868-6b44-4f3c-9966-2a87cd69dca7', 0, N'ایجاد', N'/Slider/Slideitem/Create', N'51317e17-882b-4b92-886c-d9e00e03a4fb', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'111ef4ba-959c-4ef4-91ab-3d07fa036faf', 0, N'حذف', N'/Slider/Slideitem/Delete', N'51317e17-882b-4b92-886c-d9e00e03a4fb', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'893f509a-bc42-4ec1-82af-47bded9104b3', 0, N'ایجاد', N'/Slider/Slide/Create', N'1bbbcd9a-00f7-4638-a275-0e8883dab82e', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'a8a8ce08-150f-4182-bf5e-4a66b2659ed4', NULL, N'اسلاید شو', N'/Slider/webdesignslide', NULL, 0, 1, 1, NULL, 10)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'55931e3f-8e51-4b37-9349-7bc0f59d1cfa', NULL, N'لیست', N'/Slider/webdesignslide/index', N'a8a8ce08-150f-4182-bf5e-4a66b2659ed4', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'765f4a34-d30e-4f62-ae0f-7ec1907df1e7', NULL, N'ویرایش', N'/Slider/webdesignslide/edit', N'a8a8ce08-150f-4182-bf5e-4a66b2659ed4', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'9058ebcd-6292-47a9-9aa3-a042f4d17136', 0, N'جزئیات', N'/Slider/Slide/Details', N'1bbbcd9a-00f7-4638-a275-0e8883dab82e', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'e081e2b2-a17f-4beb-88a8-a22f6f3c0281', NULL, N'ایجاد', N'/Slider/webdesignslide/create', N'a8a8ce08-150f-4182-bf5e-4a66b2659ed4', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'a470e9e2-9086-42fd-9e97-b3c93b5913fd', 0, N'ویرایش', N'/Slider/Slide/Edit', N'1bbbcd9a-00f7-4638-a275-0e8883dab82e', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'b1a2b3f7-3519-48e6-a44c-cb90c974667b', 0, N'لیست', N'/Slider/Slideitem/Index', N'51317e17-882b-4b92-886c-d9e00e03a4fb', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'a155ab52-7574-460f-8d24-da7dc287414f', 0, N'حذف', N'/Slider/Slide/Delete', N'1bbbcd9a-00f7-4638-a275-0e8883dab82e', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'21c1a65f-6c39-49ae-9301-f6af4e5628f1', NULL, N'حذف', N'/Slider/webdesignslide/delete', N'a8a8ce08-150f-4182-bf5e-4a66b2659ed4', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'a8a8ce08-150f-4182-bf5e-4a66b2659ed4')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'55931e3f-8e51-4b37-9349-7bc0f59d1cfa')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'765f4a34-d30e-4f62-ae0f-7ec1907df1e7')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'e081e2b2-a17f-4beb-88a8-a22f6f3c0281')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'21c1a65f-6c39-49ae-9301-f6af4e5628f1')
GO
