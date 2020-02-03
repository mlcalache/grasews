USE [Grasews2.0]
GO

INSERT INTO [dbo].[WsdlOutput]
           ([IdWsdlOperation]
           ,[WsdlOutputName]
           ,[RegistrationDateTime])
     VALUES
           (1
           ,'output 1'
           ,getdate())
GO


INSERT INTO [dbo].[WsdlOutput]
           ([IdWsdlOperation]
           ,[WsdlOutputName]
           ,[RegistrationDateTime])
     VALUES
           (2
           ,'output 2'
           ,getdate())
GO


INSERT INTO [dbo].[WsdlOutput]
           ([IdWsdlOperation]
           ,[WsdlOutputName]
           ,[RegistrationDateTime])
     VALUES
           (3
           ,'output 3'
           ,getdate())
GO


