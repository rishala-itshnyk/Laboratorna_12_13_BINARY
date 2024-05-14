using System;
using System.Collections.Generic;
using System.Linq;

// Вузол префіксного дерева
public class TrieNode
{
    public Dictionary<char, TrieNode> Children { get; set; }
    public bool IsEndOfWord { get; set; }
    public string Surname { get; set; }
    public int[] Grades { get; set; }

    // Конструктор класу TrieNode
    public TrieNode()
    {
        Children = new Dictionary<char, TrieNode>();
        IsEndOfWord = false;
        Surname = null;
        Grades = new int[3]; // Assuming three grades for simplicity
    }
}

// Префіксне дерево
public class Trie
{
    private TrieNode root;

    public Trie()
    {
        root = new TrieNode();
    }

    // Додавання студента в дерево
    public void Insert(string surname, int[] grades)
    {
        TrieNode current = root;
        foreach (char c in surname)
        {
            if (!current.Children.ContainsKey(c))
                current.Children[c] = new TrieNode();
            current = current.Children[c];
        }
        current.IsEndOfWord = true;
        current.Surname = surname;
        Array.Copy(grades, current.Grades, grades.Length);
    }

    // Пошук студента в дереві
    public bool Search(string surname, out int[] grades)
    {
        TrieNode current = root;
        grades = null;
        foreach (char c in surname)
        {
            if (!current.Children.ContainsKey(c))
                return false;
            current = current.Children[c];
        }
        if (current.IsEndOfWord)
        {
            grades = current.Grades;
            return true;
        }
        return false;
    }

    // Видалення студента з дерева
    public bool Delete(string surname)
    {
        TrieNode current = root;
        Stack<TrieNode> nodes = new Stack<TrieNode>();
        foreach (char c in surname)
        {
            if (!current.Children.ContainsKey(c))
                return false;
            nodes.Push(current);
            current = current.Children[c];
        }
        if (!current.IsEndOfWord)
            return false;
        current.IsEndOfWord = false;
        current.Surname = null;
        Array.Clear(current.Grades, 0, current.Grades.Length);

        // Перевірка на наявність дочірних вузлів, якщо немає, видаляємо їх
        while (nodes.Count > 0)
        {
            TrieNode node = nodes.Pop();
            if (node.Children[current.Surname[0]].Children.Count == 0 && !node.Children[current.Surname[0]].IsEndOfWord)
                node.Children.Remove(current.Surname[0]);
            else
                break;
            current = node;
        }
        return true;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Trie trie = new Trie();
        bool running = true;

        while (running)
        {
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Додати студента");
            Console.WriteLine("2. Пошук студента");
            Console.WriteLine("3. Видалити студента");
            Console.WriteLine("4. Вихід");
            Console.Write("Виберіть опцію: ");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Введено некоректну опцію. Спробуйте ще раз.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    AddStudent(trie);
                    break;
                case 2:
                    SearchStudent(trie);
                    break;
                case 3:
                    DeleteStudent(trie);
                    break;
                case 4:
                    running = false;
                    break;
                default:
                    Console.WriteLine("Введено некоректну опцію. Спробуйте ще раз.");
                    break;
            }
        }
    }

    static void AddStudent(Trie trie)
    {
        Console.Write("Введіть прізвище студента: ");
        string surname = Console.ReadLine();

        int[] grades = new int[3];
        for (int i = 0; i < grades.Length; i++)
        {
            Console.Write($"Введіть оцінку {i + 1}: ");
            if (!int.TryParse(Console.ReadLine(), out grades[i]))
            {
                Console.WriteLine("Введено некоректну оцінку. Спробуйте ще раз.");
                return;
            }
        }

        trie.Insert(surname, grades);
        Console.WriteLine("Студент доданий успішно.");
    }

    static void SearchStudent(Trie trie)
    {
        Console.Write("Введіть прізвище студента для пошуку: ");
        string surname = Console.ReadLine();

        if (trie.Search(surname, out int[] grades))
        {
            Console.WriteLine($"Знайдено студента: {surname}");
            Console.WriteLine($"Оцінки: {string.Join(", ", grades)}");
        }
        else
        {
            Console.WriteLine($"Студента з прізвищем {surname} не знайдено.");
        }
    }

    static void DeleteStudent(Trie trie)
    {
        Console.Write("Введіть прізвище студента для видалення: ");
        string surname = Console.ReadLine();

        if (trie.Delete(surname))
        {
            Console.WriteLine($"Студент {surname} видалений успішно.");
        }
        else
        {
            Console.WriteLine($"Студента з прізвищем {surname} не знайдено.");
        }
    }
}
