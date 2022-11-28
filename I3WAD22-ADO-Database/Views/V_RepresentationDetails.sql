CREATE VIEW [dbo].[V_RepresentationDetails]
	AS SELECT	[idRepresentation],
				[dateRepresentation],
				[heureRepresentation],
				S.[nom] AS [nomSpectacle]
		FROM [Representation] AS R
			JOIN [Spectacle] AS S
			ON R.[idSpectacle] = S.[idSpectacle]
