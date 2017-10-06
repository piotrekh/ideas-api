CREATE TABLE dbo.RefreshToken
(
	Id int IDENTITY(1,1) NOT NULL, 
	AspNetUserId int NOT NULL,
	Token uniqueidentifier NOT NULL,
	ApiClientId int NOT NULL,	
	IssueDate datetime2 NOT NULL,
	ExpirationDate datetime2 NOT NULL,

	CONSTRAINT PK_RefreshToken PRIMARY KEY(Id),
	CONSTRAINT FK_RefreshToken_AspNetUser_AspnetUserId FOREIGN KEY(AspNetUserId) REFERENCES dbo.AspNetUser(Id),
	CONSTRAINT FK_RefreshToken_ApiClient_ApiClientId FOREIGN KEY(ApiClientId) REFERENCES dbo.ApiClient(Id),
)

CREATE INDEX IX_RefreshToken_AspNetUserId ON dbo.RefreshToken(AspNetUserId)
CREATE INDEX IX_RefreshToken_ApiClientId ON dbo.RefreshToken(ApiClientId)

CREATE INDEX IX_RefreshToken_Token ON dbo.RefreshToken(Token) INCLUDE (AspNetUserId, ApiClientId, ExpirationDate)