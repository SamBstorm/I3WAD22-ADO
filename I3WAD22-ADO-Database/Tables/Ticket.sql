CREATE TABLE [dbo].[Ticket]
(
	[idTicket] INT NOT NULL IDENTITY, 
    [dateTicket] DATETIME2 NOT NULL DEFAULT GETDATE(), 
    [idRepresentation] INT NOT NULL, 
    [idClient] INT NOT NULL, 
    [idType] INT NOT NULL, 
    CONSTRAINT [PK_Ticket] PRIMARY KEY ([idTicket]), 
    CONSTRAINT [FK_Ticket_Representation] FOREIGN KEY ([idRepresentation]) REFERENCES [Representation]([idRepresentation]), 
    CONSTRAINT [FK_Ticket_Client] FOREIGN KEY ([idClient]) REFERENCES [Client]([idClient]), 
    CONSTRAINT [FK_Ticket_Type] FOREIGN KEY ([idType]) REFERENCES [Type]([idType]) 
)
