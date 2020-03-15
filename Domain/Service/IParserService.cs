﻿using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service
{
    public interface IParserService<T> where T: class, IDisposable
    {
        IEnumerable<string> GetEvent(T instance);
        IEnumerable<EventModel> GetEventDetails(T instance);
    }
}