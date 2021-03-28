using System.Collections.Generic;
using DIO.Series.Interfaces;

namespace DIO.Series
{
    /// <summary>
    /// Repositório de objetos
    /// </summary>
    public class FilmeCollection : ICatalogo<Filme>
    {
        #region  Singleton Object Instance

        private static FilmeCollection _instance = new FilmeCollection();
        public static FilmeCollection Instance 
        { 
            get 
            { 
                return _instance; 
            }
        }

        #endregion

        private List<Filme> _filmeList;

        /// <summary>
        /// Private Construtor
        /// </summary>
        private FilmeCollection()
        {
            this._filmeList = new List<Filme>();
        }

        #region Public Methods

        /// <summary>
        /// Remover um filme do catálogo
        /// </summary>
        /// <param name="id">Id do filme</param>
        public void Delete(int id)
        {
            if (this.Exists(id))
                _filmeList[id].Remover();
        }

        /// <summary>
        /// Restaurar um filme removido 
        /// </summary>
        /// <param name="id">Id do filme</param>
        public void Restore(int id)
        {
            if (this.Exists(id))
                _filmeList[id].Restaurar();
        }

        /// <summary>
        /// Adicionar um filme
        /// </summary>
        /// <param name="sender">Filme para adicionar na lista</param>
        public void Insert(Filme sender)
        {
            _filmeList.Add(sender);
        }

        /// <summary>
        /// Retornar os filmes
        /// </summary>
        /// <param name="consulta">Filtro para retorno</param>
        /// <returns>Retorna lista de filmes de acordo com o filtro informado</returns>
        public List<Filme> ListAll(EConsulta consulta)
        {
            switch (consulta)
            {
                case EConsulta.Disponivel:
                    return _filmeList.FindAll(p => p.Disponivel == true);
                case EConsulta.Removido:
                    return _filmeList.FindAll(p => p.Disponivel == false);
                default:
                    return _filmeList;
            }
        }

        /// <summary>
        /// Próximo Id
        /// </summary>
        /// <returns>Retorna o próximo id da lista</returns>
        public int NextId()
        {
            return _filmeList.Count;
        }

        /// <summary>
        /// Retorna o filme
        /// </summary>
        /// <param name="id">ID do filme</param>
        /// <returns>Retorna o filme do id informado</returns>
        public Filme SelectById(int id)
        {
            if (Exists(id))
                return _filmeList.Find(p => p.Id == id);
            else
                throw new KeyNotFoundException("Filme não encontrado!");
        }

        /// <summary>
        /// Atualiza o filme
        /// </summary>
        /// <param name="id">Id do filme</param>
        /// <param name="sender">Filme com os dados para atualizar</param>
        public void Update(int id, Filme sender)
        {
            if (this.Exists(id))
                _filmeList[id] = sender;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Verifica se o filme existe
        /// </summary>
        /// <param name="id">Id do filme</param>
        /// <returns>Retorna verdadeiro se o filme foi encontrado, caso contrário retorna falso</returns>
        public bool Exists(int id)
        {
            return _filmeList.Exists(p => p.Id == id);
        }

        #endregion
        
    }
}