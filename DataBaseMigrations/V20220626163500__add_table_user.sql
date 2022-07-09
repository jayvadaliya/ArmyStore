CREATE TABLE IF NOT EXISTS `user` (
    `id` int(11) NOT NULL AUTO_INCREMENT,
    `name` varchar(50) NOT NULL,
    `password_hash` blob DEFAULT NULL,
    `password_salt` blob DEFAULT NULL,
    `updated_on` datetime(3) NOT NULL,
    PRIMARY KEY (`id`));