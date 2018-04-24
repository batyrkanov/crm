/*
Navicat SQL Server Data Transfer

Source Server         : Alisher
Source Server Version : 120000
Source Host           : 192.168.1.193:1433
Source Database       : CommonDb
Source Schema         : gbb

Target Server Type    : SQL Server
Target Server Version : 120000
File Encoding         : 65001

Date: 2018-04-24 14:11:32
*/


-- ----------------------------
-- Table structure for Categories
-- ----------------------------
DROP TABLE [gbb].[Categories]
GO
CREATE TABLE [gbb].[Categories] (
[CategoryId] nvarchar(128) NOT NULL ,
[CategoryName] nvarchar(255) NULL 
)


GO

-- ----------------------------
-- Records of Categories
-- ----------------------------

-- ----------------------------
-- Table structure for Companies
-- ----------------------------
DROP TABLE [gbb].[Companies]
GO
CREATE TABLE [gbb].[Companies] (
[CompanyId] nvarchar(128) NOT NULL ,
[CompanyName] nvarchar(255) NULL 
)


GO

-- ----------------------------
-- Records of Companies
-- ----------------------------

-- ----------------------------
-- Table structure for Tasks
-- ----------------------------
DROP TABLE [gbb].[Tasks]
GO
CREATE TABLE [gbb].[Tasks] (
[TaskId] nvarchar(128) NOT NULL ,
[TaskName] nvarchar(255) NULL ,
[TaskDate] datetime2(7) NULL ,
[TaskStatus] nvarchar(128) NULL ,
[CompanyName] nvarchar(128) NULL ,
[CategoryName] nvarchar(128) NULL ,
[ManagerName] nvarchar(128) NULL ,
[Description] text NULL 
)


GO

-- ----------------------------
-- Records of Tasks
-- ----------------------------

-- ----------------------------
-- Table structure for TaskStatuses
-- ----------------------------
DROP TABLE [gbb].[TaskStatuses]
GO
CREATE TABLE [gbb].[TaskStatuses] (
[StatusId] nvarchar(128) NOT NULL ,
[StatusName] nvarchar(255) NULL 
)


GO

-- ----------------------------
-- Records of TaskStatuses
-- ----------------------------

-- ----------------------------
-- Indexes structure for table Categories
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Categories
-- ----------------------------
ALTER TABLE [gbb].[Categories] ADD PRIMARY KEY ([CategoryId])
GO

-- ----------------------------
-- Indexes structure for table Companies
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Companies
-- ----------------------------
ALTER TABLE [gbb].[Companies] ADD PRIMARY KEY ([CompanyId])
GO

-- ----------------------------
-- Indexes structure for table Tasks
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Tasks
-- ----------------------------
ALTER TABLE [gbb].[Tasks] ADD PRIMARY KEY ([TaskId])
GO

-- ----------------------------
-- Indexes structure for table TaskStatuses
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table TaskStatuses
-- ----------------------------
ALTER TABLE [gbb].[TaskStatuses] ADD PRIMARY KEY ([StatusId])
GO

-- ----------------------------
-- Foreign Key structure for table [gbb].[Tasks]
-- ----------------------------
ALTER TABLE [gbb].[Tasks] ADD FOREIGN KEY ([CategoryName]) REFERENCES [gbb].[Categories] ([CategoryId]) ON DELETE SET NULL ON UPDATE SET NULL
GO
ALTER TABLE [gbb].[Tasks] ADD FOREIGN KEY ([CompanyName]) REFERENCES [gbb].[Companies] ([CompanyId]) ON DELETE SET NULL ON UPDATE SET NULL
GO
ALTER TABLE [gbb].[Tasks] ADD FOREIGN KEY ([TaskStatus]) REFERENCES [gbb].[TaskStatuses] ([StatusId]) ON DELETE SET NULL ON UPDATE SET NULL
GO
