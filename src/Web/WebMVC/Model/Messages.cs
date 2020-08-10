namespace WebMVC.Model
{ 
    public class Messages
    {
        public ToastTypes Type { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
    }

    public enum ToastTypes
    {
        none,
        success,
        info,
        warning,
        error
    }
}
