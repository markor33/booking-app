insert into benefit (id, name) values ('e0a13754-021e-4f61-9d2a-169882622f9b', 'WiFi');
insert into benefit (id, name) values ('0e447911-5d5f-4412-8ec8-68c21ab3f432', 'TV');
insert into benefit (id, name) values ('ee7586da-2b02-4cf8-8bf9-23383dd70730', 'Kuhinja');

insert into address (id, country, city, number, street) values ('011131b3-632d-4d68-a279-8e8258a4d4ed', 'Serbia', 'Novi Sad', 'Novosadski put', '116');

insert into accomodation (id, host_id, location_id, description, min_guests, max_guests, price_type, weekend_increase, created) values ('2bd1cc97-96a4-4665-b6e2-6a9231cb7eef', '7488610c-df65-4608-9c59-f0c78ab24352', '011131b3-632d-4d68-a279-8e8258a4d4ed', 'Fin smestaj.', 1, 4, 0, 10, '2023-10-01 10:00:00');

insert into accomodation_benefit (accomodation_id, benefit_id) values ('2bd1cc97-96a4-4665-b6e2-6a9231cb7eef','e0a13754-021e-4f61-9d2a-169882622f9b');
insert into accomodation_benefit (accomodation_id, benefit_id) values ('2bd1cc97-96a4-4665-b6e2-6a9231cb7eef','0e447911-5d5f-4412-8ec8-68c21ab3f432');
insert into accomodation_benefit (accomodation_id, benefit_id) values ('2bd1cc97-96a4-4665-b6e2-6a9231cb7eef','ee7586da-2b02-4cf8-8bf9-23383dd70730');

insert into price_interval (id, accomodation_id, amount, interval_start, interval_end) values ('5ca613bf-8d51-41dd-b6ed-913654817850', '2bd1cc97-96a4-4665-b6e2-6a9231cb7eef', 12000, '2023-12-11 10:00:00', '2023-12-21 10:00:00');
insert into price_interval (id, accomodation_id, amount, interval_start, interval_end) values ('be829abe-f5fc-4c3a-99d1-c33f6c6ab49a', '2bd1cc97-96a4-4665-b6e2-6a9231cb7eef', 12000, '2023-12-21 10:00:00', '2023-12-31 10:00:00');

insert into photo (id, accomodation_id, url) values ('9bf8aa0f-9bbc-450e-9f44-91508f602b79', '2bd1cc97-96a4-4665-b6e2-6a9231cb7eef', 'https://res.cloudinary.com/disvuvajt/image/upload/v1680096546/frankfurt_ogmxqz.jpg');
insert into photo (id, accomodation_id, url) values ('80062a43-fe62-4945-a5a6-629be1e87585', '2bd1cc97-96a4-4665-b6e2-6a9231cb7eef', 'https://res.cloudinary.com/disvuvajt/image/upload/v1680776365/Novi Sad_adixu.jpg');