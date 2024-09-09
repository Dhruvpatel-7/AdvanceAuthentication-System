## Authentication System

## Steps:

Add Proper Connection String in appsettings.json:

    "Server=yourserver;Database=yourdatabase;Integrated Security=True;TrustServerCertificate=True"

Migrations(Write This Inside The Package manager Console):

    Add-Migration Initialcreate

Add To Database:

    Update-Database

Its Done ✅


    
