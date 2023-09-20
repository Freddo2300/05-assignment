-- Use the newly created database chinook
USE chinook;

CREATE TABLE superheroes(
    id INT NOT NULL AUTO_INCREMENT,
    name VARCHAR(255),
    alias VARCHAR(255),
    origin VARCHAR(255),
    PRIMARY KEY(id),
    UNIQUE (name, alias)
);

CREATE TABLE assistants(
    id INT NOT NULL AUTO_INCREMENT,
    superhero_id INT,
    name VARCHAR(255),
    PRIMARY KEY(id),
    UNIQUE (name)
);

CREATE TABLE powers(
    id INT NOT NULL AUTO_INCREMENT,
    name VARCHAR(255),
    description TEXT,
    PRIMARY KEY(id),
    UNIQUE (name)
);