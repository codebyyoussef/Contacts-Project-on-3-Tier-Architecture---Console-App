# Contacts Management Console Application

## Description
This is a console-based Contacts Management application built using **3-Tier Architecture**. It allows users to manage contact information, such as name, phone number, and email, in a structured and efficient way. The 3-tier architecture separates the application into three layers, ensuring better organization, maintainability, and scalability.

### Architecture Layers

1. **Presentation Layer (UI Layer)**
   - The console interface where users interact with the application.
   - Users can add, view, update, or delete contacts.

2. **Business Logic Layer (BLL)**
   - Contains the core logic for managing contacts.
   - Handles operations like validating input, processing updates, and enforcing business rules.

3. **Data Access Layer (DAL)**
   - Responsible for storing and retrieving contact information.
   - Can use in-memory storage, files, or a database.

### Key Features
- Add new contacts  
- View all contacts  
- Update existing contacts  
- Delete contacts  
- Follows clean separation of concerns using 3-tier architecture  

### Benefits
- Modular design for easier maintenance  
- Clear separation of responsibilities  
- Scalable and easy to extend  

## How to Use
1. Clone the repository: git clone <repository-url>
2. Restore the database:
If you’re using **SQL Server Management Studio (SSMS)**, you can restore the database in two ways:  
### Option 1: Using SSMS (GUI)
1. Open SSMS and connect to your SQL Server instance.  
2. Right-click on the **Databases** folder and select **Restore Database...**  
3. Choose **Device**, then click the **...** button.  
4. Click **Add**, browse to the location of `ContactsDB.bak`, and select it.  
5. Click **OK** to confirm the selection.  
6. Under **Destination**, ensure the database name is set to `ContactsDB`.  
7. Click **OK** to restore the database.  

### Option 2: Using SQL Query
Alternatively, you can restore the database directly using a SQL command:  
```sql
RESTORE DATABASE ContactsDB
FROM DISK = 'C:\Path\To\ContactsDB.bak';

## Technologies Used
- Programming Language: C#
- Architecture Pattern: 3-Tier (UI, BLL, DAL)
- Database: SQL Server (ContactsDB)
- Interface: Console-based application
