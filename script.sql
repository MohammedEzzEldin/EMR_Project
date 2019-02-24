USE [EMR_GP_DB]
GO
IF  EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'User', NULL,NULL))
EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User'
GO
IF  EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'User', N'COLUMN',N'User_Type'))
EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'User_Type'
GO
IF  EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'User', N'COLUMN',N'Email'))
EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Email'
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Reviews_Patient_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Reviews]'))
ALTER TABLE [dbo].[Reviews] DROP CONSTRAINT [FK_Reviews_Patient_Id]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Reviews_Medical_Org_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Reviews]'))
ALTER TABLE [dbo].[Reviews] DROP CONSTRAINT [FK_Reviews_Medical_Org_Id]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Reviews_Doctor_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Reviews]'))
ALTER TABLE [dbo].[Reviews] DROP CONSTRAINT [FK_Reviews_Doctor_Id]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Person_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Person]'))
ALTER TABLE [dbo].[Person] DROP CONSTRAINT [FK_Person_User]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permission_Patient]') AND parent_object_id = OBJECT_ID(N'[dbo].[Permission]'))
ALTER TABLE [dbo].[Permission] DROP CONSTRAINT [FK_Permission_Patient]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permission_Doctor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Permission]'))
ALTER TABLE [dbo].[Permission] DROP CONSTRAINT [FK_Permission_Doctor]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Patient_Person]') AND parent_object_id = OBJECT_ID(N'[dbo].[Patient]'))
ALTER TABLE [dbo].[Patient] DROP CONSTRAINT [FK_Patient_Person]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Operations_PatientId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Operations]'))
ALTER TABLE [dbo].[Operations] DROP CONSTRAINT [FK_Operations_PatientId]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Operations_Medical_Org_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Operations]'))
ALTER TABLE [dbo].[Operations] DROP CONSTRAINT [FK_Operations_Medical_Org_Id]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Operations_Doctor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Operations]'))
ALTER TABLE [dbo].[Operations] DROP CONSTRAINT [FK_Operations_Doctor]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Medical_Organization_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Medical_Organization]'))
ALTER TABLE [dbo].[Medical_Organization] DROP CONSTRAINT [FK_Medical_Organization_UserId]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Labs_Patient]') AND parent_object_id = OBJECT_ID(N'[dbo].[Labs]'))
ALTER TABLE [dbo].[Labs] DROP CONSTRAINT [FK_Labs_Patient]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Labs_Org]') AND parent_object_id = OBJECT_ID(N'[dbo].[Labs]'))
ALTER TABLE [dbo].[Labs] DROP CONSTRAINT [FK_Labs_Org]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_General_Examination_Patient]') AND parent_object_id = OBJECT_ID(N'[dbo].[General_Examination]'))
ALTER TABLE [dbo].[General_Examination] DROP CONSTRAINT [FK_General_Examination_Patient]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Family_History_Patient]') AND parent_object_id = OBJECT_ID(N'[dbo].[Family_History]'))
ALTER TABLE [dbo].[Family_History] DROP CONSTRAINT [FK_Family_History_Patient]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Examination_Patient]') AND parent_object_id = OBJECT_ID(N'[dbo].[Examination]'))
ALTER TABLE [dbo].[Examination] DROP CONSTRAINT [FK_Examination_Patient]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Examination_Medical_Org]') AND parent_object_id = OBJECT_ID(N'[dbo].[Examination]'))
ALTER TABLE [dbo].[Examination] DROP CONSTRAINT [FK_Examination_Medical_Org]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Examination_Doctor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Examination]'))
ALTER TABLE [dbo].[Examination] DROP CONSTRAINT [FK_Examination_Doctor]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evaluation_Medical_Org_Patient_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Evaluation_Medical_Org]'))
ALTER TABLE [dbo].[Evaluation_Medical_Org] DROP CONSTRAINT [FK_Evaluation_Medical_Org_Patient_Id]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evaluation_Medical_Org_Org_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Evaluation_Medical_Org]'))
ALTER TABLE [dbo].[Evaluation_Medical_Org] DROP CONSTRAINT [FK_Evaluation_Medical_Org_Org_Id]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evaluation_Doctor_PatientId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Evaluation_Doctor]'))
ALTER TABLE [dbo].[Evaluation_Doctor] DROP CONSTRAINT [FK_Evaluation_Doctor_PatientId]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evaluation_Doctor_DoctorId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Evaluation_Doctor]'))
ALTER TABLE [dbo].[Evaluation_Doctor] DROP CONSTRAINT [FK_Evaluation_Doctor_DoctorId]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Doctor_Person]') AND parent_object_id = OBJECT_ID(N'[dbo].[Doctor]'))
ALTER TABLE [dbo].[Doctor] DROP CONSTRAINT [FK_Doctor_Person]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Doctor_Department]') AND parent_object_id = OBJECT_ID(N'[dbo].[Doctor]'))
ALTER TABLE [dbo].[Doctor] DROP CONSTRAINT [FK_Doctor_Department]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Diseases_Patient]') AND parent_object_id = OBJECT_ID(N'[dbo].[Diseases]'))
ALTER TABLE [dbo].[Diseases] DROP CONSTRAINT [FK_Diseases_Patient]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Department_Medical_Org]') AND parent_object_id = OBJECT_ID(N'[dbo].[Department]'))
ALTER TABLE [dbo].[Department] DROP CONSTRAINT [FK_Department_Medical_Org]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Child_FollowUp_Form_Patient]') AND parent_object_id = OBJECT_ID(N'[dbo].[Child_FollowUp_Form]'))
ALTER TABLE [dbo].[Child_FollowUp_Form] DROP CONSTRAINT [FK_Child_FollowUp_Form_Patient]
GO
/****** Object:  Index [Unique_UserName]    Script Date: 10/2/2019 10:10:06 PM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND name = N'Unique_UserName')
DROP INDEX [Unique_UserName] ON [dbo].[User]
GO
/****** Object:  Index [National_Id]    Script Date: 10/2/2019 10:10:06 PM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Person]') AND name = N'National_Id')
DROP INDEX [National_Id] ON [dbo].[Person]
GO
/****** Object:  Table [dbo].[User]    Script Date: 10/2/2019 10:10:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[Reviews]    Script Date: 10/2/2019 10:10:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Reviews]') AND type in (N'U'))
DROP TABLE [dbo].[Reviews]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 10/2/2019 10:10:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Person]') AND type in (N'U'))
DROP TABLE [dbo].[Person]
GO
/****** Object:  Table [dbo].[Permission]    Script Date: 10/2/2019 10:10:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Permission]') AND type in (N'U'))
DROP TABLE [dbo].[Permission]
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 10/2/2019 10:10:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Patient]') AND type in (N'U'))
DROP TABLE [dbo].[Patient]
GO
/****** Object:  Table [dbo].[Operations]    Script Date: 10/2/2019 10:10:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Operations]') AND type in (N'U'))
DROP TABLE [dbo].[Operations]
GO
/****** Object:  Table [dbo].[Medical_Organization]    Script Date: 10/2/2019 10:10:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Medical_Organization]') AND type in (N'U'))
DROP TABLE [dbo].[Medical_Organization]
GO
/****** Object:  Table [dbo].[Labs]    Script Date: 10/2/2019 10:10:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Labs]') AND type in (N'U'))
DROP TABLE [dbo].[Labs]
GO
/****** Object:  Table [dbo].[General_Examination]    Script Date: 10/2/2019 10:10:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[General_Examination]') AND type in (N'U'))
DROP TABLE [dbo].[General_Examination]
GO
/****** Object:  Table [dbo].[Family_History]    Script Date: 10/2/2019 10:10:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Family_History]') AND type in (N'U'))
DROP TABLE [dbo].[Family_History]
GO
/****** Object:  Table [dbo].[Examination]    Script Date: 10/2/2019 10:10:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Examination]') AND type in (N'U'))
DROP TABLE [dbo].[Examination]
GO
/****** Object:  Table [dbo].[Evaluation_Medical_Org]    Script Date: 10/2/2019 10:10:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Evaluation_Medical_Org]') AND type in (N'U'))
DROP TABLE [dbo].[Evaluation_Medical_Org]
GO
/****** Object:  Table [dbo].[Evaluation_Doctor]    Script Date: 10/2/2019 10:10:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Evaluation_Doctor]') AND type in (N'U'))
DROP TABLE [dbo].[Evaluation_Doctor]
GO
/****** Object:  Table [dbo].[Doctor]    Script Date: 10/2/2019 10:10:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doctor]') AND type in (N'U'))
DROP TABLE [dbo].[Doctor]
GO
/****** Object:  Table [dbo].[Diseases]    Script Date: 10/2/2019 10:10:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Diseases]') AND type in (N'U'))
DROP TABLE [dbo].[Diseases]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 10/2/2019 10:10:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Department]') AND type in (N'U'))
DROP TABLE [dbo].[Department]
GO
/****** Object:  Table [dbo].[Child_FollowUp_Form]    Script Date: 10/2/2019 10:10:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Child_FollowUp_Form]') AND type in (N'U'))
DROP TABLE [dbo].[Child_FollowUp_Form]
GO
USE [master]
GO
/****** Object:  Database [EMR_GP_DB]    Script Date: 10/2/2019 10:10:06 PM ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'EMR_GP_DB')
DROP DATABASE [EMR_GP_DB]
GO
/****** Object:  Database [EMR_GP_DB]    Script Date: 10/2/2019 10:10:06 PM ******/
CREATE DATABASE [EMR_GP_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EMR_GP_DB', FILENAME = N'D:\Graduation Project\Database\EMR_GP_DB.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB ), 
 FILEGROUP [Medical_Doctor_Department] 
( NAME = N'Medical_Doctor_Department', FILENAME = N'D:\Graduation Project\Database\Medical_Doctor_Department.ndf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB ), 
 FILEGROUP [Patient] 
( NAME = N'Patient', FILENAME = N'D:\Graduation Project\Database\Patient.ndf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'EMR_GP_DB_log', FILENAME = N'D:\Graduation Project\Database\EMR_GP_DB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [EMR_GP_DB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EMR_GP_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EMR_GP_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EMR_GP_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EMR_GP_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EMR_GP_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EMR_GP_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [EMR_GP_DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EMR_GP_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EMR_GP_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EMR_GP_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EMR_GP_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EMR_GP_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EMR_GP_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EMR_GP_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EMR_GP_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EMR_GP_DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EMR_GP_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EMR_GP_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EMR_GP_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EMR_GP_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EMR_GP_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EMR_GP_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EMR_GP_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EMR_GP_DB] SET RECOVERY FULL 
GO
ALTER DATABASE [EMR_GP_DB] SET  MULTI_USER 
GO
ALTER DATABASE [EMR_GP_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EMR_GP_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EMR_GP_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EMR_GP_DB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [EMR_GP_DB] SET DELAYED_DURABILITY = DISABLED 
GO
USE [EMR_GP_DB]
GO
/****** Object:  Table [dbo].[Child_FollowUp_Form]    Script Date: 10/2/2019 10:10:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Child_FollowUp_Form](
	[Patient_Id] [bigint] NOT NULL,
	[Age_In_Month] [int] NOT NULL,
	[Feed_Type] [nchar](100) NOT NULL,
	[Vaccination_Type] [nchar](100) NULL,
	[Vaccination_Date] [date] NULL,
 CONSTRAINT [PK_Child_FollowUp_Form] PRIMARY KEY CLUSTERED 
(
	[Patient_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [Patient]
) ON [Patient]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 10/2/2019 10:10:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[Department_Name] [nchar](50) NOT NULL,
	[Medical_Org_Id] [bigint] NOT NULL,
	[Department_Id] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[Department_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [Medical_Doctor_Department]
) ON [Medical_Doctor_Department]
GO
/****** Object:  Table [dbo].[Diseases]    Script Date: 10/2/2019 10:10:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Diseases](
	[Patient_Id] [bigint] NOT NULL,
	[Disease_Name] [nchar](50) NOT NULL,
	[Disease_Type] [nchar](50) NOT NULL
) ON [Patient]
GO
/****** Object:  Table [dbo].[Doctor]    Script Date: 10/2/2019 10:10:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctor](
	[Doctor_Id] [bigint] NOT NULL,
	[Medium_Rate] [float] NULL,
	[Acadimic_Degree] [nchar](50) NULL,
	[Functional_Degree] [nchar](50) NULL,
	[Spacialization] [nchar](50) NULL,
	[Medical_Org_Id] [bigint] NOT NULL,
	[Department_Id] [bigint] NULL,
 CONSTRAINT [PK_Doctor] PRIMARY KEY CLUSTERED 
(
	[Doctor_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [Medical_Doctor_Department]
) ON [Medical_Doctor_Department]
GO
/****** Object:  Table [dbo].[Evaluation_Doctor]    Script Date: 10/2/2019 10:10:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Evaluation_Doctor](
	[Doctor_Id] [bigint] NOT NULL,
	[Patient_Id] [bigint] NOT NULL,
	[Rate] [float] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Evaluation_Medical_Org]    Script Date: 10/2/2019 10:10:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Evaluation_Medical_Org](
	[Medical_Org_Id] [bigint] NOT NULL,
	[Patient_Id] [bigint] NOT NULL,
	[Rate] [float] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Examination]    Script Date: 10/2/2019 10:10:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Examination](
	[Patient_Id] [bigint] NOT NULL,
	[Doctor_Id] [bigint] NOT NULL,
	[Medical_Org_Id] [bigint] NOT NULL,
	[exm_date] [date] NOT NULL,
	[exm_description_result] [nchar](500) NULL,
	[exm_midicine] [nchar](500) NULL
) ON [Patient]
GO
/****** Object:  Table [dbo].[Family_History]    Script Date: 10/2/2019 10:10:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Family_History](
	[Patient_Id] [bigint] NOT NULL,
	[Disease_Name] [nchar](50) NOT NULL,
	[Disease_Type] [nchar](50) NOT NULL
) ON [Patient]
GO
/****** Object:  Table [dbo].[General_Examination]    Script Date: 10/2/2019 10:10:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[General_Examination](
	[Patient_Id] [bigint] NOT NULL,
	[Length] [int] NULL,
	[Weight] [float] NULL,
	[Blood_Type] [nchar](3) NULL,
	[Blood_Pressure] [nchar](50) NULL,
	[Diabetes] [int] NULL,
	[Habits] [nchar](200) NULL,
	[Temperature] [int] NULL,
	[Allergies] [nchar](200) NULL,
 CONSTRAINT [PK_General_Examination] PRIMARY KEY CLUSTERED 
(
	[Patient_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [Patient]
) ON [Patient]
GO
/****** Object:  Table [dbo].[Labs]    Script Date: 10/2/2019 10:10:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Labs](
	[Patient_Id] [bigint] NOT NULL,
	[Org_Id] [bigint] NOT NULL,
	[Lab_Name] [nchar](100) NOT NULL,
	[Lab_Description_Result] [nchar](500) NULL,
	[Lab_Type] [nchar](50) NOT NULL,
	[Lab_Date] [date] NULL,
	[Lab_File] [nchar](200) NULL
) ON [Patient]
GO
/****** Object:  Table [dbo].[Medical_Organization]    Script Date: 10/2/2019 10:10:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medical_Organization](
	[Medical_Org_Id] [bigint] NOT NULL,
	[Medical_Org_Name] [nchar](100) NOT NULL,
	[Medium_Rate] [float] NULL,
 CONSTRAINT [PK_Medical_Organization] PRIMARY KEY CLUSTERED 
(
	[Medical_Org_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [Medical_Doctor_Department]
) ON [Medical_Doctor_Department]
GO
/****** Object:  Table [dbo].[Operations]    Script Date: 10/2/2019 10:10:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operations](
	[Patient_Id] [bigint] NOT NULL,
	[Op_Name] [nchar](100) NOT NULL,
	[Op_Type] [nchar](100) NOT NULL,
	[Op_Date] [date] NULL,
	[Doctor_Id] [bigint] NULL,
	[Organization_Id] [bigint] NULL
) ON [Patient]
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 10/2/2019 10:10:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient](
	[Patient_Id] [bigint] NOT NULL,
	[Learning_Status] [nchar](90) NULL,
	[Social_Status] [nchar](90) NULL,
	[Death_Date] [date] NULL,
	[Alternative_Phone] [nchar](14) NULL,
	[Job] [nchar](50) NULL,
	[Reason_Death] [nchar](500) NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[Patient_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [Patient]
) ON [Patient]
GO
/****** Object:  Table [dbo].[Permission]    Script Date: 10/2/2019 10:10:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permission](
	[Patient_Id] [bigint] NOT NULL,
	[Doctor_Id] [bigint] NOT NULL,
	[Permission] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 10/2/2019 10:10:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[Person_Id] [bigint] NOT NULL,
	[First_Name] [nchar](100) NOT NULL,
	[Last_Name] [nchar](100) NOT NULL,
	[Birth_Date] [date] NOT NULL,
	[National_Id] [nchar](14) NOT NULL,
	[Nationality] [nchar](50) NULL,
	[Gender] [nchar](6) NOT NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[Person_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reviews]    Script Date: 10/2/2019 10:10:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reviews](
	[Patient_Id] [bigint] NOT NULL,
	[Doctor_Id] [bigint] NOT NULL,
	[Organization_Id] [bigint] NOT NULL,
	[rev_date] [date] NOT NULL,
	[rev_midicine] [nchar](500) NULL,
	[rev_description_result] [nchar](500) NULL
) ON [Patient]
GO
/****** Object:  Table [dbo].[User]    Script Date: 10/2/2019 10:10:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[User_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Email] [nchar](200) NOT NULL,
	[Address] [nchar](200) NULL,
	[Phone] [nchar](15) NULL,
	[Last_Date_Of_Login] [date] NULL,
	[User_Type] [int] NOT NULL,
	[Password] [nchar](15) NOT NULL,
	[Status_Of_Account] [int] NOT NULL,
	[City] [nchar](20) NULL,
	[Photo_Url] [nchar](500) NULL,
 CONSTRAINT [PK_UserId] PRIMARY KEY CLUSTERED 
(
	[User_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (2, N'ALHayat                                                                                             ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (3, N'The Future                                                                                          ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (4, N'AL Nour for Eyes                                                                                    ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (5, N'AL Hamad Charity                                                                                    ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (6, N'Tabarak Children Hospital                                                                           ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (7, N'57357 For Children Cancer                                                                           ', 5)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (8, N'AL Galaa Teaching Hospital                                                                          ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (9, N'AL Durrah Heart Care                                                                                ', 5)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (10, N'Misr International                                                                                  ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (11, N'ALGabry                                                                                             ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (12, N'Ahmed Maher Teaching Hospital                                                                       ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (13, N'Demerdash Teaching Hospital                                                                         ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (14, N'Abbasiya Mental Health                                                                              ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (15, N'National Cancer Institute                                                                           ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (16, N'Misr El Amal                                                                                        ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (17, N'Shrouq Hospital                                                                                     ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (18, N'Dounieh Eye Hospital                                                                                ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (19, N'AL Salam International                                                                              ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (20, N'Dar El Fouad Hospital                                                                               ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (21, N'Medical Center for Arab Contractors                                                                 ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (22, N'Hewlan Universtiy Hospital                                                                          ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (23, N'Saudi German Hospital                                                                               ', 5)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (24, N'Ganzouri Specialist Hospital                                                                        ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (25, N'Ain Shams University Specialized Hospital                                                           ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (26, N'Cleopatra                                                                                           ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (27, N'Badr                                                                                                ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (28, N'Dar EL Hekma Charity                                                                                ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (29, N'Ahram                                                                                               ', 0)
INSERT [dbo].[Patient] ([Patient_Id], [Learning_Status], [Social_Status], [Death_Date], [Alternative_Phone], [Job], [Reason_Death]) VALUES (30, N'Bachelor                                                                                  ', N'Single                                                                                    ', NULL, NULL, N'Student at FCIH                                   ', NULL)
INSERT [dbo].[Patient] ([Patient_Id], [Learning_Status], [Social_Status], [Death_Date], [Alternative_Phone], [Job], [Reason_Death]) VALUES (31, N'Bachelor                                                                                  ', N'Single                                                                                    ', NULL, NULL, N'Student at FCIH                                   ', NULL)
INSERT [dbo].[Patient] ([Patient_Id], [Learning_Status], [Social_Status], [Death_Date], [Alternative_Phone], [Job], [Reason_Death]) VALUES (32, N'Bachelor                                                                                  ', N'Single                                                                                    ', NULL, NULL, N'Student at FCIH                                   ', NULL)
INSERT [dbo].[Patient] ([Patient_Id], [Learning_Status], [Social_Status], [Death_Date], [Alternative_Phone], [Job], [Reason_Death]) VALUES (33, N'Bachelor                                                                                  ', N'Single                                                                                    ', NULL, N'              ', N'Student at FCIH                                   ', NULL)
INSERT [dbo].[Patient] ([Patient_Id], [Learning_Status], [Social_Status], [Death_Date], [Alternative_Phone], [Job], [Reason_Death]) VALUES (34, N'Bachelor                                                                                  ', N'Married                                                                                   ', NULL, N'              ', N'Arabic Teacher                                    ', NULL)
INSERT [dbo].[Patient] ([Patient_Id], [Learning_Status], [Social_Status], [Death_Date], [Alternative_Phone], [Job], [Reason_Death]) VALUES (35, N'Bachelor                                                                                  ', N'Married                                                                                   ', NULL, N'              ', N'Football Player                                   ', NULL)
INSERT [dbo].[Person] ([Person_Id], [First_Name], [Last_Name], [Birth_Date], [National_Id], [Nationality], [Gender]) VALUES (1, N'Mohammad                                                                                            ', N'Mahmoud                                                                                             ', CAST(N'1997-03-09' AS Date), N'10234567892134', N'Egypt                                             ', N'male  ')
INSERT [dbo].[Person] ([Person_Id], [First_Name], [Last_Name], [Birth_Date], [National_Id], [Nationality], [Gender]) VALUES (30, N'Ahmed Tareek                                                                                        ', N'Faud                                                                                                ', CAST(N'1997-01-01' AS Date), N'01234567891234', N'Egypt                                             ', N'male  ')
INSERT [dbo].[Person] ([Person_Id], [First_Name], [Last_Name], [Birth_Date], [National_Id], [Nationality], [Gender]) VALUES (31, N'Mohamed                                                                                             ', N'Rabia                                                                                               ', CAST(N'1996-10-01' AS Date), N'98765412301346', N'Egypt                                             ', N'male  ')
INSERT [dbo].[Person] ([Person_Id], [First_Name], [Last_Name], [Birth_Date], [National_Id], [Nationality], [Gender]) VALUES (32, N'Mario                                                                                               ', N'Elhamy                                                                                              ', CAST(N'1999-06-02' AS Date), N'12378964325876', N'Egypt                                             ', N'male  ')
INSERT [dbo].[Person] ([Person_Id], [First_Name], [Last_Name], [Birth_Date], [National_Id], [Nationality], [Gender]) VALUES (33, N'Hossam                                                                                              ', N'Hassan                                                                                              ', CAST(N'1998-05-03' AS Date), N'63795781264869', N'Egypt                                             ', N'male  ')
INSERT [dbo].[Person] ([Person_Id], [First_Name], [Last_Name], [Birth_Date], [National_Id], [Nationality], [Gender]) VALUES (34, N'Hassan                                                                                              ', N'AL Saff                                                                                             ', CAST(N'1985-07-03' AS Date), N'37699812245646', N'Egypt                                             ', N'male  ')
INSERT [dbo].[Person] ([Person_Id], [First_Name], [Last_Name], [Birth_Date], [National_Id], [Nationality], [Gender]) VALUES (35, N'Islam                                                                                               ', N'Mohareb                                                                                             ', CAST(N'1995-06-03' AS Date), N'67851234569222', N'Egypt                                             ', N'male  ')
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (1, N'mohammad_mahmoudezeldin@yahoo.com                                                                                                                                                                       ', N'Cairo                                                                                                                                                                                                   ', N'00201063834250 ', CAST(N'1977-09-03' AS Date), 1, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (2, N'Al_Hayat@medical.org                                                                                                                                                                                    ', N'Egypt-Cairo-Helwan                                                                                                                                                                                      ', N'00201063834250 ', NULL, 2, N'0123456789     ', 0, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (3, N'The_Future@future.org                                                                                                                                                                                   ', N'Egypt-Cairo-Maadi                                                                                                                                                                                       ', N'00201063834250 ', NULL, 2, N'0123456789     ', 0, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (4, N'ALNourForEyes@alnour.org                                                                                                                                                                                ', N'Egypt-Giza-AL Mohandeseen                                                                                                                                                                               ', N'00201063834250 ', NULL, 2, N'0123456789     ', 0, N'Giza                ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (5, N'ALHamad_Charity@gmail.com                                                                                                                                                                               ', N'Egypt-Giza-King Faisal Street-In front of Ragab Sons                                                                                                                                                    ', N'0237803782     ', NULL, 2, N'0123456789     ', 0, N'Giza                ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (6, N'Tabarak@gmail.com                                                                                                                                                                                       ', N'Egypt-Giza-King Faisal Street-434                                                                                                                                                                       ', N'0235830647     ', NULL, 2, N'0123456789     ', 0, N'Giza                ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (7, N'57357@gmail.com                                                                                                                                                                                         ', N'Egypt-Cairo-Zainham-ALSayedah Zainab                                                                                                                                                                    ', N'0225351500     ', NULL, 2, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (8, N'ELGLAA@yahoo.com                                                                                                                                                                                        ', N'Cairo-41 26th Of July St., Intersection Of El Galaa St.                                                                                                                                                 ', N'25756474       ', NULL, 2, N'0123456789     ', 0, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (9, N'Durrah_Heart@yahoo.com                                                                                                                                                                                  ', N'Cairo-Hiloplis-Hegaz Square-91 Mohamed Farid St                                                                                                                                                         ', N'0222411110     ', NULL, 2, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (10, N'MisrInternational@yahoo.com                                                                                                                                                                             ', N'Giza-Dokki-12 El Saraya St                                                                                                                                                                              ', N'0237608261     ', NULL, 2, N'0123456789     ', 0, N'Giza                ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (11, N'ALGabry@yahoo.com                                                                                                                                                                                       ', N'Egypt-Giza-Faisal St                                                                                                                                                                                    ', NULL, NULL, 2, N'0123456789     ', 0, N'Giza                ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (12, N'Ahmed_Maher@gmail.com                                                                                                                                                                                   ', N'Cairo , Port Said St., Bab El Khalq, 341                                                                                                                                                                ', N' 0223911838    ', NULL, 2, N'0123456789     ', 0, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (13, N'Demerdash@AinShams.edu.eg                                                                                                                                                                               ', N'Egypt-56 Ramses St. Abbasieh Cairo                                                                                                                                                                      ', N'00201095811849 ', NULL, 2, N'0123456789     ', 0, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (14, N'AbbasiyaMentalHealth@gmail.com                                                                                                                                                                          ', N'Stadium, Nasr City, Cairo , Egypt                                                                                                                                                                       ', N'01289109829    ', NULL, 2, N'0123456789     ', 0, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (15, N'NationalCancerInstitute@gmail.com                                                                                                                                                                       ', N'Egypt, Cairo,Kasr Al Eini، Street، Fom El Khalig                                                                                                                                                        ', N'00201208285664 ', NULL, 2, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (16, N'MisrElAmal@gmail.com                                                                                                                                                                                    ', N'Egypt,Giza,Al Omraneyah                                                                                                                                                                                 ', N'0233479212     ', NULL, 2, N'0123456789     ', 0, N'Giza                ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (17, N'ShrouqCity@gmail.com                                                                                                                                                                                    ', N'Egypt, Cairo, Shrouq City                                                                                                                                                                               ', NULL, NULL, 2, N'01234567899    ', 0, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (18, N'Dounieh@gmail.com                                                                                                                                                                                       ', N'Egypt-Giza-Dokki-Tahrir St                                                                                                                                                                              ', N'01020524528    ', NULL, 2, N'0123456789     ', 0, N'Giza                ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (19, N'ALSalamInternational@gmail.com                                                                                                                                                                          ', N'Corniche El Nil Street - Maadi - Cairo - Egypt                                                                                                                                                          ', N'0225240250     ', NULL, 2, N'0123456789     ', 0, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (20, N'DarElFouad@gmail.com                                                                                                                                                                                    ', N'Touristic Region - 6th Of October - Giza - Egypt                                                                                                                                                        ', N'02-38356028    ', NULL, 2, N'0123456789     ', 0, N'Giza                ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (21, N'ArabContractors@gmail.com                                                                                                                                                                               ', N'The green mountain next to the ski club - Nasr City - Cairo - Egypt                                                                                                                                     ', N'02-23424560    ', NULL, 2, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (22, N'HelwanUniverstiy@gmail.com                                                                                                                                                                              ', N'Egypt - Ain Helwan                                                                                                                                                                                      ', NULL, NULL, 2, N'0123456789     ', 0, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (23, N'info@sghcairo.com                                                                                                                                                                                       ', N'Joseph Tito Street, Airport Road, El Nozha El Gadida, Cairo                                                                                                                                             ', N'16259          ', NULL, 2, N'0123456789     ', 0, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (24, N'info@ganzourihospital.com                                                                                                                                                                               ', N'63 Toman Bay Street, Tahra Palace Square, Roxy, Cairo                                                                                                                                                   ', N'0222588810     ', NULL, 2, N'0123456789     ', 0, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (25, N'Hospital@AinShams.edu.eg                                                                                                                                                                                ', N' Al-Khalifa Al-Maamoon Street, Abbasiya, Cairo                                                                                                                                                          ', N'01284472343    ', NULL, 2, N'0123456789     ', 0, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (26, N'Info@Cleopatra.org                                                                                                                                                                                      ', N'39 Cleopatra St., Salah El Din Sq., Heliopolis, Cairo                                                                                                                                                   ', N'0224178206     ', NULL, 2, N'0123456789     ', 0, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (27, N'Info@Badr.com                                                                                                                                                                                           ', N'Cairo , Badr City                                                                                                                                                                                       ', NULL, NULL, 2, N'0123456789     ', 0, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (28, N'Info@DarElhekma.com                                                                                                                                                                                     ', N'Cairo, Nasr City-Egypt                                                                                                                                                                                  ', NULL, NULL, 2, N'0123456789     ', 0, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (29, N'Info@Ahram.com                                                                                                                                                                                          ', N'Egypt - Giza                                                                                                                                                                                            ', NULL, NULL, 2, N'0123456789     ', 0, N'Giza                ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (30, N'Ahmed_Tareek@yahoo.com                                                                                                                                                                                  ', N'Egypt-Cairo-Helwan                                                                                                                                                                                      ', NULL, NULL, 4, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (31, N'mohamed_rabie@gmail.com                                                                                                                                                                                 ', N'Egypt - Menoufia                                                                                                                                                                                        ', NULL, NULL, 4, N'0123456789     ', 1, N'Menoufia            ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (32, N'mario@gmail.com                                                                                                                                                                                         ', N'Egypt-Cairo-Helwan                                                                                                                                                                                      ', NULL, NULL, 4, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (33, N'ALAmeed@gmail.com                                                                                                                                                                                       ', N'Egypt - Cairo - AL Maadi                                                                                                                                                                                ', NULL, NULL, 4, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (34, N'Hassan_ALSaff@yahoo.com                                                                                                                                                                                 ', N'Egypt - Cairo - Ain Shams                                                                                                                                                                               ', NULL, NULL, 4, N'0123456789     ', 1, N'Cairo               ', N'4.jpg                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               ')
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (35, N'Islam_Mohareb@gmail.com                                                                                                                                                                                 ', N'Egypt - Cairo - Nasr City                                                                                                                                                                               ', NULL, NULL, 4, N'0123456789     ', 1, N'Cairo               ', N'حصى.jpg                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             ')
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  Index [National_Id]    Script Date: 10/2/2019 10:10:07 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [National_Id] ON [dbo].[Person]
(
	[Person_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Unique_UserName]    Script Date: 10/2/2019 10:10:07 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [Unique_UserName] ON [dbo].[User]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Child_FollowUp_Form]  WITH CHECK ADD  CONSTRAINT [FK_Child_FollowUp_Form_Patient] FOREIGN KEY([Patient_Id])
REFERENCES [dbo].[Patient] ([Patient_Id])
GO
ALTER TABLE [dbo].[Child_FollowUp_Form] CHECK CONSTRAINT [FK_Child_FollowUp_Form_Patient]
GO
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_Medical_Org] FOREIGN KEY([Medical_Org_Id])
REFERENCES [dbo].[Medical_Organization] ([Medical_Org_Id])
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_Medical_Org]
GO
ALTER TABLE [dbo].[Diseases]  WITH CHECK ADD  CONSTRAINT [FK_Diseases_Patient] FOREIGN KEY([Patient_Id])
REFERENCES [dbo].[Patient] ([Patient_Id])
GO
ALTER TABLE [dbo].[Diseases] CHECK CONSTRAINT [FK_Diseases_Patient]
GO
ALTER TABLE [dbo].[Doctor]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_Department] FOREIGN KEY([Department_Id])
REFERENCES [dbo].[Department] ([Department_Id])
GO
ALTER TABLE [dbo].[Doctor] CHECK CONSTRAINT [FK_Doctor_Department]
GO
ALTER TABLE [dbo].[Doctor]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_Person] FOREIGN KEY([Doctor_Id])
REFERENCES [dbo].[Person] ([Person_Id])
GO
ALTER TABLE [dbo].[Doctor] CHECK CONSTRAINT [FK_Doctor_Person]
GO
ALTER TABLE [dbo].[Evaluation_Doctor]  WITH CHECK ADD  CONSTRAINT [FK_Evaluation_Doctor_DoctorId] FOREIGN KEY([Doctor_Id])
REFERENCES [dbo].[Doctor] ([Doctor_Id])
GO
ALTER TABLE [dbo].[Evaluation_Doctor] CHECK CONSTRAINT [FK_Evaluation_Doctor_DoctorId]
GO
ALTER TABLE [dbo].[Evaluation_Doctor]  WITH CHECK ADD  CONSTRAINT [FK_Evaluation_Doctor_PatientId] FOREIGN KEY([Patient_Id])
REFERENCES [dbo].[Patient] ([Patient_Id])
GO
ALTER TABLE [dbo].[Evaluation_Doctor] CHECK CONSTRAINT [FK_Evaluation_Doctor_PatientId]
GO
ALTER TABLE [dbo].[Evaluation_Medical_Org]  WITH CHECK ADD  CONSTRAINT [FK_Evaluation_Medical_Org_Org_Id] FOREIGN KEY([Medical_Org_Id])
REFERENCES [dbo].[Medical_Organization] ([Medical_Org_Id])
GO
ALTER TABLE [dbo].[Evaluation_Medical_Org] CHECK CONSTRAINT [FK_Evaluation_Medical_Org_Org_Id]
GO
ALTER TABLE [dbo].[Evaluation_Medical_Org]  WITH CHECK ADD  CONSTRAINT [FK_Evaluation_Medical_Org_Patient_Id] FOREIGN KEY([Patient_Id])
REFERENCES [dbo].[Patient] ([Patient_Id])
GO
ALTER TABLE [dbo].[Evaluation_Medical_Org] CHECK CONSTRAINT [FK_Evaluation_Medical_Org_Patient_Id]
GO
ALTER TABLE [dbo].[Examination]  WITH CHECK ADD  CONSTRAINT [FK_Examination_Doctor] FOREIGN KEY([Doctor_Id])
REFERENCES [dbo].[Doctor] ([Doctor_Id])
GO
ALTER TABLE [dbo].[Examination] CHECK CONSTRAINT [FK_Examination_Doctor]
GO
ALTER TABLE [dbo].[Examination]  WITH CHECK ADD  CONSTRAINT [FK_Examination_Medical_Org] FOREIGN KEY([Medical_Org_Id])
REFERENCES [dbo].[Medical_Organization] ([Medical_Org_Id])
GO
ALTER TABLE [dbo].[Examination] CHECK CONSTRAINT [FK_Examination_Medical_Org]
GO
ALTER TABLE [dbo].[Examination]  WITH CHECK ADD  CONSTRAINT [FK_Examination_Patient] FOREIGN KEY([Patient_Id])
REFERENCES [dbo].[Patient] ([Patient_Id])
GO
ALTER TABLE [dbo].[Examination] CHECK CONSTRAINT [FK_Examination_Patient]
GO
ALTER TABLE [dbo].[Family_History]  WITH CHECK ADD  CONSTRAINT [FK_Family_History_Patient] FOREIGN KEY([Patient_Id])
REFERENCES [dbo].[Patient] ([Patient_Id])
GO
ALTER TABLE [dbo].[Family_History] CHECK CONSTRAINT [FK_Family_History_Patient]
GO
ALTER TABLE [dbo].[General_Examination]  WITH CHECK ADD  CONSTRAINT [FK_General_Examination_Patient] FOREIGN KEY([Patient_Id])
REFERENCES [dbo].[Patient] ([Patient_Id])
GO
ALTER TABLE [dbo].[General_Examination] CHECK CONSTRAINT [FK_General_Examination_Patient]
GO
ALTER TABLE [dbo].[Labs]  WITH CHECK ADD  CONSTRAINT [FK_Labs_Org] FOREIGN KEY([Org_Id])
REFERENCES [dbo].[Medical_Organization] ([Medical_Org_Id])
GO
ALTER TABLE [dbo].[Labs] CHECK CONSTRAINT [FK_Labs_Org]
GO
ALTER TABLE [dbo].[Labs]  WITH CHECK ADD  CONSTRAINT [FK_Labs_Patient] FOREIGN KEY([Patient_Id])
REFERENCES [dbo].[Patient] ([Patient_Id])
GO
ALTER TABLE [dbo].[Labs] CHECK CONSTRAINT [FK_Labs_Patient]
GO
ALTER TABLE [dbo].[Medical_Organization]  WITH CHECK ADD  CONSTRAINT [FK_Medical_Organization_UserId] FOREIGN KEY([Medical_Org_Id])
REFERENCES [dbo].[User] ([User_Id])
GO
ALTER TABLE [dbo].[Medical_Organization] CHECK CONSTRAINT [FK_Medical_Organization_UserId]
GO
ALTER TABLE [dbo].[Operations]  WITH CHECK ADD  CONSTRAINT [FK_Operations_Doctor] FOREIGN KEY([Doctor_Id])
REFERENCES [dbo].[Doctor] ([Doctor_Id])
GO
ALTER TABLE [dbo].[Operations] CHECK CONSTRAINT [FK_Operations_Doctor]
GO
ALTER TABLE [dbo].[Operations]  WITH CHECK ADD  CONSTRAINT [FK_Operations_Medical_Org_Id] FOREIGN KEY([Organization_Id])
REFERENCES [dbo].[Medical_Organization] ([Medical_Org_Id])
GO
ALTER TABLE [dbo].[Operations] CHECK CONSTRAINT [FK_Operations_Medical_Org_Id]
GO
ALTER TABLE [dbo].[Operations]  WITH CHECK ADD  CONSTRAINT [FK_Operations_PatientId] FOREIGN KEY([Patient_Id])
REFERENCES [dbo].[Patient] ([Patient_Id])
GO
ALTER TABLE [dbo].[Operations] CHECK CONSTRAINT [FK_Operations_PatientId]
GO
ALTER TABLE [dbo].[Patient]  WITH CHECK ADD  CONSTRAINT [FK_Patient_Person] FOREIGN KEY([Patient_Id])
REFERENCES [dbo].[Person] ([Person_Id])
GO
ALTER TABLE [dbo].[Patient] CHECK CONSTRAINT [FK_Patient_Person]
GO
ALTER TABLE [dbo].[Permission]  WITH CHECK ADD  CONSTRAINT [FK_Permission_Doctor] FOREIGN KEY([Doctor_Id])
REFERENCES [dbo].[Doctor] ([Doctor_Id])
GO
ALTER TABLE [dbo].[Permission] CHECK CONSTRAINT [FK_Permission_Doctor]
GO
ALTER TABLE [dbo].[Permission]  WITH CHECK ADD  CONSTRAINT [FK_Permission_Patient] FOREIGN KEY([Patient_Id])
REFERENCES [dbo].[Patient] ([Patient_Id])
GO
ALTER TABLE [dbo].[Permission] CHECK CONSTRAINT [FK_Permission_Patient]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_User] FOREIGN KEY([Person_Id])
REFERENCES [dbo].[User] ([User_Id])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_User]
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD  CONSTRAINT [FK_Reviews_Doctor_Id] FOREIGN KEY([Doctor_Id])
REFERENCES [dbo].[Doctor] ([Doctor_Id])
GO
ALTER TABLE [dbo].[Reviews] CHECK CONSTRAINT [FK_Reviews_Doctor_Id]
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD  CONSTRAINT [FK_Reviews_Medical_Org_Id] FOREIGN KEY([Organization_Id])
REFERENCES [dbo].[Medical_Organization] ([Medical_Org_Id])
GO
ALTER TABLE [dbo].[Reviews] CHECK CONSTRAINT [FK_Reviews_Medical_Org_Id]
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD  CONSTRAINT [FK_Reviews_Patient_Id] FOREIGN KEY([Patient_Id])
REFERENCES [dbo].[Patient] ([Patient_Id])
GO
ALTER TABLE [dbo].[Reviews] CHECK CONSTRAINT [FK_Reviews_Patient_Id]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UserName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 = admin, 2 = medical org, 3 = doctor, 4 = patient ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'User_Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User'
GO
USE [master]
GO
ALTER DATABASE [EMR_GP_DB] SET  READ_WRITE 
GO
