using System.Collections.Generic;
using DIO.Series.Interfaces;

namespace DIO.Series
{
    /// <summary>
    /// Repositório de objetos
    /// </summary>
    public class SerieCollection : ICatalogo<Serie>
    {
        #region  Singleton Object Instance

        private static SerieCollection _instance = new SerieCollection();
        public static SerieCollection Instance 
        { 
            get 
            { 
                return _instance; 
            }
        }

        #endregion

        private List<Serie> _serieList;

        /// <summary>
        /// Private Construtor
        /// </summary>
        private SerieCollection()
        {
            this._serieList = new List<Serie>();
        }

        #region Public Methods

        /// <summary>
        /// Remover uma série do catálogo
        /// </summary>
        /// <param name="id">Id da série</param>
        public void Delete(int id)
        {
            if (this.Exists(id))
                _serieList[id].Remover();
        }

        /// <summary>
        /// Restaurar uma série removido 
        /// </summary>
        /// <param name="id">Id da série</param>
        public void Restore(int id)
        {
            if (this.Exists(id))
                _serieList[id].Restaurar();
        }

        /// <summary>
        /// Adicionar uma série
        /// </summary>
        /// <param name="sender">Série para adicionar na lista</param>
        public void Insert(Serie sender)
        {
            _serieList.Add(sender);
        }

        /// <summary>
        /// Retornar as séries
        /// </summary>
        /// <param name="consulta">Filtro para retorno</param>
        /// <returns>Retorna lista de séries de acordo com o filtro informado</returns>
        public List<Serie> ListAll(EConsulta consulta)
        {
            switch (consulta)
            {
                case EConsulta.Disponivel:
                    return _serieList.FindAll(p => p.Disponivel == true);
                case EConsulta.Removido:
                    return _serieList.FindAll(p => p.Disponivel == false);
                default:
                    return _serieList;
            }
        }

        /// <summary>
        /// Próximo Id
        /// </summary>
        /// <returns>Retorna o próximo id da lista</returns>
        public int NextId()
        {
            return _serieList.Count;
        }

        /// <summary>
        /// Retorna a série
        /// </summary>
        /// <param name="id">ID da série</param>
        /// <returns>Retorna a série do id informado</returns>
        public Serie SelectById(int id)
        {
            if (Exists(id))
                return _serieList.Find(p => p.Id == id);
            else
                throw new KeyNotFoundException("Série não encontrada!");
        }

        /// <summary>
        /// Atualiza a série
        /// </summary>
        /// <param name="id">Id da série</param>
        /// <param name="sender">Série com os dados para atualizar</param>
        public void Update(int id, Serie sender)
        {
            if (this.Exists(id))
                _serieList[id] = sender;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Verifica se a série existe
        /// </summary>
        /// <param name="id">Id da série</param>
        /// <returns>Retorna verdadeiro se a série foi encontrada, caso contrário retorna falso</returns>
        public bool Exists(int id)
        {
            return _serieList.Exists(p => p.Id == id);
        }

        #endregion
    }
}