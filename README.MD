# 🧩 LarSolution

Aplicação desenvolvida em **.NET 8**, estruturada com **arquitetura em camadas** (Clean Architecture) e aplicação dos princípios **SOLID** e **Clean Code**.  
O sistema tem como objetivo o gerenciamento de **usuários, pessoas e telefones**, contemplando autenticação JWT, logging estruturado, validações robustas e integração com Docker.

---

## Tecnologias utilizadas

- **.NET 8**
- **ASP.NET Core Web API**
- **Entity Framework Core** (PostgreSQL, InMemory e SQLite)
- **AutoMapper**
- **FluentValidation**
- **JWT Authentication**
- **Middleware personalizado**
- **Docker / Docker Compose**
- **Swagger (Swashbuckle)**
- **Serilog** (logging)
- **BCrypt.Net** (hash de senhas)
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


**Padrões aplicados:**
- Repository Pattern  
- Dependency Injection  
- DTOs e Mappers  
- Validation Layer  
- Middleware de erro global  
- Logging estruturado (Serilog)

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
- A imagem da API é construída via Dockerfile
- O docker-compose.yml configura a API e o banco PostgreSQL

**Subir todo o ambiente**
`docker-compose up --build`

---

## Logging

- Implementado com Serilog
- Logs para criação, atualização, exclusão e erros
- Saída no console e integração com providers externos (extensível)
- Middleware global captura exceções e retorna resposta padronizada

---

## Testes

- Testes unitários com xUnit, Moq e FluentAssertions
- Cobertura para:
- Serviços: UsuarioService, AuthService, PessoaService, TelefoneService
- Controllers: UsuariosController, PessoasController, TelefonesController, AuthController
- Validações simuladas usando mocks do FluentValidation

**Rodar os testes**
`dotnet test`

---

# Como executar o projeto

## Clonar o repositório:
`
git clone <URL_DO_REPOSITORIO>
cd LarSolution
`

## 1. Clonar o repositório:
`
git clone <URL_DO_REPOSITORIO>
cd LarSolution
`

## 2. Restaurar pacotes:
`
dotnet restore
`

## 3. Criar banco e aplicar migrations:
`
cd Lar.Infrastructure 
dotnet ef database update
`

## 4. Executar a API:
`
cd Lar.API 
dotnet run
`

## Testar endpoints via Swagger:
Acesse https://localhost:5026/swagger

---

# Exemplos de chamadas de API

## Criar Pessoa
`
POST /api/pessoas  
Body:  
{  
  "nome": "João Silva",  
  "email": "joao@email.com"  
}
`

## Login
`
POST /api/auth/login
Body: 
{ 
  "username": "joaos",
  "password": "123456"
} 
Response:
{ 
  "token": "<JWT_TOKEN>" 
}
`

## Criar Usuário
`
POST /api/usuarios >
Body: 
{  
  "username": "joaos", 
  "password": "123456", 
  "pessoaId": 1  
}
`

## Consultar Telefones por Pessoa
GET /api/telefones/pessoa/1 
Headers:  
Authorization: Bearer <JWT_TOKEN>

---

# Observações
- Endpoints protegidos com [Authorize]
- Senhas nunca são armazenadas em texto claro
- Logs monitoram operações críticas
- Projeto estruturado para manutenção e escalabilidade
- Estrutura projetada para facilidade de manutenção e escalabilidade

---

Autor

Luiz Filipe
Desenvolvedor .NET | Clean Architecture | SOLID | Docker | Testes Automatizados
🔗 GitHub

---

```json
"ConnectionStrings": {
  "PostgresConnection": "Host=localhost;Database=LarDb;Username=postgres;Password=senha"
}
