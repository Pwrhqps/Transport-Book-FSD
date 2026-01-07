
ALTER TABLE [dbo].[Passengers]
DROP CONSTRAINT [PK_Passengers];
ALTER TABLE [dbo].[Passengers]
ALTER COLUMN [UserId] NVARCHAR(120) NOT NULL;
ALTER TABLE [dbo].[Passengers]
ADD CONSTRAINT [PK_Passengers] PRIMARY KEY CLUSTERED ([UserId]);

ALTER TABLE [dbo].[DriverProfiles]
DROP CONSTRAINT [PK_DriverProfiles];
ALTER TABLE [dbo].[DriverProfiles]
ALTER COLUMN [UserId] NVARCHAR(120) NOT NULL;
ALTER TABLE [dbo].[DriverProfiles]
ADD CONSTRAINT [PK_DriverProfiles] PRIMARY KEY CLUSTERED ([UserId]);

ALTER TABLE [dbo].[Bookings]
ALTER COLUMN [PassengerId] NVARCHAR(120) NOT NULL;
ALTER TABLE [dbo].[Bookings] WITH NOCHECK
ADD CONSTRAINT [FK_PassengerID] FOREIGN KEY ([PassengerId]) REFERENCES [dbo].[Passengers] ([UserId])
ALTER TABLE [dbo].[Bookings]
ALTER COLUMN [DriverId] NVARCHAR(120) NOT NULL;
ALTER TABLE [dbo].[Bookings] WITH NOCHECK
ADD CONSTRAINT [FK_DriverId] FOREIGN KEY ([DriverId]) REFERENCES [dbo].[DriverProfiles] ([UserId])

ALTER TABLE [dbo].[Passengers]
ALTER COLUMN [UserId] NVARCHAR(MAX) NOT NULL;
ALTER TABLE [dbo].[DriverProfiles]
ALTER COLUMN [UserId] NVARCHAR(MAX) NOT NULL;
ALTER TABLE [dbo].[Bookings]
ALTER COLUMN [PassengerId] NVARCHAR(MAX) NOT NULL;
ALTER TABLE [dbo].[Bookings]
ALTER COLUMN [DriverId] NVARCHAR(MAX) NOT NULL;


ALTER TABLE [dbo].[Bookings]
ALTER COLUMN [PassengerId] int;
ALTER TABLE [dbo].[Bookings]
ALTER COLUMN [DriverId] int;
ALTER TABLE [dbo].[Bookings]
ADD CONSTRAINT [FK_Bookings_Passengers_PassengerId] FOREIGN KEY ([PassengerId]) REFERENCES [dbo].[Passengers] ([PassengerId]) ON DELETE CASCADE;

UPDATE f
SET f.DriverId = b.DriverId,
    f.PassengerId = b.PassengerId
FROM Feedbacks f
JOIN Bookings b ON f.BookingId = b.BookingId
WHERE f.DriverId IS NULL;