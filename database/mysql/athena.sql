/*
Navicat MySQL Data Transfer

Source Server         : 172.16.55.162 _3306
Source Server Version : 50547
Source Host           : 172.16.55.162 :3306
Source Database       : athena

Target Server Type    : MYSQL
Target Server Version : 50547
File Encoding         : 65001

Date: 2018-01-28 20:09:07
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for action
-- ----------------------------
DROP TABLE IF EXISTS `action`;
CREATE TABLE `action` (
  `id` int(255) NOT NULL AUTO_INCREMENT,
  `actionname` varchar(255) NOT NULL,
  `actionurl` varchar(255) DEFAULT NULL,
  `actionparam` varchar(255) DEFAULT NULL,
  `actiontype` int(255) DEFAULT NULL,
  `actionowner` varchar(255) DEFAULT NULL,
  `actioncode` int(255) NOT NULL,
  `controlername` varchar(255) NOT NULL,
  `actiondescription` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=12 DEFAULT CHARSET=gbk;

-- ----------------------------
-- Table structure for area
-- ----------------------------
DROP TABLE IF EXISTS `area`;
CREATE TABLE `area` (
  `id` int(255) NOT NULL,
  `parentid` varchar(2048) DEFAULT NULL,
  `layers` int(255) DEFAULT NULL,
  `encode` varchar(255) DEFAULT NULL,
  `fullname` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=120101 DEFAULT CHARSET=gbk;

-- ----------------------------
-- Table structure for article
-- ----------------------------
DROP TABLE IF EXISTS `article`;
CREATE TABLE `article` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(255) NOT NULL,
  `[content]` varchar(2048) DEFAULT NULL,
  `inserttime` datetime DEFAULT NULL,
  `edittime` datetime DEFAULT NULL,
  `type` int(11) DEFAULT NULL,
  `SortID` int(11) DEFAULT NULL,
  `author` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=2 DEFAULT CHARSET=gbk;

-- ----------------------------
-- Table structure for backup
-- ----------------------------
DROP TABLE IF EXISTS `backup`;
CREATE TABLE `backup` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `databasename` varchar(50) NOT NULL,
  `backupname` varchar(50) NOT NULL,
  `backupsize` varchar(50) DEFAULT NULL,
  `backuptime` datetime DEFAULT NULL,
  `backuppersonnel` varchar(50) DEFAULT NULL,
  `instructions` varchar(1024) DEFAULT NULL,
  `absolutepath` varchar(1024) DEFAULT NULL,
  `relativepath` varchar(1024) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=gbk;

-- ----------------------------
-- Table structure for group
-- ----------------------------
DROP TABLE IF EXISTS `group`;
CREATE TABLE `group` (
  `id` int(255) NOT NULL,
  `groupname` varchar(255) NOT NULL,
  `groupcode` int(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=gbk;

-- ----------------------------
-- Table structure for menu
-- ----------------------------
DROP TABLE IF EXISTS `menu`;
CREATE TABLE `menu` (
  `Id` varchar(255) NOT NULL,
  `ParentId` varchar(255) DEFAULT NULL,
  `EnCode` varchar(255) DEFAULT NULL,
  `Name` varchar(255) DEFAULT NULL,
  `Icon` varchar(255) DEFAULT NULL,
  `UrlAddress` varchar(255) DEFAULT NULL,
  `OpenTarget` varchar(255) DEFAULT NULL,
  `IsMenu` varchar(255) NOT NULL,
  `IsExpand` varchar(255) NOT NULL,
  `IsPublic` varchar(255) NOT NULL,
  `SortCode` int(255) NOT NULL,
  `Description` varchar(255) DEFAULT NULL,
  `CreationTime` datetime NOT NULL,
  `CreateUserId` varchar(255) DEFAULT NULL,
  `LastModifyTime` datetime DEFAULT NULL,
  `LastModifyUserId` varchar(255) DEFAULT NULL,
  `IsEnabled` varchar(255) NOT NULL,
  `IsDeleted` varchar(255) NOT NULL,
  `DeleteTime` datetime DEFAULT NULL,
  `DeleteUserId` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=MyISAM DEFAULT CHARSET=gbk;

-- ----------------------------
-- Table structure for propertymapping
-- ----------------------------
DROP TABLE IF EXISTS `propertymapping`;
CREATE TABLE `propertymapping` (
  `id` int(255) NOT NULL,
  `propertyName` varchar(255) DEFAULT NULL,
  `propertyValue` varchar(255) DEFAULT NULL,
  `propertyMeaning` varchar(255) DEFAULT NULL,
  `remark` varchar(1024) DEFAULT NULL,
  `parentId` int(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=gbk;

-- ----------------------------
-- Table structure for resource
-- ----------------------------
DROP TABLE IF EXISTS `resource`;
CREATE TABLE `resource` (
  `id` int(255) NOT NULL,
  `resourcename` varchar(255) NOT NULL,
  `resourcetype` int(255) NOT NULL,
  `resourceurl` varchar(1024) DEFAULT NULL,
  `resourcescript` varchar(1024) DEFAULT NULL,
  `resourceowner` varchar(1024) DEFAULT NULL,
  `resourceleval` int(255) DEFAULT NULL,
  `resourceleftico` varchar(255) DEFAULT NULL,
  `resourcerightico` varchar(255) DEFAULT NULL,
  `resourceclass` varchar(255) DEFAULT NULL,
  `resourcenumber` int(255) DEFAULT NULL,
  `order` int(255) DEFAULT NULL,
  `description` varchar(2048) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=gbk;

-- ----------------------------
-- Table structure for role
-- ----------------------------
DROP TABLE IF EXISTS `role`;
CREATE TABLE `role` (
  `id` int(255) NOT NULL,
  `rolename` varchar(255) DEFAULT NULL,
  `rolecode` varchar(255) DEFAULT NULL,
  `roleexpiretime` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=gbk;

-- ----------------------------
-- Table structure for role_action
-- ----------------------------
DROP TABLE IF EXISTS `role_action`;
CREATE TABLE `role_action` (
  `id` int(11) NOT NULL,
  `roleid` int(11) NOT NULL,
  `actionid` int(11) NOT NULL,
  `controlername` varchar(255) DEFAULT NULL,
  `actionscode` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=gbk;

-- ----------------------------
-- Table structure for role_resource
-- ----------------------------
DROP TABLE IF EXISTS `role_resource`;
CREATE TABLE `role_resource` (
  `id` int(11) NOT NULL,
  `roleid` int(11) NOT NULL,
  `resoureceid` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=gbk;

-- ----------------------------
-- Table structure for user
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `id` int(11) NOT NULL,
  `username` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `rolelist` varchar(255) DEFAULT NULL,
  `grouplist` varchar(255) DEFAULT NULL,
  `pubkey` varchar(255) DEFAULT NULL,
  `prikey` varchar(255) DEFAULT NULL,
  `photo` char(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=gbk;

-- ----------------------------
-- Table structure for user_group
-- ----------------------------
DROP TABLE IF EXISTS `user_group`;
CREATE TABLE `user_group` (
  `id` int(11) NOT NULL,
  `userid` int(11) NOT NULL,
  `groupid` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=gbk;

-- ----------------------------
-- Table structure for user_role
-- ----------------------------
DROP TABLE IF EXISTS `user_role`;
CREATE TABLE `user_role` (
  `id` int(11) NOT NULL,
  `userid` int(11) NOT NULL,
  `roleid` int(11) NOT NULL,
  `rolescode` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=gbk;

-- ----------------------------
-- Table structure for workflow
-- ----------------------------
DROP TABLE IF EXISTS `workflow`;
CREATE TABLE `workflow` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `wfname` varchar(255) NOT NULL,
  `wfmemo` varchar(255) DEFAULT NULL,
  `wfbegintime` datetime DEFAULT NULL,
  `wfstoptime` datetime DEFAULT NULL,
  `wfflag` int(11) NOT NULL,
  `wfownertable` varchar(255) DEFAULT NULL,
  `wfinstancestable` varchar(255) DEFAULT NULL,
  `wfstatus` int(11) DEFAULT NULL,
  `wflock` int(11) DEFAULT NULL,
  `wffieldname` varchar(255) DEFAULT NULL,
  `wfjsonstr` varchar(2048) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=26 DEFAULT CHARSET=gbk;

-- ----------------------------
-- Table structure for workflowinstances
-- ----------------------------
DROP TABLE IF EXISTS `workflowinstances`;
CREATE TABLE `workflowinstances` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `wfid` int(11) NOT NULL,
  `ownertabledataid` int(11) NOT NULL,
  `currentnodeid` int(11) NOT NULL,
  `status` int(11) DEFAULT NULL,
  `datalock` int(255) DEFAULT NULL,
  `nodcode` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=gbk;

-- ----------------------------
-- Table structure for workflowinstancetracings
-- ----------------------------
DROP TABLE IF EXISTS `workflowinstancetracings`;
CREATE TABLE `workflowinstancetracings` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `instanceid` int(11) NOT NULL,
  `startnode` varchar(255) NOT NULL,
  `endnode` varchar(255) NOT NULL,
  `executeaction` varchar(255) DEFAULT NULL,
  `executer` varchar(255) DEFAULT NULL,
  `executetime` datetime DEFAULT NULL,
  `remark` varchar(1024) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=gbk;

-- ----------------------------
-- Table structure for workflownode
-- ----------------------------
DROP TABLE IF EXISTS `workflownode`;
CREATE TABLE `workflownode` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `wfid` int(11) DEFAULT NULL,
  `wfnodename` varchar(255) DEFAULT NULL,
  `wfnodememo` varchar(255) DEFAULT NULL,
  `wfnodeflag` int(11) DEFAULT NULL,
  `wfnodebegintime` datetime DEFAULT NULL,
  `wfnodeendtime` datetime DEFAULT NULL,
  `wfnodestatus` int(11) DEFAULT NULL,
  `wfnodelock` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=7 DEFAULT CHARSET=gbk;

-- ----------------------------
-- Table structure for workflownodeaction
-- ----------------------------
DROP TABLE IF EXISTS `workflownodeaction`;
CREATE TABLE `workflownodeaction` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `wfid` int(11) DEFAULT NULL,
  `nodeactionname` varchar(255) NOT NULL,
  `nodeactionmemo` varchar(255) DEFAULT NULL,
  `currentnodeid` int(11) NOT NULL,
  `nextnodeid` int(11) NOT NULL,
  `nastatus` int(11) NOT NULL,
  `begintime` datetime DEFAULT NULL,
  `endtime` datetime DEFAULT NULL,
  `nacondition` varchar(1024) DEFAULT NULL,
  `nalock` int(11) DEFAULT NULL,
  `nodeactioncode` int(11) DEFAULT NULL,
  `nodetype` int(11) DEFAULT NULL,
  `actionid` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=gbk;

-- ----------------------------
-- Table structure for workflownodeoperator
-- ----------------------------
DROP TABLE IF EXISTS `workflownodeoperator`;
CREATE TABLE `workflownodeoperator` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nodeactionid` int(11) NOT NULL,
  `operatorid` int(11) NOT NULL,
  `operatortype` int(11) NOT NULL,
  `begintime` datetime DEFAULT NULL,
  `endtime` datetime DEFAULT NULL,
  `operatorstatus` int(11) DEFAULT NULL,
  `operatorlock` int(11) DEFAULT NULL,
  `nodeoperator` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=8 DEFAULT CHARSET=gbk;
