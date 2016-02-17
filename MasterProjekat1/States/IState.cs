using MasterProjekat1.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjekat1.States
{
    public interface IState
    {
        string WindowTitle { get; }
        void Submit(Preduzece pred);
    }
}
