using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3Solution
{
    class Lab3
    {
        static int[] intArray = {17,  166,  288,  324,  531,  792,  946,  157,  276,  441, 533, 355, 228, 879, 100, 421, 23, 490, 259, 227,
                                 216, 317, 161, 4, 352, 463, 420, 513, 194, 299, 25, 32, 11, 943, 748, 336, 973, 483, 897, 396,
                                 10, 42, 334, 744, 945, 97, 47, 835, 269, 480, 651, 725, 953, 677, 112, 265, 28, 358, 119, 784,
                                 220, 62, 216, 364, 256, 117, 867, 968, 749, 586, 371, 221, 437, 374, 575, 669, 354, 678, 314, 450,
                                 808, 182, 138, 360, 585, 970, 787, 3, 889, 418, 191, 36, 193, 629, 295, 840, 339, 181, 230, 150 };


        static void Main(string[] args)
        {
            //variables
            int[] haystack = intArray;
            int[] arr = intArray;


            //a. On start, it output the unsorted array elements to the console window.
            Console.WriteLine("This is the unsorted array: ");
            PrintArray(arr);


            //b.  It prompts the user to enter an integer.

            Console.WriteLine("\nInput an integer please: ");
            int needle = int.Parse(Console.ReadLine());
            int numOfComparison = 0;

            //c. It calls the method LinearSearch to search the entered integer in the unsorted array.

            int needleIndex = LinearSearch(haystack, needle, ref numOfComparison);
            //TODO 

            //If found, it outputs the index of at which the user entered value was found and number of comparisons it used to find the index.
            //If not found, it outputs the number of comparisons it used to conclude that the user entered value is not in the array.
            if (needleIndex != -1)
            {
                Console.WriteLine("\nLinear search did " + numOfComparison + " comparisons to discover that " + needle + " is at index " + needleIndex + " in this array.");

            }
            else
            {
                Console.WriteLine("\nLinear search did " + numOfComparison + " comparisons to discover that " + needle + " is not in the unsorted array.");
            }


            Console.WriteLine("\nPress enter to Continue to bubble search!");



            //d. It calls the method BubbleSort to sort the array. After sorting, it outputs the number of swapping it performed to sort the array.
            Console.WriteLine("Bubble sort made " + BubbleSort(arr) + " swaps. \n\nThe sorted array is : ");

            //e.    It output the sorted array elements to the console window.
            PrintArray(arr);


            //f. It prompts the user to enter an integer again.
            Console.WriteLine("\nEnter another integer, please: ");
            needle = int.Parse(Console.ReadLine());
            numOfComparison = 0;


            //g.    It calls the method BinarySearch to search the entered integer in the sorted array. 
            needleIndex = BinarySearch(haystack, needle, ref numOfComparison);

            //If found, it outputs the index of at which the user entered value was found and number of comparisons it used to find the index.
            //If not found, it outputs the number of comparisons it used to conclude that the user entered value is not in the array.

            if (needleIndex != -1)
            {
                Console.WriteLine("\nThe Binary Search did " + numOfComparison + " comparisons to determine that " + needle + " was at index " + needleIndex + " in the sorted array.");
            }
            else
            {
                Console.WriteLine("\nThe Binary Search did " + numOfComparison + " comparisons to determine that " + needle + " is not in the sorted array at all.");
            }

            //Exit
            Console.WriteLine("\nHit Enter to Exit");
            string exit = Console.ReadLine();
            if (exit == "")
            {
                Console.WriteLine("Exiting Now. Goodbye.");
                Environment.Exit(0);
            }

        }
        //This method returns the index of a given needle (an int) in the haystack (an int array)
        //by using linear search. It also returns the value of the number of comparison used to 
        //find the given needle through the reference parameter numOfComparison.
        static int LinearSearch(int[] haystack, int needle, ref int numOfComparison)
        {
            int needleIndex = -1;


            //Here you search the needle in the haystack.
            int N = haystack.Length;

            for (int i = 0; i < N; i++)
            {
                numOfComparison++;
                if (haystack[i] == needle)
                {
                    return i;
                }
            }
            return needleIndex;
        }


        static int BubbleSort(int[] arr)
        {
            int numOfSwaps = 0;

            //Here you implement the bubble sort to sort the integer array arr.
            //Used the algorithm demonstrated here: https://stackoverflow.com/questions/14768010/simple-bubble-sort-c-sharp
            int temp = 0;

            for (int d = 0; d < arr.Length; d++)
            {
                for (int sort = 0; sort < arr.Length - 1; sort++)
                {
                    if (arr[sort] > arr[sort + 1])
                    {
                        temp = arr[sort + 1];
                        arr[sort + 1] = arr[sort];
                        arr[sort] = temp;
                        numOfSwaps++;
                    }
                }
            }

            //for (int i = 0; i < arr.Length; i++)
            //Console.Write(arr[i] + " ");

            Console.ReadKey();

            return numOfSwaps;
        }

        ////This method returns the index of a given needle (an int) in the haystack (an int array)
        ////by using binary search. It also returns the value of the number of comparison used to 
        ////find the given needle through the reference parameter numOfComparison.


        static int BinarySearch(int[] haystack, int needle, ref int numOfComparison)
        {
            int needleIndex = -1;

            //Here you implement the binary search to find the needle in the haystack.
            //Binary search based off in lab explanation and this site: http://anh.cs.luc.edu/170/notes/CSharpHtml/binarysearching.html

            int min = 0;
            int N = haystack.Length;
            int max = N - 1;
            do
            {
                numOfComparison++;
                int mid = (min + max) / 2;
                if (needle > haystack[mid])
                    min = mid + 1;
                else
                    max = mid - 1;
                if (haystack[mid] == needle)
                    return mid;
                //if (min > max)
                //   break;
            } while (min <= max);
            return needleIndex;


        }

        //call this method to print an integer array to the console.
        static void PrintArray(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (i != arr.Length - 1)
                {
                    Console.Write("{0}, ", arr[i]);
                }
                else
                {
                    Console.Write("{0} ", arr[i]);
                }
            }
            Console.WriteLine();
        }


    }
}
