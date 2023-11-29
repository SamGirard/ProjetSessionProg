-----------------------------CRÉATION DES TABLES (fait par sam)-------------------------------
CREATE TABLE client(
    id_client VARCHAR(3) UNIQUE PRIMARY KEY,
    nom VARCHAR(50),
    adresse VARCHAR(100),
    numero_tel VARCHAR(30),
    email VARCHAR(150)
);

CREATE TABLE projet
(
    id_projet    VARCHAR(11) PRIMARY KEY UNIQUE,
    titre        VARCHAR(100),
    date_debut   DATE,
    description  VARCHAR(255),
    budget       DOUBLE,
    nb_employe   INT,
    salaireTotal DOUBLE,
    id_client    VARCHAR(3),
    statut       ENUM ('Terminé', 'En cours'),
    CHECK (nb_employe <= 5),
    FOREIGN KEY projet (id_client) REFERENCES client (id_client)
);

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
    id_projet VARCHAR(11),
    statut ENUM('Journalier', 'Permanent'),
    FOREIGN KEY employe(id_projet) REFERENCES projet(id_projet)
    );


CREATE TABLE admin(
    utilisateur VARCHAR(15),
    motDePasse VARCHAR(255) UNIQUE
);

alter table admin Add COLUMN estConnecter BOOL DEFAULT(false);


-----------------------------TRIGGER---------------------------------
/*Trigger pour l'identifiant du client (fait par sam)*/
DELIMITER  // 
CREATE TRIGGER identifiantClient BEFORE INSERT
    on client
    FOR EACH ROW
BEGIN
    SET NEW.id_client = FLOOR(rand()*899) + 100;
end //
DELIMITER ;

/*Trigger pour le numéro de proje (fait par sam)t*/
DELIMITER  //
CREATE TRIGGER numeroProjet BEFORE INSERT
    on projet
    FOR EACH ROW
BEGIN
    SET NEW.id_projet = CONCAT(UPPER(NEW.id_client), '-', FLOOR(rand()*89) + 10, '-', YEAR(NEW.date_debut));
end //
DELIMITER ;

/*(fait par sam)*/
DELIMITER  //
CREATE TRIGGER matriculeEmp before insert
    on employe
    for each row
BEGIN
    SET NEW.matricule = CONCAT(SUBSTRING(NEW.nom, 1, 2), '-', YEAR(NEW.date_naissance), '-', FLOOR(rand()*89) + 10);
end;
DELIMITER ;


-----------------------------PROCEDURES (a retravailler)---------------------------------
/*Procédure pour ajouter employé (fait par isaac)*/
DELIMITER //
CREATE PROCEDURE p_ajout_employe(IN nom VARCHAR(20), IN prenom VARCHAR(20), IN dateNaiss DATE, IN email VARCHAR(150), IN adresse VARCHAR(100), IN date_embauche DATE, IN taux DOUBLE, IN photo VARCHAR(1000), IN statut VARCHAR(20))
BEGIN
    INSERT into employe VALUES(null, nom, prenom, dateNaiss, email, adresse, date_embauche, taux, photo, statut);
end //
DELIMITER ;

/*Procédure pour ajouter client (fait par isaac)*/
DELIMITER //
CREATE PROCEDURE p_ajout_client(IN nom VARCHAR(50), IN adresse VARCHAR(100), IN numero_tel VARCHAR(30), IN email VARCHAR(150))
BEGIN
    INSERT into client VALUES(null, nom, adresse, numero_tel, email);
end //
DELIMITER ;

/*Procédure pour ajouter projet (fait par isaac)*/
DELIMITER //
CREATE PROCEDURE p_ajout_projet(IN titre VARCHAR(50), IN date_debut DATE, IN description VARCHAR(255), IN budget DOUBLE, IN nbEmpl INT, IN salairTot DOUBLE, IN id_client VARCHAR(3), IN statut VARCHAR(20))
BEGIN
    INSERT into client VALUES(null, titre, date_debut, description, budget, nbEmpl, salairTot, id_client, statut);
end //
DELIMITER ;

/*Procédure pour ajouter projet (fait par sam)*/
DELIMITER //
CREATE PROCEDURE afficher_projets()
BEGIN
    SELECT * FROM projet;
end //
DELIMITER ;

/*Procédure pour get projet spécifique (fait par isaac)*/
DELIMITER //
CREATE PROCEDURE p_get_projet (IN id varchar(11))
BEGIN
    SELECT * FROM projet WHERE id_projet = id;
end//
DELIMITER ;

-----------------------------LES VIEWS-----------------------------
/*Vue pour afficher contenu de la table client (fait par isaac)*/
CREATE VIEW afficher_client AS
SELECT * FROM client;

/*Vue pour afficher contenu de la table employé (fait par isaac)*/
CREATE VIEW afficher_employe AS
SELECT * FROM employe;

/*Vue pour afficher contenu de la table projet (fait par isaac)*/
CREATE VIEW afficher_projet AS
SELECT * FROM projet;


-----------------------------FONCTIONS-----------------------------
/*Function pour calculer automatiquement le salaire total par heure des employés du projet (fait par isaac)*/
/*Va devoir relier la function au trigger ou procedure qui va venir le insert automatiquement dans table projet*/
DELIMITER //
CREATE FUNCTION f_salTot(id VARCHAR(11)) RETURNS DOUBLE
BEGIN
    DECLARE salaire DOUBLE;
    SELECT SUM(taux) INTO salaire FROM employe WHERE id_projet = id GROUP BY id_projet;
    RETURNS salaire;
end//
DELIMITER ;


-----------------------------INSERTION DE DONNÉE (fait par sam)-----------------------------
INSERT INTO client VALUES (null, 'Jean Lamontage', '47 rue bouol', '819-555-4443', 'email@email.xom');

INSERT INTO admin VALUES ('admin', 'admin', 0);

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


INSERT INTO projet VALUES(null, 'Projet Alpha', '2023-01-15', 'Développement d\'une application mobile innovante', 10000, 3, null, 126, 'En cours');
INSERT INTO projet VALUES(null, 'Projet Beta', '2023-02-20', 'Conception d\'un système de gestion de base de données avancé', 15000, 4, null, 582, 'Terminé');
INSERT INTO projet VALUES(null, 'Projet Gamma', '2023-03-10', 'Création d\'un site web interactif pour une entreprise locale', 20000, 2, null, 613, 'En cours');
INSERT INTO projet VALUES(null, 'Projet Delta', '2023-04-05', 'Déploiement d\'un réseau de capteurs pour la surveillance environnementale', 12000, 5, null, 654, 'Terminé');
INSERT INTO projet VALUES(null, 'Projet Epsilon', '2023-05-12', 'Développement d\'un algorithme d\'intelligence artificielle pour l\'analyse de données', 18000, 1, null, 731, 'En cours');
INSERT INTO projet VALUES(null, 'Projet Visionnaire', '2023-01-15', 'Développement d\'une technologie révolutionnaire pour la réalité augmentée', 100000, 3, null, 765, 'En cours');
INSERT INTO projet VALUES(null, 'Projet Horizon', '2023-02-20', 'Construction d\'un centre de recherche spatial pour l\'exploration interplanétaire', 500000, 4, null, 769, 'Terminé');
INSERT INTO projet VALUES(null, 'Projet Renaissance', '2023-03-10', 'Restauration numérique de trésors artistiques mondiaux', 75000, 2, null, 900, 'En cours');
INSERT INTO projet VALUES(null, 'Projet Éco-Innovant', '2023-04-05', 'Développement d\'une solution durable pour la gestion des déchets', 120000, 5, null, 917, 'Terminé');
INSERT INTO projet VALUES(null, 'Projet Quantum', '2023-05-12', 'Recherche avancée en informatique quantique pour la cryptographie', 200000, 1, null, 928, 'En cours');
INSERT INTO projet VALUES(null, 'Projet FusionTech', '2023-01-15', 'Intégration de technologies émergentes pour la transformation numérique', 80000, 3, null, 931, 'En cours');
INSERT INTO projet VALUES(null, 'Projet Solaris', '2023-02-20', 'Développement d\'une solution d\'énergie solaire intelligente', 120000, 4, null, 942, 'Terminé');
INSERT INTO projet VALUES(null, 'Projet GreenCity', '2023-03-10', 'Planification et mise en œuvre d\'infrastructures durables pour les villes', 150000, 2, null, 959, 'En cours');
INSERT INTO projet VALUES(null, 'Projet AquaNet', '2023-04-05', 'Création d\'un réseau de surveillance des ressources marines', 100000, 5, null, 970, 'Terminé');
INSERT INTO projet VALUES(null, 'Projet QuantumSys', '2023-05-12', 'Développement d\'un système de traitement quantique pour l\'analyse de données complexes', 180000, 1, null, 971, 'En cours');


INSERT INTO employe VALUES(null, 'Smith', 'John', '1990-05-15', 'john.smith@email.com', '123 Main Street', '2022-02-01', 20.50, 'lien_vers_photo1.jpg', '126-11-2023', 'Permanent');
INSERT INTO employe VALUES(null, 'Johnson', 'Alice', '1988-09-20', 'alice.johnson@email.com', '456 Oak Avenue', '2021-07-15', 18.75, 'lien_vers_photo2.jpg', '582-83-2023', 'Permanent');
INSERT INTO employe VALUES(null, 'Martin', 'David', '1995-03-10', 'david.martin@email.com', '789 Pine Road', '2023-01-10', 22.00, 'lien_vers_photo3.jpg', '582-83-2023', 'Journalier');
INSERT INTO employe VALUES(null, 'Gagnon', 'Émilie', '1992-11-05', 'emilie.gagnon@email.com', '101 Cedar Lane', '2022-09-03', 19.25, 'lien_vers_photo4.jpg', '654-89-2023', 'Permanent');
INSERT INTO employe VALUES(null, 'Lefevre', 'Pierre', '1987-07-25', 'pierre.lefevre@email.com', '202 Birch Street', '2021-05-12', 21.75, 'lien_vers_photo5.jpg', '654-89-2023', 'Journalier');
INSERT INTO employe VALUES(null, 'Dubois', 'Sophie', '1993-08-12', 'sophie.dubois@email.com', '234 Elm Street', '2022-04-05', 21.00, 'lien_vers_photo6.jpg', '931-51-2023', 'Permanent');
INSERT INTO employe VALUES(null, 'Bergeron', 'Alexandre', '1991-02-28', 'alexandre.bergeron@email.com', '567 Maple Avenue', '2021-10-20', 19.50, 'lien_vers_photo7.jpg', '931-51-2023', 'Permanent');
INSERT INTO employe VALUES(null, 'Lavoie', 'Élodie', '1994-06-18', 'elodie.lavoie@email.com', '890 Cedar Road', '2023-03-15', 22.50, 'lien_vers_photo8.jpg', '931-51-2023', 'Journalier');
INSERT INTO employe VALUES(null, 'Renaud', 'David', '1989-12-03', 'david.renaud@email.com', '111 Oak Lane', '2022-01-02', 20.75, 'lien_vers_photo9.jpg', '959-18-2023', 'Permanent');
INSERT INTO employe VALUES(null, 'Boucher', 'Caroline', '1990-04-30', 'caroline.boucher@email.com', '222 Pine Street', '2021-08-10', 18.25, 'lien_vers_photo10.jpg', '942-71-2023', 'Journalier');
INSERT INTO employe VALUES(null, 'Tremblay', 'Étienne', '1992-03-22', 'etienne.tremblay@email.com', '345 Cedar Avenue', '2022-06-08', 23.00, 'lien_vers_photo11.jpg', '928-58-2023', 'Permanent');
INSERT INTO employe VALUES(null, 'Girard', 'Isabelle', '1996-09-14', 'isabelle.girard@email.com', '678 Birch Road', '2021-12-01', 18.00, 'lien_vers_photo12.jpg', '971-59-2023', 'Journalier');
INSERT INTO employe VALUES(null, 'Bélanger', 'Maxime', '1988-05-31', 'maxime.belanger@email.com', '901 Elm Lane', '2023-02-14', 24.50, 'lien_vers_photo13.jpg', '970-42-2023', 'Permanent');
INSERT INTO employe VALUES(null, 'Lévesque', 'Sophie', '1995-11-10', 'sophie.levesque@email.com', '222 Oak Avenue', '2022-04-20', 19.75, 'lien_vers_photo14.jpg', '970-42-2023', 'Permanent');


