﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public interface ISettings
    {
        string DatabaseName { get; }

        string CollectionName { get; }
    }
}