CREATE TABLE IF NOT EXISTS `product` (
    `id` int(11) NOT NULL AUTO_INCREMENT,
    `name` varchar(50) NOT NULL,
    `price` varchar(20) DEFAULT NULL,
    `image_url` varchar(250) DEFAULT NULL,
    `status` tinyint(4) NOT NULL DEFAULT 1,
    `updated_on` datetime(3) NOT NULL,
    PRIMARY KEY (`id`));

CREATE TABLE IF NOT EXISTS `product_metadata` (
    `id` int(11) NOT NULL,
    `description` varchar(50) NOT NULL,
    `specifications` varchar(500) DEFAULT NULL,
    PRIMARY KEY (`id`));
CALL AddTableForeignKey('product_metadata', 'fk_product_metadata_on_product', 'id', '`product` (`id`)', 'ON DELETE NO ACTION ON UPDATE NO ACTION');