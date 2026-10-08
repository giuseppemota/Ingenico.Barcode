# Ingenico.Barcode

API .NET 8 para cadastro, consulta e leitura de produtos por QR code. O banco de dados de produção usa PostgreSQL (Neon); imagens de produtos são armazenadas no Cloudinary.

## Desenvolvimento

Abra a pasta no VS Code e escolha **Reopen in Container**. O Dev Container instala o SDK .NET 8 e a ferramenta `dotnet-ef`; o host não precisa ter o .NET instalado.

Configure as variáveis de ambiente antes de iniciar a API:

| Variável | Finalidade |
| --- | --- |
| `ConnectionStrings__DefaultConnection` | Connection string PostgreSQL |
| `Jwt__Key` | Chave aleatória com pelo menos 32 bytes |
| `Cloudinary__CloudName` | Cloud name da conta Cloudinary |
| `Cloudinary__ApiKey` | API key do Cloudinary |
| `Cloudinary__ApiSecret` | API secret do Cloudinary |
| `BootstrapUser__Email` | E-mail do usuário inicial |
| `BootstrapUser__Password` | Senha do usuário inicial |

No Render, configure esses valores em **Environment** como variáveis/segredos do serviço. Não os grave no repositório nem os envie em mensagens. A aplicação aplica as migrations PostgreSQL e cria o usuário inicial na primeira inicialização; execuções seguintes não recriam nem redefinem a senha desse usuário.

Para gerar migrations após alterações no modelo:

```sh
export ConnectionStrings__DefaultConnection='Host=localhost;Database=ingenico;Username=postgres;Password=local-only'
dotnet ef migrations add NomeDaMigration \
  --project src/Ingenico.Barcode.Data/Ingenico.Barcode.Data.csproj \
  --startup-project src/Ingenico.Barcode.API/Ingenico.Barcode.API.csproj
```

## Deploy no Render

Use `render.yaml` como Blueprint no Render e conecte o repositório. Ele configura um serviço Docker no plano gratuito, porta `10000` e health check em `/health`; o plano gratuito pode suspender a API após períodos sem tráfego.

Ao importar o Blueprint, preencha os segredos solicitados com a connection string do Neon, uma chave JWT nova e as credenciais do Cloudinary. Defina também o e-mail e a senha fortes do usuário inicial; não reutilize a chave JWT que estava no arquivo de configuração antigo. Para o CORS, `Cors__AllowedOrigins__0` já contém `https://ingenico-barcode.vercel.app`; inclua outras origens públicas como `Cors__AllowedOrigins__1`. O Swagger fica habilitado apenas no ambiente Development.
