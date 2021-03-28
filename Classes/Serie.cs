using System.Text;

namespace DIO.Series
{
    /// <summary>
    /// Classe Serie
    /// </summary>
    public class Serie : CatalogoBase
    {
        public int Temporadas { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="id">Identificar indexável</param>
        /// <param name="genero">Gênero principal</param>
        /// <param name="titulo">Título</param>
        /// <param name="descricao">Sinopse da série</param>
        /// <param name="classificacao">Idade recomendada</param>
        /// <param name="temporadas">Total de temporadas disponíveis</param>
        /// <param name="NoCatalogo">Filme disponível para visualização</param>
        public Serie (int id, EGeneros genero, string titulo, 
            string descricao, int classificacao, int temporadas)
        {
            base.Id = id;
            base.Genero = genero;
            base.Titulo = titulo;
            base.Descricao = descricao;
            base.Classificacao = classificacao > 0 ? classificacao : 0;
            this.Temporadas = temporadas > 0 ? temporadas : 0;
            base.Disponivel = true;
        }

        /// <summary>
        /// Retorna os detalhes da Série
        /// </summary>
        /// <returns>String contendo todos os dados cadastrados</returns>
        public override string ToString()
        {
            StringBuilder serie = new StringBuilder(base.ToString());

            if (this.Temporadas == 0)
                serie.AppendLine($"\tTemporada: não informada");
            else
                serie.AppendLine($"\tTemporadas: {this.Temporadas}");

            return serie.ToString();
        }
    }
}