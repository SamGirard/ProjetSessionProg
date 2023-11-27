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


INSERT INTO projet VALUES(null, 'Projet Alpha', '2023-01-15', 'Développement d\'une application mobile innovante', 10000, 3, null, 464, 'En cours');
INSERT INTO projet VALUES(null, 'Projet Beta', '2023-02-20', 'Conception d\'un système de gestion de base de données avancé', 15000, 4, null, 961, 'Terminé');
INSERT INTO projet VALUES(null, 'Projet Gamma', '2023-03-10', 'Création d\'un site web interactif pour une entreprise locale', 20000, 2, null, 961, 'En cours');
INSERT INTO projet VALUES(null, 'Projet Delta', '2023-04-05', 'Déploiement d\'un réseau de capteurs pour la surveillance environnementale', 12000, 5, null, 507, 'Terminé');
INSERT INTO projet VALUES(null, 'Projet Epsilon', '2023-05-12', 'Développement d\'un algorithme d\'intelligence artificielle pour l\'analyse de données', 18000, 1, null, 176, 'En cours');
INSERT INTO projet VALUES(null, 'Projet Visionnaire', '2023-01-15', 'Développement d\'une technologie révolutionnaire pour la réalité augmentée', 100000, 3, null, 811, 'En cours');
INSERT INTO projet VALUES(null, 'Projet Horizon', '2023-02-20', 'Construction d\'un centre de recherche spatial pour l\'exploration interplanétaire', 500000, 4, null, 344, 'Terminé');
INSERT INTO projet VALUES(null, 'Projet Renaissance', '2023-03-10', 'Restauration numérique de trésors artistiques mondiaux', 75000, 2, null, 262, 'En cours');
INSERT INTO projet VALUES(null, 'Projet Éco-Innovant', '2023-04-05', 'Développement d\'une solution durable pour la gestion des déchets', 120000, 5, null, 518, 'Terminé');
INSERT INTO projet VALUES(null, 'Projet Quantum', '2023-05-12', 'Recherche avancée en informatique quantique pour la cryptographie', 200000, 1, null, 835, 'En cours');
INSERT INTO projet VALUES(null, 'Projet FusionTech', '2023-01-15', 'Intégration de technologies émergentes pour la transformation numérique', 80000, 3, null, 492, 'En cours');
INSERT INTO projet VALUES(null, 'Projet Solaris', '2023-02-20', 'Développement d\'une solution d\'énergie solaire intelligente', 120000, 4, null, 433, 'Terminé');
INSERT INTO projet VALUES(null, 'Projet GreenCity', '2023-03-10', 'Planification et mise en œuvre d\'infrastructures durables pour les villes', 150000, 2, null, 809, 'En cours');
INSERT INTO projet VALUES(null, 'Projet AquaNet', '2023-04-05', 'Création d\'un réseau de surveillance des ressources marines', 100000, 5, null, 262, 'Terminé');
INSERT INTO projet VALUES(null, 'Projet QuantumSys', '2023-05-12', 'Développement d\'un système de traitement quantique pour l\'analyse de données complexes', 180000, 1, null, 961, 'En cours');
