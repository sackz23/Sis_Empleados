--
CREATE DATABASE Sistema_Empleados
go
--
use Sistema_Empleados
--

CREATE TABLE Departamento (
	Id_Departamento INT PRIMARY KEY IDENTITY (1,1),
	Departamento_De_Trabajo VARCHAR(50) NOT NULL,
);

CREATE TABLE Cargo_Empleado (
	Id_CargoEmpleado INT PRIMARY KEY IDENTITY (1,1),
	Id_Departamento INT NOT NULL,
	Cargo_De_Empleado VARCHAR(50) NOT NULL,
	CONSTRAINT FK_CargoEmpleado_Departamento FOREIGN KEY(Id_Departamento) REFERENCES Departamento(Id_Departamento),
);

CREATE TABLE Empleados (
	Id_Empleado INT PRIMARY KEY IDENTITY (1,1),
	Nombre varchar(50) not null,
	Id_CargoEmpleado INT NOT NULL,
	CONSTRAINT FK_Empleado_Cargo_Empleado FOREIGN KEY (Id_CargoEmpleado) REFERENCES Cargo_Empleado(Id_CargoEmpleado),
	Fecha_Nacimiento DATE NOT NULL,
	Telefono VARCHAR(50) NOT NULL,
	Email VARCHAR(50) NOT NULL, 
	Fecha_Ingreso DATE NOT NULL,
	activo BIT NOT NULL,
);

CREATE TABLE Turno (
	Id_Turno INT PRIMARY KEY IDENTITY (1,1),
	Tipo_Turno varchar(50) NOT NULL,
);

CREATE TABLE Turno_Empleado (
	Id_TurnoEmpleado INT PRIMARY KEY IDENTITY (1,1),
	Id_Turno INT NOT NULL,
	Id_Empleado INT NOT NULL,
	CONSTRAINT FK_TurnoEmpleado_Turno FOREIGN KEY (Id_Turno) REFERENCES Turno(Id_Turno),
	CONSTRAINT FK_TurnoEmpleado_Empleado FOREIGN KEY (Id_Empleado) REFERENCES Empleados(Id_Empleado),
);

CREATE TABLE Roles (
	Id_Rol INT PRIMARY KEY IDENTITY (1,1),
	Nombre_Rol VARCHAR(50) NOT NULL,
);

CREATE TABLE Permisos (
	Id_Permiso INT PRIMARY KEY IDENTITY (1,1),
	Nombre_Permiso varchar(50) NOT NULL, 
);

CREATE TABLE Rol_Permiso (
	Id_RolPermiso INT PRIMARY KEY IDENTITY (1,1),
	Id_Rol INT NOT NULL, 
	Id_Permiso INT NOT NULL, 
	CONSTRAINT FK_RolPermiso_Rol FOREIGN KEY (Id_Rol) REFERENCES Roles(Id_Rol),
	CONSTRAINT FK_RolPermiso_Permiso FOREIGN KEY (Id_Permiso) REFERENCES Permisos(Id_Permiso)
);

CREATE TABLE Usuarios (
	Id_Usuario INT PRIMARY KEY IDENTITY (1,1),
	Id_Empleado INT NOT NULL, 
	Id_Rol INT NOT NULL,
	Nombre_Usuario varchar(50) NOT NULL,
	contraseña VARBINARY(64) NOT NULL, 
	CONSTRAINT FK_UsuarioEmpleado FOREIGN KEY (Id_Empleado) REFERENCES Empleados(Id_Empleado),
	CONSTRAINT FK_UsuarioRol FOREIGN KEY (Id_Rol) REFERENCES Roles(Id_Rol),
);

CREATE TABLE Periodos (
	Id_Periodo INT PRIMARY KEY IDENTITY (1,1),
	Periodo_De_Pago varchar(50) NOT NULL, 
);

CREATE TABLE Empleado_Salario (
	Id_EmpleadoSalario INT PRIMARY KEY IDENTITY (1,1),
	Id_Empleado INT NOT NULL,
	Id_Periodo INT NOT NULL,
	salario_base INT NOT NULL,
	CONSTRAINT FK_EmpleadoSalario_Empleado FOREIGN KEY (Id_Empleado) REFERENCES Empleados(Id_Empleado),
	CONSTRAINT FK_EmpleadoSalario_Periodo FOREIGN KEY (Id_Periodo) REFERENCES Periodos(Id_Periodo),
);

CREATE TABLE Tipo_Deducciones (
	Id_TipoDeducciones INT PRIMARY KEY IDENTITY (1,1),
	Nombre_Deduccion VARCHAR(50) NOT NULL,
);

CREATE TABLE Detalle_Deduccion (
	Id_DetalleDeduccion INT PRIMARY KEY IDENTITY (1,1),
	Id_TipoDeducciones INT NOT NULL,
	Deduccion DECIMAL NOT NULL,
	CONSTRAINT FK_DetalleDeduccion_TipoDeducciones FOREIGN KEY (Id_TipoDeducciones) REFERENCES Tipo_Deducciones(Id_TipoDeducciones),
);

CREATE TABLE Deduccion (
	Id_TotalDeducciones INT PRIMARY KEY IDENTITY (1,1),
	Id_DetalleDeduccion INT NOT NULL, 
	Id_EmpleadoSalario INT NOT NULL,
	Monto_Deduccion INT NOT NULL,
	CONSTRAINT FK_Deduccion_DetalleDeduccion FOREIGN KEY (Id_DetalleDeduccion) REFERENCES Detalle_Deduccion(Id_DetalleDeduccion),
	CONSTRAINT FK_Deduccion_EmpleadoSalario FOREIGN KEY (Id_EmpleadoSalario) REFERENCES Empleado_Salario(Id_EmpleadoSalario),
);