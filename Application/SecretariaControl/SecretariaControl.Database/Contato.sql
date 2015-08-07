CREATE TABLE [dbo].[Contato]
(
	[IdContato] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Descricao] VARCHAR(150) NOT NULL, 
    [Telefone] VARCHAR(20) NOT NULL, 
    [Email] VARCHAR(50) NOT NULL, 
    [IdGerente] INT NOT NULL, 
    CONSTRAINT [FK_IdGerente] FOREIGN KEY ([IdGerente]) REFERENCES [Usuario]([IdUsuario])
)
