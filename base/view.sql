CREATE OR REPLACE VIEW infoFilm AS 
SELECT
    f.idFilm,
    f.titre,
    f.categorieId,
    f.duree,
    f.image,
    f.classificationId,
    c.categorie,
    cl.classification
FROM film f JOIN categorie c ON f.categorieId=c.idCategorie
JOIN classification cl ON f.classificationId=cl.idClasse;


CREATE OR REPLACE VIEW diffusionFilm AS 
SELECT 
    d.idDiffusion,
    d.salleId,
    s.nomSalle,
    d.filmId,
    d.plageHoraireDebut,
    d.plageHoraireFin,
    f.titre,
    f.categorieId,
    f.duree,
    f.image,
    f.classificationId,
    c.categorie,
    cl.classification
FROM diffusion d JOIN salle s ON d.salleId=s.idSalle
JOIN film f ON d.filmId=f.idFilm
JOIN categorie c ON f.categorieId=c.idCategorie
JOIN classification cl ON f.classificationId=cl.idClasse;



-- CREATE OR REPLACE VIEW placesFilm AS 
-- SELECT 
--     d.idDiffusion,
--     f.titre AS filmTitre,
--     s.nomSalle,
--     sp.nomColonne,
--     sp.nbLignes,
--     sp.nbPlaces,
--     p.numPlace,
--     r.idReservation,
--     r.etatPlace,
--     r.dateReservation
-- FROM diffusion d JOIN salle s ON d.salleId=s.idSalle
-- JOIN film f ON d.filmId=f.idFilm
-- JOIN categorie c ON f.categorieId=c.idCategorie
-- JOIN classification cl ON f.classificationId=cl.idClasse
-- JOIN sallePlaces sp ON s.idSalle = sp.salleId
-- JOIN places p ON sp.idSallePlaces = p.sallePlaceId
-- JOIN reservation r ON r.diffusionId = d.idDiffusion;




CREATE OR REPLACE VIEW placesFilm AS 
SELECT 
    d.idDiffusion,
    r.idReservation,
    d.salleId,
    d.filmId,
    r.etatPlace,
    r.dateReservation,
    p.numPlace,
    sp.nomColonne,
    sp.nbLignes,
    sp.nbPlaces,
    s.nomSalle

FROM diffusion d
LEFT JOIN reservation r ON r.diffusionId=d.idDiffusion
JOIN film f ON d.filmId=f.idFilm
RIGHT JOIN places p ON r.placeId=p.numPlace
JOIN salleplaces sp ON sp.salleId=p.sallePlaceId
JOIN salle s On sp.salleId=s.idSalle;



CREATE OR REPLACE VIEW infoSalle AS
SELECT
    s.nomSalle,
    s.ouverture,
    s.fermeture,
    sp.nomColonne,
    sp.nbLignes,
    sp.nbPlaces
FROM salleplaces sp
JOIN salle s ON sp.salleId=s.idSalle;


CREATE OR REPLACE VIEW placesDispo AS
SELECT
    s.idSallePlaces,
    s.salleId,
    s.nomColonne,
    s.nbLignes,
    s.nbPlaces,
    p.numPlace
FROM places p
JOIN sallePlaces s ON s.nomColonne=p.colonne;


CREATE OR REPLACE VIEW listeReserve AS
SELECT
    u.idUser,
    u.name,
    r.idReservation,
    r.etatPlace, -- Supprimé le point final ici
    r.diffusionId,
    d.plageHoraireDebut,
    d.plageHoraireFin,
    d.filmId,
    d.salleId,
    f.titre,
    f.categorieId,
    s.nomSalle
FROM reservation r
JOIN user u ON r.userId = u.idUser
JOIN diffusion d ON d.idDiffusion = r.diffusionId
JOIN film f ON f.idFilm = d.filmId
JOIN categorie c ON c.idCategorie = f.categorieId
JOIN salle s ON s.idSalle = d.salleId;
