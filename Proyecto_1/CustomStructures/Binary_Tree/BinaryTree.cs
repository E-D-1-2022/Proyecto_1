using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CustomStructures.BinaryTree
{
   public class BinaryTree<T>:Itree<T>
    {
        Func<T, T, int> Comparer;
        private int contador=0;
        private int peso = 0;
        private Node<T> Raiz = null;

        public BinaryTree(Func<T, T, int> comparer) { this.Comparer = comparer; }
        public int Altura() {
            int cont1 = 0;
            int cont2 = 0;
            Node<T> Trabajo = new Node<T>();
            Trabajo = Raiz;

            while (Trabajo.Izquierda != null) {
                Trabajo = Trabajo.Izquierda;
                cont1++;
            }
            Trabajo = Raiz;
            while (Trabajo.Derecha != null)
            {
                Trabajo = Trabajo.Derecha;
                cont2++;
            }

            if (cont1 >= cont2)
            {
                return cont1;
            }
            else {
                return cont2;
            }

        }
        public void Add(T value)
        {
            Insert(value,  null);
            contador = 0;
        }
        private T Insert(T Value, Node<T> Iterando)
        {
            if (Raiz == null)
            {
                Raiz = new Node<T>();
                Raiz.Value = Value;
                peso++;
                return Raiz.Value;
            }
            else
            {
                if (Iterando == null)
                {
                    Iterando = new Node<T>();
                    if (contador == 0)
                    {
                        Iterando = Raiz;
                        contador++;
                    }
                }
            }
            int result = Comparer( Value, Iterando.Value);
            if (result < 0)
            {
                if (Iterando.Izquierda == null)
                {
                    Iterando.Izquierda = new Node<T>();
                    Iterando.Izquierda.Value = Value;
                    peso++;
                    return Value;
                }
                else {
                    return Insert(Value, Iterando.Izquierda);
                }
            }
            else if (result > 0)
            {
                if (Iterando.Derecha == null)
                {
                    Iterando.Derecha = new Node<T>();
                    Iterando.Derecha.Value = Value;
                    peso++;
                    return Value;
                }
                else
                {
                    return Insert(Value, Iterando.Derecha);
                }
            }
                return Iterando.Value;
           

        }
        protected T GetMinimun()
        {
            Node<T> trabajo = Raiz;
            while (trabajo.Izquierda != null)
            {
                trabajo = trabajo.Izquierda;
            }
            return trabajo.Value;

        }
        protected Node<T> GetMinimun(string tipo)
        {
            Node<T> trabajo = Raiz;
            while (trabajo.Izquierda != null)
            {
                trabajo = trabajo.Izquierda;
            }
            return trabajo;

        }
        protected T GetMaximun()
        {
            Node<T> trabajo = Raiz;
            while (trabajo.Derecha != null)
            {
                trabajo = trabajo.Derecha;
            }
            return trabajo.Value;

        }

        protected Node<T> GetMaximun(string tipo)
        {
            Node<T> trabajo = Raiz;
            while (trabajo.Derecha != null)
            {
                trabajo = trabajo.Derecha;
            }
            return trabajo;

        }
        public void Remove(T Value)
        {
            internalDelete(Value, Raiz);
        }
        private T internalDelete(T Value, Node<T> Iterando)
        {
            if (Iterando == null)
            {
                Iterando = new Node<T>();
                return Iterando.Value;
            }
            else {
                int result = Comparer(Value, Iterando.Value);
                if (result < 0)
                {
                    Iterando.Izquierda.Value = internalDelete(Value, Iterando.Izquierda);
                }
                else if (result > 0)
                {
                    Iterando.Derecha.Value = internalDelete(Value, Iterando.Derecha);
                }
                else {
                    if (Iterando.Izquierda == null && Iterando.Derecha == null)
                    {
                        Iterando = null;
                        return default;
                    }
                    else if (Iterando.Izquierda == null)
                    {
                        Node<T> Padre = BuscarPadre(Iterando.Derecha.Value, Raiz);
                        Padre.Derecha = Iterando.Derecha;
                        return Iterando.Value;
                    }
                    else if (Iterando.Derecha == null)
                    {
                        Node<T> Padre = BuscarPadre(Iterando.Izquierda.Value, Raiz);
                        Padre.Izquierda = Iterando.Izquierda;
                        return Iterando.Value;
                    }
                    else {
                        Node<T> min = GetMinimun("Node");
                        Iterando.Value = min.Value;
                        Iterando.Derecha.Value = internalDelete(min.Value, Iterando.Derecha);
                    }
                }
                return Iterando.Value;
            }

        }
        private Node<T> BuscarPadre(T value, Node<T> node) {
            if (node == null) {
                return null;
            }
            if (node.Izquierda != null) {
                if (node.Izquierda.Value.Equals(value)) {
                    return node;
                }
            }
            if (node.Derecha != null)
            {
                if (node.Derecha.Value.Equals(value))
                {
                    return node;
                }
            }
            int result = Comparer(node.Value, value);
            if (node.Izquierda!=null&&result < 0)
            {
                node.Izquierda.Value = internalDelete(value, node.Izquierda);
            }
            else if (node.Derecha!=null&&result > 0)
            {
                node.Derecha.Value = internalDelete(value, node.Derecha);
            }
            return node;
        }

        List<T> ArbolOrdenado = new List<T>();
        public T Search(T Value, Func<T, T, bool> Filtro)
        {
            ///Lista enviada
        
            foreach (var item in ArbolOrdenado) {
                if (Filtro(Value, item)) {
                    return item;
                 }
            }
            return default;
        }
        List<T> Data = new List<T>();
        public List<T> inorder() {
            return InternalInorder(Raiz);
        }

        private List<T> InternalInorder(Node<T> Node)
        {
            if (Node != null) {
                InternalInorder(Node.Izquierda);
                Data.Add(Node.Value);
                InternalInorder(Node.Derecha);
            }
            return Data;
        }
    }
}
