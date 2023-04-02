CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(1000) COMMENT 'User Picture',
  coverImg VARCHAR(1000)
) default charset utf8 COMMENT '';
DROP TABLE accounts;

            SELECT
            *
            FROM accounts
            WHERE id = "64013315bd1dde3ace531c41";


CREATE TABLE IF NOT EXISTS keeps(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  name VARCHAR(255) NOT NULL,
  description VARCHAR(1000),
  img VARCHAR(1000) NOT NULL,
  views INT NOT NULL DEFAULT 0,
  kept INT,

  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS vaults(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  name VARCHAR(255) NOT NULL,
  description VARCHAR(1000),
  img VARCHAR(1000) NOT NULL,
  isPrivate BOOLEAN NOT NULL DEFAULT false,

  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

  CREATE TABLE IF NOT EXISTS vaultkeeps(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  vaultId INT NOT NULL,
  keepId INT NOT NULL,

  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE
  ) default charset utf8 COMMENT '';

            SELECT
            acc.*,
            keeps.*,
            vk.id AS vkId
            FROM vaultkeeps vk
            JOIN accounts acc ON vk.creatorId = acc.id
            JOIN keeps keeps ON vk.keepId = keeps.id
            WHERE vk.vaultId = 1;

            SELECT * FROM vaults;
            SELECT * FROM vaultkeeps;

            DROP TABLE keeps;
            DROP TABLE vaults;
            DROP TABLE vaultkeeps;
            DROP TABLE houses;
            DROP TABLE favorites;
            DROP TABLE ingredients;
            DROP TABLE recipes;
            