Use master;
Go

IF DB_ID(N'TischtennisShop') IS NULL
  CREATE DATABASE TischtennisShop;
GO

Use TischtennisShop;
Go


IF OBJECT_ID('Beläge') IS NOT NULL
  DROP TABLE Beläge;
GO

IF OBJECT_ID('Mitarbeiter') IS NOT NULL
  DROP TABLE Mitarbeiter;
GO

IF OBJECT_ID('Kunden') IS NOT NULL
  DROP TABLE Kunden;
GO

IF OBJECT_ID('Rechnung') IS NOT NULL
  DROP TABLE Rechnung;
GO

IF OBJECT_ID('Verkaufteware') IS NOT NULL
  DROP TABLE Verkaufteware;
GO





CREATE TABLE Beläge (
  ID_Beläge  INT NOT NULL PRIMARY KEY IDENTITY (1,1),
  Name nvarchar(500),
 Marke nvarchar(500),
 "Belag Art"nvarchar(500),
 "Menge Rot" int,
 "Menge Schwarz" int,
 Preis int ,
 Bild image ,
);


CREATE TABLE Mitarbeiter
(
	ID_Mitarbeiter INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	Vorname nvarchar(500),
	Nachname nvarchar(500),
	Passwort int,
);


CREATE TABLE Kunden
(
	ID_Kunden INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	Vorname nvarchar (500),
	Nachname nvarchar (500),
	Straße nvarchar(500),
	Hausnummer int,
	Postleitzahl int,
	Ort nvarchar(500)
);

CREATE TABLE Rechnung 
(
	Rechnungsnummer INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	ID_Kunden int,
	Gesamtbetrag int,
	CONSTRAINT KundenNummer FOREIGN KEY (ID_Kunden)
	REFERENCES Kunden(ID_Kunden)

)

CREATE TABLE Verkaufteware 
(
	ID_Rechnung int,
	Name nvarchar(500),
	Farbe nvarchar(500),
	Menge int,
	Einzelpreis int,
	CONSTRAINT pk_Rechnung PRIMARY KEY (ID_Rechnung),
	CONSTRAINT fk_Rechnung 
	FOREIGN KEY (ID_Rechnung)
	REFERENCES Rechnung (Rechnungsnummer)
)




Insert Into Mitarbeiter(Vorname,Nachname,Passwort) VALUES 
('Max','Mustermann',154345),
('Leon','Mühlbach',176565),
('Michael','Mühlbach',123409),
('Andreas','Mühlbach',098765),
('Lukas','Becker',234567),
('David','Kern',091234),
('Paul','Paulchen',987009);

Insert Into Beläge(Name,Marke,[Belag Art],[Menge Rot],[Menge Schwarz],Preis) Values
('Evolution MX-P','Tibhar','Noppeninnen',20,30,45.00),
('Evolution EL-S','Tibhar','Noppeninnen',20,50,45.00),
('Evolution FX-S','Tibhar','Noppeminnen',30,10,45.00),
('Evolution EL-P','Tibhar','Noppeninnen',10,80,45.00),
('Evolution FX-P','Tibhar','Noppeninnen',90,50,45.00),
('Vari Spin','Tibhar','Noppeninnen',10,70,30.00),
('Rapid','Tibhar','Noppeninnen',40,20,30.00),
('Speedy Soft','Tibhar','kurze Noppen',20,60,35.00),
('Grass D-TecS','Tibhar','lange Noppen',80,70,35.00),
('Ellen','Tibhar','Anti',60,40,20.00),
('Reflection','Der Materialspezialist','Anti',20,30,60.00),
('Scandal','Der Materialspezialist','Anti',30,40,55.00),
('Elite Long','Der Materialspezialist','lange Noppen',70,20,35.00),
('Coppa','Donic','Noppeninnen',20,40,30.00),
('Acuda S1','Donic','Noppeninnen',40,10,40.00),
('Acuda S2', 'Donic','Noppeninnen',10,60,40.00),
('Acuda S3', 'Donic', 'Noppeninnen',40,40,40.00),
('Baracuda','Donic','Noppeninnen',70,20,40.00),
('Desto F1','Donic','Noppeninnen',30,40,30.00),
('Liga','Donic','Noppeninnen',90,40,15.00),
('Waran','SpinLord','kurze Noppen',40,10,20.00),
('Keiler','SpinLord','kurze Noppen',30,30,20.00),
('Degu','SpinLord','kurze Noppen',70,40,25.00),
('Feuerstich','SpinLord','lange Noppen',10,20,15.00),
('Gigant','SpinLord','Anti',20,20,15.00),
('RITC','Friendship 729','Noppeninnen',30,20,10.00),
('Geospin','Friendship 729','Noppeninnen',50,10,5.00),
('Bloom Spin','Friendship 729','Noppeninnen',70,20,25.00),
('Focus 2','Friendship 729','Noppeninnen',20,50,15.00),
('Bloom Power','Friendship 729','Noppeninnen',90,10,25.00),
('Aggressor','Lion','Noppeninnen',20,30,5.00),
('Roar','Lion','Noppeninnen',30,10,20.00),
('Mantlet','Lion','Anti',40,20,15.00),
('Amigo','Palio','Noppeninnen',40,40,15.00),
('Conqueror','Palio','Noppeninnen',90,20,10.00),
('Ak 47 blue','Palio','Noppeninnen',40,60,20.00),
('Ak 47 red','Palio','Noppeninnen',60,40,20.00),
('Ak 47 yellow','Palio','Noppeninnen',50,50,20.00),
('Peril Anti','Giant Dragon','Anti',40,20,20.00),
('Cropcircles','Giant Dragon','lange Noppen',90,10,15.00);
