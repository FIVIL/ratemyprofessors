USE [GradeMyTeacherDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 8/13/2018 4:59:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 8/13/2018 4:59:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[ID] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](20) NULL,
	[PassWord] [nvarchar](20) NULL,
	[Name] [nvarchar](20) NULL,
	[LastName] [nvarchar](20) NULL,
	[Emain] [nvarchar](60) NULL,
	[RegistarationDate] [datetime2](7) NOT NULL,
	[ISAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 8/13/2018 4:59:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[ID] [uniqueidentifier] NOT NULL,
	[ShowName] [nvarchar](20) NULL,
	[Teaching] [tinyint] NOT NULL,
	[Marking] [tinyint] NOT NULL,
	[HomeWork] [tinyint] NOT NULL,
	[Project] [tinyint] NOT NULL,
	[Moods] [tinyint] NOT NULL,
	[RollCall] [tinyint] NOT NULL,
	[Exhausting] [tinyint] NOT NULL,
	[HandOuts] [tinyint] NOT NULL,
	[Update] [tinyint] NOT NULL,
	[ScapeAtTheEnd] [tinyint] NOT NULL,
	[Answering] [tinyint] NOT NULL,
	[HardExams] [tinyint] NOT NULL,
	[Knoledge] [tinyint] NOT NULL,
	[OverAll] [tinyint] NOT NULL,
	[Comments] [nvarchar](399) NULL,
	[Like] [tinyint] NOT NULL,
	[DisLike] [tinyint] NOT NULL,
	[DateTime] [datetime2](7) NOT NULL,
	[Angry] [tinyint] NULL,
	[Bluntess] [tinyint] NULL,
	[DoYourWork] [tinyint] NULL,
	[Bad] [tinyint] NULL,
	[ProfessorID] [uniqueidentifier] NOT NULL,
	[AccountID] [uniqueidentifier] NULL,
	[EmailID] [uniqueidentifier] NOT NULL,
	[Verfied] [bit] NOT NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 8/13/2018 4:59:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[ID] [uniqueidentifier] NOT NULL,
	[Text] [nvarchar](max) NULL,
	[MailAddress] [nvarchar](60) NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Courses]    Script Date: 8/13/2018 4:59:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](40) NULL,
	[AliasNames] [nvarchar](10) NULL,
	[FacultyID] [uniqueidentifier] NULL,
	[Approved] [bit] NOT NULL,
 CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Emails]    Script Date: 8/13/2018 4:59:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Emails](
	[ID] [uniqueidentifier] NOT NULL,
	[Address] [nvarchar](60) NULL,
	[Verified] [bit] NOT NULL,
 CONSTRAINT [PK_Emails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Faculties]    Script Date: 8/13/2018 4:59:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Faculties](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](20) NULL,
	[AliasName] [nvarchar](20) NULL,
	[UniversityID] [tinyint] NOT NULL,
 CONSTRAINT [PK_Faculties] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProfCourses]    Script Date: 8/13/2018 4:59:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfCourses](
	[ID] [uniqueidentifier] NOT NULL,
	[ProfessorID] [uniqueidentifier] NOT NULL,
	[CourseID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ProfCourses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Professors]    Script Date: 8/13/2018 4:59:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Professors](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](40) NULL,
	[LastName] [nvarchar](40) NULL,
	[Link] [nvarchar](200) NULL,
	[PrivateLink] [nvarchar](200) NULL,
	[WPLink] [nvarchar](200) NULL,
	[ImageLink] [nvarchar](200) NULL,
	[Comment] [nvarchar](max) NULL,
	[Staff] [bit] NOT NULL,
	[Approved] [bit] NOT NULL,
 CONSTRAINT [PK_Professors] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProfFacs]    Script Date: 8/13/2018 4:59:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfFacs](
	[ID] [uniqueidentifier] NOT NULL,
	[ProfessorID] [uniqueidentifier] NOT NULL,
	[FacultyID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ProfFacs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Universities]    Script Date: 8/13/2018 4:59:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Universities](
	[ID] [tinyint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](40) NULL,
 CONSTRAINT [PK_Universities] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Accounts_AccountID] FOREIGN KEY([AccountID])
REFERENCES [dbo].[Accounts] ([ID])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Accounts_AccountID]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Emails_EmailID] FOREIGN KEY([EmailID])
REFERENCES [dbo].[Emails] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Emails_EmailID]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Professors_ProfessorID] FOREIGN KEY([ProfessorID])
REFERENCES [dbo].[Professors] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Professors_ProfessorID]
GO
ALTER TABLE [dbo].[Courses]  WITH CHECK ADD  CONSTRAINT [FK_Courses_Faculties_FacultyID] FOREIGN KEY([FacultyID])
REFERENCES [dbo].[Faculties] ([ID])
GO
ALTER TABLE [dbo].[Courses] CHECK CONSTRAINT [FK_Courses_Faculties_FacultyID]
GO
ALTER TABLE [dbo].[Faculties]  WITH CHECK ADD  CONSTRAINT [FK_Faculties_Universities_UniversityID] FOREIGN KEY([UniversityID])
REFERENCES [dbo].[Universities] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Faculties] CHECK CONSTRAINT [FK_Faculties_Universities_UniversityID]
GO
ALTER TABLE [dbo].[ProfCourses]  WITH CHECK ADD  CONSTRAINT [FK_ProfCourses_Courses_CourseID] FOREIGN KEY([CourseID])
REFERENCES [dbo].[Courses] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProfCourses] CHECK CONSTRAINT [FK_ProfCourses_Courses_CourseID]
GO
ALTER TABLE [dbo].[ProfCourses]  WITH CHECK ADD  CONSTRAINT [FK_ProfCourses_Professors_ProfessorID] FOREIGN KEY([ProfessorID])
REFERENCES [dbo].[Professors] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProfCourses] CHECK CONSTRAINT [FK_ProfCourses_Professors_ProfessorID]
GO
ALTER TABLE [dbo].[ProfFacs]  WITH CHECK ADD  CONSTRAINT [FK_ProfFacs_Faculties_FacultyID] FOREIGN KEY([FacultyID])
REFERENCES [dbo].[Faculties] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProfFacs] CHECK CONSTRAINT [FK_ProfFacs_Faculties_FacultyID]
GO
ALTER TABLE [dbo].[ProfFacs]  WITH CHECK ADD  CONSTRAINT [FK_ProfFacs_Professors_ProfessorID] FOREIGN KEY([ProfessorID])
REFERENCES [dbo].[Professors] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProfFacs] CHECK CONSTRAINT [FK_ProfFacs_Professors_ProfessorID]
GO
