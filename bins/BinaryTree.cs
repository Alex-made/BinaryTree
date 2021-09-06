using System;

namespace BinaryTree
{
    /// <summary>
    /// Бинарное дерево
    /// </summary>
    /// <typeparam name="T">Тип данных хранящихся в узлах</typeparam>
    public class BinaryTree<T> where T : IComparable
    {
        /// <summary>
        /// Корень бинарного дерева
        /// </summary>
        public BinaryTreeNode<T> RootNode { get; set; }

        /// <summary>
        /// Добавление нового узла в бинарное дерево
        /// </summary>
        /// <param name="node">Новый узел</param>
        /// <param name="currentNode">Текущий узел</param>
        /// <returns>Узел</returns>
        public BinaryTreeNode<T> Add(BinaryTreeNode<T> node, BinaryTreeNode<T> currentNode = null)
        {
            if (RootNode == null)
            {
                node.ParentNode = null;
                return RootNode = node;
            }

            currentNode = currentNode ?? RootNode;
            node.ParentNode = currentNode;
            int result = node.Data.CompareTo(currentNode.Data);
            return result == 0 ?
                currentNode
                : result < 0 ?
                    currentNode.LeftNode == null ?
                    currentNode.LeftNode = node
                    : Add(node, currentNode.LeftNode)
                : currentNode.RightNode == null ?
                  currentNode.RightNode = node
                  : Add(node, currentNode.RightNode);
        }

        /// <summary>
        /// Добавление нового узла в бинарное дерево
        /// </summary>
        /// <param name="node">Новый узел</param>
        /// <returns>Узел</returns>
        public BinaryTreeNode<T> Add(T node)
        {
            return Add(new BinaryTreeNode<T>(node));
        }

        /// <summary>
        /// Поиск узла в бинарном дереве
        /// </summary>
        /// <param name="node">Узел</param>
        /// <returns>Узел</returns>
        public BinaryTreeNode<T> FindNode(T node)
        {
            return FindNode(new BinaryTreeNode<T>(node));
        }

        public BinaryTreeNode<T> FindNode(BinaryTreeNode<T> node, BinaryTreeNode<T> currentNode = null)
        {
            if (RootNode == null)
            {
                return null;
            }

            var startNode = currentNode ?? this.RootNode;

            var compareIndex = node.Data.CompareTo(startNode.Data);
            return compareIndex == 0 ?  //значение искомого ключа равно корневому ключу
                startNode
                : compareIndex < 0 ?  // < 0
                    startNode.LeftNode == null ?  
                       null
                       : FindNode(node, startNode.LeftNode)

                    : startNode.RightNode == null ?  // > 0
                      null
                      : FindNode(node, startNode.RightNode);
        }

        /// <summary>
        /// Удаление узла в бинарном дереве
        /// </summary>
        /// <param name="node">Узел</param>
        public void Remove(T node)
        {
            Remove(new BinaryTreeNode<T>(node));
        }

        public void Remove(BinaryTreeNode<T> node, BinaryTreeNode<T> currentNode = null)
        {
            if (RootNode == null)
            {
                return;
            }

            if (node == null) 
                return;
            //Node здесь - это вновь созданный node, который не привязан к дереву.
            var startNode = currentNode ?? this.RootNode;
            var compareIndex = node.Data.CompareTo(startNode.Data);
            //если ключ node больше корневого узла, рекурсивно удалить K из правого поддерева Т
            if(compareIndex > 0)
            {
                Remove(node, startNode.RightNode);
            }
            //если ключ node меньше корневого узла, рекурсивно удалить K из левого поддерева Т
            if (compareIndex < 0)
            {
                Remove(node, startNode.LeftNode);
            }
            //если ключ node равен текущему узлу, то:   (в этом случае мы также можем у currentNode взять текущую сторону удаляемого узла). 
            if (compareIndex == 0)
            {
                //Если обоих детей нет, то обнуляем ссылку на него у родительского узла;
                if (currentNode.LeftNode == null && currentNode.RightNode == null)
                {
                    //var preparedForRemoveNodeSide = currentNode.NodeSide;
                    if (currentNode.NodeSide == Side.Left)
                    {
                        currentNode.ParentNode.LeftNode = null;
                    }
                    else
                    {
                        currentNode.ParentNode.RightNode = null;
                    }
                    currentNode.ParentNode = null;
                    return;
                }

                //Если одного из детей нет, то значения полей этого единственного ребёнка ставим вместо
                //соответствующих значений корневого узла, затирая его старые значения,
                //и освобождаем память, занимаемую узлом;

                //если у currentNode нет правого ребенка, тут 2 случая
                if (currentNode.LeftNode != null && currentNode.RightNode == null)
                {
                    //currentNode находится слева от родителя
                    if (currentNode.NodeSide == Side.Left)
                    {
                        currentNode.ParentNode.LeftNode = currentNode.LeftNode;
                        currentNode.LeftNode.ParentNode = currentNode.ParentNode;
                        return;
                    }
                    //currentNode находится справа от родителя
                    else
                    {
                        currentNode.ParentNode.RightNode = currentNode.LeftNode;
                        currentNode.LeftNode.ParentNode = currentNode.ParentNode;
                        return;
                    }                    
                }
                //если у currentNode нет левого ребенка, тут 2 случая
                if (currentNode.RightNode != null && currentNode.LeftNode == null)
                {
                    //currentNode находится слева от родителя
                    if (currentNode.NodeSide == Side.Left)
                    {
                        currentNode.ParentNode.LeftNode = currentNode.RightNode;
                        currentNode.RightNode.ParentNode = currentNode.ParentNode;
                        return;
                    }
                    //currentNode находится справа от родителя
                    else
                    {
                        currentNode.ParentNode.RightNode = currentNode.RightNode;
                        currentNode.RightNode.ParentNode = currentNode.ParentNode;
                        return;
                    }
                }
               
                
                //Если оба ребёнка присутствуют, то
                if (currentNode.LeftNode != null && currentNode.RightNode != null)
                {
                    //Если левый узел правого поддерева отсутствует (currentNode->right->left), то
                    //копируем правого ребенка в удаляемый, а левого потомка в левые потомки этого ребенка.
                    if (currentNode.RightNode.LeftNode == null)
                    {
                        if (currentNode.NodeSide == Side.Left)
                        {
                            currentNode.ParentNode.LeftNode = currentNode.RightNode;                          
                        }
                        else
                        {
                            currentNode.ParentNode.RightNode = currentNode.RightNode;                            
                        }
                        currentNode.RightNode.LeftNode = currentNode.LeftNode;

                        currentNode.ParentNode = null;
                        currentNode.RightNode = null;
                        currentNode.LeftNode = null;
                        return;
                    }
                    //иначе возьмём самый левый узел правого поддерева n->right,
                    //скопируем данные(кроме ссылок на дочерние элементы) из него в currentNode;
                    //Рекурсивно удалим этот самый левый узел.
                    else
                    {
                        var mostLeftNode = GetMostLeftNode(currentNode.NodeSide);
                        currentNode.Data = mostLeftNode.Data;
                        Remove(mostLeftNode, mostLeftNode);
                    }                    
                }
            }
        }

        // Pre-order обход в глубину
        public void PreOrderTraverse(BinaryTreeNode<T> currentNode, Action<BinaryTreeNode<T>> action)
        {
            if (currentNode == null) return;
            action.Invoke(currentNode);

            PreOrderTraverse(currentNode.LeftNode, action);
            PreOrderTraverse(currentNode.RightNode, action);
        }

        // Pre-order обход в глубину
        public void PreOrderTraverse(BinaryTreeNode<T> currentNode, Action<Side?, BinaryTreeNode<T>> action)
        {
            if (currentNode == null) return;
            action.Invoke(currentNode.NodeSide, currentNode);

            PreOrderTraverse(currentNode.LeftNode, action);
            PreOrderTraverse(currentNode.RightNode, action);
        }

        private BinaryTreeNode<T> GetMostLeftNode(Side? side)
        {
            //использовать переделанный вариант метода FindNode, который найдет самое маленькое значение
            // в дереве на указанной стороне, начиная с укзанного currentNode
            return FindTheLeastNodeOnSide(side);
        }

        /// <summary>
        /// Возвращает наименьший узел с указанной стороны.
        /// </summary>
        /// <param name="side"></param>
        /// <param name="startNode"></param>
        /// <returns></returns>
        private BinaryTreeNode<T> FindTheLeastNodeOnSide(Side? side, BinaryTreeNode<T> startNode = null)
        {
            if (RootNode == null)
            {
                return null;
            }

            BinaryTreeNode<T> currentNode = null;
            //если мы хотим найти наименьшее значение на левом поддереве
            if (side == Side.Left)
            {
                currentNode = startNode ?? this.RootNode.LeftNode;
            }
            //если мы хотим найти наименьшее значение на правом поддереве
            if (side == Side.Right)
            {
                currentNode = startNode ?? this.RootNode.RightNode;
            }
            if (side == null)
            {
                throw new InvalidOperationException("Не указана сторона дерева для поиска узла с наименьшим значением в нем!");
            }

            if (currentNode.LeftNode == null)
            {
                return currentNode;
            }

            return FindTheLeastNodeOnSide(Side.Left, currentNode.LeftNode);           
        }
    }
}
