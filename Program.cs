using Standard.AI.Data;
using System.ComponentModel;

public class Program
{
   static string openAIKeyVar = "key";



    static async Task Main(string[] args)
    {
        for (; ; )
        {
            try
            {
                Console.WriteLine("Please Enter your request: ");
                string query = Console.ReadLine();

                var results = await new AIDataClient(
                    openAIKey: openAIKeyVar,
                    connectionString: "Server=ALIKOLAHDOOZAN;Database=TaxDataBase;Trusted_Connection=True;MultipleActiveResultSets=true")
                        .RunAIQueryAsync(query);

                PrintDynamicList(results);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Couldn't perform this query. Try to rephrase and be as specific as possible.");
            }
        }
    }

    private static void PrintDynamicList(IEnumerable<dynamic> dynamicList)
    {
        List<string> list = new List<string>();

        if (dynamicList.Count() < 1)
        {
            Console.WriteLine("We couldn't find any matching records.");
        }
        else
        {
            foreach (var dynamicObject in dynamicList)
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(dynamicObject))
                {
                    string name = descriptor.Name;
                    object value = descriptor.GetValue(dynamicObject);

                    if (list.Contains(name + value) is false)
                    {
                        list.Add(name + value);
                        Console.WriteLine("{0}: {1}", name, value);
                    }
                }
            }
        }
    }
}