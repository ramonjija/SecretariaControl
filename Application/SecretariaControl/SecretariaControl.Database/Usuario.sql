CREATE TABLE [dbo].[Usuario]
(
	[IdUsuario] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Email] VARCHAR(50) NOT NULL, 
    [Senha] VARCHAR(50) NOT NULL, 
    [Nome] VARCHAR(70) NOT NULL, 
    [Telefone] VARCHAR(20) NOT NULL, 
    [Perfil] INT NOT NULL, 
    [Foto] VARCHAR(50) NOT NULL, 
    [IdSecretaria] INT NULL
)
