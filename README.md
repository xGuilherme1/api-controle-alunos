# api-controle-alunos

API para Controle de Alunos — Desenvolvida em C# com .NET 8

## Descrição

Projeto de exemplo que expõe uma API REST para gerenciar alunos, endereços e suas relações com cursos, turmas e disciplinas. A API usa Entity Framework Core para persistência e segue uma arquitetura simples com Controllers, Models e DTOs.

## Tecnologias

- .NET 8
- C# 12
- ASP.NET Core Web API
- Entity Framework Core

## Estrutura principal (exemplos de arquivos)

- `Controllers/AlunoController.cs` — endpoints para gerenciar alunos e relacionamentos (curso, turma, endereço, disciplinas).
- `Controllers/EnderecoController.cs` — endpoints para gerenciar endereços.
- `Models/` — entidades como `Aluno`, `Endereco`, `Curso`, `Turma`, `Disciplina`, `AlunoDisciplina`.
- `Models/Dtos/` — DTOs usados para entrada/saída nas APIs.
- `Program.cs` — configuração da aplicação e do DbContext.

## Modelos e relacionamento (resumo)

- `Aluno` possui relacionamentos N:1 com `Curso`, `Turma` e `Endereco`.
- `Aluno` tem coleção `AlunoDisciplinas` que representa a relação com `Disciplina` (N:N com payload — `Nota`).

## Endpoints principais

- `GET /alunos` — lista todos os alunos. Suporta filtros por query string: `search` (nome), `cursoId`, `turmaId`.
- `GET /alunos/{id}` — obtém um aluno por id.
- `POST /alunos` — cria um novo aluno (aceita DTO com referências ao curso, turma, endereço e lista de disciplinas com notas).
- `PUT /alunos/{id}` — atualiza um aluno existente (substitui disciplinas associadas).
- `DELETE /alunos/{id}` — remove um aluno e suas entradas em `AlunoDisciplina`.

- `GET /enderecos` — lista endereços (filtro por `cidade`).
- `GET /enderecos/{id}` — obtém um endereço por id.
- `POST /enderecos` — cria um endereço.
- `PUT /enderecos/{id}` — atualiza um endereço.
- `DELETE /enderecos/{id}` — remove um endereço.

## Como executar (local)

1. Certifique-se de ter o .NET 8 SDK instalado: `dotnet --version` (deve ser 8.x).
2. Restaurar pacotes e compilar:

   `dotnet restore`

   `dotnet build`

3. Configurar a string de conexão no arquivo `appsettings.json` (ou usar provider em memória para testes).
4. Aplicar migrations (se usar EF Core com migrations):

   `dotnet ef database update`

5. Executar a aplicação:

   `dotnet run`

## Exemplos de requisições

Criar endereço (POST /enderecos)

JSON de exemplo:

```json
{
  "logradouro": "Rua A",
  "numero": "123",
  "cidade": "São Paulo",
  "estado": "SP"
}
```

Criar aluno (POST /alunos)

JSON de exemplo (presume que `cursoId`, `turmaId` e `enderecoId` já existem):

```json
{
  "nome": "João Silva",
  "dataNascimento": "2004-05-20",
  "cursoId": 1,
  "turmaId": 1,
  "enderecoId": 1,
  "disciplinas": [
    { "disciplinaId": 1, "nota": 8.5 },
    { "disciplinaId": 2, "nota": 7.0 }
  ]
}
```

Curl simples para listar alunos:

`curl -s http://localhost:5000/alunos` (ajuste porta conforme configurada)

## Observações

- Validações básicas são feitas via DTOs e ModelState nos controllers.
- Ao criar/atualizar um aluno, o código verifica a existência de curso, turma, endereço e disciplinas e retorna `404` caso algo esteja faltando.

## Licença

Projeto de exemplo — usa licença do MIT.
