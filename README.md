# StudioAgenda

Sistema de gerenciamento para estúdios de manicure, desenvolvido com **.NET 8** utilizando os princípios da **Clean Architecture**. O projeto tem como objetivo centralizar o gerenciamento de clientes, profissionais, serviços e agendamentos, oferecendo uma API organizada, escalável e de fácil manutenção.

> **Status:** Em desenvolvimento.

## Objetivos

* Gerenciamento de clientes.
* Gerenciamento de profissionais.
* Cadastro de serviços.
* Controle de agenda e horários disponíveis.
* Agendamento de atendimentos.
* Validação das regras de negócio.
* Autenticação e autorização com JWT (em desenvolvimento).

## Tecnologias

* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* FluentValidation
* Mapster
* Docker
* Swagger / OpenAPI

## Arquitetura

O projeto segue os princípios da **Clean Architecture**, separando responsabilidades em diferentes camadas.

```text
src/
├── GerenciaStudio.API
├── GerenciaStudio.Application
├── GerenciaStudio.Domain
├── GerenciaStudio.Exception
└── GerenciaStudio.Infrastructure

```

## Recursos previstos

* Cadastro de clientes
* Cadastro de profissionais
* Cadastro de serviços
* Cadastro de horários disponíveis
* Agendamento de atendimentos
* Consulta de agenda
* Controle de disponibilidade
* Autenticação com JWT
* Documentação via Swagger

## Tecnologias utilizadas

| Tecnologia                 | Finalidade                        |
| -------------------------- | --------------------------------- |
| Entity Framework Core      | Persistência de dados             |
| FluentValidation           | Validação das requisições         |
| Mapster                    | Mapeamento entre DTOs e entidades |
| SQL Server                 | Banco de dados                    |
| Swagger                    | Documentação da API               |
| Docker                     | Ambiente de desenvolvimento       |
| JWT *(em desenvolvimento)* | Autenticação e autorização        |

## Estrutura do projeto

```text
GerenciaStudio
│
├── src
│   ├── GerenciaStudio.API
│   ├── GerenciaStudio.Application
│   ├── GerenciaStudio.Domain
|   ├── GerenciaStudio.Exception
│   └── GerenciaStudio.Infrastructure
│
└── docker-compose.yml
```

## Como executar

### Pré-requisitos

* .NET 8 SDK
* Docker
* SQL Server

### Clonar o projeto

```bash
git clone https://github.com/carolinefreitasalegre/GerenciaStudio.git

cd GerenciaStudio
```

### Executar o banco de dados

```bash
docker compose up -d
```

### Aplicar as migrations

```bash
dotnet ef database update
```

### Executar a aplicação

```bash
dotnet run --project src/GerenciaStudio.API
```

## Documentação da API

Após iniciar a aplicação, a documentação poderá ser acessada pelo Swagger.

```text
https://localhost:{porta}/swagger
```

## Funcionalidades em desenvolvimento

* Autenticação utilizando JWT
* Controle de permissões
* Refresh Token
* Testes automatizados
* Integração com envio de notificações
* Melhorias nas regras de agendamento

## Licença

Este projeto está disponível para fins de estudo e desenvolvimento.
