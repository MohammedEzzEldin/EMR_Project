USE [EMR_GP_DB]
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'User_Type'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Email'
GO
ALTER TABLE [dbo].[Reviews] DROP CONSTRAINT [FK_Reviews_Patient_Id]
GO
ALTER TABLE [dbo].[Reviews] DROP CONSTRAINT [FK_Reviews_Medical_Org_Id]
GO
ALTER TABLE [dbo].[Reviews] DROP CONSTRAINT [FK_Reviews_Doctor_Id]
GO
ALTER TABLE [dbo].[Person] DROP CONSTRAINT [FK_Person_User]
GO
ALTER TABLE [dbo].[Permission] DROP CONSTRAINT [FK_Permission_Patient]
GO
ALTER TABLE [dbo].[Permission] DROP CONSTRAINT [FK_Permission_Doctor]
GO
ALTER TABLE [dbo].[Patient] DROP CONSTRAINT [FK_Patient_Person]
GO
ALTER TABLE [dbo].[Operations] DROP CONSTRAINT [FK_Operations_PatientId]
GO
ALTER TABLE [dbo].[Operations] DROP CONSTRAINT [FK_Operations_Medical_Org_Id]
GO
ALTER TABLE [dbo].[Operations] DROP CONSTRAINT [FK_Operations_Doctor]
GO
ALTER TABLE [dbo].[Medical_Organization] DROP CONSTRAINT [FK_Medical_Organization_UserId]
GO
ALTER TABLE [dbo].[Labs] DROP CONSTRAINT [FK_Labs_Patient]
GO
ALTER TABLE [dbo].[Labs] DROP CONSTRAINT [FK_Labs_Org]
GO
ALTER TABLE [dbo].[Habits] DROP CONSTRAINT [FK_Habits_Patient]
GO
ALTER TABLE [dbo].[General_Examination] DROP CONSTRAINT [FK_General_Examination_Patient]
GO
ALTER TABLE [dbo].[Family_History] DROP CONSTRAINT [FK_Family_History_Patient]
GO
ALTER TABLE [dbo].[Examination] DROP CONSTRAINT [FK_Examination_Patient]
GO
ALTER TABLE [dbo].[Examination] DROP CONSTRAINT [FK_Examination_Medical_Org]
GO
ALTER TABLE [dbo].[Examination] DROP CONSTRAINT [FK_Examination_Doctor]
GO
ALTER TABLE [dbo].[Evaluation_Medical_Org] DROP CONSTRAINT [FK_Evaluation_Medical_Org_Patient_Id]
GO
ALTER TABLE [dbo].[Evaluation_Medical_Org] DROP CONSTRAINT [FK_Evaluation_Medical_Org_Org_Id]
GO
ALTER TABLE [dbo].[Evaluation_Doctor] DROP CONSTRAINT [FK_Evaluation_Doctor_PatientId]
GO
ALTER TABLE [dbo].[Evaluation_Doctor] DROP CONSTRAINT [FK_Evaluation_Doctor_DoctorId]
GO
ALTER TABLE [dbo].[Doctor_Schedule] DROP CONSTRAINT [FK_Doctor_Schedule_Doctor]
GO
ALTER TABLE [dbo].[Doctor] DROP CONSTRAINT [FK_Doctor_Person]
GO
ALTER TABLE [dbo].[Doctor] DROP CONSTRAINT [FK_Doctor_Department]
GO
ALTER TABLE [dbo].[Diseases] DROP CONSTRAINT [FK_Diseases_Patient]
GO
ALTER TABLE [dbo].[Department] DROP CONSTRAINT [FK_Department_Medical_Org]
GO
ALTER TABLE [dbo].[Child_FollowUp_Form] DROP CONSTRAINT [FK_Child_FollowUp_Form_Patient]
GO
ALTER TABLE [dbo].[Allergies] DROP CONSTRAINT [FK_Allergies_Patient]
GO
/****** Object:  Index [Unique_UserName]    Script Date: 10/4/2019 8:49:16 PM ******/
DROP INDEX [Unique_UserName] ON [dbo].[User]
GO
/****** Object:  Index [National_Id]    Script Date: 10/4/2019 8:49:16 PM ******/
DROP INDEX [National_Id] ON [dbo].[Person]
GO
/****** Object:  Table [dbo].[User]    Script Date: 10/4/2019 8:49:16 PM ******/
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[Reviews]    Script Date: 10/4/2019 8:49:16 PM ******/
DROP TABLE [dbo].[Reviews]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 10/4/2019 8:49:16 PM ******/
DROP TABLE [dbo].[Person]
GO
/****** Object:  Table [dbo].[Permission]    Script Date: 10/4/2019 8:49:16 PM ******/
DROP TABLE [dbo].[Permission]
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 10/4/2019 8:49:16 PM ******/
DROP TABLE [dbo].[Patient]
GO
/****** Object:  Table [dbo].[Operations]    Script Date: 10/4/2019 8:49:16 PM ******/
DROP TABLE [dbo].[Operations]
GO
/****** Object:  Table [dbo].[Medical_Organization]    Script Date: 10/4/2019 8:49:16 PM ******/
DROP TABLE [dbo].[Medical_Organization]
GO
/****** Object:  Table [dbo].[Labs]    Script Date: 10/4/2019 8:49:16 PM ******/
DROP TABLE [dbo].[Labs]
GO
/****** Object:  Table [dbo].[Habits]    Script Date: 10/4/2019 8:49:16 PM ******/
DROP TABLE [dbo].[Habits]
GO
/****** Object:  Table [dbo].[General_Examination]    Script Date: 10/4/2019 8:49:16 PM ******/
DROP TABLE [dbo].[General_Examination]
GO
/****** Object:  Table [dbo].[Family_History]    Script Date: 10/4/2019 8:49:16 PM ******/
DROP TABLE [dbo].[Family_History]
GO
/****** Object:  Table [dbo].[Examination]    Script Date: 10/4/2019 8:49:16 PM ******/
DROP TABLE [dbo].[Examination]
GO
/****** Object:  Table [dbo].[Evaluation_Medical_Org]    Script Date: 10/4/2019 8:49:16 PM ******/
DROP TABLE [dbo].[Evaluation_Medical_Org]
GO
/****** Object:  Table [dbo].[Evaluation_Doctor]    Script Date: 10/4/2019 8:49:16 PM ******/
DROP TABLE [dbo].[Evaluation_Doctor]
GO
/****** Object:  Table [dbo].[Doctor_Schedule]    Script Date: 10/4/2019 8:49:16 PM ******/
DROP TABLE [dbo].[Doctor_Schedule]
GO
/****** Object:  Table [dbo].[Doctor]    Script Date: 10/4/2019 8:49:16 PM ******/
DROP TABLE [dbo].[Doctor]
GO
/****** Object:  Table [dbo].[Diseases]    Script Date: 10/4/2019 8:49:16 PM ******/
DROP TABLE [dbo].[Diseases]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 10/4/2019 8:49:16 PM ******/
DROP TABLE [dbo].[Department]
GO
/****** Object:  Table [dbo].[Child_FollowUp_Form]    Script Date: 10/4/2019 8:49:16 PM ******/
DROP TABLE [dbo].[Child_FollowUp_Form]
GO
/****** Object:  Table [dbo].[Allergies]    Script Date: 10/4/2019 8:49:16 PM ******/
DROP TABLE [dbo].[Allergies]
GO
USE [master]
GO
/****** Object:  Database [EMR_GP_DB]    Script Date: 10/4/2019 8:49:16 PM ******/
DROP DATABASE [EMR_GP_DB]
GO
/****** Object:  Database [EMR_GP_DB]    Script Date: 10/4/2019 8:49:16 PM ******/
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
/****** Object:  Table [dbo].[Allergies]    Script Date: 10/4/2019 8:49:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Allergies](
	[Patient_Id] [bigint] NOT NULL,
	[Allergies_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Allergies_Name] [nchar](100) NOT NULL,
 CONSTRAINT [PK_Allergies] PRIMARY KEY CLUSTERED 
(
	[Allergies_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Child_FollowUp_Form]    Script Date: 10/4/2019 8:49:16 PM ******/
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
/****** Object:  Table [dbo].[Department]    Script Date: 10/4/2019 8:49:16 PM ******/
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
/****** Object:  Table [dbo].[Diseases]    Script Date: 10/4/2019 8:49:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Diseases](
	[Patient_Id] [bigint] NOT NULL,
	[Disease_Name] [nchar](50) NOT NULL,
	[Disease_Type] [nchar](50) NOT NULL,
	[disease_Id] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Diseases] PRIMARY KEY CLUSTERED 
(
	[disease_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [Patient]
) ON [Patient]
GO
/****** Object:  Table [dbo].[Doctor]    Script Date: 10/4/2019 8:49:16 PM ******/
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
/****** Object:  Table [dbo].[Doctor_Schedule]    Script Date: 10/4/2019 8:49:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctor_Schedule](
	[doctor_id] [bigint] NOT NULL,
	[day] [char](10) NOT NULL,
	[from] [time](7) NOT NULL,
	[to] [time](7) NOT NULL,
	[Schedule_Id] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Doctor_Schedule] PRIMARY KEY CLUSTERED 
(
	[Schedule_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Evaluation_Doctor]    Script Date: 10/4/2019 8:49:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Evaluation_Doctor](
	[Doctor_Id] [bigint] NOT NULL,
	[Patient_Id] [bigint] NOT NULL,
	[Rate] [float] NOT NULL,
	[Evaluation_Doctor_Id] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Evaluation_Doctor] PRIMARY KEY CLUSTERED 
(
	[Evaluation_Doctor_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Evaluation_Medical_Org]    Script Date: 10/4/2019 8:49:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Evaluation_Medical_Org](
	[Medical_Org_Id] [bigint] NOT NULL,
	[Patient_Id] [bigint] NOT NULL,
	[Rate] [float] NOT NULL,
	[Evaluation_Medical_Id] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Evaluation_Medical_Org] PRIMARY KEY CLUSTERED 
(
	[Evaluation_Medical_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Examination]    Script Date: 10/4/2019 8:49:16 PM ******/
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
	[exm_midicine] [nchar](500) NULL,
	[examination_Id] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Examination] PRIMARY KEY CLUSTERED 
(
	[examination_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [Patient]
) ON [Patient]
GO
/****** Object:  Table [dbo].[Family_History]    Script Date: 10/4/2019 8:49:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Family_History](
	[Patient_Id] [bigint] NOT NULL,
	[Disease_Name] [nchar](50) NOT NULL,
	[Disease_Type] [nchar](50) NOT NULL,
	[family_history] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Family_History] PRIMARY KEY CLUSTERED 
(
	[family_history] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [Patient]
) ON [Patient]
GO
/****** Object:  Table [dbo].[General_Examination]    Script Date: 10/4/2019 8:49:16 PM ******/
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
	[Temperature] [int] NULL,
 CONSTRAINT [PK_General_Examination] PRIMARY KEY CLUSTERED 
(
	[Patient_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [Patient]
) ON [Patient]
GO
/****** Object:  Table [dbo].[Habits]    Script Date: 10/4/2019 8:49:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Habits](
	[Patient_Id] [bigint] NOT NULL,
	[Habit_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Habit_Name] [nchar](100) NOT NULL,
 CONSTRAINT [PK_Habits] PRIMARY KEY CLUSTERED 
(
	[Habit_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Labs]    Script Date: 10/4/2019 8:49:16 PM ******/
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
	[Lab_Date] [date] NOT NULL,
	[Lab_File] [nchar](200) NULL,
	[Lab_Id] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Labs] PRIMARY KEY CLUSTERED 
(
	[Lab_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [Patient]
) ON [Patient]
GO
/****** Object:  Table [dbo].[Medical_Organization]    Script Date: 10/4/2019 8:49:16 PM ******/
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
/****** Object:  Table [dbo].[Operations]    Script Date: 10/4/2019 8:49:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operations](
	[Patient_Id] [bigint] NOT NULL,
	[Op_Name] [nchar](100) NOT NULL,
	[Op_Type] [nchar](100) NOT NULL,
	[Op_Date] [date] NOT NULL,
	[Doctor_Id] [bigint] NULL,
	[Organization_Id] [bigint] NOT NULL,
	[Operation_Id] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Operations] PRIMARY KEY CLUSTERED 
(
	[Operation_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [Patient]
) ON [Patient]
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 10/4/2019 8:49:16 PM ******/
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
/****** Object:  Table [dbo].[Permission]    Script Date: 10/4/2019 8:49:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permission](
	[Patient_Id] [bigint] NOT NULL,
	[Doctor_Id] [bigint] NOT NULL,
	[Booking_Date] [date] NOT NULL,
	[Permission_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[hour] [decimal](23, 0) NOT NULL,
 CONSTRAINT [PK_Permission_1] PRIMARY KEY CLUSTERED 
(
	[Permission_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 10/4/2019 8:49:16 PM ******/
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
/****** Object:  Table [dbo].[Reviews]    Script Date: 10/4/2019 8:49:16 PM ******/
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
	[rev_description_result] [nchar](500) NULL,
	[review_Id] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Reviews] PRIMARY KEY CLUSTERED 
(
	[review_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [Patient]
) ON [Patient]
GO
/****** Object:  Table [dbo].[User]    Script Date: 10/4/2019 8:49:16 PM ******/
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
INSERT [dbo].[Child_FollowUp_Form] ([Patient_Id], [Age_In_Month], [Feed_Type], [Vaccination_Type], [Vaccination_Date]) VALUES (30, 24, N'Weaned                                                                                              ', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Department] ON 

INSERT [dbo].[Department] ([Department_Name], [Medical_Org_Id], [Department_Id]) VALUES (N'Emergency                                         ', 23, 2)
SET IDENTITY_INSERT [dbo].[Department] OFF
SET IDENTITY_INSERT [dbo].[Diseases] ON 

INSERT [dbo].[Diseases] ([Patient_Id], [Disease_Name], [Disease_Type], [disease_Id]) VALUES (30, N'Hypertension                                      ', N'Chronic                                           ', 1)
SET IDENTITY_INSERT [dbo].[Diseases] OFF
INSERT [dbo].[Doctor] ([Doctor_Id], [Medium_Rate], [Acadimic_Degree], [Functional_Degree], [Spacialization], [Medical_Org_Id], [Department_Id]) VALUES (50, 3.775, N'Bachelor                                          ', N'                                                  ', N'General                                           ', 7, NULL)
INSERT [dbo].[Doctor] ([Doctor_Id], [Medium_Rate], [Acadimic_Degree], [Functional_Degree], [Spacialization], [Medical_Org_Id], [Department_Id]) VALUES (51, 0, N'Bachelor                                          ', N'                                                  ', N'General                                           ', 23, 2)
INSERT [dbo].[Doctor] ([Doctor_Id], [Medium_Rate], [Acadimic_Degree], [Functional_Degree], [Spacialization], [Medical_Org_Id], [Department_Id]) VALUES (52, 3.5, N'Bacholer                                          ', N'Consyltative                                      ', N'Nerves                                            ', 23, 2)
SET IDENTITY_INSERT [dbo].[Doctor_Schedule] ON 

INSERT [dbo].[Doctor_Schedule] ([doctor_id], [day], [from], [to], [Schedule_Id]) VALUES (50, N'Monday    ', CAST(N'09:00:00' AS Time), CAST(N'13:00:00' AS Time), 1)
INSERT [dbo].[Doctor_Schedule] ([doctor_id], [day], [from], [to], [Schedule_Id]) VALUES (50, N'Sunday    ', CAST(N'18:00:00' AS Time), CAST(N'22:00:00' AS Time), 2)
INSERT [dbo].[Doctor_Schedule] ([doctor_id], [day], [from], [to], [Schedule_Id]) VALUES (50, N'Wednesday ', CAST(N'18:00:00' AS Time), CAST(N'22:00:00' AS Time), 3)
INSERT [dbo].[Doctor_Schedule] ([doctor_id], [day], [from], [to], [Schedule_Id]) VALUES (51, N'Sunday    ', CAST(N'10:00:00' AS Time), CAST(N'15:00:00' AS Time), 4)
INSERT [dbo].[Doctor_Schedule] ([doctor_id], [day], [from], [to], [Schedule_Id]) VALUES (51, N'Wednesday ', CAST(N'10:00:00' AS Time), CAST(N'15:00:00' AS Time), 5)
SET IDENTITY_INSERT [dbo].[Doctor_Schedule] OFF
SET IDENTITY_INSERT [dbo].[Evaluation_Doctor] ON 

INSERT [dbo].[Evaluation_Doctor] ([Doctor_Id], [Patient_Id], [Rate], [Evaluation_Doctor_Id]) VALUES (50, 30, 4.5, 4)
INSERT [dbo].[Evaluation_Doctor] ([Doctor_Id], [Patient_Id], [Rate], [Evaluation_Doctor_Id]) VALUES (50, 30, 3, 5)
INSERT [dbo].[Evaluation_Doctor] ([Doctor_Id], [Patient_Id], [Rate], [Evaluation_Doctor_Id]) VALUES (52, 30, 3.5, 6)
SET IDENTITY_INSERT [dbo].[Evaluation_Doctor] OFF
SET IDENTITY_INSERT [dbo].[Evaluation_Medical_Org] ON 

INSERT [dbo].[Evaluation_Medical_Org] ([Medical_Org_Id], [Patient_Id], [Rate], [Evaluation_Medical_Id]) VALUES (5, 30, 3, 1)
INSERT [dbo].[Evaluation_Medical_Org] ([Medical_Org_Id], [Patient_Id], [Rate], [Evaluation_Medical_Id]) VALUES (2, 30, 3.5, 2)
INSERT [dbo].[Evaluation_Medical_Org] ([Medical_Org_Id], [Patient_Id], [Rate], [Evaluation_Medical_Id]) VALUES (4, 30, 5, 3)
INSERT [dbo].[Evaluation_Medical_Org] ([Medical_Org_Id], [Patient_Id], [Rate], [Evaluation_Medical_Id]) VALUES (8, 30, 4.5, 4)
INSERT [dbo].[Evaluation_Medical_Org] ([Medical_Org_Id], [Patient_Id], [Rate], [Evaluation_Medical_Id]) VALUES (16, 30, 2.5, 5)
SET IDENTITY_INSERT [dbo].[Evaluation_Medical_Org] OFF
SET IDENTITY_INSERT [dbo].[Examination] ON 

INSERT [dbo].[Examination] ([Patient_Id], [Doctor_Id], [Medical_Org_Id], [exm_date], [exm_description_result], [exm_midicine], [examination_Id]) VALUES (30, 50, 7, CAST(N'2019-03-29' AS Date), N'Seasonal flu                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ', N'Panadol                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             ', 1)
SET IDENTITY_INSERT [dbo].[Examination] OFF
SET IDENTITY_INSERT [dbo].[Family_History] ON 

INSERT [dbo].[Family_History] ([Patient_Id], [Disease_Name], [Disease_Type], [family_history]) VALUES (30, N'Diabetes                                          ', N'Chronic                                           ', 1)
INSERT [dbo].[Family_History] ([Patient_Id], [Disease_Name], [Disease_Type], [family_history]) VALUES (30, N'Hypertension                                      ', N'Chronic                                           ', 2)
SET IDENTITY_INSERT [dbo].[Family_History] OFF
INSERT [dbo].[General_Examination] ([Patient_Id], [Length], [Weight], [Blood_Type], [Blood_Pressure], [Diabetes], [Temperature]) VALUES (30, 190, 90, N'O+ ', N'120 / 80                                          ', 90, 37)
SET IDENTITY_INSERT [dbo].[Labs] ON 

INSERT [dbo].[Labs] ([Patient_Id], [Org_Id], [Lab_Name], [Lab_Description_Result], [Lab_Type], [Lab_Date], [Lab_File], [Lab_Id]) VALUES (30, 6, N'Urine Anlysis                                                                                       ', N'Nothing                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             ', N'urine analysis                                    ', CAST(N'2018-02-12' AS Date), N'~\Content\Lab_Radio\30\Urine Anlysis1222018.pdf                                                                                                                                                         ', 1)
SET IDENTITY_INSERT [dbo].[Labs] OFF
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (2, N'ALHayat                                                                                             ', 3.5)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (3, N'The Future                                                                                          ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (4, N'AL Nour for Eyes                                                                                    ', 5)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (5, N'AL Hamad Charity                                                                                    ', 3)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (6, N'Tabarak Children Hospital                                                                           ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (7, N'57357 For Children Cancer                                                                           ', 5)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (8, N'AL Galaa Teaching Hospital                                                                          ', 4.5)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (9, N'AL Durrah Heart Care                                                                                ', 5)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (10, N'Misr International                                                                                  ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (11, N'ALGabry                                                                                             ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (12, N'Ahmed Maher Teaching Hospital                                                                       ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (13, N'Demerdash Teaching Hospital                                                                         ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (14, N'Abbasiya Mental Health                                                                              ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (15, N'National Cancer Institute                                                                           ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (16, N'Misr El Amal                                                                                        ', 2.5)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (17, N'Shrouq Hospital                                                                                     ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (18, N'Dounieh Eye Hospital                                                                                ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (19, N'AL Salam International                                                                              ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (20, N'Dar El Fouad Hospital                                                                               ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (21, N'Medical Center for Arab Contractors                                                                 ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (23, N'Saudi German Hospital                                                                               ', 5)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (24, N'Ganzouri Specialist Hospital                                                                        ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (25, N'Ain Shams University Specialized Hospital                                                           ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (26, N'Cleopatra                                                                                           ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (27, N'Badr                                                                                                ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (28, N'Dar EL Hekma Charity                                                                                ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (29, N'Ahram                                                                                               ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (39, N'The Modern                                                                                          ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (46, N'Nour El Hayat                                                                                       ', 0)
INSERT [dbo].[Medical_Organization] ([Medical_Org_Id], [Medical_Org_Name], [Medium_Rate]) VALUES (49, N'AL Habeb                                                                                            ', 0)
SET IDENTITY_INSERT [dbo].[Operations] ON 

INSERT [dbo].[Operations] ([Patient_Id], [Op_Name], [Op_Type], [Op_Date], [Doctor_Id], [Organization_Id], [Operation_Id]) VALUES (30, N'Remove the tonsils                                                                                  ', N'Surgery                                                                                             ', CAST(N'2018-03-05' AS Date), 50, 7, 1)
SET IDENTITY_INSERT [dbo].[Operations] OFF
INSERT [dbo].[Patient] ([Patient_Id], [Learning_Status], [Social_Status], [Death_Date], [Alternative_Phone], [Job], [Reason_Death]) VALUES (30, N'Bachelor                                                                                  ', N'Single                                                                                    ', NULL, N'01234567891   ', N'Student at FCIH                                   ', NULL)
INSERT [dbo].[Patient] ([Patient_Id], [Learning_Status], [Social_Status], [Death_Date], [Alternative_Phone], [Job], [Reason_Death]) VALUES (31, N'Bachelor                                                                                  ', N'Single                                                                                    ', NULL, NULL, N'Student at FCIH                                   ', NULL)
INSERT [dbo].[Patient] ([Patient_Id], [Learning_Status], [Social_Status], [Death_Date], [Alternative_Phone], [Job], [Reason_Death]) VALUES (32, N'Bachelor                                                                                  ', N'Single                                                                                    ', NULL, NULL, N'Student at FCIH                                   ', NULL)
INSERT [dbo].[Patient] ([Patient_Id], [Learning_Status], [Social_Status], [Death_Date], [Alternative_Phone], [Job], [Reason_Death]) VALUES (33, N'Bachelor                                                                                  ', N'Single                                                                                    ', NULL, N'              ', N'Student at FCIH                                   ', NULL)
INSERT [dbo].[Patient] ([Patient_Id], [Learning_Status], [Social_Status], [Death_Date], [Alternative_Phone], [Job], [Reason_Death]) VALUES (34, N'Bachelor                                                                                  ', N'Married                                                                                   ', NULL, N'              ', N'Arabic Teacher                                    ', NULL)
INSERT [dbo].[Patient] ([Patient_Id], [Learning_Status], [Social_Status], [Death_Date], [Alternative_Phone], [Job], [Reason_Death]) VALUES (35, N'Bachelor                                                                                  ', N'Married                                                                                   ', NULL, N'              ', N'Football Player                                   ', NULL)
INSERT [dbo].[Patient] ([Patient_Id], [Learning_Status], [Social_Status], [Death_Date], [Alternative_Phone], [Job], [Reason_Death]) VALUES (36, N'Bachelor                                                                                  ', N'Married                                                                                   ', NULL, N'              ', N'Player at Ahly Club                               ', NULL)
INSERT [dbo].[Patient] ([Patient_Id], [Learning_Status], [Social_Status], [Death_Date], [Alternative_Phone], [Job], [Reason_Death]) VALUES (37, N'Bachelor                                                                                  ', N'Married                                                                                   ', NULL, N'              ', N'Player at Ahly Club                               ', NULL)
INSERT [dbo].[Patient] ([Patient_Id], [Learning_Status], [Social_Status], [Death_Date], [Alternative_Phone], [Job], [Reason_Death]) VALUES (38, N'Bachelor                                                                                  ', N'Married                                                                                   ', NULL, NULL, N'Software Eng at Link Development                  ', NULL)
INSERT [dbo].[Patient] ([Patient_Id], [Learning_Status], [Social_Status], [Death_Date], [Alternative_Phone], [Job], [Reason_Death]) VALUES (40, N'Bachelor                                                                                  ', N'Single                                                                                    ', NULL, NULL, N'Student at Banha Universty                        ', NULL)
INSERT [dbo].[Patient] ([Patient_Id], [Learning_Status], [Social_Status], [Death_Date], [Alternative_Phone], [Job], [Reason_Death]) VALUES (41, N'Bachelor                                                                                  ', N'Married                                                                                   ', NULL, NULL, N'Free Work                                         ', NULL)
INSERT [dbo].[Patient] ([Patient_Id], [Learning_Status], [Social_Status], [Death_Date], [Alternative_Phone], [Job], [Reason_Death]) VALUES (42, N'Bachelor                                                                                  ', N'Single                                                                                    ', NULL, NULL, N'Free Work                                         ', NULL)
INSERT [dbo].[Patient] ([Patient_Id], [Learning_Status], [Social_Status], [Death_Date], [Alternative_Phone], [Job], [Reason_Death]) VALUES (45, N'Bachelor                                                                                  ', N'Married                                                                                   ', NULL, NULL, N'Free Work                                         ', NULL)
INSERT [dbo].[Patient] ([Patient_Id], [Learning_Status], [Social_Status], [Death_Date], [Alternative_Phone], [Job], [Reason_Death]) VALUES (47, N'Bachelor                                                                                  ', N'Married                                                                                   ', NULL, N'              ', N'Player at Ahly Club                               ', NULL)
INSERT [dbo].[Patient] ([Patient_Id], [Learning_Status], [Social_Status], [Death_Date], [Alternative_Phone], [Job], [Reason_Death]) VALUES (48, N'Bachelor                                                                                  ', N'Married                                                                                   ', NULL, N'              ', N'Player at Zamalek Club                            ', NULL)
SET IDENTITY_INSERT [dbo].[Permission] ON 

INSERT [dbo].[Permission] ([Patient_Id], [Doctor_Id], [Booking_Date], [Permission_Id], [hour]) VALUES (30, 51, CAST(N'2019-04-24' AS Date), 6, CAST(10 AS Decimal(23, 0)))
SET IDENTITY_INSERT [dbo].[Permission] OFF
INSERT [dbo].[Person] ([Person_Id], [First_Name], [Last_Name], [Birth_Date], [National_Id], [Nationality], [Gender]) VALUES (1, N'Mohammad                                                                                            ', N'Mahmoud                                                                                             ', CAST(N'1997-03-09' AS Date), N'10234567892134', N'Egypt                                             ', N'male  ')
INSERT [dbo].[Person] ([Person_Id], [First_Name], [Last_Name], [Birth_Date], [National_Id], [Nationality], [Gender]) VALUES (30, N'Ahmed Tareek                                                                                        ', N'Faud                                                                                                ', CAST(N'1997-01-05' AS Date), N'01234567891234', N'Egypt                                             ', N'male  ')
INSERT [dbo].[Person] ([Person_Id], [First_Name], [Last_Name], [Birth_Date], [National_Id], [Nationality], [Gender]) VALUES (31, N'Mohamed                                                                                             ', N'Rabia                                                                                               ', CAST(N'1996-10-01' AS Date), N'98765412301346', N'Egypt                                             ', N'male  ')
INSERT [dbo].[Person] ([Person_Id], [First_Name], [Last_Name], [Birth_Date], [National_Id], [Nationality], [Gender]) VALUES (32, N'Mario                                                                                               ', N'Elhamy                                                                                              ', CAST(N'1999-06-02' AS Date), N'12378964325876', N'Egypt                                             ', N'male  ')
INSERT [dbo].[Person] ([Person_Id], [First_Name], [Last_Name], [Birth_Date], [National_Id], [Nationality], [Gender]) VALUES (33, N'Hossam                                                                                              ', N'Hassan                                                                                              ', CAST(N'1998-05-03' AS Date), N'63795781264869', N'Egypt                                             ', N'male  ')
INSERT [dbo].[Person] ([Person_Id], [First_Name], [Last_Name], [Birth_Date], [National_Id], [Nationality], [Gender]) VALUES (34, N'Hassan                                                                                              ', N'AL Saff                                                                                             ', CAST(N'1985-07-03' AS Date), N'37699812245646', N'Egypt                                             ', N'male  ')
INSERT [dbo].[Person] ([Person_Id], [First_Name], [Last_Name], [Birth_Date], [National_Id], [Nationality], [Gender]) VALUES (35, N'Islam                                                                                               ', N'Mohareb                                                                                             ', CAST(N'1995-06-03' AS Date), N'67851234569222', N'Egypt                                             ', N'male  ')
INSERT [dbo].[Person] ([Person_Id], [First_Name], [Last_Name], [Birth_Date], [National_Id], [Nationality], [Gender]) VALUES (36, N'Ramadan                                                                                             ', N'Sobhy                                                                                               ', CAST(N'1997-05-03' AS Date), N'46464864646464', N'Egypt                                             ', N'male  ')
INSERT [dbo].[Person] ([Person_Id], [First_Name], [Last_Name], [Birth_Date], [National_Id], [Nationality], [Gender]) VALUES (37, N'Saad                                                                                                ', N'Sameer                                                                                              ', CAST(N'1996-04-06' AS Date), N'66464364643436', N'Egypt                                             ', N'male  ')
INSERT [dbo].[Person] ([Person_Id], [First_Name], [Last_Name], [Birth_Date], [National_Id], [Nationality], [Gender]) VALUES (38, N'Haitham                                                                                             ', N'Mohamed                                                                                             ', CAST(N'1989-05-06' AS Date), N' 1335876458316', N'Egypt                                             ', N'male  ')
INSERT [dbo].[Person] ([Person_Id], [First_Name], [Last_Name], [Birth_Date], [National_Id], [Nationality], [Gender]) VALUES (40, N'Maged                                                                                               ', N'Kamel                                                                                               ', CAST(N'1999-06-02' AS Date), N'97854235571137', N'Egypt                                             ', N'male  ')
INSERT [dbo].[Person] ([Person_Id], [First_Name], [Last_Name], [Birth_Date], [National_Id], [Nationality], [Gender]) VALUES (41, N'Haitham                                                                                             ', N'Ahmed                                                                                               ', CAST(N'1980-01-01' AS Date), N'34578136987524', N'Egypt                                             ', N'male  ')
INSERT [dbo].[Person] ([Person_Id], [First_Name], [Last_Name], [Birth_Date], [National_Id], [Nationality], [Gender]) VALUES (42, N'Alaa                                                                                                ', N'Sayed                                                                                               ', CAST(N'1969-03-12' AS Date), N'37897891234578', N'Egypt                                             ', N'male  ')
INSERT [dbo].[Person] ([Person_Id], [First_Name], [Last_Name], [Birth_Date], [National_Id], [Nationality], [Gender]) VALUES (45, N'Mohsen                                                                                              ', N'Hassan                                                                                              ', CAST(N'1959-12-10' AS Date), N'34579862145634', N'Egypt                                             ', N'male  ')
INSERT [dbo].[Person] ([Person_Id], [First_Name], [Last_Name], [Birth_Date], [National_Id], [Nationality], [Gender]) VALUES (47, N'Marwan                                                                                              ', N'Mohsen                                                                                              ', CAST(N'1988-09-05' AS Date), N'34879616158615', N'Egypt                                             ', N'male  ')
INSERT [dbo].[Person] ([Person_Id], [First_Name], [Last_Name], [Birth_Date], [National_Id], [Nationality], [Gender]) VALUES (48, N'Mahmoud                                                                                             ', N'Kahraba                                                                                             ', CAST(N'1990-05-02' AS Date), N'25161631688451', N'Egypt                                             ', N'male  ')
INSERT [dbo].[Person] ([Person_Id], [First_Name], [Last_Name], [Birth_Date], [National_Id], [Nationality], [Gender]) VALUES (50, N'Ahmed                                                                                               ', N'Habeeb                                                                                              ', CAST(N'1975-06-08' AS Date), N'17895135711111', N'Egypt                                             ', N'male  ')
INSERT [dbo].[Person] ([Person_Id], [First_Name], [Last_Name], [Birth_Date], [National_Id], [Nationality], [Gender]) VALUES (51, N'Hossin                                                                                              ', N'EL said                                                                                             ', CAST(N'1980-03-03' AS Date), N'02345698710235', N'Egypt                                             ', N'male  ')
INSERT [dbo].[Person] ([Person_Id], [First_Name], [Last_Name], [Birth_Date], [National_Id], [Nationality], [Gender]) VALUES (52, N'Omar                                                                                                ', N'Hashim                                                                                              ', CAST(N'1981-09-02' AS Date), N'30000058791423', N'Egypt                                             ', N'male  ')
SET IDENTITY_INSERT [dbo].[Reviews] ON 

INSERT [dbo].[Reviews] ([Patient_Id], [Doctor_Id], [Organization_Id], [rev_date], [rev_midicine], [rev_description_result], [review_Id]) VALUES (30, 50, 7, CAST(N'2019-03-30' AS Date), N'Panadol                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             ', N'Seasonal Flue                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       ', 1)
SET IDENTITY_INSERT [dbo].[Reviews] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (1, N'mohammad_mahmoudezeldin@yahoo.com                                                                                                                                                                       ', N'Cairo                                                                                                                                                                                                   ', N'00201063834250 ', CAST(N'2019-04-10' AS Date), 1, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (2, N'Al_Hayat@medical.org                                                                                                                                                                                    ', N'Egypt-Cairo-Helwan                                                                                                                                                                                      ', N'00201063834250 ', NULL, 2, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (3, N'The_Future@future.org                                                                                                                                                                                   ', N'Egypt-Cairo-Maadi                                                                                                                                                                                       ', N'00201063834250 ', NULL, 2, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (4, N'ALNourForEyes@alnour.org                                                                                                                                                                                ', N'Egypt-Giza-AL Mohandeseen                                                                                                                                                                               ', N'00201063834250 ', NULL, 2, N'0123456789     ', 1, N'Giza                ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (5, N'ALHamad_Charity@gmail.com                                                                                                                                                                               ', N'Egypt-Giza-King Faisal Street-In front of Ragab Sons                                                                                                                                                    ', N'0237803782     ', NULL, 2, N'0123456789     ', 1, N'Giza                ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (6, N'Tabarak@gmail.com                                                                                                                                                                                       ', N'Egypt-Giza-King Faisal Street-434                                                                                                                                                                       ', N'0235830647     ', NULL, 2, N'0123456789     ', 1, N'Giza                ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (7, N'57357@gmail.com                                                                                                                                                                                         ', N'Egypt-Cairo-Zainham-ALSayedah Zainab                                                                                                                                                                    ', N'0225351500     ', NULL, 2, N'0123456789     ', 1, N'Cairo               ', N'~/Content/Med_Photo/7.jpg                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           ')
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (8, N'ELGLAA@yahoo.com                                                                                                                                                                                        ', N'Cairo-41 26th Of July St., Intersection Of El Galaa St.                                                                                                                                                 ', N'25756474       ', NULL, 2, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (9, N'Durrah_Heart@yahoo.com                                                                                                                                                                                  ', N'Cairo-Hiloplis-Hegaz Square-91 Mohamed Farid St                                                                                                                                                         ', N'0222411110     ', NULL, 2, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (10, N'MisrInternational@yahoo.com                                                                                                                                                                             ', N'Giza-Dokki-12 El Saraya St                                                                                                                                                                              ', N'0237608261     ', NULL, 2, N'0123456789     ', 1, N'Giza                ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (11, N'ALGabry@yahoo.com                                                                                                                                                                                       ', N'Egypt-Giza-Faisal St                                                                                                                                                                                    ', NULL, NULL, 2, N'0123456789     ', 1, N'Giza                ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (12, N'Ahmed_Maher@gmail.com                                                                                                                                                                                   ', N'Cairo , Port Said St., Bab El Khalq, 341                                                                                                                                                                ', N' 0223911838    ', NULL, 2, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (13, N'Demerdash@AinShams.edu.eg                                                                                                                                                                               ', N'Egypt-56 Ramses St. Abbasieh Cairo                                                                                                                                                                      ', N'00201095811849 ', NULL, 2, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (14, N'AbbasiyaMentalHealth@gmail.com                                                                                                                                                                          ', N'Stadium, Nasr City, Cairo , Egypt                                                                                                                                                                       ', N'01289109829    ', NULL, 2, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (15, N'NationalCancerInstitute@gmail.com                                                                                                                                                                       ', N'Egypt, Cairo,Kasr Al Eini، Street، Fom El Khalig                                                                                                                                                        ', N'00201208285664 ', NULL, 2, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (16, N'MisrElAmal@gmail.com                                                                                                                                                                                    ', N'Egypt,Giza,Al Omraneyah                                                                                                                                                                                 ', N'0233479212     ', NULL, 2, N'0123456789     ', 1, N'Giza                ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (17, N'ShrouqCity@gmail.com                                                                                                                                                                                    ', N'Egypt, Cairo, Shrouq City                                                                                                                                                                               ', NULL, NULL, 2, N'01234567899    ', 0, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (18, N'Dounieh@gmail.com                                                                                                                                                                                       ', N'Egypt-Giza-Dokki-Tahrir St                                                                                                                                                                              ', N'01020524528    ', NULL, 2, N'0123456789     ', 1, N'Giza                ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (19, N'ALSalamInternational@gmail.com                                                                                                                                                                          ', N'Corniche El Nil Street - Maadi - Cairo - Egypt                                                                                                                                                          ', N'0225240250     ', NULL, 2, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (20, N'DarElFouad@gmail.com                                                                                                                                                                                    ', N'Touristic Region - 6th Of October - Giza - Egypt                                                                                                                                                        ', N'02-38356028    ', NULL, 2, N'0123456789     ', 1, N'Giza                ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (21, N'ArabContractors@gmail.com                                                                                                                                                                               ', N'The green mountain next to the ski club - Nasr City - Cairo - Egypt                                                                                                                                     ', N'02-23424560    ', NULL, 2, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (23, N'info@sghcairo.com                                                                                                                                                                                       ', N'Joseph Tito Street, Airport Road, El Nozha El Gadida,                                                                                                                                                   ', N'16259          ', CAST(N'2019-04-06' AS Date), 2, N'0123456789     ', 1, N'Cairo               ', N'~/Content/Med_Photo/23.jpg                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          ')
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (24, N'info@ganzourihospital.com                                                                                                                                                                               ', N'63 Toman Bay Street, Tahra Palace Square, Roxy, Cairo                                                                                                                                                   ', N'0222588810     ', NULL, 2, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (25, N'Hospital@AinShams.edu.eg                                                                                                                                                                                ', N' Al-Khalifa Al-Maamoon Street, Abbasiya, Cairo                                                                                                                                                          ', N'01284472343    ', NULL, 2, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (26, N'Info@Cleopatra.org                                                                                                                                                                                      ', N'39 Cleopatra St., Salah El Din Sq., Heliopolis, Cairo                                                                                                                                                   ', N'0224178206     ', NULL, 2, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (27, N'Info@Badr.com                                                                                                                                                                                           ', N'Cairo , Badr City                                                                                                                                                                                       ', NULL, NULL, 2, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (28, N'Info@DarElhekma.com                                                                                                                                                                                     ', N'Cairo, Nasr City-Egypt                                                                                                                                                                                  ', NULL, NULL, 2, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (29, N'Info@Ahram.com                                                                                                                                                                                          ', N'Egypt - Giza                                                                                                                                                                                            ', NULL, NULL, 2, N'0123456789     ', 1, N'Giza                ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (30, N'Ahmed_Tareek@yahoo.com                                                                                                                                                                                  ', N'Egypt-Cairo-Helwan                                                                                                                                                                                      ', N'01234567891    ', CAST(N'2019-04-08' AS Date), 4, N'0123456789     ', 1, N'Cairo               ', N'~/Content/Patient_Photo/30.jpg                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      ')
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (31, N'mohamed_rabie@gmail.com                                                                                                                                                                                 ', N'Egypt - Menoufia                                                                                                                                                                                        ', NULL, NULL, 4, N'0123456789     ', 1, N'Menoufia            ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (32, N'mario@gmail.com                                                                                                                                                                                         ', N'Egypt-Cairo-Helwan                                                                                                                                                                                      ', NULL, NULL, 4, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (33, N'ALAmeed@gmail.com                                                                                                                                                                                       ', N'Egypt - Cairo - AL Maadi                                                                                                                                                                                ', NULL, NULL, 4, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (34, N'Hassan_ALSaff@yahoo.com                                                                                                                                                                                 ', N'Egypt - Cairo - Ain Shams                                                                                                                                                                               ', NULL, NULL, 4, N'0123456789     ', 1, N'Cairo               ', N'                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ')
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (35, N'Islam_Mohareb@gmail.com                                                                                                                                                                                 ', N'Egypt - Cairo - Nasr City                                                                                                                                                                               ', NULL, NULL, 4, N'0123456789     ', 1, N'Cairo               ', N'                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ')
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (36, N'Ramadan@ahly.com                                                                                                                                                                                        ', N'Egypt - Cairo -Nasr City                                                                                                                                                                                ', NULL, CAST(N'2019-03-29' AS Date), 4, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (37, N'Saad_Sameer@ahly.com                                                                                                                                                                                    ', N'Egypt - Cairo - Nasr City                                                                                                                                                                               ', NULL, NULL, 4, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (38, N'Haitham@yahoo.com                                                                                                                                                                                       ', N'Egypt-Cairo-Maadi                                                                                                                                                                                       ', NULL, NULL, 4, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (39, N'Info@TheModern.com                                                                                                                                                                                      ', N'Cairo                                                                                                                                                                                                   ', N'049849389483   ', NULL, 2, N'123456         ', 1, N'Cairo               ', N'                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ')
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (40, N'Maged@gmail.cp,                                                                                                                                                                                         ', N'Egypt-Cairo-Maadi                                                                                                                                                                                       ', N'00201063834250 ', NULL, 4, N'0123           ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (41, N'Haitham@gmail.cp,                                                                                                                                                                                       ', N'Egypt-Giza-Dokki-Tahrir St                                                                                                                                                                              ', NULL, NULL, 4, N'0123           ', 1, N'Giza                ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (42, N'Alaa@gmail.com                                                                                                                                                                                          ', N'Egypt-Giza-Dokki-Tahrir St                                                                                                                                                                              ', NULL, NULL, 4, N'123            ', 1, N'Giza                ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (45, N'Mohsen@yahoo.com                                                                                                                                                                                        ', N'Agamy                                                                                                                                                                                                   ', N'545454         ', NULL, 4, N'123            ', 1, N'Alexandria          ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (46, N'Info@NourElhayat.com                                                                                                                                                                                    ', N'Egypt - Cairo - Nasr City                                                                                                                                                                               ', N'               ', NULL, 2, N'0123456789     ', 1, N'Cairo               ', N'~/Content/Med_Photo/46.jpg                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          ')
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (47, N'Marwan_Mohsen@ahly.com                                                                                                                                                                                  ', N'Egypt - Cairo - Misr Al Jadidah                                                                                                                                                                         ', N'               ', NULL, 4, N'0123456789     ', 1, N'Cairo               ', N'~/Content/Patient_Photo/47.jpg                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      ')
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (48, N'Kahraba@zamalek.com                                                                                                                                                                                     ', N'Egypt - Cairo - Misr Al Jadidah                                                                                                                                                                         ', N'               ', NULL, 4, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (49, N'Info@alhabeb.com                                                                                                                                                                                        ', N'Saudi - Riyadh - Olaya                                                                                                                                                                                  ', NULL, NULL, 2, N'0123456789     ', 1, N'Riyadh              ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (50, N'Ahmed@57357.com                                                                                                                                                                                         ', N'Egypt - Cairo                                                                                                                                                                                           ', N'01234567891    ', CAST(N'2019-04-10' AS Date), 3, N'0123456789     ', 1, N'Cairo               ', N'~/Content/Doc_Photo/50.jpg                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          ')
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (51, N'Hossin_Elsaid@gmail.com                                                                                                                                                                                 ', N'Egypt - Cairo - Nasr City                                                                                                                                                                               ', NULL, CAST(N'2019-04-10' AS Date), 3, N'0123456789     ', 1, N'Cairo               ', NULL)
INSERT [dbo].[User] ([User_Id], [Email], [Address], [Phone], [Last_Date_Of_Login], [User_Type], [Password], [Status_Of_Account], [City], [Photo_Url]) VALUES (52, N'omar_hashem@saudigermani.com                                                                                                                                                                            ', N'Egypt - Cairo                                                                                                                                                                                           ', NULL, CAST(N'2019-04-10' AS Date), 3, N'0123456789     ', 1, N'Cairo               ', NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  Index [National_Id]    Script Date: 10/4/2019 8:49:16 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [National_Id] ON [dbo].[Person]
(
	[Person_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Unique_UserName]    Script Date: 10/4/2019 8:49:16 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [Unique_UserName] ON [dbo].[User]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Allergies]  WITH CHECK ADD  CONSTRAINT [FK_Allergies_Patient] FOREIGN KEY([Patient_Id])
REFERENCES [dbo].[General_Examination] ([Patient_Id])
GO
ALTER TABLE [dbo].[Allergies] CHECK CONSTRAINT [FK_Allergies_Patient]
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
ALTER TABLE [dbo].[Doctor_Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_Schedule_Doctor] FOREIGN KEY([doctor_id])
REFERENCES [dbo].[Doctor] ([Doctor_Id])
GO
ALTER TABLE [dbo].[Doctor_Schedule] CHECK CONSTRAINT [FK_Doctor_Schedule_Doctor]
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
ALTER TABLE [dbo].[Habits]  WITH CHECK ADD  CONSTRAINT [FK_Habits_Patient] FOREIGN KEY([Patient_Id])
REFERENCES [dbo].[General_Examination] ([Patient_Id])
GO
ALTER TABLE [dbo].[Habits] CHECK CONSTRAINT [FK_Habits_Patient]
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
