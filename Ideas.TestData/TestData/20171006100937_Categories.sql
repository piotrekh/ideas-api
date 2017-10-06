MERGE dbo.IdeaCategory AS dest
USING
(
	VALUES
	('Projects', GETUTCDATE()),
	('Social events', GETUTCDATE()),
	('Office equipment', GETUTCDATE()),
	('Work culture', GETUTCDATE())
)
as src
(
	Name,
	CreatedDate
)
ON dest.Name = src.Name
WHEN NOT MATCHED THEN
	INSERT
	(
		Name,
		CreatedDate
	)
	VALUES
	(
		src.Name,
		src.CreatedDate
	)
;