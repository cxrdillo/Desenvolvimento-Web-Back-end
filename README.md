# Sistema de Gestão de Franquias (API .NET 8)

API RESTful desenvolvida em C# com .NET 8 para o gerenciamento completo de redes de franquias. O sistema implementa uma arquitetura robusta baseada em separação de responsabilidades, oferecendo controle granular de acesso (RBAC) via JWT, persistência com Entity Framework Core e SQLite, além de módulos para unidades, catálogo, estoque, vendas, royalties, fornecedores, suporte e indicadores gerenciais.

---

## 🛠️ Tecnologias e Arquitetura

* **Plataforma:** .NET 8 / C#
* **Arquitetura:** ASP.NET Core Web API orientada a objetos com padrão de Repositórios e DTOs
* **Persistência:** Entity Framework Core (ORM) com banco de dados SQLite
* **Segurança:** Autenticação e Autorização baseada em tokens JWT ([Authorize], Roles)
* **Documentação:** Swagger / OpenAPI
* **Controle de Versão:** Git e GitHub

---

## 📂 Estrutura do Projeto

GestaoFranquias.Api/
├── Controllers/         # Endpoints da API (Auth, Unidades, Produtos, Vendas, etc.)
├── Entities/            # Classes de domínio e modelos do banco de dados
├── DTOs/                # Objetos de transferência de dados (entrada e saída)
├── Repositories/        # Lógica de acesso a dados e regras de negócio
├── Data/                # Contexto do EF Core (AppDbContext) e inicializador (Seed)
├── Migrations/          # Histórico de migrações estruturais do banco relacional
├── Program.cs           # Configuração de serviços, injeção de dependência e pipeline
└── README.md            # Documentação do projeto

---

## 📋 Pré-requisitos

Para executar este projeto em sua máquina local, você precisará ter instalado:
* .NET 8 SDK (https://dotnet.microsoft.com/download/dotnet/8.0)
* Um terminal compatível (PowerShell, Bash ou CMD)
* Navegador web ou ferramenta de teste de API (Swagger, Postman, Insomnia ou Bruno)

---

## ⚙️ Instruções de Execução

1. **Clone o repositório:**
   git clone https://github.com/seu-usuario/gestao-franquias-api.git
   cd gestao-franquias-api

2. **Restaure os pacotes NuGet:**
   dotnet restore

3. **Aplique as Migrations para criar o banco de dados:**
   dotnet ef database update

4. **Execute a aplicação:**
   dotnet run

5. **Acesse a interface interativa (Swagger):**
   Copie a URL exibida no terminal (ex: https://localhost:7123/swagger) e cole no seu navegador.

---

## 🔐 Perfis de Acesso e Segurança (RBAC)

A API protege suas rotas através de um sistema rígido de perfis validados via token JWT:

* **Administrador:** Possui privilégios totais na rede. Pode cadastrar novas unidades, gerenciar o catálogo global de produtos, processar e fechar o cálculo oficial de royalties e acessar todos os relatórios estratégicos.
* **Gestor:** Responsável pela administração local ou regional. Pode consultar faturamentos, acompanhar extratos de royalties, monitorar estoque crítico e gerenciar chamados da unidade.
* **Operador:** Voltado para a rotina diária de balcão. Tem permissão para registrar novas vendas, dar entrada/saída em movimentações de estoque e consultar o catálogo.

---

## 🚀 Principais Módulos e Endpoints

* **Autenticação:** POST /api/auth/login (Geração do token JWT) e POST /api/auth/registrar.
* **Unidades Franqueadas:** Gestão de filiais, CNPJ, endereços e responsáveis (/api/unidades).
* **Catálogo:** CRUD completo de produtos e serviços categorizados (/api/produtos).
* **Estoque:** Controle de saldo por unidade, movimentações e alerta preventivo de ruptura (/api/estoques).
* **Vendas:** Registro transacional de vendas com cálculo automático de totais e baixa simultânea no estoque (/api/vendas).
* **Financeiro / Royalties:** Apuração de repasses com base no faturamento e percentual contratual (/api/royalties).
* **Fornecedores:** Cadastro e homologação de parceiros da rede (/api/fornecedores).
* **Suporte:** Abertura e acompanhamento do ciclo de vida de chamados de atendimento (/api/chamados).
* **Relatórios:** Indicadores consolidados de faturamento, estoque crítico e rankings de desempenho (/api/relatorios).
