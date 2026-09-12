# Desenvolvimento-Web-Back-end# 🏢 API de Gestão de Franquias (.NET 8)

API desenvolvida em C# utilizando **ASP.NET Core**, **Entity Framework Core** e banco de dados **SQLite**, projetada para o gerenciamento completo de redes de franquias acadêmicas. O sistema atende a 100% dos requisitos de negócio propostos, incluindo arquitetura em camadas, regras de integridade de estoque, controle de vendas, cálculo automático de royalties, fornecedores, chamados de suporte e segurança via **JWT (JSON Web Token)**.

---

## 🚀 Tecnologias Utilizadas
* **C# / .NET 8** (ASP.NET Core Web API)
* **Entity Framework Core (EF Core)** (ORM)
* **SQLite** (Banco de dados relacional leve e em arquivo)
* **JWT (JSON Web Token)** (Autenticação e controle de acesso)
* **Swagger / OpenAPI** (Documentação e testes de rotas)

---

## 📦 Módulos Funcionais Implementados
1. **Unidades Franqueadas**: Cadastro e gestão das unidades da rede.
2. **Produtos e Serviços**: Catálogo centralizado de itens comercializados.
3. **Controle de Estoque**: Gestão de saldos por unidade com validação de estoque insuficiente para evitar saldos negativos.
4. **Vendas**: Registro de transações comerciais atreladas ao estoque.
5. **Royalties**: Cálculo automático dos repasses devidos pelas unidades com base nas vendas.
6. **Fornecedores**: Cadastro de parceiros homologados com validação de CNPJ.
7. **Chamados de Suporte**: Abertura e acompanhamento de ocorrências pelas unidades (Manutenção, TI, Financeiro).
8. **Autenticação (Auth)**: Endpoints de registro de usuários e login com geração de tokens JWT seguros.

---

## 🛠️ Como Executar o Projeto

### Pré-requisitos
Certifique-se de ter o [.NET 8 SDK](https://dotnet.microsoft.com/) instalado em sua máquina.

### Passos para rodar:
1. Clone o repositório ou abra a pasta do projeto no terminal.
2. Restaure as dependências do projeto:
   ```bash
   dotnet restore
Sistema de Gestão de Franquias
