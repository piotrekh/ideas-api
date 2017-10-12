--password for all test users is qwerty123

MERGE dbo.AspNetUser as dest
USING
(
	VALUES
	(0,	'20c6e2c8-b90e-46e9-a2c9-5a430d60ba66',	'admin@test.com', 1, 1,	NULL, 'ADMIN@TEST.COM', 'ADMIN@TEST.COM', 'AQAAAAEAACcQAAAAEEyzWbNnZie7k3hacGzJDvQIYgWSWTm5IQAfCsR3ZEDIMk/M3xU7eMzQX65IHQcG8Q==', NULL, 0, '7f54b8f1-54ad-4652-823d-cf6e6b0f7660', 0, 'admin@test.com',	'John', 'Doe'),
	(0,	'd01ee63b-d95e-46c2-9049-c1363744de64',	'user1@test.com', 1, 1,	NULL, 'USER1@TEST.COM', 'USER1@TEST.COM', 'AQAAAAEAACcQAAAAEJkXsv3bqMHgaLaYosqBQfHp4UDYCBHD7GspcuV6bOCpnuzlsIm/8EidyeS0KcmXzw==', NULL, 0, 'fa46ea9f-c2df-4ea9-8fbb-5073cb68598c', 0, 'user1@test.com',	'Lauren', 'Robbins'),
	(0,	'147a2ac0-e36b-4af5-90d9-40916e46cd93',	'user2@test.com', 1, 1,	NULL, 'USER2@TEST.COM', 'USER2@TEST.COM', 'AQAAAAEAACcQAAAAEONVDYVVBvsgwCRndTawlC38auVV/liVaNFtd8DxcD1ruI4bxP232h2/KZyfIH1R5w==', NULL, 0, 'cd5d6ab0-4664-42c4-8635-53b45df6a939', 0, 'user2@test.com',	'Jerry', 'Brooks'),
	(0,	'c8a289ba-6e53-476f-8da2-d1aee0968268',	'user3@test.com', 1, 1,	NULL, 'USER3@TEST.COM', 'USER3@TEST.COM', 'AQAAAAEAACcQAAAAEPdTAzHJqYO4xss103f2s6pRJn09X31c0C3LnjoOUh8Fk4PByyt6lQbNuTttZcyFVA==', NULL, 0, '59fc6d99-3323-43c3-b12f-df7489fbad17', 0, 'user3@test.com',	'Victor', 'Barnes')
)
AS src
(
	AccessFailedCount,
	ConcurrencyStamp,
	Email,
	EmailConfirmed,
	LockoutEnabled,
	LockoutEnd,
	NormalizedEmail,
	NormalizedUserName,
	PasswordHash,
	PhoneNumber,
	PhoneNumberConfirmed,
	SecurityStamp,
	TwoFactorEnabled,
	UserName,
	FirstName,
	LastName
)
ON dest.NormalizedEmail = src.NormalizedEmail
WHEN NOT MATCHED THEN
	INSERT
	(
		AccessFailedCount,
		ConcurrencyStamp,
		Email,
		EmailConfirmed,
		LockoutEnabled,
		LockoutEnd,
		NormalizedEmail,
		NormalizedUserName,
		PasswordHash,
		PhoneNumber,
		PhoneNumberConfirmed,
		SecurityStamp,
		TwoFactorEnabled,
		UserName,
		FirstName,
		LastName
	)
	VALUES
	(
		src.AccessFailedCount,
		src.ConcurrencyStamp,
		src.Email,
		src.EmailConfirmed,
		src.LockoutEnabled,
		src.LockoutEnd,
		src.NormalizedEmail,
		src.NormalizedUserName,
		src.PasswordHash,
		src.PhoneNumber,
		src.PhoneNumberConfirmed,
		src.SecurityStamp,
		src.TwoFactorEnabled,
		src.UserName,
		src.FirstName,
		src.LastName
	);

DECLARE @adminRoleId int = (select top 1 Id from dbo.AspNetRole where NormalizedName = 'ADMIN')
DECLARE @userRoleId int = (select top 1 Id from dbo.AspNetRole where NormalizedName = 'USER')

MERGE dbo.AspNetUserRole as dest
USING
(
	VALUES
	((SELECT TOP 1 Id FROM dbo.AspNetUser where NormalizedUsername = 'ADMIN@TEST.COM'), @adminRoleId),
	((SELECT TOP 1 Id FROM dbo.AspNetUser where NormalizedUsername = 'USER1@TEST.COM'), @userRoleId),
	((SELECT TOP 1 Id FROM dbo.AspNetUser where NormalizedUsername = 'USER2@TEST.COM'), @userRoleId),
	((SELECT TOP 1 Id FROM dbo.AspNetUser where NormalizedUsername = 'USER3@TEST.COM'), @userRoleId)
)
AS src (AspNetUserId, AspNetRoleId)
ON dest.AspNetUserId = src.AspNetUserId
WHEN NOT MATCHED THEN
	INSERT (AspNetUserId, AspNetRoleId)
	VALUES (src.AspNetUserId, AspNetRoleId);