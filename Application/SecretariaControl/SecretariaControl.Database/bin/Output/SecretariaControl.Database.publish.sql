﻿/*
Deployment script for SecretariaControl.Database

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "SecretariaControl.Database"
:setvar DefaultFilePrefix "SecretariaControl.Database"
:setvar DefaultDataPath "c:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "c:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO

/* LIMPANDO BASE CASO O BANCO JÁ EXISTA
**************************************************************************/
delete from ContatoCompromisso;
delete from Contato;
delete from Compromisso;
delete from Usuario;


/* RESETANDO O IDENTITY DAS TABELAS
**************************************************************************/
DBCC CHECKIDENT (Usuario, RESEED, 0);
DBCC CHECKIDENT (Contato, RESEED, 0);
DBCC CHECKIDENT (Compromisso, RESEED, 0);
DBCC CHECKIDENT (ContatoCompromisso, RESEED, 0);




/* SECRETARIAS
**************************************************************************/
INSERT INTO Usuario (Email, Senha, Nome, Telefone, Perfil, Foto, IdSecretaria)
VALUES ('sofia@mail', 'sofia', 'Sofia Acey', '21999998888', 2, '/Images/usuarios/Sofia Acey.jpg', null),
	   ('maria@mail', 'maria', 'Maria Hill', '21999998888', 2, '/Images/usuarios/Maria Hill.jpg', null),
	   ('pepper@mail', 'pepper', 'Pepper Potts', '21999998888', 2, '/Images/usuarios/Pepper Potts.jpg', null),
	   ('peggy@mail', 'peggy', 'Peggy Carter', '21999998888', 2, '/Images/usuarios/Peggy Carter.jpg', null),
	   ('sharon@mail', 'sharon', 'Sharon Carter', '21999998888', 2, '/Images/usuarios/Sharon Carter.jpg', null);

/* GERENTES
**************************************************************************/
INSERT INTO Usuario (Email, Senha, Nome, Telefone, Perfil, Foto, IdSecretaria)
VALUES ('mark@mail', 'mark', 'Mark Booth', '21991223344', 1, '/Images/usuarios/Mark Booth.jpg', (select IdUsuario from Usuario where Email = 'sofia@mail')),
	   ('joseph@mail', 'joseph', 'Joseph Barish', '21992334455', 1, '/Images/usuarios/Joseph Barish.jpg', (select IdUsuario from Usuario where Email = 'sofia@mail')),
	   ('jamie@mail', 'jamie', 'Jamie Biddy', '21993445566', 1, '/Images/usuarios/Jamie Biddy.jpg', (select IdUsuario from Usuario where Email = 'sofia@mail')),
	   ('nick@mail', 'nick', 'Nick Fury', '21993445566', 1, '/Images/usuarios/Nick Fury.jpg', (select IdUsuario from Usuario where Email = 'maria@mail')),
	   ('tony@mail', 'tony', 'Tony Stark', '21991223344', 1, '/Images/usuarios/Tony Stark.jpg', (select IdUsuario from Usuario where Email = 'pepper@mail')),
	   ('steve@mail', 'steve', 'Steve Rogers', '21991223344', 1, '/Images/usuarios/Steve Rogers.jpg', null);
	   
/* USUÁRIO DO ADMINISTRADOR
**************************************************************************/
INSERT INTO Usuario (Email, Senha, Nome, Telefone, Perfil, Foto, IdSecretaria)
VALUES ('admin@mail', 'admin', 'Admin', '21900008000', 0, '/Images/usuarios/admin.jpg', null);


/* CONTATOS
**************************************************************************/
INSERT INTO Contato (Descricao, Telefone, Email, IdGerente)
VALUES ('Tony Stark', '21993445566', 'i.am.ironman@mail.com', (select IdUsuario from Usuario where Email = 'nick@mail')),
	   ('Steve Rogers', '21993445566', 'cap.america@mail.com', (select IdUsuario from Usuario where Email = 'nick@mail')),
	   ('Thor Odinson', '21993445566', 'thor@mail.com', (select IdUsuario from Usuario where Email = 'nick@mail')),
	   ('Natasha Romanoff', '21993445566', 'viuva.negra@mail.com', (select IdUsuario from Usuario where Email = 'nick@mail')),
	   ('Bruce Banner', '21993445566', 'hulk.smash@mail.com', (select IdUsuario from Usuario where Email = 'nick@mail')),
	   ('Petter Parker', '21993445566', 'spidey@mail.com', (select IdUsuario from Usuario where Email = 'nick@mail')),
	   ('Bucky Barnes', '21993445566', 'winter.soldier@mail.com', (select IdUsuario from Usuario where Email = 'steve@mail')),
	   ('Sam Wilson', '21993445566', 'falcao@mail.com', (select IdUsuario from Usuario where Email = 'steve@mail')),
	   ('James Rhodes', '21993445566', 'war.machine@mail.com', (select IdUsuario from Usuario where Email = 'tony@mail'));
			
/* COMPROMISSOS
**************************************************************************/
INSERT INTO Compromisso (Descricao, Local, DataCompromisso, IdGerente)
VALUES ('Vencer o Ultron (No strings on meeeee!)', 'Ilha flutuante ._. ', '2015-05-01', (select IdUsuario from Usuario where Email = 'nick@mail'));
INSERT INTO ContatoCompromisso (IdCompromisso, IdContato)
VALUES (1, 1), (1, 2), (1, 3), (1, 4), (1, 5);
GO

GO
PRINT N'Update complete.';


GO
