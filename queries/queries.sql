-- Queries used in the course 
--------------------------------------------------------------------------------------------

select * 
from SensorLocations
order by population desc

--------------------------------------------------------------------------------------------

create table LegacySensorReadings(
   Id int not null identity(1,1),
   ReadingId varchar(36) not null primary key,
   ReadingDateTime datetime not null default getdate(),
   LocationId smallint not null,
   PollutionLevel tinyint not null
)

--------------------------------------------------------------------------------------------

create table IncrementalWatermarks(
   TableName varchar(50) not null primary key,
   LastInsertedId int not null
)

--------------------------------------------------------------------------------------------

insert into IncrementalWatermarks(TableName, LastInsertedId)
values('LegacySensorReadings', 0)

--------------------------------------------------------------------------------------------

select * 
from IncrementalWatermarks

--------------------------------------------------------------------------------------------

create proc spUpdateIncrementalWatermark
   @tableName varchar(50),
   @id int
as
set nocount on;

   update IncrementalWatermarks
   set LastInsertedId = @id
   where TableName = @tableName

go

--------------------------------------------------------------------------------------------

create table SensorReadings(
   ReadingId varchar(36) not null primary key,
   ReadingDateTime datetime not null,
   PollutionLevelId tinyint not null,
   PollutionLevel varchar(20) not null,
   LocationId smallint not null,
   City varchar(100) not null default 'UNKNOWN',
   Country varchar(100) not null default 'UNKNOWN',
   Population int not null default 0,
   Latitude decimal(9,6) not null default 0.0,
   Longitude decimal(9,6) not null default 0.0
)

--------------------------------------------------------------------------------------------

select *
from IncrementalWatermarks
where TableName = 'LegacySensorReadings'

--------------------------------------------------------------------------------------------

select max(Id) as MaxId 
from LegacySensorReadings

--------------------------------------------------------------------------------------------

select * 
from LegacySensorReadings
where id > @{activity('LegacySensorReadingLastInserted').output.firstRow.LastInsertedId}
and id <= @{activity('MaxLegacySensorReadingId').output.firstRow.MaxId}

--------------------------------------------------------------------------------------------

select * 
from IncrementalWatermarks

--------------------------------------------------------------------------------------------

select * 
from LegacySensorReadings

--------------------------------------------------------------------------------------------

SELECT Id, Country
FROM SensorLocations

--------------------------------------------------------------------------------------------

SELECT a.readingId, a.readingDateTime, a.locationId, a.pollutionLevel
INTO BlobStorage
FROM PollutionDataStream a TIMESTAMP BY (cast(a.ReadingDateTime as DateTime))
INNER JOIN SensorLocations b on cast(a.LocationId as bigint) = cast(b.Id as bigint)

--------------------------------------------------------------------------------------------