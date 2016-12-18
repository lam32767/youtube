USE [master]
GO
/****** Object:  Database [YoutubeDB]    Script Date: 12/18/2016 9:52:32 AM ******/
CREATE DATABASE [YoutubeDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'YoutubeDB', FILENAME = N'c:\SQLDATA\YoutubeDB.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'YoutubeDB_log', FILENAME = N'c:\SQLDATA\YoutubeDB_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [YoutubeDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [YoutubeDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [YoutubeDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [YoutubeDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [YoutubeDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [YoutubeDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [YoutubeDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [YoutubeDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [YoutubeDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [YoutubeDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [YoutubeDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [YoutubeDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [YoutubeDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [YoutubeDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [YoutubeDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [YoutubeDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [YoutubeDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [YoutubeDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [YoutubeDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [YoutubeDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [YoutubeDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [YoutubeDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [YoutubeDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [YoutubeDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [YoutubeDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [YoutubeDB] SET  MULTI_USER 
GO
ALTER DATABASE [YoutubeDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [YoutubeDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [YoutubeDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [YoutubeDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [YoutubeDB] SET DELAYED_DURABILITY = DISABLED 
GO
USE [YoutubeDB]
GO
/****** Object:  Table [dbo].[Videos]    Script Date: 12/18/2016 9:52:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Videos](
	[Id] [nvarchar](128) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Rating] [nvarchar](max) NULL,
	[Likes] [int] NOT NULL,
	[Dislikes] [int] NOT NULL,
	[ChannelTitle] [nvarchar](max) NULL,
	[Comment] [nvarchar](max) NULL,
	[PublishDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.Videos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
USE [master]
GO
ALTER DATABASE [YoutubeDB] SET  READ_WRITE 
GO
