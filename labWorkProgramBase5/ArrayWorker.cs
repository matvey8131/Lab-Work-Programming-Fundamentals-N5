using System;

namespace LabWork5
{
    /// <summary>
    /// Класс для работы с массивом целых чисел
    /// </summary>
    public class ArrayWorker
    {
        private int _size;
        private int[] _array;
        private static readonly Random _rnd = new Random();

        /// <summary>
        /// Количество элементов в массиве
        /// </summary>
        public int Size
        {
            get 
            { 
                return _size;
            }
        }

        /// <summary>
        /// Конструктор без параметров. По умолчанию — 10 элементов
        /// </summary>
        public ArrayWorker()
        {
            _size = 10;
            _array = new int[_size];
        }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="size">Количество элементов в массиве</param>
        public ArrayWorker(int size)
        {
            if (size <= 0) size = 10;
            _size = size;
            _array = new int[_size];
        }

        /// <summary>
        /// Возвращает элемент массива по индексу
        /// </summary>
        /// <param name="idx">Индекс элемента</param>
        /// <returns>Элемент массива</returns>
        public int getElementByidx(int idx)
        {
            return _array[idx];
        }

        /// <summary>
        /// Заполняет массив случайными числами
        /// </summary>
        public void Fill()
        {
            for (int i = 0; i < _array.Length; i++)
                _array[i] = _rnd.Next(_array.Length);
        }

        /// <summary>
        /// Создаёт копию массива
        /// </summary>
        /// <returns>Новый ArrayWorker — копия</returns>
        public ArrayWorker Copy()
        {
            ArrayWorker copy = new ArrayWorker(_size);
            for (int i = 0; i < _size; i++)
                copy._array[i] = _array[i];
            return copy;
        }

        /// <summary>
        /// Гномья сортировка (сортирует собственный массив)
        /// </summary>
        public void GnomeSort()
        {
            int i = 1, j = 2;
            while (i < _array.Length)
            {
                if (_array[i - 1] <= _array[i]) { i = j; j++; }
                else
                {
                    int temp = _array[i - 1];
                    _array[i - 1] = _array[i];
                    _array[i] = temp;
                    i--;
                    if (i == 0) { i = j; j++; }
                }
            }
        }

        /// <summary>
        /// Сортировка Шелла (сортирует собственный массив)
        /// </summary>
        public void ShellSort()
        {
            for (int gap = _array.Length / 2; gap > 0; gap /= 2)
                for (int i = gap; i < _array.Length; i++)
                {
                    int temp = _array[i];
                    int j;
                    for (j = i; j >= gap && _array[j - gap] > temp; j -= gap)
                        _array[j] = _array[j - gap];
                    _array[j] = temp;
                }
        }

        /// <summary>
        /// Возвращает массив в виде строки (элементы через пробел)
        /// </summary>
        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < _array.Length; i++)
                result += _array[i] + " ";
            return result;
        }
    }
}