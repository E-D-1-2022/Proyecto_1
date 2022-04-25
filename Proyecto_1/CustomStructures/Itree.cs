using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomStructures
{
    public interface Itree<T>
    {
        void Add(T value);
        void Remove(T Value);
        T Search(T Value,Func<T,T,bool> Filtro);
        List<T> inorder();
    }
}
