using System;
using System.Collections.Generic;
using System.Text;


namespace CustomStructures.AVL_Tree
{
    /// <summary>
    /// Es un árbol avl 
    /// </summary>
    /// <typeparam name="T">Es el tipo de dato con el que vas a trabajar</typeparam>
    public class AVLTree<T> : Itree_AVL<T>
    {
        List<LogDTO> ListaLog { get; set; }
        Func<T, T, int> CompareTo;
        int ConRDerecha=0;
        int ConRIzquierda = 0;
        Node<T> Raiz;
        public int Count;
        /// <summary>
        /// Inicializa el árbol
        /// </summary>
        /// <param name="CompareTo">Debes siempre enviarle una funcion que sea igual a CompareTo para poder trabajar el arbol</param>
        public AVLTree(Func<T, T, int> CompareTo)
        {
            this.CompareTo = CompareTo;
            Raiz = null;
            Count = 0;
            ListaLog = new List<LogDTO>();
        }
        private Node<T> FindParent(T value)
        {
            Node<T> nodoAnterior = null;
            Node<T> nodoActual = Raiz;

            while (nodoActual != null)
            {
                if (nodoActual.Value.Equals(value))
                {
                    return nodoAnterior;
                }
                else if (CompareTo(value, nodoActual.Value) > 0)
                {
                    nodoAnterior = nodoActual;
                    nodoActual = nodoActual.derecha;
                }
                else
                {
                    nodoAnterior = nodoActual;
                    nodoActual = nodoActual.izquierda;
                }
            }

            return null;
        }
        /// <summary>
        /// Return the element parent of the any node
        /// </summary>
        /// <param name="value">Value serchead</param>
        /// <returns>Value euquals at the value inserted</returns>
        public T Find(T value)
        {
            Node<T> nodoActual = Raiz;

            while (nodoActual != null)
            {
                int Result = CompareTo(value, nodoActual.Value);
                if (Result < 0)
                {
                    nodoActual = nodoActual.izquierda;
                }
                else if (Result > 0)
                {
                    nodoActual = nodoActual.derecha;
                }
                else
                {
                    ListaLog.Add(new LogDTO { Descripcion = "Se realizo una busqueda en donde se encotrno el dato", FechaSuceso = DateTime.Now });
                    return nodoActual.Value;
                }

            }
            ListaLog.Add(new LogDTO { Descripcion = "Se realizo una busqueda en donde no se encotrno el dato", FechaSuceso = DateTime.Now });

            return default(T);
        }
        private Node<T> IFind(T Value)
        {
            Node<T> nodoActual = Raiz;

            while (nodoActual != null)
            {
                int result= CompareTo(Value, nodoActual.Value);
                if (result>0)
                {
                    nodoActual = nodoActual.derecha;
                }
                else if (result<0)
                {
                    nodoActual = nodoActual.izquierda;
                }
                else
                {
                   return nodoActual;
                }
            }
            return null;
        }
        private Node<T> FindParent(Node<T> objetivo)
        {
            Node<T> nodoAnterior = null;
            Node<T> nodoActual = Raiz;

            while (nodoActual != null)
            {
                if (nodoActual.Equals(objetivo))
                {
                    return nodoAnterior;
                }
                else if (CompareTo(objetivo.Value, nodoActual.Value) > 0)
                {
                    nodoAnterior = nodoActual;
                    nodoActual = nodoActual.derecha;
                }
                else
                {
                    nodoAnterior = nodoActual;
                    nodoActual = nodoActual.izquierda;
                }
            }

            return null;
        }
        /// <summary>
        /// Insert a new element in the tree , if the element exist in the tree the node update with the value
        /// </summary>
        /// <param name="Value">Value for insert</param>
        public void Add(T Value)
        {
            if (Raiz == null)
            {
                Raiz = new Node<T>();
                Raiz.Value = Value;
                Count++;
                ListaLog.Add(new LogDTO { Descripcion = "Se agrego un nuevo valor al árbol", FechaSuceso = DateTime.Now });
                return;
            }
            Add(Value, Raiz);
            ListaLog.Add(new LogDTO { Descripcion = "Se agrego un nuevo valor al árbol", FechaSuceso = DateTime.Now });
            if (ConRIzquierda > 1)
            {
                ListaLog.Add(new LogDTO { Descripcion = "Se realizo una doble rotacion hacia la izquierda", FechaSuceso = DateTime.Now });
                ConRIzquierda = 0;
            }
            else if (ConRIzquierda == 1)
            {
                ListaLog.Add(new LogDTO { Descripcion = "Se realizo una simple rotacion hacia la izquierda", FechaSuceso = DateTime.Now });
                ConRIzquierda = 0;

            }
            else if (ConRDerecha > 1)
            {
                ListaLog.Add(new LogDTO { Descripcion = "Se realizo una doble rotacion hacia la derecha", FechaSuceso = DateTime.Now });
                ConRDerecha = 0;

            }
            else if (ConRDerecha == 1)
            {
                ListaLog.Add(new LogDTO { Descripcion = "Se realizo una simple rotacion hacia la derecha", FechaSuceso = DateTime.Now });
                ConRDerecha = 0;

            }
        }
        private void Add(T Value, Node<T> iterando)
        {
            int Result = CompareTo(Value, iterando.Value);
            if (Result < 0)
            {
                if (iterando.izquierda != null)
                {
                    Add(Value, iterando.izquierda);
                }
                else
                {
                    if (iterando.izquierda == null) { iterando.izquierda = new Node<T>(); }
                    iterando.izquierda.Value = Value;
                    Count++;
                }
            }
            else if (Result > 0)
            {
                if (iterando.derecha != null)
                {
                    Add(Value, iterando.derecha);
                }
                else
                {
                    if (iterando.derecha == null) { iterando.derecha = new Node<T>(); }
                    iterando.derecha.Value = Value;
                    Count++;
                }
            }
            else {
                iterando.Value = Value;
            }

            while (Math.Abs(iterando.esBalancedo()) > 1)
            {
                if (iterando.esBalancedo() > 1)
                {
                    RotacionIzquierda(iterando);
                    ConRIzquierda++;
                }
                else if (iterando.esBalancedo() < 1)
                {
                    RotacionDerecha(iterando);
                    ConRIzquierda++;
                }
            }
           
          
        }
        /// <summary>
        /// Retutn if the element exist in the tree
        /// </summary>
        /// <param name="Value">Value search</param>
        /// <returns>if element exist return true else return false</returns>
        public bool Exists(T Value) {
            Node<T> Trabajo = Raiz;
            while (Trabajo != null) {
               int Result = CompareTo(Value, Trabajo.Value);
                if (Result < 0)
                {
                    Trabajo = Trabajo.izquierda;
                }
                else if (Result > 0)
                {
                    Trabajo = Trabajo.derecha;
                }
                else {
                    ListaLog.Add(new LogDTO { Descripcion = "Se realizo una busqueda en donde se encotrno el dato y asimismo validamos que el elemento existe", FechaSuceso = DateTime.Now });

                    return true;
                }
            }
            ListaLog.Add(new LogDTO { Descripcion = "Se realizo una busqueda en donde no se encotrno el dato y asimismo validamos que el elemento existe", FechaSuceso = DateTime.Now });

            return false;
            
        }
        private void RotacionIzquierda(Node<T> targetNode)
        {
            Node<T> parentNode = FindParent(targetNode);
            Node<T> newHead = targetNode.derecha;
            Node<T> tempHolder;

            if (newHead.derecha == null && newHead.izquierda != null)
            {
                RotacionDerecha(newHead);
                newHead = targetNode.derecha;
            }

            if (parentNode != null)
            {
                if (parentNode.derecha == targetNode)
                {
                    parentNode.derecha = newHead;
                }
                else
                {
                    parentNode.izquierda = newHead;
                }
            }
            else
            {
                Raiz = newHead;
            }
            tempHolder = newHead.izquierda;

            newHead.izquierda = targetNode;
            targetNode.derecha = tempHolder;
        }
        private void RotacionDerecha(Node<T> targetNode)
        {
            Node<T> parentNode = FindParent(targetNode);
            Node<T> newHead = targetNode.izquierda;
            Node<T> tempHolder;

            if (newHead.izquierda == null && newHead.derecha != null)
            {
                RotacionIzquierda(newHead);
                newHead = targetNode.izquierda;
            }

            if (parentNode != null)
            {
                if (parentNode.izquierda == targetNode)
                {
                    parentNode.izquierda = newHead;
                }
                else
                {
                    parentNode.derecha = newHead;
                }
            }
            else
            {
                Raiz = newHead;
            }
            tempHolder = newHead.derecha;

            newHead.derecha = targetNode;
            targetNode.izquierda = tempHolder;
        }
        /// <summary>
        /// Elimina un elemento del arbol
        /// </summary>
        /// <param name="targetValue">Valor a eliminar</param>
        /// <returns></returns>
        public bool Remove(T targetValue)
        {
            if (Raiz == null)
            {
                return false;
            }

            Node<T> targetNode = IFind(targetValue);
            if (targetNode == null)
            {
                return false;
            }
            Node<T> nodoActual = Raiz;

            Count--;
            Delete(targetNode, nodoActual);
            return true;
        }
        private void Delete(Node<T> targetNode, Node<T> nodoActual)
        {
            if (targetNode == nodoActual)
            {
                Node<T> LeftMax = targetNode.FindReplacement();
                Node<T> parentNode = FindParent(targetNode);

                if (LeftMax != null)
                {
                    Node<T> replacementNode = new Node<T>();
                    replacementNode.Value = LeftMax.Value;
                    replacementNode.izquierda = targetNode.izquierda;
                    replacementNode.derecha = targetNode.derecha;

                    if (targetNode != Raiz)
                    {
                        if (CompareTo(targetNode.Value, parentNode.Value) < 0)
                        {
                            parentNode.izquierda = replacementNode;
                        }
                        else if(CompareTo(targetNode.Value,parentNode.Value)>0)
                        {
                            parentNode.derecha = replacementNode;
                        }
                    }
                    else
                    {
                        Raiz = replacementNode;
                    }
                    nodoActual = replacementNode;
                }
                else
                {
                    if (targetNode != Raiz)
                    {
                        if (CompareTo(targetNode.Value, parentNode.Value) < 0 || targetNode.Value.Equals(parentNode.Value))
                        {
                            parentNode.izquierda = targetNode.derecha;
                        }
                        else if (CompareTo(targetNode.Value, parentNode.Value)> 0)
                        {
                            parentNode.derecha = targetNode.derecha;
                        }
                    }
                    else
                    {
                        Raiz = targetNode.derecha;
                    }
                }

                if (LeftMax != null)
                {
                    Delete(LeftMax, nodoActual.izquierda);
                }
            }
            else if (CompareTo(nodoActual.Value, targetNode.Value) < 0)
            {
                Delete(targetNode, nodoActual.derecha);
            }
            else
            {
                Delete(targetNode, nodoActual.izquierda);
            }

            while (Math.Abs(nodoActual.esBalancedo()) > 1)
            {
                if (nodoActual.esBalancedo() > 1)
                {
                    RotacionIzquierda(nodoActual);
                }
                else
                {
                    RotacionDerecha(nodoActual);
                }
            }
            if (ConRIzquierda > 1)
            {
                ListaLog.Add(new LogDTO { Descripcion = "Se realizo una doble rotacion hacia la izquierda", FechaSuceso = DateTime.Now });
            }
            else if (ConRIzquierda == 1)
            {
                ListaLog.Add(new LogDTO { Descripcion = "Se realizo una simple rotacion hacia la izquierda", FechaSuceso = DateTime.Now });

            }
            else if (ConRDerecha > 1)
            {
                ListaLog.Add(new LogDTO { Descripcion = "Se realizo una doble rotacion hacia la derecha", FechaSuceso = DateTime.Now });

            }
            else if (ConRDerecha == 1)
            {
                ListaLog.Add(new LogDTO { Descripcion = "Se realizo una simple rotacion hacia la derecha", FechaSuceso = DateTime.Now });

            }
            ListaLog.Add(new LogDTO { Descripcion = "Se realizo una eliminación", FechaSuceso = DateTime.Now });

        }
        /// <summary>
        /// Recorrido in order de los arboles 
        /// </summary>
        /// <returns>Una lista con los datos del arbol, esta lista es ordenada</returns>
        List<T> returnList = new List<T>();
        public List<T> InOrder()
        {
            returnList.Clear();
            if (Raiz == null) {
                return returnList;
            }

            InOrder(Raiz);

            return returnList;
        }
        void InOrder(Node<T> startingNode)
        {
            if (startingNode.izquierda != null)
            {
                InOrder(startingNode.izquierda);
            }
            returnList.Add(startingNode.Value);
            if (startingNode.derecha != null)
            {
                InOrder(startingNode.derecha);
            }
            ListaLog.Add(new LogDTO { Descripcion = "Se realizo el recorrido inorder", FechaSuceso = DateTime.Now });

        }

        public byte[] GuardarLog() {
            if (ListaLog.Count > 0) {
                string Texto = "";
                for (int x = 0; x < ListaLog.Count; x++)
                {
                    Texto += "Descripción: " + ListaLog[x].Descripcion + " Fecha suceso: " + ListaLog[x].FechaSuceso + Environment.NewLine;
                }
                return Encoding.ASCII.GetBytes(Texto);
            }
            return Encoding.ASCII.GetBytes("No se ah realizado alguna accion para poder descargar el archivo");
        }

    }
}
