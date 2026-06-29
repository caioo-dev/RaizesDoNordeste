# 🌵 Raízes do Nordeste - API

API REST desenvolvida em **.NET 8** para gerenciamento das operações da franquia **Raízes do Nordeste**, permitindo o cadastro de clientes, produtos, cardápios, unidades e o gerenciamento completo do ciclo de vida dos pedidos.

O projeto foi desenvolvido utilizando arquitetura em camadas, separando responsabilidades entre **Domain**, **Application**, **Infrastructure** e **API**, facilitando a manutenção, evolução e testabilidade da aplicação.

---

# Tecnologias

* .NET 8
* Entity Framework Core
* SQL Server
* JWT (JSON Web Token)
* FluentValidation
* Swagger / OpenAPI
* Mapster

---

# Arquitetura

O projeto segue uma arquitetura em camadas composta por:

```text
RaizesDoNordeste
│
├── Domain
│   ├── Entidades
│   ├── Enums
│   ├── Interfaces
│   └── Regras de negócio
│
├── Application
│   ├── Services
│   ├── DTOs
│   ├── Validators
│   └── Interfaces
│
├── Infrastructure
│   ├── Persistence
│   ├── Repositories
│   ├── Authentication
│   └── Logging
│
└── Server
    ├── Controllers
    ├── Middlewares
    ├── Configurations
    └── Program.cs
```

---

# Funcionalidades

* Autenticação utilizando JWT
* Cadastro de usuários
* Cadastro de clientes
* Cadastro de produtos
* Cadastro de cardápios
* Cadastro de unidades
* Gerenciamento de pedidos
* Histórico de alteração de status dos pedidos
* Programa de fidelização
* Controle de estoque
* Registro de logs de operação
* Documentação automática via Swagger

---

# Requisitos

* .NET SDK 8
* SQL Server
* Visual Studio 2022 ou VS Code

---

# Configuração

## 1. Clone o repositório

```bash
git clone https://github.com/caioo-dev/RaizesDoNordeste.git

cd RaizesDoNordeste
```

## 2. Crie o banco de dados

```sql
CREATE DATABASE RaizesDoNordeste;
```

## 3. Configure o arquivo

```text
appsettings.Development.json
```

Exemplo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=RaizesDoNordeste;Username=postgres;Password=SUA_SENHA"
  },

  "Jwt": {
    "Issuer": "RaizesDoNordeste",
    "Audience": "RaizesDoNordeste",
    "Key": "SUA_CHAVE_SECRETA"
  }
}
```

---

# Executando a aplicação

Restaure os pacotes:

```bash
dotnet restore
```

Execute as migrations:

```bash
dotnet ef database update
```

Inicie a aplicação:

```bash
dotnet run
```

A API estará disponível em:

```
https://localhost:5001
```

ou

```
http://localhost:5000
```

*(As portas podem variar conforme o arquivo `launchSettings.json`.)*

---

# Documentação

Após iniciar a aplicação, a documentação pode ser acessada em:

```
https://localhost:{porta}/swagger
```

---

# Principais Endpoints

## Autenticação

| Método | Endpoint           | Descrição          |
| ------ | ------------------ | ------------------ |
| POST   | /api/auth/register | Registrar usuário  |
| POST   | /api/auth/login    | Autenticar usuário |

---

## Clientes

| Método | Endpoint          |
| ------ | ----------------- |
| GET    | /api/cliente      |
| GET    | /api/cliente/{id} |
| POST   | /api/cliente      |
| PUT    | /api/cliente/{id} |
| DELETE | /api/cliente/{id} |

---

## Produtos

| Método | Endpoint           |
| ------ | ------------------ |
| GET    | /api/produtos      |
| GET    | /api/produtos/{id} |
| POST   | /api/produtos      |
| PUT    | /api/produtos/{id} |
| DELETE | /api/produtos/{id} |

---

## Cardápios

| Método | Endpoint            |
| ------ | ------------------- |
| GET    | /api/cardapios      |
| GET    | /api/cardapios/{id} |
| POST   | /api/cardapios      |
| PUT    | /api/cardapios/{id} |
| DELETE | /api/cardapios/{id} |

---

## Unidades

| Método | Endpoint           |
| ------ | ------------------ |
| GET    | /api/unidades      |
| GET    | /api/unidades/{id} |
| POST   | /api/unidades      |
| PUT    | /api/unidades/{id} |
| DELETE | /api/unidades/{id} |

---

## Pedidos

| Método | Endpoint                            | Descrição                |
| ------ | ----------------------------------- | ------------------------ |
| GET    | /api/pedidos                        | Listar pedidos           |
| GET    | /api/pedidos/{id}                   | Buscar pedido            |
| POST   | /api/pedidos                        | Criar pedido             |
| PATCH  | /api/pedidos/{id}/confirmar         | Confirmar pedido         |
| PATCH  | /api/pedidos/{id}/sair-para-entrega | Pedido saiu para entrega |
| PATCH  | /api/pedidos/{id}/entregar          | Finalizar pedido         |
| PATCH  | /api/pedidos/{id}/cancelar          | Cancelar pedido          |
| GET    | /api/pedidos/{id}/historico         | Histórico do pedido      |

---

# Fluxo principal

1. Registrar um usuário (`POST /api/auth/register`);
2. Realizar login (`POST /api/auth/login`);
3. Informar o token JWT no Swagger utilizando o botão **Authorize**;
4. Cadastrar clientes, produtos e unidades;
5. Criar um pedido;
6. Confirmar o pedido;
7. Alterar o status para **Saiu para Entrega**;
8. Finalizar ou cancelar o pedido;
9. Consultar o histórico de alterações do pedido.

---

# Estrutura do projeto

```text
RaizesDoNordeste.Domain
RaizesDoNordeste.Application
RaizesDoNordeste.Infrastructure
RaizesDoNordeste.Server
```

---

# Autor

**Caio Andrade**

Projeto desenvolvido para fins acadêmicos e de estudo, com foco na aplicação de boas práticas de desenvolvimento utilizando ASP.NET Core, Entity Framework Core e arquitetura em camadas.
