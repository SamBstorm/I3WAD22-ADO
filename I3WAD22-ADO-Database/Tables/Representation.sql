CREATE TABLE [dbo].[Representation]
(
	[idRepresentation] INT IDENTITY NOT NULL,
	[dateRepresentation] DATE NOT NULL,
	[heureRepresentation] TIME NOT NULL,
	[idSpectacle] INT NOT NULL,
	CONSTRAINT PK_Representation PRIMARY KEY ([idRepresentation]), 
    CONSTRAINT [CK_Representation_date] CHECK ([dateRepresentation] > GETDATE()), 
    CONSTRAINT [FK_Representation_Spectacle] FOREIGN KEY ([idSpectacle]) REFERENCES [Spectacle]([idSpectacle])
)
