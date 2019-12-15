-- MySQL dump 10.13  Distrib 8.0.13, for Win64 (x86_64)
--
-- Host: localhost    Database: smallvendor
-- ------------------------------------------------------
-- Server version	8.0.13

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `customer`
--

DROP TABLE IF EXISTS `customer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `customer` (
  `customeruuid` varchar(36) NOT NULL,
  `CustomerName` varchar(30) NOT NULL,
  `CustomerPhone` varchar(30) NOT NULL,
  `DeliveryAddress` varchar(80) NOT NULL,
  PRIMARY KEY (`customeruuid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `order`
--

DROP TABLE IF EXISTS `order`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `order` (
  `orderuuid` varchar(36) NOT NULL,
  `OrderDate` datetime DEFAULT NULL,
  `useruuid` varchar(36) DEFAULT NULL,
  `DeliveryFee` float DEFAULT NULL,
  `TotalPrice` float DEFAULT NULL,
  `Discount` float DEFAULT NULL,
  `CustomerName` varchar(30) DEFAULT NULL,
  `CustomerPhone` varchar(30) DEFAULT NULL,
  `DeliveryAddress` varchar(80) DEFAULT NULL,
  `PayMethod` int(11) DEFAULT NULL,
  `Confirmed` tinyint(4) DEFAULT NULL,
  `Comment` varchar(40) DEFAULT NULL,
  `Deleted` tinyint(4) DEFAULT '0',
  PRIMARY KEY (`orderuuid`),
  KEY `useruid_idx` (`useruuid`),
  CONSTRAINT `usruid` FOREIGN KEY (`useruuid`) REFERENCES `user` (`uuid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `order_product`
--

DROP TABLE IF EXISTS `order_product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `order_product` (
  `orderuuid` varchar(36) NOT NULL,
  `productuuid` varchar(36) NOT NULL,
  `count` float DEFAULT NULL,
  `Deleted` tinyint(4) DEFAULT '0',
  `UnitPrice` float DEFAULT NULL,
  PRIMARY KEY (`orderuuid`,`productuuid`),
  KEY `FK_productuuid_idx` (`productuuid`),
  CONSTRAINT `FK_orderuuid` FOREIGN KEY (`orderuuid`) REFERENCES `order` (`orderuuid`),
  CONSTRAINT `FK_productuuid` FOREIGN KEY (`productuuid`) REFERENCES `product` (`productuuid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `pay_record`
--

DROP TABLE IF EXISTS `pay_record`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `pay_record` (
  `orderuuid` varchar(36) NOT NULL,
  `transaction_code` varchar(45) NOT NULL,
  `transaction_date` datetime NOT NULL,
  `pay_amount` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`transaction_code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `product`
--

DROP TABLE IF EXISTS `product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `product` (
  `productuuid` varchar(36) NOT NULL,
  `useruuid` varchar(36) NOT NULL,
  `name` varchar(40) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Photo1` mediumblob,
  `Photo2` mediumblob,
  `Photo3` mediumblob,
  `description` varchar(80) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `price` float DEFAULT '0',
  `Photo1ContentType` varchar(10) DEFAULT NULL,
  `Photo2ContentType` varchar(10) DEFAULT NULL,
  `Photo3ContentType` varchar(10) DEFAULT NULL,
  `MeasureType` int(11) DEFAULT NULL,
  `Deleted` tinyint(4) DEFAULT '0',
  PRIMARY KEY (`productuuid`),
  KEY `useruid_idx` (`useruuid`),
  CONSTRAINT `useruid` FOREIGN KEY (`useruuid`) REFERENCES `user` (`uuid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `user` (
  `uuid` varchar(36) NOT NULL,
  `name` varchar(40) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `address` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `IDCARD` varchar(18) DEFAULT NULL,
  `Photo1` mediumblob,
  `Photo2` mediumblob,
  `Scope` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `DeliveryFee` float DEFAULT '0',
  `PhoneNumber` varchar(30) DEFAULT NULL,
  `alipay` varchar(45) DEFAULT NULL,
  `wechat` varchar(45) DEFAULT NULL,
  `loginid` varchar(30) DEFAULT NULL,
  `password` varchar(60) DEFAULT NULL,
  `Discount` float DEFAULT NULL,
  `Photo1ContentType` varchar(10) DEFAULT NULL,
  `Photo2ContentType` varchar(10) DEFAULT NULL,
  `alipayBarcode` mediumblob,
  `wechatBarcode` mediumblob,
  `alipayBarcodeContentType` varchar(10) DEFAULT NULL,
  `wechatBarcodeContentType` varchar(10) DEFAULT NULL,
  `Deleted` tinyint(4) DEFAULT '0',
  `busytime` varchar(50) DEFAULT NULL,
  `securityQuestion1` varchar(70) DEFAULT NULL,
  `securityAnswer1` varchar(70) DEFAULT NULL,
  `securityQuestion2` varchar(70) DEFAULT NULL,
  `securityAnswer2` varchar(70) DEFAULT NULL,
  `securityQuestion3` varchar(70) DEFAULT NULL,
  `securityAnswer3` varchar(70) DEFAULT NULL,
  PRIMARY KEY (`uuid`),
  KEY `loginid` (`loginid`),
  KEY `PhoneNumber` (`PhoneNumber`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-11-10 22:20:42
