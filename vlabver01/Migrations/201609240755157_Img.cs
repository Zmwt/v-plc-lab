namespace vlabver01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Img : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PLCs", "ImgPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PLCs", "ImgPath");
        }
    }
}
