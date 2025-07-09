# avtobus1-url-shortener

Test task on the topic of url shortener service at Avtobus1.

## Project Architecture

The project follows an N-layer architecture:
- **UrlShortener.Web** — presentation layer (MVC + API)
- **UrlShortener.Application** — business and infrastructure logic, abstractions
- **UrlShortener.Persistence** — data access, configurations and database migrations

*(Также можно было использовать и чистую архитектуру, но она тут более чем избыточна, хотя при этом можно было вообще абстрагироваться от архитектуры и выполнить всё в одном проекте из-за его маленького размера. Паттерн Repository не использовался по той же причине)*

*(As an alternative to Docker, there is a configuration for Aspire)*

---

## Features

- **URL shortening**: Generates unpredictable, non-sequential short URLs for any valid long URL.
- **Redirection**: Short URLs redirect to the original long URL, with transition counting.
- **Statistics**: Each URL entity stores creation date and the number of transitions.
- **CRUD operations**: Add, edit, and delete shortened URLs.
- **Validation**: Input is validated for correct URL format.
- **Automatic DB migrations**: On startup, the database schema is created/updated automatically.
- **MariaDB/MySQL storage**: All data is persisted in a relational database.
- **Error handling**: User-friendly error messages for invalid input and API errors.

---

## Possible Improvements

- **Add caching** (`Redis` or `IDistributedCache`) for read-heavy operations to reduce database load.
- ...

---

## Web UI (MVC)

The project uses ASP.NET Core MVC for the user interface, styled with **Tailwind CSS**, and includes two main pages:

1. **Main Page (Index)**
   - Displays a table of all shortened URLs.
   - Columns: Long URL, Short URL, Creation Date, Transition Count.
   - Allows deleting entries.

2. **Create/Edit Page**
   - Form for creating a new short URL or editing an existing one.
   - Validates input and provides error messages for invalid URLs.

---

## Controllers

### HomeController (MVC)

- **Index**: Shows the table of all URL entities.
- **Url (GET)**: Shows the create/edit form for a URL entity.
- **Url (POST)**: Handles creation or update of a URL entity.
- **Delete**: Deletes a URL entity.
- **Error**: Error page.

### UrlController (API)

- **GET /api/v1/url**: Returns all URL entities.
- **GET /api/v1/url/{id}**: Returns a specific URL entity by ID.
- **GET /api/{code}**: Redirects to the long URL by short code and increments the transition count.
- **POST /api/v1/url**: Creates a new short URL (validates input).
- **PATCH /api/v1/url**: Updates an existing URL entity.
- **DELETE /api/v1/url/{id}**: Deletes a URL entity.

---

## How to use (Docker)

1. Clone the repo:
   ```shell
   https://github.com/SlavaKuntsov/avtobus1-url-shortener.git
   cd avtobus1-url-shortener
   ```
2. Build Docker container:
   ```shell
   docker compose --project-name url_shortener up -d --build
   ```

Check:
- Site - http://localhost:7000
- Swagger UI - http://localhost:7000/swagger/index.html

---

## Technical Requirements (from the task)

- .NET Core 8, C#
- Custom URL shortening logic (no external services)
- Transition counting for each short URL
- Main page with table: Long URL, Short URL, Creation Date, Transition Count
- Deletion and editing of entries
- MariaDB 10.3+ for storage
- Unpredictable short URL codes (not sequential)
- Automatic DB migrations
- Input validation and error handling
- No unnecessary complexity