/*
Navicat SQL Server Data Transfer

Source Server         : Alisher
Source Server Version : 120000
Source Host           : 192.168.1.193:1433
Source Database       : CommonDb
Source Schema         : dbo

Target Server Type    : SQL Server
Target Server Version : 120000
File Encoding         : 65001

Date: 2018-04-24 14:11:32
*/



-- ----------------------------
-- Table structure for Categories
-- ----------------------------



DROP TABLE [dbo].[Categories]
GO
CREATE TABLE [dbo].[Categories] (
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
DROP TABLE [dbo].[Companies]
GO
CREATE TABLE [dbo].[Companies] (
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
DROP TABLE [dbo].[Tasks]
GO
CREATE TABLE [dbo].[Tasks] (
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
DROP TABLE [dbo].[TaskStatuses]
GO
CREATE TABLE [dbo].[TaskStatuses] (
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
ALTER TABLE [dbo].[Categories] ADD PRIMARY KEY ([CategoryId])
GO

-- ----------------------------
-- Indexes structure for table Companies
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Companies
-- ----------------------------
ALTER TABLE [dbo].[Companies] ADD PRIMARY KEY ([CompanyId])
GO

-- ----------------------------
-- Indexes structure for table Tasks
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Tasks
-- ----------------------------
ALTER TABLE [dbo].[Tasks] ADD PRIMARY KEY ([TaskId])
GO

-- ----------------------------
-- Indexes structure for table TaskStatuses
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table TaskStatuses
-- ----------------------------
ALTER TABLE [dbo].[TaskStatuses] ADD PRIMARY KEY ([StatusId])
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[Tasks]
-- ----------------------------
ALTER TABLE [dbo].[Tasks] ADD FOREIGN KEY ([CategoryName]) REFERENCES [dbo].[Categories] ([CategoryId]) ON DELETE SET NULL ON UPDATE SET NULL
GO
ALTER TABLE [dbo].[Tasks] ADD FOREIGN KEY ([CompanyName]) REFERENCES [dbo].[Companies] ([CompanyId]) ON DELETE SET NULL ON UPDATE SET NULL
GO
ALTER TABLE [dbo].[Tasks] ADD FOREIGN KEY ([TaskStatus]) REFERENCES [dbo].[TaskStatuses] ([StatusId]) ON DELETE SET NULL ON UPDATE SET NULL
GO
