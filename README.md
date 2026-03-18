# Software documentation and design templates
Implement the server-side portion of the application, which consists of three layers:
-    Data Access Layer
-    Business Logic Layer
-    Presentation Layer
The relationship between the layers should be implemented using interfaces (business logic classes must use data access layer interfaces, not implementations) and the Inversion of Control and Dependency Injection patterns
The presentation layer currently does not perform any logic and is represented only by interfaces
The data access layer should be implemented using the ORM framework to populate the database created in Lab 1.b (class diagram). Additionally, the data access layer should implement reading data from a .csv file.
The business logic layer calls the data access layer to read data from the file, creates the necessary models to populate the database, and calls the data access layer to save the information to the database.

Important: All data must be contained in a single file. When loading data into the database, implement the necessary logic to ensure data is correctly stored in the table.
The file must contain at least 1,000 rows.
To create the file, create a separate module/class that runs from the command line.


Translated with DeepL.com (free version)
