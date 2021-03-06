USE [master]
GO
/****** Object:  Database [SCSE_DB]    Script Date: 10/11/2021 12:43:52 CH ******/
CREATE DATABASE [SCSE_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SCSE_DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\SCSE_DB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SCSE_DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\SCSE_DB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SCSE_DB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SCSE_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SCSE_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SCSE_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SCSE_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SCSE_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SCSE_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [SCSE_DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SCSE_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SCSE_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SCSE_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SCSE_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SCSE_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SCSE_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SCSE_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SCSE_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SCSE_DB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SCSE_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SCSE_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SCSE_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SCSE_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SCSE_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SCSE_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SCSE_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SCSE_DB] SET RECOVERY FULL 
GO
ALTER DATABASE [SCSE_DB] SET  MULTI_USER 
GO
ALTER DATABASE [SCSE_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SCSE_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SCSE_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SCSE_DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SCSE_DB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SCSE_DB] SET QUERY_STORE = OFF
GO
USE [SCSE_DB]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 10/11/2021 12:43:53 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[IDUser] [int] IDENTITY(1,1) NOT NULL,
	[IDRole] [int] NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[IDState] [int] NOT NULL,
	[CreatedByDate] [date] NULL,
	[Phone] [varchar](20) NOT NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[Image] [nvarchar](max) NULL,
	[Sex] [nvarchar](50) NULL,
 CONSTRAINT [PK_Account_1] PRIMARY KEY CLUSTERED 
(
	[IDUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 10/11/2021 12:43:53 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[IDRole] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[IDRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[LoginRole]    Script Date: 10/11/2021 12:43:53 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[LoginRole]
AS
SELECT acc.IDUser, acc.FullName, q.RoleName, acc.Username, acc.Password, acc.Email, acc.CreatedByDate, acc.IDState
FROM     dbo.Account AS acc INNER JOIN
                  dbo.Role AS q ON acc.IDRole = q.IDRole
GO
/****** Object:  Table [dbo].[NewsVN]    Script Date: 10/11/2021 12:43:53 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsVN](
	[IDNews] [int] IDENTITY(1,1) NOT NULL,
	[IdField] [int] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Slug] [nvarchar](max) NOT NULL,
	[Details] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[CreatedByDate] [date] NOT NULL,
	[UpdatedByDate] [date] NULL,
	[Author] [nvarchar](50) NOT NULL,
	[IDState] [int] NOT NULL,
 CONSTRAINT [PK_NewsVN] PRIMARY KEY CLUSTERED 
(
	[IDNews] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NewsEN]    Script Date: 10/11/2021 12:43:53 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsEN](
	[IDNewsEN] [int] NOT NULL,
	[IdField] [int] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[SlugEN] [nvarchar](max) NOT NULL,
	[Details] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[CreatedByDate] [date] NOT NULL,
	[UpdatedByDate] [date] NULL,
	[Author] [nvarchar](50) NOT NULL,
	[IDState] [int] NOT NULL,
 CONSTRAINT [PK_NewsEN] PRIMARY KEY CLUSTERED 
(
	[IDNewsEN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[NewsVNforEN]    Script Date: 10/11/2021 12:43:53 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[NewsVNforEN] AS
SELECT *
FROM   NewsVN
WHERE  NewsVN.IDNews NOT IN (SELECT NewsEN.IDNewsEN FROM NewsEN)
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 10/11/2021 12:43:53 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[IDPost] [int] IDENTITY(1,1) NOT NULL,
	[IDCat] [int] NOT NULL,
	[IDField] [int] NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Slug] [nvarchar](max) NOT NULL,
	[Details] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[CreatedByDate] [date] NOT NULL,
	[UpdatedByDate] [date] NULL,
	[Author] [nvarchar](50) NOT NULL,
	[IDState] [int] NOT NULL,
 CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED 
(
	[IDPost] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostsEN]    Script Date: 10/11/2021 12:43:53 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostsEN](
	[IDPostEN] [int] NOT NULL,
	[IDCat] [int] NOT NULL,
	[IDField] [int] NULL,
	[Title] [nvarchar](404) NOT NULL,
	[SlugEN] [nvarchar](404) NOT NULL,
	[Details] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[CreatedByDate] [date] NOT NULL,
	[UpdatedByDate] [date] NULL,
	[Author] [nvarchar](50) NOT NULL,
	[IDState] [int] NOT NULL,
 CONSTRAINT [PK_PostsEN] PRIMARY KEY CLUSTERED 
(
	[IDPostEN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[PostsforEN]    Script Date: 10/11/2021 12:43:53 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[PostsforEN] AS
SELECT *
FROM   Posts
WHERE  Posts.IDPost NOT IN (SELECT PostsEN.IDPostEN FROM PostsEN)
GO
/****** Object:  Table [dbo].[ImgPortfolio]    Script Date: 10/11/2021 12:43:53 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImgPortfolio](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](200) NOT NULL,
	[ImagePortfolio] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ImgPortfolio_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Portfolio]    Script Date: 10/11/2021 12:43:53 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Portfolio](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](200) NOT NULL,
	[PositionEN] [nchar](10) NULL,
	[Position] [nvarchar](50) NOT NULL,
	[DetailsEN] [nchar](10) NULL,
	[Details] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Portfolio] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[ImageForPortfolio]    Script Date: 10/11/2021 12:43:53 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create View [dbo].[ImageForPortfolio] As
SELECT *
FROM dbo.ImgPortfolio
WHERE (FullName NOT IN (SELECT FullName FROM dbo.Portfolio))
GO
/****** Object:  Table [dbo].[BankInformation]    Script Date: 10/11/2021 12:43:53 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankInformation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ImageQR] [nvarchar](max) NOT NULL,
	[BankName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_BankInformation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Banner]    Script Date: 10/11/2021 12:43:53 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Banner](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[CreatedByUser] [nvarchar](50) NOT NULL,
	[CreatedByDate] [date] NULL,
	[UpdateByUser] [nvarchar](50) NULL,
	[UpdatedByDate] [date] NULL,
 CONSTRAINT [PK_Banner] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 10/11/2021 12:43:53 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[IDCat] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[IDCat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 10/11/2021 12:43:53 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[Phone] [varchar](20) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Details] [nvarchar](max) NOT NULL,
	[CreatedByDate] [date] NOT NULL,
	[UpdatedByDate] [date] NULL,
	[Subtitle] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Document]    Script Date: 10/11/2021 12:43:53 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Document](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Slug] [nvarchar](max) NOT NULL,
	[Details] [nvarchar](max) NOT NULL,
	[CreatedByDate] [date] NULL,
	[UpdatedByDate] [date] NULL,
 CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Field]    Script Date: 10/11/2021 12:43:53 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Field](
	[IdField] [int] IDENTITY(1,1) NOT NULL,
	[FieldName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Field] PRIMARY KEY CLUSTERED 
(
	[IdField] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 10/11/2021 12:43:53 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[Decription] [nvarchar](max) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[Url] [nvarchar](max) NOT NULL,
	[CreatedByDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Partners]    Script Date: 10/11/2021 12:43:53 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Partners](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NULL,
	[Field] [nvarchar](max) NOT NULL,
	[Phone] [varchar](20) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Link] [nvarchar](max) NULL,
 CONSTRAINT [PK_Partners] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhotoGallery]    Script Date: 10/11/2021 12:43:53 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhotoGallery](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDCat] [int] NOT NULL,
	[IDField] [int] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[TitleEN] [nvarchar](max) NULL,
	[Slug] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NULL,
	[CreatedByDate] [date] NULL,
	[UpdatedByDate] [date] NULL,
 CONSTRAINT [PK_PhotoGallery] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[State]    Script Date: 10/11/2021 12:43:53 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StateName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VideoGallery]    Script Date: 10/11/2021 12:43:53 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VideoGallery](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDCat] [int] NOT NULL,
	[IDField] [int] NOT NULL,
	[TitleEN] [nvarchar](200) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[VideoID] [nvarchar](50) NOT NULL,
	[Image] [nvarchar](150) NOT NULL,
	[LinkYTB] [nvarchar](150) NOT NULL,
	[DescriptionEN] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[CreatedByDate] [date] NULL,
	[UpdateByDate] [date] NULL,
 CONSTRAINT [PK_Video] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Volunteer]    Script Date: 10/11/2021 12:43:53 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Volunteer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[DOB] [date] NOT NULL,
	[Phone] [varchar](20) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Project] [nvarchar](max) NOT NULL,
	[Purpose] [nvarchar](max) NOT NULL,
	[IDState] [int] NOT NULL,
 CONSTRAINT [PK_Volunteer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "acc"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 243
            End
            DisplayFlags = 280
            TopColumn = 6
         End
         Begin Table = "q"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 294
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'LoginRole'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'LoginRole'
GO
USE [master]
GO
ALTER DATABASE [SCSE_DB] SET  READ_WRITE 
GO
