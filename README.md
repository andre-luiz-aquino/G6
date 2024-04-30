
## Definição do Projeto 

Desenvolver uma aplicação web que emule a gestão automatizada de um fundo de investimento em ações. Essa aplicação terá a capacidade de simular diferentes estratégias de investimento e exibir os resultados de ganhos ou perdas para os usuários. Utilizando algoritmos avançados, a aplicação será capaz de analisar os ativos disponíveis e determinar a composição ótima da carteira de ações, levando em consideração diversos fatores como histórico de desempenho, risco, e potencial de retorno. Os resultados serão apresentados de forma clara e intuitiva no site, permitindo que os usuários acompanhem o desempenho de seus investimentos e tomem decisões informadas.

**
Obs.: Os projetos deverão ser congelados duas semanas antes do UCL Interdisciplinar e a execução será feita com o projeto congelado. Não serão permitidas alterações no projeto após o congelamento.
**
## Banco de Dados 

![image](https://github.com/andre-luiz-aquino/G6/assets/103086746/97da7083-0cf6-4eac-8507-5bc72ea1aab4)


### Descrição do banco de dados

O banco de dados está hospedado na Azure e utiliza o PostgreSQL. Ele foi construído usando o ORM do .NET, com migrações realizadas para definir sua estrutura. Além das tabelas padrão, há uma procedure para criar a tabela `DadosNormalizados` e uma função para recuperar os 10 melhores ativos via API.

#### Migrações

Para aplicar as migrações, utilize os seguintes comandos:

```Bash
dotnet ef migrations add CriacaoDoBanco --project G6Bank.Infra.Data -s G6 -c ApplicationDbContext --verbose

dotnet ef database update CriacaoDoBanco --project G6Bank.Infra.Data -s G6 -c ApplicationDbContext --verbose
```

## Versionamento 

A estrutura de versionamento no GitHub é implementada através de duas branches principais: a Master e a Desenvolvimento. Todas as pessoas do grupo têm acesso ao repositório e podem contribuir com o projeto. Essa abordagem traz diversos benefícios:

1. **Histórico de Alterações**: O versionamento permite acompanhar todas as mudanças feitas no código ao longo do tempo, o que é crucial para entender como o projeto evoluiu e quem contribuiu com quais alterações.
    
2. **Colaboração Eficiente**: Ao usar branches separadas para desenvolver novos recursos (Desenvolvimento) e para manter a versão estável do código (Master), facilitamos a colaboração entre os membros do grupo. Cada um pode trabalhar em sua própria funcionalidade sem interferir no trabalho dos outros.
    
3. **Testes e Experimentação**: O versionamento permite que experimentemos novas funcionalidades ou correções de bugs em um ambiente isolado (na branch de Desenvolvimento) antes de mesclá-las na versão principal do código. Isso reduz o risco de introduzir erros no código estável.
    
4. **Segurança e Recuperação**: Se algo der errado em uma nova implementação ou mudança de código, podemos facilmente voltar para uma versão estável anterior, graças ao histórico de commits e branches. Isso proporciona uma camada adicional de segurança ao desenvolvimento do projeto.
    
5. **Documentação Automatizada**: Cada commit no versionamento é acompanhado por uma mensagem descritiva que resume as alterações feitas. Essas mensagens servem como uma forma de documentação automática do desenvolvimento do projeto, facilitando o entendimento das mudanças ao longo do tempo.
    

Em suma, o versionamento no GitHub não apenas organiza e controla as diferentes versões do código, mas também promove uma colaboração mais eficiente, permite experimentação segura e fornece uma documentação automática do desenvolvimento do projeto.
## GitActions

Utilizamos GitHub Actions para automatizar nossos processos de integração contínua (CI) e entrega contínua (CD). Com ele, podemos verificar a estrutura do servidor, compilar o sistema e implantar a aplicação automaticamente sempre que houver um pull request para a branch master.

![Pasted image 20240425084210](https://github.com/andre-luiz-aquino/G6/assets/103086746/bff04fc1-1c8f-4fa1-8e6e-88f198c51f36)

## Tecnologias 

- C# e .Net
- Git e Github
- Trello
- GitActions
- PostgresSQL
- Servidor Linux
- Nginx
- Python
- PM2
## Rotas da API 

![image](https://github.com/andre-luiz-aquino/G6/assets/103086746/39945186-ae2d-4b5a-a4f1-114a77483029)

Ativos : Esta rota é responsável por atualizar informações de um ativo específico e inserir dados históricos associados a esse ativo em um banco de dados.
List : A faz uma chamada à API do Brapi para obter uma lista das cotações de ações. Ele solicita os dados à API e os deserializa em um objeto AtivoStockList, em seguida, ordena os resultados pelo volume de ações e seleciona os 50 principais para inserção no banco de dados por meio de um repositório. Finalmente, ele retorna a lista de códigos de ativos obtidos.
Todos : Essa rota executar uma função armazenada no banco de dados PostgreSQL que calcula a média móvel dos 10 melhores ativos com base no nome da carteira fornecido como parâmetro.
Montar Carteira : A rota é usada para calcular duas carteiras diferentes: uma com peso fixo e outra com peso variável, usando o método de paridade de riscos para calcular os pesos das ações da carteira.
Buscar Historico Ativos : A rota busca o historico de rendimento com base no ativo fornecido para rota.


## Entregas 
### Calendário de Entregas
| Data       | Atividade                                        |
|------------|--------------------------------------------------|
| 05/03/2024 |                                                  |
| 12/03/2024 |                                                  |
| 19/03/2024 | Apresentação da RF001.                           |
| 26/03/2024 |                                                  |
| 02/04/2024 |                                                  |
| 09/04/2024 | Apresentação da RF002.                           |
| 16/04/2024 |                                                  |
| 23/04/2024 |                                                  |
| 30/04/2024 | Apresentação da RF003.                           |
| 07/05/2024 |                                                  |
| 14/05/2024 |                                                  |
| 21/05/2024 | Apresentação da RF004.                           |
| 28/05/2024 |                                                  |
| 04/06/2024 |                                                  |
| 11/06/2024 | Apresentação para os professores e congelamento dos projetos. |
| 17/06/2024 | Início da execução para apresentação no UCL Interdisciplinar. |
| 22/06/2024 | Evento de Apresentação - UCL Interdisciplinar    |

### Trello
Segue Link para Acompanhar o Desenvolvimento do projeto:

[Trello](https://trello.com/b/JcQOoOn1/pi5)

![Pasted image 20240425092133](https://github.com/andre-luiz-aquino/G6/assets/103086746/fa13424e-1d0e-4364-b479-2ea485dff94d)
