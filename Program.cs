var fullInput =
@"iwrupvqb";

var smallInput =
@"abcdef";

var smallest = "";

var input = smallInput;
input = fullInput;
//input = smallest;
var timer = System.Diagnostics.Stopwatch.StartNew();

var result = 0l;

while (true)
{
    if (CreateMD5(input + result.ToString()).StartsWith("000000"))
    {
        break;
    }
    result++;
}

timer.Stop();
Console.WriteLine(result);
Console.WriteLine(timer.ElapsedMilliseconds + "ms");
Console.ReadLine();

void PrintGrid<T>(T[][] grid)
{
    for (int i = 0; i < grid.Length; i++)
    {
        for (int j = 0; j < grid[i].Length; j++)
        {
            Console.Write(grid[i][j]);
        }
        Console.WriteLine();
    }
}

// https://stackoverflow.com/questions/11454004/calculate-a-md5-hash-from-a-string
string CreateMD5(string input)
{
    // Use input string to calculate MD5 hash
    using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
    {
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hashBytes = md5.ComputeHash(inputBytes);

        return Convert.ToHexString(hashBytes); // .NET 5 +

        // Convert the byte array to hexadecimal string prior to .NET 5
        // StringBuilder sb = new System.Text.StringBuilder();
        // for (int i = 0; i < hashBytes.Length; i++)
        // {
        //     sb.Append(hashBytes[i].ToString("X2"));
        // }
        // return sb.ToString();
    }
}