CREATE TABLE `bizhi_userbindwxlog_tb` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT COMMENT '标识ID',
  `GUID` VARCHAR(36) DEFAULT '' COMMENT 'GUID',
  `UserID` INT(11) DEFAULT NULL COMMENT 'UserID',
  `BindType` INT(11) DEFAULT '0' COMMENT '绑定类型',
  `WxAccount` VARCHAR(100) DEFAULT '' COMMENT '账户名称',
  `WxOpenID` VARCHAR(100) DEFAULT '' COMMENT '微信openID',
  `BzOpenID` VARCHAR(100) DEFAULT '' COMMENT '壁纸g7udid',
  `UniqueID` BIGINT(20) DEFAULT '0' COMMENT '唯一标识(BzOpenID+WxOpenID)MD5',
  `BindTime` DATETIME DEFAULT '1900-01-01 00:00:00' COMMENT '最后一次绑定时间',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `UniqueKey` (`UniqueID`),
  KEY `UniqueID` (`UniqueID`)
) ENGINE=INNODB DEFAULT CHARSET=utf8 COMMENT='壁纸用户绑定微信账户记录'
