using System.Text.Json;

namespace Data
{
    public class Logger
    {
        private object _lock = new object();

        public void SaveLogsToFile(Ball ball)
        {
            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var objectToSerialize = new
            {
                Timestamp = DateTime.Now,
                Ball = ball
            };

            string json = JsonSerializer.Serialize(objectToSerialize, jsonOptions);

            lock (_lock)
            {
                File.AppendAllText(Path.GetFullPath(@"..\..\..\..\logs.json"), json);
            }
        }
    }
}
