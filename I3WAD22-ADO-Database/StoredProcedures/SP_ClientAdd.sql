CREATE PROCEDURE [dbo].[SP_ClientAdd]
	@email NVARCHAR(255),
	@pass NVARCHAR(32),
	@nom NVARCHAR(50),
	@prenom NVARCHAR(50),
	@adresse NVARCHAR(MAX)
AS
	INSERT INTO [Client] ([email],[pass],[nom],[prenom],[adresse])
	OUTPUT [inserted].[idClient]
	VALUES (@email, HASHBYTES('SHA2_512',@pass), @nom, @prenom, @adresse)
