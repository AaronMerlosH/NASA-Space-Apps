
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace SpaceAppsCustomVision
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Introduce direccion local de la imagen que desea leer: ");
            string imageFilePath = Console.ReadLine();

            string path = @"C:\Users\aaron\OneDrive\Documentos\apuntes\SpaceApps\Predictions.txt";
            string Text = "Hello, Hi, ByeBye";
            File.WriteAllText(path, Text);

            MakePredictionRequest(imageFilePath).Wait();
            
            

            Console.WriteLine("\n\nEnter para finalizar lectura ");
            Console.ReadLine();
        }

        public static async Task MakePredictionRequest(string imageFilePath)
        {
            var client = new HttpClient();

            //Se introducen las clave de acceso API
            client.DefaultRequestHeaders.Add("Prediction-Key", "8f35c3d822c04726967c3f6837ebe13d");

            // URL del modelo ya creado y entrenado de customvision
            string url = "https://01test-prediction.cognitiveservices.azure.com/customvision/v3.0/Prediction/99591b4c-bd1e-4eaf-b765-8c0522c3a360/classify/iterations/SpaceAppsCustomVision/image";

            HttpResponseMessage response;

            //exportacion de la imagen local
            byte[] byteData = GetImageAsByteArray(imageFilePath);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(url, content);
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }

        private static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }
        
    }
}