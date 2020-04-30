using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StoreCli
{
    public interface IMenu
    {
        void PrintMenu();
        void InputLoop();
    }
}