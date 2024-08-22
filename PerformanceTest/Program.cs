using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class Program
{
  private static ManualResetEvent _mre = new ManualResetEvent(false);
  private static List<string> _list = [];
  private static LinkedList<string> _linkedList = [];
  private static void Main(string[] args)
  {
    using StreamReader sr = new StreamReader("Data\\Text1.txt");

    while (!sr.EndOfStream)
    {
      _list.Add(sr.ReadLine());

      _linkedList.AddLast(sr.ReadLine());
    }

    //foreach(var s in _list)
    //{
    //  Console.WriteLine(s);
    //}

    //foreach (var s in _linkedList)
    //{
    //  Console.WriteLine(s);
    //}

    Thread t1 = new Thread(ListTest);
    t1.Name = "List test";
    t1.Start();

    Thread t2 = new Thread(LinkedListTest);
    t2.Name = "Linked list test";
    t2.Start();

    _mre.Set();

    t1.Join();
    t2.Join();
  }

  private static void ListTest()
  {
    _mre.WaitOne();

    var watch = Stopwatch.StartNew();

    _list.Add("Новая строка");

    watch.Stop();

    Console.WriteLine($"Вставка в List в конец: {watch.Elapsed.TotalMilliseconds}");

    watch.Restart();

    _list.Insert(_list.Count / 2, "Еще строка");

    watch.Stop();

    Console.WriteLine($"Вставка в List в середину: {watch.Elapsed.TotalMilliseconds}");
  }

  private static void LinkedListTest()
  {
    _mre.WaitOne();

    var watch = Stopwatch.StartNew();

    watch.Stop();

    _linkedList.AddLast("Новая строка");

    Console.WriteLine($"Вставка в Linked list в конец: {watch.Elapsed.TotalMilliseconds}");

    var middleLine = _linkedList.ElementAt(_linkedList.Count / 2);

    watch.Restart();

    _linkedList.AddAfter(_linkedList.Find(middleLine), "Еще строка");

    watch.Stop();

    Console.WriteLine($"Вставка в Linked list в середину: {watch.Elapsed.TotalMilliseconds}");
  }
}