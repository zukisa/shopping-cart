using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class GenerateOrderNo_Sp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"CREATE PROC dbo.spGenerateOrderNo
AS
BEGIN
	DECLARE @LastOrderNumber VARCHAR(50),@OrderNo VARCHAR(50)
	SET @LastOrderNumber = (SELECT TOP 1 CONVERT(VARCHAR(100), CONVERT(INT, LTRIM(RTRIM(REPLACE(UPPER(Id),'ORD','')))) + 1)
							FROM [Order]
							ORDER BY CONVERT(INT, LTRIM(RTRIM(REPLACE(Id,'ORD','')))) DESC)
	SET @OrderNo = 'ORD'
	IF @LastOrderNumber IS NULL
	BEGIN
		 SET @OrderNo = FORMATMESSAGE('%s00001', @OrderNo)
	END
	ELSE
		BEGIN
				SET @OrderNo = CASE LEN(@LastOrderNumber) WHEN 1 THEN FORMATMESSAGE('%s%s%s', @OrderNo, REPLICATE('0',4), @LastOrderNumber)
												WHEN 2 THEN FORMATMESSAGE('%s%s%s', @OrderNo, REPLICATE('0',3), @LastOrderNumber)
												WHEN 3 THEN FORMATMESSAGE('%s%s%s', @OrderNo, REPLICATE('0',2), @LastOrderNumber)
												WHEN 4 THEN FORMATMESSAGE('%s%s%s', @OrderNo, REPLICATE('0',1), @LastOrderNumber)
												ELSE FORMATMESSAGE('%s%s', @OrderNo,@LastOrderNumber) END
		END
	SELECT @OrderNo AS OrderNo
END
GO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP PROC dbo.spGenerateOrderNo");
        }
    }
}
