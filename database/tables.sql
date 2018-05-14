

CCREATE TABLE Casino (
    CasinoID        INT                 IDENTITY(1,1) PRIMARY KEY,
    Name            NVARCHAR(100)       NOT NULL,
    ClubName        NVARCHAR(100)       NULL,
    Theme           NVARCHAR(100)       NULL,
    Logo            NVARCHAR(100)       NULL,
    Location        NVARCHAR(4000)      Default '{}' ,
    Settings        NVARCHAR(4000)      Default '{}' 
)
GO

CREATE TABLE ClubLevel (
    ClubLevelID     INT                 IDENTITY(1,1) PRIMARY KEY,
    CasinoID        INT                 NOT NULL REFERENCES Casino (CasinoID),
    Name            NVARCHAR(100)       NOT NULL,
    Sort            INT                 DEFAULT 0
)
GO

CREATE TABLE Tier (
    TierID          INT                 IDENTITY(1,1) PRIMARY KEY,
    CasinoID        INT                 NOT NULL REFERENCES Casino (CasinoID),
    Name            NVARCHAR(100)       NOT NULL,
    Sort            INT                 DEFAULT 0
)
GO

CREATE TABLE Player (
    PlayerID        INT                 IDENTITY(1,1) PRIMARY KEY,
    CasinoID        INT                 NOT NULL REFERENCES Casino (CasinoID),
    TierID          INT                     NULL REFERENCES Tier (TierID),
    LastName        NVARCHAR(100)       NOT NULL,
    FirstName       NVARCHAR(100)       NOT NULL,
    Title           NVARCHAR(25),
    MemberNumber    NVARCHAR(100)       NOT NULL,
    BirthDate       DATE,
    Gender          CHAR(1),
    PreferredName   NVARCHAR(100),
    Profile         NVARCHAR(4000)  Default '{}' 
);
CREATE UNIQUE INDEX Player_MemberNumber_IDX ON Player (CasinoID, MemberNumber);
GO

CREATE TABLE Staff (
    StaffID         INT                 IDENTITY(1,1) PRIMARY KEY,
    CasinoID        INT                 NOT NULL REFERENCES Casino (CasinoID),
    LastName        NVARCHAR(100)       NOT NULL,
    FirstName       NVARCHAR(100)       NOT NULL,
    Status          NVARCHAR(25),
    Profile         NVARCHAR(4000)  Default '{}' 
)
GO

CREATE TABLE PlayerNote (
    NoteID          INT                 IDENTITY(1,1) PRIMARY KEY,
    PlayerID        INT                 NOT NULL REFERENCES Player (PlayerID),
    StaffID         INT                 NOT NULL REFERENCES Staff (StaffID),
    Date            DATE                NDEFAULT CURRENT_TIMESTAMP NOT NULL,
    Content         NVARCHAR(MAX)        
)
GO

CREATE TABLE Incentive (
    IncentiveID     INT                 IDENTITY(1,1) PRIMARY KEY,
    CasinoID        INT                 NOT NULL REFERENCES Casino (CasinoID),
    Label           NVARCHAR(100)       NOT NULL,
    Status          NVARCHAR(25),
    Details         NVARCHAR(4000)  Default '{}' 
)
GO

CREATE TABLE IncentiveTier (
    IncentiveID     INT                 NOT NULL REFERENCES Incentive (IncentiveID),
    TierID          INT                 NOT NULL REFERENCES Tier (TierID),
    StartDate       DATE                NOT NULL,
    EndDate         DATE                    NULL,
    Status          NVARCHAR(25),
    PRIMARY KEY (IncentiveID, TierID)
)
GO

CREATE TABLE PlayerRedemption (
    IncentiveID     INT                 NOT NULL REFERENCES Incentive (IncentiveID),
    PlayerID        INT                 NOT NULL REFERENCES Player (PlayerID),
    Date            Date                NOT NULL,
    Status          NVARCHAR(25),
    PRIMARY KEY (PlayerID, IncentiveID)
)
GO


CREATE TABLE Event (
    EventID         INT                 IDENTITY(1,1) PRIMARY KEY,
    CasinoID        INT                 NOT NULL REFERENCES Casino (CasinoID),
    EventName       NVARCHAR(100)       NOT NULL,
    EventType       NVARCHAR(25)        NOT NULL,
    EventDate       DATE                NULL,
    Status          NVARCHAR(25),
    Details         NVARCHAR(4000)  Default '{}' 
)
GO

CREATE TABLE EventTier (
    EventID         INT                 NOT NULL REFERENCES Event (EventID),
    TierID          INT                 NOT NULL REFERENCES Tier (TierID),
    StartDate       DATE                NOT NULL,
    EndDate         DATE                    NULL,
    Status          NVARCHAR(25),
    PRIMARY KEY (TierID, EventID)
)
GO

CREATE TABLE Beacon (
    BeaconID        INT                 IDENTITY(1,1) PRIMARY KEY,
    CasinoID        INT                 NOT NULL REFERENCES Casino (CasinoID),
    Label           NVARCHAR(100)       NOT NULL,
    Description     NVARCHAR(100),
    Location        NVARCHAR(4000)      Default '{}' 
)
GO

CREATE TABLE BeaconEncounter (
    EncounterID     INT                 IDENTITY(1,1) PRIMARY KEY,
    BeaconID        INT                 NOT NULL REFERENCES Beacon (BeaconID),
    PlayerID        INT                 NOT NULL REFERENCES Player (PlayerID),
    Timestamp       DATE                DEFAULT CURRENT_TIMESTAMP     NOT NULL,
    Distance        INT,
)
GO

CREATE TABLE Geofence (
    GeofenceID        INT                 IDENTITY(1,1) PRIMARY KEY,
    CasinoID      INT                 NOT NULL REFERENCES Casino (CasinoID),
    Label           NVARCHAR(100)       NOT NULL,
    Description     NVARCHAR(100),
    Location        NVARCHAR(4000)      Default '{}' 
)
GO

CREATE TABLE GeofenceAction (
    ActionID        INT                 IDENTITY(1,1) PRIMARY KEY,
    GeofenceID      INT                 NOT NULL REFERENCES Geofence (GeofenceID),
    PlayerID        INT                 NOT NULL REFERENCES Player (PlayerID),
    Timestamp       DATE                DEFAULT CURRENT_TIMESTAMP     NOT NULL,
    Action          NVARCHAR(25),
)
GO

