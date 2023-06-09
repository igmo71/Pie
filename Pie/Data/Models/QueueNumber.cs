namespace Pie.Data.Models
{
    public class QueueNumber
    {
        public int CharValue { get; set; }
        public int NumValue { get; set; }
        public string? Value { get; set; }

        public QueueNumber Next(QueueNumber currentQueueNumber)
        {
            //string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string alphabet = "ABCEHKMOPT";

            var newQueueNumber = new QueueNumber()
            {
                NumValue = currentQueueNumber.NumValue,
                CharValue = currentQueueNumber.CharValue,
                Value = currentQueueNumber.Value
            };

            newQueueNumber.NumValue = (newQueueNumber.NumValue + 1) % 1000;

            if (newQueueNumber.NumValue == 0)
            {
                newQueueNumber.NumValue++;
                newQueueNumber.CharValue = (newQueueNumber.CharValue + 1) % alphabet.Length;
            }

            newQueueNumber.Value = $"{alphabet[newQueueNumber.CharValue]}{newQueueNumber.NumValue:D3}";

            return newQueueNumber;
        }
    }
}
