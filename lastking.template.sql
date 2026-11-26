/*
MySQL Data Transfer
Source Host: localhost
Source Database: lastking
Target Host: localhost
Target Database: lastking
Date: 2015-05-27 ���� 5:13:42
*/

SET FOREIGN_KEY_CHECKS=0;
-- ----------------------------
-- Table structure for account_info
-- ----------------------------
CREATE TABLE `account_info` (
  `no` int(11) NOT NULL AUTO_INCREMENT,
  `is_admin` char(1) DEFAULT 'N',
  `u_id` varchar(12) DEFAULT NULL,
  `u_pwd` varchar(255) DEFAULT NULL,
  `email` varchar(30) DEFAULT NULL,
  `u_ip` varchar(30) DEFAULT NULL,
  `c_date` datetime DEFAULT NULL,
  `UPD_DATE` datetime DEFAULT NULL,
  `DC_YN` varchar(1) DEFAULT 'N',
  PRIMARY KEY (`no`)
) ENGINE=InnoDB AUTO_INCREMENT=113 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for gotocmd
-- ----------------------------
CREATE TABLE `gotocmd` (
  `IDX` int(255) NOT NULL AUTO_INCREMENT,
  `VilgNum` int(11) NOT NULL DEFAULT '-1',
  `GoName` varchar(255) NOT NULL,
  `Map` varchar(255) NOT NULL,
  `LocX` int(255) NOT NULL,
  `LocY` int(255) NOT NULL,
  PRIMARY KEY (`IDX`),
  KEY `vilgNum` (`VilgNum`),
  KEY `Map` (`Map`),
  CONSTRAINT `Map` FOREIGN KEY (`Map`) REFERENCES `mapmaster` (`MAPNAME`),
  CONSTRAINT `vilgNum` FOREIGN KEY (`VilgNum`) REFERENCES `vilagemaster` (`IDX`)
) ENGINE=InnoDB AUTO_INCREMENT=90 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for itemmaster
-- ----------------------------
CREATE TABLE `itemmaster` (
  `INDEX` int(255) DEFAULT NULL,
  `ID` int(11) DEFAULT NULL,
  `NAME` varchar(30) DEFAULT NULL,
  `DURABILITY` int(255) DEFAULT NULL,
  `BUYPRICE` int(20) DEFAULT NULL,
  `SELLPRICE` int(20) DEFAULT NULL,
  `LEVEL_REQ` int(10) DEFAULT NULL,
  `CLASS_REQ` int(10) DEFAULT NULL,
  `STR_REQ` int(10) DEFAULT NULL,
  `MEN_REQ` int(10) DEFAULT NULL,
  `DEX_REQ` int(10) DEFAULT NULL,
  `VIT_REQ` int(10) DEFAULT NULL,
  `STR` int(10) DEFAULT NULL,
  `MEN` int(10) DEFAULT NULL,
  `DEX` int(10) DEFAULT NULL,
  `VIT` int(10) DEFAULT NULL,
  `AC` int(10) DEFAULT NULL,
  `HIT` int(10) DEFAULT NULL,
  `DAM` int(10) DEFAULT NULL,
  `HP` int(10) DEFAULT NULL,
  `MP` int(10) DEFAULT NULL,
  `VALID_TIME` int(11) DEFAULT NULL,
  `FLAVORT_TEXT` varchar(50) DEFAULT NULL,
  `DESC_TEXT` varchar(50) DEFAULT NULL,
  `Is_Break` varchar(1) DEFAULT NULL,
  `Is_Stackable` varchar(1) DEFAULT NULL,
  `Is_Blessed` varchar(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for lunchermaster
-- ----------------------------
CREATE TABLE `lunchermaster` (
  `VER` varchar(50) NOT NULL,
  `STATE` varchar(1) NOT NULL DEFAULT '0',
  `IP` varchar(15) NOT NULL,
  `PORT` int(11) NOT NULL,
  `LIMIT_CLIENT` int(255) DEFAULT '1',
  `INTROWEB_PATH` varchar(100) NOT NULL DEFAULT '',
  `HOMEWEB_PATH` varchar(100) NOT NULL DEFAULT '',
  `NOTICE` varchar(255) NOT NULL,
  `NOTICE_DATE` datetime DEFAULT NULL,
  `DOWNLOADER_PATH` varchar(100) NOT NULL,
  `LUNCHER_PATH` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for mapmaster
-- ----------------------------
CREATE TABLE `mapmaster` (
  `IDX` int(255) NOT NULL,
  `VilgNum` int(11) DEFAULT '-1',
  `MAPNAME` varchar(255) NOT NULL,
  `FILENAME` varchar(255) NOT NULL,
  `Fog` tinyint(255) DEFAULT NULL,
  `Sound` int(255) DEFAULT NULL,
  `LimitLevel` int(255) DEFAULT NULL,
  `isPK` varchar(10) NOT NULL COMMENT '0: 피케이불가능\r\n1:피케이가능 수치안오름\r\n2:피케이가능 수치 오름',
  `isOpen` varchar(10) DEFAULT '1',
  `WorldTime` varchar(10) DEFAULT '0',
  `isLegend` varchar(10) DEFAULT '0',
  `DC_YN` varchar(10) DEFAULT 'TRUE',
  PRIMARY KEY (`IDX`),
  KEY `MAPNAME` (`MAPNAME`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for monstermaster
-- ----------------------------
CREATE TABLE `monstermaster` (
  `M_INDEX` int(11) NOT NULL,
  `ENG_NAME` varchar(50) DEFAULT NULL,
  `KOR_NAME` varchar(50) DEFAULT NULL,
  `SPRITE_ID` int(11) DEFAULT NULL,
  `COLOR` tinyint(3) DEFAULT '0',
  `WALKSPEED` int(11) DEFAULT NULL,
  `ATTACKSPEED` int(11) DEFAULT NULL,
  `XP` int(11) DEFAULT '1',
  `AC` int(11) DEFAULT '1',
  `HIT` int(11) DEFAULT '1',
  `DAM` int(11) DEFAULT '1',
  `HP` int(11) DEFAULT '10',
  `LEVEL` int(11) DEFAULT '1',
  `MIN_AGGRO` tinyint(4) DEFAULT '5',
  `MAX_AGGRO` tinyint(4) DEFAULT '5',
  `Explanation` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`M_INDEX`),
  KEY `M_INDEX` (`M_INDEX`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for portalmaster
-- ----------------------------
CREATE TABLE `portalmaster` (
  `IDX` int(255) NOT NULL AUTO_INCREMENT,
  `VilgNum` int(11) NOT NULL DEFAULT '-1',
  `From_MAP` varchar(255) NOT NULL,
  `From_LocX` int(255) NOT NULL,
  `From_LocY` int(255) NOT NULL,
  `To_MAP` varchar(255) NOT NULL,
  `To_LocX` int(255) NOT NULL,
  `To_LocY` int(255) NOT NULL,
  `DC_YN` varchar(10) NOT NULL DEFAULT 'TRUE',
  PRIMARY KEY (`IDX`)
) ENGINE=InnoDB AUTO_INCREMENT=418 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for serversetting
-- ----------------------------
CREATE TABLE `serversetting` (
  `IDX` varchar(255) DEFAULT NULL,
  `Type` varchar(255) DEFAULT NULL,
  `Key` varchar(255) DEFAULT NULL,
  `Value` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for spawnmaster
-- ----------------------------
CREATE TABLE `spawnmaster` (
  `IDX` int(11) DEFAULT NULL,
  `M_INDEX` int(11) DEFAULT NULL,
  `CHANNEL` int(11) DEFAULT NULL,
  `MAP` varchar(255) DEFAULT NULL,
  `X` int(11) DEFAULT NULL,
  `Y` int(11) DEFAULT NULL,
  `RANGE` int(11) DEFAULT NULL,
  `AMOUNT` int(11) DEFAULT NULL,
  KEY `MONSTER` (`M_INDEX`),
  CONSTRAINT `MONSTER` FOREIGN KEY (`M_INDEX`) REFERENCES `monstermaster` (`M_INDEX`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for tb_board
-- ----------------------------
CREATE TABLE `tb_board` (
  `b_idx` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `thread` int(11) unsigned NOT NULL,
  `depth` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `m_id` varchar(12) NOT NULL,
  `b_title` varchar(255) NOT NULL,
  `b_contents` text NOT NULL,
  `b_regdate` datetime NOT NULL,
  PRIMARY KEY (`b_idx`),
  KEY `index1_tb_board` (`thread`)
) ENGINE=InnoDB AUTO_INCREMENT=57 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for tb_notice
-- ----------------------------
CREATE TABLE `tb_notice` (
  `b_idx` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `thread` int(11) unsigned NOT NULL,
  `depth` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `m_id` varchar(12) NOT NULL,
  `b_title` varchar(255) NOT NULL,
  `b_contents` text NOT NULL,
  `b_regdate` datetime NOT NULL,
  PRIMARY KEY (`b_idx`),
  KEY `index1_tb_notice` (`thread`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for userprocesslist
-- ----------------------------
CREATE TABLE `userprocesslist` (
  `ProcessBlack` varchar(255) DEFAULT NULL,
  `ProcessName` varchar(255) DEFAULT NULL,
  `Description` varchar(255) DEFAULT NULL,
  `Comment` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for vilagemaster
-- ----------------------------
CREATE TABLE `vilagemaster` (
  `IDX` int(255) NOT NULL,
  `NAME` varchar(255) DEFAULT NULL,
  `isOpen` varchar(5) DEFAULT 'FALSE',
  PRIMARY KEY (`IDX`),
  KEY `IDX` (`IDX`,`NAME`),
  KEY `NAME` (`NAME`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records 
-- ----------------------------
INSERT INTO `gotocmd` VALUES ('1', '0', '마을', '영혼의마을,32', '90', '176');
INSERT INTO `gotocmd` VALUES ('2', '0', 'village', '영혼의마을,32', '90', '176');
INSERT INTO `gotocmd` VALUES ('3', '0', '알리아스', '영혼의마을,32', '92', '154');
INSERT INTO `gotocmd` VALUES ('4', '0', 'alias', '영혼의마을,32', '92', '154');
INSERT INTO `gotocmd` VALUES ('5', '-1', '휴게실', '휴게실', '18', '18');
INSERT INTO `gotocmd` VALUES ('6', '-1', '쉼터', '휴게실', '18', '18');
INSERT INTO `gotocmd` VALUES ('7', '-1', '죽음', '휴게실', '18', '18');
INSERT INTO `gotocmd` VALUES ('8', '-1', 'rest', '휴게실', '18', '18');
INSERT INTO `gotocmd` VALUES ('9', '-1', 'death', '휴게실', '18', '18');
INSERT INTO `gotocmd` VALUES ('10', '-1', '아놀드', '대장간', '8', '12');
INSERT INTO `gotocmd` VALUES ('11', '-1', 'arnold', '시약상', '8', '12');
INSERT INTO `gotocmd` VALUES ('12', '0', '아론', '영혼의마을,32', '98', '60');
INSERT INTO `gotocmd` VALUES ('13', '0', 'aron', '영혼의마을,32', '98', '60');
INSERT INTO `gotocmd` VALUES ('14', '-1', '로엔', '시약상', '8', '12');
INSERT INTO `gotocmd` VALUES ('15', '-1', 'loen', '시약상', '8', '12');
INSERT INTO `gotocmd` VALUES ('16', '0', '점원', '영혼의마을,32', '74', '97');
INSERT INTO `gotocmd` VALUES ('17', '0', '노점상', '영혼의마을,32', '128', '94');
INSERT INTO `gotocmd` VALUES ('18', '0', 'employee', '영혼의마을,32', '128', '94');
INSERT INTO `gotocmd` VALUES ('19', '-1', '초보', '초보', '31', '20');
INSERT INTO `gotocmd` VALUES ('20', '-1', 'beginner', '초보', '31', '20');
INSERT INTO `gotocmd` VALUES ('21', '0', '위클리', '영혼의마을,32', '66', '123');
INSERT INTO `gotocmd` VALUES ('22', '0', 'weakly', '영혼의마을,32', '66', '123');
INSERT INTO `gotocmd` VALUES ('23', '0', '스켈', '영혼의마을,32', '139', '137');
INSERT INTO `gotocmd` VALUES ('24', '0', 'skel', '영혼의마을,32', '139', '137');
INSERT INTO `gotocmd` VALUES ('25', '-1', '터', '터', '29', '32');
INSERT INTO `gotocmd` VALUES ('26', '-1', 'level', '터', '29', '32');
INSERT INTO `gotocmd` VALUES ('27', '0', '아이템', '영혼의마을,32', '70', '68');
INSERT INTO `gotocmd` VALUES ('28', '0', 'item', '영혼의마을,32', '70', '68');
INSERT INTO `gotocmd` VALUES ('29', '-1', '가보자', '가보자', '25', '25');
INSERT INTO `gotocmd` VALUES ('30', '0', '빅건', '영혼의마을,32', '167', '119');
INSERT INTO `gotocmd` VALUES ('31', '0', 'biggun', '영혼의마을,32', '167', '119');
INSERT INTO `gotocmd` VALUES ('32', '-1', '퀘스트', '퀘스트', '8', '11');
INSERT INTO `gotocmd` VALUES ('33', '-1', '오지마세요', '퀘스트', '8', '11');
INSERT INTO `gotocmd` VALUES ('34', '-1', '교환', '교환', '42', '40');
INSERT INTO `gotocmd` VALUES ('35', '0', 'miro', '영혼의마을,32', '135', '58');
INSERT INTO `gotocmd` VALUES ('36', '0', '보물섬', '영혼의마을,23', '150', '56');
INSERT INTO `gotocmd` VALUES ('37', '-1', 'treasureland', 'TreasureLand', '25', '25');
INSERT INTO `gotocmd` VALUES ('38', '-1', '지구라트', '지구라트1', '97', '49');
INSERT INTO `gotocmd` VALUES ('39', '-1', '그레이트', 'Great', '19', '23');
INSERT INTO `gotocmd` VALUES ('40', '-1', '가디언', '가디언', '68', '21');
INSERT INTO `gotocmd` VALUES ('41', '-1', '광산', '광산0', '17', '39');
INSERT INTO `gotocmd` VALUES ('42', '-1', 'miner', '광산0', '17', '39');
INSERT INTO `gotocmd` VALUES ('43', '-1', '아발론', '아발론', '15', '38');
INSERT INTO `gotocmd` VALUES ('44', '-1', '축구장', '축구장', '23', '18');
INSERT INTO `gotocmd` VALUES ('45', '-1', 'soccerfield', '축구장', '23', '18');
INSERT INTO `gotocmd` VALUES ('46', '-1', '이기자', '대전장', '10', '10');
INSERT INTO `gotocmd` VALUES ('47', '-1', '대전장', '대전장', '10', '10');
INSERT INTO `gotocmd` VALUES ('48', '-1', '큰대전장', '큰대전장', '18', '18');
INSERT INTO `gotocmd` VALUES ('49', '-1', '오목', '오목', '18', '18');
INSERT INTO `gotocmd` VALUES ('50', '-1', '감옥', '독방', '13', '12');
INSERT INTO `gotocmd` VALUES ('51', '-1', '독방', '독방', '13', '12');
INSERT INTO `gotocmd` VALUES ('52', '1', '마을', '정령의마을,32', '90', '176');
INSERT INTO `gotocmd` VALUES ('53', '1', 'village', '정령의마을,32', '90', '176');
INSERT INTO `gotocmd` VALUES ('54', '1', '알리아스', '정령의마을,32', '92', '154');
INSERT INTO `gotocmd` VALUES ('55', '1', 'alias', '정령의마을,32', '18', '18');
INSERT INTO `gotocmd` VALUES ('63', '1', '아론', '정령의마을,32', '98', '60');
INSERT INTO `gotocmd` VALUES ('64', '1', 'aron', '정령의마을,32', '98', '60');
INSERT INTO `gotocmd` VALUES ('67', '1', '점원', '정령의마을,32', '74', '97');
INSERT INTO `gotocmd` VALUES ('68', '1', '노점상', '정령의마을,32', '128', '94');
INSERT INTO `gotocmd` VALUES ('69', '1', 'employee', '정령의마을,32', '128', '94');
INSERT INTO `gotocmd` VALUES ('72', '1', '위클리', '정령의마을,32', '66', '123');
INSERT INTO `gotocmd` VALUES ('73', '1', 'weakly', '정령의마을,32', '66', '123');
INSERT INTO `gotocmd` VALUES ('74', '1', '스켈', '정령의마을,32', '139', '137');
INSERT INTO `gotocmd` VALUES ('75', '1', 'skel', '정령의마을,32', '139', '137');
INSERT INTO `gotocmd` VALUES ('78', '1', '아이템', '정령의마을,32', '70', '68');
INSERT INTO `gotocmd` VALUES ('79', '1', 'item', '정령의마을,32', '70', '68');
INSERT INTO `gotocmd` VALUES ('81', '1', '빅건', '정령의마을,32', '167', '119');
INSERT INTO `gotocmd` VALUES ('82', '1', 'biggun', '정령의마을,32', '167', '119');
INSERT INTO `gotocmd` VALUES ('86', '1', 'miro', '정령의마을,32', '135', '58');
INSERT INTO `gotocmd` VALUES ('87', '1', '보물섬', '정령의마을,23', '150', '56');
INSERT INTO `gotocmd` VALUES ('88', '-1', '광산5', '광산5', '76', '12');
INSERT INTO `gotocmd` VALUES ('89', '-1', 'miner5', '광산5', '76', '12');
INSERT INTO `itemmaster` VALUES ('1', '10', 'KNIFE', null, '500', '100', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '3', '0', '0', '120000', null, null, 'Y', 'N', 'N');
INSERT INTO `itemmaster` VALUES ('2', '10', 'BAMBOO KNIFE', null, '1000', '200', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '4', '0', '0', '120000', null, null, 'Y', 'N', 'N');
INSERT INTO `itemmaster` VALUES ('3', '10', 'WOODEN SWORD', null, '1000', '250', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '5', '0', '0', '120000', null, null, 'Y', 'N', 'N');
INSERT INTO `itemmaster` VALUES ('4', '10', 'DAGGER', null, '2000', '400', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '8', '0', '0', '120000', null, null, 'Y', 'N', 'N');
INSERT INTO `itemmaster` VALUES ('5', '10', 'SHORT SWORD', null, '1000', '500', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '11', '0', '0', '120000', null, null, 'Y', 'N', null);
INSERT INTO `itemmaster` VALUES ('6', '10', 'FLEURE', null, '2500', '600', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '15', '0', '0', '120000', null, null, 'Y', 'N', 'N');
INSERT INTO `itemmaster` VALUES ('7', '10', 'RAPIER', null, '3000', '750', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '16', '0', '0', '120000', null, null, 'Y', 'N', 'N');
INSERT INTO `itemmaster` VALUES ('8', '10', 'SABER', null, '30000', '2000', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '19', '0', '0', '120000', null, null, 'Y', 'N', 'N');
INSERT INTO `itemmaster` VALUES ('9', '10', null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
INSERT INTO `mapmaster` VALUES ('1', '-1', '시약상', 'loen.Map', '0', '1001', '0', '0', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('2', '-1', '대장간', 'anold.Map', '0', '1001', '0', '0', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('3', '-1', '휴게실', 'erest.map', '0', '1002', '0', '0', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('4', '1', '정령의마을,21', '21.map', '0', '1001', '0', '0', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('5', '1', '정령의마을,22', '22.map', '0', '1001', '0', '0', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('6', '1', '정령의마을,23', '23.map', '0', '1001', '0', '0', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('7', '1', '정령의마을,31', '31.map', '0', '1001', '0', '0', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('8', '1', '정령의마을,32', '32.map', '0', '1001', '0', '0', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('9', '1', '정령의마을,33', '33.map', '0', '1001', '0', '0', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('10', '1', '정령의마을,41', '41.map', '0', '1001', '0', '0', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('11', '1', '정령의마을,42', '42.map', '0', '1001', '0', '0', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('12', '1', '정령의마을,43', '43.map', '0', '1001', '0', '0', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('13', '1', '정령의마을,51', '51.map', '0', '1001', '0', '0', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('14', '1', '정령의마을,52', '52.map', '0', '1001', '0', '0', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('15', '1', '정령의마을,53', '53.map', '0', '1001', '0', '0', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('16', '-1', '교환', 'sell.map', '0', '1002', '0', '0', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('17', '-1', '초보', 'estart2.map', '1', '1003', '0', '0', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('18', '-1', 'Weakly1', 'weakly1.map', '1', '1001', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('19', '-1', 'Weakly2', 'weakly2.map', '1', '1001', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('20', '-1', 'Weakly3', 'weakly3.map', '1', '1001', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('21', '-1', 'Weakly4', 'weakly4.map', '1', '1001', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('22', '-1', 'Weakly5', 'weakly5.map', '1', '1001', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('23', '-1', '스켈1', 'skel1.map', '1', '1001', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('24', '-1', '스켈2', 'skel2.map', '1', '1001', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('25', '-1', '스켈3', 'skel3.map', '1', '1001', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('26', '-1', '스켈4', 'skel4.map', '1', '1001', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('27', '-1', '스켈5', 'skel5.map', '1', '1001', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('28', '-1', '퀘스트', 'qweak.map', '1', '1001', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('29', '-1', 'Qweak1', 'qweak1.map', '4', '1001', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('30', '-1', 'Qweak2', 'qweak2.map', '4', '1001', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('31', '-1', 'Qweak3', 'qweak3.map', '4', '1001', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('32', '-1', 'Qweak4', 'qweak4.map', '4', '1001', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('33', '-1', 'Qweak5', 'qweak5.map', '4', '1001', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('34', '-1', '가보자', 'estart1.map', '1', '1001', '30', '0', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('35', '-1', 'Start', 'estart3.map', '1', '1003', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('36', '-1', '스콜피온', 'estart4.map', '1', '1003', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('37', '-1', '동굴1층', 'cave.map', '3', '1004', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('38', '-1', '동굴2층', 'cave1.map', '3', '1005', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('39', '-1', '동굴3층', 'cave2.map', '3', '1005', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('40', '-1', '바이킹', 'Viking.map', '1', '1003', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('41', '-1', '골램', 'golem.map', '2', '1003', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('42', '-1', 'Golem1', 'golem1.map', '2', '1003', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('43', '-1', 'Golem2', 'golem2.map', '2', '1003', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('44', '-1', 'Golem3', 'golem3.map', '2', '1003', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('45', '-1', 'Golem12', 'golem12.map', '2', '1003', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('46', '-1', '아발론', 'miner0.map', '4', '1005', '0', '0', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('47', '-1', '광산0', 'miner0.map', '4', '1005', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('48', '-1', '광산1', 'miner1.map', '4', '1005', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('49', '-1', '광산2', 'miner2.map', '4', '1005', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('50', '-1', '광산3', 'miner3.map', '4', '1005', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('51', '-1', '광산4', 'miner0.map', '5', '1005', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('52', '-1', '광산5', 'miner1.map', '5', '1005', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('53', '-1', '광산6', 'miner2.map', '5', '1005', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('54', '-1', '광산7', 'miner3.map', '5', '1005', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('55', '-1', '광산8', 'miner0.map', '5', '1005', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('56', '-1', '초보광산', 'miner0.map', '5', '1005', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('57', '-1', '터', 'elevel.map', '2', '1001', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('58', '-1', '010', '010map.map', '2', '1003', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('59', '-1', '020', '020map.map', '2', '1003', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('60', '-1', '030', '030map.map', '2', '1003', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('61', '-1', '040', '040map.map', '2', '1003', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('62', '-1', '050', '050map.map', '2', '1003', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('63', '-1', '060', '060map.map', '2', '1005', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('64', '-1', '061', '061map.map', '2', '1005', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('65', '-1', '070', '070map.map', '2', '1005', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('66', '-1', '071', '071map.map', '2', '1005', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('67', '-1', '080', '080map.map', '2', '1005', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('68', '-1', '081', '081map.map', '2', '1005', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('69', '-1', 'ItemVillage', 'ivillage.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('70', '-1', 'ItemPigmy1', 'ipigmy1.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('71', '-1', 'ItemPigmy2', 'ipigmy2.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('72', '-1', 'ItemPigmy3', 'ipigmy3.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('73', '-1', 'ItemZomby1', 'izomby1.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('74', '-1', 'ItemZomby2', 'izomby2.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('75', '-1', 'ItemZomby3', 'izomby3.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('76', '-1', 'ItemSkel1', 'iskel1.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('77', '-1', 'ItemSkel2', 'iskel2.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('78', '-1', 'ItemSkel3', 'iskel3.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('79', '-1', 'ItemButcher1', 'ibut1.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('80', '-1', 'ItemButcher2', 'ibut2.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('81', '-1', 'ItemButcher3', 'ibut3.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('82', '-1', 'ItemMummy1', 'imummy1.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('83', '-1', 'ItemMummy2', 'imummy2.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('84', '-1', 'ItemMummy3', 'imummy3.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('85', '-1', 'ItemStone1', 'istone1.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('86', '-1', 'ItemStone2', 'istone2.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('87', '-1', 'ItemStone3', 'istone3.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('88', '-1', 'ItemStone4', 'istone4.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('89', '-1', 'ItemDummy1', 'imummy1.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('90', '-1', 'ItemDummy2', 'imummy2.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('91', '-1', 'ItemDummy3', 'imummy3.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('92', '-1', 'ItemDummy4', 'istone3.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('93', '-1', 'ItemDummy5', 'istone4.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('94', '-1', 'ItemWarDummy1', 'imummy1.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('95', '-1', 'ItemWarDummy2', 'imummy2.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('96', '-1', 'ItemWarDummy3', 'imummy3.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('97', '-1', 'ItemWarDummy4', 'istone3.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('98', '-1', 'ItemWarDummy5', 'istone4.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('99', '-1', 'ItemHardboil1', 'imummy1.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('100', '-1', 'ItemHardboil2', 'imummy2.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('101', '-1', 'ItemHardboil3', 'imummy3.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('102', '-1', 'ItemHardboil4', 'istone3.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('103', '-1', 'ItemHardboil5', 'istone4.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('104', '-1', 'ItemGolem1', 'imummy1.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('105', '-1', 'ItemGolem2', 'imummy2.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('106', '-1', 'ItemGolem3', 'imummy3.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('107', '-1', 'ItemGolem4', 'istone3.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('108', '-1', 'ItemGolem5', 'istone4.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('109', '-1', 'Biggun1', 'estart4.map', '1', '1004', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('110', '-1', 'Biggun2', 'estart3.map', '1', '1004', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('111', '-1', 'Miro1', 'miro1.map', '1', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('112', '-1', 'TreasureLand', '1000.map', '1', '1001', '0', '2', 'FALSE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('113', '-1', 'Great', 'great.map', '1', '1001', '231', '2', 'TRUE', 'TRUE', 'TRUE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('114', '-1', '지구라트1', 'vv1.map', '4', '1001', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('115', '-1', '지구라트2', 'vv2.map', '4', '1001', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('116', '-1', '지구라트3', 'vv3.map', '4', '1001', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('117', '-1', '지구라트4', 'vv4.map', '4', '1001', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('118', '-1', '지구라트5', 'vv5.map', '4', '1001', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('119', '-1', '대전장', 'ebox2x2.map', '1', '1001', '0', '1', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('120', '-1', '큰대전장', 'ebox4x4.map', '1', '1001', '0', '1', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('121', '-1', '오목', 'ebox4x4.map', '1', '1001', '0', '2', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('122', '-1', '독방', 'prison.map', '1', '-1', '0', '1', 'TRUE', 'FALSE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('123', '-1', '축구장', 'esoccer.map', '1', '1001', '0', '0', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('124', '-1', '가디언', 'ps01.map', '5', '1001', '231', '2', 'TRUE', 'TRUE', 'TRUE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('125', '-1', '가디언1', 'ps02.map', '5', '1001', '0', '2', 'TRUE', 'TRUE', 'TRUE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('126', '-1', '가디언2', 'ps03.map', '5', '1001', '0', '2', 'TRUE', 'TRUE', 'TRUE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('130', '0', '영혼의마을,21', '21.map', '0', '1001', '0', '0', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('131', '0', '영혼의마을,22', '22.map', '0', '1001', '0', '0', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('132', '0', '영혼의마을,23', '23.map', '0', '1001', '0', '0', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('133', '0', '영혼의마을,31', '31.map', '0', '1001', '0', '0', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('134', '0', '영혼의마을,32', '32.map', '0', '1001', '0', '0', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('135', '0', '영혼의마을,33', '33.map', '0', '1001', '0', '0', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('136', '0', '영혼의마을,41', '41.map', '0', '1001', '0', '0', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('137', '0', '영혼의마을,42', '42.map', '0', '1001', '0', '0', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('138', '0', '영혼의마을,43', '43.map', '0', '1001', '0', '0', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('139', '0', '영혼의마을,51', '51.map', '0', '1001', '0', '0', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('140', '0', '영혼의마을,52', '52.map', '0', '1001', '0', '0', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('141', '0', '영혼의마을,53', '53.map', '0', '1001', '0', '0', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `mapmaster` VALUES ('142', '-1', '뼈들의부활', 'bone01.map', '0', '1001', '0', '2', 'TRUE', 'TRUE', 'FALSE', 'FALSE');
INSERT INTO `monstermaster` VALUES ('1', 'Biggun', '', '36', '0', '500', '1000', '75000', '235', '550', '300', '5000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('2', 'Bio Rubber', '', '219', '0', '500', '1000', '1000000', '4000', '3000', '2000', '15500', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('3', 'BLOOD SHOT', '', '38', '0', '500', '1000', '3000', '75', '100', '80', '300', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('4', 'Bone Dry', '', '1', '0', '500', '1000', '397', '60', '89', '70', '200', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('5', 'Bone Dry', '', '24', '1', '500', '1000', '99', '32', '61', '33', '100', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('6', 'Bone Dry', '', '24', '2', '500', '1000', '126', '34', '63', '36', '120', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('7', 'Bone Dry', '', '24', '3', '500', '1000', '153', '39', '66', '39', '130', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('8', 'Bone Patrol', '', '21', '0', '500', '1000', '621', '68', '97', '80', '200', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('9', 'Bone Trio', '', '215', '0', '500', '1000', '2000000', '5500', '6000', '3000', '29000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('10', 'Bone Trio', '', '220', '0', '500', '1000', '500000', '6900', '5600', '4000', '25000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('11', 'Bone Trio', '', '221', '0', '500', '1000', '1000000', '4000', '2500', '2000', '15500', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('12', 'Butcher', '', '8', '0', '500', '1000', '60', '23', '52', '21', '150', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('13', 'Butcher', '', '8', '0', '500', '1000', '288', '50', '79', '56', '120', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('14', 'Butcher', '', '8', '0', '500', '1000', '16500', '140', '200', '230', '500', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('15', 'Butcher', '', '8', '1', '500', '1000', '315', '56', '81', '59', '120', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('16', 'Butcher', '', '8', '2', '500', '1000', '342', '55', '83', '62', '130', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('17', 'Butcher', '', '8', '3', '500', '1000', '369', '57', '85', '65', '130', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('18', 'Centeur', '', '222', '0', '500', '1000', '700000', '6900', '5900', '5900', '25000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('19', 'Cyclops', '', '41', '0', '500', '1000', '540000', '850', '4000', '850', '10000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('20', 'DEADLY MESSENGER', '', '203', '0', '1000', '1000', '90000', '275', '550', '500', '4500', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('21', 'Devil', '', '212', '0', '500', '1000', '5000000', '7000', '6000', '3000', '50000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('22', 'DEVIL', '', '212', '0', '500', '1000', '3000000', '7000', '6000', '3000', '200000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('23', 'CANNIBAL DEMON', '', '211', '0', '500', '1000', '500000', '4000', '4000', '2000', '15000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('24', 'Devil Kid', '', '18', '0', '500', '1000', '150000', '385', '750', '650', '7000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('25', 'Dummy', '', '13', '0', '500', '1000', '13900', '140', '192', '204', '2000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('26', 'Dummy', '', '13', '0', '500', '1000', '29824', '175', '243', '270', '1200', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('27', 'Dummy', '', '13', '1', '500', '1000', '33382', '185', '250', '281', '450', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('28', 'Dummy', '', '13', '2', '500', '1000', '36940', '185', '259', '292', '500', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('29', 'Dummy', '', '13', '3', '500', '1000', '44056', '190', '267', '302', '600', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('30', 'Fire Dragon', '', '216', '0', '500', '1000', '6000000', '10000', '6500', '6500', '1500000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('31', 'Gargoyle', '', '204', '0', '2000', '1000', '600000', '1500', '3000', '1000', '20000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('32', 'General Skel', '', '25', '0', '500', '1000', '1408', '83', '119', '108', '350', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('33', 'Ghost Knight', '', '207', '0', '500', '1000', '5000000', '7000', '6000', '3000', '50000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('34', 'Golem Guard', '', '31', '0', '1200', '1000', '90000', '380', '650', '480', '6000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('35', 'Great Stone', '', '6', '0', '1300', '1000', '9519', '130', '174', '185', '2500', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('36', 'Hag', '', '29', '0', '500', '1000', '100000', '268', '480', '500', '4000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('37', 'HARD BOILD', '', '17', '0', '1200', '1000', '77179', '225', '323', '373', '3000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('38', 'HARDBOIL', '', '17', '1', '500', '1000', '72066', '220', '314', '363', '3550', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('39', 'HARDBOIL', '', '17', '2', '500', '1000', '77235', '225', '323', '373', '3600', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('40', 'HARDBOIL', '', '17', '3', '500', '1000', '77290', '229', '328', '382', '3700', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('41', 'HARDBOILD', '', '17', '0', '500', '1000', '66843', '225', '306', '353', '3000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('42', 'Head Cutter', '', '30', '0', '500', '1000', '78', '28', '57', '26', '150', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('43', 'Hounds Of Doom', '', '19', '0', '500', '1000', '250000', '1500', '2400', '600', '10500', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('44', 'Iron Golem', '', '34', '0', '500', '1000', '210000', '445', '850', '750', '9000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('45', 'Item Butcher', '', '12', '0', '500', '1000', '1107', '108', '167', '185', '2000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('46', 'Item Dummy', '', '13', '0', '600', '1000', '1', '300', '467', '490', '5000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('47', 'Item Golem', '', '34', '0', '600', '1000', '1', '415', '870', '620', '25000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('48', 'Item Hard Boild', '', '28', '0', '600', '1000', '1', '380', '667', '790', '12900', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('49', 'Item Mummy', '', '5', '0', '500', '1000', '1863', '115', '181', '200', '450', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('50', 'Item Pigmy', '', '4', '0', '500', '1000', '1', '58', '127', '98', '300', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('51', 'Item Skel', '', '23', '0', '500', '1000', '459', '85', '157', '121', '350', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('52', 'Item Stone', '', '6', '0', '600', '1000', '1', '156', '195', '242', '5000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('53', 'Item War Dummy', '', '13', '0', '600', '1000', '1', '310', '567', '590', '8500', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('54', 'Item Zombie', '', '3', '0', '500', '1000', '144', '59', '138', '109', '300', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('55', 'LARGE SKEL', '', '2', '0', '500', '1000', '1071', '79', '113', '100', '350', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('56', 'Leo', '', '210', '0', '500', '1000', '1000000', '4000', '4000', '2000', '15500', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('57', 'Loup Garou', '', '212', '0', '500', '1000', '1200000', '3700', '3500', '1800', '32000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('58', 'MAD DOG', '', '19', '0', '1500', '1000', '24', '0', '6', '2', '15', '0', '3', '5', null);
INSERT INTO `monstermaster` VALUES ('59', 'MAD DOG', '', '19', '0', '1500', '1000', '1000', '18', '48', '16', '150', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('60', 'MEDUSA', '', '200', '0', '1000', '1000', '80000', '250', '450', '350', '3800', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('61', 'MERCENARY', '', '218', '0', '500', '1000', '3200000', '10000', '6500', '6500', '40000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('62', 'Mummy', '', '5', '0', '500', '1000', '397', '60', '89', '70', '150', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('63', 'Mummy', '', '5', '0', '500', '1000', '51172', '200', '283', '324', '2600', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('64', 'Mummy', '', '5', '1', '500', '1000', '465', '61', '91', '72', '160', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('65', 'Mummy', '', '5', '2', '500', '1000', '535', '65', '94', '76', '200', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('66', 'Mummy', '', '5', '3', '500', '1000', '621', '68', '97', '80', '220', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('67', 'Necromancer', '', '205', '0', '500', '1000', '1050000', '3600', '3200', '1200', '32000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('68', 'Neo Dummy', '', '14', '1', '500', '1000', '8500', '200', '100', '220', '5000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('69', 'Neo Great Stone', '', '6', '1', '500', '1000', '9500', '230', '120', '250', '5000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('70', 'Neo Skel Escottor', '', '23', '1', '500', '1000', '950', '35', '55', '65', '400', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('71', 'Neo Viking', '', '32', '1', '500', '1000', '410', '25', '55', '65', '1000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('72', 'Neo War Dummy', '', '14', '1', '500', '1000', '12500', '250', '130', '250', '3000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('73', 'NINJA', '', '213', '0', '2000', '1000', '1000000', '3300', '2500', '1000', '32000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('74', 'One Eye', '', '225', '0', '500', '1000', '740000', '1000', '5000', '1200', '14000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('75', 'PHANTOM', '', '227', '0', '500', '1000', '2000000', '10000', '6500', '6500', '25000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('76', 'PIGMY', '', '15', '0', '500', '1000', '15', '1', '16', '2', '10', '0', '3', '5', null);
INSERT INTO `monstermaster` VALUES ('77', 'PIGMY', '', '15', '0', '500', '1000', '15', '1', '16', '2', '25', '0', '3', '5', null);
INSERT INTO `monstermaster` VALUES ('78', 'Pigmy', '', '15', '1', '500', '1000', '24', '4', '37', '2', '40', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('79', 'Pigmy', '', '15', '2', '500', '1000', '27', '6', '39', '4', '40', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('80', 'Pigmy', '', '15', '3', '500', '1000', '28', '6', '41', '6', '40', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('81', 'Princess Shea', '', '11', '0', '500', '1000', '1000000', '10000', '6500', '6500', '30000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('82', 'TEMTATION', '', '226', '0', '500', '1000', '1000000', '10000', '6500', '6500', '30000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('83', 'Rioter', '', '37', '0', '500', '1000', '400000', '500', '3000', '555', '12500', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('84', 'Rowedy', '', '35', '0', '500', '1000', '65000', '195', '270', '350', '3000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('85', 'SAXON', '', '217', '0', '500', '1000', '1500000', '3000', '4000', '2000', '65000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('86', 'SCORPIAN', '', '39', '0', '500', '1000', '4500', '100', '150', '100', '300', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('87', 'Skel Escottor', '', '23', '0', '500', '1000', '570', '65', '74', '76', '500', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('88', 'Skel Fighter', '', '24', '0', '500', '1000', '807', '74', '105', '91', '250', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('89', 'SKEL GIANT', '', '26', '0', '500', '1000', '400000', '1000', '2500', '2000', '18000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('90', 'Skeleton', '', '1', '0', '500', '1000', '32', '5', '20', '15', '100', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('91', 'Skeleton', '', '1', '1', '500', '1000', '42', '10', '25', '22', '120', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('92', 'Small King', '', '16', '0', '500', '1000', '44', '7', '44', '30', '300', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('93', 'Smoke Giant', '', '224', '0', '500', '1000', '1000000', '6900', '5900', '5900', '20500', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('94', 'SMOKE GIANT', '', '224', '0', '500', '1000', '2000000', '6900', '5900', '5900', '25500', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('95', 'SOLO', '', '225', '0', '500', '1000', '2000000', '10000', '6500', '6500', '35000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('96', 'Spot', '', '210', '0', '500', '1000', '250000', '4000', '4000', '1200', '10500', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('97', 'Stone', '', '6', '0', '500', '1000', '5250', '110', '155', '154', '1000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('98', 'Stone', '', '6', '0', '1200', '1000', '681', '69', '100', '83', '300', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('99', 'Stone', '', '6', '1', '1200', '1000', '744', '72', '102', '87', '200', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('100', 'Stone', '', '6', '2', '1200', '1000', '807', '74', '105', '91', '210', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('101', 'Stone', '', '6', '3', '1200', '1000', '807', '78', '111', '94', '220', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('102', 'Stone Golem', '', '31', '0', '500', '1000', '77290', '232', '335', '492', '5000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('103', 'Super Iron Golem', '', '34', '0', '500', '1000', '255000', '470', '1100', '850', '9000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('104', 'MINO', '', '223', '0', '500', '1000', '2000000', '10000', '6500', '6500', '35000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('105', 'TWIN COBRA', '', '201', '0', '1000', '1000', '80000', '250', '450', '350', '3800', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('106', 'Viking', '', '32', '0', '500', '1000', '621', '68', '97', '80', '200', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('107', 'VIKING', '', '32', '0', '500', '1000', '22500', '160', '230', '250', '500', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('108', 'War Bad Girl', '', '7', '0', '500', '1000', '67', '25', '56', '24', '150', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('109', 'War Bone', '', '25', '0', '500', '1000', '931', '78', '111', '94', '250', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('110', 'War Butcher', '', '12', '0', '500', '1000', '108', '12', '35', '30', '300', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('111', 'War Dummy', '', '14', '0', '500', '1000', '26266', '171', '235', '260', '2500', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('112', 'War Dummy', '', '14', '0', '500', '1000', '40498', '190', '267', '402', '3000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('113', 'War Dummy', '', '14', '1', '500', '1000', '47614', '195', '275', '313', '450', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('114', 'War Dummy', '', '14', '2', '500', '1000', '56395', '205', '291', '333', '500', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('115', 'War Dummy', '', '14', '3', '500', '1000', '61618', '210', '299', '343', '600', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('116', 'Wild Dog', '', '27', '0', '500', '1000', '5', '0', '6', '2', '15', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('117', 'WILD MONK', '', '202', '0', '1000', '1000', '90000', '275', '550', '500', '4500', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('118', 'Zombie', '', '3', '0', '500', '1000', '33', '11', '45', '8', '50', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('119', 'Zombie', '', '3', '1', '500', '1000', '34', '13', '46', '10', '50', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('120', 'Zombie', '', '3', '2', '500', '1000', '39', '16', '47', '13', '50', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('121', 'Zombie', '', '3', '3', '500', '1000', '48', '18', '48', '16', '50', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('122', '가넷', '', '3', '0', '1200', '1000', '140', '0', '0', '0', '6000', '30', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('123', '강철', '', '3', '0', '1200', '1000', '120', '0', '0', '0', '6000', '28', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('124', '골든 다이아몬드', '', '7', '0', '1200', '1000', '7542', '0', '0', '0', '6000', '99', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('125', '구리', '', '3', '0', '1200', '1000', '20', '0', '0', '0', '6000', '2', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('126', '그레이트 사파이어', '', '7', '0', '1200', '1000', '7542', '0', '0', '0', '6000', '94', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('127', '금', '', '7', '0', '1200', '1000', '350', '0', '0', '0', '6000', '37', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('128', '납', '', '3', '0', '1200', '1000', '20', '0', '0', '0', '6000', '4', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('129', '니켈', '', '3', '0', '1200', '1000', '350', '0', '0', '0', '6000', '33', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('130', '다이아몬드', '', '7', '0', '1200', '1000', '7542', '0', '0', '0', '6000', '92', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('131', '다크스톤', '', '3', '0', '1200', '1000', '417', '0', '0', '0', '6000', '61', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('132', '레오파드제스퍼', '', '3', '0', '1200', '1000', '417', '0', '0', '0', '6000', '41', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('133', '루비', '', '4', '0', '1200', '1000', '48', '0', '0', '0', '6000', '22', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('134', '마왕철', '', '7', '0', '1200', '1000', '18855', '0', '0', '0', '6000', '96', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('135', '문스톤', '', '3', '0', '1200', '1000', '48', '0', '0', '0', '6000', '26', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('136', '백금', '', '3', '0', '1200', '1000', '5525', '0', '0', '0', '6000', '63', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('137', '블러드스톤', '', '3', '0', '1200', '1000', '417', '0', '0', '0', '6000', '48', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('138', '사파이어', '', '3', '0', '1200', '1000', '3992', '0', '0', '0', '6000', '72', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('139', '산호', '', '3', '0', '1200', '1000', '6323', '0', '0', '0', '6000', '81', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('140', '생명', '', '8', '0', '1200', '1000', '5', '0', '0', '0', '6000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('141', '석영', '', '4', '0', '1200', '1000', '16', '0', '0', '0', '6000', '17', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('142', '숙련', '', '8', '0', '1200', '1000', '5', '0', '0', '0', '6000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('143', '썬스톤', '', '3', '0', '1200', '1000', '140', '0', '0', '0', '6000', '35', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('144', '아메시스트', '', '3', '0', '1200', '1000', '3992', '0', '0', '0', '6000', '77', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('145', '아쿠아머린', '', '3', '0', '1200', '1000', '3992', '0', '0', '0', '6000', '70', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('146', '알루미늄', '', '4', '0', '1200', '1000', '20', '0', '0', '0', '6000', '6', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('147', '에메랄드', '', '3', '0', '1200', '1000', '417', '0', '0', '0', '6000', '44', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('148', '오팔', '', '7', '0', '1200', '1000', '6323', '0', '0', '0', '6000', '85', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('149', '은', '', '4', '0', '1200', '1000', '42', '0', '0', '0', '6000', '11', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('150', '제이드', '', '3', '0', '1200', '1000', '1003', '0', '0', '0', '6000', '52', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('151', '지력', '', '8', '0', '1200', '1000', '5', '0', '0', '0', '6000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('152', '지르코늄', '', '3', '0', '1200', '1000', '140', '0', '0', '0', '6000', '39', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('153', '진주', '', '3', '0', '1200', '1000', '1003', '0', '0', '0', '6000', '59', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('154', '짱돌', '', '3', '0', '1200', '1000', '2210', '0', '0', '0', '6000', '68', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('155', '철', '', '3', '0', '1200', '1000', '20', '0', '0', '0', '6000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('156', '청동', '', '3', '0', '1200', '1000', '20', '0', '0', '0', '6000', '8', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('157', '초합금', '', '7', '0', '1200', '1000', '18855', '0', '0', '0', '6000', '90', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('158', '축구공', '', '1', '0', '1200', '1000', '0', '2147483647', '2147483647', '2147483647', '2147483647', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('159', '카드뮴', '', '3', '0', '1200', '1000', '42', '0', '0', '0', '6000', '13', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('160', '캣츠아이', '', '3', '0', '1200', '1000', '3992', '0', '0', '0', '6000', '79', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('161', '코발트', '', '3', '0', '1200', '1000', '1044', '0', '0', '0', '6000', '46', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('162', '크리스탈', '', '5', '0', '1200', '1000', '48', '0', '0', '0', '6000', '24', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('163', '타이거스아이', '', '3', '0', '1200', '1000', '1003', '0', '0', '0', '6000', '50', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('164', '터키석', '', '7', '0', '1200', '1000', '6323', '0', '0', '0', '6000', '88', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('165', '텅스텐', '', '3', '0', '1200', '1000', '42', '0', '0', '0', '6000', '19', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('166', '토파즈', '', '3', '0', '1200', '1000', '16', '0', '0', '0', '6000', '15', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('167', '티타늄', '', '3', '0', '1200', '1000', '2509', '0', '0', '0', '6000', '55', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('168', '합금', '', '3', '0', '1200', '1000', '9982', '0', '0', '0', '6000', '74', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('169', '헬리도오', '', '3', '0', '1200', '1000', '1003', '0', '0', '0', '6000', '57', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('170', '호박', '', '3', '0', '1200', '1000', '2210', '0', '0', '0', '6000', '66', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('171', '흑진주', '', '7', '0', '1200', '1000', '6323', '0', '0', '0', '6000', '83', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('172', '힘', '', '8', '0', '1200', '1000', '5', '0', '0', '0', '6000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('173', 'VIOLATOR', '', '206', '0', '1200', '1000', '3200000', '15000', '6500', '6500', '40000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('174', 'BLOOD WING', '', '40', '0', '1000', '1000', '800000', '3300', '2500', '1000', '32000', '0', '7', '12', null);
INSERT INTO `monstermaster` VALUES ('175', 'MINO', '', '223', '0', '1000', '1000', '500000', '6900', '5600', '4500', '25000', '0', '7', '12', null);
INSERT INTO `portalmaster` VALUES ('1', '-1', '대장간', '12', '3', '광산0', '17', '39', 'FALSE');
INSERT INTO `portalmaster` VALUES ('2', '0', '영혼의마을,32', '139', '155', '시약상', '6', '14', 'FALSE');
INSERT INTO `portalmaster` VALUES ('3', '0', '시약상', '5', '15', '영혼의마을,32', '139', '157', 'FALSE');
INSERT INTO `portalmaster` VALUES ('4', '0', '영혼의마을,32', '65', '158', '대장간', '5', '17', 'FALSE');
INSERT INTO `portalmaster` VALUES ('5', '0', '대장간', '4', '17', '영혼의마을,32', '65', '160', 'FALSE');
INSERT INTO `portalmaster` VALUES ('6', '0', '영혼의마을,42', '61', '114', '휴게실', '19', '14', 'FALSE');
INSERT INTO `portalmaster` VALUES ('7', '0', '휴게실', '19', '12', '영혼의마을,42', '61', '116', 'FALSE');
INSERT INTO `portalmaster` VALUES ('8', '0', '영혼의마을,41', '96', '124', '퀘스트', '14', '5', 'FALSE');
INSERT INTO `portalmaster` VALUES ('9', '0', '퀘스트', '14', '3', '영혼의마을,41', '96', '126', 'FALSE');
INSERT INTO `portalmaster` VALUES ('10', '0', '영혼의마을,32', '167', '122', 'Biggun1', '98', '78', 'FALSE');
INSERT INTO `portalmaster` VALUES ('11', '0', 'Biggun1', '99', '79', '영혼의마을,32', '164', '122', 'FALSE');
INSERT INTO `portalmaster` VALUES ('12', '-1', '가디언', '41', '40', '가디언1', '47', '13', 'FALSE');
INSERT INTO `portalmaster` VALUES ('13', '-1', '가디언1', '49', '12', '가디언', '43', '41', 'FALSE');
INSERT INTO `portalmaster` VALUES ('14', '-1', 'Biggun1', '14', '17', 'Biggun1', '5', '5', 'FALSE');
INSERT INTO `portalmaster` VALUES ('15', '-1', 'Biggun1', '6', '7', 'Biggun1', '14', '19', 'FALSE');
INSERT INTO `portalmaster` VALUES ('16', '-1', 'Biggun1', '0', '0', 'Biggun2', '98', '78', 'FALSE');
INSERT INTO `portalmaster` VALUES ('17', '-1', 'Biggun2', '99', '79', 'Biggun1', '1', '1', 'FALSE');
INSERT INTO `portalmaster` VALUES ('18', '-1', '가보자', '30', '21', 'Miro1', '1', '1', 'FALSE');
INSERT INTO `portalmaster` VALUES ('19', '-1', 'Miro1', '0', '0', '가보자', '28', '22', 'FALSE');
INSERT INTO `portalmaster` VALUES ('20', '-1', 'Miro1', '74', '96', 'Miro1', '52', '156', 'FALSE');
INSERT INTO `portalmaster` VALUES ('21', '-1', 'Miro1', '52', '155', 'Miro1', '74', '97', 'FALSE');
INSERT INTO `portalmaster` VALUES ('22', '-1', '가보자', '2', '2', 'Start', '2', '2', 'FALSE');
INSERT INTO `portalmaster` VALUES ('23', '-1', 'Start', '1', '0', '가보자', '6', '4', 'FALSE');
INSERT INTO `portalmaster` VALUES ('24', '-1', 'Start', '99', '79', '스콜피온', '2', '2', 'FALSE');
INSERT INTO `portalmaster` VALUES ('25', '-1', '스콜피온', '0', '1', 'Start', '98', '78', 'FALSE');
INSERT INTO `portalmaster` VALUES ('26', '-1', '스콜피온', '14', '17', '스콜피온', '5', '5', 'FALSE');
INSERT INTO `portalmaster` VALUES ('27', '-1', '스콜피온', '6', '7', '스콜피온', '14', '19', 'FALSE');
INSERT INTO `portalmaster` VALUES ('28', '-1', '가보자', '49', '0', '골램', '15', '1', 'FALSE');
INSERT INTO `portalmaster` VALUES ('29', '-1', '골램', '15', '0', '가보자', '43', '5', 'FALSE');
INSERT INTO `portalmaster` VALUES ('30', '-1', '가보자', '44', '49', '동굴1층', '10', '8', 'FALSE');
INSERT INTO `portalmaster` VALUES ('31', '-1', '동굴1층', '9', '6', '가보자', '41', '42', 'FALSE');
INSERT INTO `portalmaster` VALUES ('32', '-1', '동굴1층', '62', '61', '동굴2층', '8', '48', 'FALSE');
INSERT INTO `portalmaster` VALUES ('33', '-1', '동굴2층', '6', '47', '동굴1층', '64', '62', 'FALSE');
INSERT INTO `portalmaster` VALUES ('34', '-1', '동굴2층', '88', '29', '동굴3층', '91', '42', 'FALSE');
INSERT INTO `portalmaster` VALUES ('35', '-1', '동굴3층', '89', '41', '동굴2층', '90', '30', 'FALSE');
INSERT INTO `portalmaster` VALUES ('36', '-1', '가보자', '2', '44', '바이킹', '10', '10', 'FALSE');
INSERT INTO `portalmaster` VALUES ('37', '-1', '바이킹', '7', '7', '가보자', '4', '44', 'FALSE');
INSERT INTO `portalmaster` VALUES ('38', '-1', '지구라트1', '146', '101', '지구라트2', '158', '71', 'FALSE');
INSERT INTO `portalmaster` VALUES ('39', '-1', '지구라트2', '160', '69', '지구라트1', '144', '103', 'FALSE');
INSERT INTO `portalmaster` VALUES ('40', '-1', '지구라트2', '169', '134', '지구라트3', '175', '56', 'FALSE');
INSERT INTO `portalmaster` VALUES ('41', '-1', '지구라트3', '176', '55', '지구라트2', '167', '136', 'FALSE');
INSERT INTO `portalmaster` VALUES ('42', '-1', '지구라트3', '55', '21', '지구라트4', '171', '32', 'FALSE');
INSERT INTO `portalmaster` VALUES ('43', '-1', '지구라트4', '172', '31', '지구라트3', '54', '22', 'FALSE');
INSERT INTO `portalmaster` VALUES ('44', '-1', '지구라트4', '46', '97', '지구라트5', '179', '157', 'FALSE');
INSERT INTO `portalmaster` VALUES ('45', '-1', '지구라트5', '180', '156', '지구라트4', '45', '98', 'FALSE');
INSERT INTO `portalmaster` VALUES ('46', '0', '지구라트5', '150', '134', '영혼의마을,32', '90', '176', 'FALSE');
INSERT INTO `portalmaster` VALUES ('47', '-1', '광산0', '139', '39', '광산1', '76', '12', 'FALSE');
INSERT INTO `portalmaster` VALUES ('48', '-1', '광산1', '76', '8', '광산0', '137', '40', 'FALSE');
INSERT INTO `portalmaster` VALUES ('49', '-1', '광산1', '85', '66', '광산2', '75', '9', 'FALSE');
INSERT INTO `portalmaster` VALUES ('50', '-1', '광산2', '76', '7', '광산1', '82', '67', 'FALSE');
INSERT INTO `portalmaster` VALUES ('51', '-1', '광산2', '15', '38', '광산3', '75', '10', 'FALSE');
INSERT INTO `portalmaster` VALUES ('52', '-1', '광산3', '76', '8', '광산2', '17', '39', 'FALSE');
INSERT INTO `portalmaster` VALUES ('53', '-1', '광산3', '77', '21', '광산4', '17', '39', 'FALSE');
INSERT INTO `portalmaster` VALUES ('54', '-1', '광산4', '15', '38', '광산3', '78', '23', 'FALSE');
INSERT INTO `portalmaster` VALUES ('55', '-1', '광산4', '139', '39', '광산5', '76', '12', 'FALSE');
INSERT INTO `portalmaster` VALUES ('56', '-1', '광산5', '76', '8', '광산4', '137', '40', 'FALSE');
INSERT INTO `portalmaster` VALUES ('57', '-1', '광산5', '85', '66', '광산6', '75', '9', 'FALSE');
INSERT INTO `portalmaster` VALUES ('58', '-1', '광산6', '76', '7', '광산5', '82', '67', 'FALSE');
INSERT INTO `portalmaster` VALUES ('59', '-1', '광산6', '15', '38', '광산7', '75', '10', 'FALSE');
INSERT INTO `portalmaster` VALUES ('60', '-1', '광산7', '76', '8', '광산6', '17', '39', 'FALSE');
INSERT INTO `portalmaster` VALUES ('61', '-1', '초보', '52', '43', '초보광산', '18', '38', 'FALSE');
INSERT INTO `portalmaster` VALUES ('62', '-1', '초보광산', '15', '38', '초보', '52', '45', 'FALSE');
INSERT INTO `portalmaster` VALUES ('63', '-1', 'Qweak1', '211', '85', 'Qweak2', '185', '35', 'FALSE');
INSERT INTO `portalmaster` VALUES ('64', '-1', 'Qweak2', '187', '33', 'Qweak1', '209', '87', 'FALSE');
INSERT INTO `portalmaster` VALUES ('65', '0', '영혼의마을,32', '99', '128', '터', '29', '32', 'FALSE');
INSERT INTO `portalmaster` VALUES ('66', '0', '터', '29', '30', '영혼의마을,32', '99', '130', 'FALSE');
INSERT INTO `portalmaster` VALUES ('67', '-1', '터', '11', '42', '010', '73', '8', 'FALSE');
INSERT INTO `portalmaster` VALUES ('68', '-1', '010', '73', '6', '터', '11', '44', 'FALSE');
INSERT INTO `portalmaster` VALUES ('69', '-1', '터', '11', '30', '020', '7', '8', 'FALSE');
INSERT INTO `portalmaster` VALUES ('70', '-1', '020', '7', '6', '터', '11', '32', 'FALSE');
INSERT INTO `portalmaster` VALUES ('71', '-1', '터', '11', '19', '030', '14', '11', 'FALSE');
INSERT INTO `portalmaster` VALUES ('72', '-1', '030', '14', '10', '터', '11', '21', 'FALSE');
INSERT INTO `portalmaster` VALUES ('73', '-1', '터', '22', '12', '040', '16', '18', 'FALSE');
INSERT INTO `portalmaster` VALUES ('74', '-1', '040', '16', '16', '터', '22', '14', 'FALSE');
INSERT INTO `portalmaster` VALUES ('75', '-1', '터', '35', '12', '050', '87', '46', 'FALSE');
INSERT INTO `portalmaster` VALUES ('76', '-1', '050', '87', '44', '터', '35', '14', 'FALSE');
INSERT INTO `portalmaster` VALUES ('77', '-1', '터', '47', '18', '060', '105', '78', 'FALSE');
INSERT INTO `portalmaster` VALUES ('78', '-1', '060', '105', '76', '터', '47', '20', 'FALSE');
INSERT INTO `portalmaster` VALUES ('79', '-1', '060', '12', '7', '061', '12', '12', 'FALSE');
INSERT INTO `portalmaster` VALUES ('80', '-1', '061', '12', '10', '060', '12', '9', 'FALSE');
INSERT INTO `portalmaster` VALUES ('81', '-1', '터', '47', '30', '070', '12', '10', 'FALSE');
INSERT INTO `portalmaster` VALUES ('82', '-1', '070', '12', '8', '터', '47', '32', 'FALSE');
INSERT INTO `portalmaster` VALUES ('83', '-1', '070', '92', '89', '071', '12', '10', 'FALSE');
INSERT INTO `portalmaster` VALUES ('84', '-1', '071', '12', '8', '070', '92', '91', 'FALSE');
INSERT INTO `portalmaster` VALUES ('85', '-1', '터', '47', '42', '080', '17', '9', 'FALSE');
INSERT INTO `portalmaster` VALUES ('86', '-1', '080', '17', '7', '터', '47', '44', 'FALSE');
INSERT INTO `portalmaster` VALUES ('87', '-1', '080', '87', '63', '081', '20', '17', 'FALSE');
INSERT INTO `portalmaster` VALUES ('88', '-1', '081', '20', '15', '080', '87', '65', 'FALSE');
INSERT INTO `portalmaster` VALUES ('89', '0', '영혼의마을,32', '70', '66', 'ItemVillage', '93', '110', 'FALSE');
INSERT INTO `portalmaster` VALUES ('90', '0', 'ItemVillage', '93', '108', '영혼의마을,32', '70', '68', 'FALSE');
INSERT INTO `portalmaster` VALUES ('91', '-1', 'ItemVillage', '2', '18', 'ItemPigmy1', '16', '31', 'FALSE');
INSERT INTO `portalmaster` VALUES ('92', '-1', 'ItemPigmy1', '16', '29', 'ItemVillage', '2', '20', 'FALSE');
INSERT INTO `portalmaster` VALUES ('93', '-1', 'ItemPigmy1', '41', '27', 'ItemPigmy2', '10', '12', 'FALSE');
INSERT INTO `portalmaster` VALUES ('94', '-1', 'ItemPigmy2', '10', '10', 'ItemPigmy1', '41', '29', 'FALSE');
INSERT INTO `portalmaster` VALUES ('95', '-1', 'ItemPigmy2', '67', '8', 'ItemPigmy3', '7', '27', 'FALSE');
INSERT INTO `portalmaster` VALUES ('96', '-1', 'ItemPigmy3', '7', '25', 'ItemPigmy2', '67', '10', 'FALSE');
INSERT INTO `portalmaster` VALUES ('97', '-1', 'ItemVillage', '25', '5', 'ItemZomby1', '14', '39', 'FALSE');
INSERT INTO `portalmaster` VALUES ('98', '-1', 'ItemZomby1', '12', '39', 'ItemVillage', '25', '7', 'FALSE');
INSERT INTO `portalmaster` VALUES ('99', '-1', 'ItemZomby1', '68', '42', 'ItemZomby2', '13', '14', 'FALSE');
INSERT INTO `portalmaster` VALUES ('100', '-1', 'ItemZomby2', '13', '12', 'ItemZomby1', '68', '44', 'FALSE');
INSERT INTO `portalmaster` VALUES ('101', '-1', 'ItemZomby2', '64', '20', 'ItemZomby3', '13', '12', 'FALSE');
INSERT INTO `portalmaster` VALUES ('102', '-1', 'ItemZomby3', '13', '10', 'ItemZomby2', '64', '22', 'FALSE');
INSERT INTO `portalmaster` VALUES ('103', '-1', 'ItemVillage', '186', '4', 'ItemSkel1', '20', '31', 'FALSE');
INSERT INTO `portalmaster` VALUES ('104', '-1', 'ItemSkel1', '20', '29', 'ItemVillage', '186', '6', 'FALSE');
INSERT INTO `portalmaster` VALUES ('105', '-1', 'ItemSkel1', '42', '21', 'ItemSkel2', '12', '13', 'FALSE');
INSERT INTO `portalmaster` VALUES ('106', '-1', 'ItemSkel2', '11', '12', 'ItemSkel1', '42', '23', 'FALSE');
INSERT INTO `portalmaster` VALUES ('107', '-1', 'ItemSkel2', '59', '12', 'ItemSkel3', '15', '18', 'FALSE');
INSERT INTO `portalmaster` VALUES ('108', '-1', 'ItemSkel3', '15', '17', 'ItemSkel2', '58', '12', 'FALSE');
INSERT INTO `portalmaster` VALUES ('109', '-1', 'ItemVillage', '190', '29', 'ItemButcher1', '19', '25', 'FALSE');
INSERT INTO `portalmaster` VALUES ('110', '-1', 'ItemButcher1', '19', '23', 'ItemVillage', '190', '31', 'FALSE');
INSERT INTO `portalmaster` VALUES ('111', '-1', 'ItemButcher1', '43', '27', 'ItemButcher2', '19', '13', 'FALSE');
INSERT INTO `portalmaster` VALUES ('112', '-1', 'ItemButcher2', '19', '11', 'ItemButcher1', '43', '29', 'FALSE');
INSERT INTO `portalmaster` VALUES ('113', '-1', 'ItemButcher2', '57', '29', 'ItemButcher3', '20', '15', 'FALSE');
INSERT INTO `portalmaster` VALUES ('114', '-1', 'ItemButcher3', '20', '13', 'ItemButcher2', '57', '31', 'FALSE');
INSERT INTO `portalmaster` VALUES ('115', '-1', 'ItemVillage', '193', '186', 'ItemMummy1', '18', '19', 'FALSE');
INSERT INTO `portalmaster` VALUES ('116', '-1', 'ItemMummy1', '18', '17', 'ItemVillage', '193', '188', 'FALSE');
INSERT INTO `portalmaster` VALUES ('117', '-1', 'ItemMummy1', '47', '34', 'ItemMummy2', '13', '11', 'FALSE');
INSERT INTO `portalmaster` VALUES ('118', '-1', 'ItemMummy2', '13', '9', 'ItemMummy1', '47', '36', 'FALSE');
INSERT INTO `portalmaster` VALUES ('119', '-1', 'ItemMummy2', '62', '36', 'ItemMummy3', '7', '9', 'FALSE');
INSERT INTO `portalmaster` VALUES ('120', '-1', 'ItemMummy3', '7', '7', 'ItemMummy2', '62', '38', 'FALSE');
INSERT INTO `portalmaster` VALUES ('121', '-1', 'ItemVillage', '177', '160', 'ItemStone1', '16', '14', 'FALSE');
INSERT INTO `portalmaster` VALUES ('122', '-1', 'ItemStone1', '16', '12', 'ItemVillage', '177', '162', 'FALSE');
INSERT INTO `portalmaster` VALUES ('123', '-1', 'ItemStone1', '31', '30', 'ItemStone2', '22', '15', 'FALSE');
INSERT INTO `portalmaster` VALUES ('124', '-1', 'ItemStone2', '22', '13', 'ItemStone1', '31', '32', 'FALSE');
INSERT INTO `portalmaster` VALUES ('125', '-1', 'ItemStone2', '44', '34', 'ItemStone3', '16', '18', 'FALSE');
INSERT INTO `portalmaster` VALUES ('126', '-1', 'ItemStone3', '16', '16', 'ItemStone2', '44', '36', 'FALSE');
INSERT INTO `portalmaster` VALUES ('127', '-1', 'ItemStone3', '37', '32', 'ItemStone4', '6', '23', 'FALSE');
INSERT INTO `portalmaster` VALUES ('128', '-1', 'ItemStone4', '6', '21', 'ItemStone3', '37', '34', 'FALSE');
INSERT INTO `portalmaster` VALUES ('129', '-1', 'ItemVillage', '23', '135', 'ItemDummy1', '18', '19', 'FALSE');
INSERT INTO `portalmaster` VALUES ('130', '-1', 'ItemDummy1', '18', '17', 'ItemVillage', '23', '136', 'FALSE');
INSERT INTO `portalmaster` VALUES ('131', '-1', 'ItemDummy1', '47', '34', 'ItemDummy2', '13', '11', 'FALSE');
INSERT INTO `portalmaster` VALUES ('132', '-1', 'ItemDummy2', '18', '17', 'ItemDummy1', '13', '9', 'FALSE');
INSERT INTO `portalmaster` VALUES ('133', '-1', 'ItemDummy2', '62', '36', 'ItemDummy3', '7', '9', 'FALSE');
INSERT INTO `portalmaster` VALUES ('134', '-1', 'ItemDummy3', '7', '7', 'ItemDummy2', '62', '38', 'FALSE');
INSERT INTO `portalmaster` VALUES ('135', '-1', 'ItemDummy3', '66', '12', 'ItemDummy4', '16', '18', 'FALSE');
INSERT INTO `portalmaster` VALUES ('136', '-1', 'ItemDummy4', '16', '16', 'ItemDummy3', '66', '14', 'FALSE');
INSERT INTO `portalmaster` VALUES ('137', '-1', 'ItemDummy4', '37', '32', 'ItemDummy5', '6', '23', 'FALSE');
INSERT INTO `portalmaster` VALUES ('138', '-1', 'ItemDummy5', '6', '21', 'ItemDummy4', '37', '34', 'FALSE');
INSERT INTO `portalmaster` VALUES ('139', '-1', 'ItemVillage', '38', '142', 'ItemWarDummy1', '18', '19', 'FALSE');
INSERT INTO `portalmaster` VALUES ('140', '-1', 'ItemWarDummy1', '18', '17', 'ItemVillage', '38', '143', 'FALSE');
INSERT INTO `portalmaster` VALUES ('141', '-1', 'ItemWarDummy1', '47', '34', 'ItemWarDummy2', '13', '11', 'FALSE');
INSERT INTO `portalmaster` VALUES ('142', '-1', 'ItemWarDummy2', '18', '17', 'ItemWarDummy1', '13', '9', 'FALSE');
INSERT INTO `portalmaster` VALUES ('143', '-1', 'ItemWarDummy2', '62', '36', 'ItemWarDummy3', '7', '9', 'FALSE');
INSERT INTO `portalmaster` VALUES ('144', '-1', 'ItemWarDummy3', '7', '7', 'ItemWarDummy2', '62', '38', 'FALSE');
INSERT INTO `portalmaster` VALUES ('145', '-1', 'ItemWarDummy3', '66', '12', 'ItemWarDummy4', '16', '18', 'FALSE');
INSERT INTO `portalmaster` VALUES ('146', '-1', 'ItemWarDummy4', '16', '16', 'ItemWarDummy3', '66', '14', 'FALSE');
INSERT INTO `portalmaster` VALUES ('147', '-1', 'ItemWarDummy4', '37', '32', 'ItemWarDummy5', '6', '23', 'FALSE');
INSERT INTO `portalmaster` VALUES ('148', '-1', 'ItemWarDummy5', '6', '21', 'ItemWarDummy4', '37', '34', 'FALSE');
INSERT INTO `portalmaster` VALUES ('149', '-1', 'ItemVillage', '13', '45', 'ItemHardboil1', '18', '19', 'FALSE');
INSERT INTO `portalmaster` VALUES ('150', '-1', 'ItemHardboil1', '18', '17', 'ItemVillage', '13', '46', 'FALSE');
INSERT INTO `portalmaster` VALUES ('151', '-1', 'ItemHardboil1', '47', '34', 'ItemHardboil2', '13', '11', 'FALSE');
INSERT INTO `portalmaster` VALUES ('152', '-1', 'ItemHardboil2', '13', '9', 'ItemHardboil1', '47', '35', 'FALSE');
INSERT INTO `portalmaster` VALUES ('153', '-1', 'ItemHardboil2', '62', '36', 'ItemHardboil3', '7', '9', 'FALSE');
INSERT INTO `portalmaster` VALUES ('154', '-1', 'ItemHardboil3', '7', '7', 'ItemHardboil2', '62', '38', 'FALSE');
INSERT INTO `portalmaster` VALUES ('155', '-1', 'ItemHardboil3', '66', '12', 'ItemHardboil4', '16', '18', 'FALSE');
INSERT INTO `portalmaster` VALUES ('156', '-1', 'ItemHardboil4', '16', '16', 'ItemHardboil3', '66', '14', 'FALSE');
INSERT INTO `portalmaster` VALUES ('157', '-1', 'ItemHardboil4', '37', '32', 'ItemHardboil5', '6', '23', 'FALSE');
INSERT INTO `portalmaster` VALUES ('158', '-1', 'ItemHardboil5', '6', '21', 'ItemHardboil4', '37', '34', 'FALSE');
INSERT INTO `portalmaster` VALUES ('159', '-1', 'ItemVillage', '7', '178', 'ItemGolem1', '18', '19', 'FALSE');
INSERT INTO `portalmaster` VALUES ('160', '-1', 'ItemGolem1', '18', '17', 'ItemVillage', '7', '179', 'FALSE');
INSERT INTO `portalmaster` VALUES ('161', '-1', 'ItemGolem1', '47', '34', 'ItemGolem2', '13', '11', 'FALSE');
INSERT INTO `portalmaster` VALUES ('162', '-1', 'ItemGolem2', '18', '17', 'ItemGolem1', '13', '9', 'FALSE');
INSERT INTO `portalmaster` VALUES ('163', '-1', 'ItemGolem2', '62', '36', 'ItemGolem3', '7', '9', 'FALSE');
INSERT INTO `portalmaster` VALUES ('164', '-1', 'ItemGolem3', '7', '7', 'ItemGolem2', '62', '38', 'FALSE');
INSERT INTO `portalmaster` VALUES ('165', '-1', 'ItemGolem3', '66', '12', 'ItemGolem4', '16', '18', 'FALSE');
INSERT INTO `portalmaster` VALUES ('166', '-1', 'ItemGolem4', '16', '16', 'ItemGolem3', '66', '14', 'FALSE');
INSERT INTO `portalmaster` VALUES ('167', '-1', 'ItemGolem4', '37', '32', 'ItemGolem5', '6', '23', 'FALSE');
INSERT INTO `portalmaster` VALUES ('168', '-1', 'ItemGolem5', '6', '21', 'ItemGolem4', '37', '34', 'FALSE');
INSERT INTO `portalmaster` VALUES ('169', '0', '영혼의마을,32', '139', '134', '스켈1', '50', '13', 'FALSE');
INSERT INTO `portalmaster` VALUES ('170', '0', '스켈1', '50', '11', '영혼의마을,32', '138', '136', 'FALSE');
INSERT INTO `portalmaster` VALUES ('171', '-1', '스켈1', '13', '47', '스켈2', '12', '14', 'FALSE');
INSERT INTO `portalmaster` VALUES ('172', '-1', '스켈2', '12', '12', '스켈1', '13', '49', 'FALSE');
INSERT INTO `portalmaster` VALUES ('173', '-1', '스켈2', '48', '42', '스켈3', '34', '14', 'FALSE');
INSERT INTO `portalmaster` VALUES ('174', '-1', '스켈3', '34', '12', '스켈2', '48', '44', 'FALSE');
INSERT INTO `portalmaster` VALUES ('175', '-1', '스켈3', '33', '49', '스켈4', '33', '24', 'FALSE');
INSERT INTO `portalmaster` VALUES ('176', '-1', '스켈4', '33', '22', '스켈3', '33', '51', 'FALSE');
INSERT INTO `portalmaster` VALUES ('177', '-1', '스켈4', '32', '35', '스켈5', '14', '21', 'FALSE');
INSERT INTO `portalmaster` VALUES ('178', '-1', '스켈5', '14', '19', '스켈4', '32', '37', 'FALSE');
INSERT INTO `portalmaster` VALUES ('179', '0', '영혼의마을,32', '65', '122', 'Weakly1', '11', '19', 'FALSE');
INSERT INTO `portalmaster` VALUES ('180', '0', 'Weakly1', '11', '17', '영혼의마을,32', '66', '123', 'FALSE');
INSERT INTO `portalmaster` VALUES ('181', '-1', 'Weakly1', '50', '44', 'Weakly2', '51', '14', 'FALSE');
INSERT INTO `portalmaster` VALUES ('182', '-1', 'Weakly2', '51', '12', 'Weakly1', '50', '46', 'FALSE');
INSERT INTO `portalmaster` VALUES ('183', '-1', 'Weakly2', '38', '53', 'Weakly3', '53', '15', 'FALSE');
INSERT INTO `portalmaster` VALUES ('184', '-1', 'Weakly3', '53', '13', 'Weakly2', '38', '55', 'FALSE');
INSERT INTO `portalmaster` VALUES ('185', '-1', 'Weakly3', '9', '34', 'Weakly4', '9', '54', 'FALSE');
INSERT INTO `portalmaster` VALUES ('186', '-1', 'Weakly4', '9', '52', 'Weakly3', '9', '36', 'FALSE');
INSERT INTO `portalmaster` VALUES ('187', '-1', 'Weakly4', '52', '51', 'Weakly5', '9', '11', 'FALSE');
INSERT INTO `portalmaster` VALUES ('188', '-1', 'Weakly5', '9', '9', 'Weakly4', '52', '53', 'FALSE');
INSERT INTO `portalmaster` VALUES ('189', '-1', '골램', '41', '49', 'Golem1', '1', '1', 'FALSE');
INSERT INTO `portalmaster` VALUES ('190', '-1', 'Golem1', '0', '0', '골램', '41', '48', 'FALSE');
INSERT INTO `portalmaster` VALUES ('191', '-1', '골램', '49', '5', 'Golem3', '27', '1', 'FALSE');
INSERT INTO `portalmaster` VALUES ('192', '-1', 'Golem3', '27', '0', '골램', '48', '5', 'FALSE');
INSERT INTO `portalmaster` VALUES ('193', '-1', 'Golem1', '35', '49', 'Golem2', '33', '27', 'FALSE');
INSERT INTO `portalmaster` VALUES ('194', '-1', 'Golem2', '33', '26', 'Golem1', '35', '48', 'FALSE');
INSERT INTO `portalmaster` VALUES ('195', '-1', 'Golem1', '40', '0', 'Golem12', '1', '2', 'FALSE');
INSERT INTO `portalmaster` VALUES ('196', '-1', 'Golem12', '0', '2', 'Golem1', '40', '1', 'FALSE');
INSERT INTO `portalmaster` VALUES ('197', '-1', 'TreasureLand', '45', '71', 'Great', '19', '22', 'FALSE');
INSERT INTO `portalmaster` VALUES ('198', '-1', 'Great', '19', '21', 'TreasureLand', '45', '72', 'FALSE');
INSERT INTO `portalmaster` VALUES ('200', '1', '정령의마을,32', '139', '155', '시약상', '6', '14', 'FALSE');
INSERT INTO `portalmaster` VALUES ('201', '1', '시약상', '5', '15', '정령의마을,32', '139', '157', 'FALSE');
INSERT INTO `portalmaster` VALUES ('202', '1', '정령의마을,32', '65', '158', '대장간', '5', '17', 'FALSE');
INSERT INTO `portalmaster` VALUES ('203', '1', '대장간', '4', '17', '정령의마을,32', '65', '160', 'FALSE');
INSERT INTO `portalmaster` VALUES ('204', '1', '정령의마을,42', '61', '114', '휴게실', '19', '14', 'FALSE');
INSERT INTO `portalmaster` VALUES ('205', '1', '휴게실', '19', '12', '정령의마을,42', '61', '116', 'FALSE');
INSERT INTO `portalmaster` VALUES ('206', '1', '정령의마을,41', '96', '124', '퀘스트', '14', '5', 'FALSE');
INSERT INTO `portalmaster` VALUES ('207', '1', '퀘스트', '14', '3', '정령의마을,41', '96', '126', 'FALSE');
INSERT INTO `portalmaster` VALUES ('208', '1', '정령의마을,32', '167', '122', 'Biggun1', '98', '78', 'FALSE');
INSERT INTO `portalmaster` VALUES ('209', '1', 'Biggun1', '99', '79', '정령의마을,32', '164', '122', 'FALSE');
INSERT INTO `portalmaster` VALUES ('244', '1', '지구라트5', '150', '134', '정령의마을,32', '90', '176', 'FALSE');
INSERT INTO `portalmaster` VALUES ('263', '1', '정령의마을,32', '99', '128', '터', '29', '32', 'FALSE');
INSERT INTO `portalmaster` VALUES ('264', '1', '터', '29', '30', '정령의마을,32', '99', '130', 'FALSE');
INSERT INTO `portalmaster` VALUES ('287', '1', '정령의마을,32', '70', '66', 'ItemVillage', '93', '110', 'FALSE');
INSERT INTO `portalmaster` VALUES ('288', '1', 'ItemVillage', '93', '108', '정령의마을,32', '70', '68', 'FALSE');
INSERT INTO `portalmaster` VALUES ('367', '1', '정령의마을,32', '139', '134', '스켈1', '50', '13', 'FALSE');
INSERT INTO `portalmaster` VALUES ('368', '1', '스켈1', '50', '11', '정령의마을,32', '138', '136', 'FALSE');
INSERT INTO `portalmaster` VALUES ('377', '1', '정령의마을,32', '65', '122', 'Weakly1', '11', '19', 'FALSE');
INSERT INTO `portalmaster` VALUES ('378', '1', 'Weakly1', '11', '17', '정령의마을,32', '66', '123', 'FALSE');
INSERT INTO `portalmaster` VALUES ('397', '2', '새벽의마을,32', '139', '155', '시약상', '6', '14', 'TRUE');
INSERT INTO `portalmaster` VALUES ('398', '2', '시약상', '5', '15', '새벽의마을,32', '139', '157', 'TRUE');
INSERT INTO `portalmaster` VALUES ('399', '2', '새벽의마을,32', '65', '158', '대장간', '5', '17', 'TRUE');
INSERT INTO `portalmaster` VALUES ('400', '2', '대장간', '4', '17', '새벽의마을,32', '65', '160', 'TRUE');
INSERT INTO `portalmaster` VALUES ('401', '2', '새벽의마을,42', '61', '114', '휴게실', '19', '14', 'TRUE');
INSERT INTO `portalmaster` VALUES ('402', '2', '휴게실', '19', '12', '새벽의마을,42', '61', '116', 'TRUE');
INSERT INTO `portalmaster` VALUES ('403', '2', '새벽의마을,41', '96', '124', '퀘스트', '14', '5', 'TRUE');
INSERT INTO `portalmaster` VALUES ('404', '2', '퀘스트', '14', '3', '새벽의마을,41', '96', '126', 'TRUE');
INSERT INTO `portalmaster` VALUES ('405', '2', '새벽의마을,32', '167', '122', 'Biggun1', '98', '78', 'TRUE');
INSERT INTO `portalmaster` VALUES ('406', '2', 'Biggun1', '99', '79', '새벽의마을,32', '164', '122', 'TRUE');
INSERT INTO `portalmaster` VALUES ('407', '2', '지구라트5', '150', '134', '새벽의마을,32', '90', '176', 'TRUE');
INSERT INTO `portalmaster` VALUES ('408', '2', '새벽의마을,32', '99', '128', '터', '29', '32', 'TRUE');
INSERT INTO `portalmaster` VALUES ('409', '2', '터', '29', '30', '새벽의마을,32', '99', '130', 'TRUE');
INSERT INTO `portalmaster` VALUES ('410', '2', '새벽의마을,32', '70', '66', 'ItemVillage', '93', '110', 'TRUE');
INSERT INTO `portalmaster` VALUES ('411', '2', 'ItemVillage', '93', '108', '새벽의마을,32', '70', '68', 'TRUE');
INSERT INTO `portalmaster` VALUES ('412', '2', '새벽의마을,32', '139', '134', '스켈1', '50', '13', 'TRUE');
INSERT INTO `portalmaster` VALUES ('413', '2', '스켈1', '50', '11', '새벽의마을,32', '138', '136', 'TRUE');
INSERT INTO `portalmaster` VALUES ('414', '2', '새벽의마을,32', '65', '122', 'Weakly1', '11', '19', 'TRUE');
INSERT INTO `portalmaster` VALUES ('415', '2', 'Weakly1', '11', '17', '새벽의마을,32', '66', '123', 'TRUE');
INSERT INTO `portalmaster` VALUES ('416', '-1', '광산7', '77', '21', '광산8', '17', '39', 'FALSE');
INSERT INTO `portalmaster` VALUES ('417', '-1', '광산8', '15', '38', '광산7', '77', '21', 'FALSE');
INSERT INTO `serversetting` VALUES ('1', 'Helper', 'Level', '15');
INSERT INTO `serversetting` VALUES ('2', 'Guild', 'MinLevel', '50');
INSERT INTO `serversetting` VALUES ('3', 'Guild', 'CreateGold', '30000000');
INSERT INTO `serversetting` VALUES ('4', 'Guild', 'CreateLevel', '0');
INSERT INTO `serversetting` VALUES ('5', 'Guild', 'CreateAlarm', 'true');
INSERT INTO `serversetting` VALUES ('6', 'Guild', 'MemberLimit', '25');
INSERT INTO `serversetting` VALUES ('7', 'General', 'ShowRange', '18');
INSERT INTO `serversetting` VALUES ('8', 'General', 'LoginAlarm', 'TRUE');
INSERT INTO `serversetting` VALUES ('9', 'General', 'GuildLoginAlarm', 'TRUE');
INSERT INTO `serversetting` VALUES ('10', 'General', 'TimeAlarm', 'TRUE');
INSERT INTO `serversetting` VALUES ('11', 'General', 'PromotoAlarm', 'TRUE');
INSERT INTO `serversetting` VALUES ('12', 'PK', 'ProtectLevel', '20');
INSERT INTO `serversetting` VALUES ('13', 'PK', 'PKAlarm', 'TRUE');
INSERT INTO `serversetting` VALUES ('14', 'PK', 'PKDelay', '120000');
INSERT INTO `serversetting` VALUES ('15', 'PK', 'PKRedCount', '5');
INSERT INTO `serversetting` VALUES ('16', 'PK', 'PrisonCount', '10');
INSERT INTO `serversetting` VALUES ('17', 'PK', 'PKCmdLevel', '1');
INSERT INTO `serversetting` VALUES ('18', 'PK', 'PKCmdTime', '600000');
INSERT INTO `serversetting` VALUES ('19', 'PK', 'MacroKillXP', '0.0');
INSERT INTO `serversetting` VALUES ('20', 'General', 'SaveTime', '180000');
INSERT INTO `serversetting` VALUES ('21', 'General', 'MaxLevel', '231');
INSERT INTO `serversetting` VALUES ('22', 'General', 'TrunXP', '100000');
INSERT INTO `serversetting` VALUES ('23', 'General', 'DieDropGold', 'TRUE');
INSERT INTO `serversetting` VALUES ('24', 'General', 'DieDropItem', 'FALSE');
INSERT INTO `serversetting` VALUES ('25', 'General', 'XPRateTime', '0');
INSERT INTO `serversetting` VALUES ('26', 'General', 'XPRate', '1.0');
INSERT INTO `serversetting` VALUES ('27', 'General', 'GoldRateTime', '0');
INSERT INTO `serversetting` VALUES ('28', 'General', 'GoldRate', '1.0');
INSERT INTO `serversetting` VALUES ('29', 'General', 'QuestTime', '180000');
INSERT INTO `serversetting` VALUES ('30', 'General', 'PebbleSpwnAmount', '10');
INSERT INTO `serversetting` VALUES ('31', 'General', 'PebbleSpwnRange', '5');
INSERT INTO `serversetting` VALUES ('32', 'General', 'OverlapLogin', 'FALSE');
INSERT INTO `serversetting` VALUES ('33', 'Neighbor', 'Level', '30');
INSERT INTO `serversetting` VALUES ('34', 'Neighbor', 'Account', 'FALSE');
INSERT INTO `serversetting` VALUES ('35', 'Neighbor', 'IP', 'FALSE');
INSERT INTO `serversetting` VALUES ('36', 'GlobalChat', 'PinkRankCount', '5');
INSERT INTO `serversetting` VALUES ('37', 'GlobalChat', 'GrayRankCount', '20');
INSERT INTO `vilagemaster` VALUES ('-1', 'ALL', 'FALSE');
INSERT INTO `vilagemaster` VALUES ('0', '영혼의마을', 'FALSE');
INSERT INTO `vilagemaster` VALUES ('1', '정령의마을', 'FALSE');
INSERT INTO `vilagemaster` VALUES ('2', '새벽의마을', 'FALSE');
INSERT INTO `vilagemaster` VALUES ('3', '창공의마을', 'FALSE');
INSERT INTO `vilagemaster` VALUES ('4', '신비의땅', 'FALSE');
INSERT INTO `vilagemaster` VALUES ('5', '약속의땅', 'FALSE');
INSERT INTO `vilagemaster` VALUES ('6', '모험의땅', 'FALSE');
INSERT INTO `vilagemaster` VALUES ('7', '어둠의땅', 'FALSE');
INSERT INTO `vilagemaster` VALUES ('8', '미지의땅', 'FALSE');
INSERT INTO `vilagemaster` VALUES ('9', '시간마을', 'FALSE');
INSERT INTO `vilagemaster` VALUES ('10', '이웃마을', 'FALSE');
INSERT INTO `vilagemaster` VALUES ('11', '초보마을', 'FALSE');
