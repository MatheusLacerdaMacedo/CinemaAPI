using Microsoft.EntityFrameworkCore;
using CinemaAPI.Data; 
using CinemaAPI.Models; 

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=CinemaDatabase.db"));

var app = builder.Build();
    
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/cinemas", async (AppDbContext db) => await db.Cinemas.Include(c => c.Filmes).ToListAsync());
app.MapPost("/cinemas", async (AppDbContext db, Cinema cinema) => 
{
    await db.Cinemas.AddAsync(cinema);
    await db.SaveChangesAsync();
    return Results.Created($"/cinemas/{cinema.Id}", cinema);
});
app.MapDelete("/cinemas/{id:int}", async (int id, AppDbContext db) =>
{
    var cinema = await db.Cinemas.FindAsync(id);
    if (cinema == null) return Results.NotFound();
    db.Cinemas.Remove(cinema);
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapPut("/cinemas/{id:int}", async (AppDbContext db, Cinema updateCinema, int id) => 
{
    var cinema = await db.Cinemas.FindAsync(id);
    if (cinema == null) return Results.NotFound();
    cinema.Nome = updateCinema.Nome;
    cinema.Cnpj = updateCinema.Cnpj;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapGet("/cinemas/{cinemaId:int}/filmes", async (int cinemaId, AppDbContext db) =>
{
    var filmes = await db.Filmes.Where(f => f.CinemaId == cinemaId).ToListAsync();
    return filmes;
});
app.MapPost("/cinemas/{cinemaId:int}/filmes", async (int cinemaId, AppDbContext db, Filme filme) => 
{
    filme.CinemaId = cinemaId;
    await db.Filmes.AddAsync(filme);
    await db.SaveChangesAsync();
    return Results.Created($"/cinemas/{cinemaId}/filmes/{filme.Id}", filme);
});

app.Run();
