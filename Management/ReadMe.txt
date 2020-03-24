
Create Model from DB (Db first)
Scaffold-DbContext "server=DESKTOP-PTGVLQG;Database=MailSystem;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models


Scaffold-DbContext "server=LAPTOP-DVJT5BST;database=StudentTracker;uid=Ahmed;pwd=**********;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force

 public SmartEducationContext(DbContextOptions<SmartEducationContext> options) : base(options) { }


Abdala PC : 
Scaffold-DbContext "server=LAPTOP-DVJT5BST;database=Credits_db;uid=AhmedTareck;pwd=35087124567Ahmed;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

Scaffold-DbContext "server=LAPTOP-DVJT5BST;database=SmartEducation;uid=Ahmed;pwd=35087124567Ahmed;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force
-----------------------------------------

Code-First Approach

Enable-migrate
Add-Migration StudentTracker
update-database

