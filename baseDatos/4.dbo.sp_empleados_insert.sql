USE PruebaTecnica
GO

IF OBJECT_ID('dbo.sp_empleados_insert') IS NOT NULL
    DROP PROCEDURE dbo.sp_empleados_insert;
GO

CREATE PROCEDURE dbo.sp_empleados_insert
    @Codigo INT,
    @Puesto VARCHAR(50),
    @Nombre VARCHAR(100),
    @CodigoJefe INT = NULL
AS
BEGIN
    INSERT INTO Empleados (Codigo, Puesto, Nombre, CodigoJefe, EstaBorrado)
    VALUES (@Codigo, @Puesto, @Nombre, @CodigoJefe, 0);
END;
GO
