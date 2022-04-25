using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomStructures
{
    public interface Itree_AVL<T>
    {
        void Add(T value);
        bool Remove(T Value);
        //T Search(T Value,Func<T,T,bool> Filtro);
        List<T> InOrder();
    }
}
