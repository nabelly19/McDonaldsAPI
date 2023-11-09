
if (exists(SELECT * from sys.databases where name  = 'McDatabase'))
	drop database McDatabase
go

create database McDatabase
go

use McDatabase
go

create table
	