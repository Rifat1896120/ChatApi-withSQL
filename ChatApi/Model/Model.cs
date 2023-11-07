using System.ComponentModel.DataAnnotations;

namespace ChatApi.Model
{
    public class FriendModel
    {
        [Key] public int Id { get; set; }
        public string username { get; set; }
        public string ownerusername { get; set; }
       
    }
    public class chatMamber
    {
        [Key] public int Id { get; set; }

        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        
    }
    public class Requests
    {
        [Key] public int Id { get; set; }

        public string username { get; set; }
        public DateTime time { get; set; }
    }
    public class friendChatModel
    {
        [Key] public int Id { get; set; }
       
        public string ownerusername { get; set; }

        public string username { get; set; }
        public string chat { get; set; }
        public string seentext { get; set; }
        public DateTime? time { get; set; }
    }
    public class accountInformation
    {
        [Key] public int Id { get; set; }

        public string username { get; set; }
        public string name { get; set; }
        public bool isactive { get; set; }
        public string base64Image { get; set; }
        public string connectionId { get; set; }
    }
}
