# LarSolution

Projeto desenvolvido em .NET 8 com arquitetura em camadas, utilizando **Entity Framework Core**, **AutoMapper**, **FluentValidation**, **JWT Authentication**, **Middleware**, **Docker**, **Swagger** e **Serilog** para logging. O projeto consiste em um sistema de gerenciamento de **usuários, pessoas e telefones**.

O projeto segue todas as convenções se SOLID e CLEAN CODE, desde a estruturação das pastas dos arquivos
até a nomenclatura de variávies e metódos.

---

## Tecnologias utilizadas

- **.NET 8**
- **ASP.NET Core Web API**
- **Entity Framework Core (PostgreSQL, InMemory, SQLite)**
- **AutoMapper**
- **FluentValidation**
- **JWT Authentication**
- **Middleware**
- **Docker**
- **Swagger**
- **BCrypt.Net** (para hashing de senhas)
- **Serilog** (logging)
- **xUnit**, **Moq**, **FluentAssertions** (testes unitários e de integração)

---

## Estrutura do projeto

O projeto está dividido em quatro camadas principais:

LarSolution/ <br>
│ <br>
├─ Lar.API -> Camada de apresentação (Controllers, autenticação JWT) <br>
├─ Lar.Application -> Lógica de negócio (Services, DTOs, Validators, AutoMapper) <br>
├─ Lar.Domain -> Entidades e interfaces de repositórios <br>
├─ Lar.Infrastructure -> Implementação de repositórios, DbContext e acesso ao banco <br>
├─ Lar.Tests -> Testes unitários e de controllers <br>

---

## Funcionalidades implementadas

### 1. **Usuários**
- CRUD de usuários
- Autenticação via JWT
- Senhas armazenadas com hash usando **BCrypt**
- Validações com **FluentValidation**:
  - `Username` obrigatório e mínimo de 3 caracteres
  - `Password` obrigatório e mínimo de 3 caracteres
  - `PessoaId` obrigatório
- Logging de operações (criação, atualização, exclusão)

### 2. **Pessoas**
- CRUD de pessoas
- Relacionamento com telefones (uma pessoa pode ter vários telefones)
- Validações:
  - `Nome` obrigatório
  - `Email` obrigatório e válido
- Logging de operações

### 3. **Telefones**
- CRUD de telefones
- Consulta de telefones por pessoa
- Logging de operações

### 4. **Autenticação**
- Login via endpoint `api/auth/login`
- Geração de token JWT com claims:
  - `sub` → username
  - `jti` → GUID único
- Validação de token em endpoints protegidos (`[Authorize]`)

---

## Banco de dados

- Utilizado **PostgreSQL** como principal
- Configuração no `appsettings.json`:

### Docker
- Imagem da API construida
- docker composer contendo o banco de dados

---

## Logging

- Configurado com Serilog para console
- Logs de criação, atualização, exclusão e erros
- Avisos quando registros não são encontrados

---

## Testes

- Testes unitários com xUnit, Moq e FluentAssertions
- Cobertura para:
- Serviços: UsuarioService, AuthService, PessoaService, TelefoneService
- Controllers: UsuariosController, PessoasController, TelefonesController, AuthController
- Validações simuladas usando mocks do FluentValidation

---

## Como rodar o projeto

# Clonar o repositório:
git clone <URL_DO_REPOSITORIO>
cd LarSolution

# 1. Clonar o repositório:
`
git clone <URL_DO_REPOSITORIO> <br>
cd LarSolution
`

# 2. Restaurar pacotes:
`
dotnet restore
`

# 3. Criar banco e aplicar migrations:
`
cd Lar.Infrastructure <br>
dotnet ef database update
`

## 4. Executar a API:
`
cd Lar.API <br>
dotnet run
`

## Testar endpoints via Swagger:
Acesse https://localhost:5026/swagger

---

# Exemplos de chamadas de API

## Criar Pessoa
`
POST /api/pessoas  <br>
Body:  <br>
{  <br>
  "nome": "João Silva",  <br>
  "email": "joao@email.com"  <br>
}
`

## Login
`
POST /api/auth/login  <br>
Body:  <br>
{  <br>
  "username": "joaos",  <br>
  "password": "123456"  <br>
}  <br> 
Response:  <br>
{  <br>
  "token": "<JWT_TOKEN>"  <br>
}
`

## Criar Usuário
`
POST /api/usuarios  <br> 
Body:  <br>
{  <br>
  "username": "joaos",  <br>
  "password": "123456",  <br> 
  "pessoaId": 1  <br>
}
`

## Consultar Telefones por Pessoa
`
GET /api/telefones/pessoa/1  <br>
Headers:  <br>
Authorization: Bearer <JWT_TOKEN>
`
---

# Observações
- Endpoints protegidos com [Authorize]
- Senhas nunca são armazenadas em texto claro
- Logs monitoram operações críticas
- Projeto estruturado para manutenção e escalabilidade

---

```json
"ConnectionStrings": {
  "PostgresConnection": "Host=localhost;Database=LarDb;Username=postgres;Password=senha"
}
