using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockingCollection
{
    class Program
    {
        private static void CreateMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose your test:");
            Console.WriteLine("1 - NonBlockingCollection");
            Console.WriteLine("2 - BlockongCollection");
            Console.WriteLine("Put number and press Enter");
        }

        private static void NonBlockCollection()
        {
            while (true)
            {
                try
                {
                    int count = 0;
                    var nonBlockingCollection = new List<string>();
                    Task.Factory.StartNew(() =>
                    {
                        while (true)
                        {
                            nonBlockingCollection.Add("value" + count);
                            count++;
                        }
                    });

                    Task.Factory.StartNew(() =>
                    {
                        while (true)
                        {
                            var value = nonBlockingCollection.FirstOrDefault();
                            if (value != null)
                                Console.WriteLine("Thread 1: " + nonBlockingCollection.Remove(nonBlockingCollection.FirstOrDefault()));
                        }
                    });

                    Task.Factory.StartNew(() =>
                    {
                        while (true)
                        {
                            var value = nonBlockingCollection.FirstOrDefault();
                            if (value != null)
                                Console.WriteLine("Thread 2: " + nonBlockingCollection.Remove(nonBlockingCollection.FirstOrDefault()));
                        }
                    });
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
        }

        private static void BlockCollection()
        {
            try
            {
                while (true)
                {
                    int count = 0;
                    var blockingCollection = new BlockingCollection<string>();
                    Task.Factory.StartNew(() =>
                    {
                        while (true)
                        {
                            blockingCollection.Add("value" + count);
                            count++;
                        }
                    });

                    Task.Factory.StartNew(() =>
                    {
                        while (true)
                        {
                            Console.WriteLine("Thread 1: " + blockingCollection.Take());
                        }
                    });

                    Task.Factory.StartNew(() =>
                    {
                        while (true)
                        {
                            Console.WriteLine("Thread 2: " + blockingCollection.Take());
                        }
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        static void Main(string[] args)
        {
            CreateMenu();
            var number = Console.ReadLine();
            int nr = 0;
            if (int.TryParse(number, out nr))
            {
                switch (nr)
                {
                    case 1:
                        NonBlockCollection();
                        break;
                    case 2:
                        BlockCollection();
                        break;
                }
            }
        }
    }
}
