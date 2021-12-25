DROP PROCEDURE IF EXISTS AddTableForeignKey;

DELIMITER $$
CREATE PROCEDURE AddTableForeignKey
( in tableName VARCHAR(128), -- If null an exception will be thrown
  in keyName VARCHAR(128), -- If null an exception will be thrown
  in fieldName VARCHAR(128), -- If null an exception will be thrown
  in referenceDefinition VARCHAR(1024), -- E.g. 'REFERENCES `organisation` (`id`)'
  in extraDefinition VARCHAR(1024) -- E.g. 'ON DELETE NO ACTION ON UPDATE NO ACTION'
)
BEGIN

DECLARE tmpSql varchar(4096) default '';

if not exists (	
	select *
	from `information_schema`.`TABLE_CONSTRAINTS`
	where `CONSTRAINT_NAME` = keyName
	and `CONSTRAINT_TYPE` = 'FOREIGN KEY'
    and `TABLE_NAME` = tableName
	and `CONSTRAINT_SCHEMA` = schema()
)
then
	-- create;
	set tmpSql = concat( 'ALTER TABLE `', tableName, '` ADD CONSTRAINT `', keyName, '` FOREIGN KEY (`', fieldName, '`) REFERENCES ', referenceDefinition, extraDefinition);
	
	set @sql = tmpSql;
	prepare tmp_stmt from @sql;
	execute tmp_stmt;
	deallocate prepare tmp_stmt;
end if;

END $$

DELIMITER ;