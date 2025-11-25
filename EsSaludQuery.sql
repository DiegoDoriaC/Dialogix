CREATE DATABASE Essalud
GO

USE Essalud
GO

CREATE TABLE Establecimiento (
    Id INT PRIMARY KEY,
    Nombre VARCHAR(150),
    Tipo VARCHAR(50),
    Region VARCHAR(80)
);

CREATE TABLE Consultorio (
    Id INT PRIMARY KEY,
    IdEstablecimiento INT,
    Nombre VARCHAR(120),
    Especialidad VARCHAR(80),
    FOREIGN KEY (IdEstablecimiento) REFERENCES Establecimiento(Id)
);

CREATE TABLE Medico (
    Id INT PRIMARY KEY,
    IdEstablecimiento INT,
    Nombre VARCHAR(150),
    CMP VARCHAR(20),
    Especialidad VARCHAR(80),
    FOREIGN KEY (IdEstablecimiento) REFERENCES Establecimiento(Id)
);

CREATE TABLE Paciente (
    Id INT PRIMARY KEY,
	DNI VARCHAR(8),
    Nombre VARCHAR(150),
    Sexo CHAR(1),
    FechaNacimiento DATE,
	Telefono VARCHAR(9),
    Correo VARCHAR(40),
	Direccion VARCHAR(50)
);

CREATE TABLE HorarioMedico (
    Id INT PRIMARY KEY,
    IdMedico INT,
    DiaSemana VARCHAR(20),
    HoraInicio TIME,
    HoraFin TIME,
    FOREIGN KEY (IdMedico) REFERENCES Medico(Id)
);

CREATE TABLE CitaMedica  (
	Id INT PRIMARY KEY,
	IdPaciente INT,
	IdMedico INT,
	FechaCita DATE,
	HoraCita TIME,
	Motivo VARCHAR(200),
	Estado VARCHAR(20)
)

CREATE TABLE ResultadosClinicos (
    IdResultadosClinicos INT IDENTITY(1,1) PRIMARY KEY,
    IdCitaMedica INT,
    TipoExamen NVARCHAR(100),
    Valor NVARCHAR(50),
    FechaResultado DATE,
    Observaciones NVARCHAR(500),
    Estado NVARCHAR(50) DEFAULT 'Registrado',
    FechaRegistro DATETIME,
	FOREIGN KEY (IdCitaMedica) REFERENCES CitaMedica(Id)
);


-- INSERTS — Establecimiento (30 registros)
INSERT INTO Establecimiento VALUES (1, 'Hospital Alberto Sabogal', 'Hospital', 'Callao');
INSERT INTO Establecimiento VALUES (2, 'Hospital Edgardo Rebagliati', 'Hospital', 'Lima');
INSERT INTO Establecimiento VALUES (3, 'Hospital Guillermo Almenara', 'Hospital', 'Lima');
INSERT INTO Establecimiento VALUES (4, 'Policlínico San Isidro', 'Policlinico', 'Lima');
INSERT INTO Establecimiento VALUES (5, 'Policlínico Grau', 'Policlinico', 'Lima');
INSERT INTO Establecimiento VALUES (6, 'Centro Médico La Victoria', 'Centro Medico', 'Lima');
INSERT INTO Establecimiento VALUES (7, 'Centro Médico Comas', 'Centro Medico', 'Lima');
INSERT INTO Establecimiento VALUES (8, 'Centro Médico SJM', 'Centro Medico', 'Lima');
INSERT INTO Establecimiento VALUES (9, 'Hospital Marino Molina', 'Hospital', 'Lima Norte');
INSERT INTO Establecimiento VALUES (10, 'Hospital Pedro de EsSalud', 'Hospital', 'Arequipa');
INSERT INTO Establecimiento VALUES (11, 'Policlínico Arequipa', 'Policlinico', 'Arequipa');
INSERT INTO Establecimiento VALUES (12, 'Centro Médico Cerro Colorado', 'Centro Medico', 'Arequipa');
INSERT INTO Establecimiento VALUES (13, 'Hospital Lambayeque', 'Hospital', 'Lambayeque');
INSERT INTO Establecimiento VALUES (14, 'Policlínico Chiclayo', 'Policlinico', 'Lambayeque');
INSERT INTO Establecimiento VALUES (15, 'Centro Médico Pimentel', 'Centro Medico', 'Lambayeque');
INSERT INTO Establecimiento VALUES (16, 'Hospital Piura', 'Hospital', 'Piura');
INSERT INTO Establecimiento VALUES (17, 'Policlínico Sullana', 'Policlinico', 'Piura');
INSERT INTO Establecimiento VALUES (18, 'Centro Médico Talara', 'Centro Medico', 'Piura');
INSERT INTO Establecimiento VALUES (19, 'Hospital Cusco', 'Hospital', 'Cusco');
INSERT INTO Establecimiento VALUES (20, 'Centro Médico Wanchaq', 'Centro Medico', 'Cusco');
INSERT INTO Establecimiento VALUES (21, 'Policlínico Juliaca', 'Policlinico', 'Puno');
INSERT INTO Establecimiento VALUES (22, 'Hospital Puno', 'Hospital', 'Puno');
INSERT INTO Establecimiento VALUES (23, 'Centro Médico Ilave', 'Centro Medico', 'Puno');
INSERT INTO Establecimiento VALUES (24, 'Hospital Iquitos', 'Hospital', 'Loreto');
INSERT INTO Establecimiento VALUES (25, 'Centro Médico Punchana', 'Centro Medico', 'Loreto');
INSERT INTO Establecimiento VALUES (26, 'Policlínico Tarapoto', 'Policlinico', 'San Martín');
INSERT INTO Establecimiento VALUES (27, 'Hospital Huaraz', 'Hospital', 'Áncash');
INSERT INTO Establecimiento VALUES (28, 'Centro Médico Chimbote', 'Centro Medico', 'Áncash');
INSERT INTO Establecimiento VALUES (29, 'Hospital Moquegua', 'Hospital', 'Moquegua');
INSERT INTO Establecimiento VALUES (30, 'Centro Médico Ilo', 'Centro Medico', 'Moquegua');


-- Consultorio — 50 INSERTs
INSERT INTO Consultorio VALUES (1, 1, 'Consultorio Medicina General A', 'Medicina General');
INSERT INTO Consultorio VALUES (2, 1, 'Consultorio Pediatría A', 'Pediatría');
INSERT INTO Consultorio VALUES (3, 2, 'Consultorio Medicina General B', 'Medicina General');
INSERT INTO Consultorio VALUES (4, 2, 'Consultorio Ginecología A', 'Ginecología');
INSERT INTO Consultorio VALUES (5, 3, 'Consultorio Medicina General C', 'Medicina General');
INSERT INTO Consultorio VALUES (6, 3, 'Consultorio Dermatología A', 'Dermatología');
INSERT INTO Consultorio VALUES (7, 4, 'Consultorio Odontología A', 'Odontología');
INSERT INTO Consultorio VALUES (8, 4, 'Consultorio Cardiología A', 'Cardiología');
INSERT INTO Consultorio VALUES (9, 5, 'Consultorio Medicina General D', 'Medicina General');
INSERT INTO Consultorio VALUES (10, 5, 'Consultorio Emergencias A', 'Emergencias');
INSERT INTO Consultorio VALUES (11, 6, 'Consultorio Pediatría B', 'Pediatría');
INSERT INTO Consultorio VALUES (12, 6, 'Consultorio Medicina General E', 'Medicina General');
INSERT INTO Consultorio VALUES (13, 7, 'Consultorio Ginecología B', 'Ginecología');
INSERT INTO Consultorio VALUES (14, 7, 'Consultorio Medicina General F', 'Medicina General');
INSERT INTO Consultorio VALUES (15, 8, 'Consultorio Cardiología B', 'Cardiología');
INSERT INTO Consultorio VALUES (16, 8, 'Consultorio Odontología B', 'Odontología');
INSERT INTO Consultorio VALUES (17, 9, 'Consultorio Medicina General G', 'Medicina General');
INSERT INTO Consultorio VALUES (18, 9, 'Consultorio Dermatología B', 'Dermatología');
INSERT INTO Consultorio VALUES (19, 10, 'Consultorio Medicina General H', 'Medicina General');
INSERT INTO Consultorio VALUES (20, 10, 'Consultorio Pediatría C', 'Pediatría');
INSERT INTO Consultorio VALUES (21, 11, 'Consultorio Ginecología C', 'Ginecología');
INSERT INTO Consultorio VALUES (22, 11, 'Consultorio Medicina General I', 'Medicina General');
INSERT INTO Consultorio VALUES (23, 12, 'Consultorio Odontología C', 'Odontología');
INSERT INTO Consultorio VALUES (24, 12, 'Consultorio Cardiología C', 'Cardiología');
INSERT INTO Consultorio VALUES (25, 13, 'Consultorio Medicina General J', 'Medicina General');
INSERT INTO Consultorio VALUES (26, 13, 'Consultorio Emergencias B', 'Emergencias');
INSERT INTO Consultorio VALUES (27, 14, 'Consultorio Pediatría D', 'Pediatría');
INSERT INTO Consultorio VALUES (28, 14, 'Consultorio Medicina General K', 'Medicina General');
INSERT INTO Consultorio VALUES (29, 15, 'Consultorio Ginecología D', 'Ginecología');
INSERT INTO Consultorio VALUES (30, 15, 'Consultorio Dermatología C', 'Dermatología');
INSERT INTO Consultorio VALUES (31, 16, 'Consultorio Medicina General L', 'Medicina General');
INSERT INTO Consultorio VALUES (32, 16, 'Consultorio Cardiología D', 'Cardiología');
INSERT INTO Consultorio VALUES (33, 17, 'Consultorio Odontología D', 'Odontología');
INSERT INTO Consultorio VALUES (34, 17, 'Consultorio Medicina General M', 'Medicina General');
INSERT INTO Consultorio VALUES (35, 18, 'Consultorio Ginecología E', 'Ginecología');
INSERT INTO Consultorio VALUES (36, 18, 'Consultorio Pediatría E', 'Pediatría');
INSERT INTO Consultorio VALUES (37, 19, 'Consultorio Medicina General N', 'Medicina General');
INSERT INTO Consultorio VALUES (38, 19, 'Consultorio Emergencias C', 'Emergencias');
INSERT INTO Consultorio VALUES (39, 20, 'Consultorio Dermatología D', 'Dermatología');
INSERT INTO Consultorio VALUES (40, 20, 'Consultorio Pediatría F', 'Pediatría');
INSERT INTO Consultorio VALUES (41, 21, 'Consultorio Medicina General O', 'Medicina General');
INSERT INTO Consultorio VALUES (42, 21, 'Consultorio Odontología E', 'Odontología');
INSERT INTO Consultorio VALUES (43, 22, 'Consultorio Cardiología E', 'Cardiología');
INSERT INTO Consultorio VALUES (44, 22, 'Consultorio Medicina General P', 'Medicina General');
INSERT INTO Consultorio VALUES (45, 23, 'Consultorio Pediatría G', 'Pediatría');
INSERT INTO Consultorio VALUES (46, 23, 'Consultorio Medicina General Q', 'Medicina General');
INSERT INTO Consultorio VALUES (47, 24, 'Consultorio Dermatología E', 'Dermatología');
INSERT INTO Consultorio VALUES (48, 24, 'Consultorio Ginecología F', 'Ginecología');
INSERT INTO Consultorio VALUES (49, 25, 'Consultorio Medicina General R', 'Medicina General');
INSERT INTO Consultorio VALUES (50, 25, 'Consultorio Emergencias D', 'Emergencias');



-- INSERTS — Médicos (80 registros)
INSERT INTO Medico VALUES (1, 1, 'Dr. Carlos Salazar', 'CMP10231', 'Medicina General');
INSERT INTO Medico VALUES (2, 1, 'Dra. Mariela Torres', 'CMP15542', 'Pediatría');
INSERT INTO Medico VALUES (3, 2, 'Dr. Hugo Lozano', 'CMP16721', 'Medicina General');
INSERT INTO Medico VALUES (4, 2, 'Dra. Paula Medina', 'CMP14220', 'Ginecología');
INSERT INTO Medico VALUES (5, 3, 'Dr. Ricardo Rojas', 'CMP13210', 'Medicina General');
INSERT INTO Medico VALUES (6, 3, 'Dra. Sandra Cárdenas', 'CMP19932', 'Dermatología');
INSERT INTO Medico VALUES (7, 4, 'Dr. Julio Paredes', 'CMP22014', 'Odontología');
INSERT INTO Medico VALUES (8, 4, 'Dr. Manuel Peralta', 'CMP21033', 'Cardiología');
INSERT INTO Medico VALUES (9, 5, 'Dr. Álvaro Hernández', 'CMP19873', 'Medicina General');
INSERT INTO Medico VALUES (10, 5, 'Dra. Lianna Campos', 'CMP17711', 'Emergencias');
INSERT INTO Medico VALUES (11, 6, 'Dr. José Aguilar', 'CMP16544', 'Pediatría');
INSERT INTO Medico VALUES (12, 6, 'Dra. Teresa Salinas', 'CMP15122', 'Medicina General');
INSERT INTO Medico VALUES (13, 7, 'Dra. Miriam Díaz', 'CMP20135', 'Ginecología');
INSERT INTO Medico VALUES (14, 7, 'Dr. Walter Cornejo', 'CMP20298', 'Medicina General');
INSERT INTO Medico VALUES (15, 8, 'Dr. Luis Quispe', 'CMP21347', 'Cardiología');
INSERT INTO Medico VALUES (16, 8, 'Dr. Alex Suárez', 'CMP22318', 'Odontología');
INSERT INTO Medico VALUES (17, 9, 'Dra. Cecilia Ramos', 'CMP18744', 'Medicina General');
INSERT INTO Medico VALUES (18, 9, 'Dr. Henry Bravo', 'CMP19900', 'Dermatología');
INSERT INTO Medico VALUES (19, 10, 'Dr. Mario Carbajal', 'CMP14555', 'Medicina General');
INSERT INTO Medico VALUES (20, 10, 'Dr. Edson Valdez', 'CMP17340', 'Pediatría');
INSERT INTO Medico VALUES (21, 11, 'Dra. Patricia Montes', 'CMP18472', 'Ginecología');
INSERT INTO Medico VALUES (22, 11, 'Dr. Diego Castillo', 'CMP18811', 'Medicina General');
INSERT INTO Medico VALUES (23, 12, 'Dr. Aldo Montoya', 'CMP19952', 'Odontología');
INSERT INTO Medico VALUES (24, 12, 'Dra. Gabriela León', 'CMP20319', 'Cardiología');
INSERT INTO Medico VALUES (25, 13, 'Dr. Arturo Linares', 'CMP21098', 'Medicina General');
INSERT INTO Medico VALUES (26, 13, 'Dr. Oscar Zamora', 'CMP21567', 'Emergencias');
INSERT INTO Medico VALUES (27, 14, 'Dra. Brenda Ríos', 'CMP17483', 'Pediatría');
INSERT INTO Medico VALUES (28, 14, 'Dr. Jaime Rivas', 'CMP18210', 'Medicina General');
INSERT INTO Medico VALUES (29, 15, 'Dra. Karina Peña', 'CMP20733', 'Ginecología');
INSERT INTO Medico VALUES (30, 15, 'Dr. Luis Alberto Paz', 'CMP21455', 'Dermatología');
INSERT INTO Medico VALUES (31, 16, 'Dr. Jorge Matos', 'CMP12833', 'Medicina General');
INSERT INTO Medico VALUES (32, 16, 'Dr. Harold Tapia', 'CMP14472', 'Cardiología');
INSERT INTO Medico VALUES (33, 17, 'Dr. Víctor Huamán', 'CMP17621', 'Odontología');
INSERT INTO Medico VALUES (34, 17, 'Dra. Rosana Gutiérrez', 'CMP18112', 'Medicina General');
INSERT INTO Medico VALUES (35, 18, 'Dra. Yessenia Barrios', 'CMP19800', 'Ginecología');
INSERT INTO Medico VALUES (36, 18, 'Dr. Juan Rojas', 'CMP19991', 'Pediatría');
INSERT INTO Medico VALUES (37, 19, 'Dr. Manuel Tello', 'CMP18777', 'Medicina General');
INSERT INTO Medico VALUES (38, 19, 'Dr. Tomás Herrera', 'CMP19988', 'Emergencias');
INSERT INTO Medico VALUES (39, 20, 'Dra. Rita Lozano', 'CMP22133', 'Dermatología');
INSERT INTO Medico VALUES (40, 20, 'Dr. Javier Silva', 'CMP22391', 'Pediatría');
INSERT INTO Medico VALUES (41, 21, 'Dr. Alan Novoa', 'CMP14481', 'Medicina General');
INSERT INTO Medico VALUES (42, 21, 'Dr. Miguel Galindo', 'CMP16700', 'Odontología');
INSERT INTO Medico VALUES (43, 22, 'Dr. Guido Trujillo', 'CMP17784', 'Cardiología');
INSERT INTO Medico VALUES (44, 22, 'Dra. Nora Félix', 'CMP18921', 'Medicina General');
INSERT INTO Medico VALUES (45, 23, 'Dr. Freddy Huaranca', 'CMP20512', 'Pediatría');
INSERT INTO Medico VALUES (46, 23, 'Dra. Andrea Bermúdez', 'CMP20934', 'Medicina General');
INSERT INTO Medico VALUES (47, 24, 'Dr. Nelson Vargas', 'CMP21481', 'Dermatología');
INSERT INTO Medico VALUES (48, 24, 'Dr. Edwin Vera', 'CMP21672', 'Ginecología');
INSERT INTO Medico VALUES (49, 25, 'Dra. Sofía Luna', 'CMP18540', 'Medicina General');
INSERT INTO Medico VALUES (50, 25, 'Dr. Abel Cáceres', 'CMP21210', 'Emergencias');
INSERT INTO Medico VALUES (51, 26, 'Dr. Edmundo Salvatierra', 'CMP18888', 'Odontología');
INSERT INTO Medico VALUES (52, 26, 'Dra. Fiorella Céspedes', 'CMP19220', 'Medicina General');
INSERT INTO Medico VALUES (53, 27, 'Dr. Daniel Meza', 'CMP20383', 'Cardiología');
INSERT INTO Medico VALUES (54, 27, 'Dra. Alejandra Pinto', 'CMP20555', 'Pediatría');
INSERT INTO Medico VALUES (55, 28, 'Dr. Cristhian Ruiz', 'CMP19510', 'Medicina General');
INSERT INTO Medico VALUES (56, 28, 'Dr. Mauricio Llerena', 'CMP19740', 'Dermatología');
INSERT INTO Medico VALUES (57, 29, 'Dra. Nicole Ormeño', 'CMP18011', 'Ginecología');
INSERT INTO Medico VALUES (58, 29, 'Dr. Ronald Cruz', 'CMP18577', 'Medicina General');
INSERT INTO Medico VALUES (59, 30, 'Dr. Eder Villanueva', 'CMP21420', 'Odontología');
INSERT INTO Medico VALUES (60, 30, 'Dr. Luciano Chiroque', 'CMP22888', 'Cardiología');
INSERT INTO Medico VALUES (61, 1, 'Dr. Samuel Zorrilla', 'CMP16555', 'Medicina General');
INSERT INTO Medico VALUES (62, 2, 'Dra. Elisa Rentería', 'CMP17373', 'Dermatología');
INSERT INTO Medico VALUES (63, 3, 'Dr. Mauro Villena', 'CMP18231', 'Pediatría');
INSERT INTO Medico VALUES (64, 4, 'Dra. Nancy Quinteros', 'CMP19901', 'Ginecología');
INSERT INTO Medico VALUES (65, 5, 'Dr. César Córdova', 'CMP20991', 'Emergencias');
INSERT INTO Medico VALUES (66, 6, 'Dr. Piero Gamboa', 'CMP21502', 'Medicina General');
INSERT INTO Medico VALUES (67, 7, 'Dr. Julio Ledesma', 'CMP21771', 'Cardiología');
INSERT INTO Medico VALUES (68, 8, 'Dra. Karla Reátegui', 'CMP22055', 'Odontología');
INSERT INTO Medico VALUES (69, 9, 'Dra. Joanna Perales', 'CMP19299', 'Pediatría');
INSERT INTO Medico VALUES (70, 10, 'Dr. Marcos Hinostroza', 'CMP19322', 'Dermatología');
INSERT INTO Medico VALUES (71, 11, 'Dr. Sebastián Vega', 'CMP20377', 'Medicina General');
INSERT INTO Medico VALUES (72, 12, 'Dra. Celeste Gálvez', 'CMP17801', 'Cardiología');
INSERT INTO Medico VALUES (73, 13, 'Dr. Renato Mezones', 'CMP18799', 'Odontología');
INSERT INTO Medico VALUES (74, 14, 'Dra. Kelly Obando', 'CMP19001', 'Dermatología');
INSERT INTO Medico VALUES (75, 15, 'Dr. Javier Loyola', 'CMP21415', 'Medicina General');
INSERT INTO Medico VALUES (76, 16, 'Dra. Melissa Sandoval', 'CMP17666', 'Pediatría');
INSERT INTO Medico VALUES (77, 17, 'Dr. Oscar Millones', 'CMP20449', 'Ginecología');
INSERT INTO Medico VALUES (78, 18, 'Dr. Elmer Gutiérrez', 'CMP20999', 'Medicina General');
INSERT INTO Medico VALUES (79, 19, 'Dra. Pierina Concha', 'CMP21111', 'Emergencias');
INSERT INTO Medico VALUES (80, 20, 'Dr. Alejandro Cárdenas', 'CMP21400', 'Medicina General');


-- Pacientes — 70 INSERTs
INSERT INTO Paciente VALUES (1,'72104533','Carlos Alberto Ruiz', 'M','1984-03-12','987654321','carlos.ruiz@example.com','Av. Los Álamos 123');
INSERT INTO Paciente VALUES (2,'74823011','María Fernanda Torres', 'F','1992-07-25','986112233','maria.torres@example.com','Jr. Las Margaritas 450');
INSERT INTO Paciente VALUES (3,'70334421','Luis Enrique Salazar', 'M','1978-11-03','983221145','luis.salazar@example.com','Av. Próceres 1290');
INSERT INTO Paciente VALUES (4,'71580244','Ana Cecilia Vargas', 'F','1989-05-18','981345987','ana.vargas@example.com','Mz C Lote 8 Urb. Los Cedros');
INSERT INTO Paciente VALUES (5,'76011289','José Manuel Pineda', 'M','1995-02-16','982555771','jose.pineda@example.com','Calle Los Fresnos 221');
INSERT INTO Paciente VALUES (6,'70149233','Lucía Alejandra Campos', 'F','1990-12-01','987100200','lucia.campos@example.com','Av. Perú 1880');
INSERT INTO Paciente VALUES (7,'71288900','Rodolfo Iván Huamán', 'M','1975-09-28','987666321','rodolfo.huaman@example.com','Jr. Huáscar 540');
INSERT INTO Paciente VALUES (8,'75500213','Carmen Rosa Díaz', 'F','1981-01-19','987991124','carmen.diaz@example.com','Av. Las Gardenias 1021');
INSERT INTO Paciente VALUES (9,'70893451','Sergio Daniel Ponce', 'M','1993-10-10','981234776','sergio.ponce@example.com','Los Jardines 455');
INSERT INTO Paciente VALUES (10,'70654382','Valeria Sofía León', 'F','2001-04-12','984300123','valeria.leon@example.com','Av. Petit Thouars 2000');
INSERT INTO Paciente VALUES (11,'73001544','Julio César Medina', 'M','1980-08-25','984112987','julio.medina@example.com','Av. Bolívar 540');
INSERT INTO Paciente VALUES (12,'76551209','Karla Milagros Rivas', 'F','1994-06-02','989334122','karla.rivas@example.com','Urb. San Gabriel Mz A Lt 9');
INSERT INTO Paciente VALUES (13,'74889912','Héctor Manuel Lozano', 'M','1977-03-14','981778900','hector.lozano@example.com','Calle Huascarán 118');
INSERT INTO Paciente VALUES (14,'70554432','Diana Elizabeth Torres', 'F','1988-02-21','982988100','diana.torres@example.com','Av. La Cultura 1500');
INSERT INTO Paciente VALUES (15,'72690310','Gabriel Alejandro Castillo', 'M','2003-05-04','989002291','gabriel.castillo@example.com','Calle Las Lomas 311');
INSERT INTO Paciente VALUES (16,'76100283','Fiorella Carolina Chávez', 'F','1991-07-16','983002311','fiorella.chavez@example.com','Av. Tomás Valle 900');
INSERT INTO Paciente VALUES (17,'70233417','Miguel Ángel Gutiérrez', 'M','1972-12-19','986777645','miguel.gutierrez@example.com','Calle Grau 120');
INSERT INTO Paciente VALUES (18,'73128893','Nathalie Paola Cárdenas', 'F','1998-11-22','983889112','nathalie.cardenas@example.com','Urb. Los Portales F3-2');
INSERT INTO Paciente VALUES (19,'74881230','Christian David Torres', 'M','1983-06-13','987123990','christian.torres@example.com','Av. Naciones Unidas 330');
INSERT INTO Paciente VALUES (20,'79011289','Andrea Jazmín Huamán', 'F','1996-01-27','981220017','andrea.huaman@example.com','Calle Las Camelias 120');
INSERT INTO Paciente VALUES (21,'77544129','Manuel Ricardo Pajares', 'M','1979-10-30','987410999','manuel.pajares@example.com','Av. Angamos 999');
INSERT INTO Paciente VALUES (22,'76203912','Daniela Verónica Dávila', 'F','2005-03-14','984560298','daniela.davila@example.com','Av. Guardia Civil 190');
INSERT INTO Paciente VALUES (23,'71588912','Oscar Renzo Salinas', 'M','1968-09-05','981664229','oscar.salinas@example.com','Jr. Puno 321');
INSERT INTO Paciente VALUES (24,'74512003','Rosa Emilia Campos', 'F','1973-04-08','987444123','rosa.campos@example.com','Av. San Martín 455');
INSERT INTO Paciente VALUES (25,'73300921','Diego Aníbal Cortez', 'M','1990-12-05','987122440','diego.cortez@example.com','Av. Universitaria 1780');
INSERT INTO Paciente VALUES (26,'76443221','Elena Karina Morales', 'F','1985-02-17','986321788','elena.morales@example.com','Urb. Los Próceres Lt 22');
INSERT INTO Paciente VALUES (27,'71234598','Alonso Martín Guerrero', 'M','1997-09-29','989432012','alonso.guerrero@example.com','Av. Independencia 531');
INSERT INTO Paciente VALUES (28,'74812311','Ruth Noemí Cueva', 'F','1976-01-02','988221001','ruth.cueva@example.com','Jr. Tarapacá 812');
INSERT INTO Paciente VALUES (29,'70023419','Cristian Omar Lazo', 'M','1994-12-22','989442200','cristian.lazo@example.com','Calle Piura 707');
INSERT INTO Paciente VALUES (30,'73099122','Evelyn Johana Segura', 'F','2002-05-10','986530221','evelyn.segura@example.com','Av. Tacna 650');
INSERT INTO Paciente VALUES (31,'71133278','Renzo Eduardo Portugal', 'M','1986-08-11','987700321','renzo.portugal@example.com','Av. Los Alisos 945');
INSERT INTO Paciente VALUES (32,'76344210','Milagros Daniela Bravo', 'F','1993-04-01','986112776','milagros.bravo@example.com','Urb. Los Rosales 123');
INSERT INTO Paciente VALUES (33,'71221903','Gerson Alexander Luján', 'M','1970-06-20','981114450','gerson.lujan@example.com','Av. La Marina 410');
INSERT INTO Paciente VALUES (34,'75221094','Cinthia Paola Quispe', 'F','1982-01-22','987990122','cinthia.quispe@example.com','Av. Arequipa 1790');
INSERT INTO Paciente VALUES (35,'74300921','Eduardo Martín Pérez', 'M','1999-12-31','989044310','eduardo.perez@example.com','Av. Javier Prado 700');
INSERT INTO Paciente VALUES (36,'70091283','Melissa Carolina Tapia', 'F','1974-07-18','982220111','melissa.tapia@example.com','Calle Los Robles 202');
INSERT INTO Paciente VALUES (37,'73120001','Pedro Antonio Ramos', 'M','1987-10-02','983330121','pedro.ramos@example.com','Av. Del Ejército 456');
INSERT INTO Paciente VALUES (38,'79832100','Yolanda Isabel Córdova', 'F','1969-02-09','987812204','yolanda.cordova@example.com','Av. Grau 711');
INSERT INTO Paciente VALUES (39,'71244533','Kevin Arturo Cabrera', 'M','2004-09-14','981223498','kevin.cabrera@example.com','Urb. El Trebol Lt 12');
INSERT INTO Paciente VALUES (40,'75331190','Leslie Ariana Romero', 'F','1983-06-26','989221900','leslie.romero@example.com','Av. Angamos Oeste 210');
INSERT INTO Paciente VALUES (41,'71233481','Mauricio José Meza', 'M','1991-07-19','987555121','mauricio.meza@example.com','Calle Cusco 919');
INSERT INTO Paciente VALUES (42,'76299123','Patricia Alejandra Zapata', 'F','1978-03-03','983221184','patricia.zapata@example.com','Av. El Sol 300');
INSERT INTO Paciente VALUES (43,'70883314','Jean Franco Mendieta', 'M','1985-02-28','981552093','jean.mendieta@example.com','Calle Bolivia 488');
INSERT INTO Paciente VALUES (44,'74500122','Lorena Magaly Huerta', 'F','1997-06-23','986330940','lorena.huerta@example.com','Av. San Luis 1200');
INSERT INTO Paciente VALUES (45,'73122390','Aldo Sebastián Ramos', 'M','2000-01-18','988331457','aldo.ramos@example.com','Av. Argentina 780');
INSERT INTO Paciente VALUES (46,'75409913','Erika María Espinoza', 'F','1989-09-01','982221778','erika.espinoza@example.com','Calle Moquegua 556');
INSERT INTO Paciente VALUES (47,'76488321','Jorge Luis Cabrera', 'M','1976-02-19','983445678','jorge.cabrera@example.com','Av. Surco 310');
INSERT INTO Paciente VALUES (48,'72339001','Diana Sofía Gamboa', 'F','1995-03-11','987410200','diana.gamboa@example.com','Av. República 612');
INSERT INTO Paciente VALUES (49,'74480912','Marco Antonio Molina', 'M','1981-08-30','981778340','marco.molina@example.com','Av. La Paz 340');
INSERT INTO Paciente VALUES (50,'72201922','Katherine Ivonne Torres', 'F','1993-02-17','982100299','katherine.torres@example.com','Av. Guardia Republicana 999');
INSERT INTO Paciente VALUES (51,'74530992','Harold Esteban Dávila', 'M','1979-07-07','987142309','harold.davila@example.com','Jr. Cañete 144');
INSERT INTO Paciente VALUES (52,'76429119','Lina Patricia Ochoa', 'F','1984-03-25','984441220','lina.ochoa@example.com','Av. El Derby 1123');
INSERT INTO Paciente VALUES (53,'74322091','Rubén Alberto Poma', 'M','1990-11-12','982311009','ruben.poma@example.com','Av. San Felipe 400');
INSERT INTO Paciente VALUES (54,'73900921','Milagros Esther Soto', 'F','1999-07-21','989000111','milagros.soto@example.com','Av. Guardia Civil 320');
INSERT INTO Paciente VALUES (55,'72423119','Rodrigo Andrés Cárdenas', 'M','1986-05-11','981883322','rodrigo.cardenas@example.com','Calle Libertad 440');
INSERT INTO Paciente VALUES (56,'74622109','Pamela Cristina Saavedra', 'F','1973-02-18','987411223','pamela.saavedra@example.com','Jr. Junín 301');
INSERT INTO Paciente VALUES (57,'74211098','Kevin Alfredo Blanco', 'M','2002-10-07','986402112','kevin.blanco@example.com','Av. Lima 1220');
INSERT INTO Paciente VALUES (58,'76398122','Carolina Isabel Pinedo', 'F','1994-03-01','983010211','carolina.pinedo@example.com','Av. Salaverry 990');
INSERT INTO Paciente VALUES (59,'72110428','Alexis Mario Goycochea', 'M','1992-12-30','981421309','alexis.goycochea@example.com','Av. La Paz 130');
INSERT INTO Paciente VALUES (60,'74033112','Jazmín Lucero Aguilar', 'F','2003-01-05','989211011','jazmin.aguilar@example.com','Av. Primavera 455');
INSERT INTO Paciente VALUES (61,'70142219','Hugo Sebastián Huerta', 'M','1988-04-27','987114209','hugo.huerta@example.com','Calle Amazonas 120');
INSERT INTO Paciente VALUES (62,'74422819','Fiorella Pilar Reyes', 'F','1996-09-17','981002334','fiorella.reyes@example.com','Urb. Las Flores 150');
INSERT INTO Paciente VALUES (63,'76229918','Daniel Mauricio Zevallos', 'M','1991-11-24','986320543','daniel.zevallos@example.com','Av. Del Parque 200');
INSERT INTO Paciente VALUES (64,'71332044','Bianca Celeste Romero', 'F','1980-07-30','987200541','bianca.romero@example.com','Av. Los Olivos 500');
INSERT INTO Paciente VALUES (65,'75500229','Stuart Raúl Castañeda', 'M','1998-03-18','983440122','stuart.castaneda@example.com','Av. Alcázar 133');
INSERT INTO Paciente VALUES (66,'76440221','Stefany Noelia López', 'F','1997-08-12','984556210','stefany.lopez@example.com','Calle Los Laureles 190');
INSERT INTO Paciente VALUES (67,'70190211','Joel Martín Jurado', 'M','1995-12-09','989778010','joel.jurado@example.com','Av. San Borja 900');
INSERT INTO Paciente VALUES (68,'72511092','Yuliana Marisol Paredes', 'F','1982-10-04','987642110','yuliana.paredes@example.com','Av. Paseo Colón 530');
INSERT INTO Paciente VALUES (69,'73520094','Henry Alonso Pastor', 'M','1990-05-22','984200099','henry.pastor@example.com','Av. Nicolás Ayllón 771');
INSERT INTO Paciente VALUES (70,'71029011','Tatiana Milena Guzmán', 'F','1998-04-15','986132110','tatiana.guzman@example.com','Av. Los Laureles 915');



-- INSERTS — HorarioMedico (70 registros)
INSERT INTO HorarioMedico VALUES (1, 1, 'Lunes', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (2, 2, 'Martes', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (3, 3, 'Miércoles', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (4, 4, 'Jueves', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (5, 5, 'Viernes', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (6, 6, 'Lunes', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (7, 7, 'Martes', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (8, 8, 'Miércoles', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (9, 9, 'Jueves', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (10, 10, 'Viernes', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (11, 11, 'Lunes', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (12, 12, 'Martes', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (13, 13, 'Miércoles', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (14, 14, 'Jueves', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (15, 15, 'Viernes', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (16, 16, 'Lunes', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (17, 17, 'Martes', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (18, 18, 'Miércoles', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (19, 19, 'Jueves', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (20, 20, 'Viernes', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (21, 21, 'Lunes', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (22, 22, 'Martes', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (23, 23, 'Miércoles', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (24, 24, 'Jueves', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (25, 25, 'Viernes', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (26, 26, 'Lunes', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (27, 27, 'Martes', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (28, 28, 'Miércoles', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (29, 29, 'Jueves', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (30, 30, 'Viernes', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (31, 31, 'Lunes', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (32, 32, 'Martes', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (33, 33, 'Miércoles', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (34, 34, 'Jueves', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (35, 35, 'Viernes', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (36, 36, 'Lunes', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (37, 37, 'Martes', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (38, 38, 'Miércoles', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (39, 39, 'Jueves', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (40, 40, 'Viernes', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (41, 41, 'Lunes', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (42, 42, 'Martes', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (43, 43, 'Miércoles', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (44, 44, 'Jueves', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (45, 45, 'Viernes', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (46, 46, 'Lunes', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (47, 47, 'Martes', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (48, 48, 'Miércoles', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (49, 49, 'Jueves', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (50, 50, 'Viernes', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (51, 51, 'Lunes', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (52, 52, 'Martes', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (53, 53, 'Miércoles', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (54, 54, 'Jueves', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (55, 55, 'Viernes', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (56, 56, 'Lunes', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (57, 57, 'Martes', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (58, 58, 'Miércoles', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (59, 59, 'Jueves', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (60, 60, 'Viernes', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (61, 61, 'Lunes', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (62, 62, 'Martes', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (63, 63, 'Miércoles', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (64, 64, 'Jueves', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (65, 65, 'Viernes', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (66, 66, 'Lunes', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (67, 67, 'Martes', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (68, 68, 'Miércoles', '14:00', '20:00');
INSERT INTO HorarioMedico VALUES (69, 69, 'Jueves', '08:00', '14:00');
INSERT INTO HorarioMedico VALUES (70, 70, 'Viernes', '14:00', '20:00');


-- INSERTS — CitaMedica (120 registros)
INSERT INTO CitaMedica VALUES (1, 12, 5, '2025-01-05', '08:00', 'Control general', 'Programada');
INSERT INTO CitaMedica VALUES (2, 44, 12, '2025-01-06', '09:30', 'Dolor de cabeza', 'Atendida');
INSERT INTO CitaMedica VALUES (3, 3, 8, '2025-01-06', '11:15', 'Chequeo anual', 'Programada');
INSERT INTO CitaMedica VALUES (4, 70, 15, '2025-01-07', '14:00', 'Dolor abdominal', 'Atendida');
INSERT INTO CitaMedica VALUES (5, 27, 20, '2025-01-07', '16:30', 'Problemas respiratorios', 'Cancelada');
INSERT INTO CitaMedica VALUES (6, 15, 1, '2025-01-08', '10:45', 'Dolor lumbar', 'Atendida');
INSERT INTO CitaMedica VALUES (7, 88, 33, '2025-01-09', '12:00', 'Consulta preventiva', 'Programada');
INSERT INTO CitaMedica VALUES (8, 101, 22, '2025-01-09', '17:15', 'Revisión post-operatoria', 'Programada');
INSERT INTO CitaMedica VALUES (9, 4, 7, '2025-01-10', '09:00', 'Gripe persistente', 'Atendida');
INSERT INTO CitaMedica VALUES (10, 133, 11, '2025-01-11', '15:45', 'Control de presión', 'Programada');

INSERT INTO CitaMedica VALUES (11, 55, 40, '2025-01-12', '08:15', 'Chequeo general', 'Programada');
INSERT INTO CitaMedica VALUES (12, 19, 45, '2025-01-12', '09:45', 'Dolor muscular', 'Atendida');
INSERT INTO CitaMedica VALUES (13, 23, 30, '2025-01-12', '11:00', 'Alergia', 'Programada');
INSERT INTO CitaMedica VALUES (14, 92, 18, '2025-01-13', '13:30', 'Control pediátrico', 'Atendida');
INSERT INTO CitaMedica VALUES (15, 120, 29, '2025-01-13', '14:45', 'Dolor de pecho', 'Programada');
INSERT INTO CitaMedica VALUES (16, 33, 50, '2025-01-14', '16:15', 'Examen de rutina', 'Programada');
INSERT INTO CitaMedica VALUES (17, 66, 61, '2025-01-14', '17:30', 'Chequeo general', 'Atendida');
INSERT INTO CitaMedica VALUES (18, 7, 10, '2025-01-15', '08:45', 'Dolor de garganta', 'Atendida');
INSERT INTO CitaMedica VALUES (19, 41, 8, '2025-01-15', '09:15', 'Inflamación', 'Programada');
INSERT INTO CitaMedica VALUES (20, 143, 13, '2025-01-15', '10:30', 'Consulta por fiebre', 'Programada');

INSERT INTO CitaMedica VALUES (21, 17, 2, '2025-01-16', '08:00', 'Control general', 'Programada');
INSERT INTO CitaMedica VALUES (22, 8, 5, '2025-01-16', '08:30', 'Dolor de cabeza', 'Cancelada');
INSERT INTO CitaMedica VALUES (23, 13, 1, '2025-01-16', '09:00', 'Chequeo anual', 'Atendida');
INSERT INTO CitaMedica VALUES (24, 4, 7, '2025-01-16', '09:30', 'Seguimiento', 'Programada');
INSERT INTO CitaMedica VALUES (25, 21, 3, '2025-01-16', '10:00', 'Consulta general', 'Cancelada');
INSERT INTO CitaMedica VALUES (26, 19, 8, '2025-01-16', '10:30', 'Dolor muscular', 'Atendida');
INSERT INTO CitaMedica VALUES (27, 6, 9, '2025-01-16', '11:00', 'Alergia', 'Programada');
INSERT INTO CitaMedica VALUES (28, 11, 4, '2025-01-16', '11:30', 'Control mensual', 'Atendida');
INSERT INTO CitaMedica VALUES (29, 15, 6, '2025-01-16', '12:00', 'Revisión', 'Cancelada');
INSERT INTO CitaMedica VALUES (30, 10, 2, '2025-01-16', '14:00', 'Fiebre persistente', 'Programada');

INSERT INTO CitaMedica VALUES (31, 3, 1, '2025-01-17', '08:00', 'Chequeo general', 'Atendida');
INSERT INTO CitaMedica VALUES (32, 12, 5, '2025-01-17', '08:30', 'Molestia estomacal', 'Cancelada');
INSERT INTO CitaMedica VALUES (33, 20, 7, '2025-01-17', '09:00', 'Evaluación', 'Programada');
INSERT INTO CitaMedica VALUES (34, 5, 8, '2025-01-17', '09:30', 'Dolor crónico', 'Atendida');
INSERT INTO CitaMedica VALUES (35, 9, 4, '2025-01-17', '10:00', 'Infección leve', 'Programada');
INSERT INTO CitaMedica VALUES (36, 14, 3, '2025-01-17', '10:30', 'Control', 'Cancelada');
INSERT INTO CitaMedica VALUES (37, 18, 6, '2025-01-17', '11:00', 'Revisión anual', 'Programada');
INSERT INTO CitaMedica VALUES (38, 7, 2, '2025-01-17', '11:30', 'Chequeo rápido', 'Atendida');
INSERT INTO CitaMedica VALUES (39, 16, 9, '2025-01-17', '12:00', 'Problema respiratorio', 'Cancelada');
INSERT INTO CitaMedica VALUES (40, 2, 1, '2025-01-17', '14:00', 'Dolor abdominal', 'Programada');

INSERT INTO CitaMedica VALUES (41, 3, 3, '2025-01-18', '08:00', 'Control general', 'Cancelada');
INSERT INTO CitaMedica VALUES (42, 10, 4, '2025-01-18', '08:30', 'Dolor de pecho', 'Atendida');
INSERT INTO CitaMedica VALUES (43, 6, 5, '2025-01-18', '09:00', 'Seguimiento', 'Programada');
INSERT INTO CitaMedica VALUES (44, 12, 6, '2025-01-18', '09:30', 'Chequeo anual', 'Cancelada');
INSERT INTO CitaMedica VALUES (45, 14, 7, '2025-01-18', '10:00', 'Fatiga', 'Programada');
INSERT INTO CitaMedica VALUES (46, 2, 8, '2025-01-18', '10:30', 'Infección viral', 'Atendida');
INSERT INTO CitaMedica VALUES (47, 1, 9, '2025-01-18', '11:00', 'Consulta general', 'Cancelada');
INSERT INTO CitaMedica VALUES (48, 8, 1, '2025-01-18', '11:30', 'Revisión', 'Programada');
INSERT INTO CitaMedica VALUES (49, 4, 2, '2025-01-18', '12:00', 'Dolor lumbar', 'Atendida');
INSERT INTO CitaMedica VALUES (50, 11, 3, '2025-01-18', '14:00', 'Control mensual', 'Programada');

INSERT INTO CitaMedica VALUES (51, 13, 4, '2025-01-19', '08:00', 'Dolor muscular', 'Programada');
INSERT INTO CitaMedica VALUES (52, 19, 5, '2025-01-19', '08:30', 'Seguimiento', 'Atendida');
INSERT INTO CitaMedica VALUES (53, 7, 6, '2025-01-19', '09:00', 'Problemas digestivos', 'Cancelada');
INSERT INTO CitaMedica VALUES (54, 15, 7, '2025-01-19', '09:30', 'Control general', 'Atendida');
INSERT INTO CitaMedica VALUES (55, 5, 8, '2025-01-19', '10:00', 'Alergia', 'Programada');
INSERT INTO CitaMedica VALUES (56, 9, 9, '2025-01-19', '10:30', 'Evaluación', 'Cancelada');
INSERT INTO CitaMedica VALUES (57, 16, 1, '2025-01-19', '11:00', 'Chequeo anual', 'Programada');
INSERT INTO CitaMedica VALUES (58, 18, 2, '2025-01-19', '11:30', 'Consulta rápida', 'Atendida');
INSERT INTO CitaMedica VALUES (59, 21, 3, '2025-01-19', '12:00', 'Control mensual', 'Cancelada');
INSERT INTO CitaMedica VALUES (60, 17, 4, '2025-01-19', '14:00', 'Dolor de cabeza', 'Programada');

INSERT INTO CitaMedica VALUES (61, 3, 5, '2025-01-20', '08:00', 'Chequeo', 'Cancelada');
INSERT INTO CitaMedica VALUES (62, 11, 6, '2025-01-20', '08:30', 'Fiebre', 'Programada');
INSERT INTO CitaMedica VALUES (63, 8, 7, '2025-01-20', '09:00', 'Control', 'Atendida');
INSERT INTO CitaMedica VALUES (64, 6, 8, '2025-01-20', '09:30', 'Dolor de espalda', 'Programada');
INSERT INTO CitaMedica VALUES (65, 9, 9, '2025-01-20', '10:00', 'Consulta', 'Cancelada');
INSERT INTO CitaMedica VALUES (66, 12, 1, '2025-01-20', '10:30', 'Alergia', 'Programada');
INSERT INTO CitaMedica VALUES (67, 13, 2, '2025-01-20', '11:00', 'Chequeo anual', 'Atendida');
INSERT INTO CitaMedica VALUES (68, 7, 3, '2025-01-20', '11:30', 'Infección leve', 'Programada');
INSERT INTO CitaMedica VALUES (69, 15, 4, '2025-01-20', '12:00', 'Revisión', 'Atendida');
INSERT INTO CitaMedica VALUES (70, 20, 5, '2025-01-20', '14:00', 'Evaluación', 'Cancelada');

INSERT INTO CitaMedica VALUES (71, 2, 6, '2025-01-21', '08:00', 'Control general', 'Programada');
INSERT INTO CitaMedica VALUES (72, 4, 7, '2025-01-21', '08:30', 'Seguimiento', 'Cancelada');
INSERT INTO CitaMedica VALUES (73, 10, 8, '2025-01-21', '09:00', 'Chequeo extraordinario', 'Atendida');
INSERT INTO CitaMedica VALUES (74, 21, 9, '2025-01-21', '09:30', 'Dolor muscular', 'Programada');
INSERT INTO CitaMedica VALUES (75, 19, 1, '2025-01-21', '10:00', 'Infección', 'Cancelada');
INSERT INTO CitaMedica VALUES (76, 1, 2, '2025-01-21', '10:30', 'Evaluación', 'Programada');
INSERT INTO CitaMedica VALUES (77, 5, 3, '2025-01-21', '11:00', 'Molestia general', 'Atendida');
INSERT INTO CitaMedica VALUES (78, 14, 4, '2025-01-21', '11:30', 'Revisión', 'Cancelada');
INSERT INTO CitaMedica VALUES (79, 16, 5, '2025-01-21', '12:00', 'Control mensual', 'Programada');
INSERT INTO CitaMedica VALUES (80, 18, 6, '2025-01-21', '14:00', 'Problema digestivo', 'Atendida');

INSERT INTO CitaMedica VALUES (81, 7, 7, '2025-01-22', '08:00', 'Alergia', 'Programada');
INSERT INTO CitaMedica VALUES (82, 12, 8, '2025-01-22', '08:30', 'Chequeo', 'Atendida');
INSERT INTO CitaMedica VALUES (83, 17, 9, '2025-01-22', '09:00', 'Dolor abdominal', 'Cancelada');
INSERT INTO CitaMedica VALUES (84, 9, 1, '2025-01-22', '09:30', 'Control general', 'Programada');
INSERT INTO CitaMedica VALUES (85, 3, 2, '2025-01-22', '10:00', 'Fiebre', 'Cancelada');
INSERT INTO CitaMedica VALUES (86, 11, 3, '2025-01-22', '10:30', 'Problema respiratorio', 'Programada');
INSERT INTO CitaMedica VALUES (87, 20, 4, '2025-01-22', '11:00', 'Evaluación', 'Atendida');
INSERT INTO CitaMedica VALUES (88, 5, 5, '2025-01-22', '11:30', 'Chequeo general', 'Programada');
INSERT INTO CitaMedica VALUES (89, 6, 6, '2025-01-22', '12:00', 'Dolor de cabeza', 'Cancelada');
INSERT INTO CitaMedica VALUES (90, 10, 7, '2025-01-22', '14:00', 'Consulta', 'Atendida');

INSERT INTO CitaMedica VALUES (91, 13, 8, '2025-01-23', '08:00', 'Control mensual', 'Programada');
INSERT INTO CitaMedica VALUES (92, 2, 9, '2025-01-23', '08:30', 'Revisión', 'Atendida');
INSERT INTO CitaMedica VALUES (93, 8, 1, '2025-01-23', '09:00', 'Molestia estomacal', 'Cancelada');
INSERT INTO CitaMedica VALUES (94, 16, 2, '2025-01-23', '09:30', 'Chequeo general', 'Programada');
INSERT INTO CitaMedica VALUES (95, 1, 3, '2025-01-23', '10:00', 'Dolor muscular', 'Atendida');
INSERT INTO CitaMedica VALUES (96, 14, 4, '2025-01-23', '10:30', 'Evaluación', 'Cancelada');
INSERT INTO CitaMedica VALUES (97, 4, 5, '2025-01-23', '11:00', 'Chequeo', 'Atendida');
INSERT INTO CitaMedica VALUES (98, 15, 6, '2025-01-23', '11:30', 'Control', 'Programada');
INSERT INTO CitaMedica VALUES (99, 6, 7, '2025-01-23', '12:00', 'Alergia', 'Cancelada');
INSERT INTO CitaMedica VALUES (100, 9, 8, '2025-01-23', '14:00', 'Revisión general', 'Atendida');

INSERT INTO CitaMedica VALUES (101, 7, 9, '2025-01-24', '08:00', 'Control mensual', 'Cancelada');
INSERT INTO CitaMedica VALUES (102, 11, 1, '2025-01-24', '08:30', 'Chequeo', 'Programada');
INSERT INTO CitaMedica VALUES (103, 18, 2, '2025-01-24', '09:00', 'Consulta', 'Atendida');
INSERT INTO CitaMedica VALUES (104, 21, 3, '2025-01-24', '09:30', 'Revisión', 'Programada');
INSERT INTO CitaMedica VALUES (105, 20, 4, '2025-01-24', '10:00', 'Evaluación', 'Cancelada');
INSERT INTO CitaMedica VALUES (106, 3, 5, '2025-01-24', '10:30', 'Dolor abdominal', 'Atendida');
INSERT INTO CitaMedica VALUES (107, 5, 6, '2025-01-24', '11:00', 'Control general', 'Programada');
INSERT INTO CitaMedica VALUES (108, 12, 7, '2025-01-24', '11:30', 'Seguimiento', 'Cancelada');
INSERT INTO CitaMedica VALUES (109, 8, 8, '2025-01-24', '12:00', 'Chequeo', 'Programada');
INSERT INTO CitaMedica VALUES (110, 10, 9, '2025-01-24', '14:00', 'Consulta rápida', 'Atendida');

INSERT INTO CitaMedica VALUES (111, 2, 1, '2025-01-25', '08:00', 'Evaluación', 'Cancelada');
INSERT INTO CitaMedica VALUES (112, 14, 2, '2025-01-25', '08:30', 'Revisión', 'Programada');
INSERT INTO CitaMedica VALUES (113, 19, 3, '2025-01-25', '09:00', 'Dolor de cabeza', 'Atendida');
INSERT INTO CitaMedica VALUES (114, 4, 4, '2025-01-25', '09:30', 'Chequeo', 'Cancelada');
INSERT INTO CitaMedica VALUES (115, 6, 5, '2025-01-25', '10:00', 'Control mensual', 'Programada');
INSERT INTO CitaMedica VALUES (116, 9, 6, '2025-01-25', '10:30', 'Fatiga', 'Atendida');
INSERT INTO CitaMedica VALUES (117, 1, 7, '2025-01-25', '11:00', 'Consulta general', 'Cancelada');
INSERT INTO CitaMedica VALUES (118, 16, 8, '2025-01-25', '11:30', 'Alergia', 'Atendida');
INSERT INTO CitaMedica VALUES (119, 11, 9, '2025-01-25', '12:00', 'Seguimiento', 'Programada');
INSERT INTO CitaMedica VALUES (120, 7, 1, '2025-01-25', '14:00', 'Chequeo', 'Atendida');


-- INSERTS — ResultadosClinicos (30 registros)
INSERT INTO ResultadosClinicos VALUES (1,'Glucosa en ayunas','98 mg/dL','2025-01-10','Normal','Validado','2025-01-05');
INSERT INTO ResultadosClinicos VALUES (2,'Hemoglobina','14.2 g/dL','2025-01-11','Dentro de rango','Registrado','2025-01-06');
INSERT INTO ResultadosClinicos VALUES (3,'Hemoglobina','12.9 g/dL','2025-01-12','Ligeramente baja','Observado','2025-01-07');
INSERT INTO ResultadosClinicos VALUES (4,'Creatinina','0.88 mg/dL','2025-01-12','Normal','Validado','2025-01-08');
INSERT INTO ResultadosClinicos VALUES (5,'Colesterol total','185 mg/dL','2025-01-13','Dentro de límites','Registrado','2025-01-09');
INSERT INTO ResultadosClinicos VALUES (6,'Triglicéridos','156 mg/dL','2025-01-13','Ligeramente elevado','Observado','2025-01-10');
INSERT INTO ResultadosClinicos VALUES (7,'Glucosa en ayunas','112 mg/dL','2025-01-14','Alto','Observado','2025-01-11');
INSERT INTO ResultadosClinicos VALUES (8,'Hemoglobina','15.0 g/dL','2025-01-15','Normal','Validado','2025-01-12');
INSERT INTO ResultadosClinicos VALUES (9,'Colesterol HDL','42 mg/dL','2025-01-15','Valor bajo','Observado','2025-01-13');
INSERT INTO ResultadosClinicos VALUES (10,'Colesterol LDL','129 mg/dL','2025-01-16','Normal','Registrado','2025-01-14');
INSERT INTO ResultadosClinicos VALUES (11,'Examen orina - Proteínas','Negativo','2025-01-16','Normal','Validado','2025-01-15');
INSERT INTO ResultadosClinicos VALUES (12,'Examen orina - Glucosa','Negativo','2025-01-17','Normal','Registrado','2025-01-16');
INSERT INTO ResultadosClinicos VALUES (13,'Hemoglobina','13.5 g/dL','2025-01-17','Normal','Validado','2025-01-17');
INSERT INTO ResultadosClinicos VALUES (14,'Triglicéridos','210 mg/dL','2025-01-18','Alto','Observado','2025-01-18');
INSERT INTO ResultadosClinicos VALUES (15,'TSH','2.1 µIU/mL','2025-01-18','Normal','Registrado','2025-01-19');
INSERT INTO ResultadosClinicos VALUES (16,'Hemoglobina','16.1 g/dL','2025-01-19','Alta pero aceptable','Validado','2025-01-20');
INSERT INTO ResultadosClinicos VALUES (17,'Creatinina','1.21 mg/dL','2025-01-19','Límite alto','Observado','2025-01-21');
INSERT INTO ResultadosClinicos VALUES (18,'COVID - Antígeno','Negativo','2025-01-20','Sin infección','Validado','2025-01-22');
INSERT INTO ResultadosClinicos VALUES (19,'COVID - Antígeno','Positivo','2025-01-20','Requiere aislamiento','Observado','2025-01-23');
INSERT INTO ResultadosClinicos VALUES (20,'Glucosa en ayunas','87 mg/dL','2025-01-21','Normal','Validado','2025-01-24');
INSERT INTO ResultadosClinicos VALUES (21,'Colesterol total','205 mg/dL','2025-01-21','Límite alto','Registrado','2025-01-25');
INSERT INTO ResultadosClinicos VALUES (22,'Hemoglobina','11.9 g/dL','2025-01-22','Baja','Observado','2025-01-26');
INSERT INTO ResultadosClinicos VALUES (23,'Examen orina - Densidad','1.020','2025-01-22','Normal','Validado','2025-01-27');
INSERT INTO ResultadosClinicos VALUES (24,'Examen orina - Sangre','Negativo','2025-01-23','Normal','Registrado','2025-01-28');
INSERT INTO ResultadosClinicos VALUES (25,'Glucosa postprandial','145 mg/dL','2025-01-23','Ligeramente elevada','Observado','2025-01-29');
INSERT INTO ResultadosClinicos VALUES (26,'Colesterol LDL','99 mg/dL','2025-01-24','Normal','Validado','2025-01-30');
INSERT INTO ResultadosClinicos VALUES (27,'Triglicéridos','132 mg/dL','2025-01-24','Normal','Registrado','2025-01-31');
INSERT INTO ResultadosClinicos VALUES (28,'Hemoglobina','13.9 g/dL','2025-01-25','Normal','Validado','2025-02-01');
INSERT INTO ResultadosClinicos VALUES (29,'Creatinina','0.76 mg/dL','2025-01-25','Normal','Validado','2025-02-02');
INSERT INTO ResultadosClinicos VALUES (30,'Glucosa en ayunas','109 mg/dL','2025-01-26','Límite alto','Observado','2025-02-03');


---------------------------------------- PROCEDURE CITAS MEDICAS ----------------------------------------
create procedure pr_insertar_citamedica
	@IdPaciente int,
	@IdMedico int,
	@FechaCita date,
	@HoraCita time,
	@Motivo varchar(200),
	@Estado varchar(20),
	@xIdCitaMedica int out
as
begin

	declare @xCitaActiva int

	select @xCitaActiva = count(*) from CitaMedica where IdPaciente = @IdPaciente AND Estado = 'Programada';

	-- validacion para que no tenga citas medicas registradas previamente
	if(@xCitaActiva > 0)
	begin
		raiserror('ya tiene una cita programada, no se puede agendar otra cita más', 16, 1)
        return;
	end

	insert into CitaMedica ([IdPaciente], [IdMedico], [FechaCita], [HoraCita], [Motivo], [Estado])
	values (@IdPaciente, @IdMedico, @FechaCita, @HoraCita, @Motivo, @Estado)

	set @xIdCitaMedica = SCOPE_IDENTITY()

end


create procedure pr_actualizar_estado_citamedica
	@IdCitaMedica int,
	@Estado varchar(20)
as
begin
	update CitaMedica set
	Estado = @Estado
	where id = @IdCitaMedica
end


create procedure pr_listar_historial_citasmedicas
	@IdPaciente int
as
begin
	select top 3 cit.Id, cit.FechaCita, cit.Estado, cit.Motivo, 
	pac.Nombre as NombrePaciente, med.Nombre as NombreMedico from CitaMedica cit
	left join Paciente pac on pac.Id = cit.IdPaciente
	left join Medico med on med.Id = cit.IdMedico
	where pac.Id = @IdPaciente 
	order by cit.Id desc
end


create procedure pr_consultar_estado_ultimos_resultados
	@IdPaciente int
as
begin
	select top 3 IdResultadosClinicos, res.TipoExamen, res.Valor, res.Observaciones, 
	res.Estado, res.FechaRegistro, pac.Nombre from ResultadosClinicos res
	left join CitaMedica cit on cit.Id = res.IdCitaMedica
	left join Paciente pac on pac.Id = res.IdCitaMedica
	where cit.IdPaciente = @IdPaciente order by res.IdResultadosClinicos desc
end

create procedure pr_informacion_citamedica
	@IdCitaMedica int
as
begin
	select Id, IdPaciente, IdMedico, FechaCita, HoraCita,
	Motivo, Estado from CitaMedica
	where Id = @IdCitaMedica
end
----------------------------------------	PROCEDURE VALIDACION PACIENTE	----------------------------------------
create procedure pr_validar_existencia_paciente
	@DniPaciente varchar(8)
as
begin
	select Id, DNI, Nombre, Sexo, FechaNacimiento, 
	Telefono, Correo, Direccion from Paciente
	where DNI = @DniPaciente
end