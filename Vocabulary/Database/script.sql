

CREATE TABLE WordType
(
	WordTypeId				INT NOT NULL AUTO_INCREMENT,
	WordTypeName			VARCHAR(255) NOT NULL,

	FULLTEXT				(WordTypeName),
	CONSTRAINT PK_WordType PRIMARY KEY (WordTypeId)
);

CREATE TABLE BookType
(
	BookTypeId				INT NOT NULL AUTO_INCREMENT,
	BookTypeName			VARCHAR(255) NOT NULL,

	FULLTEXT				(BookTypeName),
	CONSTRAINT PK_BookType PRIMARY KEY (BookTypeId)
);

CREATE TABLE BookLanguage
(
	BookLanguageId				INT NOT NULL AUTO_INCREMENT,
	BookLanguageName			VARCHAR(255) NOT NULL,

	FULLTEXT				(BookLanguageName),
	CONSTRAINT PK_BookType PRIMARY KEY (BookLanguageId)
);

CREATE TABLE Book
(
	BookId					INT NOT NULL AUTO_INCREMENT,
	BookName				VARCHAR(255) NOT NULL,
	BookTypeId				INT NOT NULL,
	BookLanguageId			INT NOT NULL,

	FULLTEXT				(BookName),
	CONSTRAINT PK_Book PRIMARY KEY (BookId),

	CONSTRAINT FK_Book_BookType
		FOREIGN KEY (BookTypeId)
		REFERENCES Vocabulary.BookType (BookTypeId),

	CONSTRAINT FK_Book_BookLanguage
		FOREIGN KEY (BookLanguageId)
		REFERENCES Vocabulary.BookLanguage (BookLanguageId)
);

CREATE TABLE Unit
(
	UnitId					INT NOT NULL AUTO_INCREMENT,
	UnitName				VARCHAR(255) NOT NULL,
	SubUnitName				VARCHAR(255) NOT NULL,
	BookId					INT NOT NULL,

	FULLTEXT				(UnitName),
	CONSTRAINT PK_Unit PRIMARY KEY (UnitId),

	CONSTRAINT FK_Unit_Book
		FOREIGN KEY (BookId)
		REFERENCES Vocabulary.Book (BookId)
);

CREATE TABLE Word
(
	WordId					INT NOT NULL AUTO_INCREMENT,
	WordName				VARCHAR(255) NOT NULL,
	UnitId					INT NOT NULL,

	CONSTRAINT PK_Word PRIMARY KEY (WordId),

	CONSTRAINT FK_Word_Unit
		FOREIGN KEY (UnitId)
		REFERENCES Vocabulary.Unit (UnitId)
);

CREATE TABLE Definition
(
	DefinitionId			INT NOT NULL AUTO_INCREMENT,
	DefinitionText			VARCHAR(255) NOT NULL,
	Example					VARCHAR(255) NOT NULL,

	WordId					INT NOT NULL,
	WordTypeId				INT NOT NULL,

	CONSTRAINT PK_Definition PRIMARY KEY (DefinitionId),

	CONSTRAINT FK_Definition_Word
		FOREIGN KEY (WordId)
		REFERENCES Vocabulary.Word (WordId),

	CONSTRAINT FK_Definition_WordType
		FOREIGN KEY (WordId)
		REFERENCES Vocabulary.WordType (WordTypeId)
);

CREATE TABLE Word_Unit
(
	WordId					INT NOT NULL,
	UnitId					INT NOT NULL,

	CONSTRAINT FK_Word_Unit_Word
		FOREIGN KEY (WordId)
		REFERENCES Vocabulary.Word (WordId),

	CONSTRAINT FK_Word_Unit_Unit
		FOREIGN KEY (UnitId)
		REFERENCES Vocabulary.Unit (UnitId)
);

INSERT INTO WordType (WordTypeName)
VALUES ('Verb'), ('Noun'), ('Adverb'), ('Adjective'), ('Phrasal Verb'), ('Prepositions');

INSERT INTO BookType (BookTypeName)
VALUES ('Student''s Book'), ('Workbook'), ('Grammarbook'), ('Dictionary');

INSERT INTO BookLanguage (BookLanguageName)
VALUES ('EN'), ('NL');

INSERT INTO Book (BookName, BookTypeId, BookLanguageId)
VALUES ('Upstream B1+', 1, 1), ('Upstream B1', 2, 1);
