using System.ComponentModel;

namespace DIO.Series
{
    public enum EGeneros
    {
        Nao_Informado,
        [Description("Ação")]
        Acao,
        Aventura,
        [Description("Comédia")]
        Comedia,
        Drama,
        Fantasia,
        [Description("Ficção Científica")]
        Ficcao_Cientifica,
        Policial,
        Romance,
        Suspense,
        Terror
    }

    public enum EOpcaoMenu
    {
        Filmes = '1',
        Series = '2',
        Sair = 'X',
        Invalido = 'Z'
    }

    public enum EOpcaoCatalogo
    {
        ListarDisponivel = '1',
        Visualizar = '2',
        Adicionar = '3',
        Atualizar = '4',
        Remover = '5',
        ListarRemovido = '8',
        Restaurar = '9',
        Voltar = 'V',
        Invalido = 'Z'
    }

    public enum EConsulta
    {
        Disponivel,
        Removido,
        Todos
    }
}