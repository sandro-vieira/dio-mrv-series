using System.Collections.Generic;

namespace DIO.Series.Interfaces
{
    public interface ICatalogo<T>
    {
         List<T> ListAll(EConsulta consulta);
         T SelectById(int id);
         void Insert(T sender);
         void Delete(int id);
         void Restore(int id);
         void Update(int id, T sender);
         int NextId();
         bool Exists(int id);
    }
}