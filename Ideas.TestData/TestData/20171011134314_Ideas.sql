--categories
DECLARE @categoryProjectsId int = (SELECT TOP 1 Id from dbo.IdeaCategory WHERE Name = 'Projects')
DECLARE @categorySocialEventsId int = (SELECT TOP 1 Id from dbo.IdeaCategory WHERE Name = 'Social events')
DECLARE @categoryOfficeEquipmentId int = (SELECT TOP 1 Id from dbo.IdeaCategory WHERE Name = 'Office equipment')
DECLARE @categoryWorkCultureId int = (SELECT TOP 1 Id from dbo.IdeaCategory WHERE Name = 'Work culture')

--subcategories
DECLARE @subcategoryMobileId int = (SELECT TOP 1 Id from dbo.IdeaSubcategory WHERE Name = 'mobile' and IdeaCategoryId = @categoryProjectsId)
DECLARE @subcategoryWebId int = (SELECT TOP 1 Id from dbo.IdeaSubcategory WHERE Name = 'web' and IdeaCategoryId = @categoryProjectsId)
DECLARE @subcategoryTeamBuildingId int = (SELECT TOP 1 Id from dbo.IdeaSubcategory WHERE Name = 'team building' and IdeaCategoryId = @categorySocialEventsId)
DECLARE @subcategoryCommunityId int = (SELECT TOP 1 Id from dbo.IdeaSubcategory WHERE Name = 'community' and IdeaCategoryId = @categorySocialEventsId)
DECLARE @subcategoryKitchenId int = (SELECT TOP 1 Id from dbo.IdeaSubcategory WHERE Name = 'kitchen' and IdeaCategoryId = @categoryOfficeEquipmentId)
DECLARE @subcategoryFurnitureId int = (SELECT TOP 1 Id from dbo.IdeaSubcategory WHERE Name = 'furniture' and IdeaCategoryId = @categoryOfficeEquipmentId)
DECLARE @subcategoryWorkplaceId int = (SELECT TOP 1 Id from dbo.IdeaSubcategory WHERE Name = 'workplace' and IdeaCategoryId = @categoryWorkCultureId)
DECLARE @subcategoryProcessesId int = (SELECT TOP 1 Id from dbo.IdeaSubcategory WHERE Name = 'processes' and IdeaCategoryId = @categoryWorkCultureId)

--users
DECLARE @adminId int = (SELECT TOP 1 Id from dbo.AspNetUser WHERE Email = 'admin@test.com')
DECLARE @user1Id int = (SELECT TOP 1 Id from dbo.AspNetUser WHERE Email = 'user1@test.com')
DECLARE @user2Id int = (SELECT TOP 1 Id from dbo.AspNetUser WHERE Email = 'user2@test.com')
DECLARE @user3Id int = (SELECT TOP 1 Id from dbo.AspNetUser WHERE Email = 'user3@test.com')

--ideas
MERGE dbo.Idea AS dest
USING
(
	VALUES
	(@adminId, 'Ideas mobile app', 'Create a mobile application that will allow to access Ideas from a smartphone', '2017-10-01 12:25', @categoryProjectsId),
	(@user1Id, 'Scheduler for conference rooms', 'A web application that will allow to schedule meetings in conference rooms.', '2017-10-02 14:42', @categoryProjectsId),
	(@user2Id, 'Outdoor activities', 'We can plan some outdoor activities that will encourage the team to integrate and improve their cooperation skills.', '2017-10-03 09:11', @categorySocialEventsId),
	(@user3Id, 'Monthly hackathons', 'Create a mobile application that will allow to access Ideas from a smartphone', '2017-10-04 16:05', @categorySocialEventsId),
	(@adminId, 'Bigger table', 'Considering the company growth and hiring more staff, I think we need a bigger table in the kitchen to comfortably eat lunch.', '2017-10-05 10:52', @categoryOfficeEquipmentId),
	(@user1Id, 'Lockers', 'It would be nice to have a locker beside the desk to store your personal belongings or important documents more securely.', '2017-10-06 10:29', @categoryOfficeEquipmentId),
	(@user2Id, 'Designate one room for quiet work', 'We should designate one room to be a place for quiet working, when you need to focus on important stuff and cannot let anything to distract you from it.', '2017-10-07 12:30', @categoryWorkCultureId),
	(@user3Id, 'Coding policy', 'We should create a document which would describe the coding standards (rules, best practises etc.) in our company.', '2017-10-08 13:15', @categoryWorkCultureId)
)
AS src
(
	AspNetUserId,
	Title,
	Description,
	CreatedDate,
	IdeaCategoryId
)
ON src.Title = dest.Title AND src.IdeaCategoryId = dest.IdeaCategoryId
WHEN NOT MATCHED THEN
	INSERT
	(
		AspNetUserId,
		Title,
		Description,
		CreatedDate,
		IdeaCategoryId
	)
	VALUES
	(
		src.AspNetUserId,
		src.Title,
		src.Description,
		src.CreatedDate,
		src.IdeaCategoryId
	)
;

--assigned ideas subcategories
MERGE dbo.AssignedIdeaSubcategory AS dest
USING
(
	VALUES
	((SELECT TOP 1 Id FROM dbo.Idea WHERE Title = 'Ideas mobile app'), @subcategoryMobileId),
	((SELECT TOP 1 Id FROM dbo.Idea WHERE Title = 'Scheduler for conference rooms'), @subcategoryWebId),
	((SELECT TOP 1 Id FROM dbo.Idea WHERE Title = 'Outdoor activities'), @subcategoryTeamBuildingId),
	((SELECT TOP 1 Id FROM dbo.Idea WHERE Title = 'Monthly hackathons'), @subcategoryCommunityId),
	((SELECT TOP 1 Id FROM dbo.Idea WHERE Title = 'Bigger table'), @subcategoryKitchenId),
	((SELECT TOP 1 Id FROM dbo.Idea WHERE Title = 'Lockers'), @subcategoryFurnitureId),
	((SELECT TOP 1 Id FROM dbo.Idea WHERE Title = 'Designate one room for quiet work'), @subcategoryWorkplaceId),
	((SELECT TOP 1 Id FROM dbo.Idea WHERE Title = 'Coding policy'), @subcategoryProcessesId)
)
AS src
(
	IdeaId,
	IdeaSubcategoryId
)
ON src.IdeaId = dest.IdeaId AND src.IdeaSubcategoryId = dest.IdeaSubcategoryId
WHEN NOT MATCHED THEN
	INSERT
	(
		IdeaId,
		IdeaSubcategoryId
	)
	VALUES
	(
		src.IdeaId,
		src.IdeaSubcategoryId
	)
;