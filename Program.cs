﻿namespace spartagame
{
    public class Character
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Gold { get; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }
    }

    public class Item
    {
        public string Name { get; }
        public string Description { get; }
        public int Type { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public bool IsEquiped { get; set; }

        public static int ItemCnt = 0;

        public Item(string name, string description, int type, int atk, int def, int hp, bool isEquiped = false)
        {
            Name = name;
            Description = description;
            Type = type;
            Atk = atk;
            Def = def;
            Hp = hp;
            IsEquiped = isEquiped;
        }
        public void PrintItemStatDescription(bool withNumber=false, int idx = 0)
        {
            Console.Write("-");
            if (withNumber)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("{0} ", idx);
                Console.ResetColor();
            }
            if (IsEquiped)
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("E");
                Console.ResetColor();
                Console.Write("]");
                Console.Write(PadRightForMixedText(Name, 9));
            }
            else Console.Write(PadRightForMixedText(Name, 12));
            Console.Write(" | ");

            if(Atk !=0) Console.Write($"공격력 {(Atk >= 0 ? "+" : "")}{Atk}");
            if (Def != 0) Console.Write($"방어력 {(Def >= 0 ? "+" : "")}{Def}");
            if (Hp != 0) Console.Write($"체력 {(Hp >= 0 ? "+" : "")} {Hp}");

            Console.Write(" | ");

            Console.WriteLine(Description);
        }
        public static int GetPrintableLength(string str)
        {
            int length = 0;
            foreach (char c in str)
            {
                if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    length += 2; 
                }
                else
                {
                    length += 1; 
                }
            }

            return length;
        }

        public static string PadRightForMixedText(string str, int totalLength)
        {
            int currentLength = GetPrintableLength(str);
            int padding = totalLength - currentLength;
            return str.PadRight(str.Length + padding);
        }

        internal class Program
        {
            static Character _player;
            static Item[] _items;
            static void Main(string[] args)
            {
                GameDataSetting();
                PrintStartLogo();
                StartMenu();
            }
            private static void PrintStartLogo()
            {
                // ASCII ART GENERATED BY https://textkool.com/en/ascii-art-generator?hl=default&vl=default&font=Red%20Phoenix
                Console.WriteLine(".___     _________     _____    _______       ________   ________    \r\n|   |    \\_   ___ \\   /  _  \\   \\      \\      \\______ \\  \\_____  \\   \r\n|   |    /    \\  \\/  /  /_\\  \\  /   |   \\      |    |  \\  /   |   \\  \r\n|   |    \\     \\____/    |    \\/    |    \\     |    `   \\/    |    \\ \r\n|___|     \\______  /\\____|__  /\\____|__  /    /_______  /\\_______  / \r\n                 \\/         \\/         \\/             \\/         \\/  \r\n                                                                     ");
                Console.WriteLine("=============================================================================");
                Console.WriteLine("                           PRESS ANYKEY TO START                             ");
                Console.WriteLine("=============================================================================");
                Console.ReadKey();
            }

            private static void GameDataSetting()
            {
                _player = new Character("chad", "전사", 1, 10, 5, 100, 1500);
                _items = new Item[10];
                //List<Item> items = new List<Item>();
                AddItem(new Item("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 0, 5, 0));
                AddItem(new Item("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 1, 2, 0, 0));

                //items.Add("골든 헬름", "희귀한 광석으로 만들어진 투구입니다.", 1, 0, 9, 0);
            }

            static void AddItem(Item item)
            {
                if (Item.ItemCnt == 10) return;
                _items[Item.ItemCnt] = item;
                Item.ItemCnt++;
            }
            private static void StartMenu()
            {
                Console.Clear();
                Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
                Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
                Console.WriteLine("");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("");

                switch (CheckValidInput(1, 2))
                {
                    case 1:
                        StatusMenu();
                        break;
                    case 2:
                        InventoryMenu();
                        break;
                }
            }
    
            private static void InventoryMenu()
            {
                Console.Clear();

                ShowHighlightedText("■ 인벤토리 ■");
                Console.WriteLine("보유 중인 아이템을 관리 할 수 있습니다.");
                    Console.WriteLine("");
                Console.WriteLine("[아이템 목록]");

                for (int i = 0; i < Item.ItemCnt; i++)
                {
                    _items[i].PrintItemStatDescription();
                }
                Console.WriteLine("");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("1. 장착관리");
                Console.WriteLine("");
                switch(CheckValidInput(0, 1))
                {
                    case 0:
                        StartMenu();
                        break; 
                    case 1:
                        EquipMenu();
                        break;
                }
            }

            private static void EquipMenu()
            {
                Console.Clear();
                ShowHighlightedText("인벤토리 - 장착관리");
                Console.WriteLine("보유중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine("");
                Console.WriteLine("[아이템 목록]");
                for(int i = 0;i < Item.ItemCnt; i++)
                {
                    _items[i].PrintItemStatDescription(true, i+1);
                }
                Console.WriteLine("");
                Console.WriteLine("0. 나가기");

                int keyInput = CheckValidInput(0, Item.ItemCnt);

                switch (keyInput)
                {
                    case 0:
                        InventoryMenu();
                        break;
                    default:
                        ToggleEquipStatus(keyInput - 1);
                        EquipMenu();
                        break;
                }
            }
            static void ToggleEquipStatus(int idx)
            {
                _items[idx].IsEquiped = !_items[idx].IsEquiped;
            }

            private static void StatusMenu()
            {
                Console.Clear();

                ShowHighlightedText("상태보기");
                Console.WriteLine("캐릭터의 정보가 표기됩니다.");

                PrintTextWithHighlights("Lv. ", _player.Level.ToString("00"));
                Console.WriteLine("");
                Console.WriteLine("{0} ( {1} )", _player.Name, _player.Job);

                int bonusAtk = getSumBonusAtk();

                PrintTextWithHighlights("공격력 : ", (_player.Atk + bonusAtk).ToString(), bonusAtk > 0 ? string.Format(" (+{0})", bonusAtk) : "");
                int bonusDef = getSumBonusDef();
                PrintTextWithHighlights("방어력 : ", (_player.Def + bonusDef).ToString(), bonusDef > 0 ? string.Format(" (+{0})", bonusDef) : "");
                int bonusHp = getSumBonusHp();
                PrintTextWithHighlights("체력 : ",   (_player.Hp + bonusHp).ToString(), bonusHp > 0 ? string.Format(" (+{0})", bonusHp) : "");
                PrintTextWithHighlights("골드 : ", _player.Gold.ToString());
                Console.WriteLine("");
                Console.WriteLine("0. 뒤로가기");
                Console.WriteLine("");
                switch (CheckValidInput(0, 0))
                {
                    case 0:
                        StartMenu();
                        break;
                }
            }
            private static int getSumBonusAtk()
            {
                int sum = 0;
                for (int i = 0; i < Item.ItemCnt; i++)
                {
                    if (_items[i].IsEquiped) sum += _items[i].Atk;
                }
                return sum;
            }
            private static int getSumBonusDef()
            {
                int sum = 0;
                for (int i = 0; i < Item.ItemCnt; i++)
                {
                    if (_items[i].IsEquiped) sum += _items[i].Def;
                }
                return sum;
            }
            private static int getSumBonusHp()
            {
                int sum = 0;
                for (int i = 0; i < Item.ItemCnt; i++)
                {
                    if (_items[i].IsEquiped) sum += _items[i].Hp;
                }
                return sum;
            }
        }
   
        private static void ShowHighlightedText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        private static void PrintTextWithHighlights(string s1, string s2, string s3 = "")
        {
            Console.Write(s1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(s2);
            Console.ResetColor();
            Console.WriteLine(s3);
        }
    
            private static int CheckValidInput(int min, int max)
            {
                int keyInput;
                bool result;
                do
                {
                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                    result = int.TryParse(Console.ReadLine(), out keyInput);
                } 
                while (result == false || CheckIfValid(keyInput, min, max) == false);

                return keyInput;
            }

            private static bool CheckIfValid(int keyinput, int min, int max)
            {
                if (min <= keyinput && keyinput <= max) return true;
                return false;
            }
        }
}