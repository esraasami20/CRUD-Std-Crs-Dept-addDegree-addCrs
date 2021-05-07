namespace MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deptcrs : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "Department_DeptId", "dbo.Departments");
            DropIndex("dbo.Courses", new[] { "Department_DeptId" });
            DropColumn("dbo.Courses", "Department_DeptId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "Department_DeptId", c => c.Int());
            CreateIndex("dbo.Courses", "Department_DeptId");
            AddForeignKey("dbo.Courses", "Department_DeptId", "dbo.Departments", "DeptId");
        }
    }
}
