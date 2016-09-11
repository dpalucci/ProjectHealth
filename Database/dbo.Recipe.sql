CREATE TABLE [dbo].[Recipe] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [RecipeName] NVARCHAR (50) NOT NULL,
    [Calories]   FLOAT (53)    NULL,
    [Fat]        FLOAT (53)    NULL,
    [Carb]       FLOAT (53)    NULL,
    [Protein]    FLOAT (53)    NULL,
    [Type]       NCHAR (10)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

