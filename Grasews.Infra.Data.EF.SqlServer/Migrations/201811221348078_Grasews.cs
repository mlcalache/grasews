namespace Grasews.Infra.Data.EF.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Grasews : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IssueTaskAnswer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Answer = c.String(nullable: false, unicode: false, storeType: "text"),
                        IdIssueTask = c.Int(nullable: false),
                        IdOwnerUser = c.Int(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IssueTask", t => t.IdIssueTask)
                .ForeignKey("dbo.User", t => t.IdOwnerUser, cascadeDelete: true)
                .Index(t => t.IdIssueTask)
                .Index(t => t.IdOwnerUser);
            
            CreateTable(
                "dbo.IssueTask",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOwnerUser = c.Int(nullable: false),
                        IdServiceDescription = c.Int(nullable: false),
                        Issue = c.String(nullable: false, unicode: false, storeType: "text"),
                        Solved = c.Boolean(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.IdOwnerUser)
                .ForeignKey("dbo.ServiceDescription", t => t.IdServiceDescription, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdServiceDescription);
            
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
                "dbo.OntologyTerm",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOntology = c.Int(),
                        IdParentOntologyTerm = c.Int(),
                        TermName = c.String(nullable: false, maxLength: 400),
                        TermRaw = c.String(nullable: false, unicode: false, storeType: "text"),
                        TermURI = c.String(nullable: false, maxLength: 500),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OntologyTerm", t => t.IdParentOntologyTerm)
                .ForeignKey("dbo.Ontology", t => t.IdOntology)
                .Index(t => t.IdOntology)
                .Index(t => t.IdParentOntologyTerm)
                .Index(t => t.TermName, unique: true, name: "UQ_OntologyTerm_TermName");
            
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
                "dbo.ServiceDescription",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CytoscapeGraphJson = c.String(unicode: false, storeType: "text"),
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
                "dbo.ServiceDescription_User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CytoscapeGraphJson = c.String(),
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
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServiceDescription", t => t.IdServiceDescription)
                .ForeignKey("dbo.User", t => t.IdUserInviter)
                .Index(t => t.IdServiceDescription)
                .Index(t => t.IdUserInviter);
            
            CreateTable(
                "dbo.TodoTask",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Done = c.Boolean(nullable: false),
                        IdOwnerUser = c.Int(nullable: false),
                        IdServiceDescription = c.Int(nullable: false),
                        Todo = c.String(nullable: false, maxLength: 1000),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.IdOwnerUser)
                .ForeignKey("dbo.ServiceDescription", t => t.IdServiceDescription, cascadeDelete: true)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdServiceDescription);
            
            CreateTable(
                "dbo.TodoTaskComment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(nullable: false, unicode: false, storeType: "text"),
                        IdOwnerUser = c.Int(nullable: false),
                        IdTodoTask = c.Int(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.IdOwnerUser, cascadeDelete: true)
                .ForeignKey("dbo.TodoTask", t => t.IdTodoTask)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdTodoTask);
            
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
                "dbo.SawsdlModelReference",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOwnerUser = c.Int(nullable: false),
                        IdWsdlInFault = c.Int(),
                        IdWsdlInterface = c.Int(),
                        IdWsdlOperation = c.Int(),
                        IdWsdlOutFault = c.Int(),
                        IdXsdComplexElement = c.Int(),
                        IdXsdSimpleElement = c.Int(),
                        ModelReference = c.String(nullable: false, maxLength: 400),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WsdlInFault", t => t.IdWsdlInFault)
                .ForeignKey("dbo.WsdlOperation", t => t.IdWsdlOperation)
                .ForeignKey("dbo.XsdComplexElement", t => t.IdXsdComplexElement)
                .ForeignKey("dbo.WsdlOutFault", t => t.IdWsdlOutFault)
                .ForeignKey("dbo.XsdSimpleElement", t => t.IdXsdSimpleElement)
                .ForeignKey("dbo.WsdlInterface", t => t.IdWsdlInterface)
                .ForeignKey("dbo.User", t => t.IdOwnerUser)
                .Index(t => t.IdOwnerUser)
                .Index(t => t.IdWsdlInFault)
                .Index(t => t.IdWsdlInterface)
                .Index(t => t.IdWsdlOperation)
                .Index(t => t.IdWsdlOutFault)
                .Index(t => t.IdXsdComplexElement)
                .Index(t => t.IdXsdSimpleElement);
            
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
                "dbo.XsdComplexElement",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdXsdDocument = c.Int(nullable: false),
                        XsdComplexElementName = c.String(),
                        RegistrationDateTime = c.DateTime(nullable: false),
                        IdWsdlOutFault = c.Int(),
                        IdWsdlOutput = c.Int(),
                        IdXsdToWsdlRelationType = c.Int(nullable: false),
                        IdWsdlInput = c.Int(),
                        IdWsdlInFault = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WsdlOutFault", t => t.IdWsdlOutFault)
                .ForeignKey("dbo.WsdlOutput", t => t.IdWsdlOutput)
                .ForeignKey("dbo.XsdDocument", t => t.IdXsdDocument)
                .ForeignKey("dbo.XsdToWsdlRelationType", t => t.IdXsdToWsdlRelationType)
                .ForeignKey("dbo.WsdlInput", t => t.IdWsdlInput)
                .ForeignKey("dbo.WsdlInFault", t => t.IdWsdlInFault)
                .Index(t => t.IdXsdDocument)
                .Index(t => t.IdWsdlOutFault)
                .Index(t => t.IdWsdlOutput)
                .Index(t => t.IdXsdToWsdlRelationType)
                .Index(t => t.IdWsdlInput)
                .Index(t => t.IdWsdlInFault);
            
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
                "dbo.XsdSimpleElement",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        XsdSimpleElementName = c.String(),
                        IdXsdDocument = c.Int(nullable: false),
                        LiftingSchemaMapping = c.String(),
                        LoweringSchemaMapping = c.String(),
                        RegistrationDateTime = c.DateTime(nullable: false),
                        IdWsdlOutput = c.Int(),
                        IdXsdToWsdlRelationType = c.Int(nullable: false),
                        IdWsdlOutFault = c.Int(),
                        IdXsdComplexElement = c.Int(),
                        IdWsdlInput = c.Int(),
                        IdWsdlInFault = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WsdlOutput", t => t.IdWsdlOutput)
                .ForeignKey("dbo.XsdDocument", t => t.IdXsdDocument)
                .ForeignKey("dbo.XsdToWsdlRelationType", t => t.IdXsdToWsdlRelationType)
                .ForeignKey("dbo.WsdlOutFault", t => t.IdWsdlOutFault)
                .ForeignKey("dbo.XsdComplexElement", t => t.IdXsdComplexElement)
                .ForeignKey("dbo.WsdlInput", t => t.IdWsdlInput)
                .ForeignKey("dbo.WsdlInFault", t => t.IdWsdlInFault)
                .Index(t => t.IdXsdDocument)
                .Index(t => t.IdWsdlOutput)
                .Index(t => t.IdXsdToWsdlRelationType)
                .Index(t => t.IdWsdlOutFault)
                .Index(t => t.IdXsdComplexElement)
                .Index(t => t.IdWsdlInput)
                .Index(t => t.IdWsdlInFault);
            
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
                "dbo.XsdToWsdlRelationType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Name = c.String(),
                        RegistrationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IssueTaskAnswer", "IdOwnerUser", "dbo.User");
            DropForeignKey("dbo.IssueTask", "IdServiceDescription", "dbo.ServiceDescription");
            DropForeignKey("dbo.ShareInvitation", "IdUserInviter", "dbo.User");
            DropForeignKey("dbo.ServiceDescription", "IdOwnerUser", "dbo.User");
            DropForeignKey("dbo.SawsdlModelReference", "IdOwnerUser", "dbo.User");
            DropForeignKey("dbo.ServiceDescription_Ontology", "IdOntology", "dbo.Ontology");
            DropForeignKey("dbo.ServiceDescription_Ontology", "IdServiceDescription", "dbo.ServiceDescription");
            DropForeignKey("dbo.XsdDocument", "Id", "dbo.ServiceDescription");
            DropForeignKey("dbo.WsdlInterface", "IdServiceDescription", "dbo.ServiceDescription");
            DropForeignKey("dbo.WsdlOperation", "IdWsdlInterface", "dbo.WsdlInterface");
            DropForeignKey("dbo.SawsdlModelReference", "IdWsdlInterface", "dbo.WsdlInterface");
            DropForeignKey("dbo.XsdSimpleElement", "IdWsdlInFault", "dbo.WsdlInFault");
            DropForeignKey("dbo.XsdComplexElement", "IdWsdlInFault", "dbo.WsdlInFault");
            DropForeignKey("dbo.WsdlOutput", "IdWsdlOperation", "dbo.WsdlOperation");
            DropForeignKey("dbo.WsdlOutFault", "IdWsdlOperation", "dbo.WsdlOperation");
            DropForeignKey("dbo.WsdlInput", "IdWsdlOperation", "dbo.WsdlOperation");
            DropForeignKey("dbo.XsdSimpleElement", "IdWsdlInput", "dbo.WsdlInput");
            DropForeignKey("dbo.XsdComplexElement", "IdWsdlInput", "dbo.WsdlInput");
            DropForeignKey("dbo.XsdSimpleElement", "IdXsdComplexElement", "dbo.XsdComplexElement");
            DropForeignKey("dbo.XsdSimpleElement", "IdWsdlOutFault", "dbo.WsdlOutFault");
            DropForeignKey("dbo.XsdSimpleElement", "IdXsdToWsdlRelationType", "dbo.XsdToWsdlRelationType");
            DropForeignKey("dbo.XsdComplexElement", "IdXsdToWsdlRelationType", "dbo.XsdToWsdlRelationType");
            DropForeignKey("dbo.XsdSimpleElement", "IdXsdDocument", "dbo.XsdDocument");
            DropForeignKey("dbo.XsdComplexElement", "IdXsdDocument", "dbo.XsdDocument");
            DropForeignKey("dbo.XsdSimpleElement", "IdWsdlOutput", "dbo.WsdlOutput");
            DropForeignKey("dbo.XsdComplexElement", "IdWsdlOutput", "dbo.WsdlOutput");
            DropForeignKey("dbo.SawsdlModelReference", "IdXsdSimpleElement", "dbo.XsdSimpleElement");
            DropForeignKey("dbo.XsdComplexElement", "IdWsdlOutFault", "dbo.WsdlOutFault");
            DropForeignKey("dbo.SawsdlModelReference", "IdWsdlOutFault", "dbo.WsdlOutFault");
            DropForeignKey("dbo.SawsdlModelReference", "IdXsdComplexElement", "dbo.XsdComplexElement");
            DropForeignKey("dbo.WsdlInFault", "IdWsdlOperation", "dbo.WsdlOperation");
            DropForeignKey("dbo.SawsdlModelReference", "IdWsdlOperation", "dbo.WsdlOperation");
            DropForeignKey("dbo.SawsdlModelReference", "IdWsdlInFault", "dbo.WsdlInFault");
            DropForeignKey("dbo.TodoTaskComment", "IdTodoTask", "dbo.TodoTask");
            DropForeignKey("dbo.TodoTaskComment", "IdOwnerUser", "dbo.User");
            DropForeignKey("dbo.TodoTask", "IdServiceDescription", "dbo.ServiceDescription");
            DropForeignKey("dbo.TodoTask", "IdOwnerUser", "dbo.User");
            DropForeignKey("dbo.ShareInvitation", "IdServiceDescription", "dbo.ServiceDescription");
            DropForeignKey("dbo.ServiceDescription_User", "IdSharedUser", "dbo.User");
            DropForeignKey("dbo.ServiceDescription_User", "IdServiceDescription", "dbo.ServiceDescription");
            DropForeignKey("dbo.Ontology", "IdOwnerUser", "dbo.User");
            DropForeignKey("dbo.OntologyTerm", "IdOntology", "dbo.Ontology");
            DropForeignKey("dbo.OntologyTerm", "IdParentOntologyTerm", "dbo.OntologyTerm");
            DropForeignKey("dbo.IssueTask", "IdOwnerUser", "dbo.User");
            DropForeignKey("dbo.IssueTaskAnswer", "IdIssueTask", "dbo.IssueTask");
            DropIndex("dbo.XsdDocument", new[] { "Id" });
            DropIndex("dbo.WsdlOutput", new[] { "IdWsdlOperation" });
            DropIndex("dbo.XsdSimpleElement", new[] { "IdWsdlInFault" });
            DropIndex("dbo.XsdSimpleElement", new[] { "IdWsdlInput" });
            DropIndex("dbo.XsdSimpleElement", new[] { "IdXsdComplexElement" });
            DropIndex("dbo.XsdSimpleElement", new[] { "IdWsdlOutFault" });
            DropIndex("dbo.XsdSimpleElement", new[] { "IdXsdToWsdlRelationType" });
            DropIndex("dbo.XsdSimpleElement", new[] { "IdWsdlOutput" });
            DropIndex("dbo.XsdSimpleElement", new[] { "IdXsdDocument" });
            DropIndex("dbo.WsdlOutFault", new[] { "IdWsdlOperation" });
            DropIndex("dbo.XsdComplexElement", new[] { "IdWsdlInFault" });
            DropIndex("dbo.XsdComplexElement", new[] { "IdWsdlInput" });
            DropIndex("dbo.XsdComplexElement", new[] { "IdXsdToWsdlRelationType" });
            DropIndex("dbo.XsdComplexElement", new[] { "IdWsdlOutput" });
            DropIndex("dbo.XsdComplexElement", new[] { "IdWsdlOutFault" });
            DropIndex("dbo.XsdComplexElement", new[] { "IdXsdDocument" });
            DropIndex("dbo.WsdlInput", new[] { "IdWsdlOperation" });
            DropIndex("dbo.WsdlOperation", new[] { "IdWsdlInterface" });
            DropIndex("dbo.WsdlInFault", new[] { "IdWsdlOperation" });
            DropIndex("dbo.SawsdlModelReference", new[] { "IdXsdSimpleElement" });
            DropIndex("dbo.SawsdlModelReference", new[] { "IdXsdComplexElement" });
            DropIndex("dbo.SawsdlModelReference", new[] { "IdWsdlOutFault" });
            DropIndex("dbo.SawsdlModelReference", new[] { "IdWsdlOperation" });
            DropIndex("dbo.SawsdlModelReference", new[] { "IdWsdlInterface" });
            DropIndex("dbo.SawsdlModelReference", new[] { "IdWsdlInFault" });
            DropIndex("dbo.SawsdlModelReference", new[] { "IdOwnerUser" });
            DropIndex("dbo.WsdlInterface", new[] { "IdServiceDescription" });
            DropIndex("dbo.TodoTaskComment", new[] { "IdTodoTask" });
            DropIndex("dbo.TodoTaskComment", new[] { "IdOwnerUser" });
            DropIndex("dbo.TodoTask", new[] { "IdServiceDescription" });
            DropIndex("dbo.TodoTask", new[] { "IdOwnerUser" });
            DropIndex("dbo.ShareInvitation", new[] { "IdUserInviter" });
            DropIndex("dbo.ShareInvitation", new[] { "IdServiceDescription" });
            DropIndex("dbo.ServiceDescription_User", new[] { "IdSharedUser" });
            DropIndex("dbo.ServiceDescription_User", new[] { "IdServiceDescription" });
            DropIndex("dbo.ServiceDescription", "UQ_ServiceDescription_ServiceName");
            DropIndex("dbo.ServiceDescription", new[] { "IdOwnerUser" });
            DropIndex("dbo.ServiceDescription_Ontology", new[] { "IdServiceDescription" });
            DropIndex("dbo.ServiceDescription_Ontology", new[] { "IdOntology" });
            DropIndex("dbo.OntologyTerm", "UQ_OntologyTerm_TermName");
            DropIndex("dbo.OntologyTerm", new[] { "IdParentOntologyTerm" });
            DropIndex("dbo.OntologyTerm", new[] { "IdOntology" });
            DropIndex("dbo.Ontology", "UQ_Ontology_OntologyName");
            DropIndex("dbo.Ontology", new[] { "IdOwnerUser" });
            DropIndex("dbo.User", "UQ_User_Username");
            DropIndex("dbo.User", "UQ_User_Email");
            DropIndex("dbo.IssueTask", new[] { "IdServiceDescription" });
            DropIndex("dbo.IssueTask", new[] { "IdOwnerUser" });
            DropIndex("dbo.IssueTaskAnswer", new[] { "IdOwnerUser" });
            DropIndex("dbo.IssueTaskAnswer", new[] { "IdIssueTask" });
            DropTable("dbo.XsdToWsdlRelationType");
            DropTable("dbo.XsdDocument");
            DropTable("dbo.WsdlOutput");
            DropTable("dbo.XsdSimpleElement");
            DropTable("dbo.WsdlOutFault");
            DropTable("dbo.XsdComplexElement");
            DropTable("dbo.WsdlInput");
            DropTable("dbo.WsdlOperation");
            DropTable("dbo.WsdlInFault");
            DropTable("dbo.SawsdlModelReference");
            DropTable("dbo.WsdlInterface");
            DropTable("dbo.TodoTaskComment");
            DropTable("dbo.TodoTask");
            DropTable("dbo.ShareInvitation");
            DropTable("dbo.ServiceDescription_User");
            DropTable("dbo.ServiceDescription");
            DropTable("dbo.ServiceDescription_Ontology");
            DropTable("dbo.OntologyTerm");
            DropTable("dbo.Ontology");
            DropTable("dbo.User");
            DropTable("dbo.IssueTask");
            DropTable("dbo.IssueTaskAnswer");
        }
    }
}
