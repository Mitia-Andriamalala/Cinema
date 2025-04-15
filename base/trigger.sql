DELIMITER //

CREATE TRIGGER keyUser
BEFORE INSERT ON user
FOR EACH ROW
BEGIN
    DECLARE idSetter VARCHAR(20);
    SET idSetter = CONCAT('USE', LPAD(COALESCE((SELECT COUNT(*) + 1 FROM user), 1), 5, '0'));
    SET NEW.idUser = idSetter;
END //

CREATE TRIGGER keySalle
BEFORE INSERT ON salle
FOR EACH ROW
BEGIN
    DECLARE idSetter VARCHAR(20);
    SET idSetter = CONCAT('SLE', LPAD(COALESCE((SELECT COUNT(*) + 1 FROM salle), 1), 5, '0'));
    SET NEW.idSalle = idSetter;
END //


CREATE TRIGGER keySallePlaces
BEFORE INSERT ON sallePlaces
FOR EACH ROW
BEGIN
    DECLARE idSetter VARCHAR(20);
    SET idSetter = CONCAT('SLP', LPAD(COALESCE((SELECT COUNT(*) + 1 FROM sallePlaces), 1), 5, '0'));
    SET NEW.idSallePlaces = idSetter;
END //


CREATE TRIGGER keySallePlaces
BEFORE INSERT ON sallePlaces
FOR EACH ROW
BEGIN
    DECLARE idSetter VARCHAR(20);
    SET idSetter = CONCAT('SLP', LPAD(COALESCE((SELECT COUNT(*) + 1 FROM sallePlaces), 1), 5, '0'));
    SET NEW.idSallePlaces = idSetter;
END //


CREATE TRIGGER keyCategorie
BEFORE INSERT ON categorie
FOR EACH ROW
BEGIN
    DECLARE idSetter VARCHAR(20);
    SET idSetter = CONCAT('CAT', LPAD(COALESCE((SELECT COUNT(*) + 1 FROM categorie), 1), 5, '0'));
    SET NEW.idCategorie = idSetter;
END //


CREATE TRIGGER keyClassification
BEFORE INSERT ON classification
FOR EACH ROW
BEGIN
    DECLARE idSetter VARCHAR(20);
    SET idSetter = CONCAT('CLA', LPAD(COALESCE((SELECT COUNT(*) + 1 FROM classification), 1), 5, '0'));
    SET NEW.idClasse = idSetter;
END //


CREATE TRIGGER keyFilm
BEFORE INSERT ON film
FOR EACH ROW
BEGIN
    DECLARE idSetter VARCHAR(20);
    SET idSetter = CONCAT('FLM', LPAD(COALESCE((SELECT COUNT(*) + 1 FROM film), 1), 5, '0'));
    SET NEW.idFilm = idSetter;
END //


CREATE TRIGGER keyDiffusion
BEFORE INSERT ON diffusion
FOR EACH ROW
BEGIN
    DECLARE idSetter VARCHAR(20);
    SET idSetter = CONCAT('DIF', LPAD(COALESCE((SELECT COUNT(*) + 1 FROM diffusion), 1), 5, '0'));
    SET NEW.idDiffusion = idSetter;
END //


CREATE OR REPLACE TRIGGER keyPlaces
BEFORE INSERT ON places
FOR EACH ROW
BEGIN
    DECLARE placeCount INT;
    DECLARE idSetter VARCHAR(50);
    DECLARE dernierNumeroPlace VARCHAR(50);

    -- Trouver le dernier numéro de place existant pour la même sallePlaceId et colonne
    SELECT numPlace
    INTO dernierNumeroPlace
    FROM places p
    WHERE p.sallePlaceId = NEW.sallePlaceId AND p.colonne = NEW.colonne
    ORDER BY numPlace DESC
    LIMIT 1;

    -- Si il y a déjà des places, on incrémente le dernier numéro trouvé
    IF dernierNumeroPlace IS NOT NULL THEN
        SET placeCount = CAST(SUBSTRING_INDEX(dernierNumeroPlace, '-', -1) AS UNSIGNED) + 1;
    ELSE
        -- Sinon, commencer avec 1 si aucune place n'existe encore
        SET placeCount = 1;
    END IF;

    -- Concaténer sallePlaceId, colonne et le nouveau numéro de place formaté
    SET idSetter = CONCAT(NEW.sallePlaceId, '-', NEW.colonne, '-', LPAD(placeCount, 5, '0'));

    -- Affecter la valeur générée à numPlace
    SET NEW.numPlace = idSetter;
END//


CREATE TRIGGER keyReservation
BEFORE INSERT ON reservation
FOR EACH ROW
BEGIN
    DECLARE idSetter VARCHAR(20);
    SET idSetter = CONCAT('RES', LPAD(COALESCE((SELECT COUNT(*) + 1 FROM reservation), 1), 5, '0'));
    SET NEW.idReservation = idSetter;
END //

DELIMITER ;