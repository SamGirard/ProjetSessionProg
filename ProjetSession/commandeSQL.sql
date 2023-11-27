-----------------------------CRÉATION DES TABLES (fait par sam)-------------------------------
CREATE TABLE employe(
    matricule VARCHAR(10) UNIQUE PRIMARY KEY,
    nom VARCHAR(50),
    prenom VARCHAR(50),
    date_naissance DATE,
    email VARCHAR(150),
    adresse VARCHAR(100),
    date_embauche DATE,
    taux DOUBLE,
    photo VARCHAR(1000),
    statut ENUM('Journalier', 'Permanent')
    );

CREATE TABLE client(
    id_client VARCHAR(3) UNIQUE PRIMARY KEY,
    nom VARCHAR(50),
    adresse VARCHAR(100),
    numero_tel VARCHAR(30),
    email VARCHAR(150)
);

CREATE TABLE projet(
    id_projet VARCHAR(11) PRIMARY KEY UNIQUE,
    titre VARCHAR(100),
    date_debut DATE,
    description VARCHAR(255),
    budget DOUBLE,
    nb_employe INT,
    salaireTotal DOUBLE,
    id_client VARCHAR(3),
    statut ENUM('Terminé', 'En cours'),
    CHECK (nb_employe <= 5),
    FOREIGN KEY projet(id_client) REFERENCES client(id_client)
);

CREATE TABLE admin(
    utilisateur VARCHAR(15),
    motDePasse VARCHAR(255) UNIQUE,
);

alter table admin Add COLUMN estConnecter BOOL DEFAULT(false);
-----------------------------TRIGGER (fait par sam)---------------------------------


/*Trigger pour l'identifiant du client*/
DELIMITER  //
CREATE TRIGGER identifiantClient BEFORE INSERT
    on client
    FOR EACH ROW
BEGIN
    SET NEW.id_client = FLOOR(rand()*899) + 100;
end //
DELIMITER ;

/*Trigger pour le numéro de projet*/
DELIMITER  //
CREATE TRIGGER numeroProjet BEFORE INSERT
    on projet
    FOR EACH ROW
BEGIN
    SET NEW.id_projet = CONCAT(UPPER(NEW.id_client), '-', FLOOR(rand()*89) + 10, '-', YEAR(NEW.date_debut));
end //
DELIMITER ;

/*Trigger pour le numéro de projet*/
DELIMITER  //
CREATE TRIGGER numeroProjet BEFORE INSERT
    on projet
    FOR EACH ROW
    BEGIN
        SET NEW.numero = CONCAT(UPPER(NEW.client), '-', FLOOR(rand()*89) + 10, '-', YEAR(NEW.date_debut));
    end //
DELIMITER ;

-----------------------------Procédures (fait par isaac)---------------------------------
DELIMITER //
CREATE PROCEDURE p_ajout_employe(IN nom VARCHAR(20), IN prenom VARCHAR(20), IN dateNaiss DATE, IN email VARCHAR(150), IN adresse VARCHAR(100), IN date_embauche DATE, IN taux DOUBLE, IN photo VARCHAR(1000), IN statut VARCHAR(20))
BEGIN
    INSERT into employe VALUES(null, nom, prenom, dateNaiss, email, adresse, date_embauche, taux, photo, statut);
end //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE p_ajout_client(IN nom VARCHAR(50), IN adresse VARCHAR(100), IN numero_tel VARCHAR(30), IN email VARCHAR(150))
BEGIN
    INSERT into client VALUES(null, nom, adresse, numero_tel, email);
end //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE p_ajout_projet(IN titre VARCHAR(50), IN date_debut DATE, IN description VARCHAR(255), IN budget DOUBLE, IN nbEmpl INT, IN salairTot DOUBLE, IN id_client VARCHAR(3), IN statut VARCHAR(20))
BEGIN
    INSERT into client VALUES(null, titre, date_debut, description, budget, nbEmpl, salairTot, id_client, statut);
end //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE afficher_projets()
BEGIN
    SELECT * FROM projet;
end //
DELIMITER ;
-----------------------------INSERTION DE DONNÉE (fait par sam)-----------------------------

INSERT INTO client VALUES (null, 'Jean Lamontage', '47 rue bouol', '819-555-4443', 'email@email.xom');

INSERT INTO projet VALUES (null, 'test', '2020-12-19', 'Descpription', 20000, 4, 15000, '811', 'Terminé');

INSERT INTO admin VALUES ('admin', 'admin');


INSERT INTO client VALUES(null, 'John Doe', '123 Rue de la Fiction', '555-1234', 'john.doe@email.com');
INSERT INTO client VALUES(null, 'Jane Smith', '456 Avenue de l\'Imagination', '555-5678', 'jane.smith@email.com');
INSERT INTO client VALUES(null, 'Bob Johnson', '789 Boulevard de l\'Illusion', '555-9012', 'bob.johnson@email.com');
INSERT INTO client VALUES(null, 'Alice Brown', '101 Rue de l\'Évasion', '555-3456', 'alice.brown@email.com');
INSERT INTO client VALUES(null, 'Charlie Davis', '202 Avenue du Rêve', '555-7890', 'charlie.davis@email.com');
INSERT INTO client VALUES(null, 'Émilie Martin', '234 Rue de l\'Imagination', '555-1234', 'emilie.martin@email.com');
INSERT INTO client VALUES(null, 'François Dupont', '567 Avenue de la Fiction', '555-5678', 'francois.dupont@email.com');
INSERT INTO client VALUES(null, 'Sophie Tremblay', '890 Boulevard du Rêve', '555-9012', 'sophie.tremblay@email.com');
INSERT INTO client VALUES(null, 'Pierre Lefevre', '111 Rue de l\'Évasion', '555-3456', 'pierre.lefevre@email.com');
INSERT INTO client VALUES(null, 'Isabelle Girard', '222 Avenue de l\'Illusion', '555-7890', 'isabelle.girard@email.com');
INSERT INTO client VALUES(null, 'Alexandre Dubois', '345 Rue de l\'Évasion', '555-1234', 'alexandre.dubois@email.com');
INSERT INTO client VALUES(null, 'Caroline Berger', '678 Avenue du Rêve', '555-5678', 'caroline.berger@email.com');
INSERT INTO client VALUES(null, 'David Lambert', '901 Boulevard de l\'Illusion', '555-9012', 'david.lambert@email.com');
INSERT INTO client VALUES(null, 'Élodie Gagnon', '222 Rue de la Fiction', '555-3456', 'elodie.gagnon@email.com');
INSERT INTO client VALUES(null, 'Gabriel Richard', '333 Avenue de l\'Imagination', '555-7890', 'gabriel.richard@email.com');


