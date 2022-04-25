using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;


namespace CustomStructures.AVL_Tree
{
    public class AVLTree<T>:Itree_AVL<T>
    {
        Func<T, T, int> CompareTo;
        Node<T> RootNode;
        public int Count;
        public AVLTree(Func<T, T, int> CompareTo)
        {
            this.CompareTo = CompareTo;
            RootNode = null;
            Count = 0;
        }
        public Node<T> FindParent(T value)
        {
            Node<T> nodoAnterior = null;
            Node<T> nodoActual = RootNode;

            while (nodoActual != null)
            {
                if (nodoActual.Value.Equals(value))
                {
                    return nodoAnterior;
                }
                else if (CompareTo(value,nodoActual.Value) > 0)
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
        public Node<T> Find(T value)
        {
            Node<T> nodoActual = RootNode;

            while (nodoActual != null)
            {
                if (nodoActual.Value.Equals(value))
                {
                    return nodoActual;
                }
                else if (CompareTo(nodoActual.Value,value) < 0)
                {
                    nodoActual = nodoActual.derecha;
                }
                else
                {
                    nodoActual = nodoActual.izquierda;
                }
            }
            return null;
        }
        private Node<T> FindParent(Node<T> objetivo)
        {
            Node<T> nodoAnterior = null;
            Node<T> nodoActual = RootNode;

            while (nodoActual != null)
            {
                if (nodoActual.Equals(objetivo))
                {
                    return nodoAnterior;
                }
                else if (CompareTo(objetivo.Value,nodoActual.Value) > 0)
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
        public void Add(T Value)
        {
            if (RootNode == null)
            {
                RootNode = new Node<T>();
                RootNode.Value = Value;
                Count++;
                return;
            }
            Count++;
            Add(Value, RootNode);
        }
        private void Add(T Value, Node<T> iterando)
        {
            bool shouldAdd = false;
            if (CompareTo(Value,iterando.Value) < 0)
            {
                if (iterando.izquierda != null)
                {
                    Add(Value, iterando.izquierda);
                }
                else
                {
                    shouldAdd = true;
                }
            }
            else if((CompareTo(Value,iterando.Value) > 0))
            {
                if (iterando.derecha != null)
                {
                    Add(Value, iterando.derecha);
                }
                else
                {
                    shouldAdd = true;
                }
            }

            if (shouldAdd)
            {
                if (
                    CompareTo(Value,iterando.Value) < 0)
                {
                    if (iterando.izquierda == null) { iterando.izquierda = new Node<T>(); }
                    iterando.izquierda.Value = Value;
                }
                else  if ((CompareTo(Value,iterando.Value) > 0))
                {
                    if (iterando.derecha == null) { iterando.derecha = new Node<T>(); }
                    iterando.derecha.Value = Value;
                }
                shouldAdd = false;
            }

            while (Math.Abs(iterando.Balance()) > 1)
            {
                if (iterando.Balance() > 1)
                {
                    LeftRotation(iterando);
                }
                else if (iterando.Balance() < 1)
                {
                    RightRotation(iterando);
                }
            }
        }
        private void LeftRotation(Node<T> targetNode)
        {
            Node<T> parentNode = FindParent(targetNode);
            Node<T> newHead = targetNode.derecha;
            Node<T> tempHolder;

            if (newHead.derecha == null && newHead.izquierda != null)
            {
                RightRotation(newHead);
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
                RootNode = newHead;
            }
            tempHolder = newHead.izquierda;

            newHead.izquierda = targetNode;
            targetNode.derecha = tempHolder;
        }
        private void RightRotation(Node<T> targetNode)
        {
            Node<T> parentNode = FindParent(targetNode);
            Node<T> newHead = targetNode.izquierda;
            Node<T> tempHolder;

            if (newHead.izquierda == null && newHead.derecha != null)
            {
                LeftRotation(newHead);
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
                RootNode = newHead;
            }
            tempHolder = newHead.derecha;

            newHead.derecha = targetNode;
            targetNode.izquierda = tempHolder;
        }
        public bool Remove(T targetValue)
        {
            if (RootNode == null)
            {
                return false;
            }

            Node<T> targetNode = Find(targetValue);
            if (targetNode == null)
            {
                return false;
            }
            Node<T> nodoActual = RootNode;

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

                    if (targetNode != RootNode)
                    {
                    //    if (targetNode.IsLessThan(parentNode))
                    //    {
                    //        parentNode.LeftChild = replacementNode;
                    //    }
                    //    else
                    //    {
                    //        parentNode.RightChild = replacementNode;
                    //    }
                    }
                    else
                    {
                        RootNode = replacementNode;
                    }
                    nodoActual = replacementNode;
                }
                else
                {
                    if (targetNode != RootNode)
                    {
                    //    if (targetNode.IsLessThan(parentNode) || targetNode.Value.Equals(parentNode.Value))
                    //    {
                    //        parentNode.LeftChild = targetNode.RightChild;
                    //    }
                    //    else
                    //    {
                    //        parentNode.RightChild = targetNode.RightChild;
                    //    }
                    }
                    else
                    {
                        RootNode = targetNode.derecha;
                    }
                }

                if (LeftMax != null)
                {
                    Delete(LeftMax, nodoActual.izquierda);
                }
            }
            //else if (nodoActual.IsLessThan(targetNode))
            //{
            //    Delete(targetNode, nodoActual.RightChild);
            //}
            else
            {
                Delete(targetNode, nodoActual.izquierda);
            }

            while (Math.Abs(nodoActual.Balance()) > 1)
            {
                if (nodoActual.Balance() > 1)
                {
                    LeftRotation(nodoActual);
                }
                else
                {
                    RightRotation(nodoActual);
                }
            }
        }
        public List<T> InOrder()
        {
            List<T> returnList = new List<T>();
            InOrder(RootNode);

            return returnList;

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
            }
        }

    }
}
