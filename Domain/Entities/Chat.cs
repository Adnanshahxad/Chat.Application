using System;

namespace Domain.Entities;

public class Chat
{
    public Guid Id { get; set; }
    public string Message { get; set; }

    public DateTime DateTime { get; set; }

    //This could be user table reference but for demo purpose using as user name
    public string UserName { get; set; }

    //This could be another table reference in database but for demo purpose using as string name
    public string ChatRoom { get; set; }
}