CREATE TABLE [dbo].[ContatoCompromisso]
(
	[IdContatoCompromisso] INT NOT NULL PRIMARY KEY IDENTITY, 
    [IdContato] INT NOT NULL, 
    [IdCompromisso] INT NOT NULL, 
    CONSTRAINT [FK_IdContato_ContatoCompromisso] FOREIGN KEY ([IdContato]) REFERENCES [Contato]([IdContato]), 
    CONSTRAINT [FK_IdCompromisso_ContatoCompromisso] FOREIGN KEY (IdCompromisso) REFERENCES [Compromisso]([IdCompromisso])
)
