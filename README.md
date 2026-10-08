# Ingenico Barcode — API

API REST em .NET 8 para cadastro, consulta e leitura de produtos por QR Code. O PostgreSQL é hospedado no Neon e as imagens dos produtos são armazenadas no Cloudinary.

## Aplicação publicada

- [Frontend](https://ingenico-barcode.vercel.app)
- [Leitor de QR Code](https://ingenico-barcode.vercel.app/leitor-qrcode)
- [API — health check](https://ingenico-barcode-api.onrender.com/health)

O leitor é uma tela pública para consulta de produtos, pensada para ficar aberta em um dispositivo ou totem de supermercado. A área de login é destinada aos usuários que operam o cadastro e o gerenciamento do catálogo.

> **Atenção:** no estado atual, o controller de produtos não exige autenticação (`[Authorize]` está comentado). Portanto, não trate as operações de cadastro, atualização ou exclusão como protegidas, mesmo que o frontend exija login para acessá-las. Antes de usar com dados reais, habilite e teste a autorização dessas operações no backend.

## Endpoints principais

| Método | Rota | Descrição |
| --- | --- | --- |
| `GET` | `/health` | Verifica a disponibilidade da API |
| `POST` | `/v1/Auth` | Registra um usuário |
| `POST` | `/v1/Auth/Login` | Autentica um usuário e retorna um JWT |
| `GET` | `/Produto/v1/Produtos` | Lista produtos |
| `GET` | `/Produto/v1/Produtos/{produtoId}` | Consulta um produto |
| `GET` | `/Produto/v1/Produtos/{produtoId}/Similares` | Lista produtos similares |
| `GET` | `/Produto/v1/Produtos/{produtoId}/imagem` | Consulta a imagem do produto |
| `POST` | `/Produto/v1/Produtos` | Cadastra um produto (`multipart/form-data`) |
| `PUT` | `/Produto/v1/Produtos` | Atualiza um produto (`multipart/form-data`) |
| `DELETE` | `/Produto/v1/Produtos` | Exclui um produto |

O Swagger fica habilitado somente no ambiente `Development`.

## Desenvolvimento local

Abra a pasta no VS Code e escolha **Reopen in Container**. O Dev Container instala o SDK .NET 8 e a ferramenta `dotnet-ef`.

Configure as variáveis necessárias antes de iniciar a API:

| Variável | Finalidade |
| --- | --- |
| `ConnectionStrings__DefaultConnection` | Connection string PostgreSQL |
| `Jwt__Key` | Chave JWT aleatória com pelo menos 32 bytes |
| `BootstrapUser__Email` | E-mail do usuário inicial |
| `BootstrapUser__Password` | Senha forte do usuário inicial |
| `Cloudinary__CloudName` | Cloud name da conta Cloudinary |
| `Cloudinary__ApiKey` | API key do Cloudinary |
| `Cloudinary__ApiSecret` | API secret do Cloudinary |
| `Cors__AllowedOrigins__0` | Origem permitida do frontend |
| `Cors__AllowedOrigins__1` | Origem permitida adicional, se necessária |

Exemplo de formato da connection string Npgsql:

```text
Host=HOST;Port=5432;Database=NOME_DO_BANCO;Username=USUARIO;Password=SENHA;SSL Mode=Require
```

Inicie a API:

```sh
dotnet run --project src/Ingenico.Barcode.API/Ingenico.Barcode.API.csproj
```

Para gerar migrations após alterações no modelo:

```sh
dotnet ef migrations add NomeDaMigration \
  --project src/Ingenico.Barcode.Data/Ingenico.Barcode.Data.csproj \
  --startup-project src/Ingenico.Barcode.API/Ingenico.Barcode.API.csproj
```

## Deploy no Render

O arquivo `render.yaml` configura um serviço Docker, a porta `10000` e o health check em `/health`. Configure os segredos do serviço em **Environment** no Render. Não grave credenciais, connection strings ou chaves JWT no repositório.

`Cors__AllowedOrigins__0` já está definido para `https://ingenico-barcode.vercel.app`. Para permitir outra origem, como um Preview do Vercel, configure `Cors__AllowedOrigins__1` com a origem exata (protocolo e domínio, sem caminho ou barra final) e faça novo deploy da API.

As migrations são aplicadas na inicialização. O usuário inicial é criado apenas se ainda não existir; alterar `BootstrapUser__Password` depois disso não redefine a senha do usuário existente. Não publique credenciais de demonstração em READMEs públicos; compartilhe acesso de teste por um canal apropriado e rotacione senhas que já tenham sido expostas.
