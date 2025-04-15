
INSERT INTO user(name) VALUES
('rakoto'),
('jean');
-- Table salle
INSERT INTO salle (nomSalle) VALUES 
('Salle 1'),
 ('Salle 2');

-- -- Table sallePlaces
INSERT INTO sallePlaces (salleId, nomColonne, nbLignes, nbPlaces) VALUES 
('SLE00001', 'A', 10, 20),
('SLE00001', 'C', 10, 20),
-('SLE00002', 'B', 8, 16);

-- -- Table categorie
 INSERT INTO categorie (categorie) VALUES 
 ('Action'),
 ('Comedie'),
- ('Drame');

-- -- Table classification
 INSERT INTO classification (classification) VALUES 
 ('Tout public'),
 ('-12 ans'),
 ('-16 ans');

-- -- Table film
INSERT INTO film (titre, categorieId, duree, classificationId) VALUES 
 ('Film Action 1', 'CAT00001', '01:30:00', 'CLA00001'),
 ('Film Comédie 1', 'CAT00002', '02:00:00', 'CLA00002'),
 ('Film Drame 1', 'CAT00003', '01:45:00', 'CLA00003');

-- -- Table diffusion
 INSERT INTO diffusion (salleId, filmId, plageHoraireDebut, plageHoraireFin) VALUES 
('SLE00001', 'FLM00001', '2024-12-07 14:00:00', '2024-12-07 15:30:00'),
 ('SLE00002', 'FLM00002', '2024-12-07 16:00:00', '2024-12-07 18:00:00');

-- -- Table places
INSERT INTO places (sallePlaceId) VALUES 
 ('SLP00001'),
('SLP00001'),
('SLP00002'),
('SLP00002');

MariaDB [cinema]> desc reservation;
+-----------------+-------------+------+-----+---------+-------+
| Field           | Type        | Null | Key | Default | Extra |
+-----------------+-------------+------+-----+---------+-------+
| idReservation   | varchar(20) | NO   | PRI | NULL    |       |
| placeId         | varchar(50) | YES  |     | NULL    |       |
| etatPlace       | int(11)     | YES  |     | NULL    |       |
| diffusionId     | varchar(20) | YES  |     | NULL    |       |
| dateReservation | datetime    | YES  |     | NULL    |       |
+-----------------+-------------+------+-----+---------+-------+

-- Table reservation
INSERT INTO reservation (placeId, etatPlace, diffusionId, dateReservation) VALUES 
('SLE00001-s1col-00001', 1, 'DIF00001', '2024-12-08 10:00:00'),
('SLE00001-s1col-00004', 2, 'DIF00001', '2024-12-08 10:30:00');

