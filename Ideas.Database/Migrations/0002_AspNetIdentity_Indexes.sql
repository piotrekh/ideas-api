
CREATE UNIQUE INDEX [RoleNameIndex] ON [dbo].[AspNetRole] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

GO

CREATE INDEX [IX_AspNetRoleClaim_AspNetRoleId] ON [dbo].[AspNetRoleClaim] ([AspNetRoleId]);

GO

CREATE INDEX [IX_AspNetUserClaim_AspNetUserId] ON [dbo].[AspNetUserClaim] ([AspNetUserId]);

GO

CREATE INDEX [IX_AspNetUserLogin_AspNetUserId] ON [dbo].[AspNetUserLogin] ([AspNetUserId]);

GO

CREATE INDEX [IX_AspNetUserRole_AspNetRoleId] ON [dbo].[AspNetUserRole] ([AspNetRoleId]);

GO

CREATE INDEX [EmailIndex] ON [dbo].[AspNetUser] ([NormalizedEmail]);

GO

CREATE UNIQUE INDEX [UserNameIndex] ON [dbo].[AspNetUser] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

GO