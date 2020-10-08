using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Sorting2
{
    class ShuffleArray
    {
        //Shuffles the array
        public void ShuffleArray2(int[] a)
        {
            int n = a.Length;
            Random random = new Random();


            for (int i = 0; i < n; i++)
            {
                swap(a, i, i + random.Next(n - i));
            }
        }
        public void swap(int[] err, int a, int b)
        {
            int temp = err[a];
            err[a] = err[b];
            err[b] = temp;
        }
    }
}
