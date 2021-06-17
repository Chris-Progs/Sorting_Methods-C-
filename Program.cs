using System;
using System.Diagnostics;
using System.IO;
// Chris
// Programming 3 - AT1.3 - Sorting Methods
/* This program uses the same randomly generated array of 500,000 elements with values between 10,000 and 1,000,000
   then sorts them 50 times each whilst timing each sort method to assess the most efficient */
namespace SortingMethods
{
    class Program
    {
        static int maxSalary = 10000000;
        static int minSalary = 10000;
        static int maxStaff = 10000;
        static int[] salaryArray = new int[maxStaff];
        static int[] salaryArray2 = new int[maxStaff];
        static int[] salaryArray3 = new int[maxStaff];
        static Random ranSalary = new Random();
        static string fileName = "List7.txt";
        static string fileName2 = "List8.txt";
        static string fileName3 = "List9.txt";

        static void Main(string[] args)
        {
            Stopwatch s1 = new Stopwatch();
            for (int i = 0; i <= 50; i++)
            {
                FillArray(salaryArray);
                salaryArray2 = salaryArray;
                salaryArray3 = salaryArray;

                s1.Start();
                QuickSort(salaryArray, 0, salaryArray.Length - 1);
                s1.Stop();
                StreamWriter outputfile = new StreamWriter(Path.Combine(fileName), true);
                outputfile.WriteLine(s1.ElapsedMilliseconds);
                s1.Reset();
                outputfile.Close();
                Console.WriteLine("Value of last element Quick: " + salaryArray[maxStaff - 1]);
                Console.WriteLine("Next Sort");

                s1.Start();
                CombSort(salaryArray2);
                s1.Stop();
                outputfile = new StreamWriter(Path.Combine(fileName2), true);
                outputfile.WriteLine(s1.ElapsedMilliseconds);
                s1.Reset();
                outputfile.Close();
                Console.WriteLine("Value of last element Comb: " + salaryArray2[maxStaff - 1]);
                Console.WriteLine("Next Sort");

                s1.Start();
                MergeSort(salaryArray3);
                s1.Stop();
                outputfile = new StreamWriter(Path.Combine(fileName3), true);
                outputfile.WriteLine(s1.ElapsedMilliseconds.ToString());
                s1.Reset();
                outputfile.Close();
                Console.WriteLine("Value of last element - Merge: " + salaryArray3[maxStaff - 1]);
                Console.WriteLine("Final Sort");
            }
        }
        // Method fills an array with 500,000 elements that hold values betwen 10,000 and 1,000,000.
        static void FillArray(int[] salary)
        {
            for (int i = 0; i < maxStaff; i++)
            {
                salaryArray[i] = ranSalary.Next(minSalary, maxSalary);
            }
        }
        /*Comb sort is sorting algorithm and it is a variant of Bubble sort, 
         * the Comb Sort increases the gap used in comparisons and exchanges and improves on bubble sort.
           The idea is to eliminate small values near the end of the list as in a bubble sort these slow the sorting down. */
        static void CombSort(int[] salary)
        {
            double gap = salaryArray2.Length;
            bool swap = true;

            while (gap > 1 || swap)
            {
                gap /= 1.247330950103979;

                if (gap < 1)
                    gap = 1;

                int i = 0;
                swap = false;

                while (i + gap < salaryArray2.Length)
                {
                    int igap = i + (int)gap;

                    if (salaryArray2[i] > salaryArray2[igap])
                    {
                        int temp = salaryArray2[i];
                        salaryArray2[i] = salaryArray2[igap];
                        salaryArray2[igap] = temp;
                        swap = true;
                    }
                    ++i;
                }
            }
        }
        // This method is called by the QuickSort method.
        static int Partition(int[] array, int left, int right)
        {
            int pivot = array[left];
            while (true)
            {
                while (array[left] < pivot)
                {
                    left++;
                }
                while (array[right] > pivot)
                {
                    right--;
                }
                if (left < right)
                {
                    if (array[left] == array[right]) return right;

                    int temp = array[left];
                    array[left] = array[right];
                    array[right] = temp;
                }
                else
                {
                    return right;
                }
            }
        }
        /* Quick sort is a comparison sort, meaning that it can sort items of any type for which
           a "less-than" relation (formally, a total order) is defined. */
        static void QuickSort(int[] array, int left, int right)
        {
            int pivot;
            if (left < right)
            {
                pivot = Partition(array, left, right);
                if (pivot > 1)
                {
                    QuickSort(array, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    QuickSort(array, pivot + 1, right);
                }
            }
        }
        /* MergeSort is a divide-and-conquer algorithm that splits an array into two halves (sub arrays) 
           and recursively sorts each sub array before merging them back into one giant, sorted array. */
        static int[] MergeSort(int[] array)
        {
            int[] left;
            int[] right;
            int[] result = new int[array.Length];
            //As this is a recursive algorithm, we need to have a base case to 
            //avoid an infinite recursion and therfore a stackoverflow
            if (array.Length <= 1)
                return array;
            // The exact midpoint of our array  
            int mid = array.Length / 2;
            //Will represent our 'left' array
            left = new int[mid];

            //if array has an even number of elements, the left and right array will have the same number of 
            //elements
            if (array.Length % 2 == 0)
                right = new int[mid];
            //if array has an odd number of elements, the right array will have one more element than left
            else
                right = new int[mid + 1];
            //populate left array
            for (int i = 0; i < mid; i++)
                left[i] = array[i];
            //populate right array   
            int x = 0;
            //We start our index from the midpoint, as we have already populated the left array from 0 to 
            for (int i = mid; i < array.Length; i++)
            {
                right[x] = array[i];
                x++;
            }
            //Recursively sort the left array
            left = MergeSort(left);
            //Recursively sort the right array
            right = MergeSort(right);
            //Merge our two sorted arrays
            result = Merge(left, right);
            return array;
        }

        //This method will be responsible for combining our two sorted arrays into one giant array
        public static int[] Merge(int[] left, int[] right)
        {
            int resultLength = right.Length + left.Length;
            int[] result = new int[resultLength];
            //
            int indexLeft = 0, indexRight = 0, indexResult = 0;
            //while either array still has an element
            while (indexLeft < left.Length || indexRight < right.Length)
            {
                //if both arrays have elements  
                if (indexLeft < left.Length && indexRight < right.Length)
                {
                    //If item on left array is less than item on right array, add that item to the result array 
                    if (left[indexLeft] <= right[indexRight])
                    {
                        result[indexResult] = left[indexLeft];
                        indexLeft++;
                        indexResult++;
                    }
                    // else the item in the right array wll be added to the results array
                    else
                    {
                        result[indexResult] = right[indexRight];
                        indexRight++;
                        indexResult++;
                    }
                }
                //if only the left array still has elements, add all its items to the results array
                else if (indexLeft < left.Length)
                {
                    result[indexResult] = left[indexLeft];
                    indexLeft++;
                    indexResult++;
                }
                //if only the right array still has elements, add all its items to the results array
                else if (indexRight < right.Length)
                {
                    result[indexResult] = right[indexRight];
                    indexRight++;
                    indexResult++;
                }
            }
            return result;
        }
    }
}

