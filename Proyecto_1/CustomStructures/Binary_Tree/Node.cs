using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomStructures.BinaryTree
{
    public class Node<T>
    {
        
        public T Value { get; set; }

        public Node<T> Izquierda { get; set; }

        public Node<T> Derecha { get; set; }

    }
}
