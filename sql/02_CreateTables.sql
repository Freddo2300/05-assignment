-- Use the newly created database chinook
USE chinook;

CREATE TABLE superheroes(
    id INT NOT NULL AUTO_INCREMENT,
    name VARCHAR(255),
    alias VARCHAR(255),
    origin VARCHAR(255),
    PRIMARY KEY(id)
);

CREATE TABLE assistants(
    id INT NOT NULL AUTO_INCREMENT,
    name VARCHAR(255),
    PRIMARY KEY(id)
);

CREATE TABLE powers(
    id INT NOT NULL AUTO_INCREMENT,
    name VARCHAR(255),
    description TEXT,
    PRIMARY KEY(id)
);