namespace _0426_Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataStructure.Dictionary<string, int> dictionary = new DataStructure.Dictionary<string, int>();
            dictionary.Add("a", 1);
            dictionary.Add("b", 2);
            dictionary.Add("c", 3);
            dictionary.Add("d", 4);

            if(dictionary.TryGetValue("a", out int value)) { Console.WriteLine(value); }
            dictionary["a"] = 0;
            Console.WriteLine(dictionary["a"]);

            //dictionary.Add("b", 3); // 중복 값 삽입
            //dictionary["e"] = 5; // 없는 값 set
            //Console.WriteLine(dictionary["e"]); // 없는 값 접근
            dictionary.Remove("a");
            //Console.WriteLine(dictionary["a"]); // 삭제한 값 접근
        }
    }
}