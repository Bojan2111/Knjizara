create table zanrovi(
	id_zanra int identity(1,1) primary key,
	naziv_zanra nvarchar(30) not null
);
create table knjige(
	id_knjige int identity(1,1) primary key,
	naziv_knjige nvarchar(100) not null,
	cena decimal(10,2),
	zanr_id int,
	izbrisana bit default 0,
	foreign key(zanr_id) references zanrovi(id_zanra)
);

insert into zanrovi(naziv_zanra) values ('Sci-Fi'), ('Komedija'), ('Horor');

insert into knjige(naziv_knjige, cena, zanr_id) values ('Autostoperski vodic kroz galaksiju', 345.99, 1);
insert into knjige(naziv_knjige, cena, zanr_id) values ('Anatomija respiratornog trakta', 1234.90, 2);
insert into knjige(naziv_knjige, cena, zanr_id) values ('Intergalakticki turnir u klizmanju', 999.01, 3);

