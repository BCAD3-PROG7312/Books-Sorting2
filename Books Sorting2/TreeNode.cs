using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Sorting2
{
    public class TreeNode<T>
    {
        public T Data { get; set; }
        public T Parent { get; set; }
        public List<TreeNode<T>> Children { get; set; }
    }
}
