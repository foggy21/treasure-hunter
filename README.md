# Искатель Сокровищ
<p style="font-size:16px">Цель игры: собрать все сокровища на локации и остаться в живых</p>

---

## Задачи
1. Создать карту-лабиринт с сокровищами и замаскированными ловушками, разместить на карте игрока.
2. Сделать движение игрока по карте, игрок должен сталкиваться со стенами.
3. У игрока должны быть очки здоровья. При столкновении с маскированной ловушкой, игрок теряет очко здоровья, а ловушка перестаёт быть замаскированной. Игрок не может столкнуться с одной и той же ловушкой больше одного раза.
4. Очки здоровья игрока должны отображаться в консоли.
5. Если игрок потеряет все очки здоровья - игра начинается сначала: игрок появляется там, откуда двигался сначала уровня. Взорванные ловушки не появляются на карте, замаскированные невозорванные ловушки остаются на карте. Игровой цикл начинается сначала.
6. У игрока должна быть сумка, в которой отображаются собранные сокровища. При столкновении игрока с сокровищем, оно переносится к нему в сумку.
7. Если игрок собрал все сокровища - игра завершается.
8. Во время игры допускается три сметри игрока, т.е. игровой цикл может перезапуститься три раза, после того, как игрок умрёт четвертый раз - игра завершается проигрышем.
---

## Геймплей
<br/>

### ![Gameplay №1](https://user-images.githubusercontent.com/46190973/220566150-aff9610a-fe16-4b0c-a0d8-8417b923c10d.gif)
<br/>

### ![Gameplay №2](https://user-images.githubusercontent.com/46190973/220566278-994747d3-16d9-4445-8057-a64db94a8c45.gif)
<br/>

# Карта
Задача создания карты была в том, как быстро получать данные из неё о том, когда игрок наступил на ловушку или наткнулся на сокровища.<br/><br/>

>**Карта была создана на основе двумерного массива символов.**

<p style="font-size:16px">Почему?</p>
Другого альтернативного эффективного варианта было не видно. Тем более, карта была по задумке не должна быть большая.

```cs
static readonly char[,] map = new char[24, 27]
static readonly char[,] mapWithTraps = new char[24, 27]
```

Было сделано две карты: одна, на которой находятся сокровища, другая с помеченными ловушками.<br/>
Такое решение занимает много памяти, но зато дальнейшая эффективность работы алгоритма значительно увеличивается.

## Начало игры
Чтобы начать игру, необходимо поставить игрока на карту. Была создана такая рекурсивная функция, которая располагает игрока на какой-либо точке по оси X или Y относительно выбранного центра (по умолчанию центр - это координаты (1;1)).

```cs
static bool SetPlayerOnMap(ref int posX, ref int posY)
{
    if ((posX > map.GetUpperBound(map.Rank - 1) || posY > map.GetUpperBound(0)) ||
        (posX <= 0 || posY <= 0))
    {
        return false;
    }

    if (map[posY, posX] != wall)
    {
        startPosX = posX;
        startPosY = posY;
        return true;
    }

    if (posX < map.GetUpperBound(map.Rank - 1))
    {
        posX++;
        return SetPlayerOnMap(ref posX, ref posY);

    }
    else if (posY < map.GetUpperBound(0))
    {
        posY++;
        return SetPlayerOnMap(ref posX, ref posY);
    } 
    else
    {
        return false;
    }
}
```
Если игрока не удалось поставить, то игра не начнётся.

# Движение игрока
При написании кода использовался функциональный подход, поэтому множество инструкций было описано с помощью функций, включая и движение игрока.
```cs
static bool MoveOfPlayer(ref int posX, ref int posY)
{
    ConsoleKey userInput = Console.ReadKey().Key;
    int copyPosX = posX, copyPosY = posY;
    switch (userInput)
    {
        case ConsoleKey.A:
            copyPosX--;
            break;
        case ConsoleKey.D:
            copyPosX++;
            break;
        case ConsoleKey.W:
            copyPosY--;
            break;
        case ConsoleKey.S:
            copyPosY++;
            break;
    }

    if (map[copyPosY, copyPosX] != wall)
    {
        posX = copyPosX;
        posY = copyPosY;
        return true;
    }
    return false;
}
```
Функция возвращает булевое значение для того, чтобы программа понимала, сдвинулся ли игрок или нет, тогда нам не придётся делать лишние операции в лучшем случае (когда игрок не сдвинулся).<br/>
# Отображение
Все отображающие инструкции были вынесены в отдельные функциии для удобного использования:
```cs
static void DrawMap()
{
    Console.SetCursorPosition(0, 0);
    int lengthOfColumns = map.GetUpperBound(map.Rank - 1) + 1;
    int iterator = 0;
    foreach (char symbol in map)
    {
        Console.Write($"{symbol}");
        if (++iterator == lengthOfColumns)
        {
            Console.Write('\n');
            iterator = 0;
        }
    }
}

static void DrawPlayer(int posX, int posY)
{
    Console.SetCursorPosition(posX, posY);
    Console.Write(player);
}

static void DrawBar(int posX, int posY, int maxStatictic, int statictic, string nameOfBar = "", ConsoleColor color = ConsoleColor.Red)
{
    ConsoleColor defaultColor = Console.BackgroundColor;
    Console.SetCursorPosition(posX, posY);
    if (nameOfBar != "") Console.Write($" {nameOfBar}: ");
    Console.Write('[');
    for (int i = 1; i <= maxStatictic; i++)
    {
        if (i > statictic)
        {
            Console.BackgroundColor = defaultColor;
        }
        else
        {
            Console.BackgroundColor = color;
        }
        Console.Write('|');
    }
    Console.BackgroundColor = defaultColor;
    Console.Write(']');
}

static void DrawBag(char[] bag, int posX, int posY, string nameOfBag = "Your bag", ConsoleColor color = ConsoleColor.Magenta)
{
    ConsoleColor defaultColor = Console.ForegroundColor;
    Console.SetCursorPosition(posX, posY);
    Console.Write($" {nameOfBag}: [");
    Console.ForegroundColor = color;
    foreach (char item in bag)
    {
        Console.Write(item);
    }
    Console.ForegroundColor = defaultColor;
    Console.Write(']');
}
```

# Ещё кое-какие удобства
Все литералы и другие постоянные поля были вынесены за функцию Main, для удобного обращения к ним. Хоть они и по сути есть глобальные переменные, но зато они константы, которые можно поменять только в одном месте (но лучше этого не делать), за искулчением переменных startPosX и startPosY, но они неинициализированные с самого начала, поэтому я их вынес, как статические переменные.

```cs
const int maxCountOfTries = 3;
const int maxHealthOfPlayer = 5;
const char player = '@';
const char treasure = 'X';
const char trap = 'T';
const char wall = '#';
const char ground = ' ';
const char explodedTrap = '*';
static int startPosX, startPosY; // Start position of player.
```
