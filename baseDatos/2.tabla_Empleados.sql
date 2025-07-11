USE PruebaTecnica
GO

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Empleados')
BEGIN
    CREATE TABLE Empleados (
        Codigo INT PRIMARY KEY,
        Puesto VARCHAR(100) NOT NULL,
        Nombre VARCHAR(100) NOT NULL,
        CodigoJefe INT NULL,
        CONSTRAINT FK_Empleados_Empleados FOREIGN KEY (CodigoJefe) REFERENCES Empleados(Codigo)
    );
END