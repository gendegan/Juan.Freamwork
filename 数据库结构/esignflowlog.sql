CREATE TABLE `bizhi_esignflowlog_tb` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `GUID` VARCHAR(36) DEFAULT '' COMMENT 'GUID',
  `UserID` INT(11) DEFAULT '0' COMMENT 'UserID',
  `FileID` VARCHAR(100) DEFAULT '' COMMENT '待签署合同文件id',
  `FileName` VARCHAR(50) DEFAULT '' COMMENT '待签署合同文件名称',
  `SignFlowId` VARCHAR(50) DEFAULT '' COMMENT '签署流程id',
  `SignerType` INT(11) DEFAULT '0' COMMENT '签署人类型(0-个人 1-机构 2-法定代表人)',
  `SignerID` VARCHAR(50) DEFAULT '' COMMENT '签署人id',  
  `FlowStatus` INT(11) DEFAULT '0' COMMENT '流程状态(0-未结束，1-已结束)',
  `StartTime` DATETIME NOT NULL DEFAULT '1900-01-01 00:00:00' COMMENT '开始时间',
  `FinishTime` DATETIME NOT NULL DEFAULT '1900-01-01 00:00:00' COMMENT '结束时间',
  PRIMARY KEY (`ID`),
  KEY `SignFlowId` (`SignFlowId`)
) ENGINE=INNODB DEFAULT CHARSET=utf8 COMMENT='e签宝签署流程记录'
