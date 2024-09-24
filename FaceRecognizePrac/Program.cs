using System;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FaceRecognizePrac
{
    internal class Program
    {
        static void Main()
        {
            MakeRequest();
            Console.WriteLine("Hit ENTER to exit...");
            Console.ReadLine();
        }

        static async void MakeRequest()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "0e986b64db554a15adc436d0c2014f4b");

            // Request parameters
            queryString["returnFaceId"] = "false";
            queryString["returnFaceLandmarks"] = "false";
            queryString["recognitionModel"] = "recognition_04";
            queryString["returnFaceAttributes"] = "glasses, smile, accessories";
            queryString["returnRecognitionModel"] = "false";
            queryString["detectionModel"] = "detection_01";
            queryString["faceIdTimeToLive"] = "86400";
            var uri = "https://westcentralus.api.cognitive.microsoft.com/face/v1.0/detect?" + queryString;

            HttpResponseMessage response;

            // Request body
            var imageUrl = "https://static.wikia.nocookie.net/omniversal-battlefield/images/a/a1/Yagoo.jpg"; // 替换为实际图像的URL
            var requestBody = new { url = imageUrl };
            string jsonBody = JsonConvert.SerializeObject(requestBody);
            byte[] byteData = Encoding.UTF8.GetBytes(jsonBody);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
                Console.WriteLine(response);
                string responseBody = await response.Content.ReadAsStringAsync();
                JArray json = JArray.Parse(responseBody);
                Console.WriteLine(json.ToString());
            }

        }
    }
}
