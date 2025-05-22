# API de Endereços

Este repositório contém um projeto de CRUD (Create, Read, Update, Delete) para gerenciamento de endereços, desenvolvido em **C#** e interage diretamente com uma tabela no **SQL Server** para realizar as operações básicas de cadastro de endereços.

### 🛠️ **O que foi utilizado:**
- **Linguagens:** C# e JavaScript
- **Banco de Dados:** SQL Server
- **Framework:** .NET

## 🧱 Estrutura da Tabela

A tabela utilizada no SQL Server é definida da seguinte forma:

```sql
CREATE TABLE [dbo].[Endereco] (
  [Id] VARCHAR(100) NOT NULL,
  [Cep] CHAR(8) NOT NULL,
  [Logradouro] VARCHAR(150) NOT NULL,
  [Complemento] VARCHAR(100) NOT NULL,
  [Numero] VARCHAR(20) NOT NULL,
  [Bairro] VARCHAR(50) NOT NULL,
  [Cidade] VARCHAR(30) NOT NULL,
  [UF] CHAR(2) NOT NULL,
  PRIMARY KEY CLUSTERED ([Id] ASC)
);





