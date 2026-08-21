# StudioAgenda

Sistema de gerenciamento para estúdios de manicure, desenvolvido com **.NET 8** utilizando os princípios da **Clean Architecture**. O projeto tem como objetivo centralizar o gerenciamento de clientes, profissionais, serviços e agendamentos, oferecendo uma API organizada, escalável e de fácil manutenção.

> **Status:** Em desenvolvimento.

---

## Sumário

- [Visão Geral](#visão-geral)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Funcionalidades e Roadmap](#funcionalidades-e-roadmap)
- [Como Executar o Projeto](#como-executar-o-projeto)
- [Executando os Testes](#executando-os-testes)
- [Licença](#licença)

---

## Visão Geral

O **StudioAgenda** é uma solução voltada para o agendamento e a gestão de atendimentos em estúdios de beleza e manicures. O projeto foi projetado seguindo as melhores práticas de arquitetura de software, desacoplando regras de negócio, infraestrutura e camada de apresentação para facilitar a evolução e testes contínuos da aplicação.

---

## Tecnologias Utilizadas

- **Linguagem & Framework:** .NET 8 (C#)
- **Banco de Dados:** SQL Server
- **ORMs / Acesso a Dados:** Entity Framework Core
- **Documentação de API:** Swagger (OpenAPI)
- **Criptografia / Segurança:** Argon2 (para cálculo seguro de hash de senhas)
- **Tratamento de erros
- **Containerização:** Docker e Docker Compose
- **Testes & Dados Fictícios:** 
  - xUnit / Moq (ou equivalente para testes unitários)
  - Bogus (geração de massa de dados fictícios para testes)

---


## Desenvolvimento da Aplicação
Ainda serão implementados sistema de mensageria para notificação de registros e uso do JWT para Tokens.

---

## Estrutura do Projeto

O repositório está organizado em camadas para manter a separação de responsabilidades (Clean Architecture):

```text
src/
├── Backend/
│   ├── StudioAgenda.Api           # Entry point / Controladores REST e documentação Swagger
│   ├── StudioAgenda.Application   # Casos de uso e regras de aplicação
│   ├── StudioAgenda.Domain        # Entidades, interfaces e regras de negócio
│   └── StudioAgenda.Infrastructure# Acesso a dados (EF Core, SQL Server) e criptografia (Argon2)
├── Shared/
│   ├── StudioAgenda.Communication # DTOs de solicitação e resposta
│   └── StudioAgenda.Exceptions    # Tratamento customizado de exceções
└── Testes/
    ├── CommonTestUtilities        # Utilitários e builders de dados para testes
    ├── UseCases.Tests             # Testes unitários dos casos de uso
    ├── Validacoes.Tests           # Testes unitários de validações
    └── WebApi.Tests               # Testes das rotas e integrações da API










