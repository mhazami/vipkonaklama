
go
drop table [Comment].CommentType
go
drop table [Comment].CommetnAttribute
go
drop table [Comment].CoomentSetting
go

alter table [Comment].Comment drop COLUMN  ComentTypeId

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Article].[ArticleComment](
	[CommentId] [uniqueidentifier] NOT NULL,
	[ArticleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_CommentArticle] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC,
	[ArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Article].[ArticleComment]  WITH CHECK ADD  CONSTRAINT [FK_CommentArticle_Article] FOREIGN KEY([ArticleId])
REFERENCES [Article].[Article] ([Id])
GO

ALTER TABLE [Article].[ArticleComment] CHECK CONSTRAINT [FK_CommentArticle_Article]
GO

ALTER TABLE [Article].[ArticleComment]  WITH CHECK ADD  CONSTRAINT [FK_CommentArticle_Comment] FOREIGN KEY([CommentId])
REFERENCES [Comment].[Comment] ([Id])
GO

ALTER TABLE [Article].[ArticleComment] CHECK CONSTRAINT [FK_CommentArticle_Comment]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [News].[NewsComment](
	[CommentId] [uniqueidentifier] NOT NULL,
	[NewsId] [int] NOT NULL,
 CONSTRAINT [PK_CommentNews] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC,
	[NewsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [News].[NewsComment]  WITH CHECK ADD  CONSTRAINT [FK_CommentNews_Comment] FOREIGN KEY([CommentId])
REFERENCES [Comment].[Comment] ([Id])
GO

ALTER TABLE [News].[NewsComment] CHECK CONSTRAINT [FK_CommentNews_Comment]
GO

ALTER TABLE [News].[NewsComment]  WITH CHECK ADD  CONSTRAINT [FK_CommentNews_News] FOREIGN KEY([NewsId])
REFERENCES [News].[News] ([Id])
GO

ALTER TABLE [News].[NewsComment] CHECK CONSTRAINT [FK_CommentNews_News]
GO
go
ALTER TABLE Comment.Comment
ALTER  COLUMN SendDate char(10);
GO
alter table  Comment.Comment  add SendTime char(5)
GO
alter table News.News add GetComment bit Not NULL default (0)
GO
alter table [Article].Article add GetComment bit Not NULL default (0)