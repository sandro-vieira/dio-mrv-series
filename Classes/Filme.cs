using System;
using System.Text;

namespace DIO.Series
{
    /// <summary>
    /// Classe de filmes
    /// </summary>
    public class Filme : CatalogoBase
    {
        public int Duracao { get; set; }

        public string Elenco { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="id">Identificar indexável</param>
        /// <param name="genero">Gênero principal</param>
        /// <param name="titulo">Título</param>
        /// <param name="descricao">Sinopse da série</param>
        /// <param name="classificacao">Idade recomendada</param>
        /// <param name="duracao">Duração do filme em minutos</param>
        /// <param name="elenco">Participantes de maior destaque</param>
        /// <param name="NoCatalogo">Filme disponível para visualização</param>
        public Filme(int id, EGeneros genero, string titulo, 
            string descricao, int classificacao, int duracao, string elenco)
            {
                base.Id = id;
                base.Genero = genero;
                base.Titulo = titulo;
                base.Descricao = descricao;
                base.Classificacao = classificacao > 0 ? classificacao : 0;
                this.Duracao = duracao > 0 ? duracao : 0;
                this.Elenco = elenco;
                base.Disponivel = true;
            }

        /// <summary>
        /// Retorna os detalhes do filme
        /// </summary>
        /// <returns>String contendo todos os dados cadastrados</returns>
        public override string ToString()
        {
            StringBuilder filme = new StringBuilder(base.ToString());

            if (this.Duracao == 0)
                filme.AppendLine($"\tDuração: não informada");
            else if (this.Duracao == 1)
                filme.AppendLine($"\tDuração: {this.Duracao} minuto");
            else
                filme.AppendLine($"\tDuração: {this.Duracao} minutos");

            string[] elenco = this.Elenco.Split(',');
            if (elenco.Length == 0)
            {
                filme.AppendLine("\tElenco: não informado");
            }
            else
            {
                filme.AppendLine("\tElenco:");
                foreach(string nome in elenco)
                    filme.AppendLine($"\t\t{nome.Trim()}");
            }

            return filme.ToString();
        }
    }
}