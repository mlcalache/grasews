USE [Grasews2.0]
GO

INSERT INTO [dbo].[WsdlOperation]
           ([IdWsdlInterface]
           ,[WsdlOperationName]
           ,[RegistrationDateTime])
     VALUES
           (1
           ,'Operation 1'
           ,getdate())
GO

INSERT INTO [dbo].[WsdlOperation]
           ([IdWsdlInterface]
           ,[WsdlOperationName]
           ,[RegistrationDateTime])
     VALUES
           (1
           ,'Operation 2'
           ,getdate())
GO

INSERT INTO [dbo].[WsdlOperation]
           ([IdWsdlInterface]
           ,[WsdlOperationName]
           ,[RegistrationDateTime])
     VALUES
           (1
           ,'Operation 3'
           ,getdate())
GO
