
INSERT INTO [superheroes] 
( [name], [alias], [origin])
VALUES
( 'Spiderman', 'Spodermon', 'New York City'),
( 'Batman', 'Bathman', 'Arkham City'),
( 'Wolverine', 'Wolvedawg', 'Alberta');



INSERT INTO [assistants]
( [name], [superhero_id])
VALUES
( 'Uncle Ben', (SELECT id FROM superheroes WHERE name='Spiderman')),
( 'Alfred', (SELECT id FROM superheroes WHERE name='Batman')),
( 'Jubilee', (SELECT id FROM superheroes WHERE name='Wolverine'));



INSERT INTO [powers]
( [name], [description])
VALUES
( 'Super Jump', 'Hero can jump very high'),
( 'Filthy Rich', 'Hero has more money than they can spend'),
( 'Retractable Claws', 'Hero can summon deadly steel claws out of their knucles'),
( 'Spider Web', 'Hero can spin out webs to travel fast or render enemies harmless');

INSERT INTO [superheroes_powers] 
([superhero_id], [power_id]) 
VALUES
((SELECT id from superheroes WHERE name='Spiderman'), (SELECT id from powers WHERE name='Super Jump')),
((SELECT id from superheroes WHERE name='Spiderman'), (SELECT id from powers WHERE name='Spider Web')),
((SELECT id from superheroes WHERE name='Batman'), (SELECT id from powers WHERE name='Super Jump')),
((SELECT id from superheroes WHERE name='Batman'), (SELECT id from powers WHERE name='Filthy Rich')),
((SELECT id from superheroes WHERE name='Wolverine'), (SELECT id from powers WHERE name='Super Jump')),
((SELECT id from superheroes WHERE name='Wolverine'), (SELECT id from powers WHERE name='Retractable Claws'));