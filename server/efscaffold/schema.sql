drop schema if exists todo cascade ;
create schema if not exists todo;

create table todo.todos(
                           id text not null primary key,
                           title text not null,
                           description text not null,
                           priority numeric not null,
                           completed boolean not null,
                           created_at timestamp not null default now()
);