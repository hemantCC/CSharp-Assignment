namespace Assignment.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveEmailConfirmFromUserProfile : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserProfiles", "EmailConfirmed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfiles", "EmailConfirmed", c => c.Boolean());
        }
    }
}
