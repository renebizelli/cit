
# René Bizelli - Client-Server-API




## Docker

Executar o comando abaixo na raiz da solution:
- docker-compose up -d 
- Na raiz da solution há um arquivo do Postman com as rotas;
## Entendimento do projeto

Entidades envolvidas:
- Carts
- Sales
- Users
- Branches
- Products
- Categories

### Cart
- O cart é cacheado no Redis para acesso rápido, e também armazenado no MongoDB, para seu aproveitamento futuro.
- Uma vez que o Cart se transforma em uma Sale, o Cart é eliminado de ambos recursos.
- Não foi implementado um CRUD completo do Cart por conta do métod CreateOrUpdate, que é um upsert, para aproveitar esse recurso do MongoDB.


### Sale
- Sales são armazenados no MongoDB, com infomações auto contidas, com Branch, User e Produtos dos items.
- Foi criado um campo na entidade Sale chamado SaleNumber, número incremental, exibido ao usuário ao invés do ObjectId do MongoDB.
- Na rota GET de Sale é possível passar como parâmetro ambos valores, exemplo:

#### GET /sales/6889489c3f322b1ef03b2c5e

#### GET /sales/10

- A lista de sales está paginada e com filtros;



### Users
- Na doc exibe o usuário com Id do tipo inteiro porém, no template da solution, essa entidade foi inicialmente implementada com Guid. Mantive o Guid.

### Branch
- É armazenado no Postgress;
- O método Get do Service é cacheado;

### Products
- É armazenado no MongoDB;
- O método Get do Service é cacheado;
- Na lista de produtos, não incluí o campo description por acreditar que não faz sentido. Ele aparece ao consultar o detalhe do pedido.
- O Id do produto é do tipo string, por conta do MongoDB;
- A lista de produtos está paginada e com filtros;

### Category
- É armazenado no Postgress;
- O método Get do Service é cacheado;

## Stack
- MongoDB: usei o Driver C# da MongoDB, ao invés e EntityFramework;
- Postgres: Usei o EntityFramework para mapeamento;
- Usei git flow para controle das branchs/features;

## Dúvidas de negócio
- Caso um item de um Sale seja cancelado, esse cancelamento influencia na aplicação de descontos?
- O desconto é aplicado no item do sale ou no valor total do sale? Caso seja aplicado no valor total, esses descontos são acumulativos ou é aplicado o maior?  O projeto foi desenvolvido aplicando o desconto por item.

## Evolução
- O user poder ter N endereços cadastrados;
- O Admin/Manager poder consultar sales por Cidade/Estado;
- Os tempos dos caches serem parametrizados;
- Os valores das regras de descontos serem parametrizadas;
- Filtrar produtos por categoria pela rota, exemplo /produts/cerveja
- Criar índices tanto nas tabelas do Postgress quando nas collections do MongoDB;
- Rating de produtos: somente recalcular se houve mudanças;
- logs e tratamentos de erros na application de Jobs;
