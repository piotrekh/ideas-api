CREATE TABLE dbo.IdeaCategory
(
	Id int NOT NULL IDENTITY,
	Name nvarchar(50) NOT NULL,
	CreatedDate datetime2 NOT NULL,
	CONSTRAINT PK_IdeaCategory PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT UQ_IdeaCategory_Name UNIQUE (Name)
)

CREATE TABLE dbo.IdeaSubcategory
(
	Id int NOT NULL IDENTITY,
	IdeaCategoryId int NOT NULL,
	Name nvarchar(50) NOT NULL,
	CreatedDate datetime2 NOT NULL,
	CONSTRAINT PK_IdeaSubcategory PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT UQ_IdeaSubcategory_IdeaCategoryId_Name UNIQUE NONCLUSTERED (IdeaCategoryId, Name),
	CONSTRAINT FK_IdeaSubcategory_IdeaCategory_IdeaCategoryId FOREIGN KEY (IdeaCategoryId) REFERENCES dbo.IdeaCategory(Id)
)

CREATE TABLE dbo.Idea
(
	Id int NOT NULL IDENTITY,
	AspNetUserId int NOT NULL,
	Title nvarchar(255) NOT NULL,
	Description nvarchar(max),
	CreatedDate datetime2 NOT NULL,
	IdeaCategoryId int NOT NULL,
	CONSTRAINT PK_Idea PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT FK_Idea_AspNetUser_AspNetUserId FOREIGN KEY (AspNetUserId) REFERENCES dbo.AspNetUser(Id),
	CONSTRAINT FK_Idea_IdeaCategory_IdeaCategoryId FOREIGN KEY (IdeaCategoryId) REFERENCES dbo.IdeaCategory(Id)
)

CREATE TABLE dbo.AssignedIdeaSubcategory
(
	IdeaId int NOT NULL,
	IdeaSubcategoryId int NOT NULL,
	CONSTRAINT PK_AssignedIdeaSubcategory PRIMARY KEY CLUSTERED (IdeaId, IdeaSubcategoryId),
	CONSTRAINT FK_AssignedIdeaSubcategory_Idea_IdeaId FOREIGN KEY (IdeaId) REFERENCES dbo.Idea(Id),
	CONSTRAINT FK_AssignedIdeaSubcategory_IdeaSubcategory_IdeaSubcategoryId FOREIGN KEY (IdeaSubcategoryId) REFERENCES dbo.IdeaSubcategory(Id)
)