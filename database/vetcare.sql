CREATE TABLE owners (
    id integer GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name varchar(80) NOT NULL,
    email varchar(160) NOT NULL UNIQUE,
    phone varchar(25) NOT NULL
);

CREATE TABLE pets (
    id integer GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name varchar(80) NOT NULL,
    species varchar(40) NOT NULL,
    age integer NOT NULL CHECK (age BETWEEN 0 AND 100),
    owner_id integer NOT NULL REFERENCES owners(id) ON DELETE RESTRICT
);

INSERT INTO owners (name, email, phone) VALUES
    ('Ana Torres', 'ana.torres@example.com', '+51 987 654 321'),
    ('Luis Mendoza', 'luis.mendoza@example.com', '+51 976 123 456');

INSERT INTO pets (name, species, age, owner_id) VALUES
    ('Luna', 'Perro', 4, 1),
    ('Michi', 'Gato', 2, 1),
    ('Nube', 'Conejo', 1, 2);
