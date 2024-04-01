

namespace Epicor.App.Helpers
{
    public  class MenuItem
    {
     
        public string Icon { get; set; }
        public string Text { get; set; }
        public Type UserControlType { get; set; }
        public string Title { get; set; }

      
        public MenuItem( string text,string icon,string title, Type userControlType)
        {
            Text = text;
            Icon = icon;
            Title = title;
            UserControlType = userControlType ;
        }

    }
}
