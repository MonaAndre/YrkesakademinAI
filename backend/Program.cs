using Anthropic;
using Anthropic.Models.Messages;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Skapa Anthropic-klient — läser ANTHROPIC_API_KEY automatiskt från miljövariabel
AnthropicClient claude = new();

// Modell som används för alla anrop
const string Modell = "claude-haiku-4-5-20251001";

// Hjälpfunktion: skicka ett meddelande till Claude och returnera svaret som sträng
async Task<string> FragaClaude(string prompt)
{
    var svar = await claude.Messages.Create(new MessageCreateParams
    {
        Model = Modell,
        MaxTokens = 1024,
        Messages = [new() { Role = Role.User, Content = prompt }]
    });

    return svar.Content.Select(b => b.Value).OfType<TextBlock>().FirstOrDefault()?.Text ?? "";
}

// POST /feedback
// Tar emot en inlämning och kriterier, returnerar ett feedbackutkast
app.MapPost("/feedback", async (FeedbackForfragan req) =>
{
    var prompt = $"""
        Du är en hjälpsam och konstruktiv lärare på en yrkesutbildning.
        Ge detaljerad feedback på följande inlämning utifrån de angivna kriterierna.

        Inlämning:
        {req.Inlamning}

        Kriterier:
        {req.Kriterier}

        Skriv ett genomtänkt och uppmuntrande feedbackutkast på svenska.
        """;

    var text = await FragaClaude(prompt);
    return Results.Ok(new { reply = text });
});

// POST /kursmaterial
// Tar emot ett ämne och en målgrupp, returnerar ett kursmaterialutkast
app.MapPost("/kursmaterial", async (KursmaterialForfragan req) =>
{
    var prompt = $"""
        Du är en pedagogisk expert och kursutvecklare.
        Skapa ett strukturerat kursmaterialutkast om följande ämne, anpassat för målgruppen.

        Ämne:
        {req.Amne}

        Målgrupp:
        {req.Malgrupp}

        Inkludera: lärandemål, innehållsöversikt, förslag på aktiviteter och bedömningsmetoder.
        Skriv på svenska.
        """;

    var text = await FragaClaude(prompt);
    return Results.Ok(new { reply = text });
});

// POST /faq
// Tar emot en fråga och returnerar ett tydligt svar
app.MapPost("/faq", async (FaqForfragan req) =>
{
    var prompt = $"""
        Du är en hjälpsam och pedagogisk assistent på en yrkesutbildning.
        Svara på följande fråga på ett tydligt och lättförståeligt sätt.

        Fråga:
        {req.Fraga}

        Skriv svaret på svenska.
        """;

    var text = await FragaClaude(prompt);
    return Results.Ok(new { reply = text });
});

app.Run();

// Datamodeller för inkommande JSON
record FeedbackForfragan(string Inlamning, string Kriterier);
record KursmaterialForfragan(string Amne, string Malgrupp);
record FaqForfragan(string Fraga);
