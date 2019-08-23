/*
Navicat MySQL Data Transfer

Source Server         : 172.16.55.162 _3306
Source Server Version : 50547
Source Host           : 172.16.55.162 :3306
Source Database       : athena

Target Server Type    : MYSQL
Target Server Version : 50547
File Encoding         : 65001

Date: 2018-01-28 20:08:37
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
-- Records of action
-- ----------------------------
INSERT INTO `action` VALUES ('1', 'roleControler', 'sss', 'ss', '0', 'NULL', '11', 'ss', null);
INSERT INTO `action` VALUES ('3', '新增', '1', '3.33333E+14', '0', '1', '111', '1', null);
INSERT INTO `action` VALUES ('4', '删除', '11', '11', '3', '1', '1111', '11', null);
INSERT INTO `action` VALUES ('11', '修改', 'http://www.baidu,com', '修改你的页面', '0', '1', '4', '未知', null);

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
-- Records of area
-- ----------------------------
INSERT INTO `area` VALUES ('110000', 'NULL', '1', '110000', '北京');
INSERT INTO `area` VALUES ('110100', '110000', '2', '110100', '北京市');
INSERT INTO `area` VALUES ('120000', 'NULL', '1', '120000', '天津');
INSERT INTO `area` VALUES ('120100', '120000', '2', '120100', '天津市');

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
-- Records of article
-- ----------------------------
INSERT INTO `article` VALUES ('1', 'test', 'test11', '2018-01-26 19:26:09', '2018-01-26 19:26:15', '1', '1', 'tt');

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
-- Records of backup
-- ----------------------------

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
-- Records of group
-- ----------------------------
INSERT INTO `group` VALUES ('1', '人事部1', '111');
INSERT INTO `group` VALUES ('6', '管理部', '4');
INSERT INTO `group` VALUES ('7', '人事部', '15');
INSERT INTO `group` VALUES ('8', '技术部', '2');
INSERT INTO `group` VALUES ('9', '生产部', '3');
INSERT INTO `group` VALUES ('10', '管理部', '4');
INSERT INTO `group` VALUES ('11', '管理部', '4');
INSERT INTO `group` VALUES ('12', '人事部', '15');
INSERT INTO `group` VALUES ('13', '技术部', '2');
INSERT INTO `group` VALUES ('14', '生产部', '3');
INSERT INTO `group` VALUES ('15', '管理部', '4');
INSERT INTO `group` VALUES ('16', '管理部', '4');
INSERT INTO `group` VALUES ('17', '人事部', '15');
INSERT INTO `group` VALUES ('18', '技术部', '2');
INSERT INTO `group` VALUES ('19', '生产部', '3');
INSERT INTO `group` VALUES ('20', '管理部2', '4');
INSERT INTO `group` VALUES ('21', '管理部', '4');
INSERT INTO `group` VALUES ('22', '人事部', '15');
INSERT INTO `group` VALUES ('23', '技术部', '2');
INSERT INTO `group` VALUES ('24', '生产部', '3');
INSERT INTO `group` VALUES ('25', '管理部', '4');
INSERT INTO `group` VALUES ('26', '管理部', '4');
INSERT INTO `group` VALUES ('27', '人事部', '15');
INSERT INTO `group` VALUES ('28', '技术部', '2');
INSERT INTO `group` VALUES ('29', '生产部', '3');
INSERT INTO `group` VALUES ('30', '管理部', '4');
INSERT INTO `group` VALUES ('31', '管理部', '4');
INSERT INTO `group` VALUES ('32', '人事部', '15');
INSERT INTO `group` VALUES ('33', '技术部', '2');
INSERT INTO `group` VALUES ('34', '生产部', '3');
INSERT INTO `group` VALUES ('35', '管理部', '4');
INSERT INTO `group` VALUES ('36', '管理部', '4');
INSERT INTO `group` VALUES ('37', '人事部', '15');
INSERT INTO `group` VALUES ('38', '技术部', '2');
INSERT INTO `group` VALUES ('39', '生产部', '3');
INSERT INTO `group` VALUES ('40', '管理部', '4');
INSERT INTO `group` VALUES ('41', '管理部', '4');
INSERT INTO `group` VALUES ('42', '人事部', '15');
INSERT INTO `group` VALUES ('43', '技术部', '2');
INSERT INTO `group` VALUES ('44', '生产部', '3');
INSERT INTO `group` VALUES ('45', '管理部', '4');
INSERT INTO `group` VALUES ('46', '管理部', '4');
INSERT INTO `group` VALUES ('47', '人事部', '15');
INSERT INTO `group` VALUES ('48', '技术部', '2');
INSERT INTO `group` VALUES ('49', '生产部', '3');
INSERT INTO `group` VALUES ('50', '管理部', '4');
INSERT INTO `group` VALUES ('51', '管理部', '4');
INSERT INTO `group` VALUES ('52', '人事部', '15');
INSERT INTO `group` VALUES ('53', '技术部', '2');
INSERT INTO `group` VALUES ('54', '生产部', '3');
INSERT INTO `group` VALUES ('55', '管理部', '4');
INSERT INTO `group` VALUES ('56', '管理部', '4');
INSERT INTO `group` VALUES ('57', '人事部', '15');
INSERT INTO `group` VALUES ('58', '技术部', '2');
INSERT INTO `group` VALUES ('59', '生产部', '3');
INSERT INTO `group` VALUES ('60', '管理部', '4');
INSERT INTO `group` VALUES ('61', '管理部', '4');
INSERT INTO `group` VALUES ('62', '人事部', '15');
INSERT INTO `group` VALUES ('63', '技术部', '2');
INSERT INTO `group` VALUES ('64', '生产部', '3');
INSERT INTO `group` VALUES ('65', '管理部', '4');
INSERT INTO `group` VALUES ('66', '管理部', '4');
INSERT INTO `group` VALUES ('67', '人事部', '15');
INSERT INTO `group` VALUES ('68', '技术部', '2');
INSERT INTO `group` VALUES ('69', '生产部', '3');
INSERT INTO `group` VALUES ('70', '管理部', '4');
INSERT INTO `group` VALUES ('71', '管理部', '4');
INSERT INTO `group` VALUES ('72', '人事部', '15');
INSERT INTO `group` VALUES ('73', '技术部', '2');
INSERT INTO `group` VALUES ('74', '生产部', '3');
INSERT INTO `group` VALUES ('75', '管理部', '4');
INSERT INTO `group` VALUES ('76', '管理部', '4');
INSERT INTO `group` VALUES ('77', '人事部', '15');
INSERT INTO `group` VALUES ('78', '技术部', '2');
INSERT INTO `group` VALUES ('79', '生产部', '3');
INSERT INTO `group` VALUES ('80', '管理部', '4');
INSERT INTO `group` VALUES ('81', '管理部', '4');
INSERT INTO `group` VALUES ('90', '2', '3');
INSERT INTO `group` VALUES ('91', '1', '8');
INSERT INTO `group` VALUES ('92', '2', '0');
INSERT INTO `group` VALUES ('93', '4', '65');
INSERT INTO `group` VALUES ('98', '测试', '43');
INSERT INTO `group` VALUES ('99', 'fdf', '43');
INSERT INTO `group` VALUES ('100', '组名', '5');
INSERT INTO `group` VALUES ('101', '管理组', '2');

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
-- Records of menu
-- ----------------------------
INSERT INTO `menu` VALUES ('33FD1267-79BA-4E23-A152-744AF73117E9', '462027E0-0848-41DD-BCC3-025DCAE65555', 'NULL', '查询测试', 'fa fa-desktop', '/SignalRSearch/Index', 'expand', '0', '1', '0', '1', 'NULL', '0000-00-00 00:00:00', 'NULL', '0000-00-00 00:00:00', '9f2ec0797d0f4fe290ab8b09a8302aba', '1', '0', '0000-00-00 00:00:00', 'NULL');
INSERT INTO `menu` VALUES ('38CA5A66-C993-4410-AF95-50489B22949C', '462027E0-0848-41DD-BCC3-025DCAE65555', 'NULL', '查询测试', 'NULL', '/SignalRSearch/Index', 'iframe', '1', '0', '0', '4', 'NULL', '0000-00-00 00:00:00', 'NULL', '0000-00-00 00:00:00', 'NULL', '1', '0', '0000-00-00 00:00:00', 'NULL');
INSERT INTO `menu` VALUES ('e72c75d0-3a69-41ad-b220-13c9a62ec788', '73FD1267-79BA-4E23-A152-744AF73117E9', 'NULL', '数据备份', 'NULL', '/SystemSecurity/DbBackup/Index', 'iframe', '1', '0', '0', '1', 'NULL', '0000-00-00 00:00:00', 'NULL', '0000-00-00 00:00:00', '9f2ec0797d0f4fe290ab8b09a8302aba', '1', '0', '0000-00-00 00:00:00', 'NULL');
INSERT INTO `menu` VALUES ('462027E0-0848-41DD-BCC3-025DCAE65555', 'NULL', 'NULL', '系统管理', 'fa fa-gears', 'NULL', 'expand', '0', '1', '0', '2', 'NULL', '0000-00-00 00:00:00', 'NULL', '0000-00-00 00:00:00', '9f2ec079-7d0f-4fe2-90ab-8b09a8302aba', '1', '0', '0000-00-00 00:00:00', 'NULL');
INSERT INTO `menu` VALUES ('73FD1267-79BA-4E23-A152-744AF73117E9', 'NULL', 'NULL', '系统安全', 'fa fa-desktop', 'NULL', 'expand', '0', '1', '0', '1', 'NULL', '0000-00-00 00:00:00', 'NULL', '0000-00-00 00:00:00', '9f2ec0797d0f4fe290ab8b09a8302aba', '1', '0', '0000-00-00 00:00:00', 'NULL');
INSERT INTO `menu` VALUES ('e7e1cfb2856d492faeaadc8e2962ac76', 'NULL', 'NULL', 'Wiki管理', 'fa fa-gears', 'NULL', 'expand', '0', '0', '0', '2', 'NULL', '0000-00-00 00:00:00', '9f2ec079-7d0f-4fe2-90ab-8b09a8302aba', '0000-00-00 00:00:00', '9f2ec079-7d0f-4fe2-90ab-8b09a8302aba', '0', '0', '0000-00-00 00:00:00', 'NULL');
INSERT INTO `menu` VALUES ('6e5b779e849e459f957f3abef2a277e6', 'e7e1cfb2856d492faeaadc8e2962ac76', 'NULL', '文档管理', 'NULL', '/WikiManage/WikiDocument/Index', 'iframe', '1', '0', '0', '1', 'NULL', '0000-00-00 00:00:00', '9f2ec079-7d0f-4fe2-90ab-8b09a8302aba', '0000-00-00 00:00:00', '9f2ec079-7d0f-4fe2-90ab-8b09a8302aba', '1', '0', '0000-00-00 00:00:00', 'NULL');
INSERT INTO `menu` VALUES ('a3a4742d-ca39-42ec-b95a-8552a6fae579', '73FD1267-79BA-4E23-A152-744AF73117E9', 'NULL', '区域管理', 'NULL', '/SystemManage/Area/Index', 'iframe', '1', '0', '0', '2', 'NULL', '0000-00-00 00:00:00', 'NULL', '0000-00-00 00:00:00', 'NULL', '1', '0', '0000-00-00 00:00:00', 'NULL');
INSERT INTO `menu` VALUES ('64A1C550-2C61-4A8C-833D-ACD0C012260F', '462027E0-0848-41DD-BCC3-025DCAE65555', 'NULL', '系统菜单', 'NULL', '/SystemManage/Module/Index', 'iframe', '1', '0', '0', '7', '测试', '0000-00-00 00:00:00', 'NULL', '0000-00-00 00:00:00', '9f2ec079-7d0f-4fe2-90ab-8b09a8302aba', '1', '0', '0000-00-00 00:00:00', 'NULL');
INSERT INTO `menu` VALUES ('38CA5A66-C993-4410-AF95-50489B22939C', '462027E0-0848-41DD-BCC3-025DCAE65555', 'NULL', '用户管理', 'NULL', '/SystemManage/User/Index', 'iframe', '1', '0', '0', '4', 'NULL', '0000-00-00 00:00:00', 'NULL', '0000-00-00 00:00:00', 'NULL', '1', '0', '0000-00-00 00:00:00', 'NULL');
INSERT INTO `menu` VALUES ('252229DB-35CA-47AE-BDAE-C9903ED5BA7B', '462027E0-0848-41DD-BCC3-025DCAE65555', 'NULL', '数据字典', 'NULL', '/SystemManage/PropertyMapping/Index', 'iframe', '1', '0', '0', '1', 'NULL', '0000-00-00 00:00:00', 'NULL', '0000-00-00 00:00:00', '9f2ec079-7d0f-4fe2-90ab-8b09a8302aba', '1', '0', '0000-00-00 00:00:00', 'NULL');
INSERT INTO `menu` VALUES ('91A6CFAD-B2F9-4294-BDAE-76DECF412C6C', '462027E0-0848-41DD-BCC3-025DCAE65555', 'NULL', '角色管理', 'NULL', '/SystemManage/role/Index', 'iframe', '1', '0', '0', '2', '&nbsp;', '0000-00-00 00:00:00', 'NULL', '0000-00-00 00:00:00', '9f2ec079-7d0f-4fe2-90ab-8b09a8302aba', '1', '0', '0000-00-00 00:00:00', 'NULL');
INSERT INTO `menu` VALUES ('a7f1f2f73ac74b5ba8421ed9b3840439', 'e7e1cfb2856d492faeaadc8e2962ac76', 'NULL', '菜单管理', 'NULL', '/WikiManage/WikiMenu/Index', 'iframe', '1', '0', '0', '2', 'NULL', '0000-00-00 00:00:00', '9f2ec079-7d0f-4fe2-90ab-8b09a8302aba', '0000-00-00 00:00:00', 'NULL', '1', '0', '0000-00-00 00:00:00', 'NULL');
INSERT INTO `menu` VALUES ('F298F868-B689-4982-8C8B-9268CBF0308D', '462027E0-0848-41DD-BCC3-025DCAE65555', 'NULL', '用户组管理', 'NULL', '/SystemManage/Group/Index', 'iframe', '1', '0', '0', '3', 'NULL', '0000-00-00 00:00:00', 'NULL', '0000-00-00 00:00:00', 'NULL', '1', '0', '0000-00-00 00:00:00', 'NULL');
INSERT INTO `menu` VALUES ('96EE855E-8CD2-47FC-A51D-127C131C9FB9', '73FD1267-79BA-4E23-A152-744AF73117E9', 'NULL', '资源管理', 'NULL', '/SystemManage/Resource/Index', 'iframe', '1', '0', '0', '3', 'NULL', '0000-00-00 00:00:00', 'NULL', '0000-00-00 00:00:00', 'NULL', '1', '0', '0000-00-00 00:00:00', 'NULL');

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
-- Records of propertymapping
-- ----------------------------
INSERT INTO `propertymapping` VALUES ('1', 'aaa', 'aa', 'aaaaaaa', '法律的后果合理的湖广会馆骨灰盒和防晒动力火锅户外广告和后果肯定是个很客观和思考过后富商大贾但和恒大华府回复回复富商大贾反应速度广大发给客户开户鸿福路口逗号隔开打工皇帝开了个会肯定会恢复考试大纲打开结果还是两个号', '1');
INSERT INTO `propertymapping` VALUES ('3', 'bb', 'vv', 'vv', 'vv', '0');
INSERT INTO `propertymapping` VALUES ('4', ' 啊打算', '是爱上das', '打算 阿萨德d', ' 阿萨德阿瑟打算大声地s', '0');

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
-- Records of resource
-- ----------------------------
INSERT INTO `resource` VALUES ('1', '系统管理', '1', 'NULL', '1sss', 'NULL', '1', 'fa fa-gears', 'fa arrow', 'NULL', '0', '0', 'NULL');
INSERT INTO `resource` VALUES ('3', '工作流', '1', null, '1', 'NULL', '1', 'fa fa-bar-chart-o', 'fa arrow', null, '0', '0', 'NULL');
INSERT INTO `resource` VALUES ('4', '常用实例', '1', null, '1', 'NULL', '1', 'fa fa-tags', 'fa arrow', null, '0', '0', 'NULL');
INSERT INTO `resource` VALUES ('5', '用户管理', '1', '/SystemManage/User/Index', '1', '1', '2', '发短信', 'NULL', 'NULL', '0', '0', 'NULL');
INSERT INTO `resource` VALUES ('7', '角色管理', '1', '/SystemManage/Role/Index', '1', '1', '2', 'NULL', 'NULL', 'NULL', '0', '0', 'NULL');
INSERT INTO `resource` VALUES ('8', '动作管理', '1', '/SystemManage/Action/Index', '1', '1', '2', 'NULL', 'NULL', 'NULL', '0', '0', 'NULL');
INSERT INTO `resource` VALUES ('9', '资源管理', '1', '/SystemManage/Resource/Index', '1', '1', '2', 'NULL', 'NULL', 'NULL', '0', '0', 'NULL');
INSERT INTO `resource` VALUES ('10', '分组管理', '1', '/SystemManage/Group/Index', '1', '1', '2', 'NULL', 'NULL', 'NULL', '0', '0', 'NULL');
INSERT INTO `resource` VALUES ('11', '数据字典', '1', '/SystemManage/PropertyMapping/Index', '1', '1', '2', 'NULL', 'NULL', 'NULL', '0', '0', 'NULL');
INSERT INTO `resource` VALUES ('12', '区域管理', '1', '/SystemManage/Area/Index', '1', '1', '2', 'NULL', 'NULL', 'NULL', '0', '0', 'NULL');
INSERT INTO `resource` VALUES ('13', '数据库备份', '1', '/SystemManage/Backup/Index', '1', '2', '2', 'NULL', 'NULL', 'NULL', '0', '0', 'NULL');
INSERT INTO `resource` VALUES ('14', '工作流管理', '1', '/WorkflowManage/Workflow/Index', '1', '3', '2', 'NULL', 'NULL', 'NULL', '0', '0', 'NULL');
INSERT INTO `resource` VALUES ('17', '节点动作', '1', '/WorkflowManage/Workflownodeaction/Index', '1', '3', '2', null, null, null, '0', '0', 'NULL');
INSERT INTO `resource` VALUES ('19', '操作者管理', '1', '/WorkflowManage/Workflownodeoperator/Index', '1', '3', '2', null, null, null, '0', '0', 'NULL');
INSERT INTO `resource` VALUES ('20', '文件2-2-1', '2', '1', '1', '4', '2', 'NULL', 'fa arrow', 'NULL', '0', '0', 'NULL');
INSERT INTO `resource` VALUES ('22', '文件3-1-1', '2', '1', '1', '20', '3', 'NULL', 'NULL', 'NULL', '0', '0', 'NULL');
INSERT INTO `resource` VALUES ('23', '文件3-1-2', '2', '1', '1', '20', '3', 'NULL', 'fa arrow', 'NULL', '0', '0', 'NULL');
INSERT INTO `resource` VALUES ('24', '文件3-2-1', '2', '1', '1', '20', '3', 'NULL', 'fa arrow', 'NULL', '0', '0', 'NULL');
INSERT INTO `resource` VALUES ('172', '节点管理', '1', '/WorkflowManage/Workflownode/Index', '1', '3', '2', null, null, null, '0', '0', 'NULL');

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
-- Records of role
-- ----------------------------
INSERT INTO `role` VALUES ('1', '管理员', '1', '0000-00-00 00:00:00');
INSERT INTO `role` VALUES ('25', '普通用户', '33', '0000-00-00 00:00:00');
INSERT INTO `role` VALUES ('26', '会员', '33', '0000-00-00 00:00:00');
INSERT INTO `role` VALUES ('28', '黄金会员', '33', '0000-00-00 00:00:00');
INSERT INTO `role` VALUES ('32', '一般用户', '0', '0000-00-00 00:00:00');
INSERT INTO `role` VALUES ('33', '235', '23', '0000-00-00 00:00:00');

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
-- Records of role_action
-- ----------------------------
INSERT INTO `role_action` VALUES ('3', '32', '1', 'NULL', '0');
INSERT INTO `role_action` VALUES ('5', '26', '1', 'NULL', '0');
INSERT INTO `role_action` VALUES ('6', '26', '3', 'NULL', '0');
INSERT INTO `role_action` VALUES ('8', '32', '4', 'NULL', '0');
INSERT INTO `role_action` VALUES ('9', '1', '1', 'NULL', '0');
INSERT INTO `role_action` VALUES ('10', '1', '3', 'NULL', '0');
INSERT INTO `role_action` VALUES ('11', '1', '4', 'NULL', '0');
INSERT INTO `role_action` VALUES ('12', '25', '1', 'NULL', '0');
INSERT INTO `role_action` VALUES ('13', '25', '3', 'NULL', '0');
INSERT INTO `role_action` VALUES ('14', '25', '4', 'NULL', '0');
INSERT INTO `role_action` VALUES ('15', '32', '3', 'NULL', '0');

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
-- Records of role_resource
-- ----------------------------
INSERT INTO `role_resource` VALUES ('2', '4', '1');
INSERT INTO `role_resource` VALUES ('3', '4', '5');
INSERT INTO `role_resource` VALUES ('4', '4', '7');
INSERT INTO `role_resource` VALUES ('6', '4', '9');
INSERT INTO `role_resource` VALUES ('7', '4', '8');
INSERT INTO `role_resource` VALUES ('8', '4', '10');
INSERT INTO `role_resource` VALUES ('16', '29', '3');
INSERT INTO `role_resource` VALUES ('17', '29', '14');
INSERT INTO `role_resource` VALUES ('18', '29', '17');
INSERT INTO `role_resource` VALUES ('19', '29', '19');
INSERT INTO `role_resource` VALUES ('20', '1', '3');
INSERT INTO `role_resource` VALUES ('21', '1', '14');
INSERT INTO `role_resource` VALUES ('22', '1', '17');
INSERT INTO `role_resource` VALUES ('23', '28', '1');
INSERT INTO `role_resource` VALUES ('25', '28', '7');
INSERT INTO `role_resource` VALUES ('27', '28', '9');
INSERT INTO `role_resource` VALUES ('31', '28', '3');
INSERT INTO `role_resource` VALUES ('32', '28', '14');
INSERT INTO `role_resource` VALUES ('33', '28', '17');
INSERT INTO `role_resource` VALUES ('34', '28', '19');
INSERT INTO `role_resource` VALUES ('35', '28', '172');
INSERT INTO `role_resource` VALUES ('36', '28', '4');
INSERT INTO `role_resource` VALUES ('37', '28', '20');
INSERT INTO `role_resource` VALUES ('38', '28', '22');
INSERT INTO `role_resource` VALUES ('39', '28', '23');
INSERT INTO `role_resource` VALUES ('40', '28', '24');
INSERT INTO `role_resource` VALUES ('41', '26', '1');
INSERT INTO `role_resource` VALUES ('42', '26', '5');
INSERT INTO `role_resource` VALUES ('43', '26', '7');
INSERT INTO `role_resource` VALUES ('44', '26', '8');
INSERT INTO `role_resource` VALUES ('45', '26', '9');
INSERT INTO `role_resource` VALUES ('46', '26', '10');
INSERT INTO `role_resource` VALUES ('47', '26', '11');
INSERT INTO `role_resource` VALUES ('48', '26', '12');
INSERT INTO `role_resource` VALUES ('49', '25', '1');
INSERT INTO `role_resource` VALUES ('50', '25', '5');
INSERT INTO `role_resource` VALUES ('51', '25', '7');
INSERT INTO `role_resource` VALUES ('52', '25', '8');
INSERT INTO `role_resource` VALUES ('53', '25', '9');
INSERT INTO `role_resource` VALUES ('54', '25', '10');
INSERT INTO `role_resource` VALUES ('55', '25', '11');
INSERT INTO `role_resource` VALUES ('56', '25', '12');
INSERT INTO `role_resource` VALUES ('57', '32', '1');
INSERT INTO `role_resource` VALUES ('58', '32', '5');
INSERT INTO `role_resource` VALUES ('59', '32', '7');
INSERT INTO `role_resource` VALUES ('60', '32', '8');
INSERT INTO `role_resource` VALUES ('61', '1', '1');
INSERT INTO `role_resource` VALUES ('62', '1', '7');

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
-- Records of user
-- ----------------------------
INSERT INTO `user` VALUES ('1', 'Athena', 'IHz0EFMvkqR97iRc6bEf9x9Xjr12PrO76kTr0EPQGPs=', ' 管理员,普通用户', ' 管理部,人事部,技术部,生产部,管理部', '60F6BEA894DA2E1D1AF423AFBE412660A5AD8FA32B4A257126EB74DD85F5A793600D32FD70CBF64C2009185A2A62BA5EEFA98B129EBE44B88CA2C0C8C9307A4E', '6A6EB123D3E9F477AF3DC2A17EF16C6368B8F6DED0CD1D7B91AD3AA265FB2226', 'NULL');
INSERT INTO `user` VALUES ('3', 'Athensssiii', 'IHz0EFMvkqR97iRc6bEf9x9Xjr12PrO76kTr0EPQGPs=', ' 管理员,普通用户,会员,黄金会员,一般用户', ' 管理部,人事部,技术部,生产部', 'NULL', 'NULL', 'NULL');
INSERT INTO `user` VALUES ('12', 'ztt', 'GdbXZMWo/zwTPBwOZKzFn4Vd1KENl77QwYRqUlR3ou8=', ' 管理员,普通用户,会员,黄金会员', ' 管理部,人事部,技术部,生产部', 'NULL', 'NULL', 'NULL');
INSERT INTO `user` VALUES ('13', '234', 'jur7a4/Wxu4nyUiSK3kNpbcrbSuAuIH8XsNII4nwTRI=', 'NULL', 'NULL', 'NULL', 'NULL', 'NULL');

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
-- Records of user_group
-- ----------------------------
INSERT INTO `user_group` VALUES ('2', '3', '6');
INSERT INTO `user_group` VALUES ('3', '1', '6');
INSERT INTO `user_group` VALUES ('4', '1', '7');
INSERT INTO `user_group` VALUES ('5', '3', '7');
INSERT INTO `user_group` VALUES ('6', '3', '8');
INSERT INTO `user_group` VALUES ('7', '3', '9');
INSERT INTO `user_group` VALUES ('8', '1', '8');
INSERT INTO `user_group` VALUES ('9', '1', '9');
INSERT INTO `user_group` VALUES ('10', '1', '10');
INSERT INTO `user_group` VALUES ('11', '12', '6');
INSERT INTO `user_group` VALUES ('12', '12', '7');
INSERT INTO `user_group` VALUES ('13', '12', '8');
INSERT INTO `user_group` VALUES ('14', '12', '9');
INSERT INTO `user_group` VALUES ('15', '1', '6');
INSERT INTO `user_group` VALUES ('16', '1', '7');
INSERT INTO `user_group` VALUES ('17', '1', '8');
INSERT INTO `user_group` VALUES ('18', '1', '9');
INSERT INTO `user_group` VALUES ('19', '1', '10');
INSERT INTO `user_group` VALUES ('20', '1', '6');
INSERT INTO `user_group` VALUES ('21', '1', '7');
INSERT INTO `user_group` VALUES ('22', '1', '8');
INSERT INTO `user_group` VALUES ('23', '1', '9');
INSERT INTO `user_group` VALUES ('24', '1', '10');

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
-- Records of user_role
-- ----------------------------
INSERT INTO `user_role` VALUES ('1', '3', '1', '0');
INSERT INTO `user_role` VALUES ('2', '1', '1', '0');
INSERT INTO `user_role` VALUES ('8', '1', '25', '0');
INSERT INTO `user_role` VALUES ('15', '3', '25', '0');
INSERT INTO `user_role` VALUES ('16', '3', '26', '0');
INSERT INTO `user_role` VALUES ('17', '3', '28', '0');
INSERT INTO `user_role` VALUES ('18', '3', '32', '0');
INSERT INTO `user_role` VALUES ('19', '12', '1', '0');
INSERT INTO `user_role` VALUES ('20', '12', '25', '0');
INSERT INTO `user_role` VALUES ('21', '12', '26', '0');
INSERT INTO `user_role` VALUES ('22', '12', '28', '0');

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
-- Records of workflow
-- ----------------------------
INSERT INTO `workflow` VALUES ('3', 'Workflowtest', 'Test', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '1', 'TestTable', 'workflowInstance', '1', '1', 'State1', null);
INSERT INTO `workflow` VALUES ('4', 'wf1', 'Tes22', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '1', 'TestTable', 'WorkflowInstance', '1', '1', 'state2', null);
INSERT INTO `workflow` VALUES ('5', 'wf1', 'Tes22', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '1', 'TestTable', 'WorkflowInstance', '1', '1', 'Fieldname', null);
INSERT INTO `workflow` VALUES ('6', 'wf1', 'Tes22', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '1', 'TestTable', 'WorkflowInstance', '1', '1', 'State', null);
INSERT INTO `workflow` VALUES ('7', 'wf1', 'Tes22', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '1', 'TestTable', 'WorkflowInstance', '1', '1', 'State2', null);
INSERT INTO `workflow` VALUES ('8', 'wf1', 'Tes22', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '1', 'TestTable', 'WorkflowInstance', '1', '1', 'State4', null);
INSERT INTO `workflow` VALUES ('9', 'wf1', 'Tes223', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '1', 'TestTable', 'WorkflowInstance', '1', '1', 'State5', null);
INSERT INTO `workflow` VALUES ('10', 'wf1', 'Tes22', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '1', 'TestTable', 'WorkflowInstance', '1', '1', 'State6', null);
INSERT INTO `workflow` VALUES ('11', 'wf1', '4', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '1', 'TestTable', 'WorkflowInstance', '1', '1', 'State7', null);
INSERT INTO `workflow` VALUES ('12', 'wf1', 'Tes222', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '1', 'TestTable', 'WorkflowInstance', '1', '1', 'State8', null);
INSERT INTO `workflow` VALUES ('13', 'wf1', '45', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '1', 'TestTable', 'WorkflowInstance', '1', '1', 'NULL', null);
INSERT INTO `workflow` VALUES ('14', 'wf1', '34', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '1', 'TestTable', 'WorkflowInstance', '1', '1', 'NULL', null);
INSERT INTO `workflow` VALUES ('15', 'wf1', '3', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '1', 'TestTable', 'WorkflowInstance', '1', '1', 'NULL', null);
INSERT INTO `workflow` VALUES ('16', 'wf1', '23', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '1', 'TestTable', 'WorkflowInstance', '1', '1', 'NULL', null);
INSERT INTO `workflow` VALUES ('17', 'wf1', 'Tes221', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '1', 'TestTable', 'WorkflowInstance', '1', '1', 'NULL', null);
INSERT INTO `workflow` VALUES ('18', 'wf1', 'Tes221', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '1', 'TestTable', 'WorkflowInstance', '1', '1', 'NULL', null);
INSERT INTO `workflow` VALUES ('19', 'wf1', 'Tes221', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '1', 'TestTable', 'WorkflowInstance', '1', '1', 'NULL', null);
INSERT INTO `workflow` VALUES ('20', 'wf1', 'Tes221', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '1', 'TestTable', 'WorkflowInstance', '1', '1', 'NULL', null);
INSERT INTO `workflow` VALUES ('21', 'wf1', 'Tes221', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '1', 'TestTable', 'WorkflowInstance', '1', '1', 'NULL', null);
INSERT INTO `workflow` VALUES ('22', 'wf1', 'Tes221', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '1', 'TestTable', 'WorkflowInstance', '1', '1', 'NULL', null);
INSERT INTO `workflow` VALUES ('23', 'wf1', 'Tes221', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '1', 'TestTable', 'WorkflowInstance', '1', '1', 'NULL', null);
INSERT INTO `workflow` VALUES ('24', '2121', '12343', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '0', '321321', '213', '2', '2', '3213', null);
INSERT INTO `workflow` VALUES ('25', '12312', '123', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '213', '124撒旦法dd', '的阿萨德阿瑟', '2', '2', '单位但我确定是a', null);

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
-- Records of workflowinstances
-- ----------------------------

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
-- Records of workflowinstancetracings
-- ----------------------------

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
-- Records of workflownode
-- ----------------------------
INSERT INTO `workflownode` VALUES ('6', '3', '0', '0', '1', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '1', '1');

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
-- Records of workflownodeaction
-- ----------------------------
INSERT INTO `workflownodeaction` VALUES ('2', '3', 'Insert', '新增', '1', '1', '1', '0000-00-00 00:00:00', '0000-00-00 00:00:00', 'NULL', '1', null, '1', null);
INSERT INTO `workflownodeaction` VALUES ('3', '3', 'Audit', '审核', '1', '3', '1', '0000-00-00 00:00:00', '0000-00-00 00:00:00', 'NULL', '1', null, '1', null);
INSERT INTO `workflownodeaction` VALUES ('4', '7', '阿斯顿撒大s', ' 阿瑟上的 的撒', '0', '0', '2', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '啊实打实的', '2', null, '1', null);

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

-- ----------------------------
-- Records of workflownodeoperator
-- ----------------------------
INSERT INTO `workflownodeoperator` VALUES ('1', '2', '1', '1', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '1', '1', null);
INSERT INTO `workflownodeoperator` VALUES ('3', '3', '1', '1', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '1', '1', null);
INSERT INTO `workflownodeoperator` VALUES ('4', '3', '2', '1', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '1', '1', null);
INSERT INTO `workflownodeoperator` VALUES ('5', '3', '1', '1', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '1', '1', null);
INSERT INTO `workflownodeoperator` VALUES ('6', '2', '22', '2', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '1', '1', null);
INSERT INTO `workflownodeoperator` VALUES ('7', '2', '1', '2', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '1', '1', null);
