-- Crear la base de datos
CREATE DATABASE TiendaVirtual_ULSA;
GO

-- Usar la base de datos
USE TiendaVirtual_ULSA;
GO

-- Tabla: Categorias
CREATE TABLE Categorias (
    id_categoria INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(100) NOT NULL,
    descripcion TEXT
);
GO

-- Tabla: Productos
CREATE TABLE Productos (
    id_producto INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(150) NOT NULL,
    descripcion TEXT,
    precio DECIMAL(10, 2) NOT NULL,
	status BIT DEFAULT 1,
    id_categoria INT FOREIGN KEY REFERENCES Categorias(id_categoria)
);
GO

-- Tabla: Inventarios
CREATE TABLE Inventarios (
    id_inventario INT PRIMARY KEY IDENTITY(1,1),
    cantidad INT NOT NULL,
    fecha_actualizacion DATETIME DEFAULT GETDATE(),
	id_producto INT NOT NULL FOREIGN KEY REFERENCES Productos(id_producto),
);
GO

-- Tabla: Usuarios
CREATE TABLE Usuarios (
    id_usuario INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(100) NOT NULL,
    contraseña VARCHAR(255) NOT NULL,
    tipo_usuario VARCHAR(20) CHECK (tipo_usuario IN ('empleado', 'admin')) DEFAULT 'empleado'
);
GO

-- Tabla: Facturas
CREATE TABLE Facturas (
    id_factura INT PRIMARY KEY IDENTITY(1,1),
    fecha DATETIME DEFAULT GETDATE(),
    total DECIMAL(10,2) NOT NULL,
	id_usuario INT NOT NULL FOREIGN KEY REFERENCES Usuarios(id_usuario),
);
GO

-- Tabla: Detalles_Factura
CREATE TABLE Detalles_Factura (
    id_detalle INT PRIMARY KEY IDENTITY(1,1),
    id_factura INT NOT NULL FOREIGN KEY REFERENCES Facturas(id_factura),
    id_producto INT NOT NULL FOREIGN KEY REFERENCES Productos(id_producto),
    cantidad INT NOT NULL,
    precio_unitario DECIMAL(10, 2) NOT NULL,
    subtotal AS (cantidad * precio_unitario) PERSISTED
);
GO


--////////Categorias////////

-- Insertar nueva categoría
CREATE PROCEDURE InsertarCategoria
    @nombre VARCHAR(100),
    @descripcion TEXT
AS
BEGIN
    INSERT INTO Categorias (nombre, descripcion)
    VALUES (@nombre, @descripcion);
END;
GO

-- Actualizar categoría
CREATE PROCEDURE ActualizarCategoria
    @id_categoria INT,
    @nombre VARCHAR(100),
    @descripcion TEXT
AS
BEGIN
    UPDATE Categorias
    SET nombre = @nombre, descripcion = @descripcion
    WHERE id_categoria = @id_categoria;
END;
GO

-- Eliminar categoría
CREATE PROCEDURE EliminarCategoria
    @id_categoria INT
AS
BEGIN
    DELETE FROM Categorias
    WHERE id_categoria = @id_categoria;
END;
GO


--////////Inventario////////

-- Insertar o actualizar inventario
CREATE PROCEDURE GuardarInventario
    @id_producto INT,
    @cantidad INT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Inventarios WHERE id_producto = @id_producto)
    BEGIN
        UPDATE Inventarios
        SET cantidad = @cantidad, fecha_actualizacion = GETDATE()
        WHERE id_producto = @id_producto;
    END
    ELSE
    BEGIN
        INSERT INTO Inventarios (id_producto, cantidad)
        VALUES (@id_producto, @cantidad);
    END
END;
GO


--////////Usuario////////


-- Insertar usuario
CREATE PROCEDURE InsertarUsuario
    @nombre VARCHAR(100),
    @contraseña VARCHAR(255),
    @tipo_usuario VARCHAR(20)
AS
BEGIN
    INSERT INTO Usuarios (nombre, contraseña, tipo_usuario)
    VALUES (@nombre, @contraseña, @tipo_usuario);
END;
GO





--////////Factura////////

CREATE PROCEDURE CrearFactura
    @id_usuario INT,
    @total DECIMAL(10,2),
    @id_factura INT OUTPUT
AS
BEGIN
    INSERT INTO Facturas (fecha, total, id_usuario)
    VALUES (GETDATE(), @total, @id_usuario);

    SET @id_factura = SCOPE_IDENTITY();
END;
GO


CREATE PROCEDURE InsertarDetalleFactura
    @id_factura INT,
    @id_producto INT,
    @cantidad INT,
    @precio_unitario DECIMAL(10,2)
AS
BEGIN
    INSERT INTO Detalles_Factura (id_factura, id_producto, cantidad, precio_unitario)
    VALUES (@id_factura, @id_producto, @cantidad, @precio_unitario);
END;
GO





CREATE PROCEDURE ObtenerProductosConStock
AS
BEGIN
    SELECT P.id_producto, P.nombre, I.cantidad
    FROM Productos P
    JOIN Inventarios I ON P.id_producto = I.id_producto
    WHERE I.cantidad > 0;
END;
GO



-- Inserta usuario admin
INSERT INTO Usuarios (nombre, contraseña, tipo_usuario)
VALUES ('admin', 'admin123', 'admin');

-- Inserta usuario Macdiel
INSERT INTO Usuarios (nombre, contraseña, tipo_usuario)
VALUES ('Macdiel', 'macdiel123', 'empleado');



INSERT INTO Categorias (nombre, descripcion)
VALUES 
('Electrónica', 'Productos electrónicos como teléfonos, televisores, computadoras y más.'),
('Ropa', 'Ropa para hombres, mujeres y niños de todas las estaciones.'),
('Hogar y Cocina', 'Artículos para el hogar, cocina, electrodomésticos y decoración.'),
('Juguetes', 'Juguetes para todas las edades, desde bebés hasta adolescentes.'),
('Deportes', 'Equipamiento deportivo, ropa y accesorios para actividades físicas.'),
('Belleza y Cuidado Personal', 'Cosméticos, productos para el cuidado del cabello y la piel.'),
('Libros', 'Libros de todos los géneros: ficción, no ficción, técnicos y educativos.'),
('Automotriz', 'Accesorios y herramientas para automóviles y motocicletas.'),
('Alimentos y Bebidas', 'Productos comestibles y bebidas no alcohólicas.'),
('Mascotas', 'Productos para perros, gatos y otras mascotas domésticas.');


-- Electrónica
INSERT INTO Productos (nombre, descripcion, precio, id_categoria)
VALUES 
('Smartphone Samsung Galaxy A54', 'Pantalla AMOLED de 6.4", 128GB de almacenamiento', 5999.00, 1),
('Laptop HP 14"', 'Intel Core i5, 8GB RAM, 512GB SSD', 11500.00, 1),
('Audífonos inalámbricos JBL', 'Bluetooth, batería de 20 horas', 899.00, 1);

-- Ropa
INSERT INTO Productos (nombre, descripcion, precio, id_categoria)
VALUES 
('Playera deportiva Nike', 'Tela Dri-FIT para máximo confort', 450.00, 2),
('Pantalón de mezclilla Levi', 'Corte recto, azul oscuro', 899.00, 2);

-- Alimentos
INSERT INTO Productos (nombre, descripcion, precio, id_categoria)
VALUES 
('Caja de cereal Kellogg', 'Cereal integral 500g', 89.50, 3),
('Botella de agua natural Bonafont', '1.5L', 18.00, 3);

-- Juguetes
INSERT INTO Productos (nombre, descripcion, precio, id_categoria)
VALUES 
('Set de bloques LEGO Classic', 'Caja creativa mediana con 484 piezas', 799.00, 4),
('Muñeca Barbie', 'Edición fashionista, incluye accesorios', 349.00, 4);

-- Hogar
INSERT INTO Productos (nombre, descripcion, precio, id_categoria)
VALUES 
('Set de sartenes T-Fal', '3 piezas antiadherentes', 749.00, 5),
('Cortina decorativa para sala', 'Tela blackout 140x230 cm', 459.00, 5);



select * from Usuarios where nombre = 'Macdiel' and contraseña = 'macdiel123'

SELECT COUNT(*) FROM Usuarios WHERE nombre = 'Macdiel' AND contraseña = 'macdiel123'

SELECT * FROM Usuarios

SELECT 
a.id_producto as [ID Producto],
a.nombre as [Nombre Producto], 
a.descripcion as [Desc Producto], 
a.precio as [Precio Producto], 
b.nombre as [Nombre Categoria] 
FROM Productos a inner join Categorias b on a.id_categoria = b.id_categoria


SELECT 
    F.id_factura,
    F.fecha,
    F.total,
    U.nombre AS usuario,
    COUNT(DF.id_detalle) AS cantidad_productos
FROM 
    Facturas F
JOIN 
    Usuarios U ON F.id_usuario = U.id_usuario
JOIN 
    Detalles_Factura DF ON F.id_factura = DF.id_factura
WHERE 
    CAST(F.fecha AS DATE) = CAST(GETDATE() AS DATE)
GROUP BY 
    F.id_factura, F.fecha, F.total, U.nombre
ORDER BY 
    F.fecha DESC;
