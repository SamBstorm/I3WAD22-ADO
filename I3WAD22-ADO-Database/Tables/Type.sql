CREATE TABLE [dbo].[Type]
(
	[idType] INT NOT NULL IDENTITY,
	[nom] NVARCHAR(50) NOT NULL,
	[prix] MONEY NOT NULL,
	CONSTRAINT [PK_Type] PRIMARY KEY ([idType]), 
    CONSTRAINT [CK_Type_prix] CHECK ([prix] >= 0)
)

GO

CREATE UNIQUE INDEX [UK_Type_nom] ON [dbo].[Type] ([nom])
