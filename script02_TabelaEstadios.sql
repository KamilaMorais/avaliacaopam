BEGIN TRANSACTION;
CREATE TABLE [TB_ESTADIOS] (
    [Id] int NOT NULL IDENTITY,
    [Nome] varchar(200) NULL,
    [Cidade] varchar(200) NULL,
    [Capacidade] int NOT NULL,
    CONSTRAINT [PK_TB_ESTADIOS] PRIMARY KEY ([Id])
);

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Capacidade', N'Cidade', N'Nome') AND [object_id] = OBJECT_ID(N'[TB_ESTADIOS]'))
    SET IDENTITY_INSERT [TB_ESTADIOS] ON;
INSERT INTO [TB_ESTADIOS] ([Id], [Capacidade], [Cidade], [Nome])
VALUES (1, 78838, 'Rio de Janeiro', 'Maracanã'),
(2, 66795, 'São Paulo', 'Morumbi'),
(3, 43713, 'São Paulo', 'Allianz Parque'),
(4, 49205, 'São Paulo', 'Arena Corinthians'),
(5, 61927, 'Belo Horizonte', 'Mineirão'),
(6, 55662, 'Porto Alegre', 'Arena do Grêmio'),
(7, 50842, 'Porto Alegre', 'Beira-Rio');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Capacidade', N'Cidade', N'Nome') AND [object_id] = OBJECT_ID(N'[TB_ESTADIOS]'))
    SET IDENTITY_INSERT [TB_ESTADIOS] OFF;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260406111139_MigracaoEstadios', N'10.0.5');

COMMIT;
GO

