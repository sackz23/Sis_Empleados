USE Sistema_Empleados

-- Crear departamento
INSERT INTO Departamento (Departamento_De_Trabajo)
VALUES ('Administración');

-- Crear cargo
INSERT INTO Cargo_Empleado (Id_Departamento, Cargo_De_Empleado)
VALUES (1, 'Administrador');

-- Crear empleado
INSERT INTO Empleados (Nombre, Id_CargoEmpleado, Fecha_Nacimiento, Telefono, Email, Fecha_Ingreso, Activo)
VALUES ('Administrador General', 1, '1990-01-01', '8888-8888', 'admin@sistema.com', GETDATE(), 1);

-- Crear rol
INSERT INTO Roles (Nombre_Rol)
VALUES ('Administrador');

-- Crear usuario
INSERT INTO Usuarios (Id_Empleado, Id_Rol, Nombre_Usuario, Contraseña)
VALUES (1, 1, 'admin', HASHBYTES('SHA2_256', 'admin123'));