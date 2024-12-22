CREATE TABLE Tasks (
    Id INT NOT NULL AUTO_INCREMENT,
    Title VARCHAR(255) NOT NULL,
    Description TEXT NOT NULL,
    Category ENUM('Daily', 'Weekly', 'Monthly') NOT NULL,
    DueDate DATETIME NOT NULL,
    IsCompleted TINYINT(1) NOT NULL,
    UserId INT NOT NULL,
    PRIMARY KEY (Id),
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE Users (
    Id INT NOT NULL AUTO_INCREMENT,
    Username VARCHAR(255) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL,
    Role ENUM('Admin', 'User') NOT NULL DEFAULT 'User',
    PRIMARY KEY (Id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;




INSERT INTO Tasks (Id, Title, Description, Category, DueDate, IsCompleted, UserId)
VALUES
    (18, 'Admin Görevi', 'Bu, admin kullanıcısı için bir test görevidir.', 'Daily', '2024-12-31 12:00:00', 0, 5),
    (19, 'System Maintenance', 'Perform routine server maintenance and apply security updates.', 'Weekly', '2024-12-29 10:00:00', 0, 5),
    (20, 'Weekly Report Submission', 'Prepare and submit the weekly performance report to the manager.', 'Weekly', '2024-12-29 17:00:00', 0, 5),
    (21, 'Monthly Team Meeting', 'Attend the monthly team meeting and present the progress report.', 'Monthly', '2024-12-31 10:00:00', 0, 5),
    (22, 'Monthly Team Meeting', 'Attend the monthly team meeting and present the progress report.', 'Monthly', '2024-12-31 10:00:00', 0, 5),
    (24, 'Prepare Presentation', 'Prepare a presentation for the weekly team meeting.', 'Weekly', '2024-12-25 15:00:00', 0, 4),
    (25, 'Complete Documentation', 'Finalize the project documentation and submit it to the manager.', 'Monthly', '2024-12-31 17:00:00', 0, 4),
    (26, 'Code Review', 'Review the pull requests for the new feature implementation.', 'Weekly', '2024-12-29 14:00:00', 0, 6);



INSERT INTO Users (Id, Username, Password, Role)
VALUES 
   INSERT INTO Users (Id, Username, Password, Role)
VALUES
    (4, 'omer', 'D/4avRoIIVNTwjPW4AlhPpXuxCU4Mqdhryj/N6xaFQw=', 'User'),
    (5, 'admin', 'A6xnQhbz4Vx2HuGl4lXwZ5U2I8iziLRFnhP5eNfIRvQ=', 'Admin'),
    (6, 'nergis', 'RifgDdxJ5VsiuoQgvm3QuUYHvsfEzN2dION+JS/vp3Y=', 'User');

