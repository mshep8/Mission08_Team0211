PRAGMA foreign_keys = ON;

DROP TABLE IF EXISTS ToDoTasks;
DROP TABLE IF EXISTS Categories;

CREATE TABLE Categories (
    CategoryId INTEGER PRIMARY KEY,
    CategoryName TEXT NOT NULL UNIQUE
);

CREATE TABLE ToDoTasks (
    ToDoTaskId INTEGER PRIMARY KEY,
    Task TEXT NOT NULL,
    DueDate TEXT NULL,
    Quadrant INTEGER NOT NULL CHECK (Quadrant BETWEEN 1 AND 4),
    CategoryId INTEGER NOT NULL,
    Completed INTEGER NOT NULL DEFAULT 0,
    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId)
);

INSERT INTO Categories (CategoryId, CategoryName) VALUES
    (1, 'Home'),
    (2, 'School'),
    (3, 'Work'),
    (4, 'Church');

INSERT INTO ToDoTasks (ToDoTaskId, Task, DueDate, Quadrant, CategoryId, Completed) VALUES
    (1, 'Finish Mission 08 setup', '2026-02-01', 1, 2, 0),
    (2, 'Plan weekly schedule', '2026-02-07', 2, 1, 0);
