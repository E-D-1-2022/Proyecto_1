using System;
using System.Collections.Generic;
using System.Text;

namespace CustomStructures.AVL_Tree
{
    public class Node<T>
    {
        public Node<T> derecha;
        public Node<T> izquierda;

        public T Value { get; set; }

        public Node()
        {
            derecha = null;
            izquierda = null;
        }

        public int Height()
        {
            if(derecha == null && izquierda == null)
            {
                return 1;
            }

            else if(derecha == null)
            {
                return izquierda.Height() + 1;
            }
            else if(izquierda == null)
            {
                return derecha.Height() + 1;
            }

            else if(derecha.Height() > izquierda.Height())
            {
                return derecha.Height() + 1;
            }
            else
            {
                return izquierda.Height() + 1;
            }
        }

        public int esBalancedo()
        {
            if(derecha == null)
            {
                if(izquierda == null)
                {
                    return 0;
                }
                return -izquierda.Height();
            }
            else if(izquierda == null)
            {
                return derecha.Height();
            }
            return derecha.Height() - izquierda.Height();
        }

       

        public Node<T> FindReplacement()
        {
            if(izquierda == null)
            {
                return null;
            }
            Node<T> currentNode = izquierda;

            while(currentNode.derecha != null)
            {
                currentNode = currentNode.derecha;
            }
            return currentNode;
        }
    }
}
