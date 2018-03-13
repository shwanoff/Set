using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Set
{
    /// <summary>
    /// Множество.
    /// </summary>
    /// <typeparam name="T"> Тип данных, хранимых во множестве. </typeparam>
    public class Set<T> : IEnumerable<T>
    {
        /// <summary>
        /// Коллекция хранимых данных.
        /// </summary>
        private List<T> _items = new List<T>();

        /// <summary>
        /// Количество элементов.
        /// </summary>
        public int Count => _items.Count;

        /// <summary>
        /// Добавить данные во множество.
        /// </summary>
        /// <param name="item"> Добавляемые данные. </param>
        public void Add(T item)
        {
            // Проверяем входные данные на пустоту.
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            // Множество может содержать только уникальные элементы,
            // поэтому если множество уже содержит такой элемент данных, то не добавляем его.
            if (!_items.Contains(item))
            {
                _items.Add(item);
            }
        }

        /// <summary>
        /// Удалить элемент из множества.
        /// </summary>
        /// <param name="item"> Удаляемый элемент данных. </param>
        public void Remove(T item)
        {
            // Проверяем входные данные на пустоту.
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            // Если коллекция не содержит данный элемент, то мы не можем его удалить.
            if (!_items.Contains(item))
            {
                throw new KeyNotFoundException($"Элемент {item} не найден в множестве.");
            }

            // Удаляем элемент из коллекции.
            _items.Remove(item);
        }

        /// <summary>
        /// Объединение множеств.
        /// </summary>
        /// <param name="set1"> Первое множество. </param>
        /// <param name="set2"> Второе множество. </param>
        /// <returns> Новое множество, содержащее все уникальные элементы полученных множеств. </returns>
        public static Set<T> Union(Set<T> set1, Set<T> set2)
        {
            // Проверяем входные данные на пустоту.
            if (set1 == null)
            {
                throw new ArgumentNullException(nameof(set1));
            }

            if (set2 == null)
            {
                throw new ArgumentNullException(nameof(set2));
            }

            // Результирующее множество.
            var resultSet = new Set<T>();

            // Элементы данных результирующего множества.
            var items = new List<T>();

            // Если первое входное множество содержит элементы данных,
            // то добавляем их в результирующее множество.
            if (set1._items != null && set1._items.Count > 0)
            {
                // т.к. список является ссылочным типом, 
                // то необходимо не просто передавать данные, а создавать их дубликаты.
                items.AddRange(new List<T>(set1._items));
            }

            // Если второе входное множество содержит элементы данных, 
            // то добавляем из в результирующее множество.
            if (set2._items != null && set2._items.Count > 0)
            {
                // т.к. список является ссылочным типом, 
                // то необходимо не просто передавать данные, а создавать их дубликаты.
                items.AddRange(new List<T>(set2._items));
            }

            // Удаляем все дубликаты из результирующего множества элементов данных.
            resultSet._items = items.Distinct().ToList();

            // Возвращаем результирующее множество.
            return resultSet;
        }

        /// <summary>
        /// Пересечение множеств.
        /// </summary>
        /// <param name="set1"> Первое множество. </param>
        /// <param name="set2"> Второе множество. </param>
        /// <returns> Новое множество, содержащее совпадающие элементы данных из полученных множеств. </returns>
        public static Set<T> Intersection(Set<T> set1, Set<T> set2)
        {
            // Проверяем входные данные на пустоту.
            if (set1 == null)
            {
                throw new ArgumentNullException(nameof(set1));
            }

            if (set2 == null)
            {
                throw new ArgumentNullException(nameof(set2));
            }

            // Результирующее множество.
            var resultSet = new Set<T>();

            // Выбираем множество содержащее наименьшее количество элементов.
            if (set1.Count < set2.Count)
            {
                // Первое множество меньше.
                // Проверяем все элементы выбранного множества.
                foreach (var item in set1._items)
                {
                    // Если элемент из первого множества содержится во втором множестве,
                    // то добавляем его в результирующее множество.
                    if (set2._items.Contains(item))
                    {
                        resultSet.Add(item);
                    }
                }
            }
            else
            {
                // Второе множество меньше или множества равны.
                // Проверяем все элементы выбранного множества.
                foreach (var item in set2._items)
                {
                    // Если элемент из второго множества содержится в первом множестве,
                    // то добавляем его в результирующее множество.
                    if (set1._items.Contains(item))
                    {
                        resultSet.Add(item);
                    }
                }
            }

            // Возвращаем результирующее множество.
            return resultSet;
        }

        /// <summary>
        /// Разность множеств.
        /// </summary>
        /// <param name="set1"> Первое множество. </param>
        /// <param name="set2"> Второе множество. </param>
        /// <returns> Новое множество, содержащее не совпадающие элементы данных между полученными множествами. </returns>
        public static Set<T> Difference(Set<T> set1, Set<T> set2)
        {
            // Проверяем входные данные на пустоту.
            if (set1 == null)
            {
                throw new ArgumentNullException(nameof(set1));
            }

            if (set2 == null)
            {
                throw new ArgumentNullException(nameof(set2));
            }

            // Результирующее множество.
            var resultSet = new Set<T>();

            // Проходим по всем элементам первого множества.
            foreach (var item in set1._items)
            {
                // Если элемент из первого множества не содержится во втором множестве,
                // то добавляем его в результирующее множество.
                if (!set2._items.Contains(item))
                {
                    resultSet.Add(item);
                }
            }

            // Проходим по всем элементам второго множества.
            foreach (var item in set2._items)
            {
                // Если элемент из второго множества не содержится в первом множестве,
                // то добавляем его в результирующее множество.
                if (!set1._items.Contains(item))
                {
                    resultSet.Add(item);
                }
            }

            // Удаляем все дубликаты из результирующего множества элементов данных.
            resultSet._items = resultSet._items.Distinct().ToList();

            // Возвращаем результирующее множество.
            return resultSet;
        }

        /// <summary>
        /// Подмножество.
        /// </summary>
        /// <param name="set1"> Множество, проверяемое на вхождение в другое множество. </param>
        /// <param name="set2"> Множество в которое проверяется вхождение другого множества. </param>
        /// <returns> Является ли первое множество подмножеством второго. true - является, false - не является. </returns>
        public static bool Subset(Set<T> set1, Set<T> set2)
        {
            // Проверяем входные данные на пустоту.
            if (set1 == null)
            {
                throw new ArgumentNullException(nameof(set1));
            }

            if (set2 == null)
            {
                throw new ArgumentNullException(nameof(set2));
            }

            // Перебираем элементы первого множества.
            // Если все элементы первого множества содержатся во втором,
            // то это подмножество. Возвращаем истину, иначе ложь.
            var result = set1._items.All(s => set2._items.Contains(s));
            return result;
        }

        /// <summary>
        /// Вернуть перечислитель, выполняющий перебор всех элементов множества.
        /// </summary>
        /// <returns> Перечислитель, который можно использовать для итерации по коллекции. </returns>
        public IEnumerator<T> GetEnumerator()
        {
            // Используем перечислитель списка элементов данных множества.
            return _items.GetEnumerator();
        }

        /// <summary>
        /// Вернуть перечислитель, который осуществляет итерационный переход по множеству.
        /// </summary>
        /// <returns> Объект IEnumerator, который используется для прохода по коллекции. </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            // Используем перечислитель списка элементов данных множества.
            return _items.GetEnumerator();
        }
    }
}
