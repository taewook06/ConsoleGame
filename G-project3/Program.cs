using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace G_project3
{
    //게임제목: Get a job

    //게임설명: 취업준비를 하고있는 취준생이 3장의 지원서 즉, 3번의 기회로 직업을 지원하여 테스트를 보며 빠르게 직업을 가져야하는 게임입니다.
    //          총 4개의 선택지가 있고 테스트가 미니게임식으로 진행됩니다.
    class Program
    {
        static IntPtr ConsoleWindowHnd = GetForegroundWindow(); //타이머를 쓰기 위한 코드
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("User32.Dll")]
        private static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);
        const int VK_RETURN = 0x0D;
        const int WM_KEYDOWN = 0x100;

        static bool isNext = false;

        static async Task theThreadTimer(int _time)
        {
            int theTime = 0;            // 현재 타이머 시간
            int timeLimit = _time;
            while (theTime < timeLimit)
            {
                await Task.Delay(1000); //1초의 딜레이 (1초가 지났다는 뜻)

                if (isNext == true)
                {
                    theTime = timeLimit;    //강제종료
                    isNext = false;
                    return;
                }

                theTime++;
            }

            isTimeOut = true;
        }

        static bool isTimeOut = false;  //시간이 모두 지났는지 체크

        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Clear();
            ForegroundColor = ConsoleColor.Yellow;
            Console.SetWindowSize(150, 40);

            Thread.Sleep(2000);
            WriteLine("");
            WriteLine("당신은 취업준비생입니다. ");
            Thread.Sleep(2000);
            WriteLine("");

            StartPlayer();

        }

        static void StartPlayer() // 인게임
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Clear();
            ForegroundColor = ConsoleColor.Yellow;

            float Chance = 3; //지원서 

            if (Chance == 0)  //지원서가 0장이면 지원을 못하므로 게임 패배.
            {
                Console.BackgroundColor = ConsoleColor.White;
                Clear();
                ForegroundColor = ConsoleColor.Red;

                WriteLine("[GAME OVER] 지원서 3장을 모두 사용하였지만, 안타깝게도 취직을 실패하였습니다.");
                //게임종료 아직구현 x
            }


            WriteLine(" ");
            WriteLine($"{Chance}장의 지원서가 남아있습니다.");
            Thread.Sleep(500); ;
            WriteLine(" ");
            WriteLine("어디에 지원을 할까요?");
            Thread.Sleep(500);
            WriteLine(" ");
            WriteLine("[1] 수학교사");
            Thread.Sleep(500);
            WriteLine("[2] 프로게이머");
            Thread.Sleep(500);
            WriteLine("[3] 변호사");
            Thread.Sleep(500);
            WriteLine("[4] 10% 확률로 20억을 가진 백수가 됩니다.");
            WriteLine(" ");

            float Hope = 0;
            Hope = float.Parse(ReadLine());

            switch (Hope)
            {
                case 1:
                    Console.BackgroundColor = ConsoleColor.Green;
                    Clear();
                    ForegroundColor = ConsoleColor.Black;
                    WriteLine("수학교사에 지원하셨습니다.");
                    Thread.Sleep(1000);
                    WriteLine(" ");
                    WriteLine("[연산테스트] 30초안에 주어진 문제를 6개 이상 맞출시 합격.");
                    WriteLine("(5초뒤 테스트가 시작합니다)");
                    Thread.Sleep(5000);
                    WriteLine(" ");

                    Math(); //연산테스트 



                    break;
                case 2:
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Clear();
                    ForegroundColor = ConsoleColor.White;
                    WriteLine("프로게이머에 지원하셨습니다.");
                    Thread.Sleep(1000);

                    WriteLine(" ");
                    WriteLine("[순발력테스트] 30초안에 주어진 문자를 60개 이상누를시 합격.");
                    WriteLine("(5초뒤 테스트가 시작합니다)");
                    Thread.Sleep(5000);


                    Speed(); //순발력테스트

                    break;
                case 3:
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Clear();
                    ForegroundColor = ConsoleColor.Yellow;
                    WriteLine("변호사에 지원하셨습니다.");
                    Thread.Sleep(1000);
                    WriteLine(" ");
                    WriteLine("[기억력테스트] 8자리 숫자들을 외우고 물음에 답하기, 3번을 맞추면 합격.");
                    WriteLine("(5초뒤 테스트가 시작합니다)");
                    Thread.Sleep(5000);
                    WriteLine(" ");
                    Memory(); //기억력테스트

                    break;
                case 4:
                   
                    WriteLine("[로또] 1~10까지 행운을 뽑으시오.");

                    float Luck = 0;
                    Luck = float.Parse(ReadLine());

                    Random random = new Random();
                    int RandLuck = random.Next(1, 11);

                    LuckColor(); // <<0.3초마다 바뀌는 색상 , 바뀌는 도중에 코드 실행을 하고 싶었으나, 구현x

                    
                    Console.BackgroundColor = ConsoleColor.White;
                    Clear();
                    ForegroundColor = ConsoleColor.Black;

                    Thread.Sleep(2000);

                    if (Luck == RandLuck)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Clear();
                        ForegroundColor = ConsoleColor.Black;

                        WriteLine("[당첨] 로또에 당첨되어 20억을 받고 돈많은 백수가 되었습니다!");
                        //게임종료 << 아직구현 x
                    }
                    Console.BackgroundColor = ConsoleColor.Red;
                    Clear();
                    ForegroundColor = ConsoleColor.Yellow;
                    WriteLine("아쉽지만, 다음기회에...");
                    //Chance -= 1;    << 기회가 줄지않음 구현 x
                    Thread.Sleep(2000);
                    StartPlayer();
                    break;

                default:
                    WriteLine("다시 입력해주세요.");
                    Thread.Sleep(2000);
                    StartPlayer();
                    break;

            }
            static void Math() //수학교사: 두 자리 + 두 자리  or 두 자리 - 두 자리를 30초안에 6개 이상 맞추기
            {
                float MathPoint = 0;

                int timerTime = 30;

                theThreadTimer(timerTime);

                for (float MRound = 0; ; MRound++)
                {
                    WriteLine(" ");

                    Random random = new Random();
                    int Math1 = random.Next(10, 100);
                    int PlusMinus = random.Next(1, 3);
                    int Math2 = random.Next(10, 100);

                    switch (PlusMinus)
                    {
                        case 1:
                            Write($"{Math1}+{Math2}= ");

                            float Input11 = 0;
                            Input11 = float.Parse(ReadLine());


                            if (Input11 == (Math1 + Math2))
                            {
                                WriteLine("정답!");
                                MathPoint += 1;
                            }
                            else if (Input11 != (Math1 + Math2))
                            {
                                WriteLine("땡!");
                            }
                            break;

                        case 2:
                            Write($"{Math1}-{Math2}= ");

                            float Input12 = 0;
                            Input12 = float.Parse(ReadLine());


                            if (Input12 == (Math1 - Math2))
                            {
                                WriteLine("정답!");
                                MathPoint += 1;
                            }
                            else if (Input12 != (Math1 - Math2))
                            {
                                WriteLine("땡!");
                            }
                            break;

                    }
                    if (isTimeOut == true) //시간이 다됐는지 확인
                    {
                        Thread.Sleep(1000);
                        if (MathPoint >= 6)
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Clear();
                            ForegroundColor = ConsoleColor.Black;
                            WriteLine($"축하합니다! {MathPoint}점으로 합격입니다.");
                            WriteLine(" ");
                            Thread.Sleep(1000);
                            WriteLine("당신은 피타고라스보다 덧셈,뺄셈을 잘하는 성공한 수학교사가 됐습니다."); // << 게임종료 아직구현 x
                        }
                        else if (MathPoint < 6)
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Clear();
                            ForegroundColor = ConsoleColor.Black;
                            WriteLine($"아쉽지만, {MathPoint}점으로 불합격입니다.");
                            Thread.Sleep(5000);

                            // Chance -= 1; << 아직구현 x
                            StartPlayer();
                        }


                    }


                }


            }
            static void Speed() //프로게이머: 출력된 문자를 빠르게 쳐서 30초안에 60점이상넘기기
            {
                float SpeedPoint = 0;

                int timerTime = 30;

                theThreadTimer(timerTime);

                for (float SRound = 0; ; SRound++)
                {
                    Random random = new Random();
                    int C = random.Next(1, 9);



                    switch (C)
                    {

                        case 1:
                            WriteLine("▲");     //C에 만약 1인 담긴다면, ▲가 출력되고, 플레이어가 UpArrow키를 누르면 1점획득
                            ConsoleKeyInfo key1 = Console.ReadKey(true); //실패코드
                            switch (key1.Key)
                            {
                                case ConsoleKey.UpArrow:
                                    SpeedPoint += 1;
                                    break;
                            }
                            break;
                        case 2:
                            WriteLine("▼");
                            ConsoleKeyInfo key2 = Console.ReadKey(true); //실패코드
                            switch (key2.Key)
                            {
                                case ConsoleKey.DownArrow:
                                    SpeedPoint += 1;
                                    break;
                            }
                            break;
                        case 3:
                            WriteLine("◀");
                            ConsoleKeyInfo key3 = Console.ReadKey(true); //실패코드
                            switch (key3.Key)
                            {
                                case ConsoleKey.LeftArrow:
                                    SpeedPoint += 1;
                                    break;
                            }
                            break;
                        case 4:
                            WriteLine("▶");
                            ConsoleKeyInfo key4 = Console.ReadKey(true); //실패코드
                            switch (key4.Key)
                            {
                                case ConsoleKey.RightArrow:
                                    SpeedPoint += 1;
                                    break;
                            }
                            break;
                        case 5:
                            WriteLine("𝐖");
                            ConsoleKeyInfo key5 = Console.ReadKey(true); //실패코드
                            switch (key5.Key)
                            {
                                case ConsoleKey.W:
                                    SpeedPoint += 1;
                                    break;
                            }
                            break;
                        case 6:
                            WriteLine("𝐀");
                            ConsoleKeyInfo key6 = Console.ReadKey(true); //실패코드
                            switch (key6.Key)
                            {
                                case ConsoleKey.A:
                                    SpeedPoint += 1;
                                    break;
                            }
                            break;
                        case 7:
                            WriteLine("𝐒");
                            ConsoleKeyInfo key7 = Console.ReadKey(true); //실패코드
                            switch (key7.Key)
                            {
                                case ConsoleKey.S:
                                    SpeedPoint += 1;
                                    break;
                            }
                            break;
                        case 8:
                            WriteLine("𝐃");
                            ConsoleKeyInfo key8 = Console.ReadKey(true); //실패코드
                            switch (key8.Key)
                            {
                                case ConsoleKey.D:
                                    SpeedPoint += 1;
                                    break;
                            }
                            break;
                    }
                    if (isTimeOut == true) //시간이 다됐는지 확인
                    {
                        Thread.Sleep(1000);
                        if (SpeedPoint >= 60)
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Clear();
                            ForegroundColor = ConsoleColor.White;
                            WriteLine($"축하합니다! {SpeedPoint}점으로 합격입니다.");
                            WriteLine(" ");
                            Thread.Sleep(1000);
                            WriteLine("당신은 페이커보다 반응속도가 좋은 프로게이머가 됐습니다.");
                            //게임종료 아직구현 x
                        }
                        else if (SpeedPoint < 60)
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Clear();
                            ForegroundColor = ConsoleColor.White;
                            WriteLine($"아쉽지만, {SpeedPoint}으로 불합격입니다.");
                            Thread.Sleep(5000);
                            // Chance -= 1; << 똑같이 필요 아직구현 x
                            StartPlayer();
                        }

                    }

                }


            }
            static void Memory() //변호사: 8자리숫자 3개를 외우고, 랜덤으로 출력된 n번째 숫자를 답하기 (3개 모두 통과시 합격)
            {
                for (float Memo = 1; Memo < 4; Memo++) //8자리 숫자 3개 뽑기
                {
                    Random random = new Random();
                    int M1 = random.Next(10000000, 99999999);
                    int M2 = random.Next(10000000, 99999999);
                    int M3 = random.Next(10000000, 99999999);
                    Clear();

                    WriteLine(" ");
                    WriteLine($"A:{M1}");
                    Thread.Sleep(5000);
                    Clear();        //위에 A 지우는 코드

                    WriteLine(" ");
                    WriteLine($"B:{M2}");
                    Thread.Sleep(5000);
                    Clear();        //위에 B 지우는 코드                                   

                    WriteLine(" ");
                    WriteLine($"C:{M3}");
                    Thread.Sleep(5000);
                    Clear();        //위에 C 지우는 코드                                         


                    float RandABC = random.Next(1, 4); // 랜덤으로 1,2,3 받기



                    switch (RandABC)
                    {
                        case 1:
                            WriteLine("A는 몇인가요?");
                            float ARead = 0;
                            ARead = float.Parse(ReadLine());
                            if (ARead == M1)
                            {
                                WriteLine(" ");
                                WriteLine($"정답입니다! (3/{Memo})");
                            }
                            else if (ARead != M1)
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                                Clear();
                                ForegroundColor = ConsoleColor.Black;
                                WriteLine(" ");
                                WriteLine("아쉽지만, 다음기회에...");
                                Thread.Sleep(5000);
                                //Chance -=1   <<아직구현 x
                                StartPlayer();
                            }
                            break;
                        case 2:
                            WriteLine("B는 몇인가요?");
                            float BRead = 0;
                            BRead = float.Parse(ReadLine());
                            if (BRead == M2)
                            {
                                WriteLine(" ");
                                WriteLine($"정답입니다! (3/{Memo})");
                            }
                            else if (BRead != M2)
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                                Clear();
                                ForegroundColor = ConsoleColor.Black;
                                WriteLine(" ");
                                WriteLine("아쉽지만, 다음기회에...");
                                Thread.Sleep(5000);
                                //Chance -=1   <<아직구현 x
                                StartPlayer();
                            }
                            break;
                        case 3:
                            WriteLine("C는 몇인가요?");
                            float CRead = 0;
                            CRead = float.Parse(ReadLine());
                            if (CRead == M3)
                            {
                                WriteLine(" ");
                                WriteLine($"정답입니다! (3/{Memo})");
                            }
                            else if (CRead != M3)
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                                Clear();
                                ForegroundColor = ConsoleColor.Black;
                                WriteLine(" ");
                                WriteLine("아쉽지만, 다음기회에...");
                                Thread.Sleep(5000);
                                //Chance -=1   <<아직구현 x
                                StartPlayer();
                            }
                            break;

                    }


                }
                Console.BackgroundColor = ConsoleColor.Yellow;
                Clear();
                ForegroundColor = ConsoleColor.Black;
                WriteLine("축하합니다! 합격입니다.");
                WriteLine(" ");
                Thread.Sleep(1000);
                WriteLine("당신은 우영우 변호사보다 기억력이 좋은 대형로펌의 변호사가 되었습니다.");
                //게임종료 아직구현 x
                Thread.Sleep(5000);
                StartPlayer();

            }

        }
        static void LuckColor()
        {
            Random Rand = new Random(); //랜덤값 사용

            ConsoleColor[] Color = {ConsoleColor.Red,ConsoleColor.Green,ConsoleColor.Yellow
            ,ConsoleColor.White , ConsoleColor.DarkGray,ConsoleColor.Blue}; //배경색과 폰트색을 랜덤하게 바꿀 색상들
            for (byte i = 0; i < 10; i++)
            {
                Console.Clear();
                
                Console.BackgroundColor = Color[Rand.Next(6)]; //0~5까지의 랜덤한 색상으로 배경색변경
                Thread.Sleep(200); //0.2초대기
            }

        }

    }

}
