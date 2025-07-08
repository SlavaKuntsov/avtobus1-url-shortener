# avtobus1-url-shortener
Test task on the topic of url shortener service at Avtobus1.

# Project Architecture
The project follows an N-layer architecture:
- UrlShortener.Web
- UrlShortener.Application
- UrlShortener.Persistence

*(Также можно было использовать и чистую архитектуру, но она тут более чем избыточна,
хотя при этом можно было вообще абстрагироваться от архитектуры и выполнить всё в одном проекте 
из-за его маленького размера.)*

# How to use (Docker)

1. Clone the repo:
``` shell
https://github.com/SlavaKuntsov/avtobus1-url-shortener.git
cd avtobus1-url-shortener
```
2. Build Docker container:
``` shell
docker compose --project-name url_shortener up -d --build
```

Check:
- Site - http://localhost:7000
- Swagger UI - http://localhost:7000/swagger/index.html