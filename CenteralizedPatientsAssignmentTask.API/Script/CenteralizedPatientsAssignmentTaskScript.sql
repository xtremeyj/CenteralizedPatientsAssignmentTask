-- 1. Create the database
CREATE DATABASE CenteralizedPatientsAssignmentTaskDB;
GO

USE CenteralizedPatientsAssignmentTaskDB
;
GO

-- 2. Create Patients table
CREATE TABLE Patients (
    PatientId UNIQUEIDENTIFIER PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Age INT NOT NULL,
    Gender NVARCHAR(10),
    ContactNo NVARCHAR(20),
    Email NVARCHAR(100),
    HospitalName NVARCHAR(100),
    Diagnosis NVARCHAR(100),
    Status NVARCHAR(20) NOT NULL DEFAULT 'Active', -- Default set here
    CreatedDate DATETIME,
    IsDeleted BIT NOT NULL DEFAULT 0
);
GO

-- 3. Create stored procedure: sp_GetPatients (with filtering, sorting, paging)
CREATE OR ALTER PROCEDURE sp_GetPatients
    @PageNumber INT = 1,
    @PageSize INT = 10,
    @Search NVARCHAR(100) = NULL,
    @SortBy NVARCHAR(50) = NULL,
    @SortDirection NVARCHAR(4) = 'ASC',
    @HospitalName NVARCHAR(100) = NULL,
    @Status NVARCHAR(50) = NULL,
    @Gender NVARCHAR(10) = NULL
AS
BEGIN

     SET NOCOUNT ON;

     DECLARE @Offset INT = (@PageNumber - 1) * @PageSize;

    -- Temp table to hold filtered results
    WITH Filtered AS (
        SELECT *
        FROM Patients
        WHERE IsDeleted = 0 AND
            (@HospitalName IS NULL OR HospitalName = @HospitalName)
            AND (@Status IS NULL OR Status = @Status)
            AND (@Gender IS NULL OR Gender = @Gender)
            AND (
                @Search IS NULL OR 
                Name LIKE '%' + @Search + '%' OR 
                Email LIKE '%' + @Search + '%' OR 
                ContactNo LIKE '%' + @Search + '%'
            )
    )
    SELECT 
        (SELECT COUNT(*) FROM Filtered) AS TotalCount,
        *
    FROM Filtered
    ORDER BY
        CASE 
            WHEN @SortBy = 'Name' AND @SortDirection = 'ASC' THEN Name
        END ASC,
        CASE 
            WHEN @SortBy = 'Name' AND @SortDirection = 'DESC' THEN Name
        END DESC,
        CASE 
            WHEN @SortBy = 'CreatedDate' AND @SortDirection = 'ASC' THEN CreatedDate
        END ASC,
        CASE 
            WHEN @SortBy = 'CreatedDate' AND @SortDirection = 'DESC' THEN CreatedDate
        END DESC
    OFFSET @Offset ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END;
GO

-- 4. Create stored procedure: sp_GetAnalyticsData
CREATE OR ALTER PROCEDURE sp_GetAnalyticsData
AS
BEGIN
    SELECT
        HospitalName,
        COUNT(*) AS PatientCount
    FROM Patients
    WHERE IsDeleted = 0
    GROUP BY HospitalName;
END;
GO

-- 5. Seed 2000 dummy patients
DECLARE @i INT = 1;
DECLARE @hospitals TABLE (Name NVARCHAR(100));
INSERT INTO @hospitals (Name)
VALUES ('Hospital A'), ('Hospital B'), ('Hospital C'), ('Hospital D'),
       ('Hospital E'), ('Hospital F'), ('Hospital G'), ('Hospital H'),
       ('Hospital I'), ('Hospital J');

WHILE @i <= 2000
BEGIN
    INSERT INTO Patients (
        PatientId,
        Name,
        Age,
        Gender,
        ContactNo,
        Email,
        HospitalName,
        Diagnosis,
        Status,
        CreatedDate,
        IsDeleted
    )
    SELECT
        NEWID(),
        CONCAT('Patient ', @i),
        ABS(CHECKSUM(NEWID())) % 100 + 1,
        CASE WHEN ABS(CHECKSUM(NEWID())) % 2 = 0 THEN 'Male' ELSE 'Female' END,
        RIGHT('9' + CAST(ABS(CHECKSUM(NEWID())) % 1000000000 AS VARCHAR(9)), 10),
        CONCAT('patient', @i, '@mail.com'),
        (SELECT TOP 1 Name FROM @hospitals ORDER BY NEWID()),
        'General Diagnosis',
        CASE FLOOR(RAND(CHECKSUM(NEWID())) * 3) + 1 
            WHEN 1 THEN 'Active' WHEN 2 THEN 'Discharged' WHEN 3 THEN 'Deceased'   ELSE 'Active' -- default fallback 
        END,
        DATEADD(DAY, -ABS(CHECKSUM(NEWID())) % 1000, GETDATE()),
        0; -- IsDeleted default false

    SET @i += 1;
END;
GO

-- 6. Verify record count
SELECT COUNT(*) AS TotalPatients FROM Patients WHERE IsDeleted = 0;
GO
