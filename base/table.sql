DROP DATABASE cinema;
CREATE DATABASE cinema;
USE cinema;

CREATE TABLE user
(
    idUser VARCHAR(20) PRIMARY KEY,
    name VARCHAR(100)
);

CREATE TABLE salle
(
    idSalle VARCHAR(20) PRIMARY KEY,
    nomSalle VARCHAR(50),
    ouverture Time,
    fermeture Time
);

CREATE TABLE sallePlaces
(
    idSallePlaces VARCHAR(20) PRIMARY KEY,
    salleId VARCHAR(20),
    nomColonne VARCHAR(50),
    nbLignes INT,
    nbPlaces INT
);

CREATE TABLE places
(
    sallePlaceId VARCHAR(20),
    colonne VARCHAR(20),
    numPlace VARCHAR(50)
);


CREATE TABLE categorie
(
    idCategorie VARCHAR(20) PRIMARY KEY,
    categorie VARCHAR(50)
);

CREATE TABLE classification
(
    idClasse VARCHAR(20) PRIMARY KEY,
    classification VARCHAR(30)
);

CREATE TABLE film
(
    idFilm VARCHAR(20) PRIMARY KEY,
    titre VARCHAR(150),
    image BLOB,
    categorieId VARCHAR(20),
    duree Time,
    classificationId VARCHAR(20)
);

CREATE TABLE diffusion
(
    idDiffusion VARCHAR(20) PRIMARY KEY,
    salleId VARCHAR(20),
    filmId VARCHAR(20),
    plageHoraireDebut DATETIME,
    plageHoraireFin DATETIME
);

CREATE TABLE reservation
(
    idReservation VARCHAR(20) PRIMARY KEY,
    placeId VARCHAR(50),
    etatPlace INT,
    diffusionId VARCHAR(20),
    userId VARCHAR(20),
    dateReservation DATETIME
);