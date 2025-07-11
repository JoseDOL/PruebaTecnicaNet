USE PruebaTecnica
GO
IF OBJECT_ID('dbo.sp_empleados_update') IS NOT NULL
    DROP PROCEDURE dbo.sp_empleados_update;
GO

CREATE PROCEDURE dbo.sp_empleados_update
    @Codigo INT,
    @Puesto VARCHAR(50),
    @Nombre VARCHAR(100),
    @CodigoJefe INT = NULL
AS
BEGIN
    UPDATE Empleados
    SET Puesto = @Puesto,
        Nombre = @Nombre,
        CodigoJefe = @CodigoJefe
    WHERE Codigo = @Codigo;
END;
GO
