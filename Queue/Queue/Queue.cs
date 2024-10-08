using System.Diagnostics;

namespace Queue;

public class Queue<T>(int initialSize = 20)
{
    /// <summary>
    ///  Кольцевой буфер (Ring Buffer) для хранения элементов очереди.
    /// </summary>
    private T[] _buffer = new T[initialSize];

    /// <summary>
    /// Указывает на первый элемент очереди
    /// </summary>
    private int _headIndex = 0;

    /// <summary>
    /// Указывает элемент, следущий за последним
    /// </summary>
    private int _tailIndex = 0;

    private int _count = 0;

    /// <summary>
    ///  Максимальное число элементов, которое можно сохранить без перераспределения памяти.
    /// </summary>
    public int Capacity => _buffer.Length;

    /// <summary>
    ///  Количество элементов в очереди.
    /// </summary>
    public int Count => _count;

    /// <summary>
    ///  Возвращает true, если очередь пуста.
    /// </summary>
    public bool IsEmpty => _count == 0;

    public void Enqueue(T element)
    {
        if (_count == _buffer.Length)
        {
            IncreaseBufferCapacity();
        }

        _buffer[_tailIndex] = element;
        _tailIndex = (_tailIndex + 1) % Capacity;
        ++_count;
    }

    public T Dequeue()
    {
        if (_count == 0)
        {
            throw new IndexOutOfRangeException("Queue is empty");
        }

        T element = _buffer[_headIndex];
        _headIndex = (_headIndex + 1) % Capacity;
        --_count;

        return element;
    }

    public void Clear()
    {
        _buffer = new T[Capacity];
        _headIndex = 0;
        _tailIndex = 0;
        _count = 0;
    }

    private void IncreaseBufferCapacity()
    {
        T[] temp = new T[_buffer.Length * 2];
        for (int tempIndex = 0; tempIndex < _count; ++tempIndex)
        {
            temp[tempIndex] = _buffer[(_headIndex + tempIndex) % _buffer.Length];
        }

        _headIndex = 0;
        _tailIndex = _count;
        _buffer = temp;
    }
}