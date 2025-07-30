# René Bizelli - Client-Server-API

## Docker

Executar o comando abaixo na raiz da _solution_:

- `docker-compose up -d`
- Na raiz da _solution_ há um arquivo do Postman com as rotas.

## Entendimento do projeto

Entidades envolvidas:

- Carts
- Sales
- Users
- Branches
- Products
- Categories

### Cart

- O _Cart_ é cacheado no Redis para acesso rápido e também armazenado no MongoDB, visando seu aproveitamento futuro.
- Uma vez que o _Cart_ se transforma em uma _Sale_, ele é eliminado de ambos os recursos.
- Não foi implementado um CRUD completo para o _Cart_ devido ao método `CreateOrUpdate`, que realiza um _upsert_, aproveitando esse recurso do MongoDB.

### Sale

- _Sales_ são armazenadas no MongoDB, com informações autocontidas, incluindo _Branch_, _User_ e produtos dos itens.
- Foi criado um campo na entidade _Sale_ chamado `SaleNumber`, um número incremental exibido ao usuário em vez do `ObjectId` do MongoDB.
- Na rota `GET` de _Sale_ é possível passar como parâmetro ambos os valores. Exemplo:

> GET /sales/6889489c3f322b1ef03b2c5e

> GET /sales/10

- A lista de _Sales_ está paginada e com filtros.

### Users

- Na documentação, o usuário é exibido com `Id` do tipo inteiro; porém, no _template_ da _solution_, essa entidade foi inicialmente implementada com `Guid`. Mantive o `Guid`.

### Branch

- É armazenado no PostgreSQL.
- O método `Get` do _Service_ é cacheado.

### Products

- É armazenado no MongoDB.
- O método `Get` do _Service_ é cacheado.
- Na lista de produtos, não incluí o campo `description`, por acreditar que não faz sentido. Ele aparece ao consultar o detalhe do pedido.
- O `Id` do produto é do tipo _string_, por conta do MongoDB.
- A lista de produtos está paginada e com filtros.

### Category

- É armazenada no PostgreSQL.
- O método `Get` do _Service_ é cacheado.

## Stack

- **MongoDB**: usei o _driver_ C# do MongoDB, em vez do Entity Framework.
- **PostgreSQL**: usei o Entity Framework para mapeamento.
- Usei _Git Flow_ para controle de _branches/features_.

## Dúvidas de negócio

- Caso um item de uma _Sale_ seja cancelado, esse cancelamento influencia na aplicação de descontos?
- O desconto é aplicado no item da _Sale_ ou no valor total da _Sale_?  
  Caso seja aplicado no valor total, os descontos são acumulativos ou é aplicado apenas o maior?

> O projeto foi desenvolvido aplicando o desconto por item.

## Evolução

- O _User_ poder ter N endereços cadastrados.
- O _Admin/Manager_ poder consultar _Sales_ por cidade/estado.
- Os tempos de _cache_ serem parametrizados.
- Os valores das regras de desconto serem parametrizados.
- Filtrar produtos por categoria na rota. Exemplo: `/products/cerveja`.
- Criar índices tanto nas tabelas do PostgreSQL quanto nas _collections_ do MongoDB.
- _Rating_ de produtos: somente recalcular se houver mudanças.
- Adicionar _logs_ e tratamentos de erros na aplicação de _Jobs_.
