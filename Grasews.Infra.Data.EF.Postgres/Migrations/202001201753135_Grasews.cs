namespace Grasews.Infra.Data.EF.Postgres.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Grasews : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.GraphNodePosition_OntologyTerm",
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
                .ForeignKey("public.OntologyTerm", t => t.IdOntologyTerm, cascadeDelete: true)
                .ForeignKey("public.User", t => t.IdOwnerUser, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdOntologyTerm);
            
            CreateTable(
                "public.OntologyTerm",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOntology = c.Int(nullable: false),
                        IdParentOntologyTerm = c.Int(),
                        TermName = c.String(nullable: false, maxLength: 400),
                        TermRaw = c.String(nullable: false),
                        TermUri = c.String(nullable: false, maxLength: 500),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.OntologyTerm", t => t.IdParentOntologyTerm, cascadeDelete: true)
                .ForeignKey("public.Ontology", t => t.IdOntology, cascadeDelete: true)
                .Index(t => t.IdOntology)
                .Index(t => t.IdParentOntologyTerm);
            
            CreateTable(
                "public.Ontology",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HexColor = c.String(nullable: false, maxLength: 7),
                        IdOwnerUser = c.Int(nullable: false),
                        OntologyName = c.String(nullable: false, maxLength: 400),
                        OntologyUrl = c.String(),
                        Xml = c.String(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.User", t => t.IdOwnerUser, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.OntologyName, unique: true, name: "UQ_Ontology_OntologyName");
            
            CreateTable(
                "public.Ontology_User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOntology = c.Int(nullable: false),
                        IdSharedUser = c.Int(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.User", t => t.IdSharedUser, cascadeDelete: true)
                .ForeignKey("public.Ontology", t => t.IdOntology, cascadeDelete: true)
                .Index(t => t.IdOntology)
                .Index(t => t.IdSharedUser);
            
            CreateTable(
                "public.User",
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
                "public.GraphNodePosition_SawsdlModelReference",
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
                .ForeignKey("public.User", t => t.IdOwnerUser, cascadeDelete: true)
                .ForeignKey("public.SawsdlModelReference", t => t.IdSawsdlModelReference, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdSawsdlModelReference);
            
            CreateTable(
                "public.SawsdlModelReference",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOntologyTerm = c.Int(nullable: false),
                        IdOwnerUser = c.Int(nullable: false),
                        IdServiceDescription = c.Int(nullable: false),
                        ModelReference = c.String(nullable: false, maxLength: 400),
                        RegistrationDateTime = c.DateTime(nullable: false),
                        IdWsdlFault = c.Int(),
                        IdWsdlInterface = c.Int(),
                        IdWsdlOperation = c.Int(),
                        IdXsdComplexType = c.Int(),
                        IdXsdSimpleType = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.OntologyTerm", t => t.IdOntologyTerm, cascadeDelete: true)
                .ForeignKey("public.User", t => t.IdOwnerUser, cascadeDelete: true)
                .ForeignKey("public.ServiceDescription", t => t.IdServiceDescription, cascadeDelete: true)
                .ForeignKey("public.WsdlFault", t => t.IdWsdlFault, cascadeDelete: true)
                .ForeignKey("public.WsdlInterface", t => t.IdWsdlInterface, cascadeDelete: true)
                .ForeignKey("public.WsdlOperation", t => t.IdWsdlOperation, cascadeDelete: true)
                .ForeignKey("public.XsdComplexType", t => t.IdXsdComplexType, cascadeDelete: true)
                .ForeignKey("public.XsdSimpleType", t => t.IdXsdSimpleType, cascadeDelete: true)
                .Index(t => t.IdOntologyTerm)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdServiceDescription)
                .Index(t => t.IdWsdlFault)
                .Index(t => t.IdWsdlInterface)
                .Index(t => t.IdWsdlOperation)
                .Index(t => t.IdXsdComplexType)
                .Index(t => t.IdXsdSimpleType);
            
            CreateTable(
                "public.ServiceDescription",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GraphJson = c.String(),
                        IdOwnerUser = c.Int(nullable: false),
                        ServiceName = c.String(nullable: false, maxLength: 400),
                        Xml = c.String(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.User", t => t.IdOwnerUser, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.ServiceName, unique: true, name: "UQ_ServiceDescription_ServiceName");
            
            CreateTable(
                "public.Issue",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        IdOwnerUser = c.Int(nullable: false),
                        IdServiceDescription = c.Int(nullable: false),
                        IdWsdlFault = c.Int(),
                        IdWsdlInterface = c.Int(),
                        IdWsdlOperation = c.Int(),
                        IdXsdComplexType = c.Int(),
                        IdXsdSimpleType = c.Int(),
                        Solved = c.Boolean(nullable: false),
                        Title = c.String(nullable: false, maxLength: 100),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.ServiceDescription", t => t.IdServiceDescription, cascadeDelete: true)
                .ForeignKey("public.WsdlFault", t => t.IdWsdlFault, cascadeDelete: true)
                .ForeignKey("public.WsdlOperation", t => t.IdWsdlOperation, cascadeDelete: true)
                .ForeignKey("public.XsdComplexType", t => t.IdXsdComplexType, cascadeDelete: true)
                .ForeignKey("public.XsdSimpleType", t => t.IdXsdSimpleType, cascadeDelete: true)
                .ForeignKey("public.WsdlInterface", t => t.IdWsdlInterface, cascadeDelete: true)
                .ForeignKey("public.User", t => t.IdOwnerUser, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdServiceDescription)
                .Index(t => t.IdWsdlFault)
                .Index(t => t.IdWsdlInterface)
                .Index(t => t.IdWsdlOperation)
                .Index(t => t.IdXsdComplexType)
                .Index(t => t.IdXsdSimpleType);
            
            CreateTable(
                "public.IssueAnswer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Answer = c.String(nullable: false),
                        IdIssue = c.Int(nullable: false),
                        IdOwnerUser = c.Int(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.User", t => t.IdOwnerUser, cascadeDelete: true)
                .ForeignKey("public.Issue", t => t.IdIssue, cascadeDelete: true)
                .Index(t => t.IdIssue)
                .Index(t => t.IdOwnerUser);
            
            CreateTable(
                "public.WsdlFault",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdWsdlInterface = c.Int(nullable: false),
                        WsdlFaultName = c.String(nullable: false, maxLength: 400),
                        LiftingSchemaMapping = c.String(),
                        LoweringSchemaMapping = c.String(),
                        RegistrationDateTime = c.DateTime(nullable: false),
                        IdWsdlInfault = c.Int(),
                        IdWsdlOutfault = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.WsdlInfault", t => t.IdWsdlInfault, cascadeDelete: true)
                .ForeignKey("public.WsdlOutfault", t => t.IdWsdlOutfault, cascadeDelete: true)
                .ForeignKey("public.WsdlInterface", t => t.IdWsdlInterface, cascadeDelete: true)
                .Index(t => t.IdWsdlInterface)
                .Index(t => t.IdWsdlInfault)
                .Index(t => t.IdWsdlOutfault);
            
            CreateTable(
                "public.GraphNodePosition_WsdlFault",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOwnerUser = c.Int(nullable: false),
                        IdWsdlFault = c.Int(nullable: false),
                        X = c.Single(nullable: false),
                        Y = c.Single(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.User", t => t.IdOwnerUser, cascadeDelete: true)
                .ForeignKey("public.WsdlFault", t => t.IdWsdlFault, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdWsdlFault);
            
            CreateTable(
                "public.WsdlInfault",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdWsdlOperation = c.Int(nullable: false),
                        IdWsdlFault = c.Int(nullable: false),
                        WsdlInfaultName = c.String(nullable: false, maxLength: 400),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.WsdlOperation", t => t.IdWsdlOperation, cascadeDelete: true)
                .Index(t => t.IdWsdlOperation);
            
            CreateTable(
                "public.GraphNodePosition_WsdlInfault",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOwnerUser = c.Int(nullable: false),
                        IdWsdlInfault = c.Int(nullable: false),
                        X = c.Single(nullable: false),
                        Y = c.Single(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.User", t => t.IdOwnerUser, cascadeDelete: true)
                .ForeignKey("public.WsdlInfault", t => t.IdWsdlInfault, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdWsdlInfault);
            
            CreateTable(
                "public.WsdlOperation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdWsdlInterface = c.Int(nullable: false),
                        WsdlOperationName = c.String(nullable: false, maxLength: 400),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.WsdlInterface", t => t.IdWsdlInterface, cascadeDelete: true)
                .Index(t => t.IdWsdlInterface);
            
            CreateTable(
                "public.GraphNodePosition_WsdlOperation",
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
                .ForeignKey("public.User", t => t.IdOwnerUser, cascadeDelete: true)
                .ForeignKey("public.WsdlOperation", t => t.IdWsdlOperation, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdWsdlOperation);
            
            CreateTable(
                "public.WsdlInput",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdWsdlOperation = c.Int(nullable: false),
                        WsdlInputName = c.String(),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.WsdlOperation", t => t.IdWsdlOperation, cascadeDelete: true)
                .Index(t => t.IdWsdlOperation);
            
            CreateTable(
                "public.GraphNodePosition_WsdlInput",
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
                .ForeignKey("public.User", t => t.IdOwnerUser, cascadeDelete: true)
                .ForeignKey("public.WsdlInput", t => t.IdWsdlInput, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdWsdlInput);
            
            CreateTable(
                "public.XsdComplexType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdXsdDocument = c.Int(nullable: false),
                        XsdComplexTypeName = c.String(nullable: false),
                        XsdComplexTypeElementType = c.String(nullable: false),
                        LiftingSchemaMapping = c.String(),
                        LoweringSchemaMapping = c.String(),
                        RegistrationDateTime = c.DateTime(nullable: false),
                        IdWsdlOutfault = c.Int(),
                        IdWsdlOutput = c.Int(),
                        IdWsdlInput = c.Int(),
                        IdWsdlInfault = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.WsdlOutfault", t => t.IdWsdlOutfault, cascadeDelete: true)
                .ForeignKey("public.WsdlOutput", t => t.IdWsdlOutput, cascadeDelete: true)
                .ForeignKey("public.XsdDocument", t => t.IdXsdDocument, cascadeDelete: true)
                .ForeignKey("public.WsdlInput", t => t.IdWsdlInput, cascadeDelete: true)
                .ForeignKey("public.WsdlInfault", t => t.IdWsdlInfault, cascadeDelete: true)
                .Index(t => t.IdXsdDocument)
                .Index(t => t.IdWsdlOutfault)
                .Index(t => t.IdWsdlOutput)
                .Index(t => t.IdWsdlInput)
                .Index(t => t.IdWsdlInfault);
            
            CreateTable(
                "public.GraphNodePosition_XsdComplexType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOwnerUser = c.Int(nullable: false),
                        IdXsdComplexType = c.Int(nullable: false),
                        X = c.Single(nullable: false),
                        Y = c.Single(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.User", t => t.IdOwnerUser, cascadeDelete: true)
                .ForeignKey("public.XsdComplexType", t => t.IdXsdComplexType, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdXsdComplexType);
            
            CreateTable(
                "public.WsdlOutfault",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdWsdlOperation = c.Int(nullable: false),
                        WsdlOutfaultName = c.String(nullable: false, maxLength: 400),
                        IdWsdlFault = c.Int(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.WsdlOperation", t => t.IdWsdlOperation, cascadeDelete: true)
                .Index(t => t.IdWsdlOperation);
            
            CreateTable(
                "public.GraphNodePosition_WsdlOutfault",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOwnerUser = c.Int(nullable: false),
                        IdWsdlOutfault = c.Int(nullable: false),
                        X = c.Single(nullable: false),
                        Y = c.Single(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.User", t => t.IdOwnerUser, cascadeDelete: true)
                .ForeignKey("public.WsdlOutfault", t => t.IdWsdlOutfault, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdWsdlOutfault);
            
            CreateTable(
                "public.XsdSimpleType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdXsdDocument = c.Int(nullable: false),
                        XsdSimpleTypeName = c.String(nullable: false),
                        XsdSimpleTypeElementType = c.String(nullable: false),
                        LiftingSchemaMapping = c.String(),
                        LoweringSchemaMapping = c.String(),
                        RegistrationDateTime = c.DateTime(nullable: false),
                        IdWsdlOutput = c.Int(),
                        IdXsdComplexType = c.Int(),
                        IdWsdlOutfault = c.Int(),
                        IdWsdlInput = c.Int(),
                        IdWsdlInfault = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.WsdlOutput", t => t.IdWsdlOutput, cascadeDelete: true)
                .ForeignKey("public.XsdComplexType", t => t.IdXsdComplexType, cascadeDelete: true)
                .ForeignKey("public.XsdDocument", t => t.IdXsdDocument, cascadeDelete: true)
                .ForeignKey("public.WsdlOutfault", t => t.IdWsdlOutfault, cascadeDelete: true)
                .ForeignKey("public.WsdlInput", t => t.IdWsdlInput, cascadeDelete: true)
                .ForeignKey("public.WsdlInfault", t => t.IdWsdlInfault, cascadeDelete: true)
                .Index(t => t.IdXsdDocument)
                .Index(t => t.IdWsdlOutput)
                .Index(t => t.IdXsdComplexType)
                .Index(t => t.IdWsdlOutfault)
                .Index(t => t.IdWsdlInput)
                .Index(t => t.IdWsdlInfault);
            
            CreateTable(
                "public.GraphNodePosition_XsdSimpleType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOwnerUser = c.Int(nullable: false),
                        IdXsdSimpleType = c.Int(nullable: false),
                        X = c.Single(nullable: false),
                        Y = c.Single(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.User", t => t.IdOwnerUser, cascadeDelete: true)
                .ForeignKey("public.XsdSimpleType", t => t.IdXsdSimpleType, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdXsdSimpleType);
            
            CreateTable(
                "public.WsdlOutput",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdWsdlOperation = c.Int(nullable: false),
                        WsdlOutputName = c.String(),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.WsdlOperation", t => t.IdWsdlOperation, cascadeDelete: true)
                .Index(t => t.IdWsdlOperation);
            
            CreateTable(
                "public.GraphNodePosition_WsdlOutput",
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
                .ForeignKey("public.User", t => t.IdOwnerUser, cascadeDelete: true)
                .ForeignKey("public.WsdlOutput", t => t.IdWsdlOutput, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdWsdlOutput);
            
            CreateTable(
                "public.XsdDocument",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        XsdPath = c.String(),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.ServiceDescription", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "public.WsdlInterface",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdServiceDescription = c.Int(nullable: false),
                        WsdlInterfaceName = c.String(nullable: false, maxLength: 400),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.ServiceDescription", t => t.IdServiceDescription, cascadeDelete: true)
                .Index(t => t.IdServiceDescription);
            
            CreateTable(
                "public.GraphNodePosition_WsdlInterface",
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
                .ForeignKey("public.User", t => t.IdOwnerUser, cascadeDelete: true)
                .ForeignKey("public.WsdlInterface", t => t.IdWsdlInterface, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdWsdlInterface);
            
            CreateTable(
                "public.ServiceDescription_Ontology",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOntology = c.Int(nullable: false),
                        IdServiceDescription = c.Int(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.ServiceDescription", t => t.IdServiceDescription, cascadeDelete: true)
                .ForeignKey("public.Ontology", t => t.IdOntology, cascadeDelete: true)
                .Index(t => t.IdOntology)
                .Index(t => t.IdServiceDescription);
            
            CreateTable(
                "public.ServiceDescription_User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdServiceDescription = c.Int(nullable: false),
                        IdSharedUser = c.Int(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.ServiceDescription", t => t.IdServiceDescription, cascadeDelete: true)
                .ForeignKey("public.User", t => t.IdSharedUser, cascadeDelete: true)
                .Index(t => t.IdServiceDescription)
                .Index(t => t.IdSharedUser);
            
            CreateTable(
                "public.ShareInvitation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        ExistingUser = c.Boolean(nullable: false),
                        IdServiceDescription = c.Int(nullable: false),
                        IdUserInviter = c.Int(nullable: false),
                        InvitationSecurity = c.Guid(nullable: false),
                        InvitationStatus = c.Int(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.ServiceDescription", t => t.IdServiceDescription, cascadeDelete: true)
                .ForeignKey("public.User", t => t.IdUserInviter, cascadeDelete: true)
                .Index(t => t.IdServiceDescription)
                .Index(t => t.IdUserInviter);
            
            CreateTable(
                "public.ShareInvitation_Ontology",
                c => new
                    {
                        IdShareInvitation = c.Int(nullable: false),
                        IdOntology = c.Int(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdShareInvitation, t.IdOntology })
                .ForeignKey("public.Ontology", t => t.IdOntology, cascadeDelete: true)
                .ForeignKey("public.ShareInvitation", t => t.IdShareInvitation, cascadeDelete: true)
                .Index(t => t.IdShareInvitation)
                .Index(t => t.IdOntology);
            
            CreateTable(
                "public.Task",
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
                .ForeignKey("public.User", t => t.IdOwnerUser, cascadeDelete: true)
                .ForeignKey("public.ServiceDescription", t => t.IdServiceDescription, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdServiceDescription);
            
            CreateTable(
                "public.TaskComment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(nullable: false),
                        IdOwnerUser = c.Int(nullable: false),
                        IdTask = c.Int(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.User", t => t.IdOwnerUser, cascadeDelete: true)
                .ForeignKey("public.Task", t => t.IdTask, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdTask);
            
            CreateTable(
                "public.ResetPassword",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        IdUser = c.Int(nullable: false),
                        NewPassword = c.String(maxLength: 50),
                        ResetPasswordSecurity = c.Guid(nullable: false),
                        ResetPasswordStatus = c.Int(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.User", t => t.IdUser, cascadeDelete: true)
                .Index(t => t.IdUser);
            
            CreateTable(
                "public.XsdComplexComplex",
                c => new
                    {
                        IdParent = c.Int(nullable: false),
                        IdChild = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdParent, t.IdChild })
                .ForeignKey("public.XsdComplexType", t => t.IdParent)
                .ForeignKey("public.XsdComplexType", t => t.IdChild)
                .Index(t => t.IdParent)
                .Index(t => t.IdChild);
            
        }
        
        public override void Down()
        {
            DropForeignKey("public.GraphNodePosition_OntologyTerm", "IdOwnerUser", "public.User");
            DropForeignKey("public.GraphNodePosition_OntologyTerm", "IdOntologyTerm", "public.OntologyTerm");
            DropForeignKey("public.ServiceDescription_Ontology", "IdOntology", "public.Ontology");
            DropForeignKey("public.Ontology", "IdOwnerUser", "public.User");
            DropForeignKey("public.OntologyTerm", "IdOntology", "public.Ontology");
            DropForeignKey("public.Ontology_User", "IdOntology", "public.Ontology");
            DropForeignKey("public.Ontology_User", "IdSharedUser", "public.User");
            DropForeignKey("public.ShareInvitation", "IdUserInviter", "public.User");
            DropForeignKey("public.ServiceDescription", "IdOwnerUser", "public.User");
            DropForeignKey("public.ResetPassword", "IdUser", "public.User");
            DropForeignKey("public.Issue", "IdOwnerUser", "public.User");
            DropForeignKey("public.GraphNodePosition_SawsdlModelReference", "IdSawsdlModelReference", "public.SawsdlModelReference");
            DropForeignKey("public.SawsdlModelReference", "IdXsdSimpleType", "public.XsdSimpleType");
            DropForeignKey("public.SawsdlModelReference", "IdXsdComplexType", "public.XsdComplexType");
            DropForeignKey("public.SawsdlModelReference", "IdWsdlOperation", "public.WsdlOperation");
            DropForeignKey("public.SawsdlModelReference", "IdWsdlInterface", "public.WsdlInterface");
            DropForeignKey("public.SawsdlModelReference", "IdWsdlFault", "public.WsdlFault");
            DropForeignKey("public.SawsdlModelReference", "IdServiceDescription", "public.ServiceDescription");
            DropForeignKey("public.XsdDocument", "Id", "public.ServiceDescription");
            DropForeignKey("public.WsdlInterface", "IdServiceDescription", "public.ServiceDescription");
            DropForeignKey("public.TaskComment", "IdTask", "public.Task");
            DropForeignKey("public.TaskComment", "IdOwnerUser", "public.User");
            DropForeignKey("public.Task", "IdServiceDescription", "public.ServiceDescription");
            DropForeignKey("public.Task", "IdOwnerUser", "public.User");
            DropForeignKey("public.ShareInvitation", "IdServiceDescription", "public.ServiceDescription");
            DropForeignKey("public.ShareInvitation_Ontology", "IdShareInvitation", "public.ShareInvitation");
            DropForeignKey("public.ShareInvitation_Ontology", "IdOntology", "public.Ontology");
            DropForeignKey("public.ServiceDescription_User", "IdSharedUser", "public.User");
            DropForeignKey("public.ServiceDescription_User", "IdServiceDescription", "public.ServiceDescription");
            DropForeignKey("public.ServiceDescription_Ontology", "IdServiceDescription", "public.ServiceDescription");
            DropForeignKey("public.XsdSimpleType", "IdWsdlInfault", "public.WsdlInfault");
            DropForeignKey("public.XsdComplexType", "IdWsdlInfault", "public.WsdlInfault");
            DropForeignKey("public.WsdlOutput", "IdWsdlOperation", "public.WsdlOperation");
            DropForeignKey("public.WsdlOutfault", "IdWsdlOperation", "public.WsdlOperation");
            DropForeignKey("public.WsdlOperation", "IdWsdlInterface", "public.WsdlInterface");
            DropForeignKey("public.WsdlFault", "IdWsdlInterface", "public.WsdlInterface");
            DropForeignKey("public.Issue", "IdWsdlInterface", "public.WsdlInterface");
            DropForeignKey("public.GraphNodePosition_WsdlInterface", "IdWsdlInterface", "public.WsdlInterface");
            DropForeignKey("public.GraphNodePosition_WsdlInterface", "IdOwnerUser", "public.User");
            DropForeignKey("public.WsdlInput", "IdWsdlOperation", "public.WsdlOperation");
            DropForeignKey("public.XsdSimpleType", "IdWsdlInput", "public.WsdlInput");
            DropForeignKey("public.XsdComplexType", "IdWsdlInput", "public.WsdlInput");
            DropForeignKey("public.XsdSimpleType", "IdWsdlOutfault", "public.WsdlOutfault");
            DropForeignKey("public.XsdSimpleType", "IdXsdDocument", "public.XsdDocument");
            DropForeignKey("public.XsdComplexType", "IdXsdDocument", "public.XsdDocument");
            DropForeignKey("public.XsdSimpleType", "IdXsdComplexType", "public.XsdComplexType");
            DropForeignKey("public.XsdSimpleType", "IdWsdlOutput", "public.WsdlOutput");
            DropForeignKey("public.XsdComplexType", "IdWsdlOutput", "public.WsdlOutput");
            DropForeignKey("public.GraphNodePosition_WsdlOutput", "IdWsdlOutput", "public.WsdlOutput");
            DropForeignKey("public.GraphNodePosition_WsdlOutput", "IdOwnerUser", "public.User");
            DropForeignKey("public.Issue", "IdXsdSimpleType", "public.XsdSimpleType");
            DropForeignKey("public.GraphNodePosition_XsdSimpleType", "IdXsdSimpleType", "public.XsdSimpleType");
            DropForeignKey("public.GraphNodePosition_XsdSimpleType", "IdOwnerUser", "public.User");
            DropForeignKey("public.XsdComplexType", "IdWsdlOutfault", "public.WsdlOutfault");
            DropForeignKey("public.WsdlFault", "IdWsdlOutfault", "public.WsdlOutfault");
            DropForeignKey("public.GraphNodePosition_WsdlOutfault", "IdWsdlOutfault", "public.WsdlOutfault");
            DropForeignKey("public.GraphNodePosition_WsdlOutfault", "IdOwnerUser", "public.User");
            DropForeignKey("public.Issue", "IdXsdComplexType", "public.XsdComplexType");
            DropForeignKey("public.GraphNodePosition_XsdComplexType", "IdXsdComplexType", "public.XsdComplexType");
            DropForeignKey("public.GraphNodePosition_XsdComplexType", "IdOwnerUser", "public.User");
            DropForeignKey("public.XsdComplexComplex", "IdChild", "public.XsdComplexType");
            DropForeignKey("public.XsdComplexComplex", "IdParent", "public.XsdComplexType");
            DropForeignKey("public.GraphNodePosition_WsdlInput", "IdWsdlInput", "public.WsdlInput");
            DropForeignKey("public.GraphNodePosition_WsdlInput", "IdOwnerUser", "public.User");
            DropForeignKey("public.WsdlInfault", "IdWsdlOperation", "public.WsdlOperation");
            DropForeignKey("public.Issue", "IdWsdlOperation", "public.WsdlOperation");
            DropForeignKey("public.GraphNodePosition_WsdlOperation", "IdWsdlOperation", "public.WsdlOperation");
            DropForeignKey("public.GraphNodePosition_WsdlOperation", "IdOwnerUser", "public.User");
            DropForeignKey("public.WsdlFault", "IdWsdlInfault", "public.WsdlInfault");
            DropForeignKey("public.GraphNodePosition_WsdlInfault", "IdWsdlInfault", "public.WsdlInfault");
            DropForeignKey("public.GraphNodePosition_WsdlInfault", "IdOwnerUser", "public.User");
            DropForeignKey("public.Issue", "IdWsdlFault", "public.WsdlFault");
            DropForeignKey("public.GraphNodePosition_WsdlFault", "IdWsdlFault", "public.WsdlFault");
            DropForeignKey("public.GraphNodePosition_WsdlFault", "IdOwnerUser", "public.User");
            DropForeignKey("public.Issue", "IdServiceDescription", "public.ServiceDescription");
            DropForeignKey("public.IssueAnswer", "IdIssue", "public.Issue");
            DropForeignKey("public.IssueAnswer", "IdOwnerUser", "public.User");
            DropForeignKey("public.SawsdlModelReference", "IdOwnerUser", "public.User");
            DropForeignKey("public.SawsdlModelReference", "IdOntologyTerm", "public.OntologyTerm");
            DropForeignKey("public.GraphNodePosition_SawsdlModelReference", "IdOwnerUser", "public.User");
            DropForeignKey("public.OntologyTerm", "IdParentOntologyTerm", "public.OntologyTerm");
            DropIndex("public.XsdComplexComplex", new[] { "IdChild" });
            DropIndex("public.XsdComplexComplex", new[] { "IdParent" });
            DropIndex("public.ResetPassword", new[] { "IdUser" });
            DropIndex("public.TaskComment", new[] { "IdTask" });
            DropIndex("public.TaskComment", new[] { "IdOwnerUser" });
            DropIndex("public.Task", new[] { "IdServiceDescription" });
            DropIndex("public.Task", new[] { "IdOwnerUser" });
            DropIndex("public.ShareInvitation_Ontology", new[] { "IdOntology" });
            DropIndex("public.ShareInvitation_Ontology", new[] { "IdShareInvitation" });
            DropIndex("public.ShareInvitation", new[] { "IdUserInviter" });
            DropIndex("public.ShareInvitation", new[] { "IdServiceDescription" });
            DropIndex("public.ServiceDescription_User", new[] { "IdSharedUser" });
            DropIndex("public.ServiceDescription_User", new[] { "IdServiceDescription" });
            DropIndex("public.ServiceDescription_Ontology", new[] { "IdServiceDescription" });
            DropIndex("public.ServiceDescription_Ontology", new[] { "IdOntology" });
            DropIndex("public.GraphNodePosition_WsdlInterface", new[] { "IdWsdlInterface" });
            DropIndex("public.GraphNodePosition_WsdlInterface", new[] { "IdOwnerUser" });
            DropIndex("public.WsdlInterface", new[] { "IdServiceDescription" });
            DropIndex("public.XsdDocument", new[] { "Id" });
            DropIndex("public.GraphNodePosition_WsdlOutput", new[] { "IdWsdlOutput" });
            DropIndex("public.GraphNodePosition_WsdlOutput", new[] { "IdOwnerUser" });
            DropIndex("public.WsdlOutput", new[] { "IdWsdlOperation" });
            DropIndex("public.GraphNodePosition_XsdSimpleType", new[] { "IdXsdSimpleType" });
            DropIndex("public.GraphNodePosition_XsdSimpleType", new[] { "IdOwnerUser" });
            DropIndex("public.XsdSimpleType", new[] { "IdWsdlInfault" });
            DropIndex("public.XsdSimpleType", new[] { "IdWsdlInput" });
            DropIndex("public.XsdSimpleType", new[] { "IdWsdlOutfault" });
            DropIndex("public.XsdSimpleType", new[] { "IdXsdComplexType" });
            DropIndex("public.XsdSimpleType", new[] { "IdWsdlOutput" });
            DropIndex("public.XsdSimpleType", new[] { "IdXsdDocument" });
            DropIndex("public.GraphNodePosition_WsdlOutfault", new[] { "IdWsdlOutfault" });
            DropIndex("public.GraphNodePosition_WsdlOutfault", new[] { "IdOwnerUser" });
            DropIndex("public.WsdlOutfault", new[] { "IdWsdlOperation" });
            DropIndex("public.GraphNodePosition_XsdComplexType", new[] { "IdXsdComplexType" });
            DropIndex("public.GraphNodePosition_XsdComplexType", new[] { "IdOwnerUser" });
            DropIndex("public.XsdComplexType", new[] { "IdWsdlInfault" });
            DropIndex("public.XsdComplexType", new[] { "IdWsdlInput" });
            DropIndex("public.XsdComplexType", new[] { "IdWsdlOutput" });
            DropIndex("public.XsdComplexType", new[] { "IdWsdlOutfault" });
            DropIndex("public.XsdComplexType", new[] { "IdXsdDocument" });
            DropIndex("public.GraphNodePosition_WsdlInput", new[] { "IdWsdlInput" });
            DropIndex("public.GraphNodePosition_WsdlInput", new[] { "IdOwnerUser" });
            DropIndex("public.WsdlInput", new[] { "IdWsdlOperation" });
            DropIndex("public.GraphNodePosition_WsdlOperation", new[] { "IdWsdlOperation" });
            DropIndex("public.GraphNodePosition_WsdlOperation", new[] { "IdOwnerUser" });
            DropIndex("public.WsdlOperation", new[] { "IdWsdlInterface" });
            DropIndex("public.GraphNodePosition_WsdlInfault", new[] { "IdWsdlInfault" });
            DropIndex("public.GraphNodePosition_WsdlInfault", new[] { "IdOwnerUser" });
            DropIndex("public.WsdlInfault", new[] { "IdWsdlOperation" });
            DropIndex("public.GraphNodePosition_WsdlFault", new[] { "IdWsdlFault" });
            DropIndex("public.GraphNodePosition_WsdlFault", new[] { "IdOwnerUser" });
            DropIndex("public.WsdlFault", new[] { "IdWsdlOutfault" });
            DropIndex("public.WsdlFault", new[] { "IdWsdlInfault" });
            DropIndex("public.WsdlFault", new[] { "IdWsdlInterface" });
            DropIndex("public.IssueAnswer", new[] { "IdOwnerUser" });
            DropIndex("public.IssueAnswer", new[] { "IdIssue" });
            DropIndex("public.Issue", new[] { "IdXsdSimpleType" });
            DropIndex("public.Issue", new[] { "IdXsdComplexType" });
            DropIndex("public.Issue", new[] { "IdWsdlOperation" });
            DropIndex("public.Issue", new[] { "IdWsdlInterface" });
            DropIndex("public.Issue", new[] { "IdWsdlFault" });
            DropIndex("public.Issue", new[] { "IdServiceDescription" });
            DropIndex("public.Issue", new[] { "IdOwnerUser" });
            DropIndex("public.ServiceDescription", "UQ_ServiceDescription_ServiceName");
            DropIndex("public.ServiceDescription", new[] { "IdOwnerUser" });
            DropIndex("public.SawsdlModelReference", new[] { "IdXsdSimpleType" });
            DropIndex("public.SawsdlModelReference", new[] { "IdXsdComplexType" });
            DropIndex("public.SawsdlModelReference", new[] { "IdWsdlOperation" });
            DropIndex("public.SawsdlModelReference", new[] { "IdWsdlInterface" });
            DropIndex("public.SawsdlModelReference", new[] { "IdWsdlFault" });
            DropIndex("public.SawsdlModelReference", new[] { "IdServiceDescription" });
            DropIndex("public.SawsdlModelReference", new[] { "IdOwnerUser" });
            DropIndex("public.SawsdlModelReference", new[] { "IdOntologyTerm" });
            DropIndex("public.GraphNodePosition_SawsdlModelReference", new[] { "IdSawsdlModelReference" });
            DropIndex("public.GraphNodePosition_SawsdlModelReference", new[] { "IdOwnerUser" });
            DropIndex("public.User", "UQ_User_Username");
            DropIndex("public.User", "UQ_User_Email");
            DropIndex("public.Ontology_User", new[] { "IdSharedUser" });
            DropIndex("public.Ontology_User", new[] { "IdOntology" });
            DropIndex("public.Ontology", "UQ_Ontology_OntologyName");
            DropIndex("public.Ontology", new[] { "IdOwnerUser" });
            DropIndex("public.OntologyTerm", new[] { "IdParentOntologyTerm" });
            DropIndex("public.OntologyTerm", new[] { "IdOntology" });
            DropIndex("public.GraphNodePosition_OntologyTerm", new[] { "IdOntologyTerm" });
            DropIndex("public.GraphNodePosition_OntologyTerm", new[] { "IdOwnerUser" });
            DropTable("public.XsdComplexComplex");
            DropTable("public.ResetPassword");
            DropTable("public.TaskComment");
            DropTable("public.Task");
            DropTable("public.ShareInvitation_Ontology");
            DropTable("public.ShareInvitation");
            DropTable("public.ServiceDescription_User");
            DropTable("public.ServiceDescription_Ontology");
            DropTable("public.GraphNodePosition_WsdlInterface");
            DropTable("public.WsdlInterface");
            DropTable("public.XsdDocument");
            DropTable("public.GraphNodePosition_WsdlOutput");
            DropTable("public.WsdlOutput");
            DropTable("public.GraphNodePosition_XsdSimpleType");
            DropTable("public.XsdSimpleType");
            DropTable("public.GraphNodePosition_WsdlOutfault");
            DropTable("public.WsdlOutfault");
            DropTable("public.GraphNodePosition_XsdComplexType");
            DropTable("public.XsdComplexType");
            DropTable("public.GraphNodePosition_WsdlInput");
            DropTable("public.WsdlInput");
            DropTable("public.GraphNodePosition_WsdlOperation");
            DropTable("public.WsdlOperation");
            DropTable("public.GraphNodePosition_WsdlInfault");
            DropTable("public.WsdlInfault");
            DropTable("public.GraphNodePosition_WsdlFault");
            DropTable("public.WsdlFault");
            DropTable("public.IssueAnswer");
            DropTable("public.Issue");
            DropTable("public.ServiceDescription");
            DropTable("public.SawsdlModelReference");
            DropTable("public.GraphNodePosition_SawsdlModelReference");
            DropTable("public.User");
            DropTable("public.Ontology_User");
            DropTable("public.Ontology");
            DropTable("public.OntologyTerm");
            DropTable("public.GraphNodePosition_OntologyTerm");
        }
    }
}
