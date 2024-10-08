namespace QueueTests;

using Queue;


public class QueueTests
{
    [Fact]
    public void Can_use_as_FIFO_queue()
    {
        Queue<int> queue = new();
        queue.Enqueue(5);
        queue.Enqueue(10);
        queue.Enqueue(20);
        queue.Enqueue(30);

        Assert.False(queue.IsEmpty);
        Assert.Equal(4, queue.Count);

        Assert.Equal(5, queue.Dequeue());
        Assert.False(queue.IsEmpty);
        Assert.Equal(3, queue.Count);

        Assert.Equal(10, queue.Dequeue());
        Assert.False(queue.IsEmpty);
        Assert.Equal(2, queue.Count);

        Assert.Equal(20, queue.Dequeue());
        Assert.False(queue.IsEmpty);
        Assert.Equal(1, queue.Count);

        Assert.Equal(30, queue.Dequeue());
        Assert.True(queue.IsEmpty);
        Assert.Equal(0, queue.Count);
    }

    [Fact]
    public void Can_be_cleared()
    {
        Queue<int> queue = new();
        queue.Enqueue(5);
        queue.Enqueue(4);
        queue.Enqueue(5);

        Assert.Equal(3, queue.Count);
        Assert.False(queue.IsEmpty);

        queue.Clear();

        Assert.Equal(0, queue.Count);
        Assert.True(queue.IsEmpty);
    }

    [Fact]
    public void Capacity_grows_when_size_exceeds_capacity()
    {
        Queue<int> queue = new(3);
        queue.Enqueue(3);
        queue.Enqueue(4);

        queue.Dequeue();
        queue.Dequeue();

        queue.Enqueue(5);
        queue.Enqueue(6);
        queue.Enqueue(7);

        Assert.Equal(5, queue.Dequeue());
        Assert.Equal(6, queue.Dequeue());
        Assert.Equal(7, queue.Dequeue());

        Assert.Equal(0, queue.Count);
        Assert.True(queue.IsEmpty);

        queue = new Queue<int>(3);

        queue.Enqueue(3);
        queue.Enqueue(4);
        queue.Enqueue(5);

        Assert.Equal(3, queue.Dequeue());
        Assert.Equal(4, queue.Dequeue());
        Assert.Equal(5, queue.Dequeue());

        Assert.Equal(0, queue.Count);
        Assert.True(queue.IsEmpty);
    }

    [Fact]
    public void Capacity_does_not_grow_until_size_exceeds_capacity()
    {
        Queue<int> queue = new(4);
        queue.Enqueue(5);
        queue.Enqueue(6);
        queue.Enqueue(7);

        Assert.Equal(4, queue.Capacity);

        Assert.Equal(5, queue.Dequeue());
        Assert.Equal(6, queue.Dequeue());

        queue.Enqueue(8);
        queue.Enqueue(9);

        Assert.Equal(7, queue.Dequeue());
        Assert.Equal(8, queue.Dequeue());

        queue.Enqueue(10);
        queue.Enqueue(11);

        Assert.Equal(9, queue.Dequeue());
        Assert.Equal(10, queue.Dequeue());

        Assert.Equal(1, queue.Count);

        Assert.Equal(4, queue.Capacity);
    }

    [Fact]
    public void Cannot_dequeue_from_empty_queue()
    {
        Queue<int> queue = new();
        queue.Enqueue(2);
        queue.Dequeue();

        Assert.Throws<IndexOutOfRangeException>(() => queue.Dequeue());
    }
}
