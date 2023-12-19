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

-----------------------------REQUÊTE SQL---------------------------------
/*Requête pour visualiser chaque client et les employés associé à leur projet (fait par isaac)*/
SELECT c.nom AS client,
       titre AS titre_projet,
       CONCAT(e.prenom, ' ', e.nom) AS nom_employé
FROM client c
INNER JOIN projet p ON c.id_client = p.id_client
INNER JOIN employe e on p.id_projet = e.id_projet
ORDER BY c.nom;


/*Requête pour visualiser chaque taux par heures de chaque employé avec leurs projets (fait par sam)*/
SELECT CONCAT(SUM(taux), '$') as Taux_total_par_projet, CONCAT(prenom, ' ', nom) as nom_employe, titre FROM employe
inner join a2023_420325ri_fabeq19.projet p on employe.id_projet = p.id_projet
GROUP BY nom_employe
ORDER BY Taux_total_par_projet

/*Requête pour voir l'âge a laquelle chaque employé on été embauché (fait par isaac)'*/
SELECT CONCAT(prenom, ' ', nom) AS nom,
       date_naissance,
       date_embauche,
       CONCAT(YEAR(date_embauche)-YEAR(date_naissance), ' ans') AS age_embauche
FROM employe;


/*sous-requête pour avoir la moyenne du salaire maximum des employé par projet (fait par sam)'*/
SELECT MAX(moyenneSalaire) FROM (SELECT AVG(taux) AS moyenneSalaire
                                 FROM employe
                                 GROUP BY (id_projet)) e;

/*sous-requête pour avoir les employé qui travail sur le projet alpha (fait par sam)'*/
SELECT matricule, nom FROM employe WHERE id_projet IN (
    SELECT id_projet FROM projet WHERE titre = 'Projet Alpha'
);
-----------------------------TRIGGER---------------------------------
/*Trigger pour générer l'identifiant du client (fait par sam)*/
DELIMITER  // 
CREATE TRIGGER identifiantClient BEFORE INSERT
    on client
    FOR EACH ROW
BEGIN
    SET NEW.id_client = FLOOR(rand()*899) + 100;
end //
DELIMITER ;

/*Trigger pour générer le numéro de projet (fait par sam)t*/
DELIMITER  //
CREATE TRIGGER numeroProjet BEFORE INSERT
    on projet
    FOR EACH ROW
BEGIN
    SET NEW.id_projet = CONCAT(UPPER(NEW.id_client), '-', FLOOR(rand()*89) + 10, '-', YEAR(NEW.date_debut));
end //
DELIMITER ;

/*fait par isaac, pour update le numero de projet seulement lorsque id du client change*/
DELIMITER  //
CREATE TRIGGER numeroProjetModif BEFORE update
    on projet
    FOR EACH ROW
BEGIN
    IF(OLD.id_client != NEW.id_client) THEN
    SET NEW.id_projet = CONCAT(UPPER(NEW.id_client), '-', FLOOR(rand()*89) + 10, '-', YEAR(NEW.date_debut));
    END IF;
end //
DELIMITER 

/*Trigger pour faire le matricule de l'employé (fait par sam)*/
DELIMITER  //
CREATE TRIGGER matriculeEmp before insert
    on employe
    for each row
BEGIN
    SET NEW.matricule = CONCAT(SUBSTRING(NEW.nom, 1, 2), '-', YEAR(NEW.date_naissance), '-', FLOOR(rand()*89) + 10);
end;
DELIMITER ;

/*Trigger pour modifier le matricule de l'employé lorsque modification de son nom (fait par isaac)*/
DELIMITER  //
CREATE TRIGGER matriculeEmpModif before update
    on employe
    for each row
BEGIN
IF (OLD.nom != NEW.nom) THEN
    SET NEW.matricule = CONCAT(SUBSTRING(NEW.nom, 1, 2), '-', YEAR(NEW.date_naissance), '-', FLOOR(rand()*89) + 10);
END IF;
end;
DELIMITER ;

/*Trigger pour update le salaire des employé (fait par sam et isaac)*/
DELIMITER //
CREATE TRIGGER update_salaireTotalAjout
    AFTER INSERT ON employe
    FOR EACH ROW
BEGIN
    DECLARE total_taux DOUBLE;
    SELECT f_salTot(NEW.id_projet) INTO total_taux;
    UPDATE projet
    SET salaireTotal = total_taux
    WHERE id_projet = NEW.id_projet;;
END //
DELIMITER ;


/*Trigger pour update le salaire total d'un projet lorsqu'un employé se fait delete (fait par isaac)*/
DELIMITER //
CREATE TRIGGER update_salaireTotalDelete
    AFTER DELETE ON employe
    FOR EACH ROW
BEGIN
    DECLARE total_taux DOUBLE;
    SELECT f_salTot(OLD.id_projet) INTO total_taux;
    UPDATE projet
    SET salaireTotal = total_taux
    WHERE id_projet = OLD.id_projet;
END //
DELIMITER ;

/*Trigger pour le statut de l'employé (fait par sam)*/
DELIMITER //
CREATE TRIGGER statutEmploye before insert on employe
    for each row
    begin
        if(YEAR(current_date) - YEAR(date_embauche) < 3) THEN
            SET NEW.statut = 'Journalier';
        ELSE
            SET NEW.statut = 'Permanent';
        END IF;
    end ;
DELIMITER //

-----------------------------PROCÉDURES---------------------------------
/*Procédure pour ajouter employé (fait par isaac)*/
DELIMITER //
CREATE PROCEDURE p_ajout_employe(IN nom VARCHAR(50), IN prenom VARCHAR(50), IN date_naiss DATE,
                                 IN email VARCHAR(150), IN adresse VARCHAR(100), IN date_emb DATE,
                                 IN taux DOUBLE, IN photo VARCHAR(1000), IN idProjet VARCHAR(15))
BEGIN
    INSERT into employe (nom, prenom, date_naissance, email, adresse, date_embauche, taux, photo, id_projet)
    VALUES (nom, prenom, date_naiss, email, adresse, date_emb, taux, photo, idProjet);
end //
DELIMITER ;

/*Procédure pour ajouter client (fait par isaac)*/
DELIMITER //
CREATE PROCEDURE p_ajout_client(IN nom VARCHAR(50), IN adresse VARCHAR(100), IN numero_tel VARCHAR(30), IN email VARCHAR(150))
BEGIN
    INSERT into client (nom, adresse, numero_tel, email)
    VALUES(nom, adresse, numero_tel, email);
end //
DELIMITER ;


/*Procédure pour ajouter projet (fait par isaac et sam)*/
DELIMITER //
CREATE PROCEDURE p_ajout_projet(IN titre varchar(50), IN date_debut date,
                                 IN description varchar(255), IN budget double, IN nbEmplo INT,
                                 IN id_client varchar(3), IN statut varchar(20))
BEGIN
    INSERT INTO projet (titre, date_debut, description, budget, nb_employe, salaireTotal, id_client, statut)
    VALUES (titre, date_debut, description, budget, nbEmplo, 0, id_client, statut);
END //
DELIMITER ;

/*Procédure pour visualiser liste des projets (fait par sam)*/
DELIMITER //
CREATE PROCEDURE afficher_projets()
BEGIN
    SELECT * FROM projet;
end //
DELIMITER ;

/*Procédure pour get un projet spécifique à partir de id (fait par isaac)*/
DELIMITER //
CREATE PROCEDURE p_get_projet (IN id varchar(11))
BEGIN
    SELECT * FROM projet WHERE id_projet = id;
end//
DELIMITER ;

/*Procédure pour avoir le client spécifique avec son id (fait par isaac)*/
DELIMITER //
CREATE PROCEDURE p_get_client (IN id varchar(15))
BEGIN
    SELECT * FROM client WHERE id_client = id;
end//
DELIMITER ;

/*Procédure pour afficher les clients (fait par sam)*/
DELIMITER //
CREATE PROCEDURE afficher_clients()
BEGIN
    SELECT * FROM client;
end //
DELIMITER ;

/*Procédure pour afficher les employés (fait par sam)*/
DELIMITER //
CREATE PROCEDURE afficher_employes()
BEGIN
    SELECT * FROM employe;
end //
DELIMITER ;

/*Procédure pour avoir l'id du client pour le projet (fait par sam)*/
DELIMITER //
CREATE PROCEDURE idClientPourAjouterProjet(IN idClient VARCHAR(100))
BEGIN
    SELECT id_client FROM client WHERE nom LIKE idClient;
end //
DELIMITER ;

/*Procédure pour mettre l'état de connexion en connecter (fait par sam)*/
Delimiter //
CREATE PROCEDURE p_estConnecter(IN compte VARCHAR(10))
BEGIN
    UPDATE admin SET estConnecter = true WHERE utilisateur = compte;
end //
DELIMITER ;

/*Procédure pour déconnecter (fait par sam)*/
DELIMITER //
CREATE procedure p_deconnexion()
BEGIN
    UPDATE admin SET estConnecter = false WHERE 1 = 1;
end //
DELIMITER ;

/*Procédure qui permet de mettre à jour le salaire total de chaque projet (fait par isaac)*/
DELIMITER //
CREATE PROCEDURE p_maj_salairesTot()
BEGIN
    DECLARE id VARCHAR(15);
    DECLARE taux DOUBLE;
    DECLARE termine INT DEFAULT 0;

    DECLARE boucle CURSOR FOR
    SELECT id_projet FROM projet;

    /*Pour ignorer toute erreur ou il n'y a pas de resultat*/
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET termine = 1;

    OPEN boucle;

    boucle_principale: LOOP
        FETCH boucle INTO id;

        /*Pour sortir de la boucle*/
        IF (termine = 1) THEN
            LEAVE boucle_principale;
        end if;

        /*Vient faire la mise à jour du salaire total de chaque projet*/
        SELECT f_salTot(id) INTO taux;
        UPDATE projet
            SET salaireTotal = taux
            WHERE id_projet = id;
    end loop boucle_principale;

    CLOSE boucle;
end //
DELIMITER ;

/*Procédure qui va chercher tout les employés d'un projet (fait par isaac)*/
DELIMITER //
CREATE PROCEDURE p_liste_empl_projet(IN idProjet VARCHAR(15))
BEGIN
    SELECT * FROM employe WHERE id_projet = idProjet;
end //
DELIMITER ;

/*Procédure pour modifier un employé (fait par isaac)*/
DELIMITER //
CREATE PROCEDURE p_modif_empl(IN matEmp VARCHAR(12), IN nomEmp VARCHAR(50), IN prenomEmp VARCHAR(50),
                                 IN emailEmp VARCHAR(150), IN adresseEmp VARCHAR(100),IN tauxEmp DOUBLE,
                                 IN photoEmp VARCHAR(1000), IN idProjet VARCHAR(15))
BEGIN
    UPDATE employe
    SET nom = nomEmp, prenom = prenomEmp, email = emailEmp, adresse = adresseEmp, taux = tauxEmp, photo = photoEmp, id_projet = idProjet
    WHERE matricule = matEmp;
end //
DELIMITER ;

/*Procédure pour modifier un projet (fait par isaac)*/
DELIMITER //
CREATE PROCEDURE p_modif_proj(IN idProjet varchar(15), IN titre varchar(50),
                              IN description varchar(255), IN budget double, IN nbEmplo int,
                              IN id_client varchar(3), IN statut varchar(20))
BEGIN
    UPDATE projet
    SET id_projet = idProjet, titre = titre, description = description, budget = budget, nb_employe = nbEmplo, id_client = id_client, statut = statut
    WHERE id_projet = idProjet;
end //
DELIMITER ;

/*Procédure pour modifier un client (fait par isaac)*/
DELIMITER //
CREATE PROCEDURE p_modif_client(IN idClient varchar(3), IN nom varchar(100), IN adresse varchar(150), IN numero_tel varchar(25), IN email varchar(200))
BEGIN
    UPDATE client
    SET id_client = idClient, nom = nom, adresse = adresse, numero_tel = numero_tel, email = email
    WHERE id_client = idClient;
end //
DELIMITER

/*Procédure pour compter le nombre de compte (fait par sam)*/
CREATE PROCEDURE count_de_compte()
begin
    SELECT COUNT(*) FROM afficher_admin;
end;

/*Procédure pour verifier si le compte est connecter (fait par sam)*/
create procedure p_verif_est_connecter()
begin
    SELECT estConnecter FROM admin;
end;

/*Procédure pour afficher si le compte est existe (fait par sam)*/
create procedure p_afficher_admin()
begin
    SELECT * from admin;
end;

/*Procédure pour creer le compte (fait par sam)*/
create
    definer = `2172853`@`%` procedure AjouterAdmin(IN utilisateurParam varchar(255), IN motDePasseParam varchar(255))
BEGIN
    INSERT INTO admin (utilisateur, motDePasse, estConnecter) VALUES (utilisateurParam, motDePasseParam, 0);
END;



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

/*Vue pour afficher contenu de la table admin (fait par sam)*/
CREATE VIEW afficher_admin AS
    SELECT * FROM admin;

/*Vue pour afficher contenu de la table admin la colonne estConnecter (fait par sam)*/
CREATE VIEW afficher_Connexion AS
    SELECT estConnecter FROM admin;

-----------------------------FONCTIONS-----------------------------
/*Function pour calculer automatiquement le salaire total par heure des employés du projet: utilisé par 2 triggers updateSalaire (fait par isaac)*/
DELIMITER //
CREATE FUNCTION f_salTot(id VARCHAR(15)) RETURNS DOUBLE
BEGIN
    DECLARE salTot DOUBLE;
    SELECT SUM(taux) INTO salTot FROM employe WHERE id_projet = id GROUP BY id_projet;
    IF (salTot IS NULL) THEN
        SET salTot = 0;
    end if;
    RETURN salTot;
end//
DELIMITER ;



/*Function pour supprimer un client (fait par sam)*/
DELIMITER //
CREATE FUNCTION supprimerClient(
    p_id_client VARCHAR(3)
)
RETURNS INT
BEGIN
    DELETE FROM client WHERE id_client = p_id_client;
    RETURN ROW_COUNT();
END //
DELIMITER ;



/*Function pour supprimer un projet (fait par sam)*/
DELIMITER //
CREATE FUNCTION supprimerProjet(
    p_id_projet VARCHAR(11)
)
RETURNS INT
BEGIN
    DELETE FROM projet WHERE id_projet = p_id_projet;
    RETURN ROW_COUNT();
END //
DELIMITER ;



/*Function pour supprimer un employé (fait par sam)*/
DELIMITER //
CREATE FUNCTION supprimerEmploye(
    p_matricule VARCHAR(10)
)
RETURNS INT
BEGIN
    DELETE FROM employe WHERE matricule = p_matricule;
    RETURN ROW_COUNT();
END //
DELIMITER ;



/*Function pour obtenir la liste des clients (fait par sam)*/
DELIMITER //
CREATE FUNCTION getListeClients()
    RETURNS VARCHAR(100)
    BEGIN
        SELECT * FROM client;
    END //
DELIMITER ;




/*Function pour obtenir la liste des employés par projets (fait par sam)*/
DELIMITER //
CREATE FUNCTION getListeEmployesParProjet(
    p_id_projet VARCHAR(11)
)
    RETURNS VARCHAR(11)
                BEGIN
    SELECT * FROM employe WHERE id_projet = p_id_projet;
END //
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


