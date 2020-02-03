USE [Grasews2.0]
GO

INSERT INTO [dbo].[XsdComplexElement]
           ([IdXsdDocument]
           ,[XsdComplexElementName]
           ,[RegistrationDateTime]
           ,[XsdToWsdlRelationType]
           ,[IdWsdlInput])
     VALUES
           (1
           ,'complex element 1'
           ,getdate()
           ,3
           ,1)
GO


INSERT INTO [dbo].[XsdComplexElement]
           ([IdXsdDocument]
           ,[XsdComplexElementName]
           ,[RegistrationDateTime]
           ,[XsdToWsdlRelationType]
           ,[IdWsdlInput])
     VALUES
           (1
           ,'complex element 2'
           ,getdate()
           ,3
           ,2)
GO

INSERT INTO [dbo].[XsdComplexElement]
           ([IdXsdDocument]
           ,[XsdComplexElementName]
           ,[RegistrationDateTime]
           ,[XsdToWsdlRelationType]
           ,[IdWsdlInput])
     VALUES
           (1
           ,'complex element 3'
           ,getdate()
           ,3
           ,3)
GO
