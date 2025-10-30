/* ---------------------------------------------------------
   Baza: soft4you
   Tabela: dbo.klienci 
   Zakres:
     - PK: klient_id INT IDENTITY + PRIMARY KEY 
     - UNIQUE: nip, email
     - CHECK: nip = 10 cyfr; 
			  tel – dozwolone znaki oraz 7–15 cyfr;
              email 
     - created_at/updated_at - trigger ustawiający updated_at
     - rowver (ROWVERSION) do współbieżności
     - Indeks dla pola 'nazwa'
   --------------------------------------------------------- */

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

-- 1) Utwórz bazę, jeśli nie istnieje
IF DB_ID(N'soft4you') IS NULL
BEGIN
    CREATE DATABASE [soft4you];
END
GO

USE [soft4you];
GO

-- 3) Utwórz tabelę, jeśli nie istnieje
IF OBJECT_ID(N'dbo.klienci', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.klienci
    (
        klient_id  INT IDENTITY(1,1) NOT NULL
            CONSTRAINT PK_klienci PRIMARY KEY CLUSTERED,

        nazwa      NVARCHAR(200)  NOT NULL,
        nip        CHAR(10)       NOT NULL,        -- 10 cyfr (PL)
        adres      NVARCHAR(255)  NOT NULL,
        nr_tel     VARCHAR(20)    NOT NULL,        -- cyfry, +, spacje, -
        email      NVARCHAR(254)  NOT NULL,

        created_at DATETIME2(0)   NOT NULL CONSTRAINT DF_klienci_created_at DEFAULT SYSUTCDATETIME(),
        updated_at DATETIME2(0)   NOT NULL CONSTRAINT DF_klienci_updated_at DEFAULT SYSUTCDATETIME(),
        rowver     ROWVERSION
    );

    -- 4) Unikalności
    ALTER TABLE dbo.klienci
      ADD CONSTRAINT UQ_klienci_nip   UNIQUE (nip),
          CONSTRAINT UQ_klienci_email UNIQUE (email);

    -- 5) Walidacje
    ALTER TABLE dbo.klienci
      ADD
        -- nip: dokładnie 10 cyfr
        CONSTRAINT CK_klienci_nip_format
            CHECK (nip NOT LIKE '%[^0-9]%' AND LEN(nip) = 10),

        -- nr_tel: tylko cyfry, spacje, +, -, a po usunięciu separatorów 7–15 cyfr
        CONSTRAINT CK_klienci_nr_tel_format
            CHECK (
                nr_tel NOT LIKE '%[^0-9+ -]%' AND
                LEN(REPLACE(REPLACE(REPLACE(nr_tel,' ',''),'-',''),'+','')) BETWEEN 7 AND 15
            ),

        -- email: prosty wzorzec
        CONSTRAINT CK_klienci_email_format
            CHECK (email LIKE '%_@_%._%');
END
GO

-- 6) Trigger: aktualizacja pola 'updated_at' dla edytowanych wierszy
CREATE OR ALTER TRIGGER dbo.trg_klienci_set_updated_at
ON dbo.klienci
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.klienci
      SET updated_at = SYSUTCDATETIME()
      WHERE klient_id IN (SELECT klient_id FROM inserted);
END
GO

-- 7) Indeks po nazwie do wyszukiwań
IF NOT EXISTS (
    SELECT 1
      FROM sys.indexes
     WHERE name = N'IX_klienci_nazwa'
       AND object_id = OBJECT_ID(N'dbo.klienci')
)
BEGIN
    CREATE INDEX IX_klienci_nazwa ON dbo.klienci(nazwa);
END
GO
