CREATE ROLE eco_user;
ALTER
USER eco_user WITH PASSWORD 'eco_user';
CREATE
DATABASE ecoekb;
GRANT ALL PRIVILEGES ON DATABASE
ecoekb TO eco_user;