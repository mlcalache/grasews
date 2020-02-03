USE [Grasews2.0]
GO

INSERT INTO [dbo].[XsdComplexElement]
           ([IdXsdDocument]
           ,[XsdComplexElementName]
           ,[RegistrationDateTime]
           ,[XsdToWsdlRelationType]
           ,[IdWsdlOutput])
     VALUES
           (1
           ,'complex element 4'
           ,getdate()
           ,5
           ,1)
GO


INSERT INTO [dbo].[XsdComplexElement]
           ([IdXsdDocument]
           ,[XsdComplexElementName]
           ,[RegistrationDateTime]
           ,[XsdToWsdlRelationType]
           ,[IdWsdlOutput])
     VALUES
           (1
           ,'complex element 5'
           ,getdate()
           ,5
           ,2)
GO


INSERT INTO [dbo].[XsdComplexElement]
           ([IdXsdDocument]
           ,[XsdComplexElementName]
           ,[RegistrationDateTime]
           ,[XsdToWsdlRelationType]
           ,[IdWsdlOutput])
     VALUES
           (1
           ,'complex element 6'
           ,getdate()
           ,5
           ,3)
GO