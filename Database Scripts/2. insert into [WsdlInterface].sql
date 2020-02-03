USE [Grasews2.0]
GO

INSERT INTO [dbo].[WsdlInterface]
           ([IdServiceDescription]
           ,[WsdlInterfaceName]
           ,[RegistrationDateTime])
     VALUES
           (1
           ,'Interface 1'
           ,getdate())
GO


