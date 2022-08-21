using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace G_project1
{
    class Program
    {
        static Random rand = new Random();
        static List<int> Check = new List<int>(); //카드 

        static IntPtr ConsoleWindowHnd = GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("User32.Dll")]
        private static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);
        const int VK_RETURN = 0x0D;
        const int WM_KEYDOWN = 0x100;

        static bool isNext = false;


        static bool isTimeOut = false;  //시간이 모두 지났는지 체크

        static void Main(string[] args)
        {
            

            Console.SetWindowSize(150, 40);

            Menu();



        }
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




        static int Card(int sta, int end) //랜덤으로 뽑은 값의 중복을 판단
        {
            int temp = rand.Next(sta, end);

            if (Check.Contains(temp) == true) // Check 라는 리스트에 값이 있는 경우
            {
                return Card(sta, end);
            }
            else
            {
                Check.Add(temp);
                return temp;
            }

        }
        static void Ingame()  //게임시작
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Clear();
            ForegroundColor = ConsoleColor.Yellow;

            int ResultUswerPoint = 0;   //유저 최종 답

            for (float Round = 1; Round < 11; Round++)
            {
                Random Dice = new Random();
                List<int> randomNumber = new List<int>      // 주사위 3개
                {
                    Dice.Next(1, 12),    //주사위 1번
                    Dice.Next(1, 12),  //주사위 2번
                    Dice.Next(1, 12)  //주사위 3번
                };

                List<float> userPutAnswerList = new List<float>();
                userPutAnswerList = RoundMainGame(Round, randomNumber); //유저가 입력한 피라카드들

                ResultUswerPoint += RoundUserCalcWhyPutThisAnswer(userPutAnswerList, randomNumber); //이번 라운드에서 얻은 점수 추가하기

            }

        }

        static List<float> RoundMainGame(float Round, List<int> randomNuber)
        {
            List<float> userAnswerList = new List<float>();

            Random rand = new Random();
            int randomNumber1 = Card(70, 90);   //1층 1번

            int randomNumber2 = Card(50, 69);   //2층 1번
            int randomNumber22 = Card(50, 69);  //2층 2번

            int randomNumber3 = Card(30, 49);   //3층 1번
            int randomNumber32 = Card(30, 49);  //3층 2번
            int randomNumber33 = Card(30, 49);  //3층 3번

            int randomNumber4 = Card(1, 29);    //4층 1번
            int randomNumber42 = Card(1, 29);   //4층 2번
            int randomNumber43 = Card(1, 29);   //4층 3번
            int randomNumber44 = Card(1, 29);   //4층 4번


            WriteLine(" ");
            WriteLine(" ");
            WriteLine(" ");
            WriteLine(" ");
            WriteLine($"{Round} 라운드");

            WriteLine(" ");
            WriteLine(" ");
            WriteLine(" ");
            WriteLine(" ");
            WriteLine("                                                         ★                                               ");
            WriteLine("                                                       ★  ★                                             ");
            WriteLine("                                                     ★      ★                                           ");
            WriteLine("                                                   ★          ★                                         ");
            WriteLine($"                                                 ★      {randomNumber1}      ★                         ");
            WriteLine("                                               ★                  ★                                     ");
            WriteLine("                                             ★                      ★                                   ");
            WriteLine("                                           ★  ■■■■■■■■■■■  ★                                 ");
            WriteLine("                                         ★      ▼▼▼▼▼▼▼▼▼      ★                               ");
            WriteLine($"                                       ★    {randomNumber2}    ▼▼▼▼▼▼▼    {randomNumber22}    ★");
            WriteLine("                                     ★              ▼▼▼▼▼              ★");
            WriteLine("                                   ★                  ▼▼▼                  ★                         ");
            WriteLine("                                 ★■■■■■■■■■■■■■■■■■■■■■■■★                ");
            WriteLine("                               ★  ◆◆◆◆◆◆◆◆◆◆◆  ◆◆◆◆◆◆◆◆◆◆◆  ★                          ");
            WriteLine("                             ★      ◆◆◆◆◆◆◆◆◆      ◆◆◆◆◆◆◆◆◆      ★                             ");
            WriteLine($"                           ★    {randomNumber3}    ◆◆◆◆◆◆◆    {randomNumber32}    ◆◆◆◆◆◆◆    {randomNumber33}    ★");
            WriteLine("                         ★              ◆◆◆◆◆              ◆◆◆◆◆              ★");
            WriteLine("                       ★                  ◆◆◆                  ◆◆◆                  ★                         ");
            WriteLine("                     ★■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■★             ");
            WriteLine("                   ★                  ★                  ★                  ★              ★                   ");
            WriteLine("                 ★●                  ●                  ●                  ●                ★                                           ");
            WriteLine($"               ★●●●      {randomNumber4}      ●●●      {randomNumber42}      ●●●      {randomNumber43}      ●●●      {randomNumber44}       ●★");
            WriteLine("             ★●●●●●          ●●●●●          ●●●●●          ●●●●●          ●●●★");
            WriteLine("           ★●●●●●●●      ●●●●●●●      ●●●●●●●      ●●●●●●●      ●●●●●★");
            WriteLine("         ★●●●●●●●●●  ●●●●●●●●●  ●●●●●●●●●  ●●●●●●●●●  ●●●●●●●★ ");
            WriteLine("       ★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★ ");
            WriteLine(" ");
            WriteLine(" ");
            WriteLine(" ");
            WriteLine($"          주사위 :       {randomNuber[0]}      {randomNuber[1]}      {randomNuber[2]}               ");
            Write("                      =======================                                                            ");

            WriteLine(" ");
            WriteLine(" ");

            Write("                                                                                                    입력1: ");
            float f1 = 0;
            f1 = float.Parse(ReadLine());
            if (isTimeOut == true) //시간이 다됐는지 체크
            {
                return userAnswerList;
            }
            while (f1 != randomNumber1 & f1 != randomNumber2 & f1 != randomNumber22 & f1 != randomNumber3 & f1 != randomNumber32 & f1 != randomNumber33 & f1 != randomNumber4 & f1 != randomNumber42 & f1 != randomNumber43 & f1 != randomNumber44)
            {
                Console.WriteLine("입력하신 파라카드가 없습니다.");

                Write("                                                                                                    입력1: ");
                f1 = float.Parse(ReadLine());
                if (isTimeOut == true)
                {
                    return userAnswerList;
                }
            }
            userAnswerList.Add(f1);

            Write("                                                                                                    입력2: ");
            float f2 = 0;
            f2 = float.Parse(ReadLine());
            if (isTimeOut == true)
            {
                return userAnswerList;
            }
            while (f2 != randomNumber1 & f2 != randomNumber2 & f2 != randomNumber22 & f2 != randomNumber3 & f2 != randomNumber32 & f2 != randomNumber33 & f2 != randomNumber4 & f2 != randomNumber42 & f2 != randomNumber43 & f2 != randomNumber44)
            {
                Console.WriteLine("입력하신 파라카드가 없습니다.");

                Write("                                                                                                    입력2: ");
                f2 = float.Parse(ReadLine());
                if (isTimeOut == true)
                {
                    return userAnswerList;
                }
            }
            userAnswerList.Add(f2);

            Write("                                                                                                    입력3: ");
            float f3 = 0;
            f3 = float.Parse(ReadLine());
            if (isTimeOut == true)
            {
                return userAnswerList;
            }
            while (f3 != randomNumber1 & f3 != randomNumber2 & f3 != randomNumber22 & f3 != randomNumber3 & f3 != randomNumber32 & f3 != randomNumber33 & f3 != randomNumber4 & f3 != randomNumber42 & f3 != randomNumber43 & f3 != randomNumber44)
            {
                Console.WriteLine("입력하신 파라카드가 없습니다.");

                Write("                                                                                                    입력3: ");
                f3 = float.Parse(ReadLine());
                if (isTimeOut == true)
                {
                    return userAnswerList;
                }
            }
            userAnswerList.Add(f3);

            Write("                                                                                                    입력4: ");
            float f4 = 0;
            f4 = float.Parse(ReadLine());
            if (isTimeOut == true)
            {
                return userAnswerList;
            }
            while (f4 != randomNumber1 & f4 != randomNumber2 & f4 != randomNumber22 & f4 != randomNumber3 & f4 != randomNumber32 & f4 != randomNumber33 & f4 != randomNumber4 & f4 != randomNumber42 & f4 != randomNumber43 & f4 != randomNumber44)
            {
                Console.WriteLine("입력하신 파라카드가 없습니다.");

                Write("                                                                                                    입력4: ");
                f4 = float.Parse(ReadLine());
                if (isTimeOut == true)
                {
                    return userAnswerList;
                }
            }
            userAnswerList.Add(f4);

            Write("                                                                                                    입력5: ");
            float f5 = 0;
            f5 = float.Parse(ReadLine());
            if (isTimeOut == true)
            {
                return userAnswerList;
            }
            while (f5 != randomNumber1 & f5 != randomNumber2 & f5 != randomNumber22 & f5 != randomNumber3 & f5 != randomNumber32 & f5 != randomNumber33 & f5 != randomNumber4 & f5 != randomNumber42 & f5 != randomNumber43 & f5 != randomNumber44)
            {
                Console.WriteLine("입력하신 파라카드가 없습니다.");

                Write("                                                                                                    입력5: ");
                f5 = float.Parse(ReadLine());
                if (isTimeOut == true)
                {
                    return userAnswerList;
                }
            }
            userAnswerList.Add(f5);

            Write("                                                                                                    입력6: ");
            float f6 = 0;
            f6 = float.Parse(ReadLine());
            if (isTimeOut == true)
            {
                return userAnswerList;
            }

            while (f6 != randomNumber1 & f6 != randomNumber2 & f6 != randomNumber22 & f6 != randomNumber3 & f6 != randomNumber32 & f6 != randomNumber33 & f6 != randomNumber4 & f6 != randomNumber42 & f6 != randomNumber43 & f6 != randomNumber44)
            {
                Console.WriteLine("입력하신 파라카드가 없습니다.");

                Write("                                                                                                    입력6: ");
                f6 = float.Parse(ReadLine());
                if (isTimeOut == true)
                {
                    return userAnswerList;
                }
            }
            userAnswerList.Add(f6);


            return userAnswerList;
        }


        //userPutAnswerList - 유저가 입력한 피라카드 숫자
        //randomNumber - 주사위 3개 숫자
        static int RoundUserCalcWhyPutThisAnswer(List<float> userPutAnswerList, List<int> randomNumber)
        {
            /*
        * 타임종료시 
        {
        다시 타이머 카드당4/8/12초 세고
        “입력한 카드 {카드}를 풀이하세요”
        R1=리드라인
        Ex) R1=4*7-2

        R1을 연산처리하기
        If(R1 연산값==파라카드)
        {
        점수 6/5/4/3 +
        “얻었습니다”
        }
        else if(틀리다면)
        {
        점수 6/5/4/3 -
        “차감됐습니다”
        }

        “현재점수:~”

        “파라오:만들 수 있었던 {카드1} …을 가져갑니다.”
        “파라오:(카드풀이)”
        “파라오가 ~점을 획득합니다.”

        다음라운드 진행하기

        } */
            //이번 라운드 총점
            int thisRoundPoint = 0;

            //유저가 넣은 피라카드 하나씩 모두 체크
            foreach (float pyramidCard in userPutAnswerList)
            {
                string userInput = "";

                //타이머 8초
                theThreadTimer(8);

                //유저가 계산식을 입력함
                WriteLine("1 번 기입했던 숫자 : " + pyramidCard.ToString());
                WriteLine("계산식 기입해주세요. 예시 -> 4*7+3");
                Write("정답 기입(*, %부터 기입, +, -는 뒤에) :");
                userInput = ReadLine();

                //유저의 입력식 계산하기
                int result = 0;
                if (userInput.Length == 5)   //숫자가 3개인 경우
                {
                    switch (userInput[1])
                    {
                        case '*':
                            result = ((int)userInput[0] - '0') * ((int)userInput[2] - '0');
                            break;
                        case '/':
                            result = ((int)userInput[0] - '0') / ((int)userInput[2] - '0');
                            break;
                            break;
                        case '+':
                            result = ((int)userInput[0] - '0') + ((int)userInput[2] - '0');
                            break;
                        case '-':
                            result = ((int)userInput[0] - '0') - ((int)userInput[2] - '0');
                            break;
                        default:
                            WriteLine("입력값 오류");

                            break;
                    }
                    switch (userInput[3])
                    {
                        case '*':
                            result = result * ((int)userInput[4] - '0');
                            break;
                        case '/':
                            result = result / ((int)userInput[4] - '0');
                            break;
                            break;
                        case '+':
                            result = result + ((int)userInput[4] - '0');
                            break;
                        case '-':
                            result = result - ((int)userInput[4] - '0');
                            break;
                        default:
                            WriteLine("입력값 오류");
                            break;
                    }
                }
                else if (userInput.Length == 3)  //숫자가 2개인 경우
                {
                    switch (userInput[1])
                    {
                        case '*':
                            result = ((int)userInput[0] - '0') * ((int)userInput[2] - '0');
                            break;
                        case '/':
                            result = ((int)userInput[0] - '0') / ((int)userInput[2] - '0');
                            break;
                            break;
                        case '+':
                            result = ((int)userInput[0] - '0') + ((int)userInput[2] - '0');
                            break;
                        case '-':
                            result = ((int)userInput[0] - '0') - ((int)userInput[2] - '0');
                            break;
                        default:
                            WriteLine("입력값 오류");

                            break;
                    }
                }
                else //그외는 받지않음
                {
                    Write("입력 문제");
                    continue;
                }

                if ((int)pyramidCard == result)
                {
                    //정답
                    thisRoundPoint += 6;
                }
                else
                {
                    //틀림
                    thisRoundPoint -= 6;
                }

                //타임아웃
                if (isTimeOut)
                {
                    //? 이게 언제 필요한지 모르겠음
                }

            }
            //이번 라운드의 총점 반환하기
            return thisRoundPoint;
        }

        static void Menu() //메인화면
        {


            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Clear();
            ForegroundColor = ConsoleColor.Black;

            WriteLine("스마트파라오            ");
            WriteLine(" ");
            WriteLine(" ");
            WriteLine("게임설명(1)");
            WriteLine("게임시작(2)");
            WriteLine("게임나가기(3)");
            WriteLine(" ");
            WriteLine(" ");
            string R1 = ReadLine();
            WriteLine(" ");

            switch (R1)
            {
                case "1":
                    Explanation();

                    break;
                case "2":
                    Ingame();
                    break;
                default:
                    break;
            }
        }
        static void Explanation()   //게임룰
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Clear();
            ForegroundColor = ConsoleColor.Yellow;

            WriteLine("1. 매 라운드마다 4층으로 나눠진 카드 10장이 나열되며 층마다 6/5/4/3의 점수를 가지고, 3개의 랜덤한 주사위를 던집니다.");
            WriteLine(" ");
            WriteLine("2. 주사위 2개이상을 1번씩 활용하고 사칙연산을 하여 나열된 카드를 만들어 입력합니다. ex) 10 (enter) 20(enter) ~");
            WriteLine(" ");
            WriteLine("3. 난이도별로 10/20/30초가 지나면 라운드가 끝나고, 파라오가 만들 수 있었던 카드를 모두 획득하며 해설합니다.");
            WriteLine(" ");
            WriteLine("4. 매 라운드가 종료될때마다, 난이도별로 카드당 4/8/12초를 제한하여 카드를 해설합니다.");
            WriteLine(" ");
            WriteLine("5. 정답이 맞다면 해당카드층의 점수획득, 해설을 못하거나 만들 수 없는 카드라면 해당카드층 점수차감됩니다.");
            WriteLine(" ");
            WriteLine("6. 모든 라운드가 종료된 후, 파라오의 점수와 플레이어 점수를 비교하여 승패가 결정납니다.");
            WriteLine(" ");
            WriteLine(" ");
            WriteLine("메뉴로 돌아가기(1)");

            float R2;
            R2 = float.Parse(ReadLine());

            switch (R2)
            {
                case 1:
                    Menu();
                    break;
                default:
                    break;
            }

        }





    }

}