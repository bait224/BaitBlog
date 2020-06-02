namespace FA.JustBlog.Core.Common
{
    public class ResponseData
    {
        public static ResponseModel Response(int Status = 200, string Message = "success", object Data = null)
        {
            return new ResponseModel()
            {
                Status = Status,
                MessageText = Message,
                Data = Data 
            };
        }
    }
}
