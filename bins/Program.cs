﻿using System;

namespace BinaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            //создаем дерево с простым хранимым типом int
            //      12
            //      /\
            //    2   14
            //   /    /\
            //  1   13  16
            //          /
            //        15
            var tree = new BinaryTree<int>();
            //добавляем первый узел
            tree.Add(12);
            //добавляем правого потомка для узла 12
            tree.Add(14);
            //добавляем левого потомка для узла 12
            tree.Add(2);
            //добавляем левого потомка для узла 2
            tree.Add(1);
            //добавляем левого потомка для узла 14
            tree.Add(13);
            //добавляем правого потомка для узла 14
            tree.Add(16);
            //добавляем левого потомка для узла 16
            tree.Add(15);

            var node1 = tree.FindNode(new BinaryTreeNode<int>(12));
            var node2 = tree.FindNode(new BinaryTreeNode<int>(14));
            var node3 = tree.FindNode(new BinaryTreeNode<int>(2));
            var node4 = tree.FindNode(new BinaryTreeNode<int>(13));
            var node5 = tree.FindNode(new BinaryTreeNode<int>(16));
            var node6 = tree.FindNode(new BinaryTreeNode<int>(15));
            var nullNode = tree.FindNode(new BinaryTreeNode<int>(15));

            //MY TODO переделать все в тесты - после удаления найти все узлы и проверит, что они не содержат удаленный
            //удалить 15 - узел без детей
            tree.Remove(15); var removedNode15 = tree.FindNode(15);
            //удалить 2 - потомка с одним ребенком
            tree.Remove(2); var removedNode2 = tree.FindNode(2);
            //удалить 14 - потомка с двумя детьми без левого потомка у потомка текущего узла (без числа 15)
            tree.Remove(14); var removedNode14 = tree.FindNode(14);


            //создаем дерево для проверки удаления числа 14
            //      12
            //      /\
            //    2   14
            //   /    /\
            //  1   13  16
            //          /\
            //        15  17
            tree = new BinaryTree<int>();
            //добавляем первый узел
            tree.Add(12);
            //добавляем правого потомка для узла 12
            tree.Add(14);
            //добавляем левого потомка для узла 12
            tree.Add(2);
            //добавляем левого потомка для узла 2
            tree.Add(1);
            //добавляем левого потомка для узла 14
            tree.Add(13);
            //добавляем правого потомка для узла 14
            tree.Add(16);
            //добавляем левого потомка для узла 16
            tree.Add(15);
            //добавляем правого потомка для узла 16
            tree.Add(17);

            //удалить 14 - потомка с двумя детьми
            tree.Remove(14); removedNode14 = tree.FindNode(14);

            //создаем дерево для проверки удаления числа 14
            //      12
            //      /\
            //     7  14
            //    /\   
            //   5  10  
            //      /\
            //     8  11
            tree = new BinaryTree<int>();
            //добавляем первый узел
            tree.Add(12);
            //добавляем правого потомка для узла 12
            tree.Add(14);
            //добавляем левого потомка для узла 12
            tree.Add(7);
            //добавляем левого потомка для узла 7
            tree.Add(5);
            //добавляем правого потомка для узла 7
            tree.Add(10);
            //добавляем правого потомка для узла 10
            tree.Add(11);
            //добавляем левого потомка для узла 10
            tree.Add(8);
            
            //удалить 7 - потомка с двумя детьми
            tree.Remove(7); removedNode14 = tree.FindNode(7);
        }
    }
}
