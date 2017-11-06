CREATE TABLE Customer
(
	[customerID] INT NOT NULL PRIMARY KEY, 
    [name] VARCHAR(50) NOT NULL, 
    [address] VARCHAR(100) NOT NULL, 
    CONSTRAINT [PK_Table] PRIMARY KEY ([customerID])
)
