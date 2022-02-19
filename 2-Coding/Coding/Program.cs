using System;
using System.IO;

namespace Coding
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get Multidimensional Array From File
            int[][] multiArray = fileToMultidimensionalArray();
            // If it has error from file
            if (multiArray == null)
            {
                Console.WriteLine("Some thing wrong");
            }
            else
            {
                // Get value of Multidimensional Array
                Console.WriteLine("Ma tran doc tu file :");
                for (int i = 0; i < multiArray.Length; i++)
                {
                    for (int j = 0; j < multiArray[i].Length; j++)
                    {
                        Console.Write("{0}\t", multiArray[i][j]);
                    }
                    Console.WriteLine();
                }
                // Get Single Array From Multidimensional Array
                int[] singleArr = new int[multiArray.Length * multiArray[0].Length];
                singleArr = convertMultiArrayToSingleArray(
                    singleArr, 0, 0, multiArray.Length - 1, 0, multiArray[0].Length - 1, 0, 0, "RIGHT"
                    );
                // Get value of the Single Array
                Console.WriteLine("Mang 1 chieu tu ma tran :");
                for (int i = 0; i < singleArr.Length; i++)
                {
                    Console.Write("{0}\t", singleArr[i]);
                }
            }
            Console.ReadKey(); // stop screen
        }

        // Convert multi array to single array
        private static int[] convertMultiArrayToSingleArray(
            int[] arrValue,
            int valuePosition,
            int topPosition,
            int bottomPosition,
            int leftPosition,
            int rightPosition,
            int rowIndexPosition,
            int itemPosition,
            string direction
            )
        {
            int[][] bigArr = fileToMultidimensionalArray();
            // Check Multidimensional Array (Null if cannot read the file)
            if (bigArr == null) return null;
            if (bigArr.Length == 1) return bigArr[0];
            // Check value array (Single Array)
            if (arrValue == null)
            {
                return convertMultiArrayToSingleArray(
                    new int[bigArr.Length * bigArr[0].Length], 0,
                    0, bigArr.Length - 1, 0, bigArr[0].Length - 1, 0, 0, "RIGHT");
            }
            if (arrValue.Length != bigArr.Length * bigArr[0].Length)
            {
                return convertMultiArrayToSingleArray(
                    new int[bigArr.Length * bigArr[0].Length], 0,
                    0, bigArr.Length - 1, 0, bigArr[0].Length - 1, 0, 0, "RIGHT");
            }
            // Check if loop is over
            if (valuePosition == arrValue.Length) return arrValue;
            arrValue[valuePosition] = bigArr[rowIndexPosition][itemPosition];
            // If Item is in top left
            if (rowIndexPosition == topPosition && itemPosition == leftPosition && direction == "UP")
            {
                return convertMultiArrayToSingleArray(
                    arrValue,
                    valuePosition + 1,
                    topPosition,
                    bottomPosition,
                    leftPosition,
                    rightPosition,
                    rowIndexPosition,
                    itemPosition + 1,
                    "RIGHT");
            }
            // If Item is in top right
            else if (rowIndexPosition == topPosition && itemPosition == rightPosition && direction == "RIGHT")
            {
                return convertMultiArrayToSingleArray(
                    arrValue,
                    valuePosition + 1,
                    topPosition + 1,
                    bottomPosition,
                    leftPosition,
                    rightPosition,
                    rowIndexPosition + 1,
                    itemPosition,
                    "DOWN");
            }
            // If Item is in bottom right
            else if (rowIndexPosition == bottomPosition && itemPosition == rightPosition && direction == "DOWN")
            {
                return convertMultiArrayToSingleArray(
                    arrValue,
                    valuePosition + 1,
                    topPosition,
                    bottomPosition,
                    leftPosition,
                    rightPosition - 1,
                    rowIndexPosition,
                    itemPosition - 1,
                   "LEFT");
            }
            // If Item is in bottom left
            else if (rowIndexPosition == bottomPosition && itemPosition == leftPosition && direction == "LEFT")
            {
                return convertMultiArrayToSingleArray(
                   arrValue,
                    valuePosition + 1,
                    topPosition,
                    bottomPosition - 1,
                    leftPosition,
                    rightPosition,
                    rowIndexPosition - 1,
                    itemPosition,
                   "UP");
            }
            // If Item move in top
            else if (rowIndexPosition == topPosition && direction == "RIGHT")
            {
                return convertMultiArrayToSingleArray(
                   arrValue,
                    valuePosition + 1,
                    topPosition,
                    bottomPosition,
                    leftPosition,
                    rightPosition,
                    rowIndexPosition,
                    itemPosition + 1,
                   "RIGHT");
            }
            // If Item move in right
            else if (itemPosition == rightPosition && direction == "DOWN")
            {
                return convertMultiArrayToSingleArray(
                   arrValue,
                    valuePosition + 1,
                    topPosition,
                    bottomPosition,
                    leftPosition,
                    rightPosition,
                    rowIndexPosition + 1,
                    itemPosition,
                   "DOWN");
            }
            // If Item move in bottom
            else if (rowIndexPosition == bottomPosition && direction == "LEFT")
            {
                return convertMultiArrayToSingleArray(
                   arrValue,
                    valuePosition + 1,
                    topPosition,
                    bottomPosition,
                    leftPosition,
                    rightPosition,
                    rowIndexPosition,
                    itemPosition - 1,
                   "LEFT");
            }
            // If Item move in left
            else if (itemPosition == leftPosition && direction == "UP")
            {
                return convertMultiArrayToSingleArray(
                   arrValue,
                    valuePosition + 1,
                    topPosition,
                    bottomPosition,
                    leftPosition,
                    rightPosition,
                    rowIndexPosition - 1,
                    itemPosition,
                   "UP");
            }
            return arrValue;
        }
        // Read file and convert to MultiArray
        private static int[][] fileToMultidimensionalArray()
        {
            int[][] value = new int[0][];
            int arrValuePosition = 0;
            string[] arrayString = readFile();
            if (arrayString == null) return null;
            if (arrayString.Length <= 1) return null;
            string[] arrItem;
            int arrayLength = 0;
            int itemLength = 0;
            for (int i = 0; i < arrayString.Length; i++)
            {
                arrItem = arrayString[i].Trim().Split(' ');
                // Debug code
                //foreach (string s in arrItem)
                //{
                //    Console.Write(s+"\t");
                //}
                //
                if (!checkStringIsArrayInt(arrItem)) return null;
                if (i == 0)
                {
                    if (arrItem.Length != 2) return null;
                    try
                    {
                        arrayLength = int.Parse(arrItem[0]);
                        itemLength = int.Parse(arrItem[1]);
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                    if (arrayLength < 1 ||
                        itemLength > 500 ||
                        arrayString.Length - 1 != arrayLength) return null;
                    value = new int[arrayLength][];
                }
                else
                {
                    if (arrItem.Length != itemLength) return null;
                    value[arrValuePosition] = new int[itemLength];
                    for (int j = 0; j < value[arrValuePosition].Length; j++)
                    {
                        try
                        {
                            if (int.Parse(arrItem[j]) < 0 ||
                                int.Parse(arrItem[j]) > 250000) return null;
                            value[arrValuePosition][j] = int.Parse(arrItem[j]);
                        }
                        catch (Exception ex)
                        {
                            return null;
                        }
                    }
                    arrValuePosition++;
                }
                arrItem = null;
            }
            return value;
        }
        /*
         * Function support
         */
        // Check if string is an array of integers
        private static bool checkStringIsArrayInt(string[] str)
        {
            if (str.Length == 0) return false;
            foreach (string s in str)
            {
                if (!checkInt(s)) return false;
            }
            return true;
        }
        // Get the value of file
        private static string[] readFile()
        {
            string[] fileValue = new string[0];
            try
            {
                fileValue = File.ReadAllLines("test.txt");
            }
            catch (Exception ex)
            {
                return null;
            }
            return fileValue;
        }
        // Check Integer type
        private static bool checkInt(string str)
        {
            int value = 0;
            return int.TryParse(str, out value);
        }
    }
}
