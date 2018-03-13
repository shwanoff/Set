using System;

namespace Set
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создаем множества.
            var set1 = new Set<int>()
            {
                1, 2, 3, 4, 5, 6
            };

            var set2 = new Set<int>()
            {
                4, 5, 6, 7, 8, 9
            };

            var set3 = new Set<int>()
            {
                2, 3, 4
            };

            // Выполняем операции со множествами.
            var union = Set<int>.Union(set1, set2);
            var difference = Set<int>.Difference(set1, set2);
            var intersection = Set<int>.Intersection(set1, set2);
            var subset1 = Set<int>.Subset(set3, set1);
            var subset2 = Set<int>.Subset(set3, set2);

            // Выводим исходные множества на консоль.
            PrintSet(set1, "Первое множество: ");
            PrintSet(set2, "Второе множество: ");
            PrintSet(set3, "Третье множество: ");

            // Выводим результирующие множества на консоль.
            PrintSet(union, "Объединение первого и второго множества: ");
            PrintSet(difference, "Разность первого и второго множества: ");
            PrintSet(intersection, "Пересечение первого и второго множества: ");
            
            // Выводим результаты проверки на подмножества.
            if(subset1)
            {
                Console.WriteLine("Третье множество является подмножеством первого.");
            }
            else
            {
                Console.WriteLine("Третье множество не является подмножеством первого.");
            }

            if (subset2)
            {
                Console.WriteLine("Третье множество является подмножеством второго.");
            }
            else
            {
                Console.WriteLine("Третье множество не является подмножеством второго.");
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Вывод множества на консоль.
        /// </summary>
        /// <param name="set"> Множество. </param>
        /// <param name="title"> Заголовок перед выводом множества. </param>
        private static void PrintSet(Set<int> set, string title)
        {
            Console.Write(title);
            foreach (var item in set)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
        }
    }
}
