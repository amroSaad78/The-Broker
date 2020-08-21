namespace WebMVC.Model
{ 
    public class Messages
    {
        public ToastTypes Type { get; private set; }
        public string Message { get; private set; }
        public string Title { get; private set; }

        public void SetMessage(ToastTypes type, string msg, string title= "Error")
        {
            this.Title = title;
            this.Message = msg;
            this.Type = type;
        }
    }

    public enum ToastTypes
    {        
        success,
        info,
        warning,
        error
    }
}
