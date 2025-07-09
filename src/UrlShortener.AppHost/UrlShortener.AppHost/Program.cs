var builder = DistributedApplication.CreateBuilder(args);

var mysql = builder.AddMySql("mysql")
	.WithDataVolume()
	.AddDatabase("url-shortener");

builder.AddProject<Projects.UrlShortener_Web>("urlshortener-web")
	.WithReference(mysql)
	.WaitFor(mysql);

builder.Build().Run();
