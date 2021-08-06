namespace Ativ_2_Mentoria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryList : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Gasto", "Categoria_IdCategory", "dbo.Categoria");
            DropIndex("dbo.Gasto", new[] { "Categoria_IdCategory" });
            DropColumn("dbo.Gasto", "CategoriaId");
            RenameColumn(table: "dbo.Gasto", name: "Categoria_IdCategory", newName: "CategoriaId");
            AlterColumn("dbo.Gasto", "CategoriaId", c => c.Int(nullable: true));
            CreateIndex("dbo.Gasto", "CategoriaId");
            AddForeignKey("dbo.Gasto", "CategoriaId", "dbo.Categoria", "IdCategory", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Gasto", "CategoriaId", "dbo.Categoria");
            DropIndex("dbo.Gasto", new[] { "CategoriaId" });
            AlterColumn("dbo.Gasto", "CategoriaId", c => c.Int());
            RenameColumn(table: "dbo.Gasto", name: "CategoriaId", newName: "Categoria_IdCategory");
            AddColumn("dbo.Gasto", "CategoriaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Gasto", "Categoria_IdCategory");
            AddForeignKey("dbo.Gasto", "Categoria_IdCategory", "dbo.Categoria", "IdCategory");
        }
    }
}
