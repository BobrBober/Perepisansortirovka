using System;
using System.Text;

namespace ConsoleApp1
{
    public class ArraySorter
    {
        private int[] _array;
        public int Index { get; private set; }

        private ArraySorter()
        {
        }

        public ArraySorter(int[] array, int index)
        {
            _array = array ?? throw new ArgumentNullException(nameof(array));
            Index = index;
        }

        // Метод для обмена значений двух переменных
        private static void Swap(ref int firstValue, ref int secondValue)
        {
            int temp = firstValue;
            firstValue = secondValue;
            secondValue = temp;
        }

        // Расчет следующего шага для сортировки расческой
        private static int CalculateNextStep(int currentStep)
        {
            int nextStep = currentStep * 1000 / 1247;
            return nextStep > 1 ? nextStep : 1;
        }

        // Сортировка расческой
        public static int[] CombSort(int[] inputArray)
        {
            if (inputArray == null)
                throw new ArgumentNullException(nameof(inputArray));

            int[] array = (int[])inputArray.Clone();
            int arrayLength = array.Length;
            int currentStep = arrayLength - 1;

            while (currentStep > 1)
            {
                PerformCombPass(array, currentStep);
                currentStep = CalculateNextStep(currentStep);
            }

            BubbleSortFinalPass(array);

            return array;
        }

        // Проход сортировки расческой
        private static void PerformCombPass(int[] array, int step)
        {
            for (int i = 0; i + step < array.Length; i++)
            {
                if (array[i] > array[i + step])
                {
                    Swap(ref array[i], ref array[i + step]);
                }
            }
        }

        // Финальный проход пузырьковой сортировки
        private static void BubbleSortFinalPass(int[] array)
        {
            int arrayLength = array.Length;
            for (int i = 1; i < arrayLength; i++)
            {
                bool swapped = false;
                for (int j = 0; j < arrayLength - i; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        Swap(ref array[j], ref array[j + 1]);
                        swapped = true;
                    }
                }
                if (!swapped)
                    break;
            }
        }

        // Поиск значения в массиве
        public static int FindValue(int[] array, int valueToFind)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            foreach (int item in array)
            {
                if (item == valueToFind)
                    return item;
            }
            return 0;
        }

        // Сортировка Шелла
        public int[] ShellSort(int[] inputArray)
        {
            if (inputArray == null)
                throw new ArgumentNullException(nameof(inputArray));

            int[] array = (int[])inputArray.Clone();
            int size = array.Length;

            for (int gap = size / 2; gap > 0; gap /= 2)
            {
                PerformShellPass(array, size, gap);
            }
            return array;
        }

        // Проход сортировки Шелла
        private static void PerformShellPass(int[] array, int size, int gap)
        {
            for (int i = gap; i < size; i++)
            {
                int temp = array[i];
                int j = i;
                while (j >= gap && array[j - gap] > temp)
                {
                    array[j] = array[j - gap];
                    j -= gap;
                }
                array[j] = temp;
            }
        }

        // Пузырьковая сортировка по убыванию
        public static int[] BubbleSortDescending(int[] inputArray)
        {
            if (inputArray == null)
                throw new ArgumentNullException(nameof(inputArray));

            int[] array = (int[])inputArray.Clone();
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    if (array[j + 1] > array[j])
                    {
                        Swap(ref array[j + 1], ref array[j]);
                    }
                }
            }
            return array;
        }

        // Преобразование массива в строку
        public static string ArrayToString(int[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            StringBuilder result = new StringBuilder();
            foreach (int value in array)
            {
                result.Append(value).Append(" ");
            }
            return result.ToString().Trim();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Тестовый массив
            int[] testArray = { 64, 34, 25, 12, 22, 11, 90 };

            Console.WriteLine("Исходный массив: " + ArraySorter.ArrayToString(testArray));

            // Тестирование сортировки расческой
            int[] combSorted = ArraySorter.CombSort(testArray);
            Console.WriteLine("Сортировка расческой: " + ArraySorter.ArrayToString(combSorted));

            // Тестирование сортировки Шелла
            ArraySorter sorter = new ArraySorter(testArray, 0);
            int[] shellSorted = sorter.ShellSort(testArray);
            Console.WriteLine("Сортировка Шелла: " + ArraySorter.ArrayToString(shellSorted));

            // Тестирование пузырьковой сортировки по убыванию
            int[] bubbleSortedDesc = ArraySorter.BubbleSortDescending(testArray);
            Console.WriteLine("Пузырьковая сортировка по убыванию: " + ArraySorter.ArrayToString(bubbleSortedDesc));

            // Тестирование поиска значения
            int valueToFind = 22;
            int foundValue = ArraySorter.FindValue(testArray, valueToFind);
            Console.WriteLine($"Значение {valueToFind} найдено: " + (foundValue != 0 ? "Да" : "Нет"));

            Console.ReadLine(); // Ожидание ввода перед закрытием консоли
        }
    }
}