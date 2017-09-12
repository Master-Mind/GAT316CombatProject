using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ArrayThatWorks<T>
{
    private System.Collections.ArrayList _arrayInternal;

    public ArrayThatWorks()
    {
        _arrayInternal = new System.Collections.ArrayList();
    }

    public ArrayThatWorks(int size)
    {
        _arrayInternal = new System.Collections.ArrayList(size);
    }

    public ArrayThatWorks(System.Collections.ICollection c)
    {
        _arrayInternal = new System.Collections.ArrayList(c);
    }

    public int Add(T item)
    {
        return _arrayInternal.Add(item);
    }

    public void AddRange(System.Collections.ICollection c)
    {
        _arrayInternal.AddRange(c);
    }
    public int BinarySearch(T item)
    {
        return _arrayInternal.BinarySearch(item);
    }

    internal void Reverse()
    {
        _arrayInternal.Reverse();
    }

    public void Clear()
    {
        _arrayInternal.Clear();
    }

    public bool Contains(T item)
    {
        return _arrayInternal.Contains(item);
    }

    public T this[int index]
    {
        get
        {
            return (T)_arrayInternal[index];
        }
        set
        {
            _arrayInternal[index] = value;
        }
    }

    public void Remove(T item)
    {
        _arrayInternal.Remove(item);
    }
    public void RemoveAt(int index)
    {
        _arrayInternal.RemoveAt(index);
    }

    public int Count()
    {
        return _arrayInternal.Count;
    }
}
