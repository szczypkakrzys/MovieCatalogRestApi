# Movie Catalog REST API
## Implementation 
- The code compiles without any warnings.
- I used SwaggerUI, so after executing _dotnet run_ command in order to see interface, you need to add _/swagger_ or _/swagger/index.html_ to URL.
- There weren't any DB requirements, so I stored testing data in memory as a list of objects.
- To comfortably work on data, I implemented model class and mapped it with DTO via AutoMapper. This operation isolates some "sensitive" data from user. Also added basic data validation.
- I used services pattern to separate methods definition from controller. This allows me to only call those methods in controller and serve the outcome with proper status codes.
## Rest API methods
1) POST: allows to add a movie to the catalog
2) GET: allows retrieving the last added movie
3) GET/[year]: retrieves movies from a given year
4) GET/[genre]: retrieves movies from a given genre.