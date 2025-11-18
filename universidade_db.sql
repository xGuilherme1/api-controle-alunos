CREATE DATABASE universidade_db;
USE universidade_db;

CREATE TABLE Curso (
    id_cur INT AUTO_INCREMENT PRIMARY KEY,
    nome_cur VARCHAR(100) NOT NULL,
    cargaHoraria_cur INT NOT NULL
);

CREATE TABLE Turma (
    id_tur INT AUTO_INCREMENT PRIMARY KEY,
    nome_tur VARCHAR(50) NOT NULL,
    ano_tur INT NOT NULL
);

CREATE TABLE Endereco (
    id_end INT AUTO_INCREMENT PRIMARY KEY,
    logradouro_end VARCHAR(150) NOT NULL,
    numero_end VARCHAR(20),
    cidade_end VARCHAR(100) NOT NULL,
    estado_end VARCHAR (50) NOT NULL
);

CREATE TABLE Aluno(
	id_alu INT AUTO_INCREMENT PRIMARY KEY,
	nome_alu VARCHAR(150) NOT NULL,
	data_nascimento_alu DATETIME NOT NULL,
	id_curso_fk INT NOT NULL,
	id_turma_fk INT NOT NULL,
	id_endereco_fk INT NOT NULL,
    FOREIGN KEY (id_curso_fk) REFERENCES Curso(id_cur),
    FOREIGN KEY (id_turma_fk) REFERENCES Turma(id_tur),
    FOREIGN KEY (id_endereco_fk) REFERENCES Endereco(id_end)
);

CREATE TABLE Disciplina (
    id_dis INT AUTO_INCREMENT PRIMARY KEY,
    nome_dis VARCHAR(100) NOT NULL,
    professor_dis VARCHAR(150) NOT NULL
);

CREATE TABLE AlunoDisciplina (
    alunoId INT NOT NULL,
    disciplinaId INT NOT NULL,
    Nota decimal,
    PRIMARY KEY (alunoId, disciplinaId),
    FOREIGN KEY (alunoId) REFERENCES Aluno(id_alu),
    FOREIGN KEY (disciplinaId) REFERENCES Disciplina(id_dis)
);