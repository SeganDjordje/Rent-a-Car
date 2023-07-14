CREATE DATABASE RentalCars;
use RentalCars

-------------------------------------------------------------------------------------------------------------------------------------------------------------------
--CREATING TABLES AND PRIMARY KEY--
CREATE TABLE Car
(
	CarID int IDENTITY(1,1) NOT NULL,
	Manufacturer nvarchar(50) NULL,
	Model nvarchar(50) NULL,
	LicencePlate nvarchar(50) NOT NULL, -- Requiered --
	Year date NULL,
	Available bit NOT NULL

	CONSTRAINT PK_Car PRIMARY KEY (CarID)
);

CREATE TABLE Rental
(
	RentalID int IDENTITY(1,1) NOT NULL,
	CustomerID int NOT NULL,
	CarID int NOT NULL,
	DateRented date NULL,
	DateReturned date NULL

	CONSTRAINT PK_Rental PRIMARY KEY (RentalID)
);

CREATE TABLE Customer
(
	CustomerID int IDENTITY(1,1) NOT NULL,
	Name nvarchar(50) NOT NULL,
	DriverLicNo nvarchar(50) NULL

	CONSTRAINT PK_Customer PRIMARY KEY (CustomerID)
);

-------------------------------------------------------------------------------------------------------------------------------------------------------------------
--FOREIGN KEYS--
ALTER TABLE Rental
ADD CONSTRAINT FK_Rental_Car
	FOREIGN KEY (CarID)
	REFERENCES Car (CarID)

ALTER TABLE Rental
ADD CONSTRAINT FK_Rental_Customer
	FOREIGN KEY (CustomerID)
	REFERENCES Customer (CustomerID)

-------------------------------------------------------------------------------------------------------------------------------------------------------------------
--NON CLUSTERED INDEX--
CREATE INDEX IX_FK_Rental_Car ON Rental (CarID)
CREATE INDEX IF_FK_Rental_Customer ON Rental (CustomerID)
