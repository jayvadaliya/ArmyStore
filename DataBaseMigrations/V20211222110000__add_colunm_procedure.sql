DROP PROCEDURE IF EXISTS AddTableColumn;

DELIMITER $$
CREATE PROCEDURE AddTableColumn
( in tableName varchar(128) -- If null an exception will be thrown
, in columnName varchar(128) -- If null an exception will be thrown
, in columnDefinition varchar(1024) -- E.g. 'int not null default 1' (Can include comment here if columnComment is null.)
, in ifPresent enum('leaveUnchanged', 'dropAndReplace', 'modifyExisting') -- null=leaveUnchanged
)
BEGIN

DECLARE doDrop tinyint(1) default null;
DECLARE doAdd tinyint(1) default null;
DECLARE doModify tinyint(1) default null;
DECLARE tmpSql varchar(4096) default '';

set ifPresent = coalesce(ifPresent, 'leaveUnchanged');

-- select schemaName, ifPresent;
if exists (	
	select *
	from `information_schema`.`COLUMNS`
	where `COLUMN_NAME` = columnName
	and `TABLE_NAME` = tableName
	and `TABLE_SCHEMA` = schema()
)
then
	-- exists;
	if (ifPresent = 'leaveUnchanged')
	then
		set doDrop = 0;
		set doAdd = 0;
		set doModify = 0;
	elseif (ifPresent = 'dropAndReplace')
	then
		set doDrop = 1;
		set doAdd = 1;
		set doModify = 0;
	elseif (ifPresent = 'modifyExisting')
	then
		set doDrop = 0;
		set doAdd = 0;
		set doModify = 1;
	end if;
else
	-- does not exist;
	set doDrop = 0;
	set doAdd = 1;
	set doModify = 0;
end if;

-- select doDrop, doAdd, doModify;
if (doDrop = 1)
then
	set tmpSql = concat( 'alter table `', tableName, '` drop column `', columnName, '` ');
    
	set @sql = tmpSql;
	prepare tmp_stmt from @sql;
	execute tmp_stmt;
	deallocate prepare tmp_stmt;
end if;

if (doAdd = 1)
then
	set tmpSql = concat( 'alter table `', tableName, '` add column `', columnName, '` ', columnDefinition);
	
	set @sql = tmpSql;
	prepare tmp_stmt from @sql;
	execute tmp_stmt;
	deallocate prepare tmp_stmt;
end if;

if (doModify = 1)
then
	set tmpSql = concat( 'alter table `', tableName, '` modify column `', columnName, '` ', columnDefinition);
	    
	set @sql = tmpSql;
	prepare tmp_stmt from @sql;
	execute tmp_stmt;
	deallocate prepare tmp_stmt;
end if;

END $$
DELIMITER ;