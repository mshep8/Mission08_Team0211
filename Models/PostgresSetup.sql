DROP TABLE IF EXISTS "ToDoTasks";
DROP TABLE IF EXISTS "Categories";

CREATE TABLE "Categories" (
    "CategoryId" INTEGER PRIMARY KEY,
    "CategoryName" VARCHAR(256) NOT NULL UNIQUE
);

CREATE TABLE "ToDoTasks" (
    "ToDoTaskId" INTEGER PRIMARY KEY,
    "Task" VARCHAR(256) NOT NULL,
    "DueDate" DATE NULL,
    "Quadrant" INTEGER NOT NULL CHECK ("Quadrant" BETWEEN 1 AND 4),
    "CategoryId" INTEGER NOT NULL,
    "Completed" BOOLEAN NOT NULL DEFAULT FALSE,
    CONSTRAINT "FK_ToDoTasks_Categories_CategoryId"
        FOREIGN KEY ("CategoryId") REFERENCES "Categories"("CategoryId")
);

INSERT INTO "Categories" ("CategoryId", "CategoryName") VALUES
    (1, 'Home'),
    (2, 'School'),
    (3, 'Work'),
    (4, 'Church');

INSERT INTO "ToDoTasks" ("ToDoTaskId", "Task", "DueDate", "Quadrant", "CategoryId", "Completed") VALUES
    (1, 'Finish Mission 08 setup', '2026-02-01', 1, 2, FALSE),
    (2, 'Plan weekly schedule', '2026-02-07', 2, 1, FALSE);
