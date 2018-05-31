CREATE DATABASE commerce;
USE commerce;
CREATE TABLE `__EFMigrationsHistory`
(
	`MigrationId` nvarchar(150) NOT NULL,
   `ProductVersion` nvarchar(32) NOT NULL,
   PRIMARY KEY (`MigrationId`)
);