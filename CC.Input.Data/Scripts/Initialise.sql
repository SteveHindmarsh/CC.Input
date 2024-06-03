USE [CC]
GO

/****** Object: Table [dbo].[Inputs] Script Date: 03/06/2024 09:41:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Inputs] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [MPAN]               BIGINT        NOT NULL,
    [MeterSerial]        NVARCHAR (10) NOT NULL,
    [DateOfInstallation] DATE          NOT NULL,
    [AddressLine1]       NVARCHAR (40) NULL,
    [PostCode]           NVARCHAR (10) NULL
);