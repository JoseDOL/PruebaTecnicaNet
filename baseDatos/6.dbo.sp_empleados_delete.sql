USE PruebaTecnica
GO
IF OBJECT_ID('dbo.sp_empleados_delete') IS NOT NULL
    DROP PROCEDURE dbo.sp_empleados_delete;
GO

CREATE PROCEDURE dbo.sp_empleados_delete
    @Codigo INT
AS
BEGIN
    UPDATE Empleados
    SET EstaBorrado = 1
    WHERE Codigo = @Codigo;
END;
GO
