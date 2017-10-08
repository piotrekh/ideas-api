DECLARE @projectsId int = (SELECT TOP 1 Id from dbo.IdeaCategory WHERE Name = 'Projects')
DECLARE @socialEventsId int = (SELECT TOP 1 Id from dbo.IdeaCategory WHERE Name = 'Social events')
DECLARE @officeEquipmentId int = (SELECT TOP 1 Id from dbo.IdeaCategory WHERE Name = 'Office equipment')
DECLARE @workCultureId int = (SELECT TOP 1 Id from dbo.IdeaCategory WHERE Name = 'Work culture')

MERGE dbo.IdeaSubcategory AS dest
USING
(
	VALUES
	(@projectsId, 'mobile', GETUTCDATE()),
	(@projectsId, 'web', GETUTCDATE()),
	(@socialEventsId, 'team building', GETUTCDATE()),
	(@socialEventsId, 'community', GETUTCDATE()),
	(@officeEquipmentId, 'kitchen', GETUTCDATE()),
	(@officeEquipmentId, 'furniture', GETUTCDATE()),
	(@workCultureId, 'workplace', GETUTCDATE()),
	(@workCultureId, 'processes', GETUTCDATE())
)
as src
(
	IdeaCategoryId,
	Name,
	CreatedDate
)
ON dest.IdeaCategoryId = src.IdeaCategoryId AND dest.Name = src.Name
WHEN NOT MATCHED THEN
	INSERT
	(
		IdeaCategoryId,
		Name,
		CreatedDate
	)
	VALUES
	(
		src.IdeaCategoryId,
		src.Name,
		src.CreatedDate
	)
;