﻿using System;

namespace App.Shared.Messages
{
    public abstract class Message : IMessage<Guid>
    {
        public Guid Id { get; set; }

        public Message()
        {
            Id = Guid.NewGuid();
        }
    }
}
