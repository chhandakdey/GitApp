using GitApp.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitApp.Domain
{
    public class AsciiSortingService : IAsciiSortingService
    {
        private readonly ILogger<AsciiSortingService> _logger;
        public AsciiSortingService(ILogger<AsciiSortingService> logger)
        {
            _logger = logger;
        }
        public IEnumerable<KeyValuePair<string, int>> GetSorted(string statement)
        {
            _logger.LogDebug($"AsciiSortingService => GetSorted: Statement {statement}");
            var tree = new BinarySearchTree();
            var fineStatement = UtilityServices.RemoveEscapeChars(statement);
            string[] arr = fineStatement.Split(" ");
            tree.treeins(arr);
            return tree.GetSortedList();
        }
    }

    class Node
    {
        public string key;
        public int Count;
        public Node left, right;

        public Node(string item)
        {
            key = item;
            left = right = null;
            Count = 1;
        }
    }
    
    class BinarySearchTree
    {
        // Root of BST
        Node root;
        List<KeyValuePair<string, int>> sortedList;

        public BinarySearchTree()
        {
            root = null;
            sortedList = new();
        }

        // This method mainly
        // calls insertRec()
        void insert(string key)
        {
            root = insertRec(root, key);
        }

        /* A recursive function to
          insert a new key in BST */
        Node insertRec(Node root, string key)
        {
            /* If the tree is empty,
                return a new node */
            if (root == null)
            {
                root = new Node(key);
                return root;
            }

            /* Otherwise, recur
                down the tree */
            if (UtilityServices.GetAsciiValue(key) < UtilityServices.GetAsciiValue(root.key))
                root.left = insertRec(root.left, key);
            else if (UtilityServices.GetAsciiValue(key) > UtilityServices.GetAsciiValue(root.key))
                root.right = insertRec(root.right, key);
            else
                root.Count += 1;

            /* return the root */
            return root;
        }

        public void treeins(string[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                insert(arr[i]);
            }
        }

        void inorderRec(Node root)
        {
            if (root != null)
            {
                inorderRec(root.left);
                KeyValuePair<string, int> node = new(root.key, root.Count);
                sortedList.Add(node);
                inorderRec(root.right);
            }
        }

        public IEnumerable<KeyValuePair<string, int>> GetSortedList()
        {
            inorderRec(root);
            return sortedList;
        }
    }
}
