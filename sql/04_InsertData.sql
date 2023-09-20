INSERT INTO superheroes (name, alias, origin) VALUES
("Spiderman", "Spodermon", "New York City"),
("Batman", "Bathman", "Arkham City"),
("Wolverine", "Wolvedawg", "Alberta");

INSERT INTO assistants (name, superhero_id) VALUES
("Uncle Ben", (SELECT id FROM superheroes WHERE name='Spiderman')),
("Alfred", (SELECT id FROM superheroes WHERE name='Batman')),
("Jubilee", (SELECT id FROM superheroes WHERE name='Wolverine'));

INSERT INTO powers (name, description) VALUES
("Super Jump", "Hero can jump very high"),
("Filthy Rich", "Hero has more money than they can spend"),
("Retractable Claws", "Hero can summon deadly steel claws out of their knucles"),
("Spider Web", "Hero can spin out webs to travel fast or render enemies harmless");