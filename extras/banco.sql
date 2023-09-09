create database pokejournal_mysql;
use pokejournal_mysql;
ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `Users` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `UserName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Email` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Password` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Salt` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `UpdatedAt` datetime(6) NOT NULL,
    CONSTRAINT `PK_Users` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;


CREATE TABLE `FavoritePokemons` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `PokemonIndex` int NOT NULL,
    `UserId` char(36) COLLATE ascii_general_ci NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `UpdatedAt` datetime(6) NOT NULL,
    CONSTRAINT `PK_FavoritePokemons` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_FavoritePokemons_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;


CREATE TABLE `PokeTeams` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `UpdatedAt` datetime(6) NOT NULL,
    `UserId` char(36) COLLATE ascii_general_ci NOT NULL,
    CONSTRAINT `PK_PokeTeams` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_PokeTeams_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;


CREATE TABLE `PokemonLists` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `DefaultName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `PokemonIndex` int NOT NULL,
    `CustomName` longtext CHARACTER SET utf8mb4 NULL,
    `ImgURL` longtext CHARACTER SET utf8mb4 NULL,
    `PokeTeamId` char(36) COLLATE ascii_general_ci NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `UpdatedAt` datetime(6) NOT NULL,
    CONSTRAINT `PK_PokemonLists` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_PokemonLists_PokeTeams_PokeTeamId` FOREIGN KEY (`PokeTeamId`) REFERENCES `PokeTeams` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;


CREATE INDEX `IX_FavoritePokemons_UserId` ON `FavoritePokemons` (`UserId`);


CREATE INDEX `IX_PokemonLists_PokeTeamId` ON `PokemonLists` (`PokeTeamId`);


CREATE INDEX `IX_PokeTeams_UserId` ON `PokeTeams` (`UserId`);
