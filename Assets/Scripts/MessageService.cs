using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageService
{
    public delegate void MessageCallback(MessageArguments args);

    private static MessageService _instance = null;
    public static MessageService Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new MessageService();
            }

            return _instance;
        }
    }
    private Dictionary<MessageType, List<MessageCallback>> susbsciberDictionary = new Dictionary<MessageType, List<MessageCallback>>();

    public static void Subscribe(MessageType message, MessageCallback callback)
    {
        Instance.GetMessageList(message).Add(callback);
    }

    public static void Unsubscribe(MessageType message, MessageCallback callback)
    {
        Instance.GetMessageList(message).Remove(callback);
    }

    public static void Send(MessageType message, object arguments = null)
    {
        var messageList = Instance.GetMessageList(message);
        var args = new MessageArguments(arguments);

        for (int i = 0; i < messageList.Count; i++)
        {
            messageList[i].Invoke(args);
        }
    }

    private List<MessageCallback> GetMessageList(MessageType message)
    {
        List<MessageCallback> messageList;
        try
        {
            messageList = susbsciberDictionary[message];
        }
        catch (KeyNotFoundException)
        {
            messageList = new List<MessageCallback>();
            susbsciberDictionary[message] = messageList;
        }
        return messageList;
    }
}

public class MessageArguments
{
    public object content = null;

    public MessageArguments(object content)
    {
        this.content = content;
    }
}

[System.Flags]
public enum MessageType
{
    Target_Hit = 0x00,
    Knife_Hit = 0x01
}