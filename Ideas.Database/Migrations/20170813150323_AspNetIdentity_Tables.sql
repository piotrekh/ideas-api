CREATE TABLE [dbo].[AspNetRole] (
    [Id] int NOT NULL IDENTITY,
    [ConcurrencyStamp] nvarchar(max),
    [Name] nvarchar(256),
    [NormalizedName] nvarchar(256),
    CONSTRAINT [PK_AspNetRole] PRIMARY KEY ([Id])
);

CREATE TABLE [dbo].[AspNetUserToken] (
    [AspNetUserId] int NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max),
    CONSTRAINT [PK_AspNetUserToken] PRIMARY KEY ([AspNetUserId], [LoginProvider], [Name])
);

CREATE TABLE [dbo].[AspNetUser] (
    [Id] int NOT NULL IDENTITY,
    [AccessFailedCount] int NOT NULL,
    [ConcurrencyStamp] nvarchar(max),
    [Email] nvarchar(256),
    [EmailConfirmed] bit NOT NULL,
    [LockoutEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset,
    [NormalizedEmail] nvarchar(256),
    [NormalizedUserName] nvarchar(256),
    [PasswordHash] nvarchar(max),
    [PhoneNumber] nvarchar(max),
    [PhoneNumberConfirmed] bit NOT NULL,
    [SecurityStamp] nvarchar(max),
    [TwoFactorEnabled] bit NOT NULL,
    [UserName] nvarchar(256),
	[FirstName] nvarchar(256) NOT NULL,
	[LastName] nvarchar(256) NOT NULL,
    CONSTRAINT [PK_AspNetUser] PRIMARY KEY ([Id])
);

CREATE TABLE [dbo].[AspNetRoleClaim] (
    [Id] int NOT NULL IDENTITY,
    [ClaimType] nvarchar(max),
    [ClaimValue] nvarchar(max),
    [AspNetRoleId] int NOT NULL,
    CONSTRAINT [PK_AspNetRoleClaim] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaim_AspNetRole_AspNetRoleId] FOREIGN KEY ([AspNetRoleId]) REFERENCES [dbo].[AspNetRole] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[AspNetUserClaim] (
    [Id] int NOT NULL IDENTITY,
    [ClaimType] nvarchar(max),
    [ClaimValue] nvarchar(max),
    [AspNetUserId] int NOT NULL,
    CONSTRAINT [PK_AspNetUserClaim] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaim_AspNetUser_AspNetUserId] FOREIGN KEY ([AspNetUserId]) REFERENCES [dbo].[AspNetUser] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[AspNetUserLogin] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max),
    [AspNetUserId] int NOT NULL,
    CONSTRAINT [PK_AspNetUserLogin] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogin_AspNetUser_AspNetUserId] FOREIGN KEY ([AspNetUserId]) REFERENCES [dbo].[AspNetUser] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[AspNetUserRole] (
    [AspNetUserId] int NOT NULL,
    [AspNetRoleId] int NOT NULL,
    CONSTRAINT [PK_AspNetUserRole] PRIMARY KEY ([AspNetUserId], [AspNetRoleId]),
    CONSTRAINT [FK_AspNetUserRole_AspNetRole_AspNetRoleId] FOREIGN KEY ([AspNetRoleId]) REFERENCES [dbo].[AspNetRole] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRole_AspNetUser_AspNetUserId] FOREIGN KEY ([AspNetUserId]) REFERENCES [dbo].[AspNetUser] ([Id]) ON DELETE CASCADE
);