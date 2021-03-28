using System;
using System.Text;

namespace DIO.Series
{
    /// <summary>
    /// Classe base para Filmes e Series
    /// </summary>
    public abstract class CatalogoBase
    {
        public int Id { get; protected set; }

        public EGeneros Genero {get; protected set; }
        public int Classificacao {get; protected set; }

        public string Titulo { get; protected set; }

        public string Descricao { get; protected set; }

        public bool Disponivel { get; protected set; }

        /// <summary>
        /// Remover título do catálogo
        /// </summary>
        public void Remover()
        {
            this.Disponivel = false;
        }

        /// <summary>
        /// Adicionar título novamente no catálogo
        /// </summary>
        public void Restaurar()
        {
            this.Disponivel = true;
        }

        /// <summary>
        /// Retorna os detalhes do título
        /// </summary>
        /// <returns>String contendo todos os dados cadastrados</returns>
        public new virtual string ToString()
        {
            StringBuilder titulo = new StringBuilder();
            titulo.AppendLine($"#ID: {this.Id}");
            titulo.AppendLine($"Título: {this.Titulo}");
            titulo.AppendLine($"Descrição: {this.Descricao}");
            titulo.AppendLine($"\tGênero: {EnumHelper.GetDescription(this.Genero)}");
            
            if (this.Classificacao == 0)
                titulo.AppendLine($"\tClassificação: Livre");
            else if (this.Classificacao == 1)
                titulo.AppendLine($"\tClassificação: {this.Classificacao} ano");
            else
                titulo.AppendLine($"\tClassificação: {this.Classificacao} anos");

            return titulo.ToString();
        }
    }
}