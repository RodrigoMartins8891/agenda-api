# 📙 README — BACKEND (.NET)

```md
# Agenda API

API REST para gerenciamento de consultas médicas.

Projeto desenvolvido em .NET com foco em arquitetura em camadas, autenticação segura e separação de responsabilidades.

## 🚀 Funcionalidades

- Cadastro e login de usuários
- Autenticação com JWT
- CRUD de consultas
- Consultas filtradas por usuário logado
- Regras de autorização no backend
- Proteção contra acesso indevido

## 🧠 Arquitetura

O backend segue organização em camadas:

- Controllers → recebem requisições HTTP
- Services → regras de negócio
- Models → entidades do sistema
- DTOs → contratos de entrada e saída
- Data → contexto e acesso ao banco

As regras de segurança e validação ficam centralizadas nos serviços, não nos controllers.

## 🔐 Segurança

- Autenticação via JWT
- Validação do usuário logado em cada operação
- Proteção de rotas com `[Authorize]`
- Associação automática das consultas ao usuário autenticado

## 🛠 Tecnologias

- .NET
- Entity Framework Core
- JWT Authentication
- SQL Server

## ▶️ Como rodar o projeto

1. Configurar string de conexão no `appsettings.json`
2. Executar migrations
3. Rodar a API

```bash
dotnet run

Swagger disponível em:

http://localhost:5000/swagger
🎯 Objetivo do projeto

Este backend foi criado para praticar construção de APIs reais com foco em arquitetura, segurança e integração com frontend.


---

Se quiser, posso te mandar também:

- ⭐ :contentReference[oaicite:0]{index=0}
- ⭐ :contentReference[oaicite:1]{index=1}
- ⭐ :contentReference[oaicite:2]{index=2}
- ⭐ :contentReference[oaicite:3]{index=3}

Quer que eu :contentReference[oaicite:4]{index=4}?
