USE [master]
GO
/****** Object:  Database [INEXFOLER]    Script Date: 08/14/2024 07:37:55 ******/
CREATE DATABASE [INEXFOLER] ON  PRIMARY 
( NAME = N'INEXFOLER', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\INEXFOLER.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'INEXFOLER_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\INEXFOLER_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [INEXFOLER] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [INEXFOLER].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [INEXFOLER] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [INEXFOLER] SET ANSI_NULLS OFF
GO
ALTER DATABASE [INEXFOLER] SET ANSI_PADDING OFF
GO
ALTER DATABASE [INEXFOLER] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [INEXFOLER] SET ARITHABORT OFF
GO
ALTER DATABASE [INEXFOLER] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [INEXFOLER] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [INEXFOLER] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [INEXFOLER] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [INEXFOLER] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [INEXFOLER] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [INEXFOLER] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [INEXFOLER] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [INEXFOLER] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [INEXFOLER] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [INEXFOLER] SET  DISABLE_BROKER
GO
ALTER DATABASE [INEXFOLER] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [INEXFOLER] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [INEXFOLER] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [INEXFOLER] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [INEXFOLER] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [INEXFOLER] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [INEXFOLER] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [INEXFOLER] SET  READ_WRITE
GO
ALTER DATABASE [INEXFOLER] SET RECOVERY SIMPLE
GO
ALTER DATABASE [INEXFOLER] SET  MULTI_USER
GO
ALTER DATABASE [INEXFOLER] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [INEXFOLER] SET DB_CHAINING OFF
GO
USE [INEXFOLER]
GO
/****** Object:  Table [dbo].[user]    Script Date: 08/14/2024 07:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[user](
	[id_user] [int] NOT NULL,
	[username] [varchar](50) NOT NULL,
	[nama_user] [nvarchar](100) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[status] [varchar](50) NOT NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[id_user] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[nilaikkp]    Script Date: 08/14/2024 07:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[nilaikkp](
	[id_kkp] [int] NULL,
	[nama_mahasiswa] [varchar](50) NULL,
	[kelas] [varchar](50) NULL,
	[prodi] [varchar](50) NULL,
	[disiplin] [int] NULL,
	[kerjasama] [int] NULL,
	[inisiatif] [int] NULL,
	[kerajinan] [int] NULL,
	[tanggung_jawab] [int] NULL,
	[sikap] [int] NULL,
	[prestasi] [int] NULL,
	[total] [int] NULL,
	[rata_rata] [int] NULL,
	[grade] [varchar](50) NULL,
	[nama_dosen] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[nilaikkn]    Script Date: 08/14/2024 07:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[nilaikkn](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_kkn] [int] NULL,
	[nama_mahasiswa] [varchar](50) NULL,
	[kelas] [varchar](50) NULL,
	[prodi] [varchar](50) NULL,
	[disiplin] [int] NULL,
	[kerjasama] [int] NULL,
	[inisiatif] [int] NULL,
	[kerajinan] [int] NULL,
	[tanggung_jawab] [int] NULL,
	[sikap] [int] NULL,
	[laporan] [int] NULL,
	[total] [int] NULL,
	[rata_rata] [int] NULL,
	[grade] [varchar](50) NULL,
	[nama_dosen] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[kkp]    Script Date: 08/14/2024 07:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[kkp](
	[id_kkp] [int] IDENTITY(1,1) NOT NULL,
	[nim] [varchar](20) NULL,
	[nama_mahasiswa] [nvarchar](100) NULL,
	[kelas] [varchar](50) NULL,
	[prodi] [varchar](50) NULL,
	[fakultas] [varchar](50) NULL,
	[angkatan] [varchar](50) NULL,
	[sks] [varchar](50) NULL,
	[ipk] [float] NULL,
	[penempatan] [varchar](max) NULL,
	[alamat] [varchar](max) NULL,
	[judul] [varchar](100) NULL,
	[durasi] [varchar](50) NULL,
	[nama_dosen] [varchar](100) NULL,
 CONSTRAINT [PK_kkp] PRIMARY KEY CLUSTERED 
(
	[id_kkp] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[kkn_h]    Script Date: 08/14/2024 07:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[kkn_h](
	[id_kkn] [int] IDENTITY(1,1) NOT NULL,
	[nama_kelompok] [varchar](50) NULL,
	[tempat] [varchar](max) NULL,
	[detail_lokasi] [varchar](max) NULL,
	[judul] [varchar](50) NULL,
	[durasi] [varchar](50) NULL,
	[nama_dosen] [varchar](100) NULL,
 CONSTRAINT [PK_kkn_h] PRIMARY KEY CLUSTERED 
(
	[id_kkn] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[kkn_d]    Script Date: 08/14/2024 07:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[kkn_d](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_kkn] [int] NULL,
	[nim] [varchar](20) NULL,
	[nama_mahasiswa] [nvarchar](100) NULL,
	[kelas] [varchar](50) NULL,
	[prodi] [varchar](50) NULL,
	[fakultas] [varchar](50) NULL,
	[angkatan] [varchar](50) NULL,
	[sks] [varchar](50) NULL,
	[ipk] [float] NULL,
 CONSTRAINT [PK_kkn_d] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[dosen]    Script Date: 08/14/2024 07:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[dosen](
	[id_dosen] [int] NULL,
	[nama_dosen] [varchar](100) NULL,
	[nidn] [varchar](50) NULL,
	[no_telp] [varchar](50) NULL,
	[email] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
