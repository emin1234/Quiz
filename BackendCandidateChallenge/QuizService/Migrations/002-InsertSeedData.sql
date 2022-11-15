--I have changed the logic in Question and Answer tables. Now, table Answer contains field IsCorrectAnswer(instead of table Question which had field CorrectAnswerId).
INSERT INTO [Quiz] (Title) VALUES ('My first quiz');
INSERT INTO [Quiz] (Title) VALUES ('My second quiz');
INSERT INTO [Question] (Text, QuizId) VALUES ('My first question', 1);
INSERT INTO [Answer] (Text, QuestionId, IsCorrectAnswer) VALUES ('My first answer to first q', 1, 1);
INSERT INTO [Answer] (Text, QuestionId, IsCorrectAnswer) VALUES ('My second answer to first q', 1, 0);
INSERT INTO [Question] (Text, QuizId) VALUES ('My second question', 1);
INSERT INTO [Answer] (Text, QuestionId, IsCorrectAnswer) VALUES ('My first answer to second q', 2, 0);
INSERT INTO [Answer] (Text, QuestionId, IsCorrectAnswer) VALUES ('My second answer to second q', 2, 0);
INSERT INTO [Answer] (Text, QuestionId, IsCorrectAnswer) VALUES ('My third answer to second q', 2, 1);


