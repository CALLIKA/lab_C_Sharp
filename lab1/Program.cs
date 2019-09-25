using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().lab3();
        }

        void printLabName(int index)
        {
            Console.WriteLine("Лабораторная работа №{0} Вариант 5", index);
        }

        double F1(double a, double b) {
            double cos2a = Math.Cos(2 * a);
            if (cos2a < 0)
            {
                throw new ArithmeticException("cos(2a) < 0 - будет корень из отрицательного числа");
            }

            if (cos2a == 0)
            {
                throw new DivideByZeroException("cos(2a) == 0 - будет деление на 0");
            }

            if (b < 0)
            {
                throw new ArithmeticException("b < 0 - невозможно посчитать логарифм из отрицательного");
            }
            

            return Math.Pow(Math.Sin(Math.Log(b, 5) / Math.Sqrt(Math.Cos(2 * a))), 2);
        }

        void printInputError(String cause)
        {
            Console.WriteLine("Некорректный ввод, повторите попытку: {0}", cause);
        }

        double ReadDouble (string name) {
            Console.Write("Введите {0}:\t", name);

            double res;
            while (!double.TryParse(Console.ReadLine(), out res))
            {
                printInputError("невозможно преобразовать в число");
                Console.Write("Введите {0}:\t", name);
            }

            return res;
        }

        int ReadInt(string name, int minValue, int maxValue)
        {
            while (true)
            {
                Console.Write("Введите {0}:\t", name);

                int res;
                if (int.TryParse(Console.ReadLine(), out res))
                {
                    if (minValue <= res && res <= maxValue)
                    {
                        return res;
                    }
                    else
                    {
                        printInputError(String.Format("{0} не входит в отрезок [{1}; {2}]", name, minValue, maxValue));
                    }
                }    
                else
                {
                    printInputError("Невозможно преобразовать в число");
                }
            }
        }
        public void lab1() //решение уравнения
        {
            printLabName(1);
            Console.WriteLine("Чему равно значение функции f=sin^2*((log_5(b))/sqrt(cos(2a)))");
            
            double a = ReadDouble("A");
            double b = ReadDouble("B");

            try
            {
                double f = F1(a, b);

                bool guessed = false;
                for (int i = 3; i > 0 && !guessed; --i)
                {
                    Console.WriteLine("Количество оставшихся попыток: {0}", i);

                    double ans = ReadDouble("предполагаемый ответ (с точностью 1e-2)");

                    double eps = 1e-2;
                    if (Math.Abs(f - ans) <= eps)
                    {
                        Console.WriteLine("Вы правы!");
                        guessed = true;
                    } else
                    {
                        Console.WriteLine("Неправильный ответ, попробуйте ещё.");
                    }
                }
                
                if (!guessed)
                {
                    Console.WriteLine("К сожалению, ваши попытки закончились. Вы проиграли");
                }

                Console.WriteLine("Правильный ответ: {0}", f);
            }
            catch (ArithmeticException e)
            {
                Console.WriteLine("Произошла ошибка вычисления: {0}", e.Message);
            }

            

            Console.ReadKey();
        }


        public bool lab2() //Меню
        {
            printLabName(2);

            string[] menuTitles =
            {
                "Выполнение лабораторной работы 1",
                "Об авторе",
                "Задание",
                "Выполнение лабораторной работы 4",
                "Игра",
                "Работа со строками",
                "Выход"
            };

            Console.WriteLine("Меню программы");
            for (int i = 0; i < menuTitles.Length-1; ++i)
            {
                Console.WriteLine("{0} - {1}", i + 1, menuTitles[i]);
            }
            Console.WriteLine("{0} - {1}", 0, menuTitles.Last());
            Console.WriteLine("Выберите пункт меню (от 0 до {0})", menuTitles.Length-1);

            int menuNumber = ReadInt("пункт меню", 0, menuTitles.Length-1);
            bool needContinue = true;
            try
            {
                switch (menuNumber)
                {
                    case 1:
                        lab1();
                        break;
                    case 2:
                        Console.WriteLine("Воронов Александр Алексеевич, 6203");
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.WriteLine("Вывести на экран значение функции f=sin^2*((log_5(b))/sqrt(cos(2a)))");
                        Console.ReadKey();
                        break;
                    case 4:
                        lab4();
                        break;
                    case 5:
                        lab5();
                        break;
                    case 6:
                        lab6();
                        break;
                    case 0:
                        needContinue = NeedContinue();
                        break;
                        
                        
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Случилась ошибка: {0}", e.Message);
            }
            return needContinue;
        }

        private bool NeedContinue()
        {
            Console.WriteLine("Хотите выйти из программы?");
            Console.WriteLine("Нажмите 'д', Если уверены, что хотите выйти, или 'н', если хотите продолжить");

            while (true)
            {
                string input = Console.ReadLine();
                if (input.Length == 1)
                {
                    char exit = input[0];
                    if (exit == 'д') return false;
                    if (exit == 'н') return true;
                }

                printInputError("необходимо ввести 'д' или 'н'");
            }

        }

        public void lab3() //Меню в цикле
        {
            printLabName(3);
            while (lab2());

            Console.WriteLine("До встречи!");
            Console.ReadKey();
        }


        static int[] createRandomArray(int size)
        {
            if (size <= 0)
            {
                new ArgumentException("Размер массива должен быть положительным");
            }

            int[] array = new int[size];

            Random rnd = new Random();
            for (int i = 0; i < size; ++i)
            {
                array[i] = rnd.Next(-100, 100);
            }
            return array;
        }

        static void printArray(string title, int [] array)
        {
            Console.Write("{0}:\t", title);
            foreach (int value in array)
            {
                Console.Write("{0}\t", value);
            }
            Console.WriteLine();
        }

        int[] insertionSort(int[] array)
        {
            int[] sorted = new int[array.Length];
            array.CopyTo(sorted, 0);

            for (int i = 0; i < sorted.Length; ++i)
            {
                for (int j = i; j > 0 && sorted[j - 1] > sorted[j]; --j)
                {
                    int tmp = sorted[j];
                    sorted[j] = sorted[j - 1];
                    sorted[j - 1] = tmp;
                }
            }
            return sorted; 


        }
        int[] gnomeSort(int[] array)
        {
            int[] sorted = new int[array.Length];
            array.CopyTo(sorted, 0);

            for (int i = 0; i < sorted.Length; )
            {
                if (i == 0 || sorted[i - 1] <= sorted[i])
                {
                    ++i;
                }
                else
                {
                    int tmp = sorted[i];
                    sorted[i] = sorted[i - 1];
                    sorted[i - 1] = tmp;
                    --i;
                }
            }
            return sorted;

        }

        public void lab4() //Сортировки 
        {
            printLabName(4);
            int n = ReadInt("размер массива", 1, 100);

            int [] array = createRandomArray(n);
            printArray("Массив до сортировки", array);

            var gnomeTimer = new Stopwatch();
            gnomeTimer.Start();
            int[] gnomeSortArray = gnomeSort(array);
            gnomeTimer.Stop();
            printArray("Массив после гномьей сортировки", gnomeSortArray);

            var insertionTimer = new Stopwatch();
            insertionTimer.Start();
            int[] insertionSortArray = insertionSort(array);
            insertionTimer.Stop();
            printArray("Массив после сортировки вставками", insertionSortArray);

            Console.WriteLine("Время работы гномьей сортировки (тики таймера): {0}", gnomeTimer.ElapsedTicks);
            Console.WriteLine("Время работы сортировки вставками (тики таймера): {0}", insertionTimer.ElapsedTicks);

            Console.ReadKey();
        }

        const int EMPTY = -1, BLACK = 1, WHITE = 0;

        static int[,] createField(int size)
        {

            if (size <= 2)
            {
                new ArgumentException("Размер поля должен быть больше 2х");
            }

            int[,] field = new int[size, size];
            for (int i = 0; i < size; ++i)
                for (int j = 0; j < size; ++j)
                    field[i, j] = EMPTY;

            int mid = (size + 1) / 2 - 1;

            field[mid, mid] = BLACK;
            field[mid+1, mid] = WHITE;
            field[mid, mid+1] = WHITE;
            field[mid + 1, mid + 1] = BLACK;
            return field;
        }

        static void printField(int[,] field)
        {
            for (int column = 0; column <= field.GetLength(1); ++column)
            {
                if (column > 0) Console.Write(column);
                Console.Write("\t");
            }
            Console.WriteLine();

            for (int row = 0; row < field.GetLength(0); ++row)
            {
                Console.Write("{0}:\t", row + 1);
                for (int column = 0; column < field.GetLength(1); ++column)
                {
                    int value = field[row, column];
                    if (value != EMPTY) Console.Write(value);
                    Console.Write("\t");
                }
                Console.WriteLine();
            }
        }

        public void lab5()
        {
            printLabName(4);
            Console.WriteLine("Игра реверси");

            int fieldSize = ReadInt("размер поля", 3, 10);
            int[,] field = createField(fieldSize);

            int[][] directions =
            {
                new int[] { -1, 0 },
                new int[] { 1, 0 },
                new int[] { 0, -1 },
                new int[] { 0, 1 }
            };

            

            for (int i = 4, total = fieldSize * fieldSize; i < total; ++i)
            {
                printField(field);

                

                Console.WriteLine("Сейчас ход: {0}", (i % 2 == 0 ? "черный" : "белый"));
                int x = ReadInt("номер строки", 1, fieldSize) - 1;
                int y = ReadInt("номер столбца", 1, fieldSize) - 1;

                if (-1 != field[x, y])
                {
                    printInputError(String.Format("клетка [{0}, {1}] занята значением {2}", x + 1, y + 1, field[x, y]));
                    --i;
                    continue;
                }

                int color = 1 - i % 2;
                field[x, y] = color;

                foreach (int[] direction in directions)
                {
                    int dx = direction[0], dy = direction[1];

                    int sameColorX = -1, sameColorY = -1;
                    for (int curX = x + dx, curY = y + dy; 0 <= curX && curX < fieldSize && 0 <= curY && curY < fieldSize; curX += dx, curY += dy)
                    {
                        if (field[curX, curY] == EMPTY) break;

                        if (field[curX, curY] == color)
                        {
                            sameColorX = curX;
                            sameColorY = curY;
                            break;
                        }
                    }    

                    if (sameColorX != -1 && sameColorY != -1)
                    {
                        for (int curX = x + dx, curY = y + dy; curX != sameColorX || curY != sameColorY; curX += dx, curY += dy)
                        {
                            field[curX, curY] = color;
                        }
                    }
                }    
            }

            Console.WriteLine("Игра завершена");
            printField(field);

            int whiteCount = 0, blackCount = 0;
            for (int row = 0; row < fieldSize; ++row)
                for (int column = 0; column < fieldSize; ++column)
                {
                    if (WHITE == field[row, column]) ++whiteCount;
                    else ++blackCount;
                }
               
            Console.WriteLine("Количество черных фишек {0}, белых - {1}", blackCount, whiteCount);
        }

        public void lab6()
        {
            Console.WriteLine("Желаете увидеть тестовую строку?\t", "Введи 'д' - если хотите и 'н'- если нет");

            while (true)
            {
                string input = Console.ReadLine();
                if (input.Length == 1)
                {
                    char exit = input[0];
                    if (exit == 'д')
                    {
                        int stringA = 13;
                        Console.WriteLine("Варкалось.Хливкие шорьки \nПырялись по наве, \nИ хрюкотали зелюки, \nКак мюмзики в мове, \nО бойся Бармаглота, сын! \nОн так свиреп и дик, \nА в глуще рымит исполин - \nЗлопастный Брандашмыг.\t", "В данной строке {0} \t", stringA);
                        break;
                    }
                    if (exit == 'н')
                        break;
                }
                printInputError("необходимо ввести 'д' или 'н'");
            }

            Console.WriteLine("Введите проверяемую строку");
            string poems = Console.ReadLine();
            int countA = 0;
            for (int i = 0; i < poems.Length; i++)
            {
                if (poems[i] == 'а' || poems[i] == 'А')
                    countA++;
            }
            Console.WriteLine(countA);
        }
    }
}
