ALTER TABLE assistants
ADD CONSTRAINT fk_assistants
FOREIGN KEY(superhero_id)
REFERENCES superheroes(id);

-- Create junction table for many-to-many
CREATE TABLE superheroes_powers (
    superhero_id INT,
    power_id INT,
    CONSTRAINT pk_superhero_power PRIMARY KEY (superhero_id, power_id),
    -- Foreign key 1: superhero
    CONSTRAINT fk_superhero
    FOREIGN KEY (power_id) REFERENCES powers(id),
    -- Foreign key 2: power
    CONSTRAINT fk_power
    FOREIGN KEY (superhero_id) REFERENCES superheroes(id)
);