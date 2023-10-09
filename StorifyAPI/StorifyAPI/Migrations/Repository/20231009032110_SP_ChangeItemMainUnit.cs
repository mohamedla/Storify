using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorifyAPI.Migrations.Repository
{
    /// <inheritdoc />
    public partial class SP_ChangeItemMainUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@" Create or ALTER PROCEDURE [dbo].[SP_ChangeItemMainUnit]
                    @ItemId uniqueidentifier ,
                    @NewUnitId uniqueidentifier = null,
                    @Parameter int -- 1 change main item, 2 change main item unit price
                AS
                BEGIN

                    --Validation
                    IF @Parameter = 1 and @NewUnitId = null
                    BEGIN
                        RAISERROR('No Unit ID Specified', 16, 1);
                        RETURN
                    END

                    IF @Parameter = 1
                    BEGIN 
                        update MaterialItemUnit set IsMain = 0 WHERE MItemID = @ItemId and isMain = 1

                        update MaterialItemUnit set IsMain = 1 WHERE MItemID = @ItemId and MUnitId = @NewUnitId 
                    END

                    DECLARE @OldUnitId uniqueidentifier, 
                    @NewUnitPrice money = (select UnitPrice from MaterialItemUnit WHERE MItemID = @ItemId and isMain = 1)
    
                    DECLARE db_cursor CURSOR FOR
                    SELECT MUnitId
                    FROM dbo.MaterialItemUnit
    
                    OPEN db_cursor
                    FETCH NEXT FROM db_cursor INTO @OldUnitId
    
                    WHILE @@FETCH_STATUS = 0
                    BEGIN
                        update MaterialItemUnit set UnitPrice = @NewUnitPrice * CFactor WHERE MItemID = @ItemId and MUnitId = @OldUnitId 
                        FETCH NEXT FROM db_cursor INTO @OldUnitId
                    END
    
                    CLOSE db_cursor
                    DEALLOCATE db_cursor
                END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE [dbo].[SP_ChangeItemMainUnit]");
        }
    }
}
