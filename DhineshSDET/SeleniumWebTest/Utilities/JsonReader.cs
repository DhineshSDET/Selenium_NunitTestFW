using Newtonsoft.Json.Linq;

namespace SeleniumTest.Utilities
{
    public class JsonReader
    {
        public JsonReader()
        {

        }

        public String ExtractStringData(String token)
        {
            String jsonString = File.ReadAllText("TestData/testData.json");
            var jsonObject = JToken.Parse(jsonString);
            return jsonObject.SelectToken(token).Value<string>();
        }
        public String[] ExtractArrayData(String token)
        {
            String jsonString = File.ReadAllText("TestData/testData.json");
            var jsonObject = JToken.Parse(jsonString);
            List<string> productList = jsonObject.SelectTokens(token).Values<string>().ToList();
            return productList.ToArray();
        }
    }
}
