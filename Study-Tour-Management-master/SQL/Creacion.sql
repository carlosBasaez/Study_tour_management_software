CREATE TABLE usuario (
  rut VARCHAR2(10) PRIMARY KEY,
  email VARCHAR2(100) NOT NULL,
  pass VARCHAR2(100) NOT NULL,
  nombre VARCHAR2(100) NOT NULL,
  apellido VARCHAR2(100) NOT NULL
);

CREATE TABLE dueño(
  rutDueño VARCHAR2(10) PRIMARY KEY,
  CONSTRAINT fk_dueño FOREIGN KEY (rutDueño) REFERENCES usuario (rut)
);

CREATE TABLE ejecutivo_venta(
  rutEjecutivoVenta VARCHAR2(10) PRIMARY KEY,
  CONSTRAINT fk_ejecutivo_venta FOREIGN KEY (rutEjecutivoVenta) REFERENCES usuario (rut)
);

CREATE TABLE apoderado(
  rutApoderado VARCHAR2(10) PRIMARY KEY,
  CONSTRAINT fk_apoderado FOREIGN KEY (rutApoderado) REFERENCES usuario (rut)
);

CREATE TABLE representante(
  rutRepresentante VARCHAR2(10) PRIMARY KEY,
  CONSTRAINT fk_representante FOREIGN KEY (rutRepresentante) REFERENCES usuario (rut)
);

CREATE TABLE alumno(
  rutAlumno VARCHAR2(10) NOT NULL,
  rutApoderado VARCHAR2(10) NOT NULL,
  nombre VARCHAR2(100) NOT NULL,
  apellido VARCHAR2(100) NOT NULL,
  CONSTRAINT fk_alumno FOREIGN KEY (rutApoderado) REFERENCES apoderado(rutApoderado),
  CONSTRAINT pk_alumno PRIMARY KEY (rutAlumno, rutApoderado)
);

CREATE TABLE region (
  idRegion INTEGER PRIMARY KEY,
  nombre VARCHAR2(100) NOT NULL
);

CREATE TABLE comuna(
  idComuna INTEGER PRIMARY KEY, 
  nombre VARCHAR2(100) NOT NULL,
  idRegion INTEGER NOT NULL,
  CONSTRAINT fk_comuna FOREIGN KEY (idRegion) REFERENCES region(idRegion)
);

CREATE TABLE colegio(
  idColegio INTEGER PRIMARY KEY,
  nombre VARCHAR2(200) NOT NULL,
  direccion VARCHAR2(200) NOT NULL,
  idComuna INTEGER NOT NULL,
  CONSTRAINT fk_colegio FOREIGN KEY (idComuna) REFERENCES comuna(idComuna)
);

CREATE TABLE curso(
  idCurso INTEGER PRIMARY KEY,
  nombre VARCHAR2(100) NOT NULL,
  rutRepresentante VARCHAR2(10) NOT NULL,
  idColegio INTEGER NOT NULL,
  CONSTRAINT fk_curso_colegio FOREIGN KEY (idColegio) REFERENCES colegio(idColegio),
  CONSTRAINT fk_curso_representante FOREIGN KEY (rutRepresentante) REFERENCES representante(rutRepresentante)
);

CREATE TABLE tipo_documento (
  idTipoDoc INTEGER PRIMARY KEY,
  nombre VARCHAR2(100) NOT NULL,
  descripcion VARCHAR2(500)
);

CREATE TABLE plan_viaje(
  idPlanViaje INTEGER PRIMARY KEY,
  nombre VARCHAR2(100) NOT NULL,
  descripcion VARCHAR2(500) NOT NULL
);

CREATE TABLE contrato(
  idContrato INTEGER PRIMARY KEY,
  idCurso INTEGER NOT NULL,
  montoObjetivo INTEGER NOT NULL,
  fechaCreacion DATE NOT NULL,
  fechaViaje DATE NOT NULL,
  observaciones VARCHAR2(1000) NULL,
  idPlanViaje INTEGER NOT NULL,
  CONSTRAINT fk_contrato_curso FOREIGN KEY (idCurso) REFERENCES curso(idCurso),
  CONSTRAINT fk_contrato_planviaje FOREIGN KEY (idPlanViaje) REFERENCES plan_viaje(idPlanViaje)
);

CREATE TABLE documento(
  idDocumento INTEGER PRIMARY KEY,
  archivo CLOB DEFAULT EMPTY_CLOB(),
  idTipoDoc INTEGER NOT NULL,
  idContrato INTEGER NOT NULL,
  CONSTRAINT fk_documento_tipo FOREIGN KEY (idTipoDoc) REFERENCES tipo_documento(idTipoDoc),
  CONSTRAINT fk_documento_contrato FOREIGN KEY (idContrato) REFERENCES contrato(idContrato)
);

CREATE TABLE cuenta(
  idCuenta INTEGER PRIMARY KEY,
  rutAlumno VARCHAR2(10) NOT NULL,
  rutApoderado VARCHAR2(10) NOT NULL,
  idCurso INTEGER NOT NULL,
  monto INTEGER NOT NULL,
  CONSTRAINT fk_cuenta_alumno FOREIGN KEY (rutAlumno, rutApoderado) REFERENCES alumno(rutAlumno, rutApoderado),
  CONSTRAINT fk_cuenta_curso FOREIGN KEY (idCurso) REFERENCES curso(idCurso)
);

CREATE TABLE actividad(
  idActividad INTEGER PRIMARY KEY,
  idCurso INTEGER NOT NULL,
  nombre VARCHAR2(100) NOT NULL,
  descripcion VARCHAR2(500) NOT NULL,
  recaudado INTEGER NOT NULL,
  CONSTRAINT fk_actividad_curso FOREIGN KEY (idCurso) REFERENCES curso(idCurso)
);

CREATE TABLE actividad_cuenta(
  idActividad INTEGER NOT NULL,
  idCuenta INTEGER NOT NULL,
  CONSTRAINT pk_actividad_cuenta PRIMARY KEY (idActividad, idCuenta),
  CONSTRAINT fk_ac_actividad FOREIGN KEY (idActividad) REFERENCES actividad(idActividad),
  CONSTRAINT fk_ac_cuenta FOREIGN KEY (idCuenta) REFERENCES cuenta(idCuenta)
);





