using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using PriorityQueue;
using System.Collections;

namespace pr_pr_8
{
    class Program
    {
        static void Main(string[] args)
        {
            task_5();
            Console.Read();
        }

        private static void task_1()
        {
            string str = File.ReadAllText("input.txt");
            Console.WriteLine(str + " is" + (bracketsBalanced(str) ? "" : "n't") + " brackets-balanced");
        }

        private static bool bracketsBalanced(String str)
        {
            List<char> brackets = new List<char>();
            foreach (char c in str)
            {
                if (c == ')' || c == '(')
                {
                    brackets.Add(c);
                }
            }
            if (brackets.Count % 2 != 0 || brackets.First() == ')' || brackets.Last() == '(')
            {
                return false;
            }
            return true;
        }

        private static void task_2()
        {
            StreamReader reader = File.OpenText("people.txt");
            PriorityQueue<double, Man> queue = new PriorityQueue<double, Man>();
            string manInfo;
            string[] info;
            Man man;
            while ((manInfo = reader.ReadLine()) != null)
            {
                info = manInfo.Split(' ');
                try
                {
                    man = new Man(info[0], info[1], info[2], double.Parse(info[3]), double.Parse(info[4]));
                } catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Not enough information in the line: " + manInfo);
                    return;
                } catch (NotFiniteNumberException)
                {
                    Console.WriteLine("Wrong value for age or weight set for the string: " + manInfo);
                    return;
                }
                queue.Enqueue(man.getAge(), man);
            }
            while (queue.Count > 0)
            {
                Console.WriteLine(queue.Dequeue().ToString());
            }
            reader.Close();
        }

        private static void task_3()
        {
            StreamReader reader = File.OpenText("people.txt");
            List<Man> list = new List<Man>();
            string manInfo;
            string[] info;
            Man man;
            while ((manInfo = reader.ReadLine()) != null)
            {
                info = manInfo.Split(' ');
                try
                {
                    man = new Man(info[0], info[1], info[2], double.Parse(info[3]), double.Parse(info[4]));
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Not enough information in the line: " + manInfo);
                    return;
                }
                catch (NotFiniteNumberException)
                {
                    Console.WriteLine("Wrong value for age or weight set for the string: " + manInfo);
                    return;
                }
                list.Add(man);
            }
            list.Sort();
            foreach (Man m in list)
            {
                Console.WriteLine(m.ToString());
            }
            reader.Close();
        }

        private static void task_4()
        {
            string[] numbers = File.ReadAllText("numbers.txt").Split(' ');
            double[] doubleNumbers = new double[numbers.Count()];
            Stack<double> stack = new Stack<double>();
            for (int i = 0; i < numbers.Count(); i++)
            {
                try
                {
                    stack.Push(doubleNumbers[i] = double.Parse(numbers[i]));
                }
                catch (FormatException)
                {
                    Console.WriteLine("Wrong number value " + numbers[i]);
                }
            }
            StringBuilder stringBuilder = new StringBuilder();
            while (stack.Count > 0)
            {
                stringBuilder.Append(stack.Pop() + " ");
            }
            File.WriteAllText("backwardNumbers.txt", stringBuilder.ToString());
        }

        private static void task_5()
        {
            ArrayList array = new ArrayList();
            Random random = new Random(System.DateTime.Now.GetHashCode());
            int number;
            //1)
            while (array.Count < 2030)
            {
                array.Add(300 + random.Next(201));
            }
            Console.WriteLine("Array size is + " + array.Count);
            //2)
            array.Sort();
            //3)
            Console.WriteLine("Sorted array:");
            foreach (int i in array)
            {
                Console.WriteLine(i);
            }
            //4)
            array.Insert(1, 0);
            //5)
            int numberForSearch = random.Next(1) + 1000;
            if (array.Contains(numberForSearch))
            {
                Console.WriteLine("The index of" + numberForSearch + " is " + array.IndexOf(numberForSearch));
            }
            else
            { 
                Console.WriteLine("The array does not contain " + numberForSearch);
            }
            //6)
            int numberForDelete = random.Next(array.Count);
            if (array.Contains(numberForDelete))
            {
                array.Remove(numberForDelete);
                Console.WriteLine("The element " + numberForDelete + " has been removed from array");
            } else
            {
                Console.WriteLine("The array does not contains the element " + numberForDelete);
            }
            Console.WriteLine("Array size is " + array.Count);
            //7)
            while (array.Count > 0)
            {
                array.RemoveAt(0);
            }
            Console.WriteLine("All elements have been removed. Array size is " + array.Count);
        }
    }
}

    public class Man : IComparable
    {
        private string firstName;
        private string middleName;
        private string lastName;
        private double age;
        private double weight;
        public Man(string firstName, string middleName, string lastName, double age, double weight)
        {
            this.firstName = firstName;
            this.middleName = middleName;
            this.lastName = lastName;
            this.age = age;
            this.weight = weight;
        }

        public double getAge()
        {
            return age;
        }

        public override string ToString()
        {
            return lastName + " " + firstName + " " + middleName + ", age: " + age + ", weight: " + weight;
        }

        public int CompareTo(object obj)
        {
            Man second = (Man) obj;
            return getAge() > second.getAge() ? 1 : getAge() < second.getAge() ? -1 : 0;
        }
}