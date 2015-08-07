CREATE TABLE [dbo].[Compromisso]
(
	[IdCompromisso] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Descricao] VARCHAR(150) NOT NULL, 
    [Local] VARCHAR(50) NOT NULL, 
    [DataCompromisso] DATETIME NOT NULL, 
    [IdGerente] INT NOT NULL, 
    CONSTRAINT [FK_IdGerente_Compromisso] FOREIGN KEY ([IdGerente]) REFERENCES [Usuario]([IdUsuario])
)
