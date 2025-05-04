namespace daily_note_be.Models
{
    public class ResponseResult
    {
        public ResponseResult()
        {
            Success = true;
        }
        public bool Success { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
        public int Pages { get; set; }
    }
}
