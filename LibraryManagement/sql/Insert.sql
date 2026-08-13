BEGIN TRY
BEGIN TRANSACTION;

    DECLARE @CategoryId UNIQUEIDENTIFIER = NEWID();
    DECLARE @PublisherId UNIQUEIDENTIFIER = NEWID();
    DECLARE @BookId UNIQUEIDENTIFIER = NEWID();
    DECLARE @Author1Id UNIQUEIDENTIFIER = NEWID();
    DECLARE @Author2Id UNIQUEIDENTIFIER = NEWID();

    --------------------------------------------------
    -- CATEGORY
    --------------------------------------------------

INSERT INTO Categories
(
    Id,
    Name,
    Description,
    ParentCategoryId,
    CreatedAt
)
VALUES
    (
        @CategoryId,
        'Science Fiction',
        'Science fiction books',
        NULL,
        GETUTCDATE()
    );

--------------------------------------------------
-- PUBLISHER
--------------------------------------------------

INSERT INTO Publishers
(
    Id,
    Name,
    Email,
    Description,
    Website,
    Address,
    Phone,
    IsActive,
    CreatedAt
)
VALUES
    (
        @PublisherId,
        'Penguin Random House',
        'contact@penguinrandomhouse.com',
        'International publishing company',
        'https://www.penguinrandomhouse.com',
        'New York, USA',
        '+1 555 123456',
        1,
        GETUTCDATE()
    );

--------------------------------------------------
-- AUTHORS
--------------------------------------------------

INSERT INTO Authors
(
    Id,
    FirstName,
    LastNames,
    Biography,
    Nationality,
    DateOfBirth,
    DateOfDeath,
    CreatedAt
)
VALUES
    (
        @Author1Id,
        'George',
        'Orwell',
        'British novelist and essayist.',
        'British',
        '1903-06-25',
        '1950-01-21',
        GETUTCDATE()
    ),
    (
        @Author2Id,
        'Aldous',
        'Huxley',
        'English writer and philosopher.',
        'British',
        '1894-07-26',
        '1963-11-22',
        GETUTCDATE()
    );

--------------------------------------------------
-- BOOK
--------------------------------------------------

INSERT INTO Books
(
    Id,
    Title,
    Isbn,
    IsAvailable,
    Description,
    CoverImageUrl,
    CoverImageKey,
    CategoryId,
    PublisherId,
    PublicationDate,
    PagesCount,
    Language,
    CreatedAt
)
VALUES
    (
        @BookId,
        'Test Book',
        '9781234567890',
        1,
        'A test book for the library.',
        NULL,
        NULL,
        @CategoryId,
        @PublisherId,
        '2020-01-15',
        300,
        'English',
        GETUTCDATE()
    );

--------------------------------------------------
-- BOOK AUTHORS
--------------------------------------------------

INSERT INTO BookAuthors
(
    Id,
    BookId,
    AuthorId,
    Role,
    CreatedAt
)
VALUES
    (
        NEWID(),
        @BookId,
        @Author1Id,
        'Main Author',
        GETUTCDATE()
    ),
    (
        NEWID(),
        @BookId,
        @Author2Id,
        'Co-Author',
        GETUTCDATE()
    );

--------------------------------------------------
-- COMMIT
--------------------------------------------------

COMMIT TRANSACTION;

--------------------------------------------------
-- VERIFICATION
--------------------------------------------------

SELECT
    b.Id AS BookId,
    b.Title AS Book,
    c.Name AS Category,
    p.Name AS Publisher,
    a.FirstName,
    a.LastNames,
    ba.Role
FROM Books b
         INNER JOIN Categories c
                    ON b.CategoryId = c.Id
         INNER JOIN Publishers p
                    ON b.PublisherId = p.Id
         INNER JOIN BookAuthors ba
                    ON b.Id = ba.BookId
         INNER JOIN Authors a
                    ON ba.AuthorId = a.Id
WHERE b.Id = @BookId;

END TRY
BEGIN CATCH

IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    THROW;

END CATCH;