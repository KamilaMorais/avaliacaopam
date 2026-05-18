-- TABELA SELECAO
-- =========================
CREATE TABLE TB_SELECOES (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(100) NOT NULL,
    Pais VARCHAR(100) NOT NULL
);

-- =========================================
-- INSERT DAS SELEÇÕES DA COPA
-- =========================================

INSERT INTO TB_SELECOES (Nome, Pais)
VALUES
('Brasil', 'Brasil'),
('Argentina', 'Argentina'),
('França', 'França'),
('Alemanha', 'Alemanha'),
('Espanha', 'Espanha'),
('Portugal', 'Portugal'),
('Inglaterra', 'Inglaterra'),
('Itália', 'Itália'),
('Holanda', 'Holanda'),
('Bélgica', 'Bélgica'),
('Uruguai', 'Uruguai'),
('Croácia', 'Croácia'),
('México', 'México'),
('Estados Unidos', 'Estados Unidos'),
('Canadá', 'Canadá'),
('Japão', 'Japão'),
('Coreia do Sul', 'Coreia do Sul'),
('Marrocos', 'Marrocos'),
('Senegal', 'Senegal'),
('Camarões', 'Camarões'),
('Sérvia', 'Sérvia'),
('Suíça', 'Suíça'),
('Dinamarca', 'Dinamarca'),
('Polônia', 'Polônia'),
('Austrália', 'Austrália'),
('Irã', 'Irã'),
('Arábia Saudita', 'Arábia Saudita'),
('Tunísia', 'Tunísia'),
('Costa Rica', 'Costa Rica'),
('Equador', 'Equador'),
('Gana', 'Gana'),
('País de Gales', 'País de Gales');

GO
-- =========================================
-- ATUALIZAR JOGADORES JÁ EXISTENTES
-- PARA A SELEÇÃO BRASILEIRA
-- =========================================
UPDATE TB_JOGADORES
SET SelecaoId = (
    SELECT Id
    FROM TB_SELECOES
    WHERE Nome = 'Brasil'
);


go
-- =========================
-- TABELA TECNICO (1:1)
-- =========================
CREATE TABLE TB_TECNICOS (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(100) NOT NULL,
    SelecaoId INT UNIQUE, 
    CONSTRAINT FK_TB_TECNICOS_TB_SELECOES 
        FOREIGN KEY (SelecaoId) REFERENCES TB_SELECOES(Id)
);

-- =========================
-- UPDATE TABELA JOGADOR (1:N)
-- =========================
ALTER TABLE TB_JOGADORES 
    ADD CONSTRAINT FK_TB_JOGADORES_TB_SELECOES 
        FOREIGN KEY (SelecaoId) REFERENCES TB_SELECOES(Id)


-- =========================
-- TABELA JOGO
-- =========================
CREATE TABLE TB_JOGOS (
    Id INT PRIMARY KEY IDENTITY(1,1),
    DataHora DATETIME NOT NULL,
    EstadioId INT NOT NULL,
    CONSTRAINT FK_TB_JOGOS_TB_ESTADIOS
        FOREIGN KEY (Id) REFERENCES TB_ESTADIOS(Id)
);

-- =========================
-- TABELA N:N JOGO-SELECAO
-- =========================
CREATE TABLE TB_JOGOS_SELECOES (
    JogoId INT NOT NULL,
    SelecaoId INT NOT NULL,
    Gols INT DEFAULT 0,
    GolsDecisaoPenaltis INT DEFAULT 0,
    PRIMARY KEY (JogoId, SelecaoId),
    CONSTRAINT FK_TB_JOGOS_SELECOES_TB_JOGOS 
        FOREIGN KEY (JogoId) REFERENCES TB_JOGOS(Id),
    CONSTRAINT FK_TB_JOGOS_SELECOES_TB_SELECOES 
        FOREIGN KEY (SelecaoId) REFERENCES TB_SELECOES(Id)
);


