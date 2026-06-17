# YrkesakademinAI

AI-drivet verktyg för lärare på Yrkesakademin. Hjälper Anna Lindqvist att snabba upp feedbackarbete, skapa kursmaterial och besvara vanliga frågor via ett enkelt webbgränssnitt.

## Funktioner

| Modul | Beskrivning |
|---|---|
| **Feedback** | Klistra in en studentinlämning och bedömningskriterier – AI genererar ett feedbackutkast som Anna kan skicka direkt till studentens e-post |
| **Kursmaterial** | Ange ämne och målgrupp – AI skapar ett strukturerat kursmaterialutkast med lärandemål och aktivitetsförslag |
| **FAQ** | Ställ en pedagogisk fråga – AI svarar på ett tydligt och lättförståeligt sätt |

## Arkitektur

```
client/ (Vue.js 3 + Vite)
    └── /api/* → proxy →
backend/ (.NET Minimal API)
    └── POST /feedback, /kursmaterial, /faq
        └── Anthropic Claude Haiku 4.5
automation/ (n8n workflow)
    └── Webhook → Skicka e-post via Gmail SMTP
```

## Tekniker

- **Backend:** .NET 9, C#, Anthropic.SDK, Claude Haiku 4.5 (`claude-haiku-4-5-20251001`)
- **Frontend:** Vue.js 3, Vite, marked.js
- **Automation:** n8n (webhook + Gmail SMTP)

## Kom igång

### Förutsättningar

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/)
- [n8n](https://n8n.io/) – installeras via `npx n8n`
- Anthropic API-nyckel ([console.anthropic.com](https://console.anthropic.com))
- Gmail-konto med App Password aktiverat (för e-postautomation)

### 1. Backend

```bash
cd backend
dotnet user-secrets set "ANTHROPIC_API_KEY" "din-api-nyckel-här"
dotnet run
# Körs på http://localhost:5141
```

### 2. Frontend

```bash
cd client
npm install
npm run dev
# Körs på http://localhost:5173
```

### 3. n8n-automation

```bash
npx n8n
# Öppna http://localhost:5678
```

Importera `automation/feedback-automation.json` i n8n:
1. Klicka **New Workflow** → **Import from file**
2. Välj `automation/feedback-automation.json`
3. Öppna **Send Email**-noden och fyll i dina SMTP-uppgifter
4. Aktivera workflow:et

> **OBS:** JSON-filen innehåller platshållare för e-postadresser och lösenord. Fyll i riktiga uppgifter lokalt – commita aldrig riktiga uppgifter till repot.

## API-endpoints

### POST /feedback

```json
{
  "inlamning": "Studentens inlämningstext...",
  "kriterier": "Bedömningskriterierna..."
}
```

Returnerar: `{ "reply": "AI-genererat feedbackutkast..." }`

### POST /kursmaterial

```json
{
  "amne": "Objektorienterad programmering",
  "malgrupp": "Nybörjare på YH-programmet"
}
```

Returnerar: `{ "reply": "Kursmaterialutkast med lärandemål..." }`

### POST /faq

```json
{
  "fraga": "Vad är skillnaden mellan frontend och backend?"
}
```

Returnerar: `{ "reply": "Pedagogiskt svar..." }`

## Säkerhet

- API-nyckeln lagras via `dotnet user-secrets` – aldrig i källkod eller Git
- CORS begränsar anrop till `http://localhost:5173`
- Mänsklig granskning krävs innan feedback skickas till student
- Se säkerhetsanalysen i inlämningsdokumentet för fullständig OWASP LLM Top 10-kartläggning

## Projektstruktur

```
YrkesakademinAI/
├── backend/          .NET Minimal API
│   ├── Program.cs
│   └── *.csproj
├── client/           Vue.js frontend
│   ├── src/
│   │   ├── views/
│   │   │   ├── FeedbackView.vue
│   │   │   ├── KursmaterialView.vue
│   │   │   └── FAQView.vue
│   │   └── App.vue
│   └── vite.config.js
└── automation/       n8n workflow
    └── feedback-automation.json
```
