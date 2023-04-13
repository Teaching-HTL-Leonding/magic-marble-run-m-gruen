// Magic Marble 
// https://github.com/htl-leo-prog-1/programming_fundamentals_cs/tree/main/exams/2023-04-13-magic-marble-run


#region Main Program
if (CheckArgumentLineInput(args, out string errorMessage))
{
    GetSegmentAndTeleports(out int segmentCount, out int teleportCount, args);
    Console.WriteLine("Segment Count: " + segmentCount);
    Console.WriteLine("Teleport Count: " + teleportCount);
}
else { Console.WriteLine(errorMessage); }
#endregion

#region Methods
bool CheckArgumentLineInput(string[] args, out string errorMessage)
{
    errorMessage = string.Empty;
    if (args.Length < 1) { errorMessage = "Please enter a Input!"; return false; }
    else if (args.Length > 1) { errorMessage = "Please enter only one Input! Try removing spaces"; return false; }
    else { return true; }
}

void GetSegmentAndTeleports(out int segmentCount, out int teleportCount, string[] args)
{
    string input = args[0];
    segmentCount = teleportCount = 0;
    bool movingRight = true;
    var locations = new HashSet<int>();

    for (int i = 0; i < input.Length;)
    {
        if (!IsValidMarbleRun(i, locations)) { locations.Add(i); }
        else { System.Console.WriteLine("Loop dedectet!"); Environment.Exit(0);}
        char c = input[i];
        if (c == '<' || c == '>') { segmentCount++; }
        else if (char.IsAsciiDigit(c) || char.IsLetter(c))
        {
            i = GetTeleportCount(input, i, movingRight);
            teleportCount++;
        }
        if (input[i] == '<') { movingRight = false; }
        else if (input[i] == '>') { movingRight = true; }
        if (movingRight) { i++; }
        else { i--; }
    }
    segmentCount += teleportCount;
}

int GetTeleportCount(string input, int i, bool movingRight)
{
    if (movingRight)
    {
        if (input[i + 1] == 'x') { i = Convert.ToInt32(input.Substring(i + 2, 4), 16); }
        else { i = int.Parse(input.Substring(i, 4)); }
    }
    else
    {
        if (input[i - 4] == 'x') { i = Convert.ToInt32(input.Substring(i - 3, 4), 16); }
        else { i = int.Parse(input.Substring(i - 3, 4)); }
    }
    return i;
}

bool IsValidMarbleRun(int i, HashSet<int> locations) => locations.Contains(i);
#endregion
