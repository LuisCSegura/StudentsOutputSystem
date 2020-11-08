DROP TABLE IF EXISTS students;
DROP TABLE IF EXISTS users;

CREATE TABLE users
(
    id serial primary key,
    name varchar not null,
	password varchar not null,
	is_admin bool default false,
    CONSTRAINT unq_name_user UNIQUE(name)
);
CREATE TABLE students
(
    id serial primary key,
    code varchar not null,
	name varchar not null,
	last_name varchar not null,
	id_number varchar not null,
	genre char not null,
	section varchar not null,
	personal_photo bytea not null,
	section_photo bytea not null,
	p_lunch bool default false,
    p_free_lessons bool default false,
    p_free_afternoon bool default  false,
    CONSTRAINT unq_code_student UNIQUE(code)
);
INSERT INTO users(name, password, is_admin) VALUES('admin','12345678',true);
INSERT INTO users(name, password, is_admin) VALUES('guard','12345',false);
SELECT * FROM users