CREATE TABLE Users (
                       UserID INT AUTO_INCREMENT,
                       Username VARCHAR(255) NOT NULL UNIQUE,
                       PasswordHash VARCHAR(255) NOT NULL,
                       Role ENUM('admin', 'user', 'guest') NOT NULL,
                       PasswordResetToken VARCHAR(255),
                       PasswordResetTokenExpiry DATETIME,
                       PRIMARY KEY (UserID)
);

CREATE TABLE Links (
                       LinkID INT AUTO_INCREMENT,
                       ShortLink VARCHAR(255) NOT NULL UNIQUE,
                       OriginalLink VARCHAR(2048) NOT NULL,
                       UserID INT,
                       CreationDate DATETIME NOT NULL,
                       ClickCount INT DEFAULT 0,
                       ExpiryDate DATETIME,
                       MaxClicks INT,
                       PRIMARY KEY (LinkID),
                       FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

CREATE TABLE Clicks (
                        ClickID INT AUTO_INCREMENT,
                        LinkID INT NOT NULL,
                        ClickTime DATETIME NOT NULL,
                        SourceIP VARCHAR(45),
                        PRIMARY KEY (ClickID),
                        FOREIGN KEY (LinkID) REFERENCES Links(LinkID)
);
