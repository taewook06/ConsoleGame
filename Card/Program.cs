using System;
using static System.Console;
using System.Collections.Generic;
using System.Threading;



namespace Card
{
    class Program
    {
        public static void Main(string[] args)
        {

            Ai();

            Base();

            string Read = "";

            Read = ReadLine();

            switch (Read)
            {
                case "1":

                    break;

                case "2":
                    WriteLine(".");
                    break;
                case "3":
                    WriteLine("게임을 종료합니다.");
                    break;
            }


        }
        static void Base()
        {
            Console.SetWindowSize(100, 40);

            Console.BackgroundColor = ConsoleColor.Gray;
            Clear();
            ForegroundColor = ConsoleColor.DarkBlue;

            Thread.Sleep(1000);
            WriteLine("카드심리게임");
            WriteLine(" ");
            WriteLine(" ");
            Thread.Sleep(1000);
            WriteLine("게임규칙(1)");
            WriteLine("게임시작(2)");
            WriteLine("게임종료(3)");
        }
        public static void Ai()
        {
            Random rand = new Random();

            List<int> Card = new List<int>
            { 1,2,3,4,5,6,7,8,9 };

            int C = rand.Next(Card.Count);

            Card.RemoveAt(C);



        }
        static void Explanation()
        {
            WriteLine("게임규칙");
            WriteLine(" ");
            WriteLine("1. 1부터 9까지 카드 중에 카드 한 장을 뽑는다.");
            WriteLine(" ");
            WriteLine("2. ai도 똑같이 1부터 9까지 카드를 뽑는다. ");
            WriteLine(" ");
            WriteLine("3. 두 수를 비교하여 높은 카드 3점, 같으면 2점, 낮으면 1점을 갖는다.");
            WriteLine(" ");
            WriteLine("4. 한 번 사용한 카드를 제외한 카드로 다시 똑같이 반복한다.");
            WriteLine(" ");
            WriteLine("5. 카드를 모두 사용했으면 점수를 합산하여 승패를 가린다.");
            WriteLine(" ");
            WriteLine(" ");
            WriteLine("시작하시겠습니까? (y/n)");
        }


    }


}
