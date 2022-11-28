CREATE PROCEDURE [dbo].[SP_ClientCheck]
	@email NVARCHAR(255),
	@pass NVARCHAR(32)
AS
	SELECT [idClient]
	FROM [Client] 
	WHERE	[email] = @email 
		AND [pass] = HASHBYTES('SHA2_512',@pass)
