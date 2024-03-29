insert into benefit (id, name) values ('e0a13754-021e-4f61-9d2a-169882622f9b', 'WiFi');
insert into benefit (id, name) values ('0e447911-5d5f-4412-8ec8-68c21ab3f432', 'TV');
insert into benefit (id, name) values ('ee7586da-2b02-4cf8-8bf9-23383dd70730', 'Kuhinja');

insert into address (id, country, city, street, number) values ('011131b3-632d-4d68-a279-8e8258a4d4ed', 'Serbia', 'Novi Sad', 'Novosadski put', '116');

insert into accomodation_benefit (accomodation_id, benefit_id) values ('2bd1cc97-96a4-4665-b6e2-6a9231cb7eef','e0a13754-021e-4f61-9d2a-169882622f9b');
insert into accomodation_benefit (accomodation_id, benefit_id) values ('2bd1cc97-96a4-4665-b6e2-6a9231cb7eef','0e447911-5d5f-4412-8ec8-68c21ab3f432');
insert into accomodation_benefit (accomodation_id, benefit_id) values ('2bd1cc97-96a4-4665-b6e2-6a9231cb7eef','ee7586da-2b02-4cf8-8bf9-23383dd70730');

insert into price_interval (id, accomodation_id, amount, interval_start, interval_end) values ('5ca613bf-8d51-41dd-b6ed-913654817850', '2bd1cc97-96a4-4665-b6e2-6a9231cb7eef', 120, '2023-12-11 11:00:00', '2023-12-21 10:00:00');
insert into price_interval (id, accomodation_id, amount, interval_start, interval_end) values ('be829abe-f5fc-4c3a-99d1-c33f6c6ab49a', '2bd1cc97-96a4-4665-b6e2-6a9231cb7eef', 100, '2023-12-21 11:00:00', '2023-12-31 10:00:00');

insert into photo (id, accomodation_id, url) values ('9bf8aa0f-9bbc-450e-9f44-91508f602b79', '2bd1cc97-96a4-4665-b6e2-6a9231cb7eef', 'https://res.cloudinary.com/dso3mvk4p/image/upload/v1683767131/154897592_jmvnb6.jpg');