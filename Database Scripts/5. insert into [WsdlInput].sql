USE [Grasews2.0]
GO

INSERT INTO [dbo].[WsdlInput]
           ([IdWsdlOperation]
           ,[WsdlInputName]
           ,[RegistrationDateTime])
     VALUES
           (1
           ,'input 1'
           ,getdate())
GO

INSERT INTO [dbo].[WsdlInput]
           ([IdWsdlOperation]
           ,[WsdlInputName]
           ,[RegistrationDateTime])
     VALUES
           (2
           ,'input 2'
           ,getdate())
GO

INSERT INTO [dbo].[WsdlInput]
           ([IdWsdlOperation]
           ,[WsdlInputName]
           ,[RegistrationDateTime])
     VALUES
           (3
           ,'input 3'
           ,getdate())
GO

