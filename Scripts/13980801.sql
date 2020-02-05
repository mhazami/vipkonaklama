
CREATE TABLE [FileManager].[WebDesignFolder](
	[WebId] [uniqueidentifier] NOT NULL,
	[FolderId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_WebDesignFolder] PRIMARY KEY CLUSTERED 
(
	[WebId] ASC,
	[FolderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [FileManager].[WebDesignFolder]  WITH CHECK ADD  CONSTRAINT [FK_WebDesignFolder_WebSite1] FOREIGN KEY([WebId])
REFERENCES [WebDesign].[WebSite] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [FileManager].[WebDesignFolder] CHECK CONSTRAINT [FK_WebDesignFolder_WebSite1]
go



INSERT INTO [FileManager].[WebDesignFolder]
           ([WebId]
           ,[FolderId])
    (select [WebId],[FolderId] from WebDesign.Folder)
GO

drop table WebDesign.Folder





CREATE TABLE [Payment].[WebDesignAccount](
	[WebId] [uniqueidentifier] NOT NULL,
	[AccountId] [smallint] NOT NULL,
 CONSTRAINT [PK_WebDesignAccount1] PRIMARY KEY CLUSTERED 
(
	[WebId] ASC,
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Payment].[WebDesignAccount]  WITH CHECK ADD  CONSTRAINT [FK_WebDesignAccount1_WebSite] FOREIGN KEY([WebId])
REFERENCES [WebDesign].[WebSite] ([Id])
GO

ALTER TABLE [Payment].[WebDesignAccount] CHECK CONSTRAINT [FK_WebDesignAccount1_WebSite]
GO




INSERT INTO [Payment].[WebDesignAccount]
           ([WebId]
           ,[AccountId])
    (select [WebId],[AccountId] from WebDesign.Account)
GO

drop table WebDesign.Account






CREATE TABLE [ContentManage].[WebDesignContainer](
	[WebId] [uniqueidentifier] NOT NULL,
	[ContainerId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_WebDesignContainer] PRIMARY KEY CLUSTERED 
(
	[WebId] ASC,
	[ContainerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [ContentManage].[WebDesignContainer]  WITH CHECK ADD  CONSTRAINT [FK_Container_WebDesignContainer] FOREIGN KEY([WebId])
REFERENCES [WebDesign].[WebSite] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [ContentManage].[WebDesignContainer] CHECK CONSTRAINT [FK_Container_WebDesignContainer]
GO





INSERT INTO [ContentManage].[WebDesignContainer]
           ([WebId]
           ,[ContainerId])
    (select [WebId],[ContainerId] from WebDesign.Container)
GO

drop table WebDesign.Container






CREATE TABLE [ContentManage].[WebDesignContent](
	[WebId] [uniqueidentifier] NOT NULL,
	[ContentId] [int] NOT NULL,
 CONSTRAINT [PK_WebDesignContent_1] PRIMARY KEY CLUSTERED 
(
	[WebId] ASC,
	[ContentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [ContentManage].[WebDesignContent]  WITH CHECK ADD  CONSTRAINT [FK_WebDesignContent_WebSite] FOREIGN KEY([WebId])
REFERENCES [WebDesign].[WebSite] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [ContentManage].[WebDesignContent] CHECK CONSTRAINT [FK_WebDesignContent_WebSite]
GO





INSERT INTO [ContentManage].[WebDesignContent]
           ([WebId]
           ,[ContentId])
    (select [WebId],[ContentId] from WebDesign.Content)
GO

drop table WebDesign.Content






CREATE TABLE [Payment].[WebDesignDiscountType](
	[WebId] [uniqueidentifier] NOT NULL,
	[DiscountTypeId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_WebDesignDiscountType1] PRIMARY KEY CLUSTERED 
(
	[WebId] ASC,
	[DiscountTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Payment].[WebDesignDiscountType]  WITH CHECK ADD  CONSTRAINT [FK_WebDesignDiscountType_DiscountType1] FOREIGN KEY([DiscountTypeId])
REFERENCES [Payment].[DiscountType] ([Id])
GO

ALTER TABLE [Payment].[WebDesignDiscountType] CHECK CONSTRAINT [FK_WebDesignDiscountType_DiscountType1]
GO

ALTER TABLE [Payment].[WebDesignDiscountType]  WITH CHECK ADD  CONSTRAINT [FK_WebDesignDiscountType_WebDesign1] FOREIGN KEY([WebId])
REFERENCES [WebDesign].[WebSite] ([Id])
GO

ALTER TABLE [Payment].[WebDesignDiscountType] CHECK CONSTRAINT [FK_WebDesignDiscountType_WebDesign1]
GO






INSERT INTO [Payment].[WebDesignDiscountType]
           ([WebId]
           ,[DiscountTypeId])
    (select [WebId],[DiscountTypeId] from WebDesign.DiscountType)
GO

drop table WebDesign.DiscountType


go


CREATE TABLE [FAQ].[WebDesignFAQ](
	[WebId] [uniqueidentifier] NOT NULL,
	[FAQId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_WebDesignFAQ_1] PRIMARY KEY CLUSTERED 
(
	[WebId] ASC,
	[FAQId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [FAQ].[WebDesignFAQ]  WITH CHECK ADD  CONSTRAINT [FK_FAQ_WebDesignFAQ1] FOREIGN KEY([FAQId])
REFERENCES [FAQ].[FAQ] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [FAQ].[WebDesignFAQ] CHECK CONSTRAINT [FK_FAQ_WebDesignFAQ1]
GO

ALTER TABLE [FAQ].[WebDesignFAQ]  WITH CHECK ADD  CONSTRAINT [FK_FAQ_WebSite1] FOREIGN KEY([WebId])
REFERENCES [WebDesign].[WebSite] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [FAQ].[WebDesignFAQ] CHECK CONSTRAINT [FK_FAQ_WebSite1]
GO






INSERT INTO [FAQ].[WebDesignFAQ]
           ([WebId]
           ,[FAQId])
    (select [WebId],[FAQId] from WebDesign.FAQ)
GO

drop table WebDesign.FAQ





CREATE TABLE [FormGenerator].[WebDesignForms](
	[WebId] [uniqueidentifier] NOT NULL,
	[FormId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_WebDesignForms] PRIMARY KEY CLUSTERED 
(
	[WebId] ASC,
	[FormId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [FormGenerator].[WebDesignForms]  WITH CHECK ADD  CONSTRAINT [FK_WebDesignForms_WebSite] FOREIGN KEY([WebId])
REFERENCES [WebDesign].[WebSite] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [FormGenerator].[WebDesignForms] CHECK CONSTRAINT [FK_WebDesignForms_WebSite]
GO








INSERT INTO [FormGenerator].[WebDesignForms]
           ([WebId]
           ,[FormId])
    (select [WebId],[FormId] from WebDesign.Forms)
GO

drop table WebDesign.Forms




CREATE TABLE [Gallery].[WebDesignGallery](
	[WebId] [uniqueidentifier] NOT NULL,
	[GalleryId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_WebDesignGallery] PRIMARY KEY CLUSTERED 
(
	[WebId] ASC,
	[GalleryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Gallery].[WebDesignGallery]  WITH CHECK ADD  CONSTRAINT [FK_WebDesignGallery_WebSite] FOREIGN KEY([WebId])
REFERENCES [WebDesign].[WebSite] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [Gallery].[WebDesignGallery] CHECK CONSTRAINT [FK_WebDesignGallery_WebSite]
GO







INSERT INTO [Gallery].[WebDesignGallery]
           ([WebId]
           ,[GalleryId])
    (select [WebId],[GalleryId] from WebDesign.Gallery)
GO

drop table WebDesign.Gallery

go


CREATE TABLE [ContentManage].[WebDesignHtml](
	[WebId] [uniqueidentifier] NOT NULL,
	[HtmlDesginId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_WebDesignHtml] PRIMARY KEY CLUSTERED 
(
	[WebId] ASC,
	[HtmlDesginId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [ContentManage].[WebDesignHtml]  WITH CHECK ADD  CONSTRAINT [FK_WebDesignHtml_WebSite] FOREIGN KEY([WebId])
REFERENCES [WebDesign].[WebSite] ([Id])
GO

ALTER TABLE [ContentManage].[WebDesignHtml] CHECK CONSTRAINT [FK_WebDesignHtml_WebSite]
GO







INSERT INTO [ContentManage].[WebDesignHtml]
           ([WebId]
           ,[HtmlDesginId])
    (select [WebId],[HtmlDesginId] from WebDesign.Html)
GO

drop table WebDesign.Html





CREATE TABLE [Common].[WebDesignLanguage](
	[WebId] [uniqueidentifier] NOT NULL,
	[LanguageId] [char](5) NOT NULL,
 CONSTRAINT [PK_WebDesignLanguage_1] PRIMARY KEY CLUSTERED 
(
	[WebId] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [Common].[WebDesignLanguage]  WITH CHECK ADD  CONSTRAINT [FK_WebDesignLanguage_Language1] FOREIGN KEY([LanguageId])
REFERENCES [Common].[Language] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [Common].[WebDesignLanguage] CHECK CONSTRAINT [FK_WebDesignLanguage_Language1]
GO

ALTER TABLE [Common].[WebDesignLanguage]  WITH CHECK ADD  CONSTRAINT [FK_WebDesignLanguage_WebSite] FOREIGN KEY([WebId])
REFERENCES [WebDesign].[WebSite] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [Common].[WebDesignLanguage] CHECK CONSTRAINT [FK_WebDesignLanguage_WebSite]
GO








INSERT INTO [Common].[WebDesignLanguage]
           ([WebId]
           ,[LanguageId])
    (select [WebId],[LanguageId] from WebDesign.Language)
GO

drop table WebDesign.Language





CREATE TABLE [ContentManage].[WebDesignMenu](
	[WebId] [uniqueidentifier] NOT NULL,
	[MenuId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_WebDesignMenu_1] PRIMARY KEY CLUSTERED 
(
	[WebId] ASC,
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [ContentManage].[WebDesignMenu]  WITH CHECK ADD  CONSTRAINT [FK_WebDesignMenu_WebSite] FOREIGN KEY([WebId])
REFERENCES [WebDesign].[WebSite] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [ContentManage].[WebDesignMenu] CHECK CONSTRAINT [FK_WebDesignMenu_WebSite]
GO









INSERT INTO [ContentManage].[WebDesignMenu]
           ([WebId]
           ,[MenuId])
    (select [WebId],[MenuId] from WebDesign.Menu)
GO

drop table WebDesign.Menu




CREATE TABLE [News].[WebDesignNews](
	[WebId] [uniqueidentifier] NOT NULL,
	[NewsId] [int] NOT NULL,
 CONSTRAINT [PK_WebDesignNews] PRIMARY KEY CLUSTERED 
(
	[WebId] ASC,
	[NewsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [News].[WebDesignNews]  WITH CHECK ADD  CONSTRAINT [FK_News_WebDesignNews] FOREIGN KEY([NewsId])
REFERENCES [News].[News] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [News].[WebDesignNews] CHECK CONSTRAINT [FK_News_WebDesignNews]
GO

ALTER TABLE [News].[WebDesignNews]  WITH CHECK ADD  CONSTRAINT [FK_WebDesignNews_WebSite] FOREIGN KEY([WebId])
REFERENCES [WebDesign].[WebSite] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [News].[WebDesignNews] CHECK CONSTRAINT [FK_WebDesignNews_WebSite]
GO








INSERT INTO [News].[WebDesignNews]
           ([WebId]
           ,[NewsId])
    (select [WebId],[NewsId] from WebDesign.News)
GO

drop table WebDesign.News




CREATE TABLE [Slider].[WebDesignSlider](
	[WebId] [uniqueidentifier] NOT NULL,
	[SlideId] [smallint] NOT NULL,
 CONSTRAINT [PK_WebDesignSlider] PRIMARY KEY CLUSTERED 
(
	[WebId] ASC,
	[SlideId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Slider].[WebDesignSlider]  WITH CHECK ADD  CONSTRAINT [FK_WebDesignSlider_WebSite] FOREIGN KEY([WebId])
REFERENCES [WebDesign].[WebSite] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [Slider].[WebDesignSlider] CHECK CONSTRAINT [FK_WebDesignSlider_WebSite]
GO






INSERT INTO [Slider].[WebDesignSlider]
           ([WebId]
           ,[SlideId])
    (select [WebId],[SlideId] from WebDesign.Slider)
GO

drop table WebDesign.Slider




CREATE TABLE [FormGenerator].[WebDesignUserForms](
	[WebId] [uniqueidentifier] NOT NULL,
	[FormId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_WebDesignUserForms] PRIMARY KEY CLUSTERED 
(
	[WebId] ASC,
	[FormId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO







INSERT INTO [FormGenerator].[WebDesignUserForms]
           ([WebId]
           ,[FormId])
    (select [WebId],[FormId] from WebDesign.UserForms)
GO

drop table WebDesign.UserForms


go
CREATE TABLE [ContentManage].[WebDesignMenuHtml](
	[WebSiteId] [uniqueidentifier] NOT NULL,
	[MenuHtmlId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_WebDesignMenuHtml] PRIMARY KEY CLUSTERED 
(
	[WebSiteId] ASC,
	[MenuHtmlId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [ContentManage].[WebDesignMenuHtml]  WITH CHECK ADD  CONSTRAINT [FK_WebDesignMenuHtml_Menu] FOREIGN KEY([MenuHtmlId])
REFERENCES [ContentManage].[MenuHtml] ([Id])
GO

ALTER TABLE [ContentManage].[WebDesignMenuHtml] CHECK CONSTRAINT [FK_WebDesignMenuHtml_Menu]
GO

ALTER TABLE [ContentManage].[WebDesignMenuHtml]  WITH CHECK ADD  CONSTRAINT [FK_WebDesignMenuHtml_WebSite] FOREIGN KEY([WebSiteId])
REFERENCES [WebDesign].[WebSite] ([Id])
GO

ALTER TABLE [ContentManage].[WebDesignMenuHtml] CHECK CONSTRAINT [FK_WebDesignMenuHtml_WebSite]
GO






INSERT INTO [ContentManage].[WebDesignMenuHtml]
           ([WebSiteId]
           ,[MenuHtmlId])
    (select [WebSiteId],[MenuHtmlId] from WebDesign.MenuHtml)
GO

drop table WebDesign.MenuHtml

go



alter table [WebDesign].[Configuration] drop column [CertificateSlideId]
go
alter table [WebDesign].[Configuration] drop column [EventsSlideId]
go
update [ContentManage].[Partials] set Url=N'/ContentManager/UIPanel/GetWebDesignMenu' where Url=N'/WebDesign/UIPanel/GetMenu'
go
update [ContentManage].[Partials] set Url=N'/ContentManager/UIPanel/GetWebDesignHeader' where Url=N'/WebDesign/UIPanel/GetHeader'
go
update [ContentManage].[Partials] set Url=N'/ContentManager/UIPanel/GetWebDesignFooter' where Url=N'/WebDesign/UIPanel/GetFooter'
go
update [ContentManage].[Partials] set Url=N'/Slider/UIPanel/GetWebDesignBigSlideShow' where Url=N'/WebDesign/UIPanel/GetBigSlideShow'
go
update [ContentManage].[Partials] set Url=N'/Slider/UIPanel/GetWebDesignMinSlideShow' where Url=N'/WebDesign/UIPanel/GetMinSlideShow'
go
update [ContentManage].[Partials] set Url=N'/Slider/UIPanel/GetWebDesignAverageSlideShow' where Url=N'/WebDesign/UIPanel/GetAverageSlideShow'
go
update [ContentManage].[Partials] set Url=N'/News/UIPanel/GetWebDesignNews' where Url=N'/WebDesign/UIPanel/GetNews'
go
delete [ContentManage].[Partials] where Url like N'%/Congress/%'
go
delete [ContentManage].[Partials] where Url=N'/WebDesign/UIPanel/GetCongressNext'
go


CREATE TABLE [Help].[HelpMenu](
	[HelpId] [uniqueidentifier] NOT NULL,
	[MenuId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_HelpMenu] PRIMARY KEY CLUSTERED 
(
	[HelpId] ASC,
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Help].[HelpMenu]  WITH CHECK ADD  CONSTRAINT [FK_HelpMenu_Menu] FOREIGN KEY([MenuId])
REFERENCES [Security].[Menu] ([Id])
GO

ALTER TABLE [Help].[HelpMenu] CHECK CONSTRAINT [FK_HelpMenu_Menu]
GO





INSERT INTO [Help].[HelpMenu]
           ([HelpId]
           ,[MenuId])
     (select [HelpId],Id from [Security].[Menu] where [HelpId]  is not null)
GO


alter table [Security].[Menu] drop Constraint [FK_Menu_Help]
go
alter table [Security].[Menu] drop column [HelpId]
go


update [Security].[Menu] set Url=N'/ContentManager/webdesignmenu/edit' where Url=N'/webdesign/menu/edit'
go
update [Security].[Menu] set Url=N'/ContentManager/webdesigncontent/create' where Url=N'/webdesign/content/create'
go
update [Security].[Menu] set Url=N'/ContentManager/webdesigncontainer' where Url=N'/webdesign/container'
go
update [Security].[Menu] set Url=N'/FAQ/webdesignfaq/delete' where Url=N'/webdesign/faq/delete'
go
update [Security].[Menu] set Url=N'/ContentManager/webdesignMenuHtml/Index' where Url=N'/WebDesign/MenuHtml/Index'
go
update [Security].[Menu] set Url=N'/News/webdesignnews/details' where Url=N'/webdesign/news/details'
go
update [Security].[Menu] set Url=N'/Payment/webdesigndiscounttype/index' where Url=N'/WebDesign/discounttype/index'
go
update [Security].[Menu] set Url=N'/FAQ/webdesignfaq/edit' where Url=N'/webdesign/faq/edit'
go
update [Security].[Menu] set Url=N'/Common/webdesignlanguage/delete' where Url=N'/webdesign/language/delete'
go
update [Security].[Menu] set Url=N'/ContentManager/webdesignmenu/create' where Url=N'/webdesign/menu/create'
go
update [Security].[Menu] set Url=N'/FileManager/webdesignfolder/create' where Url=N'/webdesign/folder/create'
go
update [Security].[Menu] set Url=N'/Common/webdesignlanguage/index' where Url=N'/webdesign/language/index'
go
update [Security].[Menu] set Url=N'/ContentManager/webdesignhtml/index' where Url=N'/webdesign/html/index'
go
update [Security].[Menu] set Url=N'/FormGenerator/webdesignforms/delete' where Url=N'/webdesign/forms/delete'
go
update [Security].[Menu] set Url=N'/FileManager/webdesignfolder' where Url=N'/webdesign/folder'
go
update [Security].[Menu] set Url=N'/FormGenerator/webdesignforms' where Url=N'/webdesign/forms'
go
update [Security].[Menu] set Url=N'/ContentManager/webdesignMenuHtml/Delete' where Url=N'/WebDesign/MenuHtml/Delete'
go
update [Security].[Menu] set Url=N'/Payment/webdesigndiscounttype/delete' where Url=N'/WebDesign/discounttype/delete'
go
update [Security].[Menu] set Url=N'/Gallery/webdesigngallery/delete' where Url=N'/webdesign/gallery/delete'
go
update [Security].[Menu] set Url=N'/FileManager/webdesignfolder/delete' where Url=N'/webdesign/folder/delete'
go
update [Security].[Menu] set Url=N'/ContentManager/webdesigncontent/details' where Url=N'/webdesign/content/details'
go
update [Security].[Menu] set Url=N'/FAQ/webdesignfaq/index' where Url=N'/webdesign/faq/index'
go
update [Security].[Menu] set Url=N'/FileManager/webdesignfolder/index' where Url=N'/webdesign/folder/index'
go
update [Security].[Menu] set Url=N'/Payment/webdesignaccount/Details' where Url=N'/webdesign/webdesignaccount/Details'
go
update [Security].[Menu] set Url=N'/FormGenerator/webdesignforms/create' where Url=N'/webdesign/forms/create'
go
update [Security].[Menu] set Url=N'/Gallery/webdesigngallery/edit' where Url=N'/webdesign/gallery/edit'
go
update [Security].[Menu] set Url=N'/ContentManager/webdesigncontent' where Url=N'/webdesign/content'
go
update [Security].[Menu] set Url=N'/Common/webdesignlanguage/edit' where Url=N'/webdesign/language/edit'
go
update [Security].[Menu] set Url=N'/ContentManager/webdesignmenu/index' where Url=N'/webdesign/menu/index'
go
update [Security].[Menu] set Url=N'/News/webdesignnews/delete' where Url=N'/webdesign/news/delete'
go
update [Security].[Menu] set Url=N'/Slider/webdesignslide' where Url=N'/webdesign/slide'
go
update [Security].[Menu] set Url=N'/ContentManager/webdesignhtml/edit' where Url=N'/webdesign/html/edit'
go
update [Security].[Menu] set Url=N'/ContentManager/webdesignmenu' where Url=N'/webdesign/menu'
go
update [Security].[Menu] set Url=N'/Payment/webdesigndiscounttype/edit' where Url=N'/WebDesign/discounttype/edit'
go
update [Security].[Menu] set Url=N'/FAQ/webdesignfaq/details' where Url=N'/webdesign/faq/details'
go
update [Security].[Menu] set Url=N'/FormGenerator/webdesignforms/details' where Url=N'/webdesign/forms/details'
go
update [Security].[Menu] set Url=N'/FileManager/webdesignfolder/edit' where Url=N'/webdesign/folder/edit'
go
update [Security].[Menu] set Url=N'/ContentManager/webdesignMenuHtml/Edit' where Url=N'/WebDesign/MenuHtml/Edit'
go
update [Security].[Menu] set Url=N'/ContentManager/webdesigncontainer/details' where Url=N'/webdesign/container/details'
go
update [Security].[Menu] set Url=N'/ContentManager/webdesigncontent/index' where Url=N'/webdesign/content/index'
go
update [Security].[Menu] set Url=N'/ContentManager/webdesignmenu/delete' where Url=N'/webdesign/menu/delete'
go
update [Security].[Menu] set Url=N'/Slider/webdesignslide/index' where Url=N'/webdesign/slide/index'
go
update [Security].[Menu] set Url=N'/ContentManager/webdesignhtml/details' where Url=N'/webdesign/html/details'
go
update [Security].[Menu] set Url=N'/Slider/webdesignslide/edit' where Url=N'/webdesign/slide/edit'
go
update [Security].[Menu] set Url=N'/ContentManager/webdesignhtml/create' where Url=N'/webdesign/html/create'
go
update [Security].[Menu] set Url=N'/FormGenerator/webdesignforms/edit' where Url=N'/webdesign/forms/edit'
go
update [Security].[Menu] set Url=N'/ContentManager/webdesignMenuHtml/Create' where Url=N'/WebDesign/MenuHtml/Create'
go
update [Security].[Menu] set Url=N'/Gallery/webdesigngallery' where Url=N'/webdesign/gallery'
go
update [Security].[Menu] set Url=N'/News/webdesignnews' where Url=N'/webdesign/news'
go
update [Security].[Menu] set Url=N'/ContentManager/webdesigncontainer/delete' where Url=N'/webdesign/container/delete'
go
update [Security].[Menu] set Url=N'/ContentManager/webdesignmenu/lookupmenu' where Url=N'/webdesign/menu/lookupmenu'
go
update [Security].[Menu] set Url=N'/Payment/webdesignaccount/Create' where Url=N'/webdesign/webdesignaccount/Create'
go
update [Security].[Menu] set Url=N'/ContentManager/webdesignmenu/details' where Url=N'/webdesign/menu/details'
go
update [Security].[Menu] set Url=N'/Slider/webdesignslide/create' where Url=N'/webdesign/slide/create'
go
update [Security].[Menu] set Url=N'/Payment/webdesignaccount/Edit' where Url=N'/webdesign/webdesignaccount/Edit'
go
update [Security].[Menu] set Url=N'/FAQ/webdesignfaq' where Url=N'/webdesign/faq'
go
update [Security].[Menu] set Url=N'/News/webdesignnews/edit' where Url=N'/webdesign/news/edit'
go
update [Security].[Menu] set Url=N'/FAQ/webdesignfaq/create' where Url=N'/webdesign/faq/create'
go
update [Security].[Menu] set Url=N'/Common/webdesignlanguage/create' where Url=N'/webdesign/language/create'
go
update [Security].[Menu] set Url=N'/Common/webdesignlanguage/details' where Url=N'/webdesign/language/details'
go
update [Security].[Menu] set Url=N'/News/webdesignnews/create' where Url=N'/webdesign/news/create'
go
update [Security].[Menu] set Url=N'/Payment/webdesigndiscounttype/details' where Url=N'/WebDesign/discounttype/details'
go
update [Security].[Menu] set Url=N'/ContentManager/webdesignMenuHtml' where Url=N'/WebDesign/MenuHtml'
go
update [Security].[Menu] set Url=N'/Gallery/webdesigngallery/index' where Url=N'/webdesign/gallery/index'
go
update [Security].[Menu] set Url=N'/Payment/webdesigndiscounttype/create' where Url=N'/WebDesign/discounttype/create'
go
update [Security].[Menu] set Url=N'/ContentManager/webdesigncontent/delete' where Url=N'/webdesign/content/delete'
go
update [Security].[Menu] set Url=N'/Payment/webdesignaccount/index' where Url=N'/webdesign/webdesignaccount/index'
go
update [Security].[Menu] set Url=N'/News/webdesignnews/index' where Url=N'/webdesign/news/index'
go
update [Security].[Menu] set Url=N'/Gallery/webdesigngallery/create' where Url=N'/webdesign/gallery/create'
go
update [Security].[Menu] set Url=N'/FileManager/webdesignfolder/details' where Url=N'/webdesign/folder/details'
go
update [Security].[Menu] set Url=N'/ContentManager/webdesignMenuHtml/Details' where Url=N'/WebDesign/MenuHtml/Details'
go
update [Security].[Menu] set Url=N'/ContentManager/webdesigncontainer/index' where Url=N'/webdesign/container/index'
go
update [Security].[Menu] set Url=N'/ContentManager/webdesigncontainer/edit' where Url=N'/webdesign/container/edit'
go
update [Security].[Menu] set Url=N'/ContentManager/webdesignhtml' where Url=N'/webdesign/html'
go
update [Security].[Menu] set Url=N'/ContentManager/webdesigncontent/edit' where Url=N'/webdesign/content/edit'
go
update [Security].[Menu] set Url=N'/Payment/webdesigndiscounttype/' where Url=N'/WebDesign/discounttype/'
go
update [Security].[Menu] set Url=N'/FormGenerator/webdesignforms/index' where Url=N'/webdesign/forms/index'
go
update [Security].[Menu] set Url=N'/Gallery/webdesigngallery/details' where Url=N'/webdesign/gallery/details'
go
update [Security].[Menu] set Url=N'/Slider/webdesignslide/delete' where Url=N'/webdesign/slide/delete'
go
update [Security].[Menu] set Url=N'/Common/webdesignlanguage' where Url=N'/webdesign/language'
go
update [Security].[Menu] set Url=N'/Payment/webdesignaccount/Delete' where Url=N'/webdesign/webdesignaccount/Delete'
go

