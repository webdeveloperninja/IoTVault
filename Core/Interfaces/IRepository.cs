﻿using Core.Models;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRepository
    {
        Task Add(Plant plant);
    }
}