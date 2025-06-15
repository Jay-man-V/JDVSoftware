/****** Object:  Database [UnitTesting]    Script Date: 12/05/2018 23:06:16 ******/
USE [master]

CREATE DATABASE [UnitTesting]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'UnitTesting', FILENAME = N'C:\Database\UnitTesting.mdf' , SIZE = 1024KB , MAXSIZE = UNLIMITED, FILEGROWTH = 2048KB )
 LOG ON 
( NAME = N'UnitTesting_log', FILENAME = N'C:\Database\UnitTesting_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 2048KB )

ALTER DATABASE [UnitTesting] SET AUTO_CLOSE OFF;
