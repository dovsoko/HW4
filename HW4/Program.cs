using System;
using System.Collections.Generic;

class Node
{
    public int Data;
    public Node Next;
    public Node Left;
    public Node Right;

    public Node(int data)
    {
        Data = data;
        Next = this; // Point to itself for circular list
        Left = null;
        Right = null;
    }
}

class RecursiveOperations
{
    private Node head;

    public RecursiveOperations()
    {
        head = null;
    }

    public void Add(int data)
    {
        Node newNode = new Node(data);
        if (head == null)
        {
            head = newNode;
        }
        else
        {
            Node current = head;
            while (current.Next != head)
            {
                current = current.Next;
            }
            current.Next = newNode;
            newNode.Next = head;
        }
    }

    public override string ToString()
    {
        return RecursiveToString(head);
    }

    private string RecursiveToString(Node current)
    {
        if (current == null)
        {
            return string.Empty;
        }

        Node start = current;
        string result = "";

        do
        {
            result += current.Data + " -> ";
            current = current.Next;
        } while (current != start);

        return result.TrimEnd('-', '>');
    }

    public bool Search(int value)
    {
        return SearchRecursive(head, value);
    }

    private bool SearchRecursive(Node current, int value)
    {
        if (current == null)
        {
            return false;
        }

        if (current.Data == value)
        {
            return true;
        }

        return SearchRecursive(current.Next, value); // Only search in the main linked list
    }

    public int Size()
    {
        return SizeRecursive(head);
    }

    private int SizeRecursive(Node current)
    {
        if (current == null)
        {
            return 0;
        }

        Node start = current;
        int size = 0;

        do
        {
            size++;
            current = current.Next;
        } while (current != start);

        return size;
    }

    public void ShellSort()
    {
        int[] arr = ConvertToSingleArray();
        int n = arr.Length;
        ShellSort(arr, n, n / 2);
        UpdateFromSingleArray(arr);
    }

    private void ShellSort(int[] arr, int n, int gap)
    {
        if (gap > 0)
        {
            for (int i = gap; i < n; i++)
            {
                int temp = arr[i];
                int j = i;
                while (j >= gap && arr[j - gap] > temp)
                {
                    arr[j] = arr[j - gap];
                    j -= gap;
                }
                arr[j] = temp;
            }
            ShellSort(arr, n, gap / 2);
        }
    }

    private int[] ConvertToSingleArray()
    {
        Node current = head;
        Node start = current;
        int size = 0;

        do
        {
            size++;
            current = current.Next;
        } while (current != start);

        int[] arr = new int[size];
        current = head;

        for (int i = 0; i < size; i++)
        {
            arr[i] = current.Data;
            current = current.Next;
        }

        return arr;
    }

    private void UpdateFromSingleArray(int[] arr)
    {
        Node current = head;
        Node start = current;
        int index = 0;

        do
        {
            current.Data = arr[index];
            index++;
            current = current.Next;
        } while (current != start);
    }
}

class Program
{
    static void Main(string[] args)
    {
        RecursiveOperations operations = new RecursiveOperations();
        operations.Add(3);
        operations.Add(1);
        operations.Add(4);
        operations.Add(2);

        Console.WriteLine("Linked List Contents: " + operations.ToString());

        int searchValue = 4;
        bool isFound = operations.Search(searchValue);

        if (isFound)
        {
            Console.WriteLine($"{searchValue} is in the linked list.");
        }
        else
        {
            Console.WriteLine($"{searchValue} is not in the linked list.");
        }

        int circularSize = operations.Size();
        Console.WriteLine("Circular List Size: " + circularSize);

        Console.WriteLine("Before Shell Sort: " + operations);
        operations.ShellSort();
        Console.WriteLine("After Shell Sort: " + operations);
    }
}
