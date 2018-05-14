

INSERT INTO Casino (Name, ClubName, Location, logo) 
VALUES('Lake of the Torches Casino', 'ClubAdvantage', '{lat:45.974161, lon:-89.891799}', 'casino-1-logo.png');

INSERT INTO ClubLevel (CasinoID, Name, Sort) 
VALUES
    (1, 'Advantage', 1),
    (1, 'Premium', 2),
    (1, 'Supreme', 3),
    (1, 'Elite', 4)
    ;

INSERT INTO Player (CasinoID, TierID, FirstName,  LastName, Title, MemberNumber, BirthDate, Gender, PreferredName)
VALUES
    (1, 1, 'Sylvester', 'Migliassi', 'Mr.', '123456789-01',   '1941-03-26', 'M', ''),
    (1, 2, 'Shantee', 'Yeaple', 'Ms.',  '123456789-02',       '1952-02-29', 'F',''),
    (1, 3, 'Nathanael', 'Divins', 'Mr.',  '123456789-03',     '1953-03-15', 'M',''),
    (1, 4, 'Ollie', 'Chaparro', '',  '123456789-04',          '1953-09-15', 'M','Your Majesty'),
    (1, 5, 'Hamlin', 'Harp', 'Mr.',  '123456789-05',         '1954-08-12', 'M','Harry'),
    (1, 6, 'Moyna', 'Malcolm', 'Mrs.',  '123456789-06',      '1954-12-08', 'F',''),
    (1, 7, 'Yancey', 'Maccheroni', 'Ms.',  '123456789-07',   '1957-10-30', 'F',''),
    (1, 8, 'Laurette', 'Edison', 'Mrs.',  '123456789-08',     '1958-04-23', 'F',''),
    (1, 4, 'Padraig', 'Menke', '',  '123456789-09',           '1963-04-03', 'M','Pat'),
    (1, 5, 'Zorine', 'Demaio-Newton', 'Ms.',  '123456789-10',  '1965-03-13', 'F','Zoe'),
    (1, 1, 'Tristan', 'Stierwalt', 'Dr.',  '123456789-11',       '1973-03-14', 'M','Dr. Stierwalt'),
    (1, 5, 'Briny', 'Olivieri', '',  '123456789-12',         '1973-05-23', 'F', 'Bitty'),
    (1, 6, 'Rudolfo', 'Locker', 'Mr.',  '123456789-13',      '1974-12-11', 'M', 'Rudy'),
    (1, 7, 'Ferdinande', 'Capanni', '',  '123456789-14',  '1975-04-21', 'F', 'Ferg'),
    (1, 8, 'Rafe', 'Palmieri', 'Mr.',  '123456789-15',       '1988-12-02', 'M', 'Mr. Palmieri')
	;


 INSERT INTO staff (CasinoID, FirstName, LastName, Status)
VALUES
	(1, 'Arabela', 'Goliger', 'Active'),
	(1, 'Emmie', 'Ogawa', 'Active'),
	(1, 'Lou', 'Camacho', 'Active'),
	(1, 'Haily', 'Nennig', 'Active'),
	(1, 'Pearl', 'Marx', 'Active'),
	(1, 'Andrew', 'Thanos', 'Active')
	;