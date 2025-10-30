USE soft4you;
GO

IF OBJECT_ID('dbo.app_log','U') IS NULL
BEGIN
    CREATE TABLE dbo.app_log
    (
        log_id     BIGINT IDENTITY(1,1) PRIMARY KEY,
        message    NVARCHAR(4000) NOT NULL,   -- krótki opis błędu
        details    NVARCHAR(MAX)  NULL,       
        created_at DATETIME2(0)   NOT NULL CONSTRAINT DF_app_log_created_at DEFAULT SYSUTCDATETIME()
    );
END
GO
