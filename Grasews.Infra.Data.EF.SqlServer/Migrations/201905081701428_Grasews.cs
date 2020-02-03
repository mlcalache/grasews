namespace Grasews.Infra.Data.EF.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Grasews : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GraphNodePosition_OntologyTerm",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOwnerUser = c.Int(nullable: false),
                        IdOntologyTerm = c.Int(nullable: false),
                        X = c.Single(nullable: false),
                        Y = c.Single(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OntologyTerm", t => t.IdOntologyTerm, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.IdOwnerUser, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdOntologyTerm);
            
            CreateTable(
                "dbo.OntologyTerm",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOntology = c.Int(),
                        IdParentOntologyTerm = c.Int(),
                        TermName = c.String(nullable: false, maxLength: 400),
                        TermRaw = c.String(nullable: false, unicode: false, storeType: "text"),
                        TermUri = c.String(nullable: false, maxLength: 500),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OntologyTerm", t => t.IdParentOntologyTerm)
                .ForeignKey("dbo.Ontology", t => t.IdOntology)
                .Index(t => t.IdOntology)
                .Index(t => t.IdParentOntologyTerm)
                .Index(t => t.TermName, name: "UQ_OntologyTerm_TermName");
            
            CreateTable(
                "dbo.Ontology",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOwnerUser = c.Int(nullable: false),
                        OntologyName = c.String(nullable: false, maxLength: 400),
                        OntologyUrl = c.String(),
                        Xml = c.String(nullable: false, unicode: false, storeType: "text"),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.IdOwnerUser, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.OntologyName, unique: true, name: "UQ_Ontology_OntologyName");
            
            CreateTable(
                "dbo.Ontology_User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOntology = c.Int(nullable: false),
                        IdSharedUser = c.Int(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.IdSharedUser)
                .ForeignKey("dbo.Ontology", t => t.IdOntology)
                .Index(t => t.IdOntology)
                .Index(t => t.IdSharedUser);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 400),
                        FirstName = c.String(nullable: false, maxLength: 200),
                        IsAdmin = c.Boolean(nullable: false),
                        LastName = c.String(nullable: false, maxLength: 400),
                        Password = c.String(nullable: false, maxLength: 50),
                        Username = c.String(nullable: false, maxLength: 50),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Email, unique: true, name: "UQ_User_Email")
                .Index(t => t.Username, unique: true, name: "UQ_User_Username");
            
            CreateTable(
                "dbo.GraphNodePosition_SawsdlModelReference",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOwnerUser = c.Int(nullable: false),
                        IdSawsdlModelReference = c.Int(nullable: false),
                        X = c.Single(nullable: false),
                        Y = c.Single(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.IdOwnerUser, cascadeDelete: true)
                .ForeignKey("dbo.SawsdlModelReference", t => t.IdSawsdlModelReference, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdSawsdlModelReference);
            
            CreateTable(
                "dbo.SawsdlModelReference",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOntologyTerm = c.Int(nullable: false),
                        IdOwnerUser = c.Int(nullable: false),
                        IdServiceDescription = c.Int(nullable: false),
                        ModelReference = c.String(nullable: false, maxLength: 400),
                        RegistrationDateTime = c.DateTime(nullable: false),
                        IdWsdlInFault = c.Int(),
                        IdWsdlInterface = c.Int(),
                        IdWsdlOperation = c.Int(),
                        IdWsdlOutFault = c.Int(),
                        IdXsdComplexElement = c.Int(),
                        IdXsdElement = c.Int(),
                        IdXsdSimpleElement = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OntologyTerm", t => t.IdOntologyTerm)
                .ForeignKey("dbo.User", t => t.IdOwnerUser)
                .ForeignKey("dbo.ServiceDescription", t => t.IdServiceDescription)
                .ForeignKey("dbo.WsdlInFault", t => t.IdWsdlInFault, cascadeDelete: true)
                .ForeignKey("dbo.WsdlInterface", t => t.IdWsdlInterface, cascadeDelete: true)
                .ForeignKey("dbo.WsdlOperation", t => t.IdWsdlOperation, cascadeDelete: true)
                .ForeignKey("dbo.WsdlOutFault", t => t.IdWsdlOutFault, cascadeDelete: true)
                .ForeignKey("dbo.XsdComplexElement", t => t.IdXsdComplexElement, cascadeDelete: true)
                .ForeignKey("dbo.XsdElement", t => t.IdXsdElement, cascadeDelete: true)
                .ForeignKey("dbo.XsdSimpleElement", t => t.IdXsdSimpleElement, cascadeDelete: true)
                .Index(t => t.IdOntologyTerm)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdServiceDescription)
                .Index(t => t.IdWsdlInFault)
                .Index(t => t.IdWsdlInterface)
                .Index(t => t.IdWsdlOperation)
                .Index(t => t.IdWsdlOutFault)
                .Index(t => t.IdXsdComplexElement)
                .Index(t => t.IdXsdElement)
                .Index(t => t.IdXsdSimpleElement);
            
            CreateTable(
                "dbo.ServiceDescription",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GraphJson = c.String(unicode: false, storeType: "text"),
                        IdOwnerUser = c.Int(nullable: false),
                        ServiceName = c.String(nullable: false, maxLength: 400),
                        Xml = c.String(nullable: false, unicode: false, storeType: "text"),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.IdOwnerUser)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.ServiceName, unique: true, name: "UQ_ServiceDescription_ServiceName");
            
            CreateTable(
                "dbo.Issue",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, unicode: false, storeType: "text"),
                        IdOwnerUser = c.Int(nullable: false),
                        IdServiceDescription = c.Int(nullable: false),
                        Solved = c.Boolean(nullable: false),
                        Title = c.String(nullable: false, maxLength: 100),
                        RegistrationDateTime = c.DateTime(nullable: false),
                        IdWsdlInFault = c.Int(),
                        IdWsdlOperation = c.Int(),
                        IdWsdlInput = c.Int(),
                        IdXsdComplexElement = c.Int(),
                        IdWsdlOutFault = c.Int(),
                        IdXsdSimpleElement = c.Int(),
                        IdWsdlOutput = c.Int(),
                        IdXsdElement = c.Int(),
                        IdWsdlInterface = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServiceDescription", t => t.IdServiceDescription, cascadeDelete: true)
                .ForeignKey("dbo.WsdlInFault", t => t.IdWsdlInFault)
                .ForeignKey("dbo.WsdlOperation", t => t.IdWsdlOperation)
                .ForeignKey("dbo.WsdlInput", t => t.IdWsdlInput)
                .ForeignKey("dbo.XsdComplexElement", t => t.IdXsdComplexElement)
                .ForeignKey("dbo.WsdlOutFault", t => t.IdWsdlOutFault)
                .ForeignKey("dbo.XsdSimpleElement", t => t.IdXsdSimpleElement)
                .ForeignKey("dbo.WsdlOutput", t => t.IdWsdlOutput)
                .ForeignKey("dbo.XsdElement", t => t.IdXsdElement)
                .ForeignKey("dbo.WsdlInterface", t => t.IdWsdlInterface)
                .ForeignKey("dbo.User", t => t.IdOwnerUser)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdServiceDescription)
                .Index(t => t.IdWsdlInFault)
                .Index(t => t.IdWsdlOperation)
                .Index(t => t.IdWsdlInput)
                .Index(t => t.IdXsdComplexElement)
                .Index(t => t.IdWsdlOutFault)
                .Index(t => t.IdXsdSimpleElement)
                .Index(t => t.IdWsdlOutput)
                .Index(t => t.IdXsdElement)
                .Index(t => t.IdWsdlInterface);
            
            CreateTable(
                "dbo.IssueAnswer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Answer = c.String(nullable: false, unicode: false, storeType: "text"),
                        IdIssue = c.Int(nullable: false),
                        IdOwnerUser = c.Int(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.IdOwnerUser)
                .ForeignKey("dbo.Issue", t => t.IdIssue)
                .Index(t => t.IdIssue)
                .Index(t => t.IdOwnerUser);
            
            CreateTable(
                "dbo.WsdlInFault",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdWsdlOperation = c.Int(nullable: false),
                        WsdlInFaultName = c.String(),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WsdlOperation", t => t.IdWsdlOperation)
                .Index(t => t.IdWsdlOperation);
            
            CreateTable(
                "dbo.GraphNodePosition_WsdlInFault",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOwnerUser = c.Int(nullable: false),
                        IdWsdlInFault = c.Int(nullable: false),
                        X = c.Single(nullable: false),
                        Y = c.Single(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.IdOwnerUser, cascadeDelete: true)
                .ForeignKey("dbo.WsdlInFault", t => t.IdWsdlInFault, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdWsdlInFault);
            
            CreateTable(
                "dbo.WsdlOperation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdWsdlInterface = c.Int(nullable: false),
                        WsdlOperationName = c.String(nullable: false, maxLength: 400),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WsdlInterface", t => t.IdWsdlInterface)
                .Index(t => t.IdWsdlInterface);
            
            CreateTable(
                "dbo.GraphNodePosition_WsdlOperation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOwnerUser = c.Int(nullable: false),
                        IdWsdlOperation = c.Int(nullable: false),
                        X = c.Single(nullable: false),
                        Y = c.Single(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.IdOwnerUser)
                .ForeignKey("dbo.WsdlOperation", t => t.IdWsdlOperation, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdWsdlOperation);
            
            CreateTable(
                "dbo.WsdlInput",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdWsdlOperation = c.Int(nullable: false),
                        WsdlInputName = c.String(),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WsdlOperation", t => t.IdWsdlOperation)
                .Index(t => t.IdWsdlOperation);
            
            CreateTable(
                "dbo.GraphNodePosition_WsdlInput",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOwnerUser = c.Int(nullable: false),
                        IdWsdlInput = c.Int(nullable: false),
                        X = c.Single(nullable: false),
                        Y = c.Single(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.IdOwnerUser, cascadeDelete: true)
                .ForeignKey("dbo.WsdlInput", t => t.IdWsdlInput, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdWsdlInput);
            
            CreateTable(
                "dbo.XsdComplexElement",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdXsdDocument = c.Int(nullable: false),
                        XsdComplexElementName = c.String(),
                        LiftingSchemaMapping = c.String(),
                        LoweringSchemaMapping = c.String(),
                        RegistrationDateTime = c.DateTime(nullable: false),
                        IdWsdlOutFault = c.Int(),
                        IdWsdlOutput = c.Int(),
                        IdWsdlInput = c.Int(),
                        IdWsdlInFault = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WsdlOutFault", t => t.IdWsdlOutFault)
                .ForeignKey("dbo.WsdlOutput", t => t.IdWsdlOutput)
                .ForeignKey("dbo.XsdDocument", t => t.IdXsdDocument)
                .ForeignKey("dbo.WsdlInput", t => t.IdWsdlInput)
                .ForeignKey("dbo.WsdlInFault", t => t.IdWsdlInFault)
                .Index(t => t.IdXsdDocument)
                .Index(t => t.IdWsdlOutFault)
                .Index(t => t.IdWsdlOutput)
                .Index(t => t.IdWsdlInput)
                .Index(t => t.IdWsdlInFault);
            
            CreateTable(
                "dbo.GraphNodePosition_XsdComplexElement",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOwnerUser = c.Int(nullable: false),
                        IdXsdComplexElement = c.Int(nullable: false),
                        X = c.Single(nullable: false),
                        Y = c.Single(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.IdOwnerUser, cascadeDelete: true)
                .ForeignKey("dbo.XsdComplexElement", t => t.IdXsdComplexElement, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdXsdComplexElement);
            
            CreateTable(
                "dbo.WsdlOutFault",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdWsdlOperation = c.Int(nullable: false),
                        WsdlOutFaultName = c.String(),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WsdlOperation", t => t.IdWsdlOperation)
                .Index(t => t.IdWsdlOperation);
            
            CreateTable(
                "dbo.GraphNodePosition_WsdlOutFault",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOwnerUser = c.Int(nullable: false),
                        IdWsdlOutFault = c.Int(nullable: false),
                        X = c.Single(nullable: false),
                        Y = c.Single(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.IdOwnerUser, cascadeDelete: true)
                .ForeignKey("dbo.WsdlOutFault", t => t.IdWsdlOutFault, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdWsdlOutFault);
            
            CreateTable(
                "dbo.XsdSimpleElement",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdXsdDocument = c.Int(nullable: false),
                        XsdSimpleElementName = c.String(nullable: false),
                        LiftingSchemaMapping = c.String(),
                        LoweringSchemaMapping = c.String(),
                        RegistrationDateTime = c.DateTime(nullable: false),
                        IdWsdlOutput = c.Int(),
                        IdWsdlOutFault = c.Int(),
                        IdWsdlInput = c.Int(),
                        IdWsdlInFault = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WsdlOutput", t => t.IdWsdlOutput)
                .ForeignKey("dbo.XsdDocument", t => t.IdXsdDocument)
                .ForeignKey("dbo.WsdlOutFault", t => t.IdWsdlOutFault)
                .ForeignKey("dbo.WsdlInput", t => t.IdWsdlInput)
                .ForeignKey("dbo.WsdlInFault", t => t.IdWsdlInFault)
                .Index(t => t.IdXsdDocument)
                .Index(t => t.IdWsdlOutput)
                .Index(t => t.IdWsdlOutFault)
                .Index(t => t.IdWsdlInput)
                .Index(t => t.IdWsdlInFault);
            
            CreateTable(
                "dbo.GraphNodePosition_XsdSimpleElement",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOwnerUser = c.Int(nullable: false),
                        IdXsdSimpleElement = c.Int(nullable: false),
                        X = c.Single(nullable: false),
                        Y = c.Single(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.IdOwnerUser, cascadeDelete: true)
                .ForeignKey("dbo.XsdSimpleElement", t => t.IdXsdSimpleElement, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdXsdSimpleElement);
            
            CreateTable(
                "dbo.WsdlOutput",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdWsdlOperation = c.Int(nullable: false),
                        WsdlOutputName = c.String(),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WsdlOperation", t => t.IdWsdlOperation)
                .Index(t => t.IdWsdlOperation);
            
            CreateTable(
                "dbo.GraphNodePosition_WsdlOutput",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOwnerUser = c.Int(nullable: false),
                        IdWsdlOutput = c.Int(nullable: false),
                        X = c.Single(nullable: false),
                        Y = c.Single(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.IdOwnerUser, cascadeDelete: true)
                .ForeignKey("dbo.WsdlOutput", t => t.IdWsdlOutput, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdWsdlOutput);
            
            CreateTable(
                "dbo.XsdDocument",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        XsdPath = c.String(),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServiceDescription", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.XsdElement",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdXsdComplexElement = c.Int(nullable: false),
                        XsdElementName = c.String(nullable: false),
                        LiftingSchemaMapping = c.String(),
                        LoweringSchemaMapping = c.String(),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.XsdComplexElement", t => t.IdXsdComplexElement)
                .Index(t => t.IdXsdComplexElement);
            
            CreateTable(
                "dbo.GraphNodePosition_XsdElement",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOwnerUser = c.Int(nullable: false),
                        IdXsdElement = c.Int(nullable: false),
                        X = c.Single(nullable: false),
                        Y = c.Single(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.IdOwnerUser, cascadeDelete: true)
                .ForeignKey("dbo.XsdElement", t => t.IdXsdElement, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdXsdElement);
            
            CreateTable(
                "dbo.WsdlInterface",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdServiceDescription = c.Int(nullable: false),
                        WsdlInterfaceName = c.String(nullable: false, maxLength: 400),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServiceDescription", t => t.IdServiceDescription)
                .Index(t => t.IdServiceDescription);
            
            CreateTable(
                "dbo.GraphNodePosition_WsdlInterface",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOwnerUser = c.Int(nullable: false),
                        IdWsdlInterface = c.Int(nullable: false),
                        X = c.Single(nullable: false),
                        Y = c.Single(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.IdOwnerUser)
                .ForeignKey("dbo.WsdlInterface", t => t.IdWsdlInterface, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdWsdlInterface);
            
            CreateTable(
                "dbo.ServiceDescription_Ontology",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOntology = c.Int(nullable: false),
                        IdServiceDescription = c.Int(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServiceDescription", t => t.IdServiceDescription)
                .ForeignKey("dbo.Ontology", t => t.IdOntology)
                .Index(t => t.IdOntology)
                .Index(t => t.IdServiceDescription);
            
            CreateTable(
                "dbo.ServiceDescription_User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdServiceDescription = c.Int(nullable: false),
                        IdSharedUser = c.Int(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServiceDescription", t => t.IdServiceDescription)
                .ForeignKey("dbo.User", t => t.IdSharedUser)
                .Index(t => t.IdServiceDescription)
                .Index(t => t.IdSharedUser);
            
            CreateTable(
                "dbo.ShareInvitation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        IdServiceDescription = c.Int(nullable: false),
                        IdUserInviter = c.Int(nullable: false),
                        InvitationStatus = c.Int(nullable: false),
                        InvitationSecurity = c.Guid(nullable: false),
                        ExistingUser = c.Boolean(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServiceDescription", t => t.IdServiceDescription)
                .ForeignKey("dbo.User", t => t.IdUserInviter)
                .Index(t => t.IdServiceDescription)
                .Index(t => t.IdUserInviter);
            
            CreateTable(
                "dbo.Task",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 1000),
                        Done = c.Boolean(nullable: false),
                        IdOwnerUser = c.Int(nullable: false),
                        IdServiceDescription = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 100),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.IdOwnerUser)
                .ForeignKey("dbo.ServiceDescription", t => t.IdServiceDescription, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdServiceDescription);
            
            CreateTable(
                "dbo.TaskComment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(nullable: false, unicode: false, storeType: "text"),
                        IdOwnerUser = c.Int(nullable: false),
                        IdTask = c.Int(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.IdOwnerUser)
                .ForeignKey("dbo.Task", t => t.IdTask)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdTask);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GraphNodePosition_OntologyTerm", "IdOwnerUser", "dbo.User");
            DropForeignKey("dbo.GraphNodePosition_OntologyTerm", "IdOntologyTerm", "dbo.OntologyTerm");
            DropForeignKey("dbo.ServiceDescription_Ontology", "IdOntology", "dbo.Ontology");
            DropForeignKey("dbo.Ontology", "IdOwnerUser", "dbo.User");
            DropForeignKey("dbo.OntologyTerm", "IdOntology", "dbo.Ontology");
            DropForeignKey("dbo.Ontology_User", "IdOntology", "dbo.Ontology");
            DropForeignKey("dbo.Ontology_User", "IdSharedUser", "dbo.User");
            DropForeignKey("dbo.ShareInvitation", "IdUserInviter", "dbo.User");
            DropForeignKey("dbo.ServiceDescription", "IdOwnerUser", "dbo.User");
            DropForeignKey("dbo.Issue", "IdOwnerUser", "dbo.User");
            DropForeignKey("dbo.GraphNodePosition_SawsdlModelReference", "IdSawsdlModelReference", "dbo.SawsdlModelReference");
            DropForeignKey("dbo.SawsdlModelReference", "IdXsdSimpleElement", "dbo.XsdSimpleElement");
            DropForeignKey("dbo.SawsdlModelReference", "IdXsdElement", "dbo.XsdElement");
            DropForeignKey("dbo.SawsdlModelReference", "IdXsdComplexElement", "dbo.XsdComplexElement");
            DropForeignKey("dbo.SawsdlModelReference", "IdWsdlOutFault", "dbo.WsdlOutFault");
            DropForeignKey("dbo.SawsdlModelReference", "IdWsdlOperation", "dbo.WsdlOperation");
            DropForeignKey("dbo.SawsdlModelReference", "IdWsdlInterface", "dbo.WsdlInterface");
            DropForeignKey("dbo.SawsdlModelReference", "IdWsdlInFault", "dbo.WsdlInFault");
            DropForeignKey("dbo.SawsdlModelReference", "IdServiceDescription", "dbo.ServiceDescription");
            DropForeignKey("dbo.XsdDocument", "Id", "dbo.ServiceDescription");
            DropForeignKey("dbo.WsdlInterface", "IdServiceDescription", "dbo.ServiceDescription");
            DropForeignKey("dbo.TaskComment", "IdTask", "dbo.Task");
            DropForeignKey("dbo.TaskComment", "IdOwnerUser", "dbo.User");
            DropForeignKey("dbo.Task", "IdServiceDescription", "dbo.ServiceDescription");
            DropForeignKey("dbo.Task", "IdOwnerUser", "dbo.User");
            DropForeignKey("dbo.ShareInvitation", "IdServiceDescription", "dbo.ServiceDescription");
            DropForeignKey("dbo.ServiceDescription_User", "IdSharedUser", "dbo.User");
            DropForeignKey("dbo.ServiceDescription_User", "IdServiceDescription", "dbo.ServiceDescription");
            DropForeignKey("dbo.ServiceDescription_Ontology", "IdServiceDescription", "dbo.ServiceDescription");
            DropForeignKey("dbo.XsdSimpleElement", "IdWsdlInFault", "dbo.WsdlInFault");
            DropForeignKey("dbo.XsdComplexElement", "IdWsdlInFault", "dbo.WsdlInFault");
            DropForeignKey("dbo.WsdlOutput", "IdWsdlOperation", "dbo.WsdlOperation");
            DropForeignKey("dbo.WsdlOutFault", "IdWsdlOperation", "dbo.WsdlOperation");
            DropForeignKey("dbo.WsdlOperation", "IdWsdlInterface", "dbo.WsdlInterface");
            DropForeignKey("dbo.Issue", "IdWsdlInterface", "dbo.WsdlInterface");
            DropForeignKey("dbo.GraphNodePosition_WsdlInterface", "IdWsdlInterface", "dbo.WsdlInterface");
            DropForeignKey("dbo.GraphNodePosition_WsdlInterface", "IdOwnerUser", "dbo.User");
            DropForeignKey("dbo.WsdlInput", "IdWsdlOperation", "dbo.WsdlOperation");
            DropForeignKey("dbo.XsdSimpleElement", "IdWsdlInput", "dbo.WsdlInput");
            DropForeignKey("dbo.XsdComplexElement", "IdWsdlInput", "dbo.WsdlInput");
            DropForeignKey("dbo.XsdElement", "IdXsdComplexElement", "dbo.XsdComplexElement");
            DropForeignKey("dbo.Issue", "IdXsdElement", "dbo.XsdElement");
            DropForeignKey("dbo.GraphNodePosition_XsdElement", "IdXsdElement", "dbo.XsdElement");
            DropForeignKey("dbo.GraphNodePosition_XsdElement", "IdOwnerUser", "dbo.User");
            DropForeignKey("dbo.XsdSimpleElement", "IdWsdlOutFault", "dbo.WsdlOutFault");
            DropForeignKey("dbo.XsdSimpleElement", "IdXsdDocument", "dbo.XsdDocument");
            DropForeignKey("dbo.XsdComplexElement", "IdXsdDocument", "dbo.XsdDocument");
            DropForeignKey("dbo.XsdSimpleElement", "IdWsdlOutput", "dbo.WsdlOutput");
            DropForeignKey("dbo.XsdComplexElement", "IdWsdlOutput", "dbo.WsdlOutput");
            DropForeignKey("dbo.Issue", "IdWsdlOutput", "dbo.WsdlOutput");
            DropForeignKey("dbo.GraphNodePosition_WsdlOutput", "IdWsdlOutput", "dbo.WsdlOutput");
            DropForeignKey("dbo.GraphNodePosition_WsdlOutput", "IdOwnerUser", "dbo.User");
            DropForeignKey("dbo.Issue", "IdXsdSimpleElement", "dbo.XsdSimpleElement");
            DropForeignKey("dbo.GraphNodePosition_XsdSimpleElement", "IdXsdSimpleElement", "dbo.XsdSimpleElement");
            DropForeignKey("dbo.GraphNodePosition_XsdSimpleElement", "IdOwnerUser", "dbo.User");
            DropForeignKey("dbo.XsdComplexElement", "IdWsdlOutFault", "dbo.WsdlOutFault");
            DropForeignKey("dbo.Issue", "IdWsdlOutFault", "dbo.WsdlOutFault");
            DropForeignKey("dbo.GraphNodePosition_WsdlOutFault", "IdWsdlOutFault", "dbo.WsdlOutFault");
            DropForeignKey("dbo.GraphNodePosition_WsdlOutFault", "IdOwnerUser", "dbo.User");
            DropForeignKey("dbo.Issue", "IdXsdComplexElement", "dbo.XsdComplexElement");
            DropForeignKey("dbo.GraphNodePosition_XsdComplexElement", "IdXsdComplexElement", "dbo.XsdComplexElement");
            DropForeignKey("dbo.GraphNodePosition_XsdComplexElement", "IdOwnerUser", "dbo.User");
            DropForeignKey("dbo.Issue", "IdWsdlInput", "dbo.WsdlInput");
            DropForeignKey("dbo.GraphNodePosition_WsdlInput", "IdWsdlInput", "dbo.WsdlInput");
            DropForeignKey("dbo.GraphNodePosition_WsdlInput", "IdOwnerUser", "dbo.User");
            DropForeignKey("dbo.WsdlInFault", "IdWsdlOperation", "dbo.WsdlOperation");
            DropForeignKey("dbo.Issue", "IdWsdlOperation", "dbo.WsdlOperation");
            DropForeignKey("dbo.GraphNodePosition_WsdlOperation", "IdWsdlOperation", "dbo.WsdlOperation");
            DropForeignKey("dbo.GraphNodePosition_WsdlOperation", "IdOwnerUser", "dbo.User");
            DropForeignKey("dbo.Issue", "IdWsdlInFault", "dbo.WsdlInFault");
            DropForeignKey("dbo.GraphNodePosition_WsdlInFault", "IdWsdlInFault", "dbo.WsdlInFault");
            DropForeignKey("dbo.GraphNodePosition_WsdlInFault", "IdOwnerUser", "dbo.User");
            DropForeignKey("dbo.Issue", "IdServiceDescription", "dbo.ServiceDescription");
            DropForeignKey("dbo.IssueAnswer", "IdIssue", "dbo.Issue");
            DropForeignKey("dbo.IssueAnswer", "IdOwnerUser", "dbo.User");
            DropForeignKey("dbo.SawsdlModelReference", "IdOwnerUser", "dbo.User");
            DropForeignKey("dbo.SawsdlModelReference", "IdOntologyTerm", "dbo.OntologyTerm");
            DropForeignKey("dbo.GraphNodePosition_SawsdlModelReference", "IdOwnerUser", "dbo.User");
            DropForeignKey("dbo.OntologyTerm", "IdParentOntologyTerm", "dbo.OntologyTerm");
            DropIndex("dbo.TaskComment", new[] { "IdTask" });
            DropIndex("dbo.TaskComment", new[] { "IdOwnerUser" });
            DropIndex("dbo.Task", new[] { "IdServiceDescription" });
            DropIndex("dbo.Task", new[] { "IdOwnerUser" });
            DropIndex("dbo.ShareInvitation", new[] { "IdUserInviter" });
            DropIndex("dbo.ShareInvitation", new[] { "IdServiceDescription" });
            DropIndex("dbo.ServiceDescription_User", new[] { "IdSharedUser" });
            DropIndex("dbo.ServiceDescription_User", new[] { "IdServiceDescription" });
            DropIndex("dbo.ServiceDescription_Ontology", new[] { "IdServiceDescription" });
            DropIndex("dbo.ServiceDescription_Ontology", new[] { "IdOntology" });
            DropIndex("dbo.GraphNodePosition_WsdlInterface", new[] { "IdWsdlInterface" });
            DropIndex("dbo.GraphNodePosition_WsdlInterface", new[] { "IdOwnerUser" });
            DropIndex("dbo.WsdlInterface", new[] { "IdServiceDescription" });
            DropIndex("dbo.GraphNodePosition_XsdElement", new[] { "IdXsdElement" });
            DropIndex("dbo.GraphNodePosition_XsdElement", new[] { "IdOwnerUser" });
            DropIndex("dbo.XsdElement", new[] { "IdXsdComplexElement" });
            DropIndex("dbo.XsdDocument", new[] { "Id" });
            DropIndex("dbo.GraphNodePosition_WsdlOutput", new[] { "IdWsdlOutput" });
            DropIndex("dbo.GraphNodePosition_WsdlOutput", new[] { "IdOwnerUser" });
            DropIndex("dbo.WsdlOutput", new[] { "IdWsdlOperation" });
            DropIndex("dbo.GraphNodePosition_XsdSimpleElement", new[] { "IdXsdSimpleElement" });
            DropIndex("dbo.GraphNodePosition_XsdSimpleElement", new[] { "IdOwnerUser" });
            DropIndex("dbo.XsdSimpleElement", new[] { "IdWsdlInFault" });
            DropIndex("dbo.XsdSimpleElement", new[] { "IdWsdlInput" });
            DropIndex("dbo.XsdSimpleElement", new[] { "IdWsdlOutFault" });
            DropIndex("dbo.XsdSimpleElement", new[] { "IdWsdlOutput" });
            DropIndex("dbo.XsdSimpleElement", new[] { "IdXsdDocument" });
            DropIndex("dbo.GraphNodePosition_WsdlOutFault", new[] { "IdWsdlOutFault" });
            DropIndex("dbo.GraphNodePosition_WsdlOutFault", new[] { "IdOwnerUser" });
            DropIndex("dbo.WsdlOutFault", new[] { "IdWsdlOperation" });
            DropIndex("dbo.GraphNodePosition_XsdComplexElement", new[] { "IdXsdComplexElement" });
            DropIndex("dbo.GraphNodePosition_XsdComplexElement", new[] { "IdOwnerUser" });
            DropIndex("dbo.XsdComplexElement", new[] { "IdWsdlInFault" });
            DropIndex("dbo.XsdComplexElement", new[] { "IdWsdlInput" });
            DropIndex("dbo.XsdComplexElement", new[] { "IdWsdlOutput" });
            DropIndex("dbo.XsdComplexElement", new[] { "IdWsdlOutFault" });
            DropIndex("dbo.XsdComplexElement", new[] { "IdXsdDocument" });
            DropIndex("dbo.GraphNodePosition_WsdlInput", new[] { "IdWsdlInput" });
            DropIndex("dbo.GraphNodePosition_WsdlInput", new[] { "IdOwnerUser" });
            DropIndex("dbo.WsdlInput", new[] { "IdWsdlOperation" });
            DropIndex("dbo.GraphNodePosition_WsdlOperation", new[] { "IdWsdlOperation" });
            DropIndex("dbo.GraphNodePosition_WsdlOperation", new[] { "IdOwnerUser" });
            DropIndex("dbo.WsdlOperation", new[] { "IdWsdlInterface" });
            DropIndex("dbo.GraphNodePosition_WsdlInFault", new[] { "IdWsdlInFault" });
            DropIndex("dbo.GraphNodePosition_WsdlInFault", new[] { "IdOwnerUser" });
            DropIndex("dbo.WsdlInFault", new[] { "IdWsdlOperation" });
            DropIndex("dbo.IssueAnswer", new[] { "IdOwnerUser" });
            DropIndex("dbo.IssueAnswer", new[] { "IdIssue" });
            DropIndex("dbo.Issue", new[] { "IdWsdlInterface" });
            DropIndex("dbo.Issue", new[] { "IdXsdElement" });
            DropIndex("dbo.Issue", new[] { "IdWsdlOutput" });
            DropIndex("dbo.Issue", new[] { "IdXsdSimpleElement" });
            DropIndex("dbo.Issue", new[] { "IdWsdlOutFault" });
            DropIndex("dbo.Issue", new[] { "IdXsdComplexElement" });
            DropIndex("dbo.Issue", new[] { "IdWsdlInput" });
            DropIndex("dbo.Issue", new[] { "IdWsdlOperation" });
            DropIndex("dbo.Issue", new[] { "IdWsdlInFault" });
            DropIndex("dbo.Issue", new[] { "IdServiceDescription" });
            DropIndex("dbo.Issue", new[] { "IdOwnerUser" });
            DropIndex("dbo.ServiceDescription", "UQ_ServiceDescription_ServiceName");
            DropIndex("dbo.ServiceDescription", new[] { "IdOwnerUser" });
            DropIndex("dbo.SawsdlModelReference", new[] { "IdXsdSimpleElement" });
            DropIndex("dbo.SawsdlModelReference", new[] { "IdXsdElement" });
            DropIndex("dbo.SawsdlModelReference", new[] { "IdXsdComplexElement" });
            DropIndex("dbo.SawsdlModelReference", new[] { "IdWsdlOutFault" });
            DropIndex("dbo.SawsdlModelReference", new[] { "IdWsdlOperation" });
            DropIndex("dbo.SawsdlModelReference", new[] { "IdWsdlInterface" });
            DropIndex("dbo.SawsdlModelReference", new[] { "IdWsdlInFault" });
            DropIndex("dbo.SawsdlModelReference", new[] { "IdServiceDescription" });
            DropIndex("dbo.SawsdlModelReference", new[] { "IdOwnerUser" });
            DropIndex("dbo.SawsdlModelReference", new[] { "IdOntologyTerm" });
            DropIndex("dbo.GraphNodePosition_SawsdlModelReference", new[] { "IdSawsdlModelReference" });
            DropIndex("dbo.GraphNodePosition_SawsdlModelReference", new[] { "IdOwnerUser" });
            DropIndex("dbo.User", "UQ_User_Username");
            DropIndex("dbo.User", "UQ_User_Email");
            DropIndex("dbo.Ontology_User", new[] { "IdSharedUser" });
            DropIndex("dbo.Ontology_User", new[] { "IdOntology" });
            DropIndex("dbo.Ontology", "UQ_Ontology_OntologyName");
            DropIndex("dbo.Ontology", new[] { "IdOwnerUser" });
            DropIndex("dbo.OntologyTerm", "UQ_OntologyTerm_TermName");
            DropIndex("dbo.OntologyTerm", new[] { "IdParentOntologyTerm" });
            DropIndex("dbo.OntologyTerm", new[] { "IdOntology" });
            DropIndex("dbo.GraphNodePosition_OntologyTerm", new[] { "IdOntologyTerm" });
            DropIndex("dbo.GraphNodePosition_OntologyTerm", new[] { "IdOwnerUser" });
            DropTable("dbo.TaskComment");
            DropTable("dbo.Task");
            DropTable("dbo.ShareInvitation");
            DropTable("dbo.ServiceDescription_User");
            DropTable("dbo.ServiceDescription_Ontology");
            DropTable("dbo.GraphNodePosition_WsdlInterface");
            DropTable("dbo.WsdlInterface");
            DropTable("dbo.GraphNodePosition_XsdElement");
            DropTable("dbo.XsdElement");
            DropTable("dbo.XsdDocument");
            DropTable("dbo.GraphNodePosition_WsdlOutput");
            DropTable("dbo.WsdlOutput");
            DropTable("dbo.GraphNodePosition_XsdSimpleElement");
            DropTable("dbo.XsdSimpleElement");
            DropTable("dbo.GraphNodePosition_WsdlOutFault");
            DropTable("dbo.WsdlOutFault");
            DropTable("dbo.GraphNodePosition_XsdComplexElement");
            DropTable("dbo.XsdComplexElement");
            DropTable("dbo.GraphNodePosition_WsdlInput");
            DropTable("dbo.WsdlInput");
            DropTable("dbo.GraphNodePosition_WsdlOperation");
            DropTable("dbo.WsdlOperation");
            DropTable("dbo.GraphNodePosition_WsdlInFault");
            DropTable("dbo.WsdlInFault");
            DropTable("dbo.IssueAnswer");
            DropTable("dbo.Issue");
            DropTable("dbo.ServiceDescription");
            DropTable("dbo.SawsdlModelReference");
            DropTable("dbo.GraphNodePosition_SawsdlModelReference");
            DropTable("dbo.User");
            DropTable("dbo.Ontology_User");
            DropTable("dbo.Ontology");
            DropTable("dbo.OntologyTerm");
            DropTable("dbo.GraphNodePosition_OntologyTerm");
        }
    }
}
