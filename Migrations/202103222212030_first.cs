namespace MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CrsId = c.Int(nullable: false, identity: true),
                        CrsName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CrsId);
            
            CreateTable(
                "dbo.studentcrs",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CrsId = c.Int(nullable: false),
                        Degree = c.Int(),
                    })
                .PrimaryKey(t => new { t.Id, t.CrsId })
                .ForeignKey("dbo.Courses", t => t.CrsId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.CrsId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Age = c.Int(nullable: false),
                        Email = c.String(),
                        password = c.Int(nullable: false),
                        studentimg = c.String(),
                        DeptId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DeptId, cascadeDelete: true)
                .Index(t => t.DeptId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DeptId = c.Int(nullable: false, identity: true),
                        DeptName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.DeptId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.studentcrs", "Id", "dbo.Students");
            DropForeignKey("dbo.Students", "DeptId", "dbo.Departments");
            DropForeignKey("dbo.studentcrs", "CrsId", "dbo.Courses");
            DropIndex("dbo.Students", new[] { "DeptId" });
            DropIndex("dbo.studentcrs", new[] { "CrsId" });
            DropIndex("dbo.studentcrs", new[] { "Id" });
            DropTable("dbo.Departments");
            DropTable("dbo.Students");
            DropTable("dbo.studentcrs");
            DropTable("dbo.Courses");
        }
    }
}
