using Fase2.src.models;

namespace Fase2.src.adts
{
    public class AVLTree
    {
        private AVLNode? Root { get; set; } = null;

        public void Insert(Repuestos data)
        {
            Root = InsertRecursively(data, Root);
        }

        private AVLNode InsertRecursively(Repuestos data, AVLNode? node)
        {
            if (node == null)
            {
                return new AVLNode(data);
            }

            if (data.Id < node.Data.Id)
            {
                // Insert in left subtree
                node.Left = InsertRecursively(data, node.Left);
                if (GetHeight(node.Left) - GetHeight(node.Right) == 2)
                {
                    if (data.Id < node.Left!.Data.Id)
                    {
                        node = RotateRight(node);
                    }
                    else
                    {
                        node = RotateLeftRight(node);
                    }
                }
            }
            if (data.Id > node.Data.Id)
            {
                // Insert in right subtree
                node.Right = InsertRecursively(data, node.Right);
                if (GetHeight(node.Right) - GetHeight(node.Left) == 2)
                {
                    if (data.Id > node.Right!.Data.Id)
                    {
                        node = RotateLeft(node);
                    }
                    else
                    {
                        node = RotateRightLeft(node);
                    }
                }
            }
            node.Height = GetMaxHeight(GetHeight(node.Left), GetHeight(node.Right)) + 1;
            return node;
        }
        private int GetHeight(AVLNode? node)
        {
            return node == null ? -1 : node.Height;
        }

        private int GetMaxHeight(int leftHeight, int rightHeight)
        {
            return leftHeight > rightHeight ? leftHeight : rightHeight;
        }

        private AVLNode RotateRight(AVLNode currentNode)
        {
            AVLNode newRoot = currentNode.Left!;
            currentNode.Left = newRoot.Right;
            newRoot.Right = currentNode;
            currentNode.Height = GetMaxHeight(GetHeight(currentNode.Left), GetHeight(currentNode.Right)) + 1;
            newRoot.Height = GetMaxHeight(GetHeight(newRoot.Left), GetHeight(newRoot.Right)) + 1;
            return newRoot;
        }

        private AVLNode RotateLeft(AVLNode currentNode)
        {
            AVLNode newRoot = currentNode.Right!;
            currentNode.Right = newRoot.Left;
            newRoot.Left = currentNode;
            currentNode.Height = GetMaxHeight(GetHeight(currentNode.Left), GetHeight(currentNode.Right)) + 1;
            newRoot.Height = GetMaxHeight(GetHeight(newRoot.Left), GetHeight(newRoot.Right)) + 1;
            return newRoot;
        }
        private AVLNode RotateLeftRight(AVLNode currentNode)
        {
            currentNode.Right = RotateLeft(currentNode.Right!);
            return RotateRight(currentNode);
        }
        private AVLNode RotateRightLeft(AVLNode currentNode)
        {
            currentNode.Left = RotateRight(currentNode.Left!);
            return RotateLeft(currentNode);
        }

        public AVLNode? Find(int id)
        {
            return FindRecursively(id, Root);
        }

        private AVLNode? FindRecursively(int id, AVLNode? node)
        {
            if (node == null)
            {
                return null;
            }
            if (id == node.Data.Id)
            {
                return node;
            }
            if (id < node.Data.Id)
            {
                return FindRecursively(id, node.Left);
            }
            return FindRecursively(id, node.Right);
        }
        public bool Search(int id)
        {
            return SearchRecursively(id, Root);
        }

        private bool SearchRecursively(int id, AVLNode? node)
        {
            if (node == null)
            {
                return false;
            }
            if (id == node.Data.Id)
            {
                return true;
            }
            if (id < node.Data.Id)
            {
                return SearchRecursively(id, node.Left);
            }
            return SearchRecursively(id, node.Right);
        }

        public bool Update(Repuestos data)
        {
            return UpdateRecursively(data, Root);
        }

        public bool UpdateRecursively(Repuestos data, AVLNode? node)
        {
            if (node == null)
            {
                return false;
            }
            if (data.Id == node.Data.Id)
            {
                node.Data = data;
                return true;
            }
            if (data.Id < node.Data.Id)
            {
                return UpdateRecursively(data, node.Left);
            }
            return UpdateRecursively(data, node.Right);
        }
    }
}
