using UnityEngine;
public class MinHeap<T>
{
    public delegate bool Comparer(T lhr, T rhr);
    Comparer comp;
    T[] _heap = new T[16];
    int _heapLen = 0;

    public MinHeap(Comparer comp)
    {
        this.comp = comp;
    }

    public void ConstructHeap(T[] heap)
    {
        _heapLen = 0;
        Expand(heap.Length);
        System.Array.Copy(heap, _heap, heap.Length);
        _heapLen = heap.Length;
        for (int i = Mathf.FloorToInt((_heapLen - 1) / 2); i >= 0; --i)
        {
            MinHeapify(i);
        }
    }

    // O(n)
    public MinHeap(T[] heap, Comparer comp)
    {
        this.comp = comp;
        ConstructHeap(heap);
    }

    // O(log(n))
    public void Expand(int len)
    {
        if (len < _heap.Length)
            return;
        int heapLen = _heap.Length;
        while (len > heapLen)
        {
            heapLen *= 2;
        }
        T[] oldHeap = _heap;
        _heap = new T[heapLen];
        System.Array.Copy(oldHeap, _heap, _heapLen);
    }

    // O(log(n))
    public void Insert(T element)
    {
        Expand(_heapLen + 1);
        _heap[_heapLen] = element;
        _heapLen += 1;
        int child = _heapLen - 1;
        int parent = Mathf.FloorToInt((_heapLen - 1) / 2);
        while (child != 0)
        {
            if (!comp.Invoke(_heap[parent], _heap[child]))
            {
                Swap(parent, child);
            }
            else
            {
                break;
            }
            child = parent;
            parent = Mathf.FloorToInt((parent - 1) / 2);
        }
    }

    public int HeapLen
    {
        get
        {
            return _heapLen;
        }
    }

    // O(log(n))
    public T ExtractMin()
    {
        T min = _heap[0];
        Swap(0, _heapLen - 1);
        _heapLen -= 1;
        MinHeapify(0);
        return min;
    }

    public void Heapify()
    {
        MinHeapify(0);
    }

    void MinHeapify(int i)
    {
        int left = 2 * i + 1;
        int right = 2 * i + 2;
        int smallest = i;
        if (left < _heapLen && comp.Invoke(_heap[left], _heap[smallest]))
        {
            smallest = left;
        }
        if (right < _heapLen && comp.Invoke(_heap[right], _heap[smallest]))
        {
            smallest = right;
        }
        if (smallest != i)
        {
            Swap(i, smallest);
            MinHeapify(smallest);
        }
    }

    void Swap(int i, int j)
    {
        T tmp = _heap[i];
        _heap[i] = _heap[j];
        _heap[j] = tmp;
    }

    public void Print()
    {
        for (int i = 0; i < _heapLen; ++i)
        {
            Print(i);
        }
    }

    void Print(int i)
    {
        if (i >= _heapLen)
            return;
        int level = 0;
        int j = i;
        while (j > 0)
        {
            j = (j - 1) / 2;
            level += 1;
        }
        string log = "level " + level + ": " + _heap[i];
        if (2 * i + 1 < _heapLen)
        {
            log += " -> " + _heap[2 * i + 1];
        }
        if (2 * i + 2 < _heapLen)
        {
            log += ", " + _heap[2 * i + 2];
        }
        Debug.Log(log);
    }
}
