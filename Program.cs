using System;
using System.Text;

namespace DIO.Series
{
    class Program
    {
        const string PRESSIONE_TECLA_MSG = "Pressione qualquer tecla para continuar...";
        const int ID_INVALIDO = -1;
        static void Main(string[] args)
        {
            MenuPrincipal();
        }

        /// <summary>
        /// Menu principal
        /// </summary>
        static void MenuPrincipal()
        {
            EOpcaoMenu opcaoMenu;

            do
            {
                EscreverCabecalho(EOpcaoMenu.Invalido);
                Console.WriteLine("Informe a opção desejada:");
                Console.WriteLine("1 - Filmes");
                Console.WriteLine("2 - Séries");
                Console.WriteLine("X - Sair");
                Console.WriteLine();
                Console.Write("> ");

                try
                {
                  opcaoMenu = (EOpcaoMenu)Console.ReadLine().ToUpper()[0];
                }
                catch(IndexOutOfRangeException)
                {
                    opcaoMenu = EOpcaoMenu.Invalido;
                }
                finally
                {
                  Console.WriteLine();
                }

                switch (opcaoMenu)
                {
                    case EOpcaoMenu.Filmes:
                    case EOpcaoMenu.Series:
                        MenuCatalogo(opcaoMenu);
                        break;
                    case EOpcaoMenu.Sair:
                        Console.WriteLine("Finalizando...");
                        break;
                    default:
                        break;
                }

            } while (opcaoMenu != EOpcaoMenu.Sair);
        }

        /// <summary>
        /// Menu Secundário
        /// </summary>
        /// <param name="opcaoMenu"></param>
        static void MenuCatalogo(EOpcaoMenu opcaoMenu)
        {
            EOpcaoCatalogo opcao;
            
            do
            {
                EscreverCabecalho(opcaoMenu);
                Console.WriteLine("Informe a opção desejada:");
                Console.WriteLine("1 - Listar disponíveis");
                Console.WriteLine("2 - Visualizar detalhes");
                Console.WriteLine("3 - Adicionar");
                Console.WriteLine("4 - Atualizar");
                Console.WriteLine("5 - Remover");
                Console.WriteLine();
                Console.WriteLine("8 - Listar removidos");
                Console.WriteLine("9 - Restaurar removido");

                Console.WriteLine("V - Voltar para menu principal");
                Console.WriteLine();
                Console.Write("> ");

               try
                {
                  opcao = (EOpcaoCatalogo)Console.ReadLine().ToUpper()[0];
                }
                catch(IndexOutOfRangeException)
                {
                    opcao = EOpcaoCatalogo.Invalido;
                }
                finally
                {
                  Console.WriteLine();
                }

                switch (opcao)
                {
                    case EOpcaoCatalogo.ListarDisponivel:
                        ListarCatalogo(opcaoMenu, EConsulta.Disponivel);
                        break;
                    case EOpcaoCatalogo.Visualizar:
                        Visualizar(opcaoMenu);
                        break;
                    case EOpcaoCatalogo.Adicionar:
                        Adicionar(opcaoMenu);
                        break;
                    case EOpcaoCatalogo.Atualizar:
                        Atualizar(opcaoMenu);
                        break;
                    case EOpcaoCatalogo.ListarRemovido:
                        ListarCatalogo(opcaoMenu, EConsulta.Removido);
                        break;
                    case EOpcaoCatalogo.Remover:
                        Remover(opcaoMenu);
                        break;
                    case EOpcaoCatalogo.Restaurar:
                        Restaurar(opcaoMenu);
                        break;
                    case EOpcaoCatalogo.Voltar:
                        Console.WriteLine("Voltando...");
                        break;
                    default:
                        break;
                }

            } while (opcao != EOpcaoCatalogo.Voltar);
        }

        /// <summary>
        /// Listagem dos títulos disponíveis
        /// </summary>
        /// <param name="opcaoMenu">Filme ou Série</param>
        static void ListarCatalogo(EOpcaoMenu opcaoMenu, EConsulta opcaoConsulta)
        {
            //Séries
            if (opcaoMenu == EOpcaoMenu.Series)
            {
                var itens = SerieCollection.Instance.ListAll(opcaoConsulta);
                if (itens.Count == 0)
                {
                    ExibirMensagem($"Nenhuma série encontrada. {PRESSIONE_TECLA_MSG}", ConsoleColor.DarkRed);
                }
                else
                {
                    if (opcaoConsulta == EConsulta.Removido)
                        ExibirTitulo("Removidos", ConsoleColor.DarkRed);

                    ExibirTitulo("Séries encontradas", ConsoleColor.DarkYellow);
                    
                    foreach(var item in itens)
                    {
                        Console.WriteLine("#ID {0}: {1}", item.Id, item.Titulo);
                    }

                    ExibirMensagem($"Fim da lista. {PRESSIONE_TECLA_MSG}", ConsoleColor.DarkYellow);
                }
            }
            //Filmes
            else if (opcaoMenu == EOpcaoMenu.Filmes)
            {
                var itens = FilmeCollection.Instance.ListAll(opcaoConsulta);
                if (itens.Count == 0)
                {
                    ExibirMensagem($"Nenhum filme encontrado. {PRESSIONE_TECLA_MSG}", ConsoleColor.DarkRed);
                }
                else
                {
                    if (opcaoConsulta == EConsulta.Removido)
                        ExibirTitulo("Removidos", ConsoleColor.DarkRed);

                    ExibirTitulo("Filmes encontrados", ConsoleColor.DarkYellow);
                    
                    foreach(var item in itens)
                    {
                        Console.WriteLine("#ID {0}: {1}", item.Id, item.Titulo);
                    }

                    ExibirMensagem($"Fim da lista. {PRESSIONE_TECLA_MSG}", ConsoleColor.DarkYellow);
                }
            }
        }

        /// <summary>
        /// Visualizar todos os dados do título
        /// </summary>
        /// <param name="opcaoMenu">Filme ou Série</param>
        static void Visualizar(EOpcaoMenu opcaoMenu)
        {
            string detalheDo = opcaoMenu == EOpcaoMenu.Filmes 
                ? "do filme" 
                : "da série";

            ExibirTitulo($"Detalhes {detalheDo}", ConsoleColor.DarkGreen);

            //Localizar registro
            Console.Write("Digite o #ID: ");
            string inputID = Console.ReadLine();
            int idAtual;

            //Tratar possível erro na conversão
            try
            {
                idAtual = int.Parse(inputID);
            }
            catch
            {
                idAtual = ID_INVALIDO;
            }

            if (!ValidarId(opcaoMenu, idAtual))
                return;

            if (opcaoMenu == EOpcaoMenu.Filmes)
            {
                string detalhes = FilmeCollection.Instance.SelectById(idAtual).ToString();
                Console.WriteLine();
                Console.WriteLine(detalhes);
            }
            else if (opcaoMenu == EOpcaoMenu.Series)
            {
                string detalhes = SerieCollection.Instance.SelectById(idAtual).ToString();
                Console.WriteLine();
                Console.WriteLine(detalhes);
            }

            ExibirMensagem(PRESSIONE_TECLA_MSG, ConsoleColor.DarkGreen);
        }

        /// <summary>
        /// Adicionar um novo título no catalogo
        /// </summary>
        /// <param name="opcaoMenu">Filme ou Serie</param>
        static void Adicionar(EOpcaoMenu opcaoMenu)
        {
            ExibirTitulo("ADICIONAR", ConsoleColor.DarkGreen);

            Console.WriteLine(ExibirGeneros());
            Console.WriteLine();

            //Genero
            Console.Write("Genero Principal: ");
            string inputGenero = Console.ReadLine();

            int generoId;
            int.TryParse(inputGenero, out generoId);

            //Titulo
            Console.Write("Título: ");
            string inputTitulo = Console.ReadLine();

            //Descrição
            Console.Write("Descrição: ");
            string inputDescricao = Console.ReadLine();

            //Classificação
            Console.Write("Classificação (somente números, 0 para todas as idades): ");
            string inputClassificacao = Console.ReadLine();

            int classificacao;
            int.TryParse(inputClassificacao, out classificacao);

            //Series
            if (opcaoMenu == EOpcaoMenu.Series)
            {
                //Temporadas
                Console.Write("Temporadas (somente números): ");
                string inputTemporadas = Console.ReadLine();

                int temporadas;
                int.TryParse(inputTemporadas, out temporadas);

                Serie novaSerie = new Serie(
                    id: SerieCollection.Instance.NextId(),
                    genero: (EGeneros)generoId,
                    titulo: inputTitulo,
                    descricao: inputDescricao,
                    classificacao: classificacao,
                    temporadas: temporadas
                );

                SerieCollection.Instance.Insert(novaSerie);
            }
            //Filmes
            else if (opcaoMenu == EOpcaoMenu.Filmes)
            {
                //Duração
                Console.Write("Duração (em minutos, somente números): ");
                string inputDuracao = Console.ReadLine();
                int duracao;
                int.TryParse(inputDuracao, out duracao);

                //Elenco
                Console.Write("Elenco (separe os nomes por vírgula): ");
                string inputElenco = Console.ReadLine();

                Filme novofilme = new Filme(
                    id: FilmeCollection.Instance.NextId(),
                    genero: (EGeneros)generoId,
                    titulo: inputTitulo,
                    descricao: inputDescricao,
                    classificacao: classificacao,
                    duracao: duracao,
                    elenco: inputElenco
                );

                FilmeCollection.Instance.Insert(novofilme);
            }

            ExibirMensagem($"Registro adicionado. {PRESSIONE_TECLA_MSG}", ConsoleColor.DarkGreen);
        }

        /// <summary>
        /// Atualizar um Filme ou Série
        /// </summary>
        /// <param name="opcaoMenu">Filme ou Série</param>
        static void Atualizar(EOpcaoMenu opcaoMenu)
        {
            ExibirTitulo("ATUALIZAR", ConsoleColor.DarkYellow);

            //Localizar registro
            Console.Write("Digite o #ID: ");
            string inputID = Console.ReadLine();
            int idAtual;

            //Tratar possível erro na conversão
            try
            {
                idAtual = int.Parse(inputID);
            }
            catch
            {
                idAtual = ID_INVALIDO;
            }

            if (!ValidarId(opcaoMenu, idAtual))
                return;

            ExibirMensagem("\t[Instrução]: Tecle <ENTER> para manter os dados atuais", ConsoleColor.Cyan, false);
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine(ExibirGeneros());
            Console.WriteLine();
            //Séries
            if (opcaoMenu == EOpcaoMenu.Series)
            {
                //Recupera registro
                var serieAtual = SerieCollection.Instance.SelectById(idAtual);

                //Gênero
                ExibirTexto($"Gênero: {EnumHelper.GetDescription(serieAtual.Genero)}", ConsoleColor.DarkGray);
                Console.Write("Genero Principal: ");
                string inputGenero = Console.ReadLine();
                int generoId;

                if (String.IsNullOrWhiteSpace(inputGenero))
                    generoId = (int)serieAtual.Genero;
                else
                    int.TryParse(inputGenero, out generoId);

                //Título
                ExibirTexto($"Título: {serieAtual.Titulo}", ConsoleColor.DarkGray);
                Console.Write("Título: ");
                string inputTitulo = Console.ReadLine();

                if (String.IsNullOrWhiteSpace(inputTitulo))
                    inputTitulo = serieAtual.Titulo;

                //Descrição
                ExibirTexto($"Descrição: {serieAtual.Descricao}", ConsoleColor.DarkGray);
                Console.Write("Descrição: ");
                string inputDescricao = Console.ReadLine();

                if (String.IsNullOrWhiteSpace(inputDescricao))
                    inputDescricao = serieAtual.Descricao;

                //Classificação
                ExibirTexto($"Classificação: {serieAtual.Classificacao}", ConsoleColor.DarkGray);
                Console.Write("Classificação (somente números, 0 para todas as idades): ");
                string inputClassificacao = Console.ReadLine();
                int classificacao;

                if (String.IsNullOrWhiteSpace(inputClassificacao))
                    classificacao = serieAtual.Classificacao;
                else
                    int.TryParse(inputClassificacao, out classificacao);

                //Temporadas
                ExibirTexto($"Temporadas: {serieAtual.Temporadas}", ConsoleColor.DarkGray);
                Console.Write("Temporadas (somente números): ");
                string inputTemporadas = Console.ReadLine();
                int temporadas;

                if (String.IsNullOrWhiteSpace(inputTemporadas))
                    temporadas = serieAtual.Temporadas;
                else
                    int.TryParse(inputTemporadas, out temporadas);

                Serie novaSerie = new Serie(
                    id: idAtual,
                    genero: (EGeneros)generoId,
                    titulo: inputTitulo,
                    descricao: inputDescricao,
                    classificacao: classificacao,
                    temporadas: temporadas
                );
                
                SerieCollection.Instance.Update(idAtual, novaSerie);
            }
            //Filmes
            else if (opcaoMenu == EOpcaoMenu.Filmes)
            {
                //Recupera registro
                var filmeAtual = FilmeCollection.Instance.SelectById(idAtual);

                //Gênero
                ExibirTexto($"Gênero: {filmeAtual.Genero}", ConsoleColor.DarkGray);
                Console.Write("Genero: ");
                string inputGenero = Console.ReadLine();
                int generoId;

                if (String.IsNullOrWhiteSpace(inputGenero))
                    generoId = (int)filmeAtual.Genero;
                else
                    int.TryParse(inputGenero, out generoId);

                //Título
                ExibirTexto($"Título: {filmeAtual.Titulo}", ConsoleColor.DarkGray);
                Console.Write("Título: ");
                string inputTitulo = Console.ReadLine();

                if (String.IsNullOrWhiteSpace(inputTitulo))
                    inputTitulo = filmeAtual.Titulo;

                //Descrição
                ExibirTexto($"Descrição: {filmeAtual.Descricao}", ConsoleColor.DarkGray);
                Console.Write("Descrição: ");
                string inputDescricao = Console.ReadLine();

                if (String.IsNullOrWhiteSpace(inputDescricao))
                    inputDescricao = filmeAtual.Descricao;

                //Classificação
                ExibirTexto($"Classificação: {filmeAtual.Classificacao}", ConsoleColor.DarkGray);
                Console.Write("Classificação (somente números, 0 para todas as idades): ");
                string inputClassificacao = Console.ReadLine();
                int classificacao;

                if (String.IsNullOrWhiteSpace(inputClassificacao))
                    classificacao = filmeAtual.Classificacao;
                else
                    int.TryParse(inputClassificacao, out classificacao);

                //Duração
                ExibirTexto($"Duração: {filmeAtual.Duracao}", ConsoleColor.DarkGray);
                Console.Write("Duração (em minutos, somente números): ");
                string inputDuracao = Console.ReadLine();
                int duracao;

                if (String.IsNullOrWhiteSpace(inputDuracao))
                    duracao = filmeAtual.Duracao;
                else
                    int.TryParse(inputDuracao, out duracao);

                //Elenco
                ExibirTexto($"Elenco: {filmeAtual.Elenco}", ConsoleColor.DarkGray);
                Console.Write("Elenco (separe os nomes por vírgula): ");
                string inputElenco = Console.ReadLine();

                if (String.IsNullOrWhiteSpace(inputElenco))
                    inputElenco = filmeAtual.Elenco;

                Filme novoFilme = new Filme(
                    id: idAtual,
                    genero: (EGeneros)generoId,
                    titulo: inputTitulo,
                    descricao: inputDescricao,
                    classificacao: classificacao,
                    duracao: duracao,
                    elenco: inputElenco
                );
                
                FilmeCollection.Instance.Update(idAtual, novoFilme);
            }

            ExibirMensagem($"Registro atualizado. {PRESSIONE_TECLA_MSG}", ConsoleColor.DarkYellow);
        }

        /// <summary>
        /// Remover titulo do catálogo disponível
        /// </summary>
        /// <param name="opcaoMenu">Filme ou Série</param>
        static void Remover(EOpcaoMenu opcaoMenu)
        {
            ExibirTitulo("REMOVER", ConsoleColor.DarkRed);

            //Id
            Console.Write("Digite o #ID: ");
            string inputID = Console.ReadLine();
            int idAtual;

            //Tratar possível erro na conversão
            try
            {
                idAtual = int.Parse(inputID);
            }
            catch
            {
                idAtual = ID_INVALIDO;
            }

            if (!ValidarId(opcaoMenu, idAtual))
                return;

            Console.WriteLine();
            Console.Write("Confirma? (S/N) > ");
            string inputConfirma = Console.ReadLine();

            //Se o usuário não confirmar retorna para o menu
            if (!inputConfirma.StartsWith("S", true, null))
                return;

            if (opcaoMenu == EOpcaoMenu.Filmes)
            {
                FilmeCollection.Instance.Delete(idAtual);
                ExibirMensagem("O filme foi removido do catálogo.", ConsoleColor.DarkGreen);
            }
            else if (opcaoMenu == EOpcaoMenu.Series)
            {
                SerieCollection.Instance.Delete(idAtual);
                ExibirMensagem("A série foi removida do catálogo.", ConsoleColor.DarkGreen);
            }
        }

        /// <summary>
        /// Restaurar titulo 
        /// </summary>
        /// <param name="opcaoMenu">Filme ou Série</param>
        static void Restaurar(EOpcaoMenu opcaoMenu)
        {
            ExibirTitulo("RESTAURAR", ConsoleColor.DarkGreen);

            //Id
            Console.Write("#Digite o #ID: ");
            string inputID = Console.ReadLine();
            int idAtual;

            //Tratar possível erro na conversão
            try
            {
                idAtual = int.Parse(inputID);
            }
            catch
            {
                idAtual = ID_INVALIDO;
            }

            if (!ValidarId(opcaoMenu, idAtual))
                return;

            if (opcaoMenu == EOpcaoMenu.Filmes)
            {
                FilmeCollection.Instance.Restore(idAtual);
                ExibirMensagem($"O filme foi adicionado no catálogo. {PRESSIONE_TECLA_MSG}", ConsoleColor.DarkGreen);
            }
            else if (opcaoMenu == EOpcaoMenu.Series)
            {
                SerieCollection.Instance.Restore(idAtual);
                ExibirMensagem($"A série foi adicionada no catálogo. {PRESSIONE_TECLA_MSG}", ConsoleColor.DarkGreen);
            }
        }

        /// <summary>
        /// Retorna a lista de gêneros do enumerator
        /// </summary>
        /// <returns><see cref="System.String">string</see> com a lista de gêneros</returns>
        static string ExibirGeneros()
        {
            StringBuilder generos = new StringBuilder("Generos: ");
            foreach(int i in Enum.GetValues(typeof(EGeneros)))
            {
                if (i == 0)
                    continue;

                generos.Append($"{i}: {EnumHelper.GetDescription((EGeneros)i)}\t");
            }
            
            return generos.ToString();
        }

        /// <summary>
        /// Cabeçalho personalizado de acordo com a opção escolhida
        /// </summary>
        /// <param name="opcao">Opção escolhida pelo usuário</param>
        static void EscreverCabecalho(EOpcaoMenu opcao)
        {
            Console.Clear();
            Console.WriteLine("┌─────┬─────┬─────────────────────────────────┐");
            Console.Write("│ ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("DIO");
            Console.ResetColor();
            Console.Write(" │ ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("MRV");
            Console.ResetColor();

            if (opcao == EOpcaoMenu.Filmes)
                Console.Write(" │ Seu catálogo de Filmes          ■\n");
            else if (opcao == EOpcaoMenu.Series)
                Console.Write(" │ Seu catálogo de Séries          ■\n");
            else
                Console.Write(" │ Seu catálogo de Filmes e Séries ■\n");

            Console.WriteLine("└─────┴─────┴─────────────────────────────────┘");
            Console.WriteLine();
        }

        /// <summary>
        /// Personaliza a exibição de mensagens para o usuário
        /// </summary>
        /// <param name="mensagem">Mensagem a ser exibida</param>
        /// <param name="cor">Cor do texto</param>
        /// <param name="readKey">Indica se o usuário deve pressionar uma tecla para continuar</param>
        static void ExibirMensagem(string mensagem, ConsoleColor cor, bool readKey = true)
        {
            Console.WriteLine();
            Console.ForegroundColor = cor;
            Console.Write(mensagem);
            Console.ResetColor();

            if (readKey)
                Console.ReadKey();
        }

        /// <summary>
        /// Personaliza e exibição de um título em CAIXA ALTA
        /// </summary>
        /// <param name="titulo">Título a ser mostrado</param>
        /// <param name="cor">Cor do texto</param>
        static void ExibirTitulo(string titulo, ConsoleColor cor)
        {
            Console.ForegroundColor = cor;
            Console.WriteLine(titulo.ToUpper());
            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// Personaliza a exibição de um texto
        /// </summary>
        /// <param name="texto">Texto a ser mostrado</param>
        /// <param name="cor">Cor do texto</param>
        /// <param name="novaLinha">Identifica se haverá quebra de linha</param>
        static void ExibirTexto(string texto, ConsoleColor cor, bool novaLinha = true)
        {
            Console.ForegroundColor = cor;

            if (novaLinha)
                Console.WriteLine(texto);
            else
                Console.Write(texto);
            
            Console.ResetColor();
        }

        /// <summary>
        /// Verifica se o ID informado existe na coleção
        /// </summary>
        /// <param name="opcaoMenu">Filme ou Série</param>
        /// <param name="idAtual">Id digitado pelo usuário</param>
        /// <returns>Verdadeiro se o Id for localizado na coleção, caso contrário retorna falso</returns>
        static bool ValidarId(EOpcaoMenu opcaoMenu, int idAtual)
        {
            //Séries
            if (opcaoMenu == EOpcaoMenu.Series)
            {
                if (!Series.SerieCollection.Instance.Exists(idAtual))
                {
                    ExibirMensagem($"Série não encontrada. {PRESSIONE_TECLA_MSG}", ConsoleColor.DarkRed);
                    return false;
                }
            }
            //Filmes
            else if (opcaoMenu == EOpcaoMenu.Filmes)
            {
                if (!Series.FilmeCollection.Instance.Exists(idAtual))
                {
                    ExibirMensagem($"Filme não encontrado. {PRESSIONE_TECLA_MSG}", ConsoleColor.DarkRed);
                    return false;
                }
            }

            return true;
        }
    }
}
