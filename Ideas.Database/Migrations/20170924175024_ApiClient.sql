CREATE TABLE dbo.ApiClient
(
	Id int IDENTITY(1,1) NOT NULL,	
	ExternalId uniqueidentifier NOT NULL,
	Name varchar(20) NOT NULL,	

	CONSTRAINT PK_ApiClient PRIMARY KEY(Id),
	CONSTRAINT UC_PK_ApiClient_ExternalId UNIQUE (ExternalId),
	CONSTRAINT UC_PK_ApiClient_Name UNIQUE (Name)
)

INSERT INTO dbo.ApiClient (ExternalId, Name)
VALUES
('B4D4B78F-E715-433A-940B-E06A05581B27', 'web')