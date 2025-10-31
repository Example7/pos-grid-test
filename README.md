1. Połączenie z bazą
W pliku **`backend/appsettings.json`** uzupełnij dane Supabase:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "User Id=postgres.[twój_id];Password=[twoje_hasło];Server=aws-1-eu-north-1.pooler.supabase.com;Port=5432;Database=postgres;SSL Mode=Require;Trust Server Certificate=true"
  }
}
```

2. Konfiguracja backendu

```bash
cd backend
dotnet restore
dotnet build
dotnet run
```

3. Konfiguracja frontendu

```bash
cd ../frontend
yarn install
yarn dev
```