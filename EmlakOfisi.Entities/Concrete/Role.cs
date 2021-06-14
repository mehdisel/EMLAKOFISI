using EmlakOfisi.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmlakOfisi.Entities.Concrete
{
    public class Role :IdentityRole<int>,IEntity
    {
    }
}
