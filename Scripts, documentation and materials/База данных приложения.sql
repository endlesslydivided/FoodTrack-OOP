use master;

CREATE DATABASE DietManager;
use DietManager;

CREATE TABLE Users
(
Id int constraint PK_USERS primary key (Id) identity(1,1),
IsAdmin bit NOT NULL default '0',
UserLogin nvarchar(25) NOT NULL UNIQUE,
UserPassword varbinary(20)  NOT NULL UNIQUE,
Salt nvarchar(50) NOT NULL
)

CREATE TABLE UsersParams
(
Id int constraint PK_USERS_PARAMS primary key(Id) identity(1,1),
IdParams int NOT NULL  constraint FK_USERS_PARAMS_USERS foreign key (IdParams) references Users(Id),
ParamsDate datetime NOT NULL,
UserWeight decimal(4,1) default '0' NOT NULL,
UserHeight int default '0' NOT NULL
)

CREATE TABLE UsersData
(
Id int  constraint PK_USERS_DATA primary key(Id) identity(1,1),
IdData int NOT NULL constraint FK_USERS_DATA_USERS foreign key (IdData) references Users(Id),
FullName nvarchar(300) NOT NULL,
Birthday date  NOT NULL
)

CREATE TABLE FoodCategories
(
Id int constraint PK_FCATEGORIES primary key (Id) identity(1,1),
CategoryName nvarchar(50) NOT NULL UNIQUE
)

CREATE TABLE Products
(
Id int constraint PK_PRODUCTS primary key(Id) identity(1,1), 
IdAdded int NOT NULL  constraint FK_PRODUCTS_USERS foreign key(IdAdded) references Users(Id),
ProductName varchar(200) NOT NULL UNIQUE,
CaloriesGram decimal(7,2) NOT NULL default '0',
ProteinsGram decimal(7,2) NOT NULL default '0',
FatsGram decimal(7,2) NOT NULL default '0',
CarbohydratesGram decimal(7,2) NOT NULL default '0',
FoodCategory nvarchar(50) constraint FK_PRODUCTS_FCATEGORY foreign key (FoodCategory) references FoodCategories(CategoryName)
)

CREATE TABLE Reports
(
Id int  constraint PK_REPORTS primary key(Id) identity(1,1),
IdReport int NOT NULL constraint FK_REPORTS_USERS foreign key (IdReport) references Users(Id) ,
ProductName varchar(200) NOT NULL constraint FK_REPORTS_PRODUCTS foreign key (ProductName) references Products(ProductName),
ReportDate datetime NOT NULL,
EatPeriod varchar(8) NOT NULL,
DayGram decimal(7,2) NOT NULL default '0',
DayCalories decimal(7,2) NOT NULL default '0',
DayProteins decimal(7,2) NOT NULL default '0',
DayFats decimal(7,2) NOT NULL default '0',
DayCarbohydrates decimal(7,2) NOT NULL default '0',
MostCategory nvarchar(50) constraint FK_REPORTS_FCATEGORY foreign key (MostCategory) references FoodCategories(CategoryName)
)

