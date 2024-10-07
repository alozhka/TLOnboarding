namespace QueueTests;

using Queue;


//TODO: добавить и удалять элементы до переполнения очереди (когда begin != 0)
//TODO: тест на оборачивание кольца без переполнения (количество добавлений больше размеров, но без переполнения)
public class QueueTests
{
    [Fact]
    public void CanUseAsFIFO()
    {
        Queue<int> queue = new();
        queue.Enqueue(5);
        queue.Enqueue(10);
        queue.Enqueue(20);
        queue.Enqueue(30);

        Assert.False(queue.IsEmpty());
        Assert.Equal(4, queue.Count);

        Assert.Equal(5, queue.Dequeue());
        Assert.False(queue.IsEmpty());
        Assert.Equal(3, queue.Count);

        Assert.Equal(10, queue.Dequeue());
        Assert.False(queue.IsEmpty());
        Assert.Equal(2, queue.Count);

        Assert.Equal(20, queue.Dequeue());
        Assert.False(queue.IsEmpty());
        Assert.Equal(1, queue.Count);

        Assert.Equal(30, queue.Dequeue());
        Assert.True(queue.IsEmpty());
        Assert.Equal(0, queue.Count);
    }

    [Fact]
    public void CanBeCleared()
    {
        Queue<int> queue = new();
        queue.Enqueue(5);
        queue.Enqueue(4);
        queue.Enqueue(5);

        Assert.Equal(3, queue.Count);
        Assert.False(queue.IsEmpty());

        queue.Clear();

        Assert.Equal(0, queue.Count);
        Assert.True(queue.IsEmpty());
    }

    [Fact]
    public void CanIncreaseCapacity()
    {
        Queue<int> queue = new(2);
        queue.Enqueue(3);
        queue.Enqueue(4);

        queue.Enqueue(5);

        Assert.Equal(3, queue.Dequeue());
        Assert.Equal(4, queue.Dequeue());
        Assert.Equal(5, queue.Dequeue());
    }

    [Fact]
    public void ThrowsExceptionIfDequeueFromEmptyQueue()
    {
        Queue<int> queue = new();

        bool caughtExpection = false;
        try
        {
            queue.Dequeue();
        }
        catch (IndexOutOfRangeException)
        {
            caughtExpection = true;
        }

        Assert.True(caughtExpection);
    }
}
