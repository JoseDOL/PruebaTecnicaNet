USE PruebaTecnica
GO

IF OBJECT_ID('dbo.sp_empleados_get_all') IS NOT NULL
    DROP PROCEDURE dbo.sp_empleados_get_all;
GO

CREATE PROCEDURE dbo.sp_empleados_get_all
AS
BEGIN
    SELECT Codigo, Puesto, Nombre, CodigoJefe, EstaBorrado
    FROM Empleados
    WHERE EstaBorrado = 0;
END;
GO
